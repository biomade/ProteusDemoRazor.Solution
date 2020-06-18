
using Proteus.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proteus.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetProductList();
        Task<ProductViewModel> GetProductById(int productId);
        Task<IEnumerable<ProductViewModel>> GetProductByName(string productName);
        Task<IEnumerable<ProductViewModel>> GetProductByCategory(int categoryId);
        Task<ProductViewModel> Create(ProductViewModel productModel);
        Task Update(ProductViewModel productModel);
        Task Delete(ProductViewModel productModel);
        Task<IEnumerable<CategoryViewModel>> GetCategoryList();
        Task<IEnumerable<ProductViewModel>> GetProducts(string searchTerm);
    }
}
