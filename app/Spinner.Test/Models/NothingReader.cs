using Spinner.Attribute;
using Spinner.Test.Helper;
using System;

namespace Spinner.Test.Models
{
    [ObjectMapper(length: 50)]
    internal class NothingReader : IEquatable<NothingReader>
    {
        [ReadProperty(start: 1, length: 19)]
        public string Name { get; set; }

        [ReadProperty(start: 20, length: 30)]
        public string Adress { get; set; }

        public bool Equals(NothingReader other)
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
            return obj is NothingReader nothingReader && Equals(nothingReader);
        }
    }

    [ObjectMapper(length: 4)]
    internal class NothingDecimalReader : IEquatable<NothingDecimalReader>
    {
        [ReadProperty(start: 0, length: 4, type: typeof(DecimalParser))]
        public decimal Value { get; set; }

        public bool Equals(NothingDecimalReader other)
        {
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public override bool Equals(object obj)
        {
            return obj is NothingDecimalReader nothingDecimalReader && Equals(nothingDecimalReader);
        }
    }
}
