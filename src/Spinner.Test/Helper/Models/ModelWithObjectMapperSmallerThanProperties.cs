using Spinner.Attribute;

namespace Spinner.Test.Helper.Models
{
    [ObjectMapper(length: 30)]
    internal sealed class ModelWithObjectMapperSmallerThanProperties
    {
        public ModelWithObjectMapperSmallerThanProperties() { }

        public ModelWithObjectMapperSmallerThanProperties(string firstName, string lastName)
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
