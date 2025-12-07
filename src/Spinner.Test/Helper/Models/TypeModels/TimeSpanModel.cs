using Spinner.Attribute;

namespace Spinner.Test.Helper.Models.TypeModels
{
    public class TimeSpanModel
    {
        [ReadProperty(0, 20)]
        public System.TimeSpan Value { get; set; }
    }
}
