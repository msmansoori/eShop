using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShop.ProductEntities.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedOn", "ExternalId", "InternalReference", "ModifiedOn", "Name" },
                values: new object[] { 1L, true, new DateTime(2020, 8, 31, 11, 43, 11, 894, DateTimeKind.Utc).AddTicks(9132), new Guid("62825b80-f20b-481f-8df5-41a3b3a00839"), 1L, null, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedOn", "ExternalId", "InternalReference", "ModifiedOn", "Name" },
                values: new object[] { 2L, true, new DateTime(2020, 8, 31, 11, 43, 11, 895, DateTimeKind.Utc).AddTicks(444), new Guid("163e6890-336e-4dc1-b0dc-20c5c8be94b4"), 2L, null, "Retailer" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "ExternalId", "ModifiedById", "ModifiedOn", "Name" },
                values: new object[] { 1L, true, 2L, new DateTime(2020, 8, 31, 11, 43, 11, 896, DateTimeKind.Utc).AddTicks(8317), new Guid("ff2b29b3-f4b8-4ef5-94f6-07582da3f885"), null, null, "SKMEIMore Men Watches from SKMEI" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "ExternalId", "ModifiedById", "ModifiedOn", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1L, true, 1L, new DateTime(2020, 8, 31, 11, 43, 11, 896, DateTimeKind.Utc).AddTicks(7228), new Guid("1b55a924-11f2-4248-9820-55fad0688836"), null, null, "Women", 0L },
                    { 2L, true, 1L, new DateTime(2020, 8, 31, 11, 43, 11, 896, DateTimeKind.Utc).AddTicks(7360), new Guid("a9990744-7f30-4110-86c7-e46ae805ca28"), null, null, "Men", 0L },
                    { 3L, true, 1L, new DateTime(2020, 8, 31, 11, 43, 11, 896, DateTimeKind.Utc).AddTicks(7364), new Guid("0c426574-d0ca-4fb8-9907-ccd047d15907"), null, null, "Kid", 0L },
                    { 4L, true, 1L, new DateTime(2020, 8, 31, 11, 43, 11, 896, DateTimeKind.Utc).AddTicks(7367), new Guid("56568eec-83c5-466d-bc97-01b9ce0643fa"), null, null, "Accessories", 0L },
                    { 5L, true, 1L, new DateTime(2020, 8, 31, 11, 43, 11, 896, DateTimeKind.Utc).AddTicks(7369), new Guid("ca1c35a8-38a8-40b7-92a8-ee0f99b6616c"), null, null, "Cosmetic", 0L }
                });

            migrationBuilder.InsertData(
                table: "UploadedFiles",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "Entity", "EntityId", "ExternalId", "FilePath", "ModifiedById", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1L, true, 1L, new DateTime(2020, 8, 31, 11, 43, 11, 897, DateTimeKind.Utc).AddTicks(9395), 1, 1L, new Guid("6b7f8362-5f48-4d44-a1df-8bb7b2e0dda1"), "/img/product/details/product-1.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 1" },
                    { 2L, true, 1L, new DateTime(2020, 8, 31, 11, 43, 11, 897, DateTimeKind.Utc).AddTicks(9677), 1, 1L, new Guid("24b0f523-2ed8-42fc-8dfb-724a14b0d511"), "/img/product/details/product-2.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 2" },
                    { 3L, true, 1L, new DateTime(2020, 8, 31, 11, 43, 11, 897, DateTimeKind.Utc).AddTicks(9688), 1, 1L, new Guid("350efe8c-e80e-4e48-b246-383c4a54aeb3"), "/img/product/details/product-3.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 3" },
                    { 4L, true, 1L, new DateTime(2020, 8, 31, 11, 43, 11, 897, DateTimeKind.Utc).AddTicks(9691), 1, 1L, new Guid("9a1510d3-c489-45eb-8b34-366218ac63b5"), "/img/product/details/product-4.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 4" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Active", "AvailableStock", "BrandId", "CategoryId", "Colors", "CreatedById", "CreatedOn", "Description", "Discount", "DiscountPrice", "ExternalId", "ModifiedById", "ModifiedOn", "Name", "Price", "Promotion", "ShortDescription", "Sizes", "Specification" },
                values: new object[] { 1L, true, 10, 1L, 1L, "1;2;3", 2L, new DateTime(2020, 8, 31, 11, 43, 11, 896, DateTimeKind.Utc).AddTicks(9676), @"Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt loret. Neque porro lorem quisquam est, qui dolorem ipsum quia dolor si.Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia ipsu consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Nulla consequat massa quis enim.  Lorem ipsum dolor sit amet, consectetuer adipiscing elit.Aenean commodo ligula eget dolor.Aenean massa.Cum sociis natoque penatibus et magnis dis parturient montes, 
 nascetur ridiculus mus.Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem", 10.0, 50.05m, new Guid("0fd95952-fd8f-4dad-9a95-3d8f27f654f0"), null, null, "ESSENTIAL STRUCTURED BLAZER", 500.5m, 1, "Nemo enim ipsam voluptatem quia aspernatur aut odit aut loret fugit, sed quia consequuntur magni lores eos qui ratione voluptatem sequi nesciunt.", "1;2;3", @"Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt loret. Neque porro lorem quisquam est, qui dolorem ipsum quia dolor si. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia ipsu consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Nulla consequat massa quis enim. 
 Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UploadedFiles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UploadedFiles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "UploadedFiles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "UploadedFiles",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
