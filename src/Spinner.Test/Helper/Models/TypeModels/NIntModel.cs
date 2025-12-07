using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class NIntModel
    {
        [ReadProperty(0, 10)]
        public nint Value { get; set; }
    }
}
