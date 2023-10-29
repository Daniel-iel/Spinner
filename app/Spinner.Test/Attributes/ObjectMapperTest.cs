using Spinner.Attribute;
using Spinner.Test.Helper;
using System;
using System.Reflection;
using Xunit;

namespace Spinner.Test.Attributes
{
    public class ObjectMapperTest
    {
        [Fact]
        public void Should_ValidateHowManyConstructorExistsInObjectMapperAttributeFile()
        {
            // Arrange & Act
            ConstructorInfo[] constructors = FileInspect<ObjectMapperAttribute>.GetConstructors();

            // Assert
            Assert.Single(constructors);
        }

        [Fact]
        public void Should_ValidateParamsTypeAndNameOnConstructor()
        {
            // Arrange
            ConstructorInfo[] constructors = FileInspect<ObjectMapperAttribute>.GetConstructors();
            ConstructorInfo firstConstructor = constructors[0];

            // Act
            ParameterInfo[] parameters = firstConstructor.GetParameters();

            // Assert
            Assert.Single(parameters);

            Assert.Equal("length", parameters[0].Name);
            Assert.Equal(typeof(ushort), parameters[0].ParameterType);
        }

        [Fact]
        public void Should_ValidateHowManyAttributesExistsInObjectMapperAttributeFile()
        {
            // Arrange & Act
            object[] attributes = FileInspect<ObjectMapperAttribute>.GetAttributes();

            AttributeUsageAttribute attributeUsage = attributes[0] as AttributeUsageAttribute;

            // Assert
            Assert.Single(attributes);
            Assert.Equal(AttributeTargets.Struct | AttributeTargets.Class, attributeUsage.ValidOn);
        }
    }
}