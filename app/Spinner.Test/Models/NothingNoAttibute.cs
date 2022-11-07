using Spinner.Attribute;
using System;

namespace Spinner.Test.Models
{
    [ObjectMapper(length: 50)]
    internal struct NothingNoAttibute : IEquatable<NothingNoAttibute>
    {
        public NothingNoAttibute(string name, string webSite)
        {
            Name = name;
            WebSite = webSite;
        }

        public string Name { get; set; }

        public string WebSite { get; set; }

        public bool Equals(NothingNoAttibute other)
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
            return obj is NothingNoAttibute nothingNoAttibute && Equals(nothingNoAttibute);
        }
    }
}
