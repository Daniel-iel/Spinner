using Spinner.Attribute;

namespace Spinner.Test
{
    public partial class SpinnerWriteTest
    {
        [ObjectMapper(50)]
        public class ModelWithSmall50Length
        {
            [WriteProperty(50, 0, ' ')]
            public string Data { get; set; }
        }
    }
}