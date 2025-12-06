using Spinner.Attribute;
using Spinner.Exceptions;
using Spinner.Test.Models;
using System;
using System.Linq;
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
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>();
            const string expected = "             spinner            www.spinner.com.br";

            // Act
            string positionalString = spinner.WriteAsString(nothing);

            // Assert
            Assert.Equal(50, positionalString.Length);
            Assert.Equal(expected, positionalString);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldReturnObjectMappedAsStringInOrderOfConfigurationProperty()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>();
            const string expected = "            www.spinner.com.br             spinner";

            // Act
            string positionalString = spinner.WriteAsString(nothing);

            // Assert
            Assert.Equal(50, positionalString.Length);
            Assert.NotEqual(expected, positionalString);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthStringThatWasMappedWithPadLeft()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("             spinner", "             www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>();
            const string expected = "             spinner             www.spinner.com.b";

            // Act
            ObjectMapperAttribute conf = spinner.GetObjectMapper;
            string positionalString = spinner.WriteAsString(nothing);

            // Assert
            Assert.Equal(conf.Length, positionalString.Length);
            Assert.Equal(expected, positionalString);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfNoObjectMapperIsUsedWithPadLeft()
        {
            // Arrange
            NothingLeftNoObjectMapper nothing = new NothingLeftNoObjectMapper("             spinner", "            www.spinner.com.br");
            Spinner<NothingLeftNoObjectMapper> spinner = new Spinner<NothingLeftNoObjectMapper>();
            const string expected = "             spinner            www.spinner.com.br";

            // Act
            ObjectMapperAttribute conf = spinner.GetObjectMapper;
            string positionalString = spinner.WriteAsString(nothing);

            // Assert
            Assert.Null(conf);
            Assert.Equal(expected, positionalString);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldReturnObjectMappedAsSpanWithPadLeft()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>();
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("             spinner            www.spinner.com.br".ToCharArray());

            // Act
            ReadOnlySpan<char> positionalString = spinner.WriteAsSpan(nothing);

            // Assert
            Assert.Equal(50, positionalString.Length);
            Assert.Equal(expected.ToString(), positionalString.ToString());
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthSpanThatWasMappedWithPadLeft()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>();
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("             spinner            www.spinner.com.br".ToCharArray());

            // Act
            ObjectMapperAttribute objectMapper = spinner.GetObjectMapper;
            ReadOnlySpan<char> positionalString = spinner.WriteAsSpan(nothing);

            // Assert
            Assert.Equal(objectMapper.Length, positionalString.Length);
            Assert.Equal(expected.ToString(), positionalString.ToString());
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfNoObjectMapperIsUsedWithPadLeft()
        {
            // Arrange
            NothingLeftNoObjectMapper nothing = new NothingLeftNoObjectMapper("             spinner", "            www.spinner.com.br");
            Spinner<NothingLeftNoObjectMapper> spinner = new Spinner<NothingLeftNoObjectMapper>();
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("             spinner            www.spinner.com.br".ToCharArray());

            // Act
            ObjectMapperAttribute objectMapper = spinner.GetObjectMapper;
            ReadOnlySpan<char> positionalString = spinner.WriteAsSpan(nothing);

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

            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>();

            // Act
            string positionalStringFirst = spinner.WriteAsString(nothingFirst);
            string positionalStringSecond = spinner.WriteAsString(nothingSecond);

            // Assert
            Assert.True(positionalStringFirst != positionalStringSecond);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfTwoResponsesAreDifferent_WithPadLeft()
        {
            // Arrange
            NothingPadLeft nothingFirst = new NothingPadLeft("spinnerFirst", "www.spinner.com.br");
            NothingPadLeft nothingSecond = new NothingPadLeft("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>();

            // Act
            ReadOnlySpan<char> positionalStringFirst = spinner.WriteAsSpan(nothingFirst);
            ReadOnlySpan<char> positionalStringSecond = spinner.WriteAsSpan(nothingSecond);

            // Assert
            Assert.True(positionalStringFirst != positionalStringSecond);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldReturnObjectMappedAsStringWithPadRight()
        {
            // Arrange
            NothingPadRight nothing = new NothingPadRight("spinner", "www.spinner.com.br");
            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>();
            const string expected = "spinner             www.spinner.com.br            ";

            // Act
            string positionalString = spinner.WriteAsString(nothing);

            // Assert
            Assert.Equal(50, positionalString.Length);
            Assert.Equal(expected, positionalString);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthStringThatWasMappedWithPadRight()
        {
            // Arrange
            NothingPadRight nothing = new NothingPadRight("spinner", "www.spinner.com.br");
            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>();
            const string expected = "spinner             www.spinner.com.br            ";

            // Act
            ObjectMapperAttribute objectMapper = spinner.GetObjectMapper;
            string positionalString = spinner.WriteAsString(nothing);

            // Assert
            Assert.Equal(objectMapper.Length, positionalString.Length);
            Assert.Equal(expected, positionalString);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfTwoResponsesAreDifferentPadRight()
        {
            // Arrange
            NothingPadRight nothing = new NothingPadRight("spinner", "www.spinner.com.br");
            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>();
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("spinner             www.spinner.com.br            ".ToCharArray());

            // Act
            ReadOnlySpan<char> positionalString = spinner.WriteAsSpan(nothing);

            // Assert
            Assert.Equal(50, positionalString.Length);
            Assert.Equal(expected.ToString(), positionalString.ToString());
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfConfigurationLengthIsEqualToLengthSpanThatWasMappedWithPadRight()
        {
            // Arrange
            NothingPadRight nothing = new NothingPadRight("spinner", "www.spinner.com.br");
            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>();
            ReadOnlySpan<char> expected = new ReadOnlySpan<char>("spinner             www.spinner.com.br            ".ToCharArray());

            // Act
            ObjectMapperAttribute objectMapper = spinner.GetObjectMapper;
            ReadOnlySpan<char> positionalString = spinner.WriteAsSpan(nothing);

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

            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>();

            // Act
            string positionalStringFirst = spinner.WriteAsString(nothingFirst);
            string positionalStringSecond = spinner.WriteAsString(nothingSecond);

            // Assert
            Assert.True(positionalStringFirst != positionalStringSecond);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldValidateIfTwoResponsesAreDifferentWithPadRight()
        {
            // Arrange
            NothingPadRight nothingFirst = new NothingPadRight("spinnerFirst", "www.spinner.com.br");
            NothingPadRight nothingSecond = new NothingPadRight("spinnerSecond", "www.spinner.com.br");

            Spinner<NothingPadRight> spinner = new Spinner<NothingPadRight>();

            // Act
            ReadOnlySpan<char> positionalStringFirst = spinner.WriteAsSpan(nothingFirst);
            ReadOnlySpan<char> positionalStringSecond = spinner.WriteAsSpan(nothingSecond);

            // Assert
            Assert.True(positionalStringFirst != positionalStringSecond);
        }

        [Fact]
        public void GetWriteProperties_WhenCaller_ShouldValidateHowManyPropertiesWasMapped()
        {
            // Act
            var properties = typeof(NothingPadRight).GetProperties()
                .Where(p => p.GetCustomAttributes(typeof(WritePropertyAttribute), false).Length > 0)
                .ToArray();

            // Assert
            Assert.Equal(2, properties.Length);
            Assert.Equal("Name", properties[0].Name);
            Assert.Equal("WebSite", properties[1].Name);
        }

        [Fact]
        public void WriteAsString_WhenCalled_ShouldThrowExceptionIfNotExistsAnyPropertiesWithWritePropertyAttribute()
        {
            // Act
            Action act = () =>
            {
                // Arrange
                NothingNoAttribute nothing = new NothingNoAttribute("spinnerFirst", "www.spinner.com.br");

                Spinner<NothingNoAttribute> spinner = new Spinner<NothingNoAttribute>();

                spinner.WriteAsString(nothing);
            };

            // Assert
            PropertyNotMappedException ex = Assert.Throws<PropertyNotMappedException>(act);
            Assert.Equal("Type Spinner.Test.Models.NothingNoAttribute does not have properties mapped for writing.", ex.Message);
        }

        [Fact]
        public void WriteAsSpan_WhenCalled_ShouldThrowExceptionIfNotExistsAnyPropertiesWithWritePropertyAttribute()
        {
            // Act
            Action act = () =>
            {
                // Arrange
                NothingNoAttribute nothing = new NothingNoAttribute("spinnerFirst", "www.spinner.com.br");

                Spinner<NothingNoAttribute> spinner = new Spinner<NothingNoAttribute>();

                spinner.WriteAsSpan(nothing);
            };

            // Assert
            PropertyNotMappedException ex = Assert.Throws<PropertyNotMappedException>(act);
            Assert.Equal("Type Spinner.Test.Models.NothingNoAttribute does not have properties mapped for writing.", ex.Message);
        }
    }
}