using System;
using eShop.Common.Enums;
using eShop.ProductEntities.Entities;
using eShop.ProductEntities.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace eShop.ProductEntities.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users").HasData(
                new User { Id = 1, Active = true, Name = "Admin", UserType = UserType.Admin, InternalReference = Guid.Parse("CCF5361B-74A9-4B75-8211-37B26E2D1BC0"), CreatedOn = DateTime.UtcNow, ExternalId = Guid.NewGuid() },
                new User { Id = 2, Active = true, Name = "Retailer", UserType = UserType.Retailer, InternalReference = Guid.Parse("D25F7D72-E475-4D6F-8CA7-AAF189D322C4"), CreatedOn = DateTime.UtcNow, ExternalId = Guid.NewGuid() });

            modelBuilder.Entity<Category>().ToTable("Categories").HasData(
                new Category { Id = 1, Active = true, Name = "Women", CreatedById = 1, CreatedOn = DateTime.UtcNow, ExternalId = Guid.NewGuid() },
                new Category { Id = 2, Active = true, Name = "Men", CreatedById = 1, CreatedOn = DateTime.UtcNow, ExternalId = Guid.NewGuid() },
                new Category { Id = 3, Active = true, Name = "Kid", CreatedById = 1, CreatedOn = DateTime.UtcNow, ExternalId = Guid.NewGuid() },
                new Category { Id = 4, Active = true, Name = "Accessories", CreatedById = 1, CreatedOn = DateTime.UtcNow, ExternalId = Guid.NewGuid() },
                new Category { Id = 5, Active = true, Name = "Cosmetic", CreatedById = 1, CreatedOn = DateTime.UtcNow, ExternalId = Guid.NewGuid() });

            modelBuilder.Entity<Brand>().ToTable("Brands").HasData(
                new Brand { Id = 1, Active = true, Name = "SKMEIMore Men Watches from SKMEI", CreatedById = 2, CreatedOn = DateTime.UtcNow, ExternalId = Guid.NewGuid() });

            modelBuilder.Entity<Product>().ToTable("Products")
                .HasData(
                new Product
                {
                    Id = 1,
                    Active = true,
                    Name = "ESSENTIAL STRUCTURED BLAZER",
                    CreatedById = 2,
                    CreatedOn = DateTime.UtcNow,
                    ExternalId = Guid.NewGuid(),
                    CategoryId = 1,
                    BrandId = 1,
                    Price = (decimal)500.50,
                    Discount = 10,
                    DiscountPrice = (decimal)50.05,
                    Description = "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt loret. Neque porro lorem quisquam est, qui dolorem ipsum quia dolor si." +
                    "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia ipsu consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Nulla consequat massa quis enim.  Lorem ipsum dolor sit amet, " +
                    "consectetuer adipiscing elit.Aenean commodo ligula eget dolor.Aenean massa.Cum sociis natoque penatibus et magnis dis parturient montes, \n nascetur ridiculus mus.Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem",
                    ShortDescription = "Nemo enim ipsam voluptatem quia aspernatur aut odit aut loret fugit, sed quia consequuntur magni lores eos qui ratione voluptatem sequi nesciunt.",
                    Specification = "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt loret." +
                    " Neque porro lorem quisquam est, qui dolorem ipsum quia dolor si. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia ipsu consequuntur magni dolores" +
                    " eos qui ratione voluptatem sequi nesciunt. Nulla consequat massa quis enim. \n Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa." +
                    " Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem.",
                    Colors = "1;2;3",
                    Sizes = "1;2;3",
                    AvailableStock = 10,
                    Promotion = Promotion.FreeShipping
                });

            modelBuilder.Entity<UploadedFile>().ToTable("UploadedFiles").HasData(
                new UploadedFile { Id = 1, Name = "ESSENTIAL STRUCTURED BLAZER 1", FilePath = "/img/product/details/product-1.jpg", Entity = Entity.Product, EntityId = 1, Active = true, CreatedById = 1, CreatedOn = DateTime.UtcNow, ExternalId = Guid.NewGuid() },
                new UploadedFile { Id = 2, Name = "ESSENTIAL STRUCTURED BLAZER 2", FilePath = "/img/product/details/product-2.jpg", Entity = Entity.Product, EntityId = 1, Active = true, CreatedById = 1, CreatedOn = DateTime.UtcNow, ExternalId = Guid.NewGuid() },
                new UploadedFile { Id = 3, Name = "ESSENTIAL STRUCTURED BLAZER 3", FilePath = "/img/product/details/product-3.jpg", Entity = Entity.Product, EntityId = 1, Active = true, CreatedById = 1, CreatedOn = DateTime.UtcNow, ExternalId = Guid.NewGuid() },
                new UploadedFile { Id = 4, Name = "ESSENTIAL STRUCTURED BLAZER 4", FilePath = "/img/product/details/product-4.jpg", Entity = Entity.Product, EntityId = 1, Active = true, CreatedById = 1, CreatedOn = DateTime.UtcNow, ExternalId = Guid.NewGuid() });

        }
    }
}
