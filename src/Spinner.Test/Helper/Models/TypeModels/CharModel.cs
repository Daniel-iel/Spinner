using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class CharModel
    {
        [ReadProperty(0, 1)]
        public char Value { get; set; }
    }
}
