using Proteus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Proteus.Core.Tests.Entities
{
    public class ProductEntityXUnitTests
    {
        
        [Trait("Core", "Entity")]
        [Theory]
       
         [InlineData(1, 1,  "IPhone", 19.50, (short)1, (short)2,  (short)1, false)]
        [InlineData(2, 1, "Samsung", 33.5, (short)2, (short)3, (short)2,  false)]
        [InlineData(3, 2, "LG TV", 35.5, (short)3, (short)4, (short)3, false)]
        public void ProductTest_IsValid(int productId, int categoryId, 
                                        string productName, double? unitPrice = null, 
                                        short? unitsInStock = null, short? unitsOnOrder = null, 
                                        short? reorderLevel = null, bool discontinued = false
                                        )
        {
            //assemble
            var result = new Product(productId)
            {
                CategoryId = (int)categoryId,
                ProductName = productName,
                UnitPrice = Convert.ToDecimal(unitPrice),
                UnitsInStock = unitsInStock,
                UnitsOnOrder = unitsOnOrder,
                ReorderLevel = reorderLevel,
                Discontinued = discontinued
            };

            //assert assert it is of the correct type
            Assert.IsType<Product>(result);

        }
    }
}
