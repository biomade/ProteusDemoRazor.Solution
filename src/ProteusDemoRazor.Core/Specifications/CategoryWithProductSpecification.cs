using Proteus.Core.Entities;
using Proteus.Core.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Core.Specifications
{
    public sealed class CategoryWithProductsSpecification : BaseSpecification<Category>
    {
        public CategoryWithProductsSpecification(int categoryId)
            : base(b => b.Id == categoryId)
        {
            AddInclude(b => b.Products);
        }
    }
}
