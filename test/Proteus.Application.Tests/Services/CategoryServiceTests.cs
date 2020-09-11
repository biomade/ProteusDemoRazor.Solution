using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Proteus.Application.Interfaces;
using Proteus.Application.Services;
using Proteus.Application.ViewModels;
using Proteus.Core.Entities;
using Proteus.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Proteus.Application.Tests.Services
{
    public class CategoryServiceTests:  CategoryServiceTestFixture
    {

        [Trait("Application", "CategoryService")]
        [Fact]
        public void CategorySerice_CreateTest()
        {
            //setup
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            var loggerMoq = new Mock<ILogger<CategoryService>>();
            
            //act
            var service = new CategoryService(mockCategoryRepo.Object, loggerMoq.Object);
            //assert
            Assert.IsType<CategoryService>(service);
        }

        [Fact]
        [Trait("Application", "CategoryService")]
        public async System.Threading.Tasks.Task CategoryService_GetAllTestAsync()
        {
            //setup
            var loggerMoq = new Mock<ILogger<CategoryService>>();
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            //add two categories to a list to pass to the service in the repo
            var categories = base.GetCategoryData();
            mockCategoryRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories.ToList());

            //create the service
            var service = new CategoryService(mockCategoryRepo.Object, loggerMoq.Object);

            //act
            var result = await service.GetCategoryList();

            //assert
            Assert.IsAssignableFrom<IEnumerable<Category>>(result);
        }

    }
}
