using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class UInt64Model
    {
        [ReadProperty(0, 20)]
        public ulong Value { get; set; }
    }
}
