using Microsoft.Extensions.Logging;
using Moq;
using Proteus.Application.Services;
using Proteus.Core.Entities;
using Proteus.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Proteus.Application.Tests.Services
{
    public class ProductServiceTests: ProductServiceTestFixture
    {

        [Trait("Application", "ProductService")]
        [Fact]
        public void ProductSerice_CreateTest()
        {
            //setup
            var mockProductRepo = new Mock<IProductRepository>();
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            var loggerMoq = new Mock<ILogger<ProductService>>();

            //act
            var service = new ProductService(mockProductRepo.Object, mockCategoryRepo.Object, loggerMoq.Object);
            //assert
            Assert.IsType<ProductService>(service);
        }

        [Fact]
        [Trait("Application", "ProductService")]
        public async Task ProductService_GetProductListAsync()
        {
            //setup
            var mockProductRepo = new Mock<IProductRepository>();
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            var loggerMoq = new Mock<ILogger<ProductService>>();
            //add two categories to a list to pass to the service in the repo
            var products = base.GetProductData();
            mockProductRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products.ToList());

            //create the service
            var service = new ProductService(mockProductRepo.Object, mockCategoryRepo.Object, loggerMoq.Object);

            //act
            var result = await service.GetProductList();

            //assert
            Assert.IsAssignableFrom<IEnumerable<Product>>(result);
        }

        [Fact]
        [Trait("Application", "ProductService")]
        public async Task ProductService_GetProductByIdAsync()
        {
            //setup
            var mockProductRepo = new Mock<IProductRepository>();
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            var loggerMoq = new Mock<ILogger<ProductService>>();
            //add two categories to a list to pass to the service in the repo
            var products = base.GetProductData().ToList();
            //mockProductRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products.ToList());
            mockProductRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(products[0]));

            //create the service
            var service = new ProductService(mockProductRepo.Object, mockCategoryRepo.Object, loggerMoq.Object);
            var id = 1;
            //act
            var result = await service.GetProductById(id);

            //assert
            Assert.Equal(products[0], result);
        }

        [Fact]
        [Trait("Application", "ProductService")]
        public async System.Threading.Tasks.Task ProductService_GetProductByNameAsync()
        {
           
            //setup
            var mockProductRepo = new Mock<IProductRepository>();
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            var loggerMoq = new Mock<ILogger<ProductService>>();
            //add two categories to a list to pass to the service in the repo
            var products = base.GetProductData();
            mockProductRepo.Setup(x => x.GetProductByNameAsync(It.IsAny<string>())).ReturnsAsync(products);

            //create the service
            var service = new ProductService(mockProductRepo.Object, mockCategoryRepo.Object, loggerMoq.Object);
            var name = "IPhone";
            //act
            var result = await service.GetProductByName(name);

            //assert
            Assert.Equal(products, result);
        }

        [Fact]
        [Trait("Application", "ProductService")]
        public async Task ProductService_GetProductByCategory_ReturnsValid()
        {
            //setup
            var mockProductRepo = new Mock<IProductRepository>();
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            var loggerMoq = new Mock<ILogger<ProductService>>();
            //add two categories to a list to pass to the service in the repo
            var products = base.GetProductData();
            var categoryId = 1;
            IEnumerable<Product> productsFiltered = (IEnumerable<Product>)products.Where(cid => cid.CategoryId == categoryId);


            mockProductRepo.Setup(r => r.GetProductByCategory(categoryId))
                .ReturnsAsync(products.Where(r => r.CategoryId == categoryId));

            //create the service
            var service = new ProductService(mockProductRepo.Object, mockCategoryRepo.Object, loggerMoq.Object);
            
            //act
            var result = await service.GetProductByCategory(categoryId);

            //assert
            Assert.Equal(productsFiltered, result);
        }

        [Fact]
        [Trait("Application", "ProductService")]
        public async Task ProductService_GetProductByCategory_ReturnsEmpty()
        {
            //setup
            var mockProductRepo = new Mock<IProductRepository>();
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            var loggerMoq = new Mock<ILogger<ProductService>>();
            //add two categories to a list to pass to the service in the repo
            var products = base.GetProductData();
            var categoryId = 2;
            IEnumerable<Product> productsFiltered = (IEnumerable<Product>)products.Where(cid => cid.CategoryId == categoryId);


            mockProductRepo.Setup(r => r.GetProductByCategory(categoryId))
                .ReturnsAsync(products.Where(r => r.CategoryId == It.IsAny<int>()));

            //create the service
            var service = new ProductService(mockProductRepo.Object, mockCategoryRepo.Object, loggerMoq.Object);

            //act
            var result = await service.GetProductByCategory(categoryId+1);

            //assert
            Assert.Empty(result);
        }

        [Fact]
        [Trait("Application", "ProductService")]
        public async Task ProductService_Create()
        {
            //setup
            var mockProductRepo = new Mock<IProductRepository>();
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            var loggerMoq = new Mock<ILogger<ProductService>>();
            //add two categories to a list to pass to the service in the repo
            var products = base.GetProductData().ToList();
            Product product = products[0];
            mockProductRepo.Setup(r => r.AddAsync(product))
                .ReturnsAsync(product);

            //create the service
            var service = new ProductService(mockProductRepo.Object, mockCategoryRepo.Object, loggerMoq.Object);

            //act
            var result = await service.Create(products[0]);

            //assert
            Assert.Same(products[0], result);
        }

        [Fact]
        [Trait("Application", "ProductService")]
        public async Task ProductService_Update()
        {
            //setup
            var mockProductRepo = new Mock<IProductRepository>();
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            var loggerMoq = new Mock<ILogger<ProductService>>();
            //add two categories to a list to pass to the service in the repo
            var products = base.GetProductData().ToList();
            var categoryId = It.IsInRange<int>(0, 2, Moq.Range.Inclusive);
            var product = products[categoryId];

            //required since get by Id is called inthe Update function
            mockProductRepo.Setup(r => r.GetByIdAsync(product.Id)).ReturnsAsync(product);

            mockProductRepo.Setup(r => r.UpdateAsync(product)).Verifiable("Tested");
            

            //create the service
            var service = new ProductService(mockProductRepo.Object, mockCategoryRepo.Object, loggerMoq.Object);

            //act
            await service.Update(product);

            //assert it was called once
            mockProductRepo.Verify();
        }

        [Fact]
        [Trait("Application", "ProductService")]
        public void ProductService_Delete()
        {
            //using a search term
        }

        [Fact]
        [Trait("Application", "ProductService")]
        public void ProductService_GetCategoryList()
        {

        }

        [Fact]
        [Trait("Application", "ProductService")]
        public void ProductService_GetProducts()
        {
            //using a search term
        }

    }
}
