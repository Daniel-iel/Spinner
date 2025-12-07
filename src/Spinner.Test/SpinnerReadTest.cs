using Spinner.Attribute;
using Spinner.Exceptions;
using Spinner.Internals.Cache;
using Spinner.Test.Helper.Interceptors;
using Spinner.Test.Helper.Models;
using System;
using System.Linq;
using Xunit;

namespace Spinner.Test
{
    public class SpinnerReadTest
    {
        [Fact]
        public void ReadFromString_WhenCalled_ShouldReturnObjectMappedFromString()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinnerWriter = new Spinner<NothingPadLeft>();
            Spinner<NothingReader> spinnerReader = new Spinner<NothingReader>();

            // Act
            string positionalString = spinnerWriter.WriteAsString(nothing);
            NothingReader nothingReader = spinnerReader.ReadFromString(positionalString);

            // Assert
            Assert.True(nothing.GetHashCode() == nothingReader.GetHashCode());
        }

        [Fact]
        public void ReadFromString_WhenCalled_ShouldParsePropertyToDecimal()
        {
            // Arrange
            NothingDecimal nothing = new NothingDecimal("0001");
            Spinner<NothingDecimal> spinnerWriter = new Spinner<NothingDecimal>();
            Spinner<NothingDecimalReader> spinnerReader = new Spinner<NothingDecimalReader>();

            // Act
            string positionalString = spinnerWriter.WriteAsString(nothing);
            NothingDecimalReader nothingReader = spinnerReader.ReadFromString(positionalString);

            // Assert
            Assert.Equal(00.01M, nothingReader.Value);
        }

        [Fact]
        public void ReadFromString_WhenCalled_ShouldValidateIfPropertyInterceptorIsCached()
        {
            // Arrange
            NothingDecimal nothing = new NothingDecimal("0001");
            Spinner<NothingDecimal> spinnerWriter = new Spinner<NothingDecimal>();
            Spinner<NothingDecimalReader> spinnerReader = new Spinner<NothingDecimalReader>();

            string positionalString = spinnerWriter.WriteAsString(nothing);
            spinnerReader.ReadFromString(positionalString);

            // Act
            bool decimalInterceptorWasCached = InterceptorCache.TryGet<string>(typeof(DecimalInterceptor), out Interceptors.IInterceptor<string> interceptor);

            // Assert
            Assert.True(decimalInterceptorWasCached);
            Assert.IsType<DecimalInterceptor>(interceptor);
        }

        [Fact]
        public void ReadFromString_WhenCalled_ShouldValidateIfTwoResponsesAreDifferent()
        {
            // Arrange
            NothingPadLeft nothingFirst = new NothingPadLeft("spinnerFirst", "www.spinner.com.br");
            NothingPadLeft nothingSecond = new NothingPadLeft("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadLeft> spinnerWriter = new Spinner<NothingPadLeft>();

            Spinner<NothingReader> spinnerReaderFirst = new Spinner<NothingReader>();
            Spinner<NothingReader> spinnerReaderSecond = new Spinner<NothingReader>();

            // Act
            string positionalStringFirst = spinnerWriter.WriteAsString(nothingFirst);
            string positionalStringSecond = spinnerWriter.WriteAsString(nothingSecond);

            NothingReader nothingReaderFirst = spinnerReaderFirst.ReadFromString(positionalStringFirst);
            NothingReader nothingReaderSecond = spinnerReaderSecond.ReadFromString(positionalStringSecond);

            // Assert
            Assert.True(nothingReaderFirst.GetHashCode() != nothingReaderSecond.GetHashCode());
        }

        [Fact]
        public void GetReadProperties_WhenCaller_ShouldValidateHowManyPropertiesWasMapped()
        {
            // Act
            var properties = typeof(NothingReader).GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(ReadPropertyAttribute), false).Length > 0)
                .ToArray();

            // Assert
            Assert.Equal(2, properties.Length);
            Assert.Equal("Name", properties[0].Name);
            Assert.Equal("WebSite", properties[1].Name);
        }

        [Fact]
        public void ReadFromString_WhenCalled_ShouldThrowExceptionIfNotExistsAnyPropertiesWithReadPropertyAttribute()
        {
            Action act = () =>
            {
                // Arrange
                Spinner<NothingNoAttribute> spinnerReader = new Spinner<NothingNoAttribute>();

                // Act
                spinnerReader.ReadFromString("");
            };

            // Assert
            PropertyNotMappedException ex = Assert.Throws<PropertyNotMappedException>(act);
            Assert.Equal("Type Spinner.Test.Helper.Models.NothingNoAttribute does not have properties mapped for reading.", ex.Message);
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
    }
}