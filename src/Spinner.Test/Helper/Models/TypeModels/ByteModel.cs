using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class ByteModel
    {
        [ReadProperty(0, 3)]
        public byte Value { get; set; }
    }
}
