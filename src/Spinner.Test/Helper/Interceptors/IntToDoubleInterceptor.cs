using Spinner.Interceptors;

namespace Spinner.Test.Helper.Interceptors
{
    public class IntToDoubleInterceptor : IInterceptor<int>
    {
        public int Parse(string value)
        {
            return int.Parse(value);
        }
    }
}
