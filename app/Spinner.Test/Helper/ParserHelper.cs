using Spinner.Parsers;

namespace Spinner.Test.Helper
{
    internal class CacheParser : ITypeParse
    {
        public object Parser(object obj)
        {
            return obj;
        }
    }

    internal class DecimalParser : ITypeParse
    {
        public object Parser(object obj)
        {
            string value = obj.ToString().Insert(2, ",");
            return decimal.Parse(value);
        }
    }
}
