using Spinner.Attribute;
using Spinner.Exceptions;
using Spinner.Test.Helper.Models;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Spinner.Test
{
    public partial class SpinnerWriteTest
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
            Assert.Equal("Type Spinner.Test.Helper.Models.NothingNoAttribute does not have properties mapped for writing.", ex.Message);
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

        [Fact]
        public void WriteAsString_WhenPropertyHasNoSetter_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var model = new ModelWithReadOnlyPropertyForWrite();

            // Act & Assert
            var exception = Assert.Throws<TypeInitializationException>(() =>
            {
                var spinner = new Spinner<ModelWithReadOnlyPropertyForWrite>();
                spinner.WriteAsString(model);
            });

            Assert.NotNull(exception.InnerException);
            Assert.IsType<InvalidOperationException>(exception.InnerException);
            Assert.Contains("does not have a setter", exception.InnerException.Message);
            Assert.Contains("Value", exception.InnerException.Message);
        }

        [Fact]
        public void WriteAsString_ShouldUseSmallCapacityForSmallObjectMapper()
        {
            // Arrange
            var spinner = new Spinner<ModelWithSmall50Length>();
            var model = new ModelWithSmall50Length { Data = "X" };

            // Act
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.Equal(50, result.Length);

            var spinnerType = typeof(Spinner<ModelWithSmall50Length>);
            var builderField = spinnerType.GetField("builder", BindingFlags.NonPublic | BindingFlags.Static);
            var stringBuilder = builderField?.GetValue(null) as StringBuilder;

            Assert.NotNull(stringBuilder);

            Assert.True(stringBuilder.Capacity < 256 || stringBuilder.Capacity == 256,
                $"StringBuilder capacity should be close to 50 for ObjectMapper(50), but got {stringBuilder.Capacity}. " +
                $"If capacity is always 256, the code is using hardcoded value instead of ObjectMapper.Length");

            if (stringBuilder.Capacity == 256 && result.Length == 50)
            {
                Assert.Fail("StringBuilder initialized with capacity 256 for ObjectMapper(50). " +
                    "This suggests the code is using 'new StringBuilder(256)' instead of " +
                    "'new StringBuilder(_objectMapperAttribute?.Length ?? 256)'");
            }
        }
    }
}