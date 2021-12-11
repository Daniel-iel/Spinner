using Spinner.Attribute;
using System;

namespace Spinner.Test
{
    [ObjectMapper(lenght: 50)]
    internal struct NothingLeft : IEquatable<NothingLeft>
    {
        public NothingLeft(string name, string adress)
        {
            this.Name = name;
            this.Adress = adress;
        }

        [WriteProperty(lenght: 20, order: 1, paddingChar: ' ')]
        public string Name { get; private set; }

        [WriteProperty(lenght: 30, order: 2, paddingChar: ' ')]
        public string Adress { get; private set; }

        public bool Equals(NothingLeft other)
        {
            return this.Name == other.Name &&
                   this.Adress == other.Adress;
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
