using Spinner.Exceptions;
using Spinner.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Spinner.Test
{
    public class SpinnerReadTest
    {
        [Fact]
        public void ReadFromString_WhenCalled_ShoudReturnObjectMappedFromString()
        {
            // Arrange
            NothingLeft nothingLeft = new NothingLeft("spinner", "www.spinner.com.br");
            Spinner<NothingLeft> spinnerWriter = new Spinner<NothingLeft>(nothingLeft);
            Spinner<NothingReader> spinnerReader = new Spinner<NothingReader>();

            // Act
            string stringResponse = spinnerWriter.WriteAsString();
            NothingReader nothingReader = spinnerReader.ReadFromStringToType(stringResponse);

            // Assert
            Assert.True(nothingLeft.GetHashCode() == nothingReader.GetHashCode());
        }

        [Fact]
        public void ReadFromSpan_WhenCalled_ShoudReturnObjectMappedAsSpan()
        {
            // Arrange
            NothingLeft nothingLeft = new NothingLeft("spinner", "www.spinner.com.br");
            Spinner<NothingLeft> spinnerWriter = new Spinner<NothingLeft>(nothingLeft);
            Spinner<NothingReader> spinnerReader = new Spinner<NothingReader>();

            // Act
            System.ReadOnlySpan<char> stringResponse = spinnerWriter.WriteAsSpan();
            NothingReader nothingReader = spinnerReader.ReadFromSpan(stringResponse);

            // Assert
            Assert.True(nothingLeft.GetHashCode() == nothingReader.GetHashCode());
        }

        [Fact]
        public void ReadFromString_WhenCalled_ShouldValidateIfTwoResponsesAreDiferents()
        {
            // Arrange
            NothingLeft nothingLeftFirst = new NothingLeft("spinnerFirst", "www.spinner.com.br");
            NothingLeft nothingLeftSecond = new NothingLeft("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingLeft> spinnerWriterFirst = new Spinner<NothingLeft>(nothingLeftFirst);
            Spinner<NothingLeft> spinnerWriterSecond = new Spinner<NothingLeft>(nothingLeftSecond);

            Spinner<NothingReader> spinnerReaderFirst = new Spinner<NothingReader>();
            Spinner<NothingReader> spinnerReaderSecond = new Spinner<NothingReader>();

            // Act
            string stringResponseFirst = spinnerWriterFirst.WriteAsString();
            string stringResponseSecond = spinnerWriterSecond.WriteAsString();

            NothingReader nothingReaderFirst = spinnerReaderFirst.ReadFromString(stringResponseFirst);
            NothingReader nothingReaderSecond = spinnerReaderSecond.ReadFromString(stringResponseSecond);

            // Assert
            Assert.True(nothingReaderFirst.GetHashCode() != nothingReaderSecond.GetHashCode());
        }

        [Fact]
        public void ReadFromSpan_WhenCalled_ShouldValidateIfTwoResponsesAreDiferents()
        {
            // Arrange
            NothingLeft nothingLeftFirst = new NothingLeft("spinnerFirst", "www.spinner.com.br");
            NothingLeft nothingLeftSecond = new NothingLeft("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingLeft> spinnerWriterFirst = new Spinner<NothingLeft>(nothingLeftFirst);
            Spinner<NothingLeft> spinnerWriterSecond = new Spinner<NothingLeft>(nothingLeftSecond);

            Spinner<NothingReader> spinnerReaderFirst = new Spinner<NothingReader>();
            Spinner<NothingReader> spinnerReaderSecond = new Spinner<NothingReader>();

            // Act
            System.ReadOnlySpan<char> stringResponseFirst = spinnerWriterFirst.WriteAsSpan();
            System.ReadOnlySpan<char> stringResponseSecond = spinnerWriterSecond.WriteAsSpan();

            NothingReader nothingReaderFirst = spinnerReaderFirst.ReadFromSpan(stringResponseFirst);
            NothingReader nothingReaderSecond = spinnerReaderSecond.ReadFromSpan(stringResponseSecond);

            // Assert
            Assert.True(nothingReaderFirst.GetHashCode() != nothingReaderSecond.GetHashCode());
        }

        [Fact]
        public void GetReadProperties_WhenCaller_ShouldValidadeHowManyPropertiesWasMapped()
        {
            Spinner<NothingReader> spinnerFirst = new Spinner<NothingReader>();

            IEnumerable<PropertyInfo> props = spinnerFirst.GetReadProperties;

            Assert.Equal(2, props.Count());
            Assert.Equal("Name", props.First().Name);
            Assert.Equal("Adress", props.Last().Name);
        }

        [Fact]
        public void ReadFromString_WhenCalled_ShouldThrowExceptionIfNotExistsAnyPropertiesWithReadPropertyAttribute()
        {
            Action act = () =>
            {
                // Arrange
                Spinner<NothingNoAttibute> spinnerReader = new Spinner<NothingNoAttibute>();

                // Act
                spinnerReader.ReadFromString("");
            };

            // Assert
            var ex = Assert.Throws<PropertyNotMappedException>(act);
            Assert.Equal("Property Name should have ReadProperty configured.", ex.Message);
        }

        [Fact]
        public void ReadFromSpan_WhenCalled_ShouldThrowExceptionIfNotExistsAnyPropertiesWithReadPropertyAttribute()
        {
            Action act = () =>
            {
                // Arrange
                Spinner<NothingNoAttibute> spinnerReader = new Spinner<NothingNoAttibute>();

                // Act
                spinnerReader.ReadFromSpan("");
            };

            // Assert
            var ex = Assert.Throws<PropertyNotMappedException>(act);
            Assert.Equal("Property Name should have ReadProperty configured.", ex.Message);
        }
    }
}
