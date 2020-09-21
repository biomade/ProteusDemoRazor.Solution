using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Proteus.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityDbContext = Proteus.Infrastructure.Identity.IdentityDbContext;

namespace Proteus.Infrastructure.Tests.Identity
{
    public abstract class BaseEFIdentityTestFixture
    {
        protected static DbContextOptions<IdentityDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<IdentityDbContext>();
            builder.UseInMemoryDatabase("Identity")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected IdentityDbContext GetDBContenxt()
        {
            var options = CreateNewContextOptions();
            IdentityDbContext dbContext = new IdentityDbContext(options);
            return dbContext;
        }

        protected List<User> GetUserList()
        {
            List<User> users = new List<User>();
            //insert seed data into db
            User user = new User();
            user.FirstName = "AdminFirstName";
            user.LastName = "AdminLastName";
            user.UserName = "Admin";
            user.NormalizedUserName = "Admin".ToUpper();
            user.Email = "admin@gmail.com";
            user.NormalizedEmail = "admin@gmail.com".ToUpper();
            user.IsEnabled = true;
            user.IsLockedOut = false;
            user.CreatedDate = System.DateTime.Now;
            user.LastLoginDate = System.DateTime.Now;
            user.EDI = "NONE";

            users.Add(user);

            //insert seed data into db
            User user2 = new User();
            user2.FirstName = "Mary";
            user2.LastName = "Lamb";
            user2.UserName = "MaryLamb";
            user2.NormalizedUserName = "MaryLamb".ToUpper();
            user2.Email = "mary.lamb@gmail.com";
            user2.NormalizedEmail = "mary.lamb@gmail.com".ToUpper();
            user2.IsEnabled = true;
            user2.IsLockedOut = false;
            user2.CreatedDate = System.DateTime.Now;
            user2.LastLoginDate = System.DateTime.Now;
            user2.EDI = "NONE";

            users.Add(user2);

            return users;
        }

        protected List<Role> GetRoleList()
        {
            List<Role> roles = new List<Role>();
            Role role = new Role();

            role.Name = "Administrator";
            role.NormalizedName = role.Name.ToUpper();
            role.Description = "Can Do Everything";
            roles.Add(role);

            role = new Role();
            role.Name = "SuperUser";
            role.NormalizedName = role.Name.ToUpper();
            role.Description = "Almost everything";
            roles.Add(role);

            role = new Role();
            role.Name = "Visitor";
            role.NormalizedName = role.Name.ToUpper();
            role.Description = "A user who can look but not touch";
            roles.Add(role);

            return roles;
        }

        protected UserRole CreateUserRoleObj()
        {
            UserRole userRole = new UserRole();
            //Generic Role
            UserRole ur = new UserRole();            
            ur.CreatedDate = System.DateTime.Now;
            return userRole;
        }
    }
}
