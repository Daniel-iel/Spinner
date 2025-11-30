using Spinner.Attribute;
using Spinner.Benchmark.Interceptors;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Spinner.Benchmark.Models
{
    [ObjectMapper(length: 50)]
    public sealed class PersonWithInterceptor : IEquatable<PersonWithInterceptor>, IEqualityComparer<PersonWithInterceptor>
    {
        public PersonWithInterceptor() { }

        public PersonWithInterceptor(string name, string webSite)
        {
            Name = name;
            WebSite = webSite;
        }

        [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
        [ReadProperty(start: 0, length: 19)]
        public string Name { get; set; }

        [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
        [ReadProperty(start: 19, length: 30, type: typeof(WebSiteInterceptor))]
        public string WebSite { get; set; }

        public bool Equals(PersonWithInterceptor other)
        {
            return Name == other.Name &&
                   WebSite == other.WebSite;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            PersonWithInterceptor other = (PersonWithInterceptor)obj;

            return other.Equals(this);
        }

        public bool Equals(PersonWithInterceptor x, PersonWithInterceptor y)
        {
            return x.Equals(y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, WebSite);
        }

        public int GetHashCode([DisallowNull] PersonWithInterceptor obj)
        {
            return HashCode.Combine(obj.Name, obj.WebSite);
        }
    }
}
