using Spinner.Interceptors;

namespace Spinner.Test.Helper.Interceptors
{
    internal sealed class CacheInterceptor : IInterceptor
    {
        public object Parse(string obj)
        {
            return obj;
        }
    }
}