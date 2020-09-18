using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using Proteus.Application.Interfaces;
using Proteus.Application.ViewModels;
using Proteus.UI.Pages.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Proteus.UI.Tests.Pages.Product
{
    public class ProductDetailsTests: ProductTestFixture
    {
        private readonly Mock<IProductService> _productService;
        private readonly Mock<ILogger<DetailsModel>> _logger;
        public ProductDetailsTests()
        {
            _productService = new Mock<IProductService>();
            _productService.Setup(p => p.GetCategoryList()).ReturnsAsync(base.GetCategoryData());
            _logger = new Mock<ILogger<DetailsModel>>();
        }


        [Fact]
        [Trait("UI", "Product")]
        public void ProductDetails_Constructor()
        {
            //assemble
            //done in the constructor

            //act
            var result = new DetailsModel(_productService.Object,_logger.Object);

            //assert
            Assert.IsType<DetailsModel>(result);
        }

        [Fact]
        [Trait("UI", "Product")]
        public async Task ProductDetails_OnGetAsync_NoIdReturnsNotFoundAsync()
        {
            //assemble
            //done in the constructor
            var pageModel = new DetailsModel(_productService.Object, _logger.Object);

            //act
            var result = await pageModel.OnGetAsync(null);

            //assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("UI", "Product")]
        public async Task ProductDetails_OnGetAsync_InvalidIdReturnsNotFoundAsync()
        {
            //assemble
            //done in the constructor
            var pageModel = new DetailsModel(_productService.Object, _logger.Object);

            //act
            var result = await pageModel.OnGetAsync(It.IsInRange(4, 100, Moq.Range.Inclusive));

            //assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("UI", "Product")]
        public async Task ProductDetails_OnGetAsync_WalidIdReturnsProductVMAsync()
        {
            //assemble
            //done in the constructor
            var pageModel = new DetailsModel(_productService.Object, _logger.Object);
            int i = 1;
            var products = base.GetProductData().ToList();
            _productService.Setup(p => p.GetProductById(i)).ReturnsAsync(products[i-1]);

            //act
            var result = await pageModel.OnGetAsync(i);
            var pVM = pageModel.Product;
            
            //assert
            Assert.IsType<ProductViewModel>(pVM);
            Assert.Equal(i, pVM.Id);
            Assert.IsType<PageResult>(result);
           
        }
    }
}
