using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Spinner.Test")]

namespace Spinner.Extencions
{
    internal static class SpanExtensions
    {
        internal static unsafe ReadOnlySpan<char> PadLeft(this ReadOnlySpan<char> @this, int totalWidth, char paddingChar)
        {
            int totalPad = totalWidth - @this.Length;
            bool shouldNotPad = totalWidth - @this.Length < 1;
            if (shouldNotPad) return @this;

            Span<char> tmp = stackalloc char[totalWidth];

            for (int index = 0; index <= totalWidth; index++)
            {
                if (totalPad > index)
                {
                    tmp[index] = paddingChar;
                    continue;
                }

                foreach (var item in @this)
                {
                    tmp[index] = item;
                    index++;
                }
                break;
            }

            return new ReadOnlySpan<char>(tmp.ToArray());
        }

        internal static unsafe ReadOnlySpan<char> PadRight(this ReadOnlySpan<char> @this, int totalWidth, char paddingChar)
        {
            int totalPad = totalWidth - @this.Length;
            bool shouldNotPad = totalWidth - @this.Length < 1;
            if (shouldNotPad) return @this;

            Span<char> tmp = stackalloc char[totalWidth];

            ushort index = 0;

            foreach (var item in @this)
            {
                tmp[index] = item;
                index++;
                continue;
            }


            for (int i = index; i < totalWidth; i++)
            {             
                tmp[i] = paddingChar;
            }

            return new ReadOnlySpan<char>(tmp.ToArray());
        }
    }
}
