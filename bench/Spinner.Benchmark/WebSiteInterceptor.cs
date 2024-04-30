using Spinner.Interceptors;

namespace Writer.Benchmark
{
    internal class WebSiteInterceptor : IInterceptor
    {
        public object Parse(object propertyValue)
        {
            return $"WebSite: {propertyValue.ToString()}";
        }
    }
}
