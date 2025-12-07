using Spinner.Attribute;
using Spinner.Test.Helper.Interceptors;
using System;

namespace Spinner.Test.Helper.Models
{
    [ObjectMapper(length: 4)]
    internal sealed class NothingDecimalReader : IEquatable<NothingDecimalReader>
    {
        public NothingDecimalReader() { }

        [ReadProperty(start: 0, length: 4, type: typeof(DecimalInterceptor))]
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