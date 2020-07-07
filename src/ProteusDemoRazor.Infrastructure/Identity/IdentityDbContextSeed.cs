using Microsoft.AspNetCore.Identity;
using Proteus.Core.Constants;
using Proteus.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proteus.Infrastructure.Identity
{
    //TODO IDENTITY: Step 4 build out seed data for identity
    public static class IdentityDbContextSeed
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            //used to call everything else
            SeedRoles(roleManager);
            SeedUser(userManager);
        }

        private static void SeedRoles(RoleManager<Role> roleManager)
        {
            bool roleExists = roleManager.RoleExistsAsync("Admin").Result;
            //only create roles
            if (!roleExists)
            {
                Role role = new Role();
                role.Name = "Admin";
                role.NormalizedName = role.Name.ToUpper();
                role.Description = "Administrator of Application";
                role.CreatedDate = System.DateTime.Now;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            roleExists = roleManager.RoleExistsAsync("SuperUser").Result;
            if (!roleExists)
            {
                Role role = new Role();
                role.Name = "SuperUser";
                role.NormalizedName = role.Name.ToUpper();
                role.Description = "Can do more than a normal user but less than the admin";
                role.CreatedDate = System.DateTime.Now;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            roleExists = roleManager.RoleExistsAsync("GeneralUser").Result;
            if (!roleExists)
            {
                Role role = new Role();
                role.Name = "GeneralUser";
                role.NormalizedName = role.Name.ToUpper();
                role.Description = "They can look but not touch";
                role.CreatedDate = System.DateTime.Now;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        private static void SeedUser(UserManager<User> userManager)
        {
            //only create users and the user manager can add the user to a role
            if (userManager.FindByNameAsync("Admin").Result == null)
            {
                User user = new User();
                user.FirstName = "AdminFirstName";
                user.LastName = "AdminLastName";
                user.UserName = "Admin";
                user.NormalizedUserName = user.UserName.ToUpper();
                user.Email = "admin@gmail.com";
                user.NormalizedEmail = user.Email.ToUpper();
                user.IsApproved = true;
                user.IsLockedOut = false;
                user.CreatedDate = System.DateTime.Now;
                IdentityResult userResult = userManager.CreateAsync(user, "Fluxgate1!").Result;
                if (userResult.Succeeded)
                {
                    Task<IdentityResult> result = userManager.AddToRoleAsync(user, "Admin");//use normaized role name
                }
            }

            if (userManager.FindByNameAsync("Mary.Lamb").Result == null)
            {
                User user = new User();
                user.FirstName = "Mary";
                user.LastName = "Lamb";
                user.UserName = "Mary.Lamb";
                user.Email = "mary.lamb@oldmac.farm";
                user.NormalizedEmail = user.Email.ToUpper();
                user.NormalizedUserName = user.UserName.ToUpper();
                user.IsApproved = true;
                user.IsLockedOut = false;
                user.CreatedDate = System.DateTime.Now;
                IdentityResult userResult = userManager.CreateAsync(user, "Fluxgate1!").Result;
                if (userResult.Succeeded)
                {
                    Task<IdentityResult> result = userManager.AddToRoleAsync(user, "GeneralUser");
                }
            }
        }
    }
}
