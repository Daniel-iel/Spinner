using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class DateTimeModel
    {
        [ReadProperty(0, 19)]
        public System.DateTime Value { get; set; }
    }
}
