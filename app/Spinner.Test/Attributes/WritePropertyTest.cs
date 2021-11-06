using Spinner.Attribute;
using Spinner.Test.Helper;
using Xunit;
using System;
using System.Linq;

namespace Spinner.Test.Attributes
{
    public class WritePropertyTest
    {
        [Fact]
        public void Should_ValidateHowManyContructorsExistsInWritePropertyFile()
        {
            // Arrange & Act
            var constructors = FileInpect<WriteProperty>.GetConstructors();

            // Assert
            Assert.Equal(2, constructors.Length);
        }

        [Fact]
        public void Should_ValidateParansTypeAndNameOfFirstConstructors()
        {
            // Arrange & Act
            var constructors = FileInpect<WriteProperty>.GetConstructors();
            var firstConstructor = constructors[0];
            var parameters = firstConstructor.GetParameters();

            // Assert
            Assert.Equal(3, parameters.Length);

            Assert.Equal("lenght", parameters[0].Name);
            Assert.Equal(typeof(ushort), parameters[0].ParameterType);

            Assert.Equal("order", parameters[1].Name);
            Assert.Equal(typeof(ushort), parameters[1].ParameterType);

            Assert.Equal("paddingChar", parameters[2].Name);
            Assert.Equal(typeof(char), parameters[2].ParameterType);
        }

        [Fact]
        public void Should_ValidateParansTypeAndNameOfSecondConstructors()
        {
            // Arrange & Act
            var constructors = FileInpect<WriteProperty>.GetConstructors();
            var secondConstructor = constructors[1];
            var parameters = secondConstructor.GetParameters();

            // Assert
            Assert.Equal(4, parameters.Length);

            Assert.Equal("lenght", parameters[0].Name);
            Assert.Equal(typeof(ushort), parameters[0].ParameterType);

            Assert.Equal("order", parameters[1].Name);
            Assert.Equal(typeof(ushort), parameters[1].ParameterType);

            Assert.Equal("paddingChar", parameters[2].Name);
            Assert.Equal(typeof(char), parameters[2].ParameterType);
        }

        [Fact]
        public void Should_ValidateHowManyAttributesExistsInWritePropertyFile()
        {
            // Arrange & Act
            var attibutes = FileInpect<WriteProperty>.GetAttributes();

            var attributeUsage = attibutes.First() as AttributeUsageAttribute;

            // Assert
            Assert.Single(attibutes);
            Assert.Equal(AttributeTargets.Property, attributeUsage.ValidOn);

        }
    }
}
