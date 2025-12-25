using Spinner.Attribute;

namespace Spinner.Test.Helper.Models
{
    [ObjectMapper(50)]
    public class ModelWithValidLength
    {
        [WriteProperty(25, 0, ' ')]
        public string Name { get; set; }

        [WriteProperty(25, 1, ' ')]
        public string Description { get; set; }
    }
}
