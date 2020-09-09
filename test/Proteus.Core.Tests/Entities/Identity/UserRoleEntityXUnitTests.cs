using Proteus.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Proteus.Core.Tests.Entities.Identity
{
    public class UserRoleEntityXUnitTests
    {
        [Trait("Core", "Entity_Identity")]
        [Fact]
        public void UserRoleTest_ObjectIsValid()
        {
            //assemble
            var result = new UserRole();
            //assert it is of the correct type
            Assert.IsType<UserRole>(result);
        }
    }
}
