using Spinner.Attribute;
using Spinner.Test.Helper;
using Xunit;
using System;
using System.Linq;
using System.Reflection;

namespace Spinner.Test.Attributes
{
    public class WritePropertyTest
    {
        [Fact]
        public void Should_ValidateHowManyContructorsExistsInWritePropertyFile()
        {
            // Arrange & Act
            ConstructorInfo[] constructors = FileInpect<WriteProperty>.GetConstructors();

            // Assert
            Assert.Equal(2, constructors.Length);
        }

        [Fact]
        public void Should_ValidateParansTypeAndNameOfFirstConstructors()
        {
            // Arrange & Act
            ConstructorInfo[] constructors = FileInpect<WriteProperty>.GetConstructors();
            ConstructorInfo firstConstructor = constructors[0];
            ParameterInfo[] parameters = firstConstructor.GetParameters();

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
            ConstructorInfo[] constructors = FileInpect<WriteProperty>.GetConstructors();
            ConstructorInfo secondConstructor = constructors[1];
            ParameterInfo[] parameters = secondConstructor.GetParameters();

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
            object[] attibutes = FileInpect<WriteProperty>.GetAttributes();

            AttributeUsageAttribute attributeUsage = attibutes.First() as AttributeUsageAttribute;

            // Assert
            Assert.Single(attibutes);
            Assert.Equal(AttributeTargets.Property, attributeUsage.ValidOn);
        }
    }
}
