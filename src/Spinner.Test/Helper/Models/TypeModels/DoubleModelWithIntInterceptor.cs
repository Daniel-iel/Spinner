using Spinner.Attribute;
using Spinner.Test.Helper.Interceptors;

namespace Spinner.Test.Helper.Models.TypeModels
{
    [ObjectMapper(10)]
    public class DoubleModelWithIntInterceptor
    {
        [ReadProperty(0, 10, typeof(IntToDoubleInterceptor))]
        public double Value { get; set; }
    }
}
