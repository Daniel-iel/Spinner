using Spinner.Exceptions;
using Spinner.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Spinner.Test
{
    public class SpinnerWriteTest
    {
        [Fact]
        public void WriteAsString_WhenCalled_ShoudReturnObjectMappedAsStringWithPadLeft()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>(nothing);
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
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>(nothing);
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
            NothingPadLeft nothing = new NothingPadLeft("             spinner", "             www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>(nothing);
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
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>(nothing);
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
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>(nothing);
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
            NothingPadLeft nothingFirst = new NothingPadLeft("spinnerFirst", "www.spinner.com.br");
            NothingPadLeft nothingSecond = new NothingPadLeft("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadLeft> spinnerFirst = new Spinner<NothingPadLeft>(nothingFirst);
            Spinner<NothingPadLeft> spinnerSecond = new Spinner<NothingPadLeft>(nothingSecond);

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
            NothingPadLeft nothingFirst = new NothingPadLeft("spinnerFirst", "www.spinner.com.br");
            NothingPadLeft nothingSecond = new NothingPadLeft("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadLeft> spinnerFirst = new Spinner<NothingPadLeft>(nothingFirst);
            Spinner<NothingPadLeft> spinnerSecond = new Spinner<NothingPadLeft>(nothingSecond);

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
            NothingPadRight nothing = new NothingPadRight("spinner", "www.spinner.com.br");
            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>(nothing);
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
            NothingPadRight nothing = new NothingPadRight("spinner", "www.spinner.com.br");
            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>(nothing);
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
            NothingPadRight nothing = new NothingPadRight("spinner", "www.spinner.com.br");
            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>(nothing);
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
            NothingPadRight nothing = new NothingPadRight("spinner", "www.spinner.com.br");
            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>(nothing);
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
            NothingPadRight nothingFirst = new NothingPadRight("spinnerFirst", "www.spinner.com.br");
            NothingPadRight nothingSecond = new NothingPadRight("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadRight> spinnerFirst = new Spinner<NothingPadRight>(nothingFirst);
            Spinner<NothingPadRight> spinnerSecond = new Spinner<NothingPadRight>(nothingSecond);

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
            NothingPadRight nothingFirst = new NothingPadRight("spinnerFirst", "www.spinner.com.br");
            NothingPadRight nothingSecond = new NothingPadRight("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadRight> spinnerFirst = new Spinner<NothingPadRight>(nothingFirst);
            Spinner<NothingPadRight> spinnerSecond = new Spinner<NothingPadRight>(nothingSecond);

            // Act
            ReadOnlySpan<char> stringResponseFirst = spinnerFirst.WriteAsSpan();
            ReadOnlySpan<char> stringResponseSecond = spinnerSecond.WriteAsSpan();

            // Assert
            Assert.True(stringResponseFirst != stringResponseSecond);
        }

        [Fact]
        public void GetWriteProperties_WhenCaller_ShouldValidadeHowManyPropertiesWasMapped()
        {
            // Arrange
            NothingPadRight nothing = new NothingPadRight("spinnerFirst", "www.spinner.com.br");

            Spinner<NothingPadRight> spinnerFirst = new Spinner<NothingPadRight>(nothing);

            // Act
            IEnumerable<PropertyInfo> props = spinnerFirst.GetWriteProperties;

            // Assert
            Assert.Equal(2, props.Count());
            Assert.Equal("Name", props.First().Name);
            Assert.Equal("WebSite", props.Last().Name);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldThrowExceptionIfNotExistsAnyPropertiesWithWritePropertyAttribute()
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
        public void WriteAsSpan_WhenCalled_ShouldThrowExceptionIfNotExistsAnyPropertiesWithWritePropertyAttribute()
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