using Proteus.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace Proteus.Core.Tests.Exceptions
{
    public class CoreExceptionsXUnitTests
    {
        //put this on an internal class so it can be tested [assembly: InternalsVisibleTo("NAMESPACE OF TEST PROJECT.CLASS")]

        [Fact]
        [Trait("Core", "Exception")]
        public void CoreException_ObjectIsValid()
        {
            string businessMessage = "This is a test";
            var result = new CoreException(businessMessage);

            Assert.IsType<CoreException>(result);
        }
    }
}
