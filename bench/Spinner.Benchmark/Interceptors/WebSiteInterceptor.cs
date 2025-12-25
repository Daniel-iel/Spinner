using Spinner.Interceptors;

namespace Spinner.Benchmark.Interceptors
{
    internal class WebSiteInterceptor : IInterceptor<string>
    {
        public string Parse(string propertyValue)
        {
            return $"WebSite: {propertyValue}";
        }
    }
}
