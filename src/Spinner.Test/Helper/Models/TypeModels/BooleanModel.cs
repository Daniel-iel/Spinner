using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class BooleanModel
    {
        [ReadProperty(0, 5)]
        public bool Value { get; set; }
    }
}
