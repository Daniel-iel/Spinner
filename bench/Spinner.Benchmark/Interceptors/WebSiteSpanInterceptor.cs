using Spinner.Interceptors;
using System;

namespace Spinner.Benchmark.Interceptors
{
    /// <summary>
    /// High-performance interceptor que usa ReadOnlySpan para evitar alocações desnecessárias.
    /// </summary>
    internal class WebSiteSpanInterceptor : ISpanInterceptor
    {
        private const string Prefix = "WebSite: ";

        /// <summary>
        /// Fallback para interface antiga IInterceptor (compatibilidade).
        /// </summary>
        public object Parse(object obj)
        {
            return $"{Prefix}{obj?.ToString() ?? string.Empty}";
        }

        /// <summary>
        /// Versão otimizada que processa ReadOnlySpan diretamente - zero alocações extras.
        /// </summary>
        public object ParseSpan(ReadOnlySpan<char> span)
        {
            // Usar string.Concat para criar a string final com tamanho exato conhecido
            // Evita alocações intermediárias de string
            return string.Concat(Prefix, span);
        }
    }
}
