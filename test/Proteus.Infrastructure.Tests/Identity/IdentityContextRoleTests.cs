using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Proteus.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Proteus.Infrastructure.Tests.Identity
{
    public class IdentityContextRoleTests : BaseEFIdentityTestFixture
    {
        [Trait("Infrastructure", "Identity")]
        [Fact]
        public void IdentityContext_GetRoleList()
        {
            //assembl
            var context = base.GetDBContenxt();

            //act
            var result = context.Roles.ToList();

            //assert
            Assert.IsType<List<Role>>(result);
        }


        [Trait("Infrastructure", "Identity")]
        [Fact]
        public void ProteusContext_AddRoles()
        {
            //assemble
            var context = base.GetDBContenxt();
            //insert seed data into db
            var role = base.CreateRoles();

            context.Add(role[0]);
            context.SaveChanges();

            //act
            var result = context.Roles.ToList();

            //assert
            Assert.Single<Role>(result);
        }

        [Trait("Infrastructure", "Identity")]
        [Fact]
        public void ProteusContext_ChangeRole()
        {
            //assemble
            var context = base.GetDBContenxt();
            //seed data
            var roles = base.CreateRoles();
            foreach (var r in roles)
            {
                //if the role exists, don't add
                var found = context.Roles.Where(ro => ro.Name == r.Name).FirstOrDefault();
                if (found == null)
                {
                    context.Add(r);
                    context.SaveChanges();
                }
            }

            //act
            var result = context.Roles.ToList();
            //act
            result[0].Name= "ChangedRoleName";
            context.SaveChanges();

            //refresh the list
            result = context.Roles.ToList();

            //assert
            Assert.Equal("ChangedRoleName", result[0].Name);
        }

        [Trait("Infrastructure", "Identity")]
        [Fact]
        public void ProteusContext_DeleteRole()
        {
            //assemble
            var context = base.GetDBContenxt();
            //insert seed data into db
            var roles = base.CreateRoles();
            foreach (var r in roles)
            {
                //if the role exists, don't add
                var found = context.Roles.Where(ro => ro.Name == r.Name).FirstOrDefault();
                if (found == null)
                {
                    context.Add(r);
                    context.SaveChanges();
                }
            }

            //act
            var result = context.Roles.ToList();
            context.Roles.Remove(result[0]);
            context.SaveChanges();
            //refresh list
            result = context.Roles.ToList();

            //assert
            Assert.Equal(2,result.Count);
        }
    }
}
