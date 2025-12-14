using Spinner.Interceptors;
using System;
using System.Globalization;

namespace Spinner.Test.Helper.Interceptors
{
    public class DateTimeOffsetInterceptor : IInterceptor<DateTimeOffset>
    {
        public DateTimeOffset Parse(string value)
        {
            return DateTimeOffset.Parse(value, CultureInfo.InvariantCulture);
        }
    }
}
