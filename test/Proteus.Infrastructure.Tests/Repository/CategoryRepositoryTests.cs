﻿
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Proteus.Core.Entities;
using Proteus.Core.Interfaces.Repositories;
using Proteus.Infrastructure.Data;
using Proteus.Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Proteus.Infrastructure.Tests.Repository
{
    [TestFixture]//denotes a class that contains unit tests.
    public class CategoryRepositoryTests
    {
        private ICategoryRepository _sutCategory; //system under test
        private ProteusContext _dbContext;
        [SetUp]
        public void Setup()
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
            _dbContext = new ProteusContext(options);
        }

        [Test]
        public void GetAllCategoriesTest()
        {
            _sutCategory = new CategoryRepository(_dbContext);

            //Act
            var categoryList = _sutCategory.GetAll();

            //Assert
            Assert.AreEqual(expected: 2, actual: categoryList.Count);
        }

    }
}
