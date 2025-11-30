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
        private static StringBuilder _builder;

        private static readonly (PropertyInfo PropertyInfo, WritePropertyAttribute Attribute, Func<T, string> Getter)[] WriteProperties;
        private static readonly (PropertyInfo PropertyInfo, ReadPropertyAttribute Attribute, Delegate Setter)[] ReadProperties;
        private static readonly ObjectMapperAttribute ReadObjectMapper;

        static Spinner()
        {
            var allProperties = typeof(T).GetProperties();

            ReadObjectMapper = (ObjectMapperAttribute)typeof(T)
                .GetCustomAttributes(typeof(ObjectMapperAttribute), false)
                .FirstOrDefault();

            var readProps = allProperties
                .Where(PredicateForReadProperty())
                .ToArray();

            var writeProps = typeof(T)
              .GetProperties()
              .Where(PredicateForWriteProperty())
              .OrderBy(PredicateForOrderByWriteProperty())
              .ToArray();

            ReadProperties = new (PropertyInfo, ReadPropertyAttribute, Delegate)[readProps.Length];
            WriteProperties = new (PropertyInfo, WritePropertyAttribute, Func<T, string>)[writeProps.Length];

            for (int i = 0; i < readProps.Length; i++)
            {
                var prop = readProps[i];
                var readAttr = (ReadPropertyAttribute)prop.GetCustomAttributes(typeof(ReadPropertyAttribute), false)[0];
                ReadProperties[i] = (prop, readAttr, CreateTypedSetDelegate(prop));
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
        public ObjectMapperAttribute GetObjectMapper => ReadObjectMapper;

        /// <summary>
        /// Gets or creates the thread-local StringBuilder instance.
        /// </summary>
        private static StringBuilder GetStringBuilder()
        {
            var sb = _builder;
            if (sb == null)
            {
                sb = new StringBuilder(ReadObjectMapper?.Length ?? 256);
                _builder = sb;
            }
            return sb;
        }

        /// <summary>
        /// Convert T in a positional string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Returns a string mapped of T.</returns>
        public string WriteAsString(T obj)
        {
            var sb = GetStringBuilder();
            sb.Clear();

            WritePositionalString(ref sb, obj);

            return ReadObjectMapper is not null
                    ? sb.ToString(0, ReadObjectMapper.Length)
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
                var (prop, attribute, setter) = ReadProperties[i];

                var trimmedSlice = text.Slice(attribute.Start, attribute.Length).Trim();

                if (attribute.Type is not null)
                {
                    var interceptor = InterceptorCache.GetOrAdd(attribute.Type);
                    InvokeTypedSetter(setter, obj, interceptor.Parse(trimmedSlice.ToString()), prop.PropertyType);
                }
                else
                {
                    InvokeTypedSetter(setter, obj, new string(trimmedSlice), prop.PropertyType);
                }
            }

            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void WritePositionalString(ref StringBuilder sb, T obj)
        {
            Guard.WriteProperty.NotMapped<T>(WriteProperties.Length);

            for (int i = 0; i < WriteProperties.Length; i++)
            {
                var (_, atr, getter) = WriteProperties[i];
                string str = getter(obj);

                int len = atr.Length;

                if (str.Length >= len)
                {
                    sb.Append(str, 0, len);
                }
                else
                {
                    if (atr.Padding == PaddingType.Left)
                    {
                        sb.Append(atr.PaddingChar, len - str.Length);
                    }

                    sb.Append(str);

                    if (atr.Padding == PaddingType.Right)
                    {
                        sb.Append(atr.PaddingChar, len - str.Length);
                    }
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InvokeTypedSetter(Delegate setter, T instance, object value, Type propertyType)
        {
            if (propertyType == typeof(string))
            {
                ((Action<T, string>)setter)(instance, value as string ?? value.ToString());
            }
            else if (propertyType == typeof(int))
            {
                ((Action<T, int>)setter)(instance, value is int intVal ? intVal : int.Parse(value.ToString()));
            }
            else if (propertyType == typeof(long))
            {
                ((Action<T, long>)setter)(instance, value is long longVal ? longVal : long.Parse(value.ToString()));
            }
            else if (propertyType == typeof(decimal))
            {
                ((Action<T, decimal>)setter)(instance, value is decimal decVal ? decVal : decimal.Parse(value.ToString()));
            }
            else if (propertyType == typeof(DateTime))
            {
                ((Action<T, DateTime>)setter)(instance, value is DateTime dtVal ? dtVal : DateTime.Parse(value.ToString()));
            }
            else if (propertyType == typeof(bool))
            {
                ((Action<T, bool>)setter)(instance, value is bool boolVal ? boolVal : bool.Parse(value.ToString()));
            }
            else if (propertyType == typeof(TimeSpan))
            {
                ((Action<T, TimeSpan>)setter)(instance, value is TimeSpan tsVal ? tsVal : TimeSpan.Parse(value.ToString()));
            }
            else if (propertyType == typeof(byte))
            {
                ((Action<T, byte>)setter)(instance, value is byte byteVal ? byteVal : byte.Parse(value.ToString()));
            }
            else if (propertyType == typeof(sbyte))
            {
                ((Action<T, sbyte>)setter)(instance, value is sbyte sbyteVal ? sbyteVal : sbyte.Parse(value.ToString()));
            }
            else if (propertyType == typeof(short))
            {
                ((Action<T, short>)setter)(instance, value is short shortVal ? shortVal : short.Parse(value.ToString()));
            }
            else if (propertyType == typeof(ushort))
            {
                ((Action<T, ushort>)setter)(instance, value is ushort ushortVal ? ushortVal : ushort.Parse(value.ToString()));
            }
            else if (propertyType == typeof(uint))
            {
                ((Action<T, uint>)setter)(instance, value is uint uintVal ? uintVal : uint.Parse(value.ToString()));
            }
            else if (propertyType == typeof(ulong))
            {
                ((Action<T, ulong>)setter)(instance, value is ulong ulongVal ? ulongVal : ulong.Parse(value.ToString()));
            }
            else if (propertyType == typeof(float))
            {
                ((Action<T, float>)setter)(instance, value is float floatVal ? floatVal : float.Parse(value.ToString()));
            }
            else if (propertyType == typeof(double))
            {
                ((Action<T, double>)setter)(instance, value is double doubleVal ? doubleVal : double.Parse(value.ToString()));
            }
            else if (propertyType == typeof(char))
            {
                ((Action<T, char>)setter)(instance, value is char charVal ? charVal : char.Parse(value.ToString()));
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