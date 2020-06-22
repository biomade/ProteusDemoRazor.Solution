
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Proteus.Core.Entities;
using Proteus.Core.Interfaces.Repositories;
using Proteus.Infrastructure.Data;
using Proteus.Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Proteus.Infrastructure.Tests.Repository
{
    public class CategoryRepositoryTests
    {

        private ICategoryRepository _sut; //system under test

        public CategoryRepositoryTests()
        {
            //Arrange or setup
            //create in memory db
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
            var dbContext = new ProteusContext(options);
            _sut = new CategoryRepository(dbContext);
        }

        [Test]
        public void GetAllCategoriesTest()
        {
            
            //Act
            var categoryList = _sut.GetAll();

            //Assert
            Assert.AreEqual(expected: 2, actual: categoryList.Count);
        }

        private IEnumerable<Category> GetListofCategories()
        {
            var categories = new[] {
                new Category() { CategoryName = "Phone" },
                new Category() { CategoryName = "TV" }
            };
            return categories;
        }

        private Mock<DbSet<Category>> CreateDbSetMock(IEnumerable<Category> elements)
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<Category>>();
            dbSetMock.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }

    }
}
