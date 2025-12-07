using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class UInt32Model
    {
        [ReadProperty(0, 10)]
        public uint Value { get; set; }
    }
}
