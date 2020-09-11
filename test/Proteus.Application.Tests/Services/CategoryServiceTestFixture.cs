using Proteus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Application.Tests.Services
{
    public class CategoryServiceTestFixture
    {
        public  IEnumerable<Category> GetCategoryData()
        {
            IList<Category> categories = new List<Category>
            {
               new Category(1)
               {
                  CategoryName = "Phone",
                  Description = "All types of phones",
               },
               new Category(2)
               {
                    CategoryName = "TV",
                    Description = "Televisions"
               }
             };

            return categories;
        }
    }
}
