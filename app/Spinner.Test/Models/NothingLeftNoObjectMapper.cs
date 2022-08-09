using Spinner.Attribute;
using System;

namespace Spinner.Test.Models
{
    internal struct NothingLeftNoObjectMapper : IEquatable<NothingLeftNoObjectMapper>
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

        public bool Equals(NothingLeftNoObjectMapper other)
        {
            return Name == other.Name &&
                   Adress == other.Adress;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Adress);
        }

        public override bool Equals(object obj)
        {
            return obj is NothingLeftNoObjectMapper nothingLeftNoObjectMapper && Equals(nothingLeftNoObjectMapper);
        }

        public static bool operator ==(NothingLeftNoObjectMapper left, NothingLeftNoObjectMapper right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(NothingLeftNoObjectMapper left, NothingLeftNoObjectMapper right)
        {
            return !(left == right);
        }
    }
}
