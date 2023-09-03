using Spinner.Cache;
using Spinner.Exceptions;
using Spinner.Test.Helper.Parses;
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
            Spinner<NothingPadLeft> spinnerWriter = new Spinner<NothingPadLeft>(nothing);
            Spinner<NothingReader> spinnerReader = new Spinner<NothingReader>();

            // Act
            string positionalString = spinnerWriter.WriteAsString();
            NothingReader nothingReader = spinnerReader.ReadFromString(positionalString);

            // Assert
            Assert.True(nothing.GetHashCode() == nothingReader.GetHashCode());
        }

        [Fact]
        public void ReadFromString_WhenCalled_ShouldParsePropertyToDecimal()
        {
            // Arrange
            NothingDecimal nothing = new NothingDecimal("0001");
            Spinner<NothingDecimal> spinnerWriter = new Spinner<NothingDecimal>(nothing);
            Spinner<NothingDecimalReader> spinnerReader = new Spinner<NothingDecimalReader>();

            // Act
            string positionalString = spinnerWriter.WriteAsString();
            NothingDecimalReader nothingReader = spinnerReader.ReadFromString(positionalString);

            // Assert
            Assert.Equal(00.01M, nothingReader.Value);
        }

        [Fact]
        public void ReadFromString_WhenCalled_ShouldValidateIfPropertyParseIsCached()
        {
            // Arrange
            NothingDecimal nothing = new NothingDecimal("0001");
            Spinner<NothingDecimal> spinnerWriter = new Spinner<NothingDecimal>(nothing);
            Spinner<NothingDecimalReader> spinnerReader = new Spinner<NothingDecimalReader>();

            string positionalString = spinnerWriter.WriteAsString();
            spinnerReader.ReadFromString(positionalString);

            // Act
            bool decimalParserWasCached = ParserTypeCache.Parses.Any(c => c is DecimalParser);

            // Assert
            Assert.True(decimalParserWasCached);
        }

        [Fact]
        public void ReadFromSpan_WhenCalled_ShouldReturnObjectMappedAsSpan()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinnerWriter = new Spinner<NothingPadLeft>(nothing);
            Spinner<NothingReader> spinnerReader = new Spinner<NothingReader>();

            // Act
            ReadOnlySpan<char> positionalString = spinnerWriter.WriteAsSpan();
            NothingReader nothingReader = spinnerReader.ReadFromSpan(positionalString);

            // Assert
            Assert.True(nothing.GetHashCode() == nothingReader.GetHashCode());
        }

        [Fact]
        public void ReadFromString_WhenCalled_ShouldValidateIfTwoResponsesAreManyDifferent()
        {
            // Arrange
            NothingPadLeft nothingFirst = new NothingPadLeft("spinnerFirst", "www.spinner.com.br");
            NothingPadLeft nothingSecond = new NothingPadLeft("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadLeft> spinnerWriterFirst = new Spinner<NothingPadLeft>(nothingFirst);
            Spinner<NothingPadLeft> spinnerWriterSecond = new Spinner<NothingPadLeft>(nothingSecond);

            Spinner<NothingReader> spinnerReaderFirst = new Spinner<NothingReader>();
            Spinner<NothingReader> spinnerReaderSecond = new Spinner<NothingReader>();

            // Act
            string positionalStringFirst = spinnerWriterFirst.WriteAsString();
            string positionalStringSecond = spinnerWriterSecond.WriteAsString();

            NothingReader nothingReaderFirst = spinnerReaderFirst.ReadFromString(positionalStringFirst);
            NothingReader nothingReaderSecond = spinnerReaderSecond.ReadFromString(positionalStringSecond);

            // Assert
            Assert.True(nothingReaderFirst.GetHashCode() != nothingReaderSecond.GetHashCode());
        }

        [Fact]
        public void ReadFromSpan_WhenCalled_ShouldValidateIfTwoResponsesAreManyDifferent()
        {
            // Arrange
            NothingPadLeft nothingFirst = new NothingPadLeft("spinnerFirst", "www.spinner.com.br");
            NothingPadLeft nothingSecond = new NothingPadLeft("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadLeft> spinnerWriterFirst = new Spinner<NothingPadLeft>(nothingFirst);
            Spinner<NothingPadLeft> spinnerWriterSecond = new Spinner<NothingPadLeft>(nothingSecond);

            Spinner<NothingReader> spinnerReaderFirst = new Spinner<NothingReader>();
            Spinner<NothingReader> spinnerReaderSecond = new Spinner<NothingReader>();

            // Act
            ReadOnlySpan<char> positionalStringFirst = spinnerWriterFirst.WriteAsSpan();
            ReadOnlySpan<char> positionalStringSecond = spinnerWriterSecond.WriteAsSpan();

            NothingReader nothingReaderFirst = spinnerReaderFirst.ReadFromSpan(positionalStringFirst);
            NothingReader nothingReaderSecond = spinnerReaderSecond.ReadFromSpan(positionalStringSecond);

            // Assert
            Assert.True(nothingReaderFirst.GetHashCode() != nothingReaderSecond.GetHashCode());
        }

        [Fact]
        public void GetReadProperties_WhenCaller_ShouldValidateHowManyPropertiesWasMapped()
        {
            Spinner<NothingReader> spinner = new Spinner<NothingReader>();

            ImmutableList<PropertyInfo> props = spinner.GetReadProperties.ToImmutableList();

            Assert.Equal(2, props.Count);
            Assert.Equal("Name", props.First().Name);
            Assert.Equal("WebSite", props.Last().Name);
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