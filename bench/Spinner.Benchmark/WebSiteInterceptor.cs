using Spinner.Interceptors;

namespace Writer.Benchmark
{
    internal class WebSiteInterceptor : IInterceptors
    {
        public object Parse(object propertyValue)
        {
            return $"WebSite: {propertyValue.ToString()}";
        }
    }
}
