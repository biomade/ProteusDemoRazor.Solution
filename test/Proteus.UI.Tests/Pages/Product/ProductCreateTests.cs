using Microsoft.Extensions.Logging;
using Moq;
using Proteus.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Proteus.UI.Pages.Product;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proteus.Application.ViewModels;
using System.Linq;
using AutoMapper;
using Proteus.Application.Mapper;

namespace Proteus.UI.Tests.Pages.Product
{
    public class ProductCreateTests : ProductTestFixture
    {
        private readonly Mock<IProductService> _productService;
        private readonly Mock<ILogger<CreateModel>> _logger;

        public ProductCreateTests()
        {
            _productService = new Mock<IProductService>();
            _productService.Setup(p => p.GetCategoryList()).ReturnsAsync(base.GetCategoryData());
            _logger = new Mock<ILogger<CreateModel>>();
        }


        [Fact]
        [Trait("UI", "Product")]
        public void ProductCreate_Constructor()
        {
            //assemble
            //done in the constructor

            //act
            var result = new CreateModel(_productService.Object, _logger.Object);

            //assert
            Assert.IsType<CreateModel>(result);
        }


        [Fact]
        [Trait("UI", "Product")]
        public async Task ProductCreate_OnGetAsync_ReturnsPageAsync()
        {
            //assemble
            //done in the constructor
            var pageModel = new CreateModel(_productService.Object, _logger.Object);
            pageModel.ProductVM = new ProductViewModel();

            //act
            var result = await pageModel.OnGetAsync();

            //assert
            var pageResult= Assert.IsType<PageResult>(result);
           
            var categorySelectList = Assert.IsAssignableFrom<List<SelectListItem>>(pageModel.CategoryList);
        }

        [Fact]
        [Trait("UI", "Product")]
        public async Task ProductCreate_OnGetAsync_OnPostAsyncInvalidViewModelReturnsPageAsync()
        {   //assemble
            var pageModel = new CreateModel(_productService.Object, _logger.Object);
            //for invalid Model state
            pageModel.ModelState.AddModelError("Message.Text", "The Text field is required.");
            pageModel.ProductVM = new ProductViewModel();

            //act
            var result = await pageModel.OnPostAsync();
          
            //assert
            var pageResult = Assert.IsType<PageResult>(result);      
        }

        [Fact]
        [Trait("UI", "Product")]
        public async Task ProductCreate_OnGetAsync_OnPostAsyncValidViewModelRedirectsAsync()
        {   //assemble
            var pageModel = new CreateModel(_productService.Object, _logger.Object);
            var products = base.GetProductData().ToList();
            var product = products[It.IsInRange<int>(0, 2, Moq.Range.Inclusive)];
            var productVM = ObjectMapper.Mapper.Map<ProductViewModel>(product);
            _productService.Setup(p => p.Create(product)).ReturnsAsync(product);

            pageModel.ProductVM = new ProductViewModel();

            //act
            var result = await pageModel.OnPostAsync();

            //assert redirect to index page
            Assert.IsType<RedirectToPageResult>(result);
            RedirectToPageResult redirect = result as RedirectToPageResult; //<--cast here to get the actual object
            Assert.Equal("./Index", redirect.PageName);
        }
    }
}
