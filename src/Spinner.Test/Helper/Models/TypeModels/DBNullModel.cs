using Spinner.Attribute;
using Spinner.Test.Helper.Interceptors;
using System;

namespace Spinner.Test.Helper.Models.TypeModels
{
    [ObjectMapper(20)]
    public class DBNullModel
    {
        [ReadProperty(0, 20, typeof(StringInterceptor))]
        public DBNull Value { get; set; }
    }
}
