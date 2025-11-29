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
        private static readonly (PropertyInfo PropertyInfo, ReadPropertyAttribute Attribute, Action<T, string> Setter)[] ReadProperties;
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

            ReadProperties = new (PropertyInfo, ReadPropertyAttribute, Action<T, string>)[readProps.Length];
            WriteProperties = new (PropertyInfo, WritePropertyAttribute, Func<T, string>)[writeProps.Length];

            for (int i = 0; i < readProps.Length; i++)
            {
                var prop = readProps[i];
                var readAttr = (ReadPropertyAttribute)prop.GetCustomAttributes(typeof(ReadPropertyAttribute), false)[0];
                ReadProperties[i] = (prop, readAttr, CreateSpanSetDelegate(prop));
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
        /// Convert T in a positional string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Returns a string mapped of T.</returns>
        public string WriteAsString(T obj)
        {
            var sb = builder ??= new StringBuilder(ReadObjectMapper?.Length ?? 256);
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
                    setter(obj, interceptor.Parse(trimmedSlice.ToString()));
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
        private static Action<T, string> CreateSpanSetDelegate(PropertyInfo propertyInfo)
        {
            var setMethod = propertyInfo.GetSetMethod() ?? throw new InvalidOperationException($"Property {propertyInfo.Name} does not have a setter.");
            var propertyType = propertyInfo.PropertyType;

            return propertyType switch
            {
                Type t when t == typeof(string) =>
                    (Action<T, string>)Delegate.CreateDelegate(typeof(Action<T, string>), setMethod),

                Type t when t == typeof(int) =>
                    CreateTypedSetter(setMethod, int.Parse),

                Type t when t == typeof(long) =>
                    CreateTypedSetter(setMethod, long.Parse),

                Type t when t == typeof(decimal) =>
                    CreateTypedSetter(setMethod, decimal.Parse),

                Type t when t == typeof(DateTime) =>
                    CreateTypedSetter(setMethod, DateTime.Parse),

                Type t when t == typeof(bool) =>
                    CreateTypedSetter(setMethod, bool.Parse),

                Type t when t == typeof(TimeSpan) =>
                    CreateTypedSetter(setMethod, TimeSpan.Parse),

                Type t when t == typeof(byte) =>
                    CreateTypedSetter(setMethod, byte.Parse),

                Type t when t == typeof(sbyte) =>
                    CreateTypedSetter(setMethod, sbyte.Parse),

                Type t when t == typeof(short) =>
                    CreateTypedSetter(setMethod, short.Parse),

                Type t when t == typeof(ushort) =>
                    CreateTypedSetter(setMethod, ushort.Parse),

                Type t when t == typeof(uint) =>
                    CreateTypedSetter(setMethod, uint.Parse),

                Type t when t == typeof(ulong) =>
                    CreateTypedSetter(setMethod, ulong.Parse),

                Type t when t == typeof(float) =>
                    CreateTypedSetter(setMethod, float.Parse),

                Type t when t == typeof(double) =>
                    CreateTypedSetter(setMethod, double.Parse),

                Type t when t == typeof(char) =>
                    CreateTypedSetter(setMethod, char.Parse),

                Type t when t == typeof(nint) =>
                    CreateTypedSetter(setMethod, nint.Parse),

                Type t when t == typeof(nuint) =>
                    CreateTypedSetter(setMethod, nuint.Parse),

                _ => CreateGenericSetter(setMethod, propertyType)
            };
        }

        private static Action<T, string> CreateTypedSetter<TValue>(MethodInfo setMethod, Func<string, TValue> parser)
        {
            var typedSetter = (Action<T, TValue>)Delegate.CreateDelegate(typeof(Action<T, TValue>), setMethod);
            return (instance, str) => typedSetter(instance, parser(str));
        }

        private static Action<T, string> CreateGenericSetter(MethodInfo setMethod, Type propertyType)
        {
            // Use reflection to create a strongly - typed delegate and avoid boxing
            var typedSetter = Delegate.CreateDelegate(typeof(Action<T, object>), setMethod);

            return (instance, str) =>
            {
                object value = Convert.ChangeType(str, propertyType);
                ((Action<T, object>)typedSetter)(instance, value);
            };
        }
    }
}