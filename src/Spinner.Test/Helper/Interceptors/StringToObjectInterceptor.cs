using Spinner.Interceptors;
using System.Globalization;

namespace Spinner.Test.Helper.Interceptors
{
    public class StringToObjectInterceptor : IInterceptor<object>
    {
        public object Parse(string value)
        {            
            return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var intValue) ? intValue : value;
        }
    }
}
