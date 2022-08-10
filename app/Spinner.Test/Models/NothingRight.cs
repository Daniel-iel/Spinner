using Spinner.Attribute;
using Spinner.Enums;
using System;

namespace Spinner.Test.Models
{
    [ObjectMapper(length: 50)]
    internal struct NothingRight : IEquatable<NothingRight>
    {
        public NothingRight(string name, string adress)
        {
            Name = name;
            Adress = adress;
        }

        [WriteProperty(length: 20, order: 1, paddingChar: ' ', PaddingType.Right)]
        public string Name { get; }

        [WriteProperty(length: 30, order: 2, paddingChar: ' ', PaddingType.Right)]
        public string Adress { get; }

        public bool Equals(NothingRight other)
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
            return obj is NothingRight nothingRight && Equals(nothingRight);
        }
    }
}
