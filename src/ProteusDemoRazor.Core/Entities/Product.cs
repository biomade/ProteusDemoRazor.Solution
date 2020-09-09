using Proteus.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Core.Entities
{
    public class Product : Entity
    {
        public Product()
        {
        }

        public Product(int id)
        {
            base.Id = id;
        }

        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}
