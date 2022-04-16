using Spinner.Attribute;
using Spinner.Enums;
using System;

namespace Spinner.Test
{
    [ObjectMapperAttribute(length: 50)]
    internal struct NothingRight : IEquatable<NothingRight>
    {
        public NothingRight(string name, string adress)
        {
            this.Name = name;
            this.Adress = adress;
        }

        [WritePropertyAttribute(length: 20, order: 1, paddingChar: ' ', PaddingType.Right)]
        public string Name { get; }

        [WritePropertyAttribute(length: 30, order: 2, paddingChar: ' ', PaddingType.Right)]
        public string Adress { get; }

        public bool Equals(NothingRight other)
        {
            return this.Name == other.Name &&
                   this.Adress == other.Adress;
        }

        public override bool Equals(object obj)
        {
            NothingRight other = (NothingRight)obj;

            return other.Equals(this);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Adress);
        }
    }
}
