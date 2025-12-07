using Spinner.Attribute;

namespace Spinner.Test.Helper.Models
{
    [ObjectMapper(300)]
    public class ModelWithLargeLength
    {
        [WriteProperty(300, 0, ' ')]
        public string Data { get; set; }
    }
}
