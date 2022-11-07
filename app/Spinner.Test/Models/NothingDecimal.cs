using Spinner.Attribute;
using System;

namespace Spinner.Test.Models
{
    [ObjectMapper(length: 4)]
    internal struct NothingDecimal : IEquatable<NothingDecimal>
    {
        public NothingDecimal(string value)
        {
            Value = value;
        }

        [WriteProperty(length: 4, order: 0, paddingChar: ' ')]
        public string Value { get; set; }

        public bool Equals(NothingDecimal other)
        {
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public override bool Equals(object obj)
        {
            return obj is NothingDecimal nothingDecimal && Equals(nothingDecimal);
        }
    }
}
