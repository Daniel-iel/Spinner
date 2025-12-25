using Spinner.Attribute;
using Spinner.Test.Helper.Interceptors;

namespace Spinner.Test.Helper.Models.TypeModels
{
    [ObjectMapper(10)]
    public class BoolAsObjectModel
    {
        [ReadProperty(0, 10, typeof(BoolAsObjectInterceptor))]
        public object Value { get; set; }
    }
}
