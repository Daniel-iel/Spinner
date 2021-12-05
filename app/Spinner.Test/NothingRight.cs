using Spinner.Attribute;
using Spinner.Enum;
using System;

namespace Spinner.Test
{
    [ObjectMapper(lenght: 50)]
    internal struct NothingRight : IEquatable<NothingRight>
    {
        public NothingRight(string name, string adress)
        {
            this.Name = name;
            this.Adress = adress;
        }

        [WriteProperty(lenght: 20, order: 1, paddingChar: ' ', PaddingType.Right)]
        public string Name { get; private set; }

        [WriteProperty(lenght: 30, order: 2, paddingChar: ' ', PaddingType.Right)]
        public string Adress { get; private set; }

        public bool Equals(NothingRight other)
        {
            return this.Name == other.Name &&
                   this.Adress == other.Adress;
        }

        public override bool Equals(object obj)
        {
            var other = (NothingRight)obj;

            return other.Equals(this);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Adress);
        }
    }
}
