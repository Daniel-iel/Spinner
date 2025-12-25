using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class Int32Model
    {
        [ReadProperty(0, 10)]
        public int Value { get; set; }
    }
}
