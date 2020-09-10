using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Proteus.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Proteus.Infrastructure.Tests.Identity
{
    public class IdentityContextUserRoleTests : BaseEFIdentityTestFixture
    {
        [Trait("Infrastructure", "Identity")]
        [Fact]
        public void IdentityContext_GetUserRoleList()
        {
            //assembl
            var context = base.GetDBContenxt();

            //act
            var result = context.UserRoles.ToList();

            //assert
            Assert.IsType<List<UserRole>>(result);
        }


        [Trait("Infrastructure", "Identity")]
        [Fact]
        public void ProteusContext_AddUserRoles()
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

            var users = base.CreateUsers();
            foreach (var u in users)
            {               
                var found = context.Users.Where(ur => ur.UserName == ur.UserName).FirstOrDefault();
                if (found == null)
                {
                    context.Add(u);
                    context.SaveChanges();
                }
            }

            //act
            //now add the user roles
            var userRole = base.CreateUserRole();
            //set up the Admin as the Administrator Role
            var user = context.Users.Where(u => u.UserName == "Admin").FirstOrDefault();
            var role = context.Roles.Where(r => r.Name == "Administrator").FirstOrDefault();
            userRole.UserId = user.Id;
            userRole.RoleId = role.Id;
            context.UserRoles.Add(userRole);
            context.SaveChanges();

            var result = context.UserRoles.ToList();

            //assert
            Assert.Single<UserRole>(result);
        }

        [Trait("Infrastructure", "Identity")]
        [Fact]
        public void ProteusContext_ChangeUserRole()
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

            var users = base.CreateUsers();
            foreach (var u in users)
            {
                var found = context.Users.Where(ur => ur.UserName == ur.UserName).FirstOrDefault();
                if (found == null)
                {
                    context.Add(u);
                    context.SaveChanges();
                }
            }
            //now add the user roles
            var userRole = base.CreateUserRole();
            //set up the Admin as the Administrator Role
            var user = context.Users.Where(u => u.UserName == "Admin").FirstOrDefault();
            var role = context.Roles.Where(r => r.Name == "Administrator").FirstOrDefault();
            userRole.UserId = user.Id;
            userRole.RoleId = role.Id;
            context.UserRoles.Add(userRole);
            context.SaveChanges();

            var result = context.UserRoles.ToList();

            //act
            //change the role to super user
            role = context.Roles.Where(r => r.Name == "SuperUser").FirstOrDefault();
            result[0].RoleId = role.Id;
            
            result = context.UserRoles.ToList();
            
            
            //assert
            Assert.Equal(role.Id, result[0].RoleId);
        }

        [Trait("Infrastructure", "Identity")]
        [Fact]
        public void ProteusContext_DeleteUserRole()
        {//assemble
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

            var users = base.CreateUsers();
            foreach (var u in users)
            {
                var found = context.Users.Where(ur => ur.UserName == ur.UserName).FirstOrDefault();
                if (found == null)
                {
                    context.Add(u);
                    context.SaveChanges();
                }
            }
            //now add the user roles
            var userRole = base.CreateUserRole();
            //set up the Admin as the Administrator Role
            var user = context.Users.Where(u => u.UserName == "Admin").FirstOrDefault();
            var role = context.Roles.Where(r => r.Name == "Administrator").FirstOrDefault();
            userRole.UserId = user.Id;
            userRole.RoleId = role.Id;
            context.UserRoles.Add(userRole);
            context.SaveChanges();

            var result = context.UserRoles.ToList();

            //delete one!
            context.UserRoles.Remove(result[0]);
            context.SaveChanges();

            //assert
            Assert.Empty(result);
        }
    }

    
}
