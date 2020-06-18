﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Proteus.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Infrastructure.Migrations
{
    [DbContext(typeof(ProteusContext))]
    [Migration("20190321052921_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.aspnetrun_type_hilo", "'aspnetrun_type_hilo', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AspnetRun.Core.Entities.Category", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:HiLoSequenceName", "aspnetrun_type_hilo")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                b.Property<string>("CategoryName")
                    .IsRequired()
                    .HasMaxLength(100);

                b.Property<string>("Description");

                b.HasKey("Id");

                b.ToTable("Category");
            });

            modelBuilder.Entity("AspnetRun.Core.Entities.Product", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:HiLoSequenceName", "aspnetrun_type_hilo")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                b.Property<int>("CategoryId");

                b.Property<bool>("Discontinued");

                b.Property<string>("ProductName")
                    .IsRequired()
                    .HasMaxLength(100);

                b.Property<string>("QuantityPerUnit");

                b.Property<short?>("ReorderLevel");

                b.Property<decimal?>("UnitPrice");

                b.Property<short?>("UnitsInStock");

                b.Property<short?>("UnitsOnOrder");

                b.HasKey("Id");

                b.HasIndex("CategoryId");

                b.ToTable("Product");
            });

            modelBuilder.Entity("AspnetRun.Core.Entities.Product", b =>
            {
                b.HasOne("AspnetRun.Core.Entities.Category", "Category")
                    .WithMany("Products")
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}
