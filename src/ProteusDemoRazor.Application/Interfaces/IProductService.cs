
using Proteus.Application.ViewModels;
using Proteus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proteus.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductList();
        Task<Product> GetProductById(int productId);
        Task<IEnumerable<Product>> GetProductByName(string productName);
        Task<IEnumerable<Product>> GetProductByCategory(int categoryId);
        Task<Product> Create(Product productModel);
        Task Update(Product productModel);
        Task Delete(Product productModel);
        Task<IEnumerable<Category>> GetCategoryList();
        Task<IEnumerable<Product>> GetProducts(string searchTerm);
    }
}
