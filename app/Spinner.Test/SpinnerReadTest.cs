using Xunit;
using System.Linq;

namespace Spinner.Test
{
    public class SpinnerReadTest
    {        
        [Fact]
        public void ReadFromString_WhenCalled_ShoudReturnObjectMappedFromString()
        {
            // Arrange
            var nothingLeft = new NothingLeft("spinner", "www.spinner.com.br");
            var spinnerWriter = new Spinner<NothingLeft>(nothingLeft);
            var spinnerReader = new Spinner<NothingReader>();

            // Act
            var stringResponse = spinnerWriter.WriteAsString();
            var nothingReader = spinnerReader.ReadFromString(stringResponse);

            // Assert
            Assert.True(nothingLeft.GetHashCode() == nothingReader.GetHashCode());
        }

        [Fact]
        public void ReadFromSpan_WhenCalled_ShoudReturnObjectMappedAsSpan()
        {
            // Arrange
            var nothingLeft = new NothingLeft("spinner", "www.spinner.com.br");
            var spinnerWriter = new Spinner<NothingLeft>(nothingLeft);
            var spinnerReader = new Spinner<NothingReader>();

            // Act
            var stringResponse = spinnerWriter.WriteAsSpan();
            var nothingReader = spinnerReader.ReadFromSpan(stringResponse);

            // Assert
            Assert.True(nothingLeft.GetHashCode() == nothingReader.GetHashCode());
        }

        [Fact]
        public void ReadFromString_WhenCalled_ShouldValidateIfTwoResponseIsDiferent()
        {    
            // Arrange
            var nothingLeftFirst = new NothingLeft("spinnerFirst", "www.spinner.com.br");
            var nothingLeftSecond = new NothingLeft("spinnerSecond", "www.spinner.com.br");

            var spinnerWriterFirst = new Spinner<NothingLeft>(nothingLeftFirst);
            var spinnerWriterSecond = new Spinner<NothingLeft>(nothingLeftSecond);

            var spinnerReaderFirst = new Spinner<NothingReader>();
            var spinnerReaderSecond = new Spinner<NothingReader>();

            // Act
            var stringResponseFirst = spinnerWriterFirst.WriteAsString();
            var stringResponseSecond = spinnerWriterSecond.WriteAsString();

            var nothingReaderFirst = spinnerReaderFirst.ReadFromString(stringResponseFirst);
            var nothingReaderSecond = spinnerReaderSecond.ReadFromString(stringResponseSecond);

            // Assert
            Assert.True(nothingReaderFirst.GetHashCode() != nothingReaderSecond.GetHashCode());
        }

        [Fact]
        public void ReadFromSpan_WhenCalled_ShouldValidateIfTwoResponseIsDiferent()
        {
            // Arrange
            var nothingLeftFirst = new NothingLeft("spinnerFirst", "www.spinner.com.br");
            var nothingLeftSecond = new NothingLeft("spinnerSecond", "www.spinner.com.br");

            var spinnerWriterFirst = new Spinner<NothingLeft>(nothingLeftFirst);
            var spinnerWriterSecond = new Spinner<NothingLeft>(nothingLeftSecond);

            var spinnerReaderFirst = new Spinner<NothingReader>();
            var spinnerReaderSecond = new Spinner<NothingReader>();

            // Act
            var stringResponseFirst = spinnerWriterFirst.WriteAsSpan();
            var stringResponseSecond = spinnerWriterSecond.WriteAsSpan();

            var nothingReaderFirst = spinnerReaderFirst.ReadFromSpan(stringResponseFirst);
            var nothingReaderSecond = spinnerReaderSecond.ReadFromSpan(stringResponseSecond);

            // Assert
            Assert.True(nothingReaderFirst.GetHashCode() != nothingReaderSecond.GetHashCode());
        }

        [Fact]
        public void GetWriteProperties_WhenCaller_ShouldValidadeHowManyPropertiesWasMapped()
        {
            var spinnerFirst = new Spinner<NothingReader>();

            var props = spinnerFirst.GetReadProperties;

            Assert.Equal(2, props.Count());
            Assert.Equal("Name", props.First().Name);
            Assert.Equal("Adress", props.Last().Name);
        }
    }
}
