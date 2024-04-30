using Spinner.Attribute;
using Spinner.Exceptions;
using Spinner.Test.Models;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Spinner.Test
{
    public class SpinnerWriteTest
    {
        [Fact]
        public void WriteAsString_WhenCalled_ShouldReturnObjectMappedAsStringWithPadLeft()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>(nothing);
            const string expected = "             spinner            www.spinner.com.br";

            // Act
            string positionalString = spinner.WriteAsString();

            // Assert
            Assert.Equal(50, positionalString.Length);
            Assert.Equal(expected, positionalString);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldReturnObjectMappedAsStringInOrderOfConfigurationProperty()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>(nothing);
            const string expected = "            www.spinner.com.br             spinner";

            // Act
            string positionalString = spinner.WriteAsString();

            // Assert
            Assert.Equal(50, positionalString.Length);
            Assert.NotEqual(expected, positionalString);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthStringThatWasMappedWithPadLeft()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("             spinner", "             www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>(nothing);
            const string expected = "             spinner             www.spinner.com.b";

            // Act
            ObjectMapperAttribute conf = spinner.GetObjectMapper;
            string positionalString = spinner.WriteAsString();

            // Assert
            Assert.Equal(conf.Length, positionalString.Length);
            Assert.Equal(expected, positionalString);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfNoObjectMapperIsUsedWithPadLeft()
        {
            // Arrange
            NothingLeftNoObjectMapper nothing = new NothingLeftNoObjectMapper("             spinner", "            www.spinner.com.br");
            Spinner<NothingLeftNoObjectMapper> spinner = new Spinner<NothingLeftNoObjectMapper>(nothing);
            const string expected = "             spinner            www.spinner.com.br";

            // Act
            ObjectMapperAttribute conf = spinner.GetObjectMapper;
            string positionalString = spinner.WriteAsString();

            // Assert
            Assert.Null(conf);
            Assert.Equal(expected, positionalString);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldReturnObjectMappedAsSpanWithPadLeft()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>(nothing);
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("             spinner            www.spinner.com.br".ToCharArray());

            // Act
            ReadOnlySpan<char> positionalString = spinner.WriteAsSpan();

            // Assert
            Assert.Equal(50, positionalString.Length);
            Assert.Equal(expected.ToString(), positionalString.ToString());
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthSpanThatWasMappedWithPadLeft()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>(nothing);
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("             spinner            www.spinner.com.br".ToCharArray());

            // Act
            ObjectMapperAttribute objectMapper = spinner.GetObjectMapper;
            ReadOnlySpan<char> positionalString = spinner.WriteAsSpan();

            // Assert
            Assert.Equal(objectMapper.Length, positionalString.Length);
            Assert.Equal(expected.ToString(), positionalString.ToString());
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfNoObjectMapperIsUsedWithPadLeft()
        {
            // Arrange
            NothingLeftNoObjectMapper nothing = new NothingLeftNoObjectMapper("             spinner", "            www.spinner.com.br");
            Spinner<NothingLeftNoObjectMapper> spinner = new Spinner<NothingLeftNoObjectMapper>(nothing);
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("             spinner            www.spinner.com.br".ToCharArray());

            // Act
            ObjectMapperAttribute objectMapper = spinner.GetObjectMapper;
            ReadOnlySpan<char> positionalString = spinner.WriteAsSpan();

            // Assert
            Assert.Null(objectMapper);
            Assert.Equal(expected.ToString(), positionalString.ToString());
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfTwoResponseAreDifferentWithPadLeft()
        {
            // Arrange
            NothingPadLeft nothingFirst = new NothingPadLeft("spinnerFirst", "www.spinner.com.br");
            NothingPadLeft nothingSecond = new NothingPadLeft("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadLeft> spinnerFirst = new Spinner<NothingPadLeft>(nothingFirst);
            Spinner<NothingPadLeft> spinnerSecond = new Spinner<NothingPadLeft>(nothingSecond);

            // Act
            string positionalStringFirst = spinnerFirst.WriteAsString();
            string positionalStringSecond = spinnerSecond.WriteAsString();

            // Assert
            Assert.True(positionalStringFirst != positionalStringSecond);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfTwoResponsesAreDifferent_WithPadLeft()
        {
            // Arrange
            NothingPadLeft nothingFirst = new NothingPadLeft("spinnerFirst", "www.spinner.com.br");
            NothingPadLeft nothingSecond = new NothingPadLeft("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadLeft> spinnerFirst = new Spinner<NothingPadLeft>(nothingFirst);
            Spinner<NothingPadLeft> spinnerSecond = new Spinner<NothingPadLeft>(nothingSecond);

            // Act
            ReadOnlySpan<char> positionalStringFirst = spinnerFirst.WriteAsSpan();
            ReadOnlySpan<char> positionalStringSecond = spinnerSecond.WriteAsSpan();

            // Assert
            Assert.True(positionalStringFirst != positionalStringSecond);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldReturnObjectMappedAsStringWithPadRight()
        {
            // Arrange
            NothingPadRight nothing = new NothingPadRight("spinner", "www.spinner.com.br");
            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>(nothing);
            const string expected = "spinner             www.spinner.com.br            ";

            // Act
            string positionalString = spinner.WriteAsString();

            // Assert
            Assert.Equal(50, positionalString.Length);
            Assert.Equal(expected, positionalString);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthStringThatWasMappedWithPadRight()
        {
            // Arrange
            NothingPadRight nothing = new NothingPadRight("spinner", "www.spinner.com.br");
            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>(nothing);
            const string expected = "spinner             www.spinner.com.br            ";

            // Act
            ObjectMapperAttribute objectMapper = spinner.GetObjectMapper;
            string positionalString = spinner.WriteAsString();

            // Assert
            Assert.Equal(objectMapper.Length, positionalString.Length);
            Assert.Equal(expected, positionalString);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfTwoResponsesAreDifferentPadRight()
        {
            // Arrange
            NothingPadRight nothing = new NothingPadRight("spinner", "www.spinner.com.br");
            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>(nothing);
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("spinner             www.spinner.com.br            ".ToCharArray());

            // Act
            ReadOnlySpan<char> positionalString = spinner.WriteAsSpan();

            // Assert
            Assert.Equal(50, positionalString.Length);
            Assert.Equal(expected.ToString(), positionalString.ToString());
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthSpanThatWasMappedWithPadRight()
        {
            // Arrange
            NothingPadRight nothing = new NothingPadRight("spinner", "www.spinner.com.br");
            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>(nothing);
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("spinner             www.spinner.com.br            ".ToCharArray());

            // Act
            ObjectMapperAttribute objectMapper = spinner.GetObjectMapper;
            ReadOnlySpan<char> positionalString = spinner.WriteAsSpan();

            // Assert
            Assert.Equal(objectMapper.Length, positionalString.Length);
            Assert.Equal(expected.ToString(), positionalString.ToString());
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfTwoResponseAreDifferentWithPadRight()
        {
            // Arrange
            NothingPadRight nothingFirst = new NothingPadRight("spinnerFirst", "www.spinner.com.br");
            NothingPadRight nothingSecond = new NothingPadRight("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadRight> spinnerFirst = new Spinner<NothingPadRight>(nothingFirst);
            Spinner<NothingPadRight> spinnerSecond = new Spinner<NothingPadRight>(nothingSecond);

            // Act
            string positionalStringFirst = spinnerFirst.WriteAsString();
            string positionalStringSecond = spinnerSecond.WriteAsString();

            // Assert
            Assert.True(positionalStringFirst != positionalStringSecond);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfTwoResponsesAreDifferentWithPadRight()
        {
            // Arrange
            NothingPadRight nothingFirst = new NothingPadRight("spinnerFirst", "www.spinner.com.br");
            NothingPadRight nothingSecond = new NothingPadRight("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadRight> spinnerFirst = new Spinner<NothingPadRight>(nothingFirst);
            Spinner<NothingPadRight> spinnerSecond = new Spinner<NothingPadRight>(nothingSecond);

            // Act
            ReadOnlySpan<char> positionalStringFirst = spinnerFirst.WriteAsSpan();
            ReadOnlySpan<char> positionalStringSecond = spinnerSecond.WriteAsSpan();

            // Assert
            Assert.True(positionalStringFirst != positionalStringSecond);
        }

        [Fact]
        public void GetWriteProperties_WhenCaller_ShouldValidateHowManyPropertiesWasMapped()
        {
            // Arrange
            NothingPadRight nothing = new NothingPadRight("spinnerFirst", "www.spinner.com.br");

            Spinner<NothingPadRight> spinnerFirst = new Spinner<NothingPadRight>(nothing);

            // Act
            IImmutableList<PropertyInfo> props = spinnerFirst.GetWriteProperties;

            // Assert
            Assert.Equal(2, props.Count);
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
                NothingNoAttribute nothing = new NothingNoAttribute("spinnerFirst", "www.spinner.com.br");

                Spinner<NothingNoAttribute> spinnerFirst = new Spinner<NothingNoAttribute>(nothing);

                spinnerFirst.WriteAsString();
            };

            // Assert
            PropertyNotMappedException ex = Assert.Throws<PropertyNotMappedException>(act);
            Assert.Equal("Property Name should have WriteProperty configured.", ex.Message);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldThrowExceptionIfNotExistsAnyPropertiesWithWritePropertyAttribute()
        {
            // Act
            Action act = () =>
            {
                // Arrange
                NothingNoAttribute nothing = new NothingNoAttribute("spinnerFirst", "www.spinner.com.br");

                Spinner<NothingNoAttribute> spinnerFirst = new Spinner<NothingNoAttribute>(nothing);

                spinnerFirst.WriteAsSpan();
            };

            // Assert
            PropertyNotMappedException ex = Assert.Throws<PropertyNotMappedException>(act);
            Assert.Equal("Property Name should have WriteProperty configured.", ex.Message);
        }
    }
}