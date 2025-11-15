using Spinner.Attribute;
using Spinner.Enums;
using Spinner.Internals.Guards;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using ZLinq;

namespace Spinner
{
    /// <summary>
    /// Spinner object that abstract all rule to read or write an string.
    /// Thread-safe and stateless implementation.
    /// </summary>
    /// <typeparam name="T">The type of object to write or read.</typeparam>
    public sealed class SpinnerV7<T> where T : new()
    {
        private static readonly Tuple<PropertyInfo, WritePropertyAttribute, Func<T, string>>[] WriteProperties;

        // Cached metadata - computed once per type T
        private static readonly ObjectMapperAttribute ReadObjectMapper;
        //private static readonly PropertyInfo[] WriteProperties;
        private static readonly PropertyInfo[] ReadProperties;


        /// <summary>
        /// Static constructor - executes all expensive reflection logic once per type T.
        /// </summary>
        static SpinnerV7()
        {
            var allProperties = typeof(T).GetProperties();

            // Cache ObjectMapper attribute
            ReadObjectMapper = (ObjectMapperAttribute)typeof(T)
                .GetCustomAttributes(typeof(ObjectMapperAttribute), false)
                .FirstOrDefault();

            // Cache ReadProperties and their attributes
            ReadProperties = allProperties
                .AsValueEnumerable()
                .Where(p => p.GetCustomAttributes(typeof(ReadPropertyAttribute), false).Length > 0)
                .ToArray();


            var p = typeof(T)
              .GetProperties()
              .AsValueEnumerable()
              .Where(PredicateForWriteProperty())
              .OrderBy(PredicateForOrderByWriteProperty())
              .ToArray();

            WriteProperties = new Tuple<PropertyInfo, WritePropertyAttribute, Func<T, string>>[p.Length];

            for (int i = 0; i < p.Length; i++)
            {
                var wp = GetWriteProperty(p[i]);

                Guard.WriteProperty.NotMapped(p[i], wp);

                WriteProperties[i] = new Tuple<PropertyInfo, WritePropertyAttribute, Func<T, string>>(p[i], wp, CreateDelegate(p[i]));
            }
        }

        private static WritePropertyAttribute GetWriteProperty(PropertyInfo info) =>
           info
               .GetCustomAttributes(typeof(WritePropertyAttribute), false)
                .AsValueEnumerable()
               .Cast<WritePropertyAttribute>()
               .FirstOrDefault();

        ///// <summary>
        ///// Default constructor - no state, fully reusable and thread-safe.
        ///// All expensive reflection logic is executed in the static constructor.
        ///// </summary>
        //public SpinnerV6()
        //{
        //    // No initialization needed - all work is done in static constructor
        //}

        /// <summary>
        /// Get configuration property of T.
        /// </summary>
        //public ObjectMapperAttribute GetObjectMapper => ReadObjectMapper;

        ///// <summary>
        ///// Get all properties with WriteProperty decoration present in T.
        ///// </summary>
        //public IImmutableList<PropertyInfo> GetWriteProperties => ImmutableWriteProperties;

        ///// <summary>
        ///// Get all properties with ReadProperty decoration present in T.
        ///// </summary>
        //public IImmutableList<PropertyInfo> GetReadProperties => ImmutableReadProperties;

        /// <summary>
        /// Convert T in a positional string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Returns a string mapped of T.</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public string WriteAsString(T obj)
        {
            StringBuilder builder = new StringBuilder(ReadObjectMapper.Length);

            builder.Clear();

            //PooledStringBuilder sb = PooledStringBuilder.GetInstance();

            WritePositionalString(ref builder, obj);

            return ReadObjectMapper is not null
                    ? builder.ToString(0, ReadObjectMapper.Length)
                    : builder.ToString();
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

            //for (int i = 0; i < ReadProperties.Length; i++)
            //{
            //    ref PropertyInfo property = ref ReadProperties[i];
            //    ReadPropertyAttribute attribute = ReadPropertyCache[property];

            //    Guard.ReadProperty.NotMapped(property, attribute);

            //    var slice = text.Slice(attribute.Start, attribute.Length);

            //    // Manual trim to avoid allocation
            //    int start = 0;
            //    int end = slice.Length - 1;

            //    while (start <= end && char.IsWhiteSpace(slice[start]))
            //        start++;

            //    while (end >= start && char.IsWhiteSpace(slice[end]))
            //        end--;

            //    var trimmedSlice = start <= end ? slice.Slice(start, end - start + 1) : ReadOnlySpan<char>.Empty;

            //    if (attribute.Type is not null)
            //    {
            //        if (!InterceptorCache.TryGet(attribute.Type.Name, out IInterceptor interceptor))
            //        {
            //            interceptor = (IInterceptor)Activator.CreateInstance(attribute.Type);
            //            InterceptorCache.Add(attribute.Type.Name, interceptor);
            //        }

            //        property.SetValue(obj, interceptor.Parse(new string(trimmedSlice)));
            //    }
            //    else
            //    {
            //        property.SetValue(obj, new string(trimmedSlice));
            //    }
            //}

            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void WritePositionalString(ref StringBuilder sb, T obj)
        {
            for (int i = 0; i < WriteProperties.Length; i++)
            {
                var prop = WriteProperties[i];
                string str = prop.Item3(obj);
                var atr = prop.Item2;

                int len = atr.Length;

                //var value = prop.Item1.GetValue(obj) as string;
                //int valueLength = value.Length;

                //var attr = prop.Item2;
                //int targetLength = attr.Length;

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
                .AsValueEnumerable()
                .FirstOrDefault())?.Order ?? default;
        }

        private static Func<PropertyInfo, bool> PredicateForWriteProperty()
        {
            return (prop) => prop
                .GetCustomAttributes(typeof(WritePropertyAttribute), false)
                .AsValueEnumerable()
                .All(attribute => attribute is WritePropertyAttribute);
        }

        public static Func<T, string> CreateDelegate(PropertyInfo propertyInfo)
        {
            var getMethod = propertyInfo.GetGetMethod();
            return (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), null, getMethod);
        }
    }
}