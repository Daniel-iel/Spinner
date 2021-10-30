using System;

namespace Spinner.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class WriteProperty : System.Attribute
    {
        public WriteProperty(ushort lenght, ushort order, char paddingChar)
        {

            this.Lenght = lenght;
            this.Order = order;
            this.PaddingChar = paddingChar;
        }

        public ushort Lenght { get; }
        public ushort Order { get; }
        public char PaddingChar { get; }
    }
}
