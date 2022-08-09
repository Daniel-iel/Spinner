using System;
using Xunit;
using System.Linq;
using Spinner.Test.Models;
using Spinner.Exceptions;

namespace Spinner.Test
{
    public class SpinnerWriteTest
    {
        [Fact]
        public void WriteAsString_WhenCalled_ShoudReturnObjectMappedAsStringWithPadLeft()
        {
            // Arrange
            NothingLeft nothing = new NothingLeft("spinner", "www.spinner.com.br");
            Spinner<NothingLeft> spinner = new Spinner<NothingLeft>(nothing);
            const string expected = "             spinner            www.spinner.com.br";

            // Act
            string stringResponse = spinner.WriteAsString();

            // Assert
            Assert.Equal(50, stringResponse.Length);
            Assert.Equal(expected, stringResponse);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShoudReturnObjectMappedAsStringInOrderOfConfiguratedProperty()
        {
            // Arrange
            NothingLeft nothing = new NothingLeft("spinner", "www.spinner.com.br");
            Spinner<NothingLeft> spinner = new Spinner<NothingLeft>(nothing);
            const string expected = "            www.spinner.com.br             spinner";

            // Act
            string stringResponse = spinner.WriteAsString();

            // Assert
            Assert.Equal(50, stringResponse.Length);
            Assert.NotEqual(expected, stringResponse);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthStringThatWasMappedWithPadLeft()
        {
            // Arrange
            NothingLeft nothing = new NothingLeft("             spinner", "             www.spinner.com.br");
            Spinner<NothingLeft> spinner = new Spinner<NothingLeft>(nothing);
            const string expected = "             spinner             www.spinner.com.b";

            // Act
            Attribute.ObjectMapperAttribute conf = spinner.GetObjectMapper;
            string stringResponse = spinner.WriteAsString();

            // Assert
            Assert.Equal(conf.Length, stringResponse.Length);
            Assert.Equal(expected, stringResponse);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfNoObjectMapperIsUsedWithPadLeft()
        {
            // Arrange
            NothingLeftNoObjectMapper nothing = new NothingLeftNoObjectMapper("             spinner", "            www.spinner.com.br");
            Spinner<NothingLeftNoObjectMapper> spinner = new Spinner<NothingLeftNoObjectMapper>(nothing);
            const string expected = "             spinner            www.spinner.com.br";

            // Act
            Attribute.ObjectMapperAttribute conf = spinner.GetObjectMapper;
            string stringResponse = spinner.WriteAsString();

            // Assert
            Assert.Null(conf);
            Assert.Equal(expected, stringResponse);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShoudReturnObjectMappedAsSpanWithPadLeft()
        {
            // Arrange
            NothingLeft nothing = new NothingLeft("spinner", "www.spinner.com.br");
            Spinner<NothingLeft> spinner = new Spinner<NothingLeft>(nothing);
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("             spinner            www.spinner.com.br".ToCharArray());

            // Act
            ReadOnlySpan<char> stringResponseAsSpan = spinner.WriteAsSpan();

            // Assert
            Assert.Equal(50, stringResponseAsSpan.Length);
            Assert.Equal(expected.ToString(), stringResponseAsSpan.ToString());
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthSpanThatWasMappedWithPadLeft()
        {
            // Arrange
            NothingLeft nothing = new NothingLeft("spinner", "www.spinner.com.br");
            Spinner<NothingLeft> spinner = new Spinner<NothingLeft>(nothing);
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("             spinner            www.spinner.com.br".ToCharArray());

            // Act
            Attribute.ObjectMapperAttribute conf = spinner.GetObjectMapper;
            ReadOnlySpan<char> stringResponseAsSpan = spinner.WriteAsSpan();

            // Assert
            Assert.Equal(conf.Length, stringResponseAsSpan.Length);
            Assert.Equal(expected.ToString(), stringResponseAsSpan.ToString());
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfNoObjectMapperIsUsedWithPadLeft()
        {
            // Arrange
            NothingLeftNoObjectMapper nothing = new NothingLeftNoObjectMapper("             spinner", "            www.spinner.com.br");
            Spinner<NothingLeftNoObjectMapper> spinner = new Spinner<NothingLeftNoObjectMapper>(nothing);
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("             spinner            www.spinner.com.br".ToCharArray());

            // Act
            Attribute.ObjectMapperAttribute conf = spinner.GetObjectMapper;
            ReadOnlySpan<char> stringResponseAsSpan = spinner.WriteAsSpan();

            // Assert
            Assert.Null(conf);
            Assert.Equal(expected.ToString(), stringResponseAsSpan.ToString());
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfTwoResponseIsDiferentWithPadLeft()
        {
            // Arrange
            NothingLeft nothingFirst = new NothingLeft("spinnerFirst", "www.spinner.com.br");
            NothingLeft nothingSecond = new NothingLeft("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingLeft> spinnerFirst = new Spinner<NothingLeft>(nothingFirst);
            Spinner<NothingLeft> spinnerSecond = new Spinner<NothingLeft>(nothingSecond);

            // Act
            string stringResponseFirst = spinnerFirst.WriteAsString();
            string stringResponseSecond = spinnerSecond.WriteAsString();

            // Assert
            Assert.True(stringResponseFirst != stringResponseSecond);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfTwoResponseIsDiferentWithPadLeft()
        {
            // Arrange
            NothingLeft nothingFirst = new NothingLeft("spinnerFirst", "www.spinner.com.br");
            NothingLeft nothingSecond = new NothingLeft("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingLeft> spinnerFirst = new Spinner<NothingLeft>(nothingFirst);
            Spinner<NothingLeft> spinnerSecond = new Spinner<NothingLeft>(nothingSecond);

            // Act
            ReadOnlySpan<char> stringResponseFirst = spinnerFirst.WriteAsSpan();
            ReadOnlySpan<char> stringResponseSecond = spinnerSecond.WriteAsSpan();

            // Assert
            Assert.True(stringResponseFirst != stringResponseSecond);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShoudReturnObjectMappedAsStringWithPadRight()
        {
            // Arrange
            NothingRight nothing = new NothingRight("spinner", "www.spinner.com.br");
            Spinner<NothingRight> spinner = new Spinner<NothingRight>(nothing);
            const string expected = "spinner             www.spinner.com.br            ";

            // Act
            string stringResponse = spinner.WriteAsString();

            // Assert
            Assert.Equal(50, stringResponse.Length);
            Assert.Equal(expected, stringResponse);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthStringThatWasMappedWithPadRight()
        {
            // Arrange
            NothingRight nothing = new NothingRight("spinner", "www.spinner.com.br");
            Spinner<NothingRight> spinner = new Spinner<NothingRight>(nothing);
            const string expected = "spinner             www.spinner.com.br            ";

            // Act
            Attribute.ObjectMapperAttribute conf = spinner.GetObjectMapper;
            string stringResponse = spinner.WriteAsString();

            // Assert
            Assert.Equal(conf.Length, stringResponse.Length);
            Assert.Equal(expected, stringResponse);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShoudReturnObjectMappedAsSpanWithPadRight()
        {
            // Arrange
            NothingRight nothing = new NothingRight("spinner", "www.spinner.com.br");
            Spinner<NothingRight> spinner = new Spinner<NothingRight>(nothing);
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("spinner             www.spinner.com.br            ".ToCharArray());

            // Act
            ReadOnlySpan<char> stringResponseAsSpan = spinner.WriteAsSpan();

            // Assert
            Assert.Equal(50, stringResponseAsSpan.Length);
            Assert.Equal(expected.ToString(), stringResponseAsSpan.ToString());
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthSpanThatWasMappedWithPadRight()
        {
            // Arrange
            NothingRight nothing = new NothingRight("spinner", "www.spinner.com.br");
            Spinner<NothingRight> spinner = new Spinner<NothingRight>(nothing);
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("spinner             www.spinner.com.br            ".ToCharArray());

            // Act
            Attribute.ObjectMapperAttribute conf = spinner.GetObjectMapper;
            ReadOnlySpan<char> stringResponseAsSpan = spinner.WriteAsSpan();

            // Assert
            Assert.Equal(conf.Length, stringResponseAsSpan.Length);
            Assert.Equal(expected.ToString(), stringResponseAsSpan.ToString());
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfTwoResponseIsDiferentWithPadRight()
        {
            // Arrange
            NothingRight nothingFirst = new NothingRight("spinnerFirst", "www.spinner.com.br");
            NothingRight nothingSecond = new NothingRight("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingRight> spinnerFirst = new Spinner<NothingRight>(nothingFirst);
            Spinner<NothingRight> spinnerSecond = new Spinner<NothingRight>(nothingSecond);

            // Act
            string stringResponseFirst = spinnerFirst.WriteAsString();
            string stringResponseSecond = spinnerSecond.WriteAsString();

            // Assert
            Assert.True(stringResponseFirst != stringResponseSecond);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfTwoResponseIsDiferentWithPadRight()
        {
            // Arrange
            NothingRight nothingFirst = new NothingRight("spinnerFirst", "www.spinner.com.br");
            NothingRight nothingSecond = new NothingRight("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingRight> spinnerFirst = new Spinner<NothingRight>(nothingFirst);
            Spinner<NothingRight> spinnerSecond = new Spinner<NothingRight>(nothingSecond);

            // Act
            ReadOnlySpan<char> stringResponseFirst = spinnerFirst.WriteAsSpan();
            ReadOnlySpan<char> stringResponseSecond = spinnerSecond.WriteAsSpan();

            // Assert
            Assert.True(stringResponseFirst != stringResponseSecond);
        }

        [Fact]
        public void GetWriteProperties_WhenCaller_ShouldValidadeHowManyPropertiesWasMapped()
        {
            NothingRight nothing = new NothingRight("spinnerFirst", "www.spinner.com.br");

            Spinner<NothingRight> spinnerFirst = new Spinner<NothingRight>(nothing);

            System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> props = spinnerFirst.GetWriteProperties;

            Assert.Equal(2, props.Count());
            Assert.Equal("Name", props.First().Name);
            Assert.Equal("Adress", props.Last().Name);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldNotThrowExceptionIfNotExistsAnyPropertiesWithWritePropertyAttribute()
        {
            // Act
            Action act = () =>
            {
                // Arrange
                NothingNoAttibute nothing = new NothingNoAttibute("spinnerFirst", "www.spinner.com.br");

                Spinner<NothingNoAttibute> spinnerFirst = new Spinner<NothingNoAttibute>(nothing);

                spinnerFirst.WriteAsString();
            };

            // Assert
            var ex = Assert.Throws<PropertyNotMappedException>(act);
            Assert.Equal("Property Name should have WriteProperty configured.", ex.Message);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldNotThrowExceptionIfNotExistsAnyPropertiesWithWritePropertyAttribute()
        {
            // Act
            Action act = () =>
            {
                // Arrange
                NothingNoAttibute nothing = new NothingNoAttibute("spinnerFirst", "www.spinner.com.br");

                Spinner<NothingNoAttibute> spinnerFirst = new Spinner<NothingNoAttibute>(nothing);

                spinnerFirst.WriteAsSpan();
            };

            // Assert
            var ex = Assert.Throws<PropertyNotMappedException>(act);
            Assert.Equal("Property Name should have WriteProperty configured.", ex.Message);
        }
    }
}
