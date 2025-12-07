using Spinner.Attribute;

namespace Spinner.Test.Helper.Models
{
    [ObjectMapper(0)]
    public class ModelWithZeroLength
    {
        [WriteProperty(10, 0, ' ')]
        public string Value { get; set; }
    }
}
