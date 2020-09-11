using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Proteus.Application.Tests.Exceptions
{
    public class ApplicationExceptionTests
    {
        [Fact]
        [Trait("Application", "Exception")]
        public void InfrastructureException_ObjectIsValid_1()
        {
            string businessMessage = "This is a test";
            var result = new ApplicationException(businessMessage);

            Assert.IsType<ApplicationException>(result);
        }

        [Fact]
        [Trait("Application", "Exception")]
        public void InfrastructureException_ObjectIsValid_2()
        {
            string businessMessage = "This is a test";
            Exception innerException = new Exception("Inner Exceptions");
            var result = new ApplicationException(businessMessage, innerException);

            Assert.IsType<ApplicationException>(result);
        }
    }
}
