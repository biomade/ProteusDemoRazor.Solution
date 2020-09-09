using Proteus.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Core.Entities
{
    public class Category : Entity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public Category(int id)
        {
            base.Id = id;
            Products = new HashSet<Product>();
        }

        public string CategoryName { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; private set; }

    }
}
