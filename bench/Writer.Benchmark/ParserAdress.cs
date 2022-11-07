using Spinner.Parsers;

namespace Writer.Benchmark
{
    internal class ParserWebSite : ITypeParse
    {
        public object Parser(object propertyValue)
        {
            return $"WebSite: {propertyValue.ToString()}";
        }
    }
}
