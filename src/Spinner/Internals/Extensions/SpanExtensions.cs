using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Spinner.Test")]

namespace Spinner.Internals.Extensions
{
    internal static class SpanExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static unsafe ReadOnlySpan<char> PadLeft(this ReadOnlySpan<char> @this, int totalWidth,
            char paddingChar)
        {
            if (@this.Length >= totalWidth)
            {
                return @this;
            }

            Span<char> newString = stackalloc char[totalWidth];
            newString[..(totalWidth - @this.Length)].Fill(paddingChar);
            @this.CopyTo(newString[(totalWidth - @this.Length)..]);
            return newString.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static unsafe ReadOnlySpan<char> PadRight(this ReadOnlySpan<char> @this, int totalWidth, char paddingChar)
        {
            if (@this.Length >= totalWidth)
            {
                return @this;
            }

            Span<char> newString = stackalloc char[totalWidth];
            @this.CopyTo(newString);

            for (int i = @this.Length; i < totalWidth; i++)
            {
                newString[i] = paddingChar;
            }

            return newString.ToArray();
        }
    }
}