using Spinner.Interceptors;

namespace Spinner.Test.Helper.Interceptors
{
    public class StringToObjectInterceptor : IInterceptor<object>
    {
        public object Parse(string value)
        {            
            return int.TryParse(value, out var intValue) ? intValue : value;
        }
    }
}
