using Spinner.Attribute;
using System;
using System.Reflection;
using System.Text;
using Xunit;

namespace Spinner.Test
{
    public class StringBuilderCapacityReflectionTest
    {
        [Fact]
        public void WriteAsString_ShouldInitializeStringBuilderWithObjectMapperLength()
        {
            // Arrange
            var spinner = new Spinner<ModelWithExactly500Length>();
            var model = new ModelWithExactly500Length { Data = "Test" };

            // Act
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.Equal(500, result.Length);

            var spinnerType = typeof(Spinner<ModelWithExactly500Length>);
            var builderField = spinnerType.GetField("builder", BindingFlags.NonPublic | BindingFlags.Static);
            
            Assert.NotNull(builderField);

            var stringBuilder = builderField.GetValue(null) as StringBuilder;
            
            Assert.NotNull(stringBuilder);
            
            Assert.True(stringBuilder.Capacity >= 500, 
                $"StringBuilder capacity ({stringBuilder.Capacity}) should be >= 500 when ObjectMapper.Length is 500. " +
                $"This indicates the initial capacity was set correctly to ObjectMapper.Length (500) instead of hardcoded 256.");
        }

        [Fact]
        public void WriteAsString_ShouldInitializeStringBuilderWith256WhenNoObjectMapper()
        {
            // Arrange
            var spinner = new Spinner<ModelWithoutObjectMapperReflection>();
            var model = new ModelWithoutObjectMapperReflection { Name = "Test", Value = "Data" };

            // Act
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.Equal(40, result.Length);

            var spinnerType = typeof(Spinner<ModelWithoutObjectMapperReflection>);
            var builderField = spinnerType.GetField("builder", BindingFlags.NonPublic | BindingFlags.Static);
            var stringBuilder = builderField?.GetValue(null) as StringBuilder;

            Assert.NotNull(stringBuilder);
            
            Assert.True(stringBuilder.Capacity >= 256,
                $"StringBuilder capacity should be >= 256 when no ObjectMapper is defined. Got: {stringBuilder.Capacity}");
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

        [Fact]
        public void WriteAsString_StringBuilderCapacity_ShouldMatchObjectMapperLength()
        {
            // Arrange
            var spinner50 = new Spinner<ModelWithSmall50Length>();
            var spinner500 = new Spinner<ModelWithExactly500Length>();
            var spinnerNoMapper = new Spinner<ModelWithoutObjectMapperReflection>();

            // Act
            spinner50.WriteAsString(new ModelWithSmall50Length { Data = "A" });
            spinner500.WriteAsString(new ModelWithExactly500Length { Data = "B" });
            spinnerNoMapper.WriteAsString(new ModelWithoutObjectMapperReflection { Name = "C", Value = "D" });

            int capacity50 = GetStringBuilderCapacity<ModelWithSmall50Length>();
            int capacity500 = GetStringBuilderCapacity<ModelWithExactly500Length>();
            int capacityNoMapper = GetStringBuilderCapacity<ModelWithoutObjectMapperReflection>();

            // Assert
            Assert.True(capacity500 >= 500, 
                $"Expected capacity >= 500 for ObjectMapper(500), but got {capacity500}");
            
            Assert.NotEqual(256, capacity50);
            
            Assert.NotEqual(capacity50, capacity500);
            Assert.NotEqual(capacity50, capacityNoMapper);
        }

        private int GetStringBuilderCapacity<T>() where T : new()
        {
            var spinnerType = typeof(Spinner<T>);
            var builderField = spinnerType.GetField("builder", BindingFlags.NonPublic | BindingFlags.Static);
            var stringBuilder = builderField?.GetValue(null) as StringBuilder;
            return stringBuilder?.Capacity ?? 0;
        }
    }

    [ObjectMapper(500)]
    public class ModelWithExactly500Length
    {
        [WriteProperty(500, 0, ' ')]
        public string Data { get; set; }
    }

    [ObjectMapper(50)]
    public class ModelWithSmall50Length
    {
        [WriteProperty(50, 0, ' ')]
        public string Data { get; set; }
    }

    public class ModelWithoutObjectMapperReflection
    {
        [WriteProperty(20, 0, ' ')]
        public string Name { get; set; }

        [WriteProperty(20, 1, ' ')]
        public string Value { get; set; }
    }
}
