using System;
using Xunit;
using System.Linq;

namespace Spinner.Test
{
    public class SpinnerWriteTest
    {        
        [Fact]
        public void WriteAsString_WhenCalled_ShoudReturnObjectMappedAsStringWithPadLeft()
        {
            // Arrange
            var nothing = new NothingLeft("spinner", "www.spinner.com.br");
            var spinner = new Spinner<NothingLeft>(nothing);
            var expected = "             spinner            www.spinner.com.br";

            // Act
            var stringResponse = spinner.WriteAsString();

            // Assert
            Assert.Equal(50, stringResponse.Length);
            Assert.Equal(expected, stringResponse);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthStringThatWasMappedWithPadLeft()
        {
            // Arrange
            var nothing = new NothingLeft("spinner", "www.spinner.com.br");
            var spinner = new Spinner<NothingLeft>(nothing);
            var expected = "             spinner            www.spinner.com.br";

            // Act
            var conf = spinner.GetObjectMapper;
            var stringResponse = spinner.WriteAsString();

            // Assert
            Assert.Equal(conf.Lenght, stringResponse.Length);
            Assert.Equal(expected, stringResponse);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShoudReturnObjectMappedAsSpanWithPadLeft()
        {
            // Arrange
            var nothing = new NothingLeft("spinner", "www.spinner.com.br");
            var spinner = new Spinner<NothingLeft>(nothing);
            var expected = new ReadOnlySpan<char>("             spinner            www.spinner.com.br".ToCharArray());

            // Act
            var stringResponseAsSpan = spinner.WriteAsSpan();

            // Assert
            Assert.Equal(50, stringResponseAsSpan.Length);
            Assert.Equal(expected.ToString(), stringResponseAsSpan.ToString());
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthSpanThatWasMappedWithPadLeft()
        {
            // Arrange
            var nothing = new NothingLeft("spinner", "www.spinner.com.br");
            var spinner = new Spinner<NothingLeft>(nothing);
            var expected = new ReadOnlySpan<char>("             spinner            www.spinner.com.br".ToCharArray());

            // Act
            var conf = spinner.GetObjectMapper;
            var stringResponseAsSpan = spinner.WriteAsSpan();

            // Assert
            Assert.Equal(conf.Lenght, stringResponseAsSpan.Length);
            Assert.Equal(expected.ToString(), stringResponseAsSpan.ToString());
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfTwoResponseIsDiferentWithPadLeft()
        {
            // Arrange
            var nothingFirst = new NothingLeft("spinnerFirst", "www.spinner.com.br");
            var nothingSecond = new NothingLeft("spinnerSecond", "www.spinner.com.br");

            var spinnerFirst = new Spinner<NothingLeft>(nothingFirst);
            var spinnerSecond = new Spinner<NothingLeft>(nothingSecond);

            // Act            
            var stringResponseFirst = spinnerFirst.WriteAsString();
            var stringResponseSecond = spinnerSecond.WriteAsString();

            // Assert
            Assert.True(stringResponseFirst != stringResponseSecond);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShoudReturnObjectMappedAsStringWithPadRight()
        {
            // Arrange
            var nothing = new NothingRight("spinner", "www.spinner.com.br");
            var spinner = new Spinner<NothingRight>(nothing);
            var expected = "spinner             www.spinner.com.br            ";

            // Act
            var stringResponse = spinner.WriteAsString();

            // Assert
            Assert.Equal(50, stringResponse.Length);
            Assert.Equal(expected, stringResponse);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthStringThatWasMappedWithPadRight()
        {
            // Arrange
            var nothing = new NothingRight("spinner", "www.spinner.com.br");
            var spinner = new Spinner<NothingRight>(nothing);
            var expected = "spinner             www.spinner.com.br            ";

            // Act
            var conf = spinner.GetObjectMapper;
            var stringResponse = spinner.WriteAsString();

            // Assert
            Assert.Equal(conf.Lenght, stringResponse.Length);
            Assert.Equal(expected, stringResponse);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShoudReturnObjectMappedAsSpanWithPadRight()
        {
            // Arrange
            var nothing = new NothingRight("spinner", "www.spinner.com.br");
            var spinner = new Spinner<NothingRight>(nothing);
            var expected = new ReadOnlySpan<char>("spinner             www.spinner.com.br            ".ToCharArray());

            // Act
            var stringResponseAsSpan = spinner.WriteAsSpan();

            // Assert
            Assert.Equal(50, stringResponseAsSpan.Length);
            Assert.Equal(expected.ToString(), stringResponseAsSpan.ToString());
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthSpanThatWasMappedWithPadRight()
        {
            // Arrange
            var nothing = new NothingRight("spinner", "www.spinner.com.br");
            var spinner = new Spinner<NothingRight>(nothing);
            var expected = new ReadOnlySpan<char>("spinner             www.spinner.com.br            ".ToCharArray());

            // Act
            var conf = spinner.GetObjectMapper;
            var stringResponseAsSpan = spinner.WriteAsSpan();

            // Assert
            Assert.Equal(conf.Lenght, stringResponseAsSpan.Length);
            Assert.Equal(expected.ToString(), stringResponseAsSpan.ToString());
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfTwoResponseIsDiferentWithPadRight()
        {
            // Arrange
            var nothingFirst = new NothingRight("spinnerFirst", "www.spinner.com.br");
            var nothingSecond = new NothingRight("spinnerSecond", "www.spinner.com.br");

            var spinnerFirst = new Spinner<NothingRight>(nothingFirst);
            var spinnerSecond = new Spinner<NothingRight>(nothingSecond);

            // Act            
            var stringResponseFirst = spinnerFirst.WriteAsString();
            var stringResponseSecond = spinnerSecond.WriteAsString();

            // Assert
            Assert.True(stringResponseFirst != stringResponseSecond);
        }

        [Fact]
        public void GetWriteProperties_WhenCaller_ShouldValidadeHowManyPropertiesWasMapped()
        {
            var nothing = new NothingRight("spinnerFirst", "www.spinner.com.br");

            var spinnerFirst = new Spinner<NothingRight>(nothing);

            var props = spinnerFirst.GetWriteProperties;

            Assert.Equal(2, props.Count());
            Assert.Equal("Name", props.First().Name);
            Assert.Equal("Adress", props.Last().Name);
        }
    }
}
