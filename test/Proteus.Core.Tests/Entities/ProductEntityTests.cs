using NUnit.Framework;
using Proteus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Core.Tests
{
    [TestFixture]//denotes a class that contains unit tests.
    class ProductEntityTests
    {
        //this attribute serves the dual purpose of marking a method with parameters as a test method and providing inline data to be used when invoking that method
        [TestCase(1, 1, "IPhone", "19.5M", 10, "2", 1, 1, false )]
        [TestCase(2, 1, "Samsung", "33.5M", 10, "2",  1, 1, false  )]
        [TestCase(3, 2, "LG TV", "33.5M", 10, "2", 1, 1,  false)]
        public void CreateProductTest(int productId, int categoryId, string productNname, decimal? unitPrice = null, short? unitsInStock = null, short? unitsOnOrder = null, short? reorderLevel = null, bool discontinued = false)
        {
            //assemble
            // Arrange
            var product = Product.Create(productId, categoryId, productNname, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued);
            //assert
            Assert.AreEqual(productId, product.Id);
            Assert.AreEqual(categoryId, product.CategoryId);
            Assert.AreEqual(productNname, product.QuantityPerUnit);
            Assert.AreEqual(unitPrice, product.UnitPrice);
            Assert.AreEqual(unitsInStock, product.UnitsInStock);
            Assert.AreEqual(unitsOnOrder, product.UnitsOnOrder);
            Assert.AreEqual(reorderLevel, product.ReorderLevel);
            Assert.AreEqual(discontinued, product.Discontinued);
        }
    }
}

