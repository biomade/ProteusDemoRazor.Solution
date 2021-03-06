﻿using Proteus.Core.Entities;
using Proteus.Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proteus.Core.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductListAsync();
        Task<IEnumerable<Product>> GetProductByNameAsync(string productName);
        Task<IEnumerable<Product>> GetProductByCategoryAsync(int categoryId);
    }
}
