using Spinner.Interceptors;

namespace Spinner.Test.Helper.Interceptors
{
    internal sealed class CacheInterceptor : IInterceptors
    {
        public object Parse(object obj)
        {
            return obj;
        }
    }
}