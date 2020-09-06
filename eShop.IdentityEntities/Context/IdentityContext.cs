using eShop.IdentityEntities.Entities;
using eShop.IdentityEntities.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace eShop.IdentityEntities.Context
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users").HasData(
                    new User
                    {
                        Id = 1,
                        Active = true,
                        Name = "Admin",
                        CreatedOn = DateTime.UtcNow,
                        ExternalId = Guid.Parse("CCF5361B-74A9-4B75-8211-37B26E2D1BC0"),
                        PersonalEmail = "admin@gmail.com",
                        Password = "admin",
                        City = "Ahmedabad",
                        State = "Gujrat",
                        Line1 = "Test line 1",
                        Line2 = "test line 2",
                        ContactNumber = "7665856521",
                        UserType = UserType.Admin,
                        Zipcode = "314038"
                    },
                    new User
                 {
                     Id = 2,
                     Active = true,
                     Name = "Retailer",
                     CreatedOn = DateTime.UtcNow,
                     ExternalId = Guid.Parse("D25F7D72-E475-4D6F-8CA7-AAF189D322C4"),
                     PersonalEmail = "retailer@gmail.com",
                     Password = "retailer",
                     City = "Ahmedabad",
                     State = "Gujrat",
                     Line1 = "Test line 1",
                     Line2 = "test line 2",
                     ContactNumber = "7665856521",
                     UserType = UserType.Retailer,
                     Zipcode = "314038"
                 },
                    new User
                    {
                        Id = 3,
                        Active = true,
                        Name = "Customer",
                        CreatedOn = DateTime.UtcNow,
                        ExternalId = Guid.Parse("9B887027-FD67-45BA-9AD2-EDECCA67C1B3"),
                        PersonalEmail = "customer@gmail.com",
                        Password = "customer",
                        City = "Ahmedabad",
                        State = "Gujrat",
                        Line1 = "Test line 1",
                        Line2 = "test line 2",
                        ContactNumber = "7665856521",
                        UserType = UserType.Customer,
                        Zipcode = "314038"
                    });

            modelBuilder.Entity<UserToken>().ToTable("UserTokens");

        }
    }
}
