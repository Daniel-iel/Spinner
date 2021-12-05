using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Spinner
{
    using Spinner.Enum;
    using Spinner.Attribute;
    using Spinner.Extencions;
    using System.Linq;

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
        public ObjectMapper GetObjectMapper
        {
            get => ReadObjectMapper;
        }

        /// <summary>
        /// Get all properties with WriteProperty decoration present in T.
        /// </summary>
        public IEnumerable<PropertyInfo> GetWriteProperties
        {
            get => WriteProperties;
        }

        /// <summary>
        /// Get all properties with ReadProperty decoration present in T.
        /// </summary>
        public IEnumerable<PropertyInfo> GetReadProperties
        {
            get => ReadProperties;
        }

        /// <summary>
        /// Convert T in a positional string.
        /// </summary>
        /// <returns>Return a string mapped of T.</returns>
        public string WriteAsString()
        {
            PooledStringBuilder sb = PooledStringBuilder.GetInstance();

            foreach (PropertyInfo property in WriteProperties)
            {
                var attribute = GetWriteProperty(property);

                sb.Builder.Append(
                    FormatValue(
                        (property.GetValue(this.obj) as string).AsSpan(),
                        attribute
                    ));
            }

            return GetObjectMapper != null ?
                sb.ToStringAndFree(0, GetObjectMapper.Lenght) :
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
                var atribuite = GetWriteProperty(property);

                sb.Builder.Append(
                    FormatValue(
                        (property.GetValue(this.obj) as string).AsSpan(),
                        atribuite
                    ));
            }

            return new ReadOnlySpan<char>(
                    GetObjectMapper != null ?
                    sb.ToStringAndFree(0, GetObjectMapper.Lenght).ToCharArray() :
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
                var attribute = GetReaderProperty(property);

                property.SetValue(
                    this.obj,
                    new string(valuesToSlice.Slice(attribute.Start, attribute.Lenght).Trim()));
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
                var attribute = GetReaderProperty(property);

                property.SetValue(
                    this.obj,
                    new string(value.Slice(attribute.Start, attribute.Lenght).Trim()));
            }

            return this.obj;
        }

        private static ReadOnlySpan<char> FormatValue(ReadOnlySpan<char> value, WriteProperty property)
        {
            if (property.Padding == PaddingType.Left)
            {
                return value.PadLeft(property.Lenght, property.PaddingChar).Slice(0, property.Lenght);
            }

            return value.PadRight(property.Lenght, property.PaddingChar).Slice(0, property.Lenght);
        }

        private static readonly ObjectMapper ReadObjectMapper =
            typeof(T)
            .GetCustomAttributes(typeof(ObjectMapper), false)
            .Cast<ObjectMapper>()
            .FirstOrDefault();

        private static WriteProperty GetWriteProperty(PropertyInfo info) =>
          info
            .GetCustomAttributes(typeof(WriteProperty), false)
            .Cast<WriteProperty>()
            .FirstOrDefault();

        private static ReadProperty GetReaderProperty(PropertyInfo info) =>
          info
            .GetCustomAttributes(typeof(ReadProperty), false)
            .Cast<ReadProperty>()
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
            return (prop) => prop.GetCustomAttributes(typeof(WriteProperty), false).All(a => a.GetType() == typeof(WriteProperty));
        }

        private static Func<PropertyInfo, ushort> PrecicateForOrderByWriteProperty()
        {
            return (prop) => ((WriteProperty)prop.GetCustomAttributes(true)
                                        .Where(x => x.GetType() == typeof(WriteProperty))
                                        .FirstOrDefault()).Order;
        }

        private static Func<PropertyInfo, bool> PredicateForReadProperty()
        {
            return (prop) => prop.GetCustomAttributes(typeof(ReadProperty), false).All(a => a.GetType() == typeof(ReadProperty));
        }
    }
}
