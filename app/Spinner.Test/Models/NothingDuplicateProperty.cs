using Spinner.Attribute;
using System;

namespace Spinner.Test.Models
{
    [ObjectMapper(length: 50)]
    internal struct NothingNoAttibute : IEquatable<NothingNoAttibute>
    {
        public NothingNoAttibute(string name, string adress)
        {
            Name = name;
            Adress = adress;
        }

        public string Name { get; set; }

        public string Adress { get; set; }

        public bool Equals(NothingNoAttibute other)
        {
            return Name == other.Name &&
                   Adress == other.Adress;
        }

        public override bool Equals(object obj)
        {
            NothingNoAttibute other = (NothingNoAttibute)obj;

            return other.Equals(this);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Adress);
        }
    }
}
