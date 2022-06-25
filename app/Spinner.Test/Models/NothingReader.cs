﻿using Spinner.Attribute;
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

        public override bool Equals(object obj)
        {
            NothingReader other = (NothingReader)obj;

            return other.Equals(this);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Adress);
        }
    }
}
