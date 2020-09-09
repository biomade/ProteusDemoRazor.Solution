using Proteus.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Proteus.Core.Tests.Entities.Identity
{
    public class RoleEntityXUnitTests
    {
        [Trait("Core", "Entity_Identity")]
        [Fact]
        public void RoleTest_ObjectIsValid()
        {
            //assemble
            var result = new Role();
            //assert it is of the correct type
            Assert.IsType<Role>(result);
        }
    }
}
