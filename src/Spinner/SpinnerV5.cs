using Microsoft.VisualStudio.Utilities;
using Spinner.Attribute;
using Spinner.Enums;
using Spinner.Interceptors;
using Spinner.Internals.Cache;
using Spinner.Internals.Guards;
using System;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Spinner
{
    /// <summary>
    /// Spinner object that abstract all rule to read or write an string.
    /// Thread-safe and stateless implementation.
    /// </summary>
    /// <typeparam name="T">The type of object to write or read.</typeparam>
    public sealed class SpinnerV5<T> where T : new()
    {
        // Cached metadata - computed once per type T
        private static readonly ObjectMapperAttribute ReadObjectMapper;
        private static readonly PropertyInfo[] WriteProperties;
        private static readonly PropertyInfo[] ReadProperties;
        private static readonly FrozenDictionary<PropertyInfo, WritePropertyAttribute> WritePropertyCache;
        private static readonly FrozenDictionary<PropertyInfo, ReadPropertyAttribute> ReadPropertyCache;
        private static readonly IImmutableList<PropertyInfo> ImmutableWriteProperties;
        private static readonly IImmutableList<PropertyInfo> ImmutableReadProperties;

        /// <summary>
        /// Static constructor - executes all expensive reflection logic once per type T.
        /// </summary>
        static SpinnerV5()
        {
            var allProperties = typeof(T).GetProperties();

            // Cache ObjectMapper attribute
            ReadObjectMapper = (ObjectMapperAttribute)typeof(T)
                .GetCustomAttributes(typeof(ObjectMapperAttribute), false)
                .FirstOrDefault();

            // Cache ReadProperties and their attributes
            ReadProperties = allProperties
                .Where(p => p.GetCustomAttributes(typeof(ReadPropertyAttribute), false).Length > 0)
                .ToArray();

            ReadPropertyCache = ReadProperties
                .ToFrozenDictionary(
                    p => p,
                    p => (ReadPropertyAttribute)p.GetCustomAttributes(typeof(ReadPropertyAttribute), false)[0]
                );

            ImmutableReadProperties = ReadProperties.ToImmutableList();

            // Cache WriteProperties and their attributes
            var writePropsWithAttrs = allProperties
                .Where(p => p.GetCustomAttributes(typeof(WritePropertyAttribute), false).Length > 0)
                .Select(p => new
                {
                    Property = p,
                    Attribute = (WritePropertyAttribute)p.GetCustomAttributes(typeof(WritePropertyAttribute), false)[0]
                })
                .OrderBy(x => x.Attribute.Order)
                .ToArray();

            WriteProperties = writePropsWithAttrs.Select(x => x.Property).ToArray();
            WritePropertyCache = writePropsWithAttrs.ToFrozenDictionary(x => x.Property, x => x.Attribute);
            ImmutableWriteProperties = WriteProperties.ToImmutableList();
        }

        /// <summary>
        /// Default constructor - no state, fully reusable and thread-safe.
        /// All expensive reflection logic is executed in the static constructor.
        /// </summary>
        public SpinnerV5()
        {
            // No initialization needed - all work is done in static constructor
        }

        /// <summary>
        /// Get configuration property of T.
        /// </summary>
        public ObjectMapperAttribute GetObjectMapper => ReadObjectMapper;

        /// <summary>
        /// Get all properties with WriteProperty decoration present in T.
        /// </summary>
        public IImmutableList<PropertyInfo> GetWriteProperties => ImmutableWriteProperties;

        /// <summary>
        /// Get all properties with ReadProperty decoration present in T.
        /// </summary>
        public IImmutableList<PropertyInfo> GetReadProperties => ImmutableReadProperties;

        /// <summary>
        /// Convert T in a positional string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Returns a string mapped of T.</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public string WriteAsString(T obj)
        {
            PooledStringBuilder sb = PooledStringBuilder.GetInstance();

            WritePositionalString(ref sb, obj);

            return ReadObjectMapper is not null
                    ? sb.ToStringAndFree(0, ReadObjectMapper.Length)
                    : sb.ToStringAndFree();
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
                ref PropertyInfo property = ref ReadProperties[i];
                ReadPropertyAttribute attribute = ReadPropertyCache[property];

                Guard.ReadProperty.NotMapped(property, attribute);

                var slice = text.Slice(attribute.Start, attribute.Length);

                // Manual trim to avoid allocation
                int start = 0;
                int end = slice.Length - 1;

                while (start <= end && char.IsWhiteSpace(slice[start]))
                    start++;

                while (end >= start && char.IsWhiteSpace(slice[end]))
                    end--;

                var trimmedSlice = start <= end ? slice.Slice(start, end - start + 1) : ReadOnlySpan<char>.Empty;

                if (attribute.Type is not null)
                {
                    if (!InterceptorCache.TryGet(attribute.Type.Name, out IInterceptor interceptor))
                    {
                        interceptor = (IInterceptor)Activator.CreateInstance(attribute.Type);
                        InterceptorCache.Add(attribute.Type.Name, interceptor);
                    }

                    property.SetValue(obj, interceptor.Parse(new string(trimmedSlice)));
                }
                else
                {
                    property.SetValue(obj, new string(trimmedSlice));
                }
            }

            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void WritePositionalString(ref PooledStringBuilder sb, T obj)
        {
            for (int i = 0; i < WriteProperties.Length; i++)
            {
                ref PropertyInfo property = ref WriteProperties[i];
                WritePropertyAttribute attribute = WritePropertyCache[property];

                Guard.WriteProperty.NotMapped(property, attribute);

                var value = property.GetValue(obj) as string ?? string.Empty;
                int valueLength = value.Length;
                int targetLength = attribute.Length;

                if (attribute.Padding == PaddingType.Left)
                {
                    // PadLeft: padding first, then value
                    int paddingCount = Math.Max(0, targetLength - valueLength);

                    if (paddingCount > 0)
                    {
                        sb.Builder.Append(attribute.PaddingChar, paddingCount);
                    }

                    if (valueLength <= targetLength)
                    {
                        sb.Builder.Append(value);
                    }
                    else
                    {
                        sb.Builder.Append(value, 0, targetLength);
                    }
                }
                else
                {
                    // PadRight: value first, then padding
                    if (valueLength <= targetLength)
                    {
                        sb.Builder.Append(value);

                        int paddingCount = targetLength - valueLength;
                        if (paddingCount > 0)
                        {
                            sb.Builder.Append(attribute.PaddingChar, paddingCount);
                        }
                    }
                    else
                    {
                        sb.Builder.Append(value, 0, targetLength);
                    }
                }
            }
        }
    }
}