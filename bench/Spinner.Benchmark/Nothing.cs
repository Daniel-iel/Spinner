using Spinner.Attribute;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Writer.Benchmark
{
    [ObjectMapper(length: 50)]
    internal struct Nothing : IEquatable<Nothing>, IEqualityComparer<Nothing>
    {
        public Nothing(string name, string webSite)
        {
            Name = name;
            WebSite = webSite;
        }

        [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
        [ReadProperty(start: 0, length: 19)]
        public string Name { get; set; }

        [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
        [ReadProperty(start: 19, length: 30)]
        public string WebSite { get; set; }

        public bool Equals(Nothing other)
        {
            return Name == other.Name &&
                   WebSite == other.WebSite;
        }

        public override bool Equals(object obj)
        {
            Nothing other = (Nothing)obj;

            return other.Equals(this);
        }

        public bool Equals(Nothing x, Nothing y)
        {
            return x.Equals(y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, WebSite);
        }

        public int GetHashCode([DisallowNull] Nothing obj)
        {
            return HashCode.Combine(obj.Name, obj.WebSite);
        }
    }
}
