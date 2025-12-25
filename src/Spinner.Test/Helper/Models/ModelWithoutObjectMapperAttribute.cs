using Spinner.Attribute;

namespace Spinner.Test.Helper.Models
{
    public class ModelWithoutObjectMapperAttribute
    {
        [WriteProperty(10, 0, ' ')]
        public string Value { get; set; }
    }
}
