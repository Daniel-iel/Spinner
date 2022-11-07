using Spinner.Attribute;
using Spinner.Test.Helper.Parses;
using System;

namespace Spinner.Test.Models
{
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