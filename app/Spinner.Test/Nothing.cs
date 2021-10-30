using Spinner.Attribute;

namespace Spinner.Test
{
    [ContextProperty(lenght: 50)]
    internal struct Nothing
    {
        public Nothing(string name, string adress)
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
