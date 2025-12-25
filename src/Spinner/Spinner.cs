using NoAlloq;
using Spinner.Attribute;
using Spinner.Enums;
using Spinner.Internals.Cache;
using Spinner.Internals.Guards;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Spinner
{
    public sealed class Spinner<T> where T : new()
    {
        [ThreadStatic]
        private static StringBuilder builder;

        private static readonly (PropertyInfo PropertyInfo, WritePropertyAttribute Attribute, Func<T, string> Getter)[] WriteProperties;
        private static readonly (PropertyInfo PropertyInfo, ReadPropertyAttribute Attribute, Delegate Setter, Func<string, object> Parser)[] ReadProperties;
        private static readonly ObjectMapperAttribute _objectMapperAttribute;

        static Spinner()
        {
            var allProperties = typeof(T).GetProperties();

            _objectMapperAttribute = (ObjectMapperAttribute)typeof(T)
                .GetCustomAttributes(typeof(ObjectMapperAttribute), false)
                .FirstOrDefault();

            if (_objectMapperAttribute is not null)
            {
                Guard.ObjectMapper.ValidateLength<T>(_objectMapperAttribute.Length);
            }

            var readProps = allProperties
                .Where(PredicateForReadProperty())
                .ToArray();

            var writeProps = typeof(T)
              .GetProperties()
              .Where(PredicateForWriteProperty())
              .OrderBy(PredicateForOrderByWriteProperty())
              .ToArray();

            ReadProperties = new (PropertyInfo, ReadPropertyAttribute, Delegate, Func<string, object>)[readProps.Length];
            WriteProperties = new (PropertyInfo, WritePropertyAttribute, Func<T, string>)[writeProps.Length];

            for (int i = 0; i < readProps.Length; i++)
            {
                var prop = readProps[i];
                var readAttr = (ReadPropertyAttribute)prop.GetCustomAttributes(typeof(ReadPropertyAttribute), false)[0];
                var parser = readAttr.Type is not null ? CreateParserDelegate(readAttr.Type, prop.PropertyType) : null;
                ReadProperties[i] = (prop, readAttr, CreateTypedSetDelegate(prop), parser);
            }

            for (int i = 0; i < writeProps.Length; i++)
            {
                var prop = writeProps[i];
                var writeAttr = (WritePropertyAttribute)prop.GetCustomAttributes(typeof(WritePropertyAttribute), false)[0];
                WriteProperties[i] = (prop, writeAttr, CreateGetDelegate(prop));
            }
        }

        /// <summary>
        /// Get configuration property of T.
        /// </summary>
        public ObjectMapperAttribute GetObjectMapper => _objectMapperAttribute;

        /// <summary>
        /// Convert T in a positional string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Returns a string mapped of T.</returns>
        public string WriteAsString(T obj)
        {
            var sb = builder;
            if (sb == null)
            {
                sb = builder = new StringBuilder(_objectMapperAttribute?.Length ?? 256);
            }
            else
            {
                sb.Clear();
            }

            WritePositionalString(obj, ref sb);

            return _objectMapperAttribute is not null
                    ? sb.ToString(0, _objectMapperAttribute.Length)
                    : sb.ToString();
        }

        /// <summary>
        /// Convert T in a positional string as span.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Return an string mapped of T as span.</returns>
        public ReadOnlySpan<char> WriteAsSpan(T obj)
        {
            var str = WriteAsString(obj);
            return str.AsSpan();
        }

        /// <summary>
        /// Convert string in an object.
        /// </summary>
        /// <param name="value">Positional string to map in an object.</param>
        /// <returns>Returns instance of T.</returns>
        public T ReadFromString(string value)
        {
            return ReadPositionalString(value.AsSpan());
        }

        /// <summary>
        /// Convert string in an object.
        /// </summary>
        /// <param name="value">Span with data to map an object.</param>
        /// <returns>Returns instance of T.</returns>
        public T ReadFromSpan(ReadOnlySpan<char> value)
        {
            return ReadPositionalString(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static T ReadPositionalString(ReadOnlySpan<char> text)
        {
            Guard.ReadProperty.NotMapped<T>(ReadProperties.Length);

            var obj = new T();

            for (int i = 0; i < ReadProperties.Length; i++)
            {
                var (prop, attribute, setter, parser) = ReadProperties[i];

                var trimmedSlice = text.Slice(attribute.Start, attribute.Length).Trim();

                if (parser is not null)
                {
                    InvokeTypedSetter(
                        setter,
                        obj,
                        parser(trimmedSlice.ToString()),
                        prop.PropertyType
                    );
                }
                else
                {
                    InvokeTypedSetter(
                        setter,
                        obj,
                        new string(trimmedSlice),
                        prop.PropertyType
                    );
                }
            }

            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void WritePositionalString(T obj, ref StringBuilder sb)
        {
            Guard.WriteProperty.NotMapped<T>(WriteProperties.Length);

            for (int i = 0; i < WriteProperties.Length; i++)
            {
                var (_, atr, getter) = WriteProperties[i];
                string str = getter(obj);

                int len = atr.Length;

                // Stryker disable all: it's not possible to write a test that covers this case from >= to >
                if (str.Length >= len)
                {
                    sb.Append(str, 0, len);
                    continue;
                }

                if (atr.Padding == PaddingType.Left)
                {
                    sb.Append(atr.PaddingChar, len - str.Length);
                    sb.Append(str);
                }

                if (atr.Padding == PaddingType.Right)
                {
                    sb.Append(str);
                    sb.Append(atr.PaddingChar, len - str.Length);
                }
            }
        }

        private static Func<PropertyInfo, ushort> PredicateForOrderByWriteProperty()
        {
            return (prop) => ((WritePropertyAttribute)prop
             .GetCustomAttributes(typeof(WritePropertyAttribute), false)
             .FirstOrDefault())?.Order ?? default;
        }

        private static Func<PropertyInfo, bool> PredicateForWriteProperty()
        {
            return (prop) => prop
                .GetCustomAttributes(typeof(WritePropertyAttribute), false)
                .Any(attribute => attribute is WritePropertyAttribute);
        }

        private static Func<PropertyInfo, bool> PredicateForReadProperty()
        {
            return (prop) => prop
                .GetCustomAttributes(typeof(ReadPropertyAttribute), false)
                .Any(attribute => attribute is ReadPropertyAttribute);
        }

        public static Func<T, string> CreateGetDelegate(PropertyInfo propertyInfo)
        {
            return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), null, propertyInfo.GetGetMethod());
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Delegate CreateTypedSetDelegate(PropertyInfo propertyInfo)
        {
            var setMethod = propertyInfo.GetSetMethod() ?? throw new InvalidOperationException($"Property {propertyInfo.Name} does not have a setter.");
            var propertyType = propertyInfo.PropertyType;

            var delegateType = typeof(Action<,>).MakeGenericType(typeof(T), propertyType);
            return Delegate.CreateDelegate(delegateType, setMethod);
        }

        private static Func<string, object> CreateParserDelegate(Type interceptorType, Type propertyType)
        {
            var interceptorInterfaceType = interceptorType
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(Interceptors.IInterceptor<>))
                ?? throw new InvalidOperationException($"Type {interceptorType.FullName} does not implement IInterceptor<T>");

            var returnType = interceptorInterfaceType.GetGenericArguments()[0];

            var createParserMethod = typeof(Spinner<T>)
                .GetMethod(nameof(CreateTypedParser), BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(returnType);

            return (Func<string, object>)createParserMethod.Invoke(null, new object[] { interceptorType });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Func<string, object> CreateTypedParser<TResult>(Type interceptorType)
        {
            var interceptor = InterceptorCache.GetOrAdd<TResult>(interceptorType);
            return (string value) => interceptor.Parse(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InvokeTypedSetter(Delegate setter, T instance, object value, Type propertyType)
        {
            switch (Type.GetTypeCode(propertyType))
            {
                case TypeCode.String:
                    InvokeAction<string>(setter, instance, value.ToString());
                    break;
                case TypeCode.Int32:
                    InvokeAction<int>(setter, instance, ConvertValue<int>(value));
                    break;
                case TypeCode.Int64:
                    InvokeAction<long>(setter, instance, ConvertValue<long>(value));
                    break;
                case TypeCode.Decimal:
                    InvokeAction<decimal>(setter, instance, ConvertValue<decimal>(value));
                    break;
                case TypeCode.DateTime:
                    InvokeAction<DateTime>(setter, instance, ConvertValue<DateTime>(value));
                    break;
                case TypeCode.Boolean:
                    InvokeAction<bool>(setter, instance, ConvertValue<bool>(value));
                    break;
                case TypeCode.Byte:
                    InvokeAction<byte>(setter, instance, ConvertValue<byte>(value));
                    break;
                case TypeCode.SByte:
                    InvokeAction<sbyte>(setter, instance, ConvertValue<sbyte>(value));
                    break;
                case TypeCode.Int16:
                    InvokeAction<short>(setter, instance, ConvertValue<short>(value));
                    break;
                case TypeCode.UInt16:
                    InvokeAction<ushort>(setter, instance, ConvertValue<ushort>(value));
                    break;
                case TypeCode.UInt32:
                    InvokeAction<uint>(setter, instance, ConvertValue<uint>(value));
                    break;
                case TypeCode.UInt64:
                    InvokeAction<ulong>(setter, instance, ConvertValue<ulong>(value));
                    break;
                case TypeCode.Single:
                    InvokeAction<float>(setter, instance, ConvertValue<float>(value));
                    break;
                case TypeCode.Double:
                    InvokeAction<double>(setter, instance, ConvertValue<double>(value));
                    break;
                case TypeCode.Char:
                    InvokeAction<char>(setter, instance, ConvertValue<char>(value));
                    break;
                default:
                    InvokeSetterForOtherTypes(setter, instance, value, propertyType);
                    break;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InvokeAction<TValue>(Delegate setter, T instance, TValue value)
        {
            ((Action<T, TValue>)setter)(instance, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TValue ConvertValue<TValue>(object value) where TValue : struct, IParsable<TValue>
        {
            return value is TValue typedValue ? typedValue : TValue.Parse(value.ToString(), null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InvokeSetterForOtherTypes(Delegate setter, T instance, object value, Type propertyType)
        {
            if (propertyType == typeof(TimeSpan))
            {
                ((Action<T, TimeSpan>)setter)(instance, ConvertValue<TimeSpan>(value));
            }
            else if (propertyType == typeof(nint))
            {
                ((Action<T, nint>)setter)(instance, value is nint nintVal ? nintVal : nint.Parse(value.ToString()));
            }
            else if (propertyType == typeof(nuint))
            {
                ((Action<T, nuint>)setter)(instance, value is nuint nuintVal ? nuintVal : nuint.Parse(value.ToString()));
            }
            else
            {
                if (value.GetType() != propertyType)
                {
                    value = Convert.ChangeType(value, propertyType);
                }

                setter.DynamicInvoke(instance, value);
            }
        }
    }
}