using Spinner.Parsers;

namespace Writer.Benchmark
{
    internal class ParserWebSite : ITypeParser
    {
        public object Parser(object propertyValue)
        {
            return $"WebSite: {propertyValue.ToString()}";
        }
    }
}
