using Proteus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Proteus.Infrastructure.Tests.Data
{
    
    public class ProteusContextProductXUnitTests : BaseEFProteusRepoTestFixture
    {
       

        [Trait("Infrastructure", "Data")]
        [Fact]
        public void ProteusContext_GetProductsList()
        {
            //assembl
            var context = base.GetDBContenxt();

            //act
            var categories = context.Products.ToList();

            //assert
            Assert.IsType<List<Product>>(categories);
        }

        [Trait("Infrastructure", "Data")]
        [Fact]
        public void ProteusContext_AddProducts()
        {
            //assemble
            var context = base.GetDBContenxt();
            //insert seed data into db
            context.Products.Add(new Product() { ProductName = "IPhone", CategoryId = 1, UnitPrice = 19.55M, UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 2, Discontinued = false });
            context.Products.Add(new Product() { ProductName = "Samsung", CategoryId = 1, UnitPrice = 33.66M, UnitsInStock = 20, QuantityPerUnit = "4", UnitsOnOrder = 4, ReorderLevel = 8, Discontinued = false });
            context.Products.Add(new Product() { ProductName = "LG TV", CategoryId = 2, UnitPrice = 68.35M, UnitsInStock = 30, QuantityPerUnit = "6", UnitsOnOrder = 8, ReorderLevel = 12, Discontinued = false });
            context.SaveChanges();

            //act
            var products = context.Products.ToList();

            //assert
            Assert.Equal(3, products.Count);
        }

        [Trait("Infrastructure", "Data")]
        [Fact]
        public void ProteusContext_ChangeProducts()
        {
            //assemble
            var context = base.GetDBContenxt();
            //insert seed data into db
            context.Products.Add(new Product() { ProductName = "IPhone", CategoryId = 1, UnitPrice = 19.55M, UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 2, Discontinued = false });
            context.Products.Add(new Product() { ProductName = "Samsung", CategoryId = 1, UnitPrice = 33.66M, UnitsInStock = 20, QuantityPerUnit = "4", UnitsOnOrder = 4, ReorderLevel = 8, Discontinued = false });
            context.Products.Add(new Product() { ProductName = "LG TV", CategoryId = 2, UnitPrice = 68.35M, UnitsInStock = 30, QuantityPerUnit = "6", UnitsOnOrder = 8, ReorderLevel = 12, Discontinued = false });
            context.SaveChanges();

            //act
            var products = context.Products.ToList();
            products[0].ProductName = "Apple IPhone 10";
            context.SaveChanges();
            //refresh the list
            products = context.Products.ToList();

            //assert
            Assert.Equal("Apple IPhone 10", products[0].ProductName);
        }
        
        [Trait("Infrastructure", "Data")]
        [Fact]
        public void ProteusContext_DeleteProducts()
        {
            //assemble
            var context = base.GetDBContenxt();
            //insert seed data into db
            context.Products.Add(new Product() { ProductName = "IPhone", CategoryId = 1, UnitPrice = 19.55M, UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 2, Discontinued = false });
            context.Products.Add(new Product() { ProductName = "Samsung", CategoryId = 1, UnitPrice = 33.66M, UnitsInStock = 20, QuantityPerUnit = "4", UnitsOnOrder = 4, ReorderLevel = 8, Discontinued = false });
            context.Products.Add(new Product() { ProductName = "LG TV", CategoryId = 2, UnitPrice = 68.35M, UnitsInStock = 30, QuantityPerUnit = "6", UnitsOnOrder = 8, ReorderLevel = 12, Discontinued = false });
            context.SaveChanges();

            //act
            var products = context.Products.ToList();
            context.Products.Remove(products[0]);
            context.SaveChanges();
            //refresh list
            products = context.Products.ToList();

            //assert
            Assert.Equal(2, products.Count);
        }
    }
}
