using Spinner.Attribute;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Spinner.Benchmark.Models
{
    [ObjectMapper(length: 50)]
    internal sealed class Person : IEquatable<Person>, IEqualityComparer<Person>
    {
        public Person() { }

        public Person(string name, string webSite)
        {
            Name = name;
            WebSite = webSite;
        }

        [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
        [ReadProperty(start: 0, length: 19)]
        public string Name { get; set; }

        [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
        [ReadProperty(start: 19, length: 30)]
        public string WebSite { get; set; }

        public bool Equals(Person other)
        {
            return Name == other.Name &&
                   WebSite == other.WebSite;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Person other = (Person)obj;

            return other.Equals(this);
        }

        public bool Equals(Person x, Person y)
        {
            return x.Equals(y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, WebSite);
        }

        public int GetHashCode([DisallowNull] Person obj)
        {
            return HashCode.Combine(obj.Name, obj.WebSite);
        }
    }
}
