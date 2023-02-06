using Microsoft.VisualStudio.Utilities;
using Spinner.Attribute;
using Spinner.Cache;
using Spinner.Enums;
using Spinner.Extensions;
using Spinner.Guards;
using Spinner.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Spinner
{
    /// <summary>
    /// Spinner object that abstract all rule to read or write an string.
    /// </summary>
    /// <typeparam name="T">The type of object to write or read.</typeparam>
    public ref struct Spinner<T> where T : new()
    {
        private readonly PooledStringBuilder _sb = PooledStringBuilder.GetInstance();
        private readonly T _obj;

        /// <summary>
        /// T is the object that can be mapped using the attributes WriteProperty, ReadProperty and ContextProperty.
        /// </summary>
        /// <param name="obj">Object of T that will be used to map.</param>
        public Spinner(T obj)
        {
            _obj = obj;
        }

        /// <summary>
        /// Default constructor, should only use in ReadFromString or ReadFromSpan.
        /// </summary>
        public Spinner()
        {
            _obj = new T();
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
            WritePositionalString();

            return GetObjectMapper is not null ?
                _sb.ToStringAndFree(0, GetObjectMapper.Length) :
                _sb.ToStringAndFree();
        }

        /// <summary>
        /// Convert T in a positional string as span.
        /// </summary>
        /// <returns>Return an string mapped of T as span.</returns>
        public ReadOnlySpan<char> WriteAsSpan()
        {
            WritePositionalString();

            return new ReadOnlySpan<char>(
                    GetObjectMapper is not null ?
                    _sb.ToStringAndFree(0, GetObjectMapper.Length).ToCharArray() :
                    _sb.ToStringAndFree().ToCharArray());
        }

        /// <summary>
        /// Convert string in an object.
        /// </summary>
        /// <param name="value">Positional string to map in an object.</param>
        /// <returns>returns instance of T.</returns>
        public T ReadFromString(string value)
        {
            ReadPositionalString(value);

            return _obj;
        }

        /// <summary>
        /// Convert string in an object.
        /// </summary>
        /// <param name="value">Span with data to map an object.</param>
        /// <returns>returns instance of T.</returns>
        public T ReadFromSpan(ReadOnlySpan<char> value)
        {
            ReadPositionalString(value);

            return _obj;
        }

        private void ReadPositionalString(ReadOnlySpan<char> text)
        {
            for (int i = 0; i < ReadProperties.Length; i++)
            {
                ref PropertyInfo property = ref ReadProperties[i];
                ReadPropertyAttribute attribute = GetReaderProperty(property);

                Guard.ReadProperty.NotMapped(property, attribute);

                if (attribute.Type is not null)
                {
                    if (!ParserTypeCache.TryGet(attribute.Type.Name, out ITypeParse typeParser))
                    {
                        typeParser = (ITypeParse)Activator.CreateInstance(attribute.Type);
                        ParserTypeCache.Add(attribute.Type.Name, typeParser);
                    }

                    property.SetValue(_obj, typeParser.Parser(new string(text.Slice(attribute.Start, attribute.Length).Trim())));

                    continue;
                }

                property.SetValue(_obj, new string(text.Slice(attribute.Start, attribute.Length).Trim()));
            }
        }

        private void WritePositionalString()
        {
            for (int i = 0; i < WriteProperties.Length; i++)
            {
                ref PropertyInfo property = ref WriteProperties[i];
                WritePropertyAttribute attribute = GetWriteProperty(property);

                Guard.WriteProperty.NotMapped(property, attribute);

                _sb.Builder.Append(FormatValue((property.GetValue(_obj) as string).AsSpan(), attribute));
            }
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

        private static readonly PropertyInfo[] WriteProperties =
            typeof(T)
            .GetProperties()
            .Where(PredicateForWriteProperty())
            .OrderBy(PredicateForOrderByWriteProperty())
            .ToArray();

        private static readonly PropertyInfo[] ReadProperties =
            typeof(T)
            .GetProperties()
            .Where(PredicateForReadProperty())
            .ToArray();

        private static Func<PropertyInfo, bool> PredicateForWriteProperty()
        {
            return (prop) =>
            {
                return prop.GetCustomAttributes(typeof(WritePropertyAttribute), false)
                           .All(attribute => attribute.GetType() == typeof(WritePropertyAttribute));
            };
        }

        private static Func<PropertyInfo, ushort> PredicateForOrderByWriteProperty()
        {
            return (prop) => ((WritePropertyAttribute)prop
                .GetCustomAttributes(typeof(WritePropertyAttribute), false)
                .FirstOrDefault())?.Order ?? default;
        }

        private static Func<PropertyInfo, bool> PredicateForReadProperty()
        {
            return (prop) =>
            {
                return prop.GetCustomAttributes(typeof(ReadPropertyAttribute), false)
                           .All(attribute => attribute.GetType() == typeof(ReadPropertyAttribute));
            };
        }
    }
}