using Spinner.Attribute;
using Spinner.Test.Helper.Interceptors;
using System;

namespace Spinner.Test.Helper.Models.TypeModels
{
    [ObjectMapper(36)]
    public class GuidModel
    {
        [ReadProperty(0, 36, typeof(GuidInterceptor))]
        public Guid Value { get; set; }
    }
}
