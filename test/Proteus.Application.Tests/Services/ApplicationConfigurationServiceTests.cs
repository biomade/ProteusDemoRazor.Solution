using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Proteus.Application.Interfaces;
using Proteus.Application.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Xunit;

namespace Proteus.Application.Tests.Services
{
    //NOTE: NOT ALL TESTS IMPLEMENTED AS THIS IS A DEMO SOLUTION
    public class ApplicationConfigurationServiceTests: AppConfigServiceTestFixture
    {
       private readonly IApplicationConfiguration _service;
        public ApplicationConfigurationServiceTests()
        {            
            _service = base.GetService();
        }

        [Trait("Application", "AppConfig")]
        [Fact]
        public void DefaultRole_ShouldEqualVistor()
        {
            //assemble
            string expectedValue = "Visitor";

            //Act
            string result = _service.DefaultRole;

            //assert
            Assert.Equal(expectedValue, result);
        }
    }
}
