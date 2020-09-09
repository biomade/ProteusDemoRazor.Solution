using Proteus.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Proteus.Core.Tests.Entities.Identity
{
    public class UserEntityXUnitTests
    {
        [Trait("Core", "Entity_Identity")]
        [Fact]
        public void UserTest_ObjectIsValid()
        {
            //assemble
            var result = new User();
            //assert it is of the correct type
            Assert.IsType<User>(result);
        }
    }
}
