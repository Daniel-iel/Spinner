using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class UInt16Model
    {
        [ReadProperty(0, 5)]
        public ushort Value { get; set; }
    }
}
