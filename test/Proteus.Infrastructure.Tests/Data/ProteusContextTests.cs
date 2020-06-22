using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Proteus.Core.Entities;
using Proteus.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proteus.Infrastructure.Tests.Data
{
    public class ProteusContextTests
    {
        //For EF Code, do not mock use an inmemory database, as it will validate if the db is correct
        [Test]
        public void GetAllCategoriesTest()
        {
            // Arrange - We're mocking our dbSet & dbContext
            var options = new DbContextOptionsBuilder<ProteusContext>()
                .UseInMemoryDatabase(databaseName: "Proteus")
                .Options;
            //insert seed data into db
            using (var context = new ProteusContext(options))
            {
                context.Categories.Add(new Category() { CategoryName = "Phone" });
                context.Categories.Add(new Category() { CategoryName = "TV" });
                context.SaveChanges();
            }

            // Act - fetch data
            var dbContext = new ProteusContext(options);
            List<Category> acutaldResults = dbContext.Categories.ToList();

            //Assert
            Assert.AreEqual(expected: 2, actual: acutaldResults.Count) ;
        }


        [Test]
        public void GetAllProductsTest()
        {
            // Arrange - We're mocking our dbSet & dbContext
            var options = new DbContextOptionsBuilder<ProteusContext>()
                .UseInMemoryDatabase(databaseName: "Proteus")
                .Options;
            //insert seed data into db
            using (var context = new ProteusContext(options))
            {
                context.Products.Add(new Product() { ProductName = "IPhone", CategoryId = 1, UnitPrice = 19.5M, UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 1, Discontinued = false });
                context.Products.Add(new Product() { ProductName = "Samsung", CategoryId = 1, UnitPrice = 33.5M, UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 1, Discontinued = false });
                context.Products.Add(new Product() { ProductName = "LG TV", CategoryId = 2, UnitPrice = 33.5M, UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 1, Discontinued = false });
                context.SaveChanges();
            }

            // Act - fetch data
            var dbContext = new ProteusContext(options);
            List<Product> acutaldResults = dbContext.Products.ToList();

            //Assert
            Assert.AreEqual(expected: 3, actual: acutaldResults.Count);
        }
    }
}
