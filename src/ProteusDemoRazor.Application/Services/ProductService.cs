using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Proteus.Core.Entities;
using Proteus.Core.Interfaces;
using Proteus.Core.Repositories;
using Proteus.Application.Mapper;
using Proteus.Application.Interfaces;
using Proteus.Application.ViewModels;

namespace Proteus.Application.Services
{
    //Equivalent of the Businss Logic Layer
    // TODO : add validation , authorization, logging, exception handling etc. -- cross cutting activities in here.
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAppLogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IAppLogger<ProductService> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductList()
        {
            var productList = await _productRepository.GetProductListAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductViewModel>>(productList);
            return mapped;
        }

        public async Task<ProductViewModel> GetProductById(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            var mapped = ObjectMapper.Mapper.Map<ProductViewModel>(product);
            return mapped;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductByName(string productName)
        {
            var productList = await _productRepository.GetProductByNameAsync(productName);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductViewModel>>(productList);
            return mapped;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductByCategory(int categoryId)
        {
            var productList = await _productRepository.GetProductByCategoryAsync(categoryId);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductViewModel>>(productList);
            return mapped;
        }

        public async Task<ProductViewModel> Create(ProductViewModel productModel)
        {
            await ValidateProductIfExist(productModel);

            var mappedEntity = ObjectMapper.Mapper.Map<Product>(productModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _productRepository.AddAsync(mappedEntity);
            _logger.LogInformation($"Entity successfully added - AspnetRunAppService");

            var newMappedEntity = ObjectMapper.Mapper.Map<ProductViewModel>(newEntity);
            return newMappedEntity;
        }

        public async Task Update(ProductViewModel productModel)
        {
            ValidateProductIfNotExist(productModel);

            var editProduct = await _productRepository.GetByIdAsync(productModel.Id);
            if (editProduct == null)
                throw new ApplicationException($"Entity could not be loaded.");

            ObjectMapper.Mapper.Map<ProductViewModel, Product>(productModel, editProduct);

            await _productRepository.UpdateAsync(editProduct);
            _logger.LogInformation($"Entity successfully updated - AspnetRunAppService");
        }

        public async Task Delete(ProductViewModel productModel)
        {
            ValidateProductIfNotExist(productModel);
            var deletedProduct = await _productRepository.GetByIdAsync(productModel.Id);
            if (deletedProduct == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await _productRepository.DeleteAsync(deletedProduct);
            _logger.LogInformation($"Entity successfully deleted - AspnetRunAppService");
        }

        private async Task ValidateProductIfExist(ProductViewModel productModel)
        {
            var existingEntity = await _productRepository.GetByIdAsync(productModel.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{productModel.ToString()} with this id already exists");
        }

        private void ValidateProductIfNotExist(ProductViewModel productModel)
        {
            var existingEntity = _productRepository.GetByIdAsync(productModel.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{productModel.ToString()} with this id is not exists");
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoryList()
        {
            var category = await _categoryRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CategoryViewModel>>(category);
            return mapped;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                var list = await _productRepository.GetProductListAsync();
                var allmapped = ObjectMapper.Mapper.Map<IEnumerable<ProductViewModel>>(list);
                return allmapped;
            }

            var listByName = await _productRepository.GetProductByNameAsync(searchTerm);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductViewModel>>(listByName);
            return mapped;
        }
    }
}
