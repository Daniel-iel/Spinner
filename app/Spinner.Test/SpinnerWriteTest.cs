using System;
using Xunit;

namespace Spinner.Test
{
    public class SpinnerWriteTest
    {
        [Fact]
        public void WriteAsString_WhenCalled_ShoudReturnObjectMappedAsString()
        {
            // Arrange
            var nothing = new Nothing("spinner", "www.spinner.com.br");
            var spinner = new Spinner<Nothing>(nothing);
            var expected = "             spinner            www.spinner.com.br";

            // Act
            var stringResponse = spinner.WriteAsString();

            // Assert
            Assert.Equal(50, stringResponse.Length);
            Assert.Equal(expected, stringResponse);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthStringThatWasMapped()
        {
            // Arrange
            var nothing = new Nothing("spinner", "www.spinner.com.br");
            var spinner = new Spinner<Nothing>(nothing);
            var expected = "             spinner            www.spinner.com.br";

            // Act
            var conf = spinner.GetConfigurationProperty;
            var stringResponse = spinner.WriteAsString();

            // Assert
            Assert.Equal(conf.Lenght, stringResponse.Length);
            Assert.Equal(expected, stringResponse);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShoudReturnObjectMappedAsSpan()
        {
            // Arrange
            var nothing = new Nothing("spinner", "www.spinner.com.br");
            var spinner = new Spinner<Nothing>(nothing);
            var expected = new ReadOnlySpan<char>("             spinner            www.spinner.com.br".ToCharArray());

            // Act
            var stringResponseAsSpan = spinner.WriteAsSpan();

            // Assert
            Assert.Equal(50, stringResponseAsSpan.Length);
            Assert.Equal(expected.ToString(), stringResponseAsSpan.ToString());
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthSpanThatWasMapped()
        {
            // Arrange
            var nothing = new Nothing("spinner", "www.spinner.com.br");
            var spinner = new Spinner<Nothing>(nothing);
            var expected = new ReadOnlySpan<char>("             spinner            www.spinner.com.br".ToCharArray());

            // Act
            var conf = spinner.GetConfigurationProperty;
            var stringResponseAsSpan = spinner.WriteAsSpan();
                       
            // Assert
            Assert.Equal(conf.Lenght, stringResponseAsSpan.Length);
            Assert.Equal(expected.ToString(), stringResponseAsSpan.ToString());
        }

    }
}
