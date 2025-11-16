using System;

namespace Spinner.Interceptors
{
    /// <summary>
    /// High-performance interceptor interface that works with ReadOnlySpan to avoid allocations.
    /// </summary>
    public interface ISpanInterceptor : IInterceptor
    {
        /// <summary>
        /// Parse a ReadOnlySpan of characters without allocating a string.
        /// </summary>
        /// <param name="span">The span to parse</param>
        /// <returns>The parsed object</returns>
        object ParseSpan(ReadOnlySpan<char> span);
    }
}
