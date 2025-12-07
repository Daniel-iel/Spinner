using Spinner.Attribute;
using System;
using Xunit;

namespace Spinner.Test
{
    public class InvalidOperationExceptionTest
    {
        [Fact]
        public void ReadFromString_WhenPropertyHasNoSetter_ShouldThrowInvalidOperationException()
        {
            // Arrange
            const string input = "test value";

            // Act & Assert
            var exception = Assert.Throws<TypeInitializationException>(() =>
            {
                var spinner = new Spinner<ModelWithReadOnlyProperty>();
                spinner.ReadFromString(input);
            });

            Assert.NotNull(exception.InnerException);
            Assert.IsType<InvalidOperationException>(exception.InnerException);
            Assert.Contains("does not have a setter", exception.InnerException.Message);
            Assert.Contains("Value", exception.InnerException.Message);
        }

        [Fact]
        public void WriteAsString_WhenPropertyHasNoSetter_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var model = new ModelWithReadOnlyPropertyForWrite();

            // Act & Assert
            var exception = Assert.Throws<TypeInitializationException>(() =>
            {
                var spinner = new Spinner<ModelWithReadOnlyPropertyForWrite>();
                spinner.WriteAsString(model);
            });

            Assert.NotNull(exception.InnerException);
            Assert.IsType<InvalidOperationException>(exception.InnerException);
            Assert.Contains("does not have a setter", exception.InnerException.Message);
            Assert.Contains("Value", exception.InnerException.Message);
        }

        [Fact]
        public void ReadFromString_WhenInterceptorDoesNotImplementIInterceptor_ShouldThrowInvalidOperationException()
        {
            // Arrange
            const string input = "test value";

            // Act & Assert
            var exception = Assert.Throws<TypeInitializationException>(() =>
            {
                var spinner = new Spinner<ModelWithInvalidInterceptor>();
                spinner.ReadFromString(input);
            });

            Assert.NotNull(exception.InnerException);
            Assert.IsType<InvalidOperationException>(exception.InnerException);
            Assert.Contains("does not implement IInterceptor<T>", exception.InnerException.Message);
            Assert.Contains("InvalidInterceptor", exception.InnerException.Message);
        }

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

    public class ModelWithReadOnlyProperty
    {
        [ReadProperty(0, 10)]
        public string Value => "ReadOnly";
    }

    public class ModelWithReadOnlyPropertyForWrite
    {
        [ReadProperty(0, 10)]
        public string Value => "ReadOnly";
    }

    public class ModelWithInvalidInterceptor
    {
        [ReadProperty(0, 10, typeof(InvalidInterceptor))]
        public string Value { get; set; }
    }

    public class InvalidInterceptor
    {
        public string Parse(string value) => value;
    }

    [ObjectMapper(0)]
    public class ModelWithZeroLength
    {
        [WriteProperty(10, 0, ' ')]
        public string Value { get; set; }
    }

    [ObjectMapper(50)]
    public class ModelWithValidLength
    {
        [WriteProperty(25, 0, ' ')]
        public string Name { get; set; }

        [WriteProperty(25, 1, ' ')]
        public string Description { get; set; }
    }

    public class ModelWithoutObjectMapperAttribute
    {
        [WriteProperty(10, 0, ' ')]
        public string Value { get; set; }
    }

    [ObjectMapper(300)]
    public class ModelWithLargeLength
    {
        [WriteProperty(300, 0, ' ')]
        public string Data { get; set; }
    }
}
