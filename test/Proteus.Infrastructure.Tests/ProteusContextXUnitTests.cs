using Proteus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Proteus.Infrastructure.Tests.Data
{
    public class ProteusContextXUnitTests : BaseEFProteusRepoTestFixture
    {
        [Trait("Infrastructure", "Data")]
        //[Fact]
        public void ProteusContext_GetCategoriesList()
        {
            //assembl
            var repository = base.GetDBContenxt();

            //act
            var categories = repository.Categories.ToList();

            //assert
            Assert.IsType<List<Category>>(categories);
        }
    }
}
