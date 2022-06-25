using Spinner.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Writer.Benchmark
{
    [ObjectMapper(length: 50)]
    internal struct ObjectBench : IEquatable<ObjectBench>
    {
        public ObjectBench(string name, string adress)
        {
            Name = name;
            Adress = adress;
        }

        [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
        public string Adress { get; set; }

        [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
        public string Name { get; set; }

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

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Adress);
        }
    }
}
