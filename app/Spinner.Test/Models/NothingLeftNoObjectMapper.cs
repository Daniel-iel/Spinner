using Spinner.Attribute;
using System;

namespace Spinner.Test.Models
{
    internal struct NothingLeftNoObjectMapper : IEquatable<NothingLeft>
    {
        public NothingLeftNoObjectMapper(string name, string adress)
        {
            Name = name;
            Adress = adress;
        }

        [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
        public string Name { get; set; }

        [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
        public string Adress { get; set; }

        public bool Equals(NothingLeft other)
        {
            return Name == other.Name &&
                   Adress == other.Adress;
        }

        public override bool Equals(object obj)
        {
            NothingLeft other = (NothingLeft)obj;

            return other.Equals(this);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Adress);
        }
    }
}
