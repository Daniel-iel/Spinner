using System;

namespace Spinner.Attribute
{
    /// <summary>
    /// TODO
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ReadPropertyAttribute : System.Attribute
    {
        /// <summary>
        /// TODO
        /// </summary>
        public ReadPropertyAttribute(ushort start, ushort length)
        {
            this.Start = start;
            this.Length = length;
        }

        /// <summary>
        /// TODO
        /// </summary>
        public ushort Start { get; }

        /// <summary>
        /// TODO
        /// </summary>
        public ushort Length { get; }
    }
}
