using System;
using Xunit;

namespace Proteus.Infrastructure.Tests
{
    public class UnitTest1
    {
        [Trait("Infrastructure", "Sample")]
        [Theory]
        [InlineData(4, 5, 9)]
        [InlineData(1, 2, 3)]
        public void SampleTests_AddNumbers(int x, int y, int expectedResult)
        {
            // 1. Arrange create object 
            var result = 0;

            // 2. Act 
            result = x + y;

            // 3. Assert 
            Assert.Equal(expectedResult, result);
        }
    }
}
