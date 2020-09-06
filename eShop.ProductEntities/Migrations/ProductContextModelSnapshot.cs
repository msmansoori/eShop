﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eShop.ProductEntities.Context;

namespace eShop.ProductEntities.Migrations
{
    [DbContext(typeof(ProductContext))]
    partial class ProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eShop.ProductEntities.Entities.Brand", b =>
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

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = true,
                            CreatedOn = new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(5179),
                            ExternalId = new Guid("a903aad9-91e4-4621-b849-e04412431df8"),
                            Name = "SKMEIMore Men Watches from SKMEI"
                        });
                });

            modelBuilder.Entity("eShop.ProductEntities.Entities.Category", b =>
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

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ParentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = true,
                            CreatedOn = new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(4399),
                            ExternalId = new Guid("a992095e-7c12-4e89-9ee4-1b68edab29fe"),
                            Name = "Women",
                            ParentId = 0L
                        },
                        new
                        {
                            Id = 2L,
                            Active = true,
                            CreatedOn = new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(4482),
                            ExternalId = new Guid("7218611b-3951-4655-aac1-462b4f5a4bcc"),
                            Name = "Men",
                            ParentId = 0L
                        },
                        new
                        {
                            Id = 3L,
                            Active = true,
                            CreatedOn = new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(4485),
                            ExternalId = new Guid("c7020e64-5b03-424e-8095-973d7bbe6f92"),
                            Name = "Kid",
                            ParentId = 0L
                        },
                        new
                        {
                            Id = 4L,
                            Active = true,
                            CreatedOn = new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(4487),
                            ExternalId = new Guid("9efec92d-7592-41ec-93d6-33cd662ae733"),
                            Name = "Accessories",
                            ParentId = 0L
                        },
                        new
                        {
                            Id = 5L,
                            Active = true,
                            CreatedOn = new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(4500),
                            ExternalId = new Guid("03b42fab-192e-4f64-9e52-d39a66a04d60"),
                            Name = "Cosmetic",
                            ParentId = 0L
                        });
                });

            modelBuilder.Entity("eShop.ProductEntities.Entities.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("AvailableStock")
                        .HasColumnType("int");

                    b.Property<long>("BrandId")
                        .HasColumnType("bigint");

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<string>("Colors")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<decimal>("DiscountPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Promotion")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sizes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specification")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = true,
                            AvailableStock = 10,
                            BrandId = 1L,
                            CategoryId = 1L,
                            Colors = "1;2;3",
                            CreatedOn = new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(6237),
                            Description = @"Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt loret. Neque porro lorem quisquam est, qui dolorem ipsum quia dolor si.Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia ipsu consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Nulla consequat massa quis enim.  Lorem ipsum dolor sit amet, consectetuer adipiscing elit.Aenean commodo ligula eget dolor.Aenean massa.Cum sociis natoque penatibus et magnis dis parturient montes, 
 nascetur ridiculus mus.Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem",
                            Discount = 10.0,
                            DiscountPrice = 50.05m,
                            ExternalId = new Guid("59a8ac0a-050d-478b-8ea6-49b53a220d2b"),
                            Name = "ESSENTIAL STRUCTURED BLAZER",
                            Price = 500.5m,
                            Promotion = 1,
                            ShortDescription = "Nemo enim ipsam voluptatem quia aspernatur aut odit aut loret fugit, sed quia consequuntur magni lores eos qui ratione voluptatem sequi nesciunt.",
                            Sizes = "1;2;3",
                            Specification = @"Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt loret. Neque porro lorem quisquam est, qui dolorem ipsum quia dolor si. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia ipsu consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Nulla consequat massa quis enim. 
 Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem."
                        });
                });

            modelBuilder.Entity("eShop.ProductEntities.Entities.UploadedFile", b =>
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

                    b.Property<int>("Entity")
                        .HasColumnType("int");

                    b.Property<long>("EntityId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("UploadedFiles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = true,
                            CreatedOn = new DateTime(2020, 9, 6, 14, 20, 12, 131, DateTimeKind.Utc).AddTicks(3522),
                            Entity = 1,
                            EntityId = 1L,
                            ExternalId = new Guid("e375bfcb-e02d-4a94-9347-d49a3992125d"),
                            FilePath = "/img/product/details/product-1.jpg",
                            Name = "ESSENTIAL STRUCTURED BLAZER 1"
                        },
                        new
                        {
                            Id = 2L,
                            Active = true,
                            CreatedOn = new DateTime(2020, 9, 6, 14, 20, 12, 131, DateTimeKind.Utc).AddTicks(3571),
                            Entity = 1,
                            EntityId = 1L,
                            ExternalId = new Guid("63ab7c45-df8b-4486-a592-19b066e5cd56"),
                            FilePath = "/img/product/details/product-2.jpg",
                            Name = "ESSENTIAL STRUCTURED BLAZER 2"
                        },
                        new
                        {
                            Id = 3L,
                            Active = true,
                            CreatedOn = new DateTime(2020, 9, 6, 14, 20, 12, 131, DateTimeKind.Utc).AddTicks(3574),
                            Entity = 1,
                            EntityId = 1L,
                            ExternalId = new Guid("89614092-208e-4fe0-b1f7-c942efe49d20"),
                            FilePath = "/img/product/details/product-3.jpg",
                            Name = "ESSENTIAL STRUCTURED BLAZER 3"
                        },
                        new
                        {
                            Id = 4L,
                            Active = true,
                            CreatedOn = new DateTime(2020, 9, 6, 14, 20, 12, 131, DateTimeKind.Utc).AddTicks(3576),
                            Entity = 1,
                            EntityId = 1L,
                            ExternalId = new Guid("2093a383-bb29-4c5b-8992-f3800c85213a"),
                            FilePath = "/img/product/details/product-4.jpg",
                            Name = "ESSENTIAL STRUCTURED BLAZER 4"
                        });
                });

            modelBuilder.Entity("eShop.ProductEntities.Entities.User", b =>
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

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InternalReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Active = true,
                            CreatedOn = new DateTime(2020, 9, 6, 14, 20, 12, 129, DateTimeKind.Utc).AddTicks(580),
                            ExternalId = new Guid("5324a678-83cd-42a2-be4f-0d06241e9917"),
                            InternalReference = new Guid("ccf5361b-74a9-4b75-8211-37b26e2d1bc0"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2L,
                            Active = true,
                            CreatedOn = new DateTime(2020, 9, 6, 14, 20, 12, 129, DateTimeKind.Utc).AddTicks(1565),
                            ExternalId = new Guid("1ce948ba-ee3e-48eb-aeae-b1ef3e98297d"),
                            InternalReference = new Guid("d25f7d72-e475-4d6f-8ca7-aaf189d322c4"),
                            Name = "Retailer"
                        });
                });

            modelBuilder.Entity("eShop.ProductEntities.Entities.Brand", b =>
                {
                    b.HasOne("eShop.ProductEntities.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("eShop.ProductEntities.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("eShop.ProductEntities.Entities.Category", b =>
                {
                    b.HasOne("eShop.ProductEntities.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("eShop.ProductEntities.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("eShop.ProductEntities.Entities.Product", b =>
                {
                    b.HasOne("eShop.ProductEntities.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eShop.ProductEntities.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eShop.ProductEntities.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("eShop.ProductEntities.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("eShop.ProductEntities.Entities.UploadedFile", b =>
                {
                    b.HasOne("eShop.ProductEntities.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("eShop.ProductEntities.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");
                });
#pragma warning restore 612, 618
        }
    }
}
