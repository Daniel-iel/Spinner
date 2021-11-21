using Spinner.Attribute;

namespace Spinner.Test
{
    [ObjectMapper(lenght: 50)]
    internal struct NothingLeft
    {
        public NothingLeft(string name, string adress)
        {
            this.Name = name;
            this.Adress = adress;
        }

        [WriteProperty(lenght: 20, order: 1, paddingChar: ' ')]
        public string Name { get; private set; }

        [WriteProperty(lenght: 30, order: 2, paddingChar: ' ')]
        public string Adress { get; private set; }
    }
}
