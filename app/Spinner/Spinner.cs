using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Spinner
{
    using Spinner.Attribute;
    using Spinner.Extencions;
    using System.Linq;

    public struct Spinner<T> where T : struct
    {
        private readonly T localObj;

        /// <summary>
        /// T is the object that can be mapped using the attributes WriteProperty, ReadProperty and ContextProperty.
        /// </summary>
        /// <param name="obj">Object of T that will be used to map.</param>
        public Spinner(T obj)
        {
            this.localObj = obj;
        }

        /// <summary>
        /// Get configuration property of T.
        /// </summary>
        public ObjectMapper GetObjectMapper
        {
            get => ReadObjectMapper;
        }

        /// <summary>
        /// Get all property present in T.
        /// </summary>
        public IEnumerable<PropertyInfo> GetWriteProperties
        {
            get => WriteProperties;
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
                var atribuite = GetWriteProperty(property);

                sb.Builder.Append(
                    FormatValue(
                        (property.GetValue(this.localObj) as string).AsSpan(),
                        atribuite
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
                        (property.GetValue(this.localObj) as string).AsSpan(),
                        atribuite
                    ));
            }

            return new ReadOnlySpan<char>(
                    GetObjectMapper != null ?
                    sb.ToStringAndFree(0, GetObjectMapper.Lenght).ToCharArray() :
                    sb.ToStringAndFree().ToCharArray()
                );
        }

        public T ReadFromString(string value)
        {
            ReadOnlySpan<char> valuesToSlice = new ReadOnlySpan<char>(value.ToCharArray());

            foreach (PropertyInfo property in ReadProperties)
            {
                var atribuite = GetReaderProperty(property);

                property.SetValue(
                    this.localObj,
                    new string(valuesToSlice.Slice(atribuite.Start, atribuite.Lenght).Trim()));
            }

            return this.localObj;
        }

        public T ReadFromSpan(ReadOnlySpan<char> value)
        {
            foreach (PropertyInfo property in ReadProperties)
            {
                var atribuite = GetReaderProperty(property);

                property.SetValue(
                    this.localObj,
                    new string(value.Slice(atribuite.Start, atribuite.Lenght).Trim()));
            }

            return this.localObj;
        }

        private static ReadOnlySpan<char> FormatValue(ReadOnlySpan<char> value, WriteProperty property)
        {
            if (property.Padding == Enuns.PaddingType.Left)
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
