using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class Int64Model
    {
        [ReadProperty(0, 19)]
        public long Value { get; set; }
    }
}
