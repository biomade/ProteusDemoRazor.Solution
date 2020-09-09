using Proteus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Proteus.Core.Tests.Entities
{
    public class CategoryEntityXUnitTests
    {
        [Trait("Core", "Entity")]
        [Theory]
        [InlineData(1, "Phone", null)]
        [InlineData(2, "TV", "Types of Televisions")]
        public void CategoryTest_ObjectIsValid(int id, string categoryName, string description)
        {
            //assemble
            var result = new Category(id)
            {
                CategoryName = categoryName,
                Description = description
            };

            //assert it is of the correct type
            Assert.IsType<Category>(result);
        }

    }
}
