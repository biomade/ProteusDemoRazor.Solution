using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Proteus.Application.Interfaces;
using Proteus.Application.Services;
using Proteus.Core.Interfaces.Repositories;
using Proteus.Infrastructure.Data;
using Proteus.Infrastructure.Repository;
using Proteus.UI.Pages.Category;
using Proteus.Core.Entities;
using System.Collections.Generic;
using Proteus.Application.ViewModels;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Proteus.Application.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Proteus.UI.Tests.Pages.Category
{
    [TestFixture]
    public class IndexPageTest
   {
        private Mock<ICategoryService> _categoryServiceMock = new Mock<ICategoryService>();
        private IndexModel _pageModel;
       private  Mock<ILogger<IndexModel>> _loggerMock = new Mock<ILogger<IndexModel>>();

        [SetUp]
        public void Setup()
        {
            List<Proteus.Core.Entities.Category> list = new List<Proteus.Core.Entities.Category>
            {
                new Proteus.Core.Entities.Category() { CategoryName = "Phone"},
                new Proteus.Core.Entities.Category() {CategoryName = "TV"}
            };

            //create CategoryViewModelList using Automapper
            var cvmList = ObjectMapper.Mapper.Map<IEnumerable<CategoryViewModel>>(list);

            _categoryServiceMock.Setup(c => c.GetCategoryList()).Returns(Task.FromResult(cvmList));

            // Arrange
            _pageModel = new IndexModel((ICategoryService)_categoryServiceMock.Object, (ILogger<IndexModel>)_loggerMock.Object);
        }

        [Test]
        public void Index_Page_Test()
        {
            //// Arrange & Act
            var result = _pageModel.OnGetAsync();
            Assert.IsInstanceOf<IActionResult>(result);

        }
        
    }
}
/*
var mockAppDbContext = new Mock<AppDbContext>(optionsBuilder.Options);
var expectedMessages = AppDbContext.GetSeedingMessages();
mockAppDbContext.Setup(
    db => db.GetMessagesAsync()).Returns(Task.FromResult(expectedMessages));
var pageModel = new IndexModel(mockAppDbContext.Object);

// Act
await pageModel.OnGetAsync(); 
// Assert
var actualMessages = Assert.IsAssignableFrom<List<Message>>(pageModel.Messages);
Assert.Equal(
    expectedMessages.OrderBy(m => m.Id).Select(m => m.Text), 
    actualMessages.OrderBy(m => m.Id).Select(m => m.Text));

 */
