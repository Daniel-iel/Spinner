using Spinner.Attribute;

namespace Spinner.Test.Helper.Models
{
    public class ModelWithReadOnlyPropertyForWrite
    {
        [ReadProperty(0, 10)]
        public string Value => "ReadOnly";
    }
}
