using Spinner.Interceptors;
using System.Globalization;

namespace Spinner.Test.Helper.Interceptors
{
    public class IntToDoubleInterceptor : IInterceptor<int>
    {
        public int Parse(string value)
        {
            return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) ? result : 0;
        }
    }
}
