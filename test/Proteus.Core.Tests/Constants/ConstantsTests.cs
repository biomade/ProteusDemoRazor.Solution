using Proteus.Core.Constants;
using System;
using Xunit;

namespace Proteus.Core.Tests
{
    //this test uses XUnit, not NUnit
    public class ConstantsTests
    {
        [Theory(DisplayName = "Sample Test")]
        [InlineData(4, 5, 9)]
        [InlineData(1, 2, 3)]//this should fail
        public void SampleTest(int x, int y, int expectedResult)
        {
            // 1. Arrange create object 
            var result = 0;

            // 2. Act 
            result = x + y;

            // 3. Assert 
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void AuthorizationConstantsTest()
        {
            // 1. Arrange create object 
            var adminConstant = "Administrators";
            var ac = AuthorizationConstants.Roles.ADMINISTRATORS;

            // 2. Act 
            //not required for this test

            // 3. Assert 
            Assert.Equal(adminConstant, ac);
        }
    }
}
