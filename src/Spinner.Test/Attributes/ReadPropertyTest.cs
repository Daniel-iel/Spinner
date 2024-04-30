using Spinner.Attribute;
using Spinner.Test.Helper;
using System;
using System.Reflection;
using Xunit;

namespace Spinner.Test.Attributes
{
    public class ReadPropertyTest
    {
        [Fact]
        public void Should_ValidateHowManyConstructorsExistsInReadPropertyAttributeFile()
        {
            // Arrange & Act
            ConstructorInfo[] constructors = FileInspect<ReadPropertyAttribute>.GetConstructors();

            // Assert
            Assert.Equal(2, constructors.Length);
        }

        [Fact]
        public void Should_ValidateParamsTypeAndNameOfFirstConstructor()
        {
            // Arrange
            ConstructorInfo[] constructors = FileInspect<ReadPropertyAttribute>.GetConstructors();
            ConstructorInfo firstConstructor = constructors[0];

            // Act
            ParameterInfo[] parameters = firstConstructor.GetParameters();

            // Assert
            Assert.Equal(2, parameters.Length);

            Assert.Equal("start", parameters[0].Name);
            Assert.Equal(typeof(ushort), parameters[0].ParameterType);

            Assert.Equal("length", parameters[1].Name);
            Assert.Equal(typeof(ushort), parameters[1].ParameterType);
        }

        [Fact]
        public void Should_ValidateHowManyAttributesExistsInReadPropertyAttributeFile()
        {
            // Arrange & Act
            object[] attibutes = FileInspect<ReadPropertyAttribute>.GetAttributes();

            AttributeUsageAttribute attributeUsage = attibutes[0] as AttributeUsageAttribute;

            // Assert
            Assert.Single(attibutes);
            Assert.Equal(AttributeTargets.Property, attributeUsage.ValidOn);
        }
    }
}