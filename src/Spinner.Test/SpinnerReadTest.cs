using Spinner.Attribute;
using Spinner.Exceptions;
using Spinner.Interceptors;
using Spinner.Internals.Cache;
using Spinner.Test.Helper.Interceptors;
using Spinner.Test.Models;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
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
            NothingDecimalReader nothingDecimalReader = spinnerReader.ReadFromString(positionalString);

            // Act
            bool decimalInterceptorWasCached = InterceptorCache.TryGet(typeof(DecimalInterceptor), out IInterceptor interceptor);

            // Assert
            Assert.True(decimalInterceptorWasCached);
            Assert.IsType<DecimalInterceptor>(interceptor);
        }

        [Fact]
        public void ReadFromSpan_WhenCalled_ShouldReturnObjectMappedAsSpan()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinnerWriter = new Spinner<NothingPadLeft>();
            Spinner<NothingReader> spinnerReader = new Spinner<NothingReader>();

            // Act
            ReadOnlySpan<char> positionalString = spinnerWriter.WriteAsSpan(nothing);
            NothingReader nothingReader = spinnerReader.ReadFromSpan(positionalString);

            // Assert
            Assert.True(nothing.GetHashCode() == nothingReader.GetHashCode());
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
        public void ReadFromSpan_WhenCalled_ShouldValidateIfTwoResponsesAreDifferent()
        {
            // Arrange
            NothingPadLeft nothingFirst = new NothingPadLeft("spinnerFirst", "www.spinner.com.br");
            NothingPadLeft nothingSecond = new NothingPadLeft("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadLeft> spinnerWriter = new Spinner<NothingPadLeft>();

            Spinner<NothingReader> spinnerReaderFirst = new Spinner<NothingReader>();
            Spinner<NothingReader> spinnerReaderSecond = new Spinner<NothingReader>();

            // Act
            ReadOnlySpan<char> positionalStringFirst = spinnerWriter.WriteAsSpan(nothingFirst);
            ReadOnlySpan<char> positionalStringSecond = spinnerWriter.WriteAsSpan(nothingSecond);

            NothingReader nothingReaderFirst = spinnerReaderFirst.ReadFromSpan(positionalStringFirst);
            NothingReader nothingReaderSecond = spinnerReaderSecond.ReadFromSpan(positionalStringSecond);

            // Assert
            Assert.True(nothingReaderFirst.GetHashCode() != nothingReaderSecond.GetHashCode());
        }

        [Fact]
        public void GetReadProperties_WhenCaller_ShouldValidateHowManyPropertiesWasMapped()
        {
            // Act
            var properties = typeof(NothingReader).GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(ReadPropertyAttribute), false).Any())
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
            Assert.Equal("Property Name should have ReadProperty configured.", ex.Message);
        }

        [Fact]
        public void ReadFromSpan_WhenCalled_ShouldThrowExceptionIfNotExistsAnyPropertiesWithReadPropertyAttribute()
        {
            Action act = () =>
            {
                // Arrange
                Spinner<NothingNoAttribute> spinnerReader = new Spinner<NothingNoAttribute>();

                // Act
                spinnerReader.ReadFromSpan("");
            };

            // Assert
            PropertyNotMappedException ex = Assert.Throws<PropertyNotMappedException>(act);
            Assert.Equal("Property Name should have ReadProperty configured.", ex.Message);
        }
    }
}