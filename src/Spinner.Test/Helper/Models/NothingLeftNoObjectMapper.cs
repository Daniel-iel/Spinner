using Spinner.Attribute;
using System;

namespace Spinner.Test.Helper.Models
{
    internal sealed class NothingLeftNoObjectMapper : IEquatable<NothingLeftNoObjectMapper>
    {
        public NothingLeftNoObjectMapper() { }
        public NothingLeftNoObjectMapper(string name, string webSite)
        {
            Name = name;
            WebSite = webSite;
        }

        [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
        public string Name { get; set; }

        [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
        public string WebSite { get; set; }

        public bool Equals(NothingLeftNoObjectMapper other)
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
            return obj is NothingLeftNoObjectMapper nothingLeftNoObjectMapper && Equals(nothingLeftNoObjectMapper);
        }
    }
}