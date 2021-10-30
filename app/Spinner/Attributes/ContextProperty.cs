using System;

namespace Spinner.Attribute
{
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class )]
    public sealed class ContextProperty : System.Attribute
    {
        public ContextProperty(ushort lenght)
        {

            this.Lenght = lenght;
        }

        public ushort Lenght { get; }
    }
}
