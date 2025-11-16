using NoAlloq;
using Spinner.Attribute;
using Spinner.Enums;
using Spinner.Interceptors;
using Spinner.Internals.Cache;
using Spinner.Internals.Guards;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Spinner
{
    public sealed class SpinnerV2<T> where T : new()
    {
        [ThreadStatic]
        private static StringBuilder builder;

        private static readonly (PropertyInfo PropertyInfo, WritePropertyAttribute Attribute, Func<T, string> Getter)[] WriteProperties;
        private static readonly (PropertyInfo PropertyInfo, ReadPropertyAttribute Attribute, Action<T, string> Setter)[] ReadProperties;
        private static readonly ObjectMapperAttribute ReadObjectMapper;

        static SpinnerV2()
        {
            var allProperties = typeof(T).GetProperties();

            ReadObjectMapper = (ObjectMapperAttribute)typeof(T)
                .GetCustomAttributes(typeof(ObjectMapperAttribute), false)
                .FirstOrDefault();

            var readProps = allProperties
                .Where(PredicateForReadProperty())
                .ToArray();

            builder ??= new StringBuilder(ReadObjectMapper?.Length ?? 256);

            var writeProps = typeof(T)
              .GetProperties()
              .Where(PredicateForWriteProperty())
              .OrderBy(PredicateForOrderByWriteProperty())
              .ToArray();

            ReadProperties = new (PropertyInfo, ReadPropertyAttribute, Action<T, string>)[readProps.Length];
            WriteProperties = new (PropertyInfo, WritePropertyAttribute, Func<T, string>)[writeProps.Length];

            for (int i = 0; i < readProps.Length; i++)
            {
                var prop = readProps[i];
                var readAttr = (ReadPropertyAttribute)prop.GetCustomAttributes(typeof(ReadPropertyAttribute), false)[0];
                Guard.ReadProperty.NotMapped(prop, readAttr);
                ReadProperties[i] = (prop, readAttr, CreateSpanSetDelegate(prop));
            }

            for (int i = 0; i < writeProps.Length; i++)
            {
                var prop = writeProps[i];
                var writeAttr = (WritePropertyAttribute)prop.GetCustomAttributes(typeof(WritePropertyAttribute), false)[0];
                Guard.WriteProperty.NotMapped(prop, writeAttr);
                WriteProperties[i] = (prop, writeAttr, CreateGetDelegate(prop));
            }
        }

        /// <summary>
        /// Get configuration property of T.
        /// </summary>
        public ObjectMapperAttribute GetObjectMapper => ReadObjectMapper;

        /// <summary>
        /// Convert T in a positional string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Returns a string mapped of T.</returns>
        public string WriteAsString(T obj)
        {
            var sb = builder;
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
            var obj = new T();

            for (int i = 0; i < ReadProperties.Length; i++)
            {
                var (_, attribute, setter) = ReadProperties[i];

                var trimmedSlice = text.Slice(attribute.Start, attribute.Length).Trim();

                if (attribute.Type is not null)
                {
                    var interceptor = InterceptorCache.GetOrAdd(attribute.Type);

                    if (interceptor is ISpanInterceptor spanInterceptor)
                    {
                        setter(obj, new string(trimmedSlice));
                    }
                    else
                    {
                        setter(obj, new string(trimmedSlice));
                    }
                }
                else
                {
                    setter(obj, new string(trimmedSlice));
                }
            }

            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void WritePositionalString(ref StringBuilder sb, T obj)
        {
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
                .GetCustomAttributes(typeof(WritePropertyAttribute), false)[0])?.Order ?? default;
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

        private static Action<T, string> CreateSpanSetDelegate(PropertyInfo propertyInfo)
        {
            var setMethod = propertyInfo.GetSetMethod();

            if (setMethod is null)
            {
                throw new InvalidOperationException($"Property {propertyInfo.Name} does not have a setter.");
            }

            var propertyType = propertyInfo.PropertyType;

            if (propertyType == typeof(string))
            {
                var typedSetter = (Action<T, string>)Delegate.CreateDelegate(typeof(Action<T, string>), setMethod);
                return (instance, str) => typedSetter(instance, str);
            }
            else if (propertyType == typeof(int))
            {
                var typedSetter = (Action<T, int>)Delegate.CreateDelegate(typeof(Action<T, int>), setMethod);
                return (instance, str) => typedSetter(instance, int.Parse(str));
            }
            else if (propertyType == typeof(long))
            {
                var typedSetter = (Action<T, long>)Delegate.CreateDelegate(typeof(Action<T, long>), setMethod);
                return (instance, str) => typedSetter(instance, long.Parse(str));
            }
            else if (propertyType == typeof(decimal))
            {
                var typedSetter = (Action<T, decimal>)Delegate.CreateDelegate(typeof(Action<T, decimal>), setMethod);
                return (instance, str) => typedSetter(instance, decimal.Parse(str));
            }
            else if (propertyType == typeof(DateTime))
            {
                var typedSetter = (Action<T, DateTime>)Delegate.CreateDelegate(typeof(Action<T, DateTime>), setMethod);
                return (instance, str) => typedSetter(instance, DateTime.Parse(str));
            }
            else if (propertyType == typeof(bool))
            {
                var typedSetter = (Action<T, bool>)Delegate.CreateDelegate(typeof(Action<T, bool>), setMethod);
                return (instance, str) => typedSetter(instance, bool.Parse(str));
            }
            else
            {
                var delegateType = typeof(Action<,>).MakeGenericType(typeof(T), propertyType);
                var typedDelegate = Delegate.CreateDelegate(delegateType, setMethod);

                return (instance, str) =>
                {
                    var value = Convert.ChangeType(str, propertyType);
                    typedDelegate.DynamicInvoke(instance, value);
                };
            }
        }
    }
}
