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

        public Spinner(T obj)
        {
            this.localObj = obj;
        }

        public ContextProperty GetConfigurationProperty
        {
            get => ReadConfigurationProperty;
        }

        public IEnumerable<PropertyInfo> GetWriteProperties
        {
            get => WriteProperties;
        }

        public IEnumerable<PropertyInfo> GetReadProperties
        {
            get => default;
        }

        public string WriteAsString()
        {
            PooledStringBuilder sb = PooledStringBuilder.GetInstance();

            foreach (PropertyInfo property in WriteProperties)
            {
                var atribuite = GetWriteProperty(property);

                sb.Builder.Append((property.GetValue(this.localObj) as string)
                                   .AsSpan()
                                   .PadLeft(atribuite.Lenght, atribuite.PaddingChar)
                                   .Slice(0, atribuite.Lenght)
                                );
            }

            return GetConfigurationProperty != null ? 
                sb.ToStringAndFree(0, GetConfigurationProperty.Lenght) : 
                sb.ToStringAndFree();
        }

        public ReadOnlySpan<char> WriteAsSpan()
        {
            PooledStringBuilder sb = PooledStringBuilder.GetInstance();

            foreach (PropertyInfo property in WriteProperties)
            {
                var atribuite = GetWriteProperty(property);

                sb.Builder.Append((property.GetValue(this.localObj) as string)
                                   .AsSpan()
                                   .PadLeft(atribuite.Lenght, atribuite.PaddingChar)
                                   .Slice(0, atribuite.Lenght)
                                );
            }

            return new ReadOnlySpan<char>(
                    GetConfigurationProperty != null ? 
                    sb.ToStringAndFree(0, GetConfigurationProperty.Lenght).ToCharArray() : 
                    sb.ToStringAndFree().ToCharArray()
                );
        }

        private static readonly ContextProperty ReadConfigurationProperty =
            typeof(T)
            .GetCustomAttributes(typeof(ContextProperty), false)
            .Cast<ContextProperty>()
            .FirstOrDefault();

        private static WriteProperty GetWriteProperty(PropertyInfo info) =>
          info
            .GetCustomAttributes(typeof(WriteProperty), false)
            .Cast<WriteProperty>()
            .FirstOrDefault();

        private static readonly IEnumerable<PropertyInfo> WriteProperties =
            typeof(T)
            .GetProperties()
            .Where(PredicateForWriteProperty())
            .OrderBy(PrecicateForOrderByWriteProperty());

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
    }
}
