using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Proteus.Application.Interfaces;
using Proteus.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Application.Tests.Services
{
    public class AppConfigServiceTestFixture
    {
      
        private IConfigurationRoot GetConfigurationRoot()
        {
            string pathToFile = @"C:\Users\lauri\Repos\ProteusDemoRazor.Solution\src\ProteusDemoRazor.UI\";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(pathToFile)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            return configuration;
        }

        public IApplicationConfiguration GetService()
        {
            var appSettings = GetConfigurationRoot();

            var services = new ServiceCollection();
            services.AddScoped<IApplicationConfiguration, ApplicationConfiguration>(
                e => appSettings.GetSection("AppSettings")
                        .Get<ApplicationConfiguration>());

            ServiceProvider serviceProvider = services
                .Configure<IApplicationConfiguration>(appSettings)
                .BuildServiceProvider();
            return serviceProvider.GetService<IApplicationConfiguration>();
        }
    }
}
