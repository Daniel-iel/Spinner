using Spinner.Attribute;
using Spinner.Test.Helper;
using Xunit;
using System;
using System.Linq;
using System.Reflection;

namespace Spinner.Test.Attributes
{
    public class ObjectMapperTest
    {
        [Fact]
        public void Should_ValidateHowManyContructorsExistsInObjectMapper()
        {
            // Arrange & Act
            ConstructorInfo[] constructors = FileInpect<ObjectMapper>.GetConstructors();

            // Assert
            Assert.Single(constructors);
        }

        [Fact]
        public void Should_ValidateParansTypeAndNameOnConstructor()
        {
            // Arrange & Act
            ConstructorInfo[] constructors = FileInpect<ObjectMapper>.GetConstructors();
            ConstructorInfo firstConstructor = constructors[0];
            ParameterInfo[] parameters = firstConstructor.GetParameters();

            // Assert
            Assert.Single(parameters);

            Assert.Equal("lenght", parameters[0].Name);
            Assert.Equal(typeof(ushort), parameters[0].ParameterType);           
        }
               
        [Fact]
        public void Should_ValidateHowManyAttributesExistsInWritePropertyFile()
        {
            // Arrange & Act
            object[] attibutes = FileInpect<ObjectMapper>.GetAttributes();

            AttributeUsageAttribute attributeUsage = attibutes.First() as AttributeUsageAttribute;

            // Assert
            Assert.Single(attibutes);
            Assert.Equal(AttributeTargets.Struct | AttributeTargets.Class, attributeUsage.ValidOn);
        }
    }
}
