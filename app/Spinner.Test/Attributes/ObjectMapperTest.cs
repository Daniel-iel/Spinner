using Spinner.Attribute;
using Spinner.Test.Helper;
using Xunit;
using System;
using System.Linq;

namespace Spinner.Test.Attributes
{
    public class ObjectMapperTest
    {
        [Fact]
        public void Should_ValidateHowManyContructorsExistsInObjectMapper()
        {
            // Arrange & Act
            var constructors = FileInpect<ObjectMapper>.GetConstructors();

            // Assert
            Assert.Single(constructors);
        }

        [Fact]
        public void Should_ValidateParansTypeAndNameOnConstructor()
        {
            // Arrange & Act
            var constructors = FileInpect<ObjectMapper>.GetConstructors();
            var firstConstructor = constructors[0];
            var parameters = firstConstructor.GetParameters();

            // Assert
            Assert.Single(parameters);

            Assert.Equal("lenght", parameters[0].Name);
            Assert.Equal(typeof(ushort), parameters[0].ParameterType);           
        }

       
        [Fact]
        public void Should_ValidateHowManyAttributesExistsInWritePropertyFile()
        {
            // Arrange & Act
            var attibutes = FileInpect<ObjectMapper>.GetAttributes();

            var attributeUsage = attibutes.First() as AttributeUsageAttribute;

            // Assert
            Assert.Single(attibutes);
            Assert.Equal(AttributeTargets.Struct | AttributeTargets.Class, attributeUsage.ValidOn);
        }
    }
}
