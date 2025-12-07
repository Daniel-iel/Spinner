using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class Int16Model
    {
        [ReadProperty(0, 6)]
        public short Value { get; set; }
    }
}
