using Proteus.Core.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Proteus.Core.Tests.Constants
{
    public class ContantXunitTest
    {
        
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

        [Trait("Core", "Constants")]
        [Fact]
        public void AuthorizationConstantsTest_Administrator()
        {
            // 1. Arrange create object 
            var adminConstant = "Administrators";           

            // 2. Act 
             var ac = AuthorizationConstants.Roles.ADMINISTRATORS;

            // 3. Assert 
            Assert.Equal(adminConstant, ac);
        }


        [Trait("Core", "Constants")]
        [Fact]
        public void AuthorizationConstantsTest_DefaultPassword()
        {
            // 1. Arrange create object 
            var pwdConstant = "Abc123!";

            // 2. Act 
            var pwd = AuthorizationConstants.DEFAULT_PASSWORD;

            // 3. Assert 
            Assert.Equal(pwdConstant, pwd);
        }
    }
}
