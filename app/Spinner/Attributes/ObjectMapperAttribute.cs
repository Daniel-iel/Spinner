using System;

namespace Spinner.Attribute
{
    /// <summary>
    /// TODO
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
    public sealed class ObjectMapperAttribute : System.Attribute
    {
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="length"></param>
        public ObjectMapperAttribute(ushort length)
        {
            this.Length = length;
        }

        /// <summary>
        /// TODO
        /// </summary>
        public ushort Length { get; }
    }
}
