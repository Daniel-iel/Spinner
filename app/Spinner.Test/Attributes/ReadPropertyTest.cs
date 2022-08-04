using Spinner.Attribute;
using Spinner.Test.Helper;
using Xunit;
using System;
using System.Reflection;

namespace Spinner.Test.Attributes
{
    public class ReadPropertyTest
    {
        [Fact]
        public void Should_ValidateHowManyContructorsExistsInWritePropertyFile()
        {
            // Arrange & Act
            ConstructorInfo[] constructors = FileInpect<ReadPropertyAttribute>.GetConstructors();

            // Assert
            Assert.Single(constructors);
        }

        [Fact]
        public void Should_ValidateParansTypeAndNameOfFirstConstructors()
        {
            // Arrange & Act
            ConstructorInfo[] constructors = FileInpect<ReadPropertyAttribute>.GetConstructors();
            ConstructorInfo firstConstructor = constructors[0];
            ParameterInfo[] parameters = firstConstructor.GetParameters();

            // Assert
            Assert.Equal(2, parameters.Length);

            Assert.Equal("start", parameters[0].Name);
            Assert.Equal(typeof(ushort), parameters[0].ParameterType);

            Assert.Equal("length", parameters[1].Name);
            Assert.Equal(typeof(ushort), parameters[1].ParameterType);
        }

        [Fact]
        public void Should_ValidateHowManyAttributesExistsInWritePropertyFile()
        {
            // Arrange & Act
            object[] attibutes = FileInpect<ReadPropertyAttribute>.GetAttributes();

            AttributeUsageAttribute attributeUsage = attibutes[0] as AttributeUsageAttribute;

            // Assert
            Assert.Single(attibutes);
            Assert.Equal(AttributeTargets.Property, attributeUsage.ValidOn);
        }
    }
}
