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
    public class UserStoreTests : BaseEFIdentityTestFixture
    {        
        private readonly IdentityDbContext _context;

        public UserStoreTests()
        {
            _context  = base.GetDBContenxt();
        }

        [Trait("Infrastructure", "Store")]
        [Fact]
        public void UserStore_GetStore()
        {
            //assemble
            //requires a DI of IdentityDbContext

            //act
            var result = new UserStore(_context);

            //assert
            Assert.IsType<UserStore>(result);
        }

        [Trait("Infrastructure", "Store")]
        [Fact]
        public async Task UserStore_AddUsersAsync()
        {
            //assemble
            var users = base.GetUserList();
            var store = new UserStore(_context);

            //act
            foreach (var u in users)
            {
                var identityResult = await store.CreateAsync(u);
                //assert
                Assert.Equal(Microsoft.AspNetCore.Identity.IdentityResult.Success, identityResult);
            }
           
        }

        [Trait("Infrastructure", "Store")]
        [Fact]
        public async Task UserStore_FindByNameAsync()
        {

            //assemble
            var users = base.GetUserList();
            var store = new UserStore(_context);
            foreach (var u in users)
            {
                var identityResult = await store.CreateAsync(u);
            }


            //act
            foreach (var u in users)
            {
                var result = await store.FindByNameAsync(u.NormalizedUserName);
                // assert
                Assert.Equal(u.Id, result.Id);
                Assert.Equal(u.UserName, result.UserName);
                Assert.Equal(u.NormalizedUserName, result.NormalizedUserName);
            }


        }
    }
}
