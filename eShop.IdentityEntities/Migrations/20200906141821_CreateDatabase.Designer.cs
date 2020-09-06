﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eShop.IdentityEntities.Context;

namespace eShop.IdentityEntities.Migrations
{
    [DbContext(typeof(IdentityContext))]
    [Migration("20200906141821_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eShop.IdentityEntities.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("BusinessEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Line1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.Property<string>("Zipcode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = true,
                            City = "Ahmedabad",
                            ContactNumber = "7665856521",
                            CreatedOn = new DateTime(2020, 9, 6, 14, 18, 21, 251, DateTimeKind.Utc).AddTicks(1265),
                            ExternalId = new Guid("ccf5361b-74a9-4b75-8211-37b26e2d1bc0"),
                            Line1 = "Test line 1",
                            Line2 = "test line 2",
                            Name = "Admin",
                            Password = "admin",
                            PersonalEmail = "admin@gmail.com",
                            State = "Gujrat",
                            UserType = 1,
                            Zipcode = "314038"
                        },
                        new
                        {
                            Id = 2L,
                            Active = true,
                            City = "Ahmedabad",
                            ContactNumber = "7665856521",
                            CreatedOn = new DateTime(2020, 9, 6, 14, 18, 21, 251, DateTimeKind.Utc).AddTicks(5728),
                            ExternalId = new Guid("d25f7d72-e475-4d6f-8ca7-aaf189d322c4"),
                            Line1 = "Test line 1",
                            Line2 = "test line 2",
                            Name = "Retailer",
                            Password = "retailer",
                            PersonalEmail = "retailer@gmail.com",
                            State = "Gujrat",
                            UserType = 3,
                            Zipcode = "314038"
                        },
                        new
                        {
                            Id = 3L,
                            Active = true,
                            City = "Ahmedabad",
                            ContactNumber = "7665856521",
                            CreatedOn = new DateTime(2020, 9, 6, 14, 18, 21, 251, DateTimeKind.Utc).AddTicks(5823),
                            ExternalId = new Guid("9b887027-fd67-45ba-9ad2-edecca67c1b3"),
                            Line1 = "Test line 1",
                            Line2 = "test line 2",
                            Name = "Customer",
                            Password = "customer",
                            PersonalEmail = "customer@gmail.com",
                            State = "Gujrat",
                            UserType = 2,
                            Zipcode = "314038"
                        });
                });

            modelBuilder.Entity("eShop.IdentityEntities.Entities.UserToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("eShop.IdentityEntities.Entities.UserToken", b =>
                {
                    b.HasOne("eShop.IdentityEntities.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");
                });
#pragma warning restore 612, 618
        }
    }
}