using Spinner.Attribute;
using Spinner.Test.Helper.Interceptors;

namespace Spinner.Test.Helper.Models
{
    public class ModelWithInvalidInterceptor
    {
        [ReadProperty(0, 10, typeof(InvalidInterceptor))]
        public string Value { get; set; }
    }
}
