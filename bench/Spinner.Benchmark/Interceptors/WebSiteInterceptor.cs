using Spinner.Interceptors;

namespace Spinner.Benchmark.Interceptors
{
    internal class WebSiteInterceptor : IInterceptor
    {
        public string Parse(string propertyValue)
        {
            return $"WebSite: {propertyValue}";
        }
    }
}
