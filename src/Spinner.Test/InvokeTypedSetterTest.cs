using Spinner.Test.Helper.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Spinner.Test
{
    public class InvokeTypedSetterTest
    {
        [Theory]
        [InlineData("TestString          ", "TestString")]
        [InlineData("DirectString        ", "DirectString")]
        [InlineData("OptimalString       ", "OptimalString")]
        [InlineData("CastTest            ", "CastTest")]
        [InlineData("ToStringOnly        ", "ToStringOnly")]
        [InlineData("                    ", "")]
        [InlineData("Test@#$%&*()        ", "Test@#$%&*()")]
        [InlineData("AlwaysString        ", "AlwaysString")]
        public void InvokeTypedSetter_ShouldHandleStringType(string input, string expected)
        {
            // Arrange
            var spinner = new Spinner<StringModel>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
            Assert.IsType<string>(result.Value);
        }

        [Theory]
        [InlineData("ObjectToString      ", "ObjectToString")]
        [InlineData("ParsedObject        ", "ParsedObject")]
        public void InvokeTypedSetter_ShouldHandleStringTypeWithInterceptor(string input, string expected)
        {
            // Arrange
            var spinner = new Spinner<StringModelWithInterceptor>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
            Assert.IsType<string>(result.Value);
        }

        [Theory]
        [InlineData("2147483647", 2147483647)]
        [InlineData("-214748364", -214748364)]
        public void InvokeTypedSetter_ShouldHandleInt32Type(string input, int expected)
        {
            // Arrange
            var spinner = new Spinner<Int32Model>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("9223372036854775807", 9223372036854775807L)]
        [InlineData("-922337203685477580", -922337203685477580L)]
        public void InvokeTypedSetter_ShouldHandleInt64Type(string input, long expected)
        {
            // Arrange
            var spinner = new Spinner<Int64Model>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        public static IEnumerable<object[]> DateTimeTestData =>
            new List<object[]>
            {
                new object[] { "2024-12-20 10:30:45", new DateTime(2024, 12, 20, 10, 30, 45) }
            };

        [Theory]
        [MemberData(nameof(DateTimeTestData))]
        public void InvokeTypedSetter_ShouldHandleDateTimeType(string input, DateTime expected)
        {
            // Arrange
            var spinner = new Spinner<DateTimeModel>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("True ", true)]
        [InlineData("False", false)]
        public void InvokeTypedSetter_ShouldHandleBooleanType(string input, bool expected)
        {
            // Arrange
            var spinner = new Spinner<BooleanModel>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("255", 255)]
        [InlineData("0  ", 0)]
        public void InvokeTypedSetter_ShouldHandleByteType(string input, byte expected)
        {
            // Arrange
            var spinner = new Spinner<ByteModel>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("-128", -128)]
        [InlineData("127 ", 127)]
        public void InvokeTypedSetter_ShouldHandleSByteType(string input, sbyte expected)
        {
            // Arrange
            var spinner = new Spinner<SByteModel>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("-32768", -32768)]
        [InlineData("32767 ", 32767)]
        public void InvokeTypedSetter_ShouldHandleInt16Type(string input, short expected)
        {
            // Arrange
            var spinner = new Spinner<Int16Model>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("65535", 65535)]
        [InlineData("0    ", 0)]
        public void InvokeTypedSetter_ShouldHandleUInt16Type(string input, ushort expected)
        {
            // Arrange
            var spinner = new Spinner<UInt16Model>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("4294967295", 4294967295U)]
        [InlineData("0         ", 0U)]
        public void InvokeTypedSetter_ShouldHandleUInt32Type(string input, uint expected)
        {
            // Arrange
            var spinner = new Spinner<UInt32Model>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("18446744073709551615", 18446744073709551615UL)]
        [InlineData("0                   ", 0UL)]
        public void InvokeTypedSetter_ShouldHandleUInt64Type(string input, ulong expected)
        {
            // Arrange
            var spinner = new Spinner<UInt64Model>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("123,456   ", 123.456F)]
        [InlineData("-99,99    ", -99.99F)]
        public void InvokeTypedSetter_ShouldHandleSingleType(string input, float expected)
        {
            // Arrange
            var spinner = new Spinner<SingleModel>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value, 3);
        }

        [Theory]
        [InlineData("123,456789     ", 123.456789)]
        [InlineData("-99,99         ", -99.99)]
        public void InvokeTypedSetter_ShouldHandleDoubleType(string input, double expected)
        {
            // Arrange
            var spinner = new Spinner<DoubleModel>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value, 6);
        }

        [Theory]
        [InlineData("X", 'X')]
        [InlineData("@", '@')]
        public void InvokeTypedSetter_ShouldHandleCharType(string input, char expected)
        {
            // Arrange
            var spinner = new Spinner<CharModel>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        public static IEnumerable<object[]> TimeSpanTestData()
        {
            var longTimeSpan = new TimeSpan(1, 2, 3, 4, 5);
            yield return new object[] { longTimeSpan.ToString().PadRight(20), longTimeSpan };

            var shortTimeSpan = new TimeSpan(0, 1, 30, 0);
            yield return new object[] { shortTimeSpan.ToString().PadRight(20), shortTimeSpan };
        }

        [Theory]
        [MemberData(nameof(TimeSpanTestData))]
        public void InvokeTypedSetter_ShouldHandleTimeSpanType(string input, TimeSpan expected)
        {
            // Arrange
            var spinner = new Spinner<TimeSpanModel>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("2147483647", 2147483647)]
        [InlineData("-214748364", -214748364)]
        public void InvokeTypedSetter_ShouldHandleNIntType(string input, int expectedValue)
        {
            // Arrange
            var spinner = new Spinner<NIntModel>();
            var expected = (nint)expectedValue;

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Theory]
        [InlineData("4294967295", 4294967295U)]
        [InlineData("0         ", 0U)]
        public void InvokeTypedSetter_ShouldHandleNUIntType(string input, uint expectedValue)
        {
            // Arrange
            var spinner = new Spinner<NUIntModel>();
            var expected = (nuint)expectedValue;

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        public static IEnumerable<object[]> DecimalTestData =>
            new List<object[]>
            {
                new object[] { "0001", 0.01M }
            };

        [Theory]
        [MemberData(nameof(DecimalTestData))]
        public void InvokeTypedSetter_ShouldHandleDecimalType(string input, decimal expected)
        {
            // Arrange
            var spinner = new Spinner<NothingDecimalReader>();

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }
    }
}
