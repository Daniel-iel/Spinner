using Spinner.Attribute;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Writer.Benchmark
{
    [ObjectMapper(length: 50)]
    internal struct ObjectBenchWithParser : IEquatable<ObjectBenchWithParser>, IEqualityComparer<ObjectBenchWithParser>
    {
        public ObjectBenchWithParser(string name, string address)
        {
            Name = name;
            Address = address;
        }

        [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
        [ReadProperty(start: 0, length: 19)]
        public string Name { get; set; }

        [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
        [ReadProperty(start: 19, length: 30, type: typeof(ParserWebSite))]
        public string Address { get; set; }

        public bool Equals(ObjectBenchWithParser other)
        {
            return Name == other.Name &&
                   Address == other.Address;
        }

        public override bool Equals(object obj)
        {
            ObjectBenchWithParser other = (ObjectBenchWithParser)obj;

            return other.Equals(this);
        }

        public bool Equals(ObjectBenchWithParser x, ObjectBenchWithParser y)
        {
            return x.Equals(y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Address);
        }

        public int GetHashCode([DisallowNull] ObjectBenchWithParser obj)
        {
            return HashCode.Combine(obj.Name, obj.Address);
        }
    }
}
