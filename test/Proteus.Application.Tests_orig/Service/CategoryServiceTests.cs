using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Proteus.Application.Interfaces;
using Proteus.Application.Mapper;
using Proteus.Application.Services;
using Proteus.Application.ViewModels;
using Proteus.Core.Entities;
using Proteus.Core.Interfaces.Repositories;
using Proteus.Infrastructure.Data;
using Proteus.Infrastructure.Repository;
using System;
using System.Collections.Generic;


namespace Proteus.Infrastructure.Tests.Service
{
    public class CategoryServiceTests
    {
        private readonly ICategoryService _sut;
        private readonly ICategoryRepository _categoryRepoMock;
        private readonly Mock<ILogger<CategoryService>> _loggerMock = new Mock<ILogger<CategoryService>>();

        public CategoryServiceTests()
        {
           
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
            _categoryRepoMock = new CategoryRepository(dbContext);

            _sut = new CategoryService(_categoryRepoMock, _loggerMock.Object);
        }
        [Test]
        public void ShouldReturnAllCategoryItemsInListAsync()
        {
            //Arrange            
            List<Category> list = new List<Category>
            {
                new Category() { CategoryName = "Phone"},
                new Category() {CategoryName = "TV"}
            };

            //create CategoryViewModelList using Automapper
            var cvmList = ObjectMapper.Mapper.Map<IEnumerable<CategoryViewModel>>(list);

            ////Act
            var actual =_sut.GetCategoryList();


            //Assert
            Assert.AreEqual(expected: cvmList, actual: actual);
        }


    }
}
