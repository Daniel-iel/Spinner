using Spinner.Attribute;
using System;

namespace Spinner.Test.Models
{
    [ObjectMapper(length: 50)]
    internal class NothingReader : IEquatable<NothingReader>
    {
        [ReadProperty(start: 1, length: 19)]
        public string Name { get; set; }

        [ReadProperty(start: 20, length: 30)]
        public string WebSite { get; set; }

        public bool Equals(NothingReader other)
        {
            return Name == other.Name &&
                   WebSite == other.WebSite;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, WebSite);
        }

        public override bool Equals(object obj)
        {
            return obj is NothingReader nothingReader && Equals(nothingReader);
        }
    }
}