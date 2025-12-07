using Spinner.Test.Models;
using System;
using Xunit;

namespace Spinner.Test
{
    public class InvokeTypedSetterTest
    {
        [Fact]
        public void InvokeTypedSetter_ShouldHandleStringType_Scenario1_CurrentImplementation()
        {
            // Arrange
            var spinner = new Spinner<StringModel>();
            const string input = "TestString          ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("TestString", result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleStringType_Scenario1_WithDirectString()
        {
            // Arrange
            var spinner = new Spinner<StringModel>();
            const string input = "DirectString        ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("DirectString", result.Value);
            Assert.IsType<string>(result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleStringType_Scenario1_WithObjectToString()
        {
            // Arrange
            var spinner = new Spinner<StringModelWithInterceptor>();
            const string input = "ObjectToString      ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("ObjectToString", result.Value);
            Assert.IsType<string>(result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleStringType_Scenario2_WouldBeOptimal()
        {
            // Arrange
            var spinner = new Spinner<StringModel>();
            const string input = "OptimalString       ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("OptimalString", result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleStringType_Scenario2_DirectCast()
        {
            // Arrange
            var spinner = new Spinner<StringModel>();
            const string input = "CastTest            ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("CastTest", result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleStringType_Scenario3_ToStringOnly()
        {
            // Arrange
            var spinner = new Spinner<StringModel>();
            const string input = "ToStringOnly        ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("ToStringOnly", result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleStringType_Scenario3_EmptyString()
        {
            // Arrange
            var spinner = new Spinner<StringModel>();
            const string input = "                    ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("", result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleStringType_Scenario3_SpecialCharacters()
        {
            // Arrange
            var spinner = new Spinner<StringModel>();
            const string input = "Test@#$%&*()        ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("Test@#$%&*()", result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_WithoutParser_ValueIsAlwaysString()
        {
            // Arrange
            var spinner = new Spinner<StringModel>();
            const string input = "AlwaysString        ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("AlwaysString", result.Value);
            Assert.IsType<string>(result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_WithParser_ValueMightBeObject()
        {
            // Arrange
            var spinner = new Spinner<StringModelWithInterceptor>();
            const string input = "ParsedObject        ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("ParsedObject", result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleStringType()
        {
            // Arrange
            var spinner = new Spinner<StringModel>();
            const string input = "TestString          ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("TestString", result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleStringType_WhenValueIsAlreadyString()
        {
            // Arrange
            var spinner = new Spinner<StringModel>();
            const string input = "DirectString        ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("DirectString", result.Value);
            Assert.IsType<string>(result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleStringType_WithEmptyString()
        {
            // Arrange
            var spinner = new Spinner<StringModel>();
            const string input = "                    ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("", result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleStringType_WithSpecialCharacters()
        {
            // Arrange
            var spinner = new Spinner<StringModel>();
            const string input = "Test@#$%&*()        ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("Test@#$%&*()", result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleStringType_WhenValueIsObjectWithToString()
        {
            // Arrange
            var spinner = new Spinner<StringModelWithInterceptor>();
            const string input = "ObjectToString      ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal("ObjectToString", result.Value);
            Assert.IsType<string>(result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleInt32Type()
        {
            // Arrange
            var spinner = new Spinner<Int32Model>();
            const string input = "2147483647";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(2147483647, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleInt32Type_Negative()
        {
            // Arrange
            var spinner = new Spinner<Int32Model>();
            const string input = "-214748364";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(-214748364, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleInt64Type()
        {
            // Arrange
            var spinner = new Spinner<Int64Model>();
            const string input = "9223372036854775807";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(9223372036854775807L, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleInt64Type_Negative()
        {
            // Arrange
            var spinner = new Spinner<Int64Model>();
            const string input = "-922337203685477580";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(-922337203685477580L, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleDateTimeType()
        {
            // Arrange
            var spinner = new Spinner<DateTimeModel>();
            var expected = new DateTime(2024, 12, 20, 10, 30, 45);
            const string input = "2024-12-20 10:30:45";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleBooleanType_True()
        {
            // Arrange
            var spinner = new Spinner<BooleanModel>();
            const string input = "True ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.True(result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleBooleanType_False()
        {
            // Arrange
            var spinner = new Spinner<BooleanModel>();
            const string input = "False";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.False(result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleByteType()
        {
            // Arrange
            var spinner = new Spinner<ByteModel>();
            const string input = "255";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(255, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleByteType_MinValue()
        {
            // Arrange
            var spinner = new Spinner<ByteModel>();
            const string input = "0  ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(0, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleSByteType()
        {
            // Arrange
            var spinner = new Spinner<SByteModel>();
            const string input = "-128";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(-128, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleSByteType_MaxValue()
        {
            // Arrange
            var spinner = new Spinner<SByteModel>();
            const string input = "127 ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(127, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleInt16Type()
        {
            // Arrange
            var spinner = new Spinner<Int16Model>();
            const string input = "-32768";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(-32768, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleInt16Type_MaxValue()
        {
            // Arrange
            var spinner = new Spinner<Int16Model>();
            const string input = "32767 ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(32767, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleUInt16Type()
        {
            // Arrange
            var spinner = new Spinner<UInt16Model>();
            const string input = "65535";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(65535, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleUInt16Type_MinValue()
        {
            // Arrange
            var spinner = new Spinner<UInt16Model>();
            const string input = "0    ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(0, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleUInt32Type()
        {
            // Arrange
            var spinner = new Spinner<UInt32Model>();
            const string input = "4294967295";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(4294967295U, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleUInt32Type_MinValue()
        {
            // Arrange
            var spinner = new Spinner<UInt32Model>();
            const string input = "0         ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(0U, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleUInt64Type()
        {
            // Arrange
            var spinner = new Spinner<UInt64Model>();
            const string input = "18446744073709551615";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(18446744073709551615UL, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleUInt64Type_MinValue()
        {
            // Arrange
            var spinner = new Spinner<UInt64Model>();
            const string input = "0                   ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(0UL, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleSingleType()
        {
            // Arrange
            var spinner = new Spinner<SingleModel>();
            const string input = "123,456   ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(123.456F, result.Value, 3);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleSingleType_Negative()
        {
            // Arrange
            var spinner = new Spinner<SingleModel>();
            const string input = "-99,99    ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(-99.99F, result.Value, 2);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleDoubleType()
        {
            // Arrange
            var spinner = new Spinner<DoubleModel>();
            const string input = "123,456789     ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(123.456789, result.Value, 6);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleDoubleType_Negative()
        {
            // Arrange
            var spinner = new Spinner<DoubleModel>();
            const string input = "-99,99         ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(-99.99, result.Value, 2);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleCharType()
        {
            // Arrange
            var spinner = new Spinner<CharModel>();
            const string input = "X";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal('X', result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleCharType_SpecialChar()
        {
            // Arrange
            var spinner = new Spinner<CharModel>();
            const string input = "@";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal('@', result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleTimeSpanType()
        {
            // Arrange
            var spinner = new Spinner<TimeSpanModel>();
            var expected = new TimeSpan(1, 2, 3, 4, 5);
            var input = expected.ToString().PadRight(20);

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleTimeSpanType_ShortFormat()
        {
            // Arrange
            var spinner = new Spinner<TimeSpanModel>();
            var expected = new TimeSpan(0, 1, 30, 0);
            var input = expected.ToString().PadRight(20);

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleNIntType()
        {
            // Arrange
            var spinner = new Spinner<NIntModel>();
            const string input = "2147483647";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal((nint)2147483647, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleNIntType_Negative()
        {
            // Arrange
            var spinner = new Spinner<NIntModel>();
            const string input = "-214748364";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal((nint)(-214748364), result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleNUIntType()
        {
            // Arrange
            var spinner = new Spinner<NUIntModel>();
            const string input = "4294967295";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal((nuint)4294967295, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleNUIntType_MinValue()
        {
            // Arrange
            var spinner = new Spinner<NUIntModel>();
            const string input = "0         ";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal((nuint)0, result.Value);
        }

        [Fact]
        public void InvokeTypedSetter_ShouldHandleDecimalType()
        {
            // Arrange
            var spinner = new Spinner<NothingDecimalReader>();
            const string input = "0001";

            // Act
            var result = spinner.ReadFromString(input);

            // Assert
            Assert.Equal(0.01M, result.Value);
        }
    }
}
