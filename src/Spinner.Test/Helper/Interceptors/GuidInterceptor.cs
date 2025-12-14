using Spinner.Interceptors;
using System;

namespace Spinner.Test.Helper.Interceptors
{
    public class GuidInterceptor : IInterceptor<Guid>
    {
        public Guid Parse(string value)
        {
            return Guid.Parse(value);
        }
    }
}
