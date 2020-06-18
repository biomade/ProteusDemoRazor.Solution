using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proteus.UI.ViewModels;

namespace Proteus.UI.Interfaces
{
    public interface IProductPageService
    {
        Task<IEnumerable<ProductViewModel>> GetProducts(string productName);
        Task<ProductViewModel> GetProductById(int productId);
        Task<IEnumerable<ProductViewModel>> GetProductByCategory(int categoryId);
        Task<IEnumerable<CategoryViewModel>> GetCategories();
        Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel);
        Task UpdateProduct(ProductViewModel productViewModel);
        Task DeleteProduct(ProductViewModel productViewModel);
    }
}
