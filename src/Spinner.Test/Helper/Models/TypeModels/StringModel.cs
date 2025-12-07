using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class StringModel
    {
        [ReadProperty(0, 20)]
        public string Value { get; set; }
    }
}
