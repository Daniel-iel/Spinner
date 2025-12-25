using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class SByteModel
    {
        [ReadProperty(0, 4)]
        public sbyte Value { get; set; }
    }
}
