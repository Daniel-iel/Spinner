﻿using Spinner.Attribute;
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
        public void Should_ValidateParamsTypeAndNameOfSecondConstructor()
        {
            // Arrange
            ConstructorInfo[] constructors = FileInspect<ReadPropertyAttribute>.GetConstructors();
            ConstructorInfo firstConstructor = constructors[1];

            // Act
            ParameterInfo[] parameters = firstConstructor.GetParameters();

            // Assert
            Assert.Equal(3, parameters.Length);

            Assert.Equal("start", parameters[0].Name);
            Assert.Equal(typeof(ushort), parameters[0].ParameterType);

            Assert.Equal("length", parameters[1].Name);
            Assert.Equal(typeof(ushort), parameters[1].ParameterType);

            Assert.Equal("type", parameters[2].Name);
            Assert.Equal(typeof(Type), parameters[2].ParameterType);
        }

        [Fact]
        public void Should_ValidateHowManyAttributesExistsInReadPropertyAttributeFile()
        {
            // Arrange & Act
            object[] attributes = FileInspect<ReadPropertyAttribute>.GetAttributes();

            AttributeUsageAttribute attributeUsage = attributes[0] as AttributeUsageAttribute;

            // Assert
            Assert.Single(attributes);
            Assert.Equal(AttributeTargets.Property, attributeUsage.ValidOn);
        }
    }
}