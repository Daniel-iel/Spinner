using Spinner.Attribute;
using Spinner.Enums;
using System;

namespace Spinner.Test.Models
{
    [ObjectMapper(length: 50)]
    internal struct NothingPadRight : IEquatable<NothingPadRight>
    {
        public NothingPadRight(string name, string webSite)
        {
            Name = name;
            WebSite = webSite;
        }

        [WriteProperty(length: 20, order: 1, paddingChar: ' ', PaddingType.Right)]
        public string Name { get; }

        [WriteProperty(length: 30, order: 2, paddingChar: ' ', PaddingType.Right)]
        public string WebSite { get; }

        public bool Equals(NothingPadRight other)
        {
            return Name == other.Name &&
                   WebSite == other.WebSite;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, WebSite);
        }

        public override bool Equals(object obj)
        {
            return obj is NothingPadRight nothingPadRight && Equals(nothingPadRight);
        }
    }
}