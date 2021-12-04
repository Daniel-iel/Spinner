using Spinner.Enum;
using System;

namespace Spinner.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class WriteProperty : System.Attribute
    {
        public WriteProperty(ushort lenght, ushort order, char paddingChar)
            : this(lenght: lenght, order: order, paddingChar: paddingChar, padding: PaddingType.Left)
        {
        }

        public WriteProperty(ushort lenght, ushort order, char paddingChar, PaddingType padding)
        {
            this.Lenght = lenght;
            this.Order = order;
            this.PaddingChar = paddingChar;
            this.Padding = padding;
        }

        public ushort Lenght { get; }
        public ushort Order { get; }
        public char PaddingChar { get; }
        public PaddingType Padding { get; }
    }
}
