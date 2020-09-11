using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Proteus.Core.Entities;
using Proteus.Application.Mapper;
using Proteus.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Proteus.Core.Interfaces.Repositories;

namespace Proteus.Application.Services
{
    //Equivalent of the Businss Logic Layer
    // TODO : add validation , authorization, logging, exception handling etc. -- cross cutting activities in here.
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Product>> GetProductList()
        {
            var productList = await _productRepository.GetProductListAsync();
            
            return productList;
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
           
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductByName(string productName)
        {
            var productList = await _productRepository.GetProductByNameAsync(productName);
            //var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductViewModel>>(productList);
            return productList;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(int categoryId)
        {
            var productList = await _productRepository.GetProductByCategory(categoryId);
            //var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductViewModel>>(productList);
            return productList;
        }

        public async Task<Product> Create(Product productModel)
        {
            await ValidateProductIfExist(productModel);

            //var mappedEntity = ObjectMapper.Mapper.Map<Product>(productModel);
            //if (mappedEntity == null)
            //    throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _productRepository.AddAsync(productModel);
            _logger.LogInformation($"Entity successfully added - AspnetRunAppService");

            //var newMappedEntity = ObjectMapper.Mapper.Map<ProductViewModel>(newEntity);
            return newEntity;
        }

        public async Task Update(Product productModel)
        {
            ValidateProductIfNotExist(productModel);

            var editProduct = await _productRepository.GetByIdAsync(productModel.Id);
            if (editProduct == null)
                throw new ApplicationException($"Entity could not be loaded.");
            //update the properties
            //editProduct.Category = productModel.Category;
            editProduct.CategoryId = productModel.CategoryId;
            editProduct.Discontinued = productModel.Discontinued;
            editProduct.ProductName = productModel.ProductName;
            editProduct.QuantityPerUnit = productModel.QuantityPerUnit;
            editProduct.ReorderLevel = productModel.ReorderLevel;
            editProduct.UnitPrice = productModel.UnitPrice;
            editProduct.UnitsInStock = productModel.UnitsInStock;
            editProduct.UnitsOnOrder = productModel.UnitsOnOrder;


            await _productRepository.UpdateAsync(editProduct);
            _logger.LogInformation($"Entity successfully updated - AspnetRunAppService");
        }

        public async Task Delete(Product productModel)
        {
            ValidateProductIfNotExist(productModel);
            var deletedProduct = await _productRepository.GetByIdAsync(productModel.Id);
            if (deletedProduct == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await _productRepository.DeleteAsync(deletedProduct);
            _logger.LogInformation($"Entity successfully deleted - AspnetRunAppService");
        }

        private async Task ValidateProductIfExist(Product productModel)
        {
            var existingEntity = await _productRepository.GetByIdAsync(productModel.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{productModel.ToString()} with this id already exists");
        }

        private void ValidateProductIfNotExist(Product productModel)
        {
            var existingEntity = _productRepository.GetByIdAsync(productModel.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{productModel.ToString()} with this id is not exists");
        }

        public async Task<IEnumerable<Category>> GetCategoryList()
        {
            var category = await _categoryRepository.GetAllAsync();
            return category;
        }

        public async Task<IEnumerable<Product>> GetProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                var list = await _productRepository.GetProductListAsync();
                //var allmapped = ObjectMapper.Mapper.Map<IEnumerable<ProductViewModel>>(list);
                return list;
            }

            var listByName = await _productRepository.GetProductByNameAsync(searchTerm);
            //var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductViewModel>>(listByName);
            return listByName;
        }
    }
}
