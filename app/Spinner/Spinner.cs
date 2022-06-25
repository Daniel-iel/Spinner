using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Spinner
{
    using Spinner.Enums;
    using Spinner.Attribute;
    using Spinner.Extensions;
    using System.Linq;
    using Spinner.Exceptions;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public ref struct Spinner<T> where T : new()
    {
        private readonly T obj;

        /// <summary>
        /// T is the object that can be mapped using the attributes WriteProperty, ReadProperty and ContextProperty.
        /// </summary>
        /// <param name="obj">Object of T that will be used to map.</param>
        public Spinner(T obj)
        {
            this.obj = obj;
        }

        /// <summary>
        /// Default constructor, should only use in ReadFromString or ReadFromSpan.
        /// </summary>
        public Spinner()
        {
            this.obj = new T();
        }

        /// <summary>
        /// Get configuration property of T.
        /// </summary>
        public ObjectMapperAttribute GetObjectMapper => ReadObjectMapper;

        /// <summary>
        /// Get all properties with WriteProperty decoration present in T.
        /// </summary>
        public IEnumerable<PropertyInfo> GetWriteProperties => WriteProperties;

        /// <summary>
        /// Get all properties with ReadProperty decoration present in T.
        /// </summary>
        public IEnumerable<PropertyInfo> GetReadProperties => ReadProperties;

        /// <summary>
        /// Convert T in a positional string.
        /// </summary>
        /// <returns>Return a string mapped of T.</returns>
        public string WriteAsString()
        {
            PooledStringBuilder sb = PooledStringBuilder.GetInstance();

            foreach (PropertyInfo property in WriteProperties)
            {
                WritePropertyAttribute attribute = GetWriteProperty(property);

                if (attribute == null)
                {
                    throw new PropertyNotMappedException($"Property {property.Name} should have WriteProperty configured.");
                }

                sb.Builder.Append(
                    FormatValue(
                        (property.GetValue(this.obj) as string).AsSpan(),
                        attribute
                    ));
            }

            return GetObjectMapper != null ?
                sb.ToStringAndFree(0, GetObjectMapper.Length) :
                sb.ToStringAndFree();
        }

        /// <summary>
        /// Convert T in a positional string as span.
        /// </summary>
        /// <returns>Return an string mapped of T as span.</returns>
        public ReadOnlySpan<char> WriteAsSpan()
        {
            PooledStringBuilder sb = PooledStringBuilder.GetInstance();

            foreach (PropertyInfo property in WriteProperties)
            {
                WritePropertyAttribute attribute = GetWriteProperty(property);

                if (attribute == null)
                {
                    throw new PropertyNotMappedException($"Property {property.Name} should have WriteProperty configured.");
                }

                sb.Builder.Append(
                    FormatValue(
                        (property.GetValue(this.obj) as string).AsSpan(),
                        attribute
                    ));
            }

            return new ReadOnlySpan<char>(
                    GetObjectMapper != null ?
                    sb.ToStringAndFree(0, GetObjectMapper.Length).ToCharArray() :
                    sb.ToStringAndFree().ToCharArray()
                );
        }

        /// <summary>
        /// Convert string in an object.
        /// </summary>
        /// <param name="value">Positional string to map in an object.</param>
        /// <returns></returns>
        public T ReadFromString(string value)
        {
            ReadOnlySpan<char> valuesToSlice = new ReadOnlySpan<char>(value.ToCharArray());

            foreach (PropertyInfo property in ReadProperties)
            {
                ReadPropertyAttribute attribute = GetReaderProperty(property);

                if (attribute == null)
                {
                    throw new PropertyNotMappedException($"Property {property.Name} should have ReadProperty configured.");
                }

                property.SetValue(
                    this.obj,
                    new string(valuesToSlice.Slice(attribute.Start, attribute.Length).Trim()));
            }

            return this.obj;
        }

        /// <summary>
        /// Convert string in an object.
        /// </summary>
        /// <param name="value">Span with data to map an object.</param>
        /// <returns></returns>
        public T ReadFromSpan(ReadOnlySpan<char> value)
        {
            foreach (PropertyInfo property in ReadProperties)
            {
                ReadPropertyAttribute attribute = GetReaderProperty(property);

                if (attribute == null)
                {
                    throw new PropertyNotMappedException($"Property {property.Name} should have ReadProperty configured.");
                }

                property.SetValue(
                    this.obj,
                    new string(value.Slice(attribute.Start, attribute.Length).Trim()));
            }

            return this.obj;
        }

        private static ReadOnlySpan<char> FormatValue(ReadOnlySpan<char> value, WritePropertyAttribute property)
        {
            return property.Padding == PaddingType.Left
                ? value.PadLeft(property.Length, property.PaddingChar)[..property.Length]
                : value.PadRight(property.Length, property.PaddingChar)[..property.Length];
        }

        private static readonly ObjectMapperAttribute ReadObjectMapper =
            typeof(T)
            .GetCustomAttributes(typeof(ObjectMapperAttribute), false)
            .Cast<ObjectMapperAttribute>()
            .FirstOrDefault();

        private static WritePropertyAttribute GetWriteProperty(PropertyInfo info) =>
          info
            .GetCustomAttributes(typeof(WritePropertyAttribute), false)
            .Cast<WritePropertyAttribute>()
            .FirstOrDefault();

        private static ReadPropertyAttribute GetReaderProperty(PropertyInfo info) =>
          info
            .GetCustomAttributes(typeof(ReadPropertyAttribute), false)
            .Cast<ReadPropertyAttribute>()
            .FirstOrDefault();

        private static readonly IEnumerable<PropertyInfo> WriteProperties =
            typeof(T)
            .GetProperties()
            .Where(PredicateForWriteProperty())
            .OrderBy(PrecicateForOrderByWriteProperty());

        private static readonly IEnumerable<PropertyInfo> ReadProperties =
            typeof(T)
            .GetProperties()
            .Where(PredicateForReadProperty());

        private static Func<PropertyInfo, bool> PredicateForWriteProperty()
        {
            return (prop) =>
            {
                return prop.GetCustomAttributes(typeof(WritePropertyAttribute), false)
                           .All(a => a.GetType() == typeof(WritePropertyAttribute));
            };
        }

        private static Func<PropertyInfo, ushort> PrecicateForOrderByWriteProperty()
        {
            return (prop) => ((WritePropertyAttribute)prop.GetCustomAttributes(false)
                                         .Where(x => x.GetType() == typeof(WritePropertyAttribute))
                                         .FirstOrDefault())?.Order ?? default;
        }

        private static Func<PropertyInfo, bool> PredicateForReadProperty()
        {
            return (prop) =>
            {
                return prop.GetCustomAttributes(typeof(ReadPropertyAttribute), false)
                           .All(a => a.GetType() == typeof(ReadPropertyAttribute));
            };
        }
    }
}
