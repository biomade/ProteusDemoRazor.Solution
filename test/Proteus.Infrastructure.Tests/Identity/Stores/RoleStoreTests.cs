using Proteus.Core.Entities.Identity;
using Proteus.Infrastructure.Identity;
using Proteus.Infrastructure.Identity.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Proteus.Infrastructure.Tests.Identity.Stores
{
    //NOTE: NOT ALL POSSIBLE TESTS HAVE BEEN WRITTEN SINCE THIS IS A DEMO SOLUTION
    public class RoleStoreTests: BaseEFIdentityTestFixture
    {        
        private readonly IdentityDbContext _context;

        public RoleStoreTests()
        {
            _context  = base.GetDBContenxt();
        }

        [Trait("Infrastructure", "Store")]
        [Fact]
        public void RoleStore_GetStore()
        {
            //assemble
            //requires a DI of IdentityDbContext

            //act
            var result = new RoleStore(_context);

            //assert
            Assert.IsType<RoleStore>(result);
        }

        [Trait("Infrastructure", "Store")]
        [Fact]
        public async Task RoleStore_AddRolesAsync()
        {
            //assemble
            var roles = base.GetRoleList();
            var store = new RoleStore(_context);

            //act
            foreach (var r in roles)
            {
                var identityResult = await store.CreateAsync(r);
                //assert
                Assert.Equal(Microsoft.AspNetCore.Identity.IdentityResult.Success, identityResult);
            }
        }

        [Trait("Infrastructure", "Store")]
        [Fact]
        public async Task RoleStore_FindByNameAsync()
        {

            //assemble
            var roles = base.GetRoleList();
            var store = new RoleStore(_context);
            foreach (var r in roles)
            {
                var identityResult = await store.CreateAsync(r);
            }


            //act
            foreach (var r in roles)
            {
                var result = await store.FindByNameAsync(r.NormalizedName);
                // assert
                Assert.Equal(r.Id, result.Id);
                Assert.Equal(r.Name, result.Name);
                Assert.Equal(r.NormalizedName, result.NormalizedName);
            }
            

        }
    }
}
