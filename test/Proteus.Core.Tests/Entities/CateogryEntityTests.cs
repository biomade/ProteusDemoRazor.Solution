using NUnit.Framework;
using Proteus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Core.Tests
{
    [TestFixture]//denotes a class that contains unit tests.
    class CateogryEntityTests
    {
        //this attribute serves the dual purpose of marking a method with parameters as a test method and providing inline data to be used when invoking that method
        [TestCase(1, "Phone")]  
        [TestCase(2, "TV")]
        public void CreateCategoryTest(int id, string categoryName)
        {
            //assemble
            // Arrange
            var category = Category.Create(id,categoryName, null);
            //assert
            Assert.AreEqual(id, category.Id);
            Assert.AreEqual(categoryName, category.CategoryName);

        }
    }
}
