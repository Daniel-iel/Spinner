using Spinner.Attribute;
using Spinner.Test.Helper;
using System;
using System.Reflection;
using Xunit;

namespace Spinner.Test.Attributes
{
    public class WritePropertyTest
    {
        [Fact]
        public void Should_ValidateHowManyContructorsExistsInWritePropertyFile()
        {
            // Arrange & Act
            ConstructorInfo[] constructors = FileInspect<WritePropertyAttribute>.GetConstructors();

            // Assert
            Assert.Equal(2, constructors.Length);
        }

        [Fact]
        public void Should_ValidateParamsTypeAndNameOfFirstConstructor()
        {
            // Arrange
            ConstructorInfo[] constructors = FileInspect<WritePropertyAttribute>.GetConstructors();
            ConstructorInfo firstConstructor = constructors[0];

            // Act
            ParameterInfo[] parameters = firstConstructor.GetParameters();

            // Assert
            Assert.Equal(3, parameters.Length);

            Assert.Equal("length", parameters[0].Name);
            Assert.Equal(typeof(ushort), parameters[0].ParameterType);

            Assert.Equal("order", parameters[1].Name);
            Assert.Equal(typeof(ushort), parameters[1].ParameterType);

            Assert.Equal("paddingChar", parameters[2].Name);
            Assert.Equal(typeof(char), parameters[2].ParameterType);
        }

        [Fact]
        public void Should_ValidateParansTypeAndNameOfSecondConstructors()
        {
            // Arrange
            ConstructorInfo[] constructors = FileInspect<WritePropertyAttribute>.GetConstructors();
            ConstructorInfo secondConstructor = constructors[1];

            // Act
            ParameterInfo[] parameters = secondConstructor.GetParameters();

            // Assert
            Assert.Equal(4, parameters.Length);

            Assert.Equal("length", parameters[0].Name);
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
            object[] attibutes = FileInspect<WritePropertyAttribute>.GetAttributes();

            AttributeUsageAttribute attributeUsage = attibutes[0] as AttributeUsageAttribute;

            // Assert
            Assert.Single(attibutes);
            Assert.Equal(AttributeTargets.Property, attributeUsage.ValidOn);
        }
    }
}