﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proteus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Infrastructure.Data
{
    public class ProteusContext : DbContext
    {
        //When registering multiple DbContext types make sure that the constructor for each context type has a DbContextOptions<TContext> parameter rather than a non-generic DbContextOptions parameter   
        //https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext
        public ProteusContext(DbContextOptions<ProteusContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(ConfigureProduct);
            builder.Entity<Category>(ConfigureCategory);
        }

        private void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .ForSqlServerUseSequenceHiLo("aspnetrun_type_hilo")
               .IsRequired();

            builder.Property(cb => cb.ProductName)
                .IsRequired()
                .HasMaxLength(100);
        }

        private void ConfigureCategory(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .ForSqlServerUseSequenceHiLo("aspnetrun_type_hilo")
               .IsRequired();

            builder.Property(cb => cb.CategoryName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
