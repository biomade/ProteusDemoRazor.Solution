using Proteus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proteus.Infrastructure.Data
{
    public class ProteusContextSeed
    {
        public static async Task SeedAsync(ProteusContext proteusContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                // TODO: Only run this if using a real database
                // aspnetrunContext.Database.Migrate();
                // aspnetrunContext.Database.EnsureCreated();

                if (!proteusContext.Categories.Any())
                {
                    proteusContext.Categories.AddRange(GetPreconfiguredCategories());
                    await proteusContext.SaveChangesAsync();
                }

                if (!proteusContext.Products.Any())
                {
                    proteusContext.Products.AddRange(GetPreconfiguredProducts());
                    await proteusContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<ProteusContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(proteusContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }


        private static IEnumerable<Category> GetPreconfiguredCategories()
        {
            return new List<Category>()
            {
                new Category() { CategoryName = "Phone"},
                new Category() { CategoryName = "TV"}
            };
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product() { ProductName = "IPhone", CategoryId = 1 , UnitPrice = 19.5M , UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 1, Discontinued = false },
                new Product() { ProductName = "Samsung", CategoryId = 1 , UnitPrice = 33.5M , UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 1, Discontinued = false },
                new Product() { ProductName = "LG TV", CategoryId = 2 , UnitPrice = 33.5M , UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 1, Discontinued = false }
            };
        }
    }
}
