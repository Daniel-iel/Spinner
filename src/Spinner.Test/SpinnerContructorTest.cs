using Spinner.Attribute;
using Spinner.Test.Helper.Models;
using System;
using Xunit;

namespace Spinner.Test
{
    public class InvalidOperationExceptionTest
    {    
        [Fact]
        public void Spinner_WhenObjectMapperAttributeLengthIsZero_ShouldThrowInvalidOperationException()
        {
            // Act & Assert
            var exception = Assert.Throws<TypeInitializationException>(() =>
            {
                var spinner = new Spinner<ModelWithZeroLength>();
            });

            Assert.NotNull(exception.InnerException);
            Assert.IsType<InvalidOperationException>(exception.InnerException);
            Assert.Contains("ObjectMapperAttribute.Length must be greater than 0", exception.InnerException.Message);
            Assert.Contains("ModelWithZeroLength", exception.InnerException.Message);
        }

        [Fact]
        public void Spinner_WhenObjectMapperAttributeLengthIsValid_ShouldNotThrow()
        {
            // Act & Assert
            var spinner = new Spinner<ModelWithValidLength>();

            Assert.NotNull(spinner);
            Assert.NotNull(spinner.GetObjectMapper);
            Assert.Equal(50, spinner.GetObjectMapper.Length);
        }

        [Fact]
        public void Spinner_WhenObjectMapperAttributeIsNotPresent_ShouldNotThrow()
        {
            // Act & Assert
            var spinner = new Spinner<ModelWithoutObjectMapperAttribute>();

            Assert.NotNull(spinner);
            Assert.Null(spinner.GetObjectMapper);
        }
    }
}
