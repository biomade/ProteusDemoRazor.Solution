using Proteus.Core.Entities;
using Proteus.Core.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Core.Specifications
{
    public class ProductWithCategorySpecification : BaseSpecification<Product>
    {
        public ProductWithCategorySpecification(string productName)
            : base(p => p.ProductName.ToLower().Contains(productName.ToLower()))
        {
            AddInclude(p => p.Category);
        }

        public ProductWithCategorySpecification() : base(null)
        {
            AddInclude(p => p.Category);
        }
    }
}
