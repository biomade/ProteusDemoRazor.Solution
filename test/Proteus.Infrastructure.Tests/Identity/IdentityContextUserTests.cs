using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Proteus.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Proteus.Infrastructure.Tests.Identity
{
    public class IdentityContextUserTests : BaseEFIdentityTestFixture
    {
        [Trait("Infrastructure", "Identity")]
        [Fact]
        public void IdentityContext_GetUserList()
        {
            //assembl
            var context = base.GetDBContenxt();

            //act
            var result = context.Users.ToList();

            //assert
            Assert.IsType<List<User>>(result);
        }


        [Trait("Infrastructure", "Identity")]
        [Fact]
        public void ProteusContext_AddCategories()
        {
            //assemble
            var context = base.GetDBContenxt();
            //insert seed data into db
            var users = base.GetUserList();
            context.Add(users[0]);
           
            context.SaveChanges();

            //act
            var result = context.Users.ToList();

            //assert
            Assert.Single<User>(result);
        }

        [Trait("Infrastructure", "Identity")]
        [Fact]
        public void ProteusContext_ChangeUser()
        {
            //assemble
            var context = base.GetDBContenxt();
            //insert seed data into db
            var users = base.GetUserList();
            foreach(var u in users)
            {
                context.Add(u);
                context.SaveChanges();            
            }

            //act
            var result = context.Users.ToList();
            //act
            result[0].FirstName = "ChangedFirstName";
            context.SaveChanges();


            //refresh the list
            result = context.Users.ToList();

            //assert
            Assert.Equal("ChangedFirstName", result[0].FirstName);
        }

        [Trait("Infrastructure", "Identity")]
        [Fact]
        public void ProteusContext_DeleteUser()
        {
            //assemble
            var context = base.GetDBContenxt();
            var users = base.GetUserList();
            foreach (var u in users)
            {
                context.Add(u);
                context.SaveChanges();
            }

            //act
            var result = context.Users.ToList();
            context.Users.Remove(result[0]);
               context.SaveChanges();
            //refresh list
            result = context.Users.ToList();

            //assert
            Assert.Single<User>(result);
        }
    }
}
