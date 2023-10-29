using Spinner.Internals.Extensions;
using System;
using Xunit;

namespace Spinner.Test.Extensions
{
    public class SpanExtensionsTest
    {
        [Theory]
        [InlineData("Spinner", "***Spinner", 10)]
        [InlineData("Spinner", "********Spinner", 15)]
        [InlineData("Spinner", "Spinner", 7)]
        [InlineData("", "", 0)]
        [InlineData("Spinne", "*Spinne", 7)]
        public void PadLeft_WhenTheStringLengthIsLessThanMaximumStringLength_ShouldPadLeft(string value, string expected, ushort numberOfPad)
        {
            // Arrange
            ReadOnlySpan<char> array = value.AsSpan();

            // Act
            ReadOnlySpan<char> result = array.PadLeft(numberOfPad, '*');

            // Assert
            Assert.Equal(numberOfPad, result.Length);
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("Spinner", "Spinner", 6)]
        [InlineData("Spinner", "Spinner", 5)]
        public void PadLeft_WhenTheStringIsEqualOfMaximumStringLength_ShouldNotPadLeft(string value, string expected, ushort numberOfPad)
        {
            // Arrange
            ReadOnlySpan<char> array = value.AsSpan();

            // Act
            ReadOnlySpan<char> result = array.PadLeft(numberOfPad, '*');

            // Assert
            Assert.NotEqual(numberOfPad, result.Length);
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("Spinner", "Spinner***", 10)]
        [InlineData("Spinner", "Spinner********", 15)]
        [InlineData("Spinner", "Spinner", 7)]
        public void PadRight_WhenTheStringLengthIsLessThanMaximumStringLength_PadRight(string value, string expected, ushort numberOfPad)
        {
            // Arrange
            ReadOnlySpan<char> array = value.AsSpan();

            // Act
            ReadOnlySpan<char> result = array.PadRight(numberOfPad, '*');

            // Assert
            Assert.Equal(numberOfPad, result.Length);
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("Spinner", "Spinner", 6)]
        [InlineData("Spinner", "Spinner", 5)]
        public void PadRight_WhenTheStringIsEqualOfMaximumStringLength_ShouldNotPadRight(string value, string expected, ushort numberOfPad)
        {
            // Arrange
            ReadOnlySpan<char> array = value.AsSpan();

            // Act
            ReadOnlySpan<char> result = array.PadRight(numberOfPad, '*');

            // Assert
            Assert.NotEqual(numberOfPad, result.Length);
            Assert.Equal(expected, result.ToString());
        }
    }
}