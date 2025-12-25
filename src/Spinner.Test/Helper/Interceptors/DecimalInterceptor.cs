using Spinner.Interceptors;
using System.Globalization;

namespace Spinner.Test.Helper.Interceptors
{
    internal sealed class DecimalInterceptor : IInterceptor<string>
    {
        public string Parse(string obj)
        {
            string value = obj.Insert(2, ".");
            return decimal.Parse(value, new CultureInfo("en-US")).ToString();
        }
    }
}