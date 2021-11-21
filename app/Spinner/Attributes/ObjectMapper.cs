using System;

namespace Spinner.Attribute
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class )]
    public sealed class ObjectMapper : System.Attribute
    {
        public ObjectMapper(ushort lenght)
        {
            this.Lenght = lenght;
        }

        public ushort Lenght { get; }
    }
}
