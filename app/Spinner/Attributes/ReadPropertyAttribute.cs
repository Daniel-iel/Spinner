using System;

namespace Spinner.Attribute
{
    /// <summary>
    /// Attribute responsible for configuring reading a field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ReadPropertyAttribute : System.Attribute
    {
        /// <summary>
        /// Constructor to configure the initial position and total characters of a field.
        /// </summary>
        /// <param name="start">Inicial position.</param>
        /// <param name="length">Total characters.</param>
        public ReadPropertyAttribute(ushort start, ushort length)
        {
            this.Start = start;
            this.Length = length;
        }

        public ReadPropertyAttribute(ushort start, ushort length, Type type)
        {
            this.Start = start;
            this.Length = length;
            this.Type = type;
        }

        /// <summary>
        /// Inicial position.
        /// </summary>
        public ushort Start { get; }

        /// <summary>
        /// Total characters.
        /// </summary>
        public ushort Length { get; }

        /// <summary>
        /// Parser property type
        /// </summary>
        public Type Type { get; }
    }
}
