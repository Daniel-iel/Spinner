﻿using Spinner.Attribute;
using System;

namespace Spinner.Test.Models
{
    [ObjectMapper(length: 50)]
    internal struct NothingLeft : IEquatable<NothingLeft>
    {
        public NothingLeft(string name, string adress)
        {
            Name = name;
            Adress = adress;
        }

        [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
        public string Adress { get; set; }

        [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
        public string Name { get; set; }

        public bool Equals(NothingLeft other)
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
            return obj is NothingLeft nothingLeft && Equals(nothingLeft);
        }
    }
}
