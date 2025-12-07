using Spinner.Attribute;

namespace Spinner.Test.Helper.Models
{
    internal sealed class ModelWithoutObjectMapper
    {
        public ModelWithoutObjectMapper() { }

        public ModelWithoutObjectMapper(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
        public string FirstName { get; set; }

        [WriteProperty(length: 20, order: 2, paddingChar: ' ')]
        public string LastName { get; set; }
    }
}
