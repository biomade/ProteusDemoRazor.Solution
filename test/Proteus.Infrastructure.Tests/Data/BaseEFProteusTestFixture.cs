using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proteus.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Infrastructure.Tests.Data
{
    public abstract class BaseEFProteusTestFixture
    {
        protected static DbContextOptions<ProteusContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ProteusContext>();
            builder.UseInMemoryDatabase("Proteus")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected ProteusContext GetDBContenxt()
        {
            var options = CreateNewContextOptions();
            ProteusContext dbContext = new ProteusContext(options);
            return  dbContext;
        }
    }
}
