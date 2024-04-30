using System;

namespace Spinner.Attribute
{
    /// <summary>
    /// Configuration attribute to not allow wrong configuration of ReadPropertyAttribute and WritePropertyAttribute attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
    public sealed class ObjectMapperAttribute : System.Attribute
    {
        /// <summary>
        /// Constructor for configuration parameterization to lock the maximum size of all fields.
        /// </summary>
        /// <param name="length">Length of all field.</param>
        public ObjectMapperAttribute(ushort length)
        {
            this.Length = length;
        }

        /// <summary>
        /// Length of all field.
        /// </summary>
        public ushort Length { get; }
    }
}