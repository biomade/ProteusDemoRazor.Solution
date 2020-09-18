
using Microsoft.Extensions.Logging;
using Moq;
using Proteus.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Proteus.UI.Pages.Product;
using Xunit;
using Proteus.Core.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proteus.Application.Mapper;
using Proteus.Application.ViewModels;
using System.Linq;

namespace Proteus.UI.Tests.Pages.Product
{
    public class ProductIndexTests: ProductTestFixture
    {
        //private readonly Mock<ICategoryService> _categoryService;
        private readonly Mock<IProductService> _productService;
        private readonly Mock<ILogger<IndexModel>> _logger;

        public ProductIndexTests()
        {
           // _categoryService = new Mock<ICategoryService>();
            _productService = new Mock<IProductService>();
            _productService.Setup(p => p.GetProducts(It.IsAny<string>())).ReturnsAsync(base.GetProductData());
            _logger = new Mock<ILogger<IndexModel>>();
        }

        [Fact]
        [Trait("UI", "Product")]
        public void ProductIndex_Constructor()
        {
            //assemble
            //done in the constructor

            //act
            //need to pass in IProductService productService, ILogger<IndexModel> logger
            var result = new IndexModel(_productService.Object, _logger.Object);

            //assert
            Assert.IsType<IndexModel>(result);
        }


        [Fact]
        [Trait("UI", "Product")]
        public async Task ProductIndex_OnGetAsync_ReturnsPageAsync()
        {
            //assemble
            //done in the constructor
            var pageModel = new IndexModel(_productService.Object, _logger.Object);
            
            //act
            //need to pass in IProductService productService, ILogger<IndexModel> logger
            var result = await pageModel.OnGetAsync();

            //assert
            Assert.IsAssignableFrom<IActionResult>(result);//itis an actionable result
                                                           
            Assert.IsType<PageResult>(result);//we can get a page object 

        }

        [Fact]
        [Trait("UI", "Product")]
        public async Task ProductIndex_OnGetAsync_LoadsProductListAsync()
        {
            //assemble
            var pageModel = new IndexModel(_productService.Object, _logger.Object);
            var products = base.GetProductData();
            var expectedCount = products.ToList().Count;
            //act
            //need to pass in IProductService productService, ILogger<IndexModel> logger
            var result = await pageModel.OnGetAsync();

            //assert
            var lst = Assert.IsAssignableFrom<List<ProductViewModel>>(pageModel.ProductList);
            Assert.Equal(expectedCount, lst.Count);
              
            //Assert.Equal(
            //    expectedMessages.OrderBy(m => m.Id).Select(m => m.Text),
            //    actualMessages.OrderBy(m => m.Id).Select(m => m.Text));
        }
    }
}
