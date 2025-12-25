using Spinner.Interceptors;

namespace Spinner.Test.Helper.Interceptors
{
    public class StringInterceptor : IInterceptor<string>
    {
        public string Parse(string value)
        {
            return value.Trim();
        }
    }
}
