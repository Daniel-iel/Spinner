using Spinner.Attribute;
using System;

namespace Spinner.Test.Models
{
    [ObjectMapper(length: 50)]
    internal sealed class NothingPadLeft : IEquatable<NothingPadLeft>
    {
        public NothingPadLeft() { }

        public NothingPadLeft(string name, string webSite)
        {
            Name = name;
            WebSite = webSite;
        }

        [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
        public string WebSite { get; set; }

        [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
        public string Name { get; set; }

        public bool Equals(NothingPadLeft other)
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
            return obj is NothingPadLeft nothingLeft && Equals(nothingLeft);
        }
    }
}