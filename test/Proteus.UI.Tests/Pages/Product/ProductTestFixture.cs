using System;
using System.Collections.Generic;
using System.Text;
using Proteus.Core.Entities;

namespace Proteus.UI.Tests.Pages.Product
{
    public class ProductTestFixture
    {
        public IEnumerable<Proteus.Core.Entities.Product> GetProductData()
        {
            IEnumerable<Proteus.Core.Entities.Product> products = new List<Proteus.Core.Entities.Product>
            {
                 new Proteus.Core.Entities.Product(1) {
                     ProductName = "IPhone",
                     CategoryId = 1,
                     UnitPrice = 19.55M,
                     UnitsInStock = 10,
                     QuantityPerUnit = "2",
                     UnitsOnOrder = 1,
                     ReorderLevel = 2,
                     Discontinued = false },
                new Proteus.Core.Entities.Product(2) {
                    ProductName = "Samsung",
                    CategoryId = 1,
                    UnitPrice = 33.66M,
                    UnitsInStock = 20,
                    QuantityPerUnit = "4",
                    UnitsOnOrder = 4,
                    ReorderLevel = 8,
                    Discontinued = false },
                new Proteus.Core.Entities.Product(3) {
                    ProductName = "LG TV",
                    CategoryId = 2,
                    UnitPrice = 68.35M,
                    UnitsInStock = 30,
                    QuantityPerUnit = "6",
                    UnitsOnOrder = 8,
                    ReorderLevel = 12,
                    Discontinued = false }
        };

            return products;
        }

        public IEnumerable<Category> GetCategoryData()
        {
            IList<Category> categories = new List<Category>
            {
               new Category(1)
               {
                  CategoryName = "Phone",
                  Description = "All types of phones",
               },
               new Category(2)
               {
                    CategoryName = "TV",
                    Description = "Televisions"
               }
             };

            return categories;
        }

    }
}
