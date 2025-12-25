using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class SingleModel
    {
        [ReadProperty(0, 10)]
        public float Value { get; set; }
    }
}
