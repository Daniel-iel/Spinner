using Spinner.Attribute;
using Spinner.Test.Helper.Interceptors;

namespace Spinner.Test.Helper.Models.TypeModels
{
    [ObjectMapper(10)]
    public class ObjectPropertyModel
    {
        [ReadProperty(0, 10, typeof(StringToObjectInterceptor))]
        public object Value { get; set; }
    }
}
