using Spinner.Parsers;
using System.Globalization;

namespace Spinner.Test.Helper.Parses
{
    internal sealed class DecimalParser : ITypeParser
    {
        public object Parser(object obj)
        {
            string value = obj.ToString().Insert(2, ".");
            return decimal.Parse(value, new CultureInfo("en-US"));
        }
    }
}