using System;

namespace Spinner.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ReadProperty : System.Attribute
    {
        public ReadProperty(ushort start, ushort length)
        {
            this.Start = start;
            this.Length = length;
        }

        public ushort Start { get; }
        public ushort Length { get; }
    }
}
