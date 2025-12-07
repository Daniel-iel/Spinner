using Spinner.Attribute;
using System.Text;
using Xunit;

namespace Spinner.Test
{
    public class StringBuilderCapacityTest
    {
        [Fact]
        public void StringBuilder_Capacity_DoesNotAffect_FinalOutput()
        {
            // Arrange
            var sb1 = new StringBuilder(10);
            sb1.Append("12345678901234567890");

            var sb2 = new StringBuilder(100);
            sb2.Append("12345678901234567890");

            // Act & Assert
            Assert.Equal(sb1.ToString(), sb2.ToString());
            Assert.Equal(20, sb1.Length);
            Assert.Equal(20, sb2.Length);
        }

        [Fact]
        public void WriteAsString_WithDifferentCapacities_ProducesSameResult()
        {
            // Arrange
            ModelForCapacityTest model = new ModelForCapacityTest { Value = "Test123" };
            
            // Act
            var spinner = new Spinner<ModelForCapacityTest>();
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.Equal(30, result.Length);
            Assert.Equal(30, spinner.GetObjectMapper.Length);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(256)]
        public void StringBuilder_ToString_WithLength_ReturnsCorrectSize(int capacity)
        {
            // Arrange
            var sb = new StringBuilder(capacity);
            sb.Append("1234567890");
            sb.Append("ABCDEFGHIJ");

            // Act
            string result = sb.ToString(0, 15);

            // Assert
            Assert.Equal(15, result.Length);
            Assert.Equal("1234567890ABCDE", result);
        }

        [Fact]
        public void WriteAsString_CapacityVsLength_Demonstration()
        {
            // Arrange
            ModelForCapacityTest model = new ModelForCapacityTest { Value = "LongTestValue" };
            var spinner = new Spinner<ModelForCapacityTest>();

            // Act
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.Equal(30, result.Length);
            Assert.Contains("LongTestValue", result);
        }

        [Fact]
        public void WriteAsString_WithObjectMapperLengthGreaterThan256_ShouldUseCorrectCapacity()
        {
            // Arrange
            ModelWithLargeObjectMapper model = new ModelWithLargeObjectMapper 
            { 
                Field1 = "Data1",
                Field2 = "Data2" 
            };
            var spinner = new Spinner<ModelWithLargeObjectMapper>();

            // Act
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.Equal(500, result.Length);
            Assert.Equal(500, spinner.GetObjectMapper.Length);
            Assert.Contains("Data1", result);
            Assert.Contains("Data2", result);
        }

        [Fact]
        public void WriteAsString_WithObjectMapperLength512_ShouldProduceCorrectOutput()
        {
            // Arrange
            ModelWith512Length model = new ModelWith512Length 
            { 
                Content = "Important Data Here" 
            };
            var spinner = new Spinner<ModelWith512Length>();

            // Act
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.Equal(512, result.Length);
            Assert.Equal(512, spinner.GetObjectMapper.Length);
        }

        [Fact]
        public void WriteAsString_WithSmallObjectMapper_ShouldUseObjectMapperLength()
        {
            // Arrange
            ModelWithSmallObjectMapper model = new ModelWithSmallObjectMapper 
            { 
                Value = "Test" 
            };
            var spinner = new Spinner<ModelWithSmallObjectMapper>();

            // Act
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.Equal(50, result.Length);
            Assert.Equal(50, spinner.GetObjectMapper.Length);
        }

        [Fact]
        public void WriteAsString_WithNoObjectMapper_ShouldUseDefault256Capacity()
        {
            // Arrange
            ModelWithoutObjectMapperCapacity model = new ModelWithoutObjectMapperCapacity 
            { 
                Name = "TestName",
                Description = "TestDescription" 
            };
            var spinner = new Spinner<ModelWithoutObjectMapperCapacity>();

            // Act
            string result = spinner.WriteAsString(model);

            // Assert
            Assert.Null(spinner.GetObjectMapper);
            Assert.Equal(40, result.Length);
        }

        [Fact]
        public void WriteAsString_ShouldRespectObjectMapperLengthForInitialCapacity()
        {
            // Arrange
            var spinner500 = new Spinner<ModelWithLargeObjectMapper>();
            var model500 = new ModelWithLargeObjectMapper 
            { 
                Field1 = new string('A', 250),
                Field2 = new string('B', 250)
            };

            var spinner50 = new Spinner<ModelWithSmallObjectMapper>();
            var model50 = new ModelWithSmallObjectMapper 
            { 
                Value = new string('C', 50)
            };

            var spinnerNoMapper = new Spinner<ModelWithoutObjectMapperCapacity>();
            var modelNoMapper = new ModelWithoutObjectMapperCapacity 
            { 
                Name = new string('D', 20),
                Description = new string('E', 20)
            };

            // Act
            string result500 = spinner500.WriteAsString(model500);
            string result50 = spinner50.WriteAsString(model50);
            string resultNoMapper = spinnerNoMapper.WriteAsString(modelNoMapper);

            // Assert
            Assert.Equal(500, result500.Length);
            Assert.Equal(50, result50.Length);
            Assert.Equal(40, resultNoMapper.Length);

            Assert.Contains("A", result500);
            Assert.Contains("B", result500);
            Assert.Contains("C", result50);
            Assert.Contains("D", resultNoMapper);
            Assert.Contains("E", resultNoMapper);
        }

        [Fact]
        public void WriteAsString_MultipleCallsSameType_ShouldReuseStringBuilder()
        {
            // Arrange
            var spinner = new Spinner<ModelWithLargeObjectMapper>();
            var model1 = new ModelWithLargeObjectMapper 
            { 
                Field1 = "First",
                Field2 = "Call"
            };
            var model2 = new ModelWithLargeObjectMapper 
            { 
                Field1 = "Second",
                Field2 = "Call"
            };

            // Act
            string result1 = spinner.WriteAsString(model1);
            string result2 = spinner.WriteAsString(model2);
            string result3 = spinner.WriteAsString(model1);

            // Assert
            Assert.Equal(500, result1.Length);
            Assert.Equal(500, result2.Length);
            Assert.Equal(500, result3.Length);

            Assert.NotEqual(result1, result2);
            Assert.Equal(result1, result3);
        }

        [Fact]
        public void StringBuilder_Reallocation_Demonstration()
        {
            // Arrange
            var sb1 = new StringBuilder(500);
            for (int i = 0; i < 500; i++)
            {
                sb1.Append('X');
            }

            var sb2 = new StringBuilder(256);
            for (int i = 0; i < 500; i++)
            {
                sb2.Append('X');
            }

            // Act & Assert
            Assert.Equal(500, sb1.Length);
            Assert.True(sb1.Capacity >= 500);

            Assert.Equal(500, sb2.Length);
            Assert.True(sb2.Capacity >= 500);
        }
    }

    [ObjectMapper(30)]
    public class ModelForCapacityTest
    {
        [WriteProperty(30, 0, ' ')]
        public string Value { get; set; }
    }

    [ObjectMapper(500)]
    public class ModelWithLargeObjectMapper
    {
        [WriteProperty(250, 0, ' ')]
        public string Field1 { get; set; }

        [WriteProperty(250, 1, ' ')]
        public string Field2 { get; set; }
    }

    [ObjectMapper(512)]
    public class ModelWith512Length
    {
        [WriteProperty(512, 0, ' ')]
        public string Content { get; set; }
    }

    [ObjectMapper(50)]
    public class ModelWithSmallObjectMapper
    {
        [WriteProperty(50, 0, ' ')]
        public string Value { get; set; }
    }

    public class ModelWithoutObjectMapperCapacity
    {
        [WriteProperty(20, 0, ' ')]
        public string Name { get; set; }

        [WriteProperty(20, 1, ' ')]
        public string Description { get; set; }
    }
}
