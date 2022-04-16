﻿using Spinner.Attribute;
using System;

namespace Spinner.Test
{
    [ObjectMapperAttribute(length: 50)]
    internal struct NothingLeft : IEquatable<NothingLeft>
    {
        public NothingLeft(string name, string adress)
        {
            this.Name = name;
            this.Adress = adress;
        }

        [WritePropertyAttribute(length: 20, order: 1, paddingChar: ' ')]
        public string Name { get; }

        [WritePropertyAttribute(length: 30, order: 2, paddingChar: ' ')]
        public string Adress { get; }

        public bool Equals(NothingLeft other)
        {
            return this.Name == other.Name &&
                   this.Adress == other.Adress;
        }

        public override bool Equals(object obj)
        {
            NothingLeft other = (NothingLeft)obj;

            return other.Equals(this);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Adress);
        }
    }
}
