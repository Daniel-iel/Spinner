using Spinner.Interceptors;

namespace Spinner.Test.Helper.Interceptors
{
    internal sealed class CacheInterceptor : IInterceptor<string>
    {
        public string Parse(string obj)
        {
            return obj;
        }
    }
}