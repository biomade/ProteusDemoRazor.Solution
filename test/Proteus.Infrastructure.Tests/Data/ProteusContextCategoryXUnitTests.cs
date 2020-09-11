using Proteus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Proteus.Infrastructure.Tests.Data
{
    
    public class ProteusContextCategoryXUnitTests : BaseEFProteusTestFixture
    {

        [Trait("Infrastructure", "Data")]
        [Fact]
        public void ProteusContext_GetCategoriesList()
        {
            //assembl
            var context = base.GetDBContenxt();

            //act
            var categories = context.Categories.ToList();

            //assert
            Assert.IsType<List<Category>>(categories);
        }


        [Trait("Infrastructure", "Data")]
        [Fact]
        public void ProteusContext_AddCategories()
        {
            //assemble
            var context = base.GetDBContenxt();
            //insert seed data into db
            context.Categories.Add(new Category() { CategoryName = "Phone" });
            context.Categories.Add(new Category() { CategoryName = "TV" });
            context.SaveChanges();

            //act
            var categories = context.Categories.ToList();

            //assert
            Assert.Equal(2, categories.Count);
        }

        [Trait("Infrastructure", "Data")]
        [Fact]
        public void ProteusContext_ChangeCategories()
        {
            //assemble
            var context = base.GetDBContenxt();
            //insert seed data into db
            context.Categories.Add(new Category() { CategoryName = "Phone" });
            context.Categories.Add(new Category() { CategoryName = "TV" });
            context.SaveChanges();

            //act
            var categories = context.Categories.ToList();
            categories[0].CategoryName = "Mobile Phone";
            context.SaveChanges();
            //refresh the list
            categories = context.Categories.ToList();

            //assert
            Assert.Equal("Mobile Phone", categories[0].CategoryName);
        }

        [Trait("Infrastructure", "Data")]
        [Fact]
        public void ProteusContext_DeleteCategories()
        {
            //assemble
            var context = base.GetDBContenxt();
            //insert seed data into db
            context.Categories.Add(new Category() { CategoryName = "Phone" });
            context.Categories.Add(new Category() { CategoryName = "TV" });
            context.SaveChanges();

            //act
            var categories = context.Categories.ToList();
            context.Categories.Remove(categories[0]);
            context.SaveChanges();
            //refresh list
            categories = context.Categories.ToList();

            //assert
            Assert.Single<Category>(categories);
        }

    }
}
