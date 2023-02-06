using Spinner.Enums;
using System;

namespace Spinner.Attribute
{
    /// <summary>
    /// Attribute responsible for configuring writing a field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public sealed class WritePropertyAttribute : System.Attribute
    {
        /// <summary>
        /// Constructor to configure the length, order and char padding of a field.
        /// </summary>
        /// <param name="length">Total characters.</param>
        /// <param name="order">Field order within string.</param>
        /// <param name="paddingChar">Type of characters to padding.</param>
        public WritePropertyAttribute(ushort length, ushort order, char paddingChar)
            : this(length: length, order: order, paddingChar: paddingChar, padding: PaddingType.Left)
        {
        }

        /// <summary>
        /// Constructor to configure the length, order and char padding of a field.
        /// </summary>
        /// <param name="length">Total characters.</param>
        /// <param name="order">Field order within string.</param>
        /// <param name="paddingChar">Type of characters to padding.</param>
        /// <param name="padding">Type of padding Left or Right.</param>
        public WritePropertyAttribute(ushort length, ushort order, char paddingChar, PaddingType padding)
        {
            this.Length = length;
            this.Order = order;
            this.PaddingChar = paddingChar;
            this.Padding = padding;
        }

        /// <summary>
        /// Total characters.
        /// </summary>
        public ushort Length { get; }

        /// <summary>
        /// Field order within string.
        /// </summary>
        public ushort Order { get; }

        /// <summary>
        /// Type of characters to padding.
        /// </summary>
        public char PaddingChar { get; }

        /// <summary>
        /// Type of padding Left or Right.
        /// </summary>
        public PaddingType Padding { get; }
    }
}