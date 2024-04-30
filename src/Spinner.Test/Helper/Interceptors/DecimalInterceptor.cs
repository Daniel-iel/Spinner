using Spinner.Interceptors;
using System.Globalization;

namespace Spinner.Test.Helper.Interceptors
{
    internal sealed class DecimalInterceptor : IInterceptors
    {
        public object Parse(object obj)
        {
            string value = obj.ToString().Insert(2, ".");
            return decimal.Parse(value, new CultureInfo("en-US"));
        }
    }
}