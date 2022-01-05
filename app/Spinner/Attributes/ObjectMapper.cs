using System;

namespace Spinner.Attribute
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
    public sealed class ObjectMapper : System.Attribute
    {
        public ObjectMapper(ushort length)
        {
            this.Length = length;            
        }

        public ushort Length { get; }
    }
}
