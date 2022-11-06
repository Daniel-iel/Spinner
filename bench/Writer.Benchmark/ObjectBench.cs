using Spinner.Attribute;
using Spinner.Parsers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Writer.Benchmark
{
    [ObjectMapper(length: 50)]
    internal struct ObjectBench : IEquatable<ObjectBench>, IEqualityComparer<ObjectBench>
    {
        public ObjectBench(string name, string adress)
        {
            Name = name;
            Adress = adress;
        }

        [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
        [ReadProperty(start: 0, length: 19)]
        public string Name { get; set; }

        [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
        [ReadProperty(start: 19, length: 30, type: typeof(ParserAdress))]
        public string Adress { get; set; }

        public bool Equals(ObjectBench other)
        {
            return Name == other.Name &&
                   Adress == other.Adress;
        }

        public override bool Equals(object obj)
        {
            ObjectBench other = (ObjectBench)obj;

            return other.Equals(this);
        }

        public bool Equals(ObjectBench x, ObjectBench y)
        {
            return x.Equals(y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Adress);
        }

        public int GetHashCode([DisallowNull] ObjectBench obj)
        {
            return HashCode.Combine(obj.Name, obj.Adress);
        }
    }
}
