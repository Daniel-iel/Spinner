using Spinner.Attribute;
using Spinner.Enum;

namespace Spinner.Test
{
    [ObjectMapper(lenght: 50)]
    internal struct NothingRight
    {
        public NothingRight(string name, string adress)
        {
            this.Name = name;
            this.Adress = adress;
        }

        [WriteProperty(lenght: 20, order: 1, paddingChar: ' ', PaddingType.Right)]
        public string Name { get; private set; }

        [WriteProperty(lenght: 30, order: 2, paddingChar: ' ', PaddingType.Right)]
        public string Adress { get; private set; }
    }
}
