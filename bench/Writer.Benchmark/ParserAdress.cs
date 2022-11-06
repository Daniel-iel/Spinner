using Spinner.Parsers;

namespace Writer.Benchmark
{
    internal class ParserAdress : ITypeParse
    {
        public object Parser(object propertyValue)
        {
            return $"Adress: {propertyValue.ToString()}";
        }
    }
}
