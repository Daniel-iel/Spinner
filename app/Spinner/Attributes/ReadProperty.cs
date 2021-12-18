using System;

namespace Spinner.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ReadProperty : System.Attribute
    {
        public ReadProperty(ushort start, ushort lenght)
        {
            this.Start = start;
            this.Lenght = lenght;
        }

        public ushort Start { get; }
        public ushort Lenght { get; }
    }
}
