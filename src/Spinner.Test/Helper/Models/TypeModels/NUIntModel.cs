using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class NUIntModel
    {
        [ReadProperty(0, 10)]
        public nuint Value { get; set; }
    }
}
