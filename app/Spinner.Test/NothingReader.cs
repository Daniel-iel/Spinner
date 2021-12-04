using Spinner.Attribute;

namespace Spinner.Test
{
    [ObjectMapper(lenght: 50)]
    internal class NothingReader
    {
        [ReadProperty(start:1, lenght: 19 )]        
        public string Name { get; private set; }

        [ReadProperty(start: 20, lenght: 30)]        
        public string Adress { get; private set; }
    }
}
