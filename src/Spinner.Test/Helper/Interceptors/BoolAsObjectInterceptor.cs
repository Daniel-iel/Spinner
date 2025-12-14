using Spinner.Interceptors;

namespace Spinner.Test.Helper.Interceptors
{
    public class BoolAsObjectInterceptor : IInterceptor<object>
    {
        public object Parse(string value)
        {
            return bool.TryParse(value.Trim(), out var result) ? result : false;
        }
    }
}
