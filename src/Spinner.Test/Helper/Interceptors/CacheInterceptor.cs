using Spinner.Interceptors;

namespace Spinner.Test.Helper.Interceptors
{
    internal sealed class CacheInterceptor : IInterceptor
    {
        public object Parse(object obj)
        {
            return obj;
        }
    }
}