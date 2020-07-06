using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proteus.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Infrastructure.Identity
{
    //TODO IDENTITY: Step 2a - Create a  dbContext for just Identity
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
            //constructor
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            //TODO IDENTITY: Step 2b- create model/database!!
            base.OnModelCreating(modelbuilder);


            //TODO IDENTITY: Step 6 run db create commands, in the PM select the project that has the Identity context
            //PM>Add-Migration CreateIdentity -Context IdentityDbContext
            //PM>Update-Database -Context IdentityDbContext

        }
    }
}

