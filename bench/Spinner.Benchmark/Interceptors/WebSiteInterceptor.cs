using Spinner.Interceptors;

namespace Spinner.Benchmark.Interceptors
{
    internal class WebSiteInterceptor : IInterceptor
    {
        public object Parse(object propertyValue)
        {
            return $"WebSite: {propertyValue.ToString()}";
        }
    }
}
