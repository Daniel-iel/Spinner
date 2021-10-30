using System;
using Xunit;
using Spinner.Extencions;

namespace Spinner.Test.Extensions
{
    public class SpanExtensionsTest
    {

        [Fact]
        public void Should_PadLeft()
        {
            // Arrenge
            ReadOnlySpan<char> array = "Spinner".AsSpan();

            // Act
            var result = array.PadLeft(10, '*');

            //Assert
            Assert.Equal(10, result.Length);
            Assert.Equal("***Spinner", result.ToString());
        }
    }
}
