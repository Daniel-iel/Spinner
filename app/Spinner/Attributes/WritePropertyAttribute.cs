using Spinner.Enums;
using System;

namespace Spinner.Attribute
{
    /// <summary>
    /// TODO
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public sealed class WritePropertyAttribute : System.Attribute
    {
        /// <summary>
        /// TODO
        /// </summary>
        public WritePropertyAttribute(ushort length, ushort order, char paddingChar)
            : this(length: length, order: order, paddingChar: paddingChar, padding: PaddingType.Left)
        {
        }

        /// <summary>
        /// TODO
        /// </summary>
        public WritePropertyAttribute(ushort length, ushort order, char paddingChar, PaddingType padding)
        {
            this.Length = length;
            this.Order = order;
            this.PaddingChar = paddingChar;
            this.Padding = padding;
        }

        /// <summary>
        /// TODO
        /// </summary>
        public ushort Length { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public ushort Order { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public char PaddingChar { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public PaddingType Padding { get; }
    }
}
