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
        public void WriteAsString_ShouldReuseThreadStaticStringBuilder_WhenCalledMultipleTimes()
        {
            // Arrange
            NothingPadLeft nothing = new NothingPadLeft("spinner", "www.spinner.com.br");
            var spinner = new Spinner<NothingPadLeft>();

            // Act
            var result1 = spinner.WriteAsString(nothing);
            var result2 = spinner.WriteAsString(nothing);

            // Assert
            Assert.Equal(result1, result2);
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

        [Fact]
        public void WriteAsString_WhenObjectMapperAttributeIsNotNull_ShouldReturnStringWithDefinedLength()
        {
            // Arrange
            NothingPadLeft model = new NothingPadLeft("Test", "Site");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>();

            // Act
            ObjectMapperAttribute objectMapper = spinner.GetObjectMapper;
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.NotNull(objectMapper);
            Assert.Equal(50, objectMapper.Length);
            Assert.Equal(50, result.Length);
            Assert.Equal("                Test                          Site", result);
        }

        [Fact]
        public void WriteAsString_WhenObjectMapperAttributeIsNull_ShouldReturnStringWithoutLengthRestriction()
        {
            // Arrange
            NothingLeftNoObjectMapper model = new NothingLeftNoObjectMapper("Test", "Site");
            Spinner<NothingLeftNoObjectMapper> spinner = new Spinner<NothingLeftNoObjectMapper>();

            // Act
            ObjectMapperAttribute objectMapper = spinner.GetObjectMapper;
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.Null(objectMapper);
            Assert.Equal(50, result.Length);
            Assert.Equal("                Test                          Site", result);
        }

        [Fact]
        public void WriteAsString_WhenObjectMapperAttributeIsNotNull_ShouldTruncateToObjectMapperLength()
        {
            // Arrange
            ModelWithObjectMapperSmallerThanProperties model = new ModelWithObjectMapperSmallerThanProperties("John", "Doe");
            Spinner<ModelWithObjectMapperSmallerThanProperties> spinner = new Spinner<ModelWithObjectMapperSmallerThanProperties>();

            // Act
            ObjectMapperAttribute objectMapper = spinner.GetObjectMapper;
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.NotNull(objectMapper);
            Assert.Equal(30, objectMapper.Length);
            Assert.Equal(30, result.Length);
            Assert.Equal("                John          ", result);
        }

        [Fact]
        public void WriteAsString_WhenObjectMapperAttributeIsNull_ShouldReturnFullContentBasedOnWriteProperties()
        {
            // Arrange
            ModelWithoutObjectMapper model = new ModelWithoutObjectMapper("John", "Doe");
            Spinner<ModelWithoutObjectMapper> spinner = new Spinner<ModelWithoutObjectMapper>();

            // Act
            ObjectMapperAttribute objectMapper = spinner.GetObjectMapper;
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.Null(objectMapper);
            Assert.Equal(40, result.Length);
            Assert.Equal("                John                 Doe", result);
        }

        [Fact]
        public void WriteAsString_WhenObjectMapperLengthIsSmall_ShouldStillUseStringBuilderCapacity256()
        {
            // Arrange
            ModelWithValidLength model = new ModelWithValidLength { Name = "Test", Description = "Description" };
            Spinner<ModelWithValidLength> spinner = new Spinner<ModelWithValidLength>();

            // Act
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(50, result.Length);
            Assert.Equal(50, spinner.GetObjectMapper.Length);
        }

        [Fact]
        public void WriteAsString_WhenCalledMultipleTimes_ShouldReuseStringBuilderWith256Capacity()
        {
            // Arrange
            NothingPadLeft model1 = new NothingPadLeft("first", "test1");
            NothingPadLeft model2 = new NothingPadLeft("second", "test2");
            Spinner<NothingPadLeft> spinner = new Spinner<NothingPadLeft>();

            // Act
            string result1 = spinner.WriteAsString(model1);
            string result2 = spinner.WriteAsString(model2);
            string result3 = spinner.WriteAsString(model1);

            // Assert
            Assert.Equal(50, result1.Length);
            Assert.Equal(50, result2.Length);
            Assert.Equal(50, result3.Length);
            Assert.Equal(result1, result3);
            Assert.NotEqual(result1, result2);
        }

        [Fact]
        public void WriteAsString_WhenObjectMapperLengthIsLargerThan256_ShouldStillWorkCorrectly()
        {
            // Arrange
            ModelWithLargeLength model = new ModelWithLargeLength { Data = "Test Data" };
            Spinner<ModelWithLargeLength> spinner = new Spinner<ModelWithLargeLength>();

            // Act
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(300, result.Length);
            Assert.Equal(300, spinner.GetObjectMapper.Length);
        }
    }
}