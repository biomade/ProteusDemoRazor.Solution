using Proteus.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Proteus.Infrastructure.Tests.Exceptions
{
    public class InfrastructureExceptionsTests
    {
        //put this on an internal class, above the namespace so it can be tested => [assembly: InternalsVisibleTo("NAMESPACE OF TEST PROJECT.CLASS")]
        [Fact]
        [Trait("Infrastructure", "Exception")]
        public void InfrastructureException_ObjectIsValid_1()
        {
            string businessMessage = "This is a test";
            var result = new InfrastructureException(businessMessage);

            Assert.IsType<InfrastructureException>(result);
        }

        [Fact]
        [Trait("Infrastructure", "Exception")]
        public void InfrastructureException_ObjectIsValid_2()
        {
            string businessMessage = "This is a test";
            Exception innerException = new Exception("Inner Exceptions");
            var result = new InfrastructureException(businessMessage, innerException);

            Assert.IsType<InfrastructureException>(result);
        }
    }
}
