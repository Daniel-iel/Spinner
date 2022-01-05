using Spinner.Enum;
using System;

namespace Spinner.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class WriteProperty : System.Attribute
    {
        public WriteProperty(ushort length, ushort order, char paddingChar)
            : this(length: length, order: order, paddingChar: paddingChar, padding: PaddingType.Left)
        {
        }

        public WriteProperty(ushort length, ushort order, char paddingChar, PaddingType padding)
        {
            this.Length = length;
            this.Order = order;
            this.PaddingChar = paddingChar;
            this.Padding = padding;
        }

        public ushort Length { get; }
        public ushort Order { get; }
        public char PaddingChar { get; }
        public PaddingType Padding { get; }
    }
}
