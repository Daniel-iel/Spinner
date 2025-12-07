using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class DoubleModel
    {
        [ReadProperty(0, 15)]
        public double Value { get; set; }
    }
}
