using System;

namespace Spinner.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ReadProperty : System.Attribute
    {
        public ReadProperty(ushort start, ushort end)
        {
            Start = start;
            End = end;
        }

        public ushort Start { get; }
        public ushort End { get; }
    }
}
