using Spinner.Attribute;
using Spinner.Test.Helper.Interceptors;
using System;

namespace Spinner.Test.Helper.Models.TypeModels
{
    [ObjectMapper(30)]
    public class DateTimeOffsetModel
    {
        [ReadProperty(0, 30, typeof(DateTimeOffsetInterceptor))]
        public DateTimeOffset Value { get; set; }
    }
}
