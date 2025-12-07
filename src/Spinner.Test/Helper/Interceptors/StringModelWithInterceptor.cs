using Spinner.Attribute;

namespace Spinner.Test.Helper.Interceptors
{
    public class StringModelWithInterceptor
    {
        [ReadProperty(0, 20, typeof(ObjectToStringInterceptor))]
        public string Value { get; set; }
    }
}
