using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShop.ProductEntities.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    ExternalId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<long>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    InternalReference = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    ExternalId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<long>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Brands_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    ExternalId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<long>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    ExternalId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<long>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    EntityId = table.Column<long>(nullable: false),
                    Entity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(nullable: false),
                    ExternalId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<long>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CategoryId = table.Column<long>(nullable: false),
                    BrandId = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    DiscountPrice = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Specification = table.Column<string>(nullable: true),
                    Colors = table.Column<string>(nullable: true),
                    Sizes = table.Column<string>(nullable: true),
                    AvailableStock = table.Column<int>(nullable: false),
                    Promotion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "ExternalId", "InternalReference", "ModifiedById", "ModifiedOn", "Name", "UserType" },
                values: new object[] { 1L, true, null, new DateTime(2020, 9, 7, 6, 44, 41, 493, DateTimeKind.Utc).AddTicks(1189), new Guid("e146d1ec-eddb-4d8a-a046-1364fd357040"), new Guid("ccf5361b-74a9-4b75-8211-37b26e2d1bc0"), null, null, "Admin", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "ExternalId", "InternalReference", "ModifiedById", "ModifiedOn", "Name", "UserType" },
                values: new object[] { 2L, true, null, new DateTime(2020, 9, 7, 6, 44, 41, 493, DateTimeKind.Utc).AddTicks(2179), new Guid("dfa81c2a-1685-4c8b-9c33-ac258be1d6a6"), new Guid("d25f7d72-e475-4d6f-8ca7-aaf189d322c4"), null, null, "Retailer", 3 });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "ExternalId", "ModifiedById", "ModifiedOn", "Name" },
                values: new object[] { 1L, true, 2L, new DateTime(2020, 9, 7, 6, 44, 41, 494, DateTimeKind.Utc).AddTicks(5539), new Guid("a1912521-35d8-4a1e-b765-9a308a824df2"), null, null, "SKMEIMore Men Watches from SKMEI" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "ExternalId", "ModifiedById", "ModifiedOn", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1L, true, 1L, new DateTime(2020, 9, 7, 6, 44, 41, 494, DateTimeKind.Utc).AddTicks(4773), new Guid("a89e306a-955b-4581-8187-962f745d1a44"), null, null, "Women", 0L },
                    { 2L, true, 1L, new DateTime(2020, 9, 7, 6, 44, 41, 494, DateTimeKind.Utc).AddTicks(4853), new Guid("cfc14a93-52e0-48e7-8ff4-333e9cf4a87e"), null, null, "Men", 0L },
                    { 3L, true, 1L, new DateTime(2020, 9, 7, 6, 44, 41, 494, DateTimeKind.Utc).AddTicks(4856), new Guid("d6d52e80-41e7-4ab4-bfda-7b2353363a72"), null, null, "Kid", 0L },
                    { 4L, true, 1L, new DateTime(2020, 9, 7, 6, 44, 41, 494, DateTimeKind.Utc).AddTicks(4858), new Guid("9cc500bf-a294-4cf6-ae3b-7b20034c5438"), null, null, "Accessories", 0L },
                    { 5L, true, 1L, new DateTime(2020, 9, 7, 6, 44, 41, 494, DateTimeKind.Utc).AddTicks(4871), new Guid("513c3a23-b9e0-4c91-bcb0-def761e0abf4"), null, null, "Cosmetic", 0L }
                });

            migrationBuilder.InsertData(
                table: "UploadedFiles",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "Entity", "EntityId", "ExternalId", "FilePath", "ModifiedById", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1L, true, 1L, new DateTime(2020, 9, 7, 6, 44, 41, 495, DateTimeKind.Utc).AddTicks(3259), 1, 1L, new Guid("e9422b29-b694-4648-90a8-f2a582d9f3e5"), "/img/product/details/product-1.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 1" },
                    { 2L, true, 1L, new DateTime(2020, 9, 7, 6, 44, 41, 495, DateTimeKind.Utc).AddTicks(3441), 1, 1L, new Guid("322e3e83-4220-4466-a7cc-4d374a06cedd"), "/img/product/details/product-2.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 2" },
                    { 3L, true, 1L, new DateTime(2020, 9, 7, 6, 44, 41, 495, DateTimeKind.Utc).AddTicks(3444), 1, 1L, new Guid("af39fd3a-b94d-4a67-ae68-fb4ce38d2e17"), "/img/product/details/product-3.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 3" },
                    { 4L, true, 1L, new DateTime(2020, 9, 7, 6, 44, 41, 495, DateTimeKind.Utc).AddTicks(3446), 1, 1L, new Guid("f1d98c6a-52eb-498c-88a0-c28932e116d5"), "/img/product/details/product-4.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 4" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Active", "AvailableStock", "BrandId", "CategoryId", "Colors", "CreatedById", "CreatedOn", "Description", "Discount", "DiscountPrice", "ExternalId", "ModifiedById", "ModifiedOn", "Name", "Price", "Promotion", "ShortDescription", "Sizes", "Specification" },
                values: new object[] { 1L, true, 10, 1L, 1L, "1;2;3", 2L, new DateTime(2020, 9, 7, 6, 44, 41, 494, DateTimeKind.Utc).AddTicks(6683), @"Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt loret. Neque porro lorem quisquam est, qui dolorem ipsum quia dolor si.Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia ipsu consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Nulla consequat massa quis enim.  Lorem ipsum dolor sit amet, consectetuer adipiscing elit.Aenean commodo ligula eget dolor.Aenean massa.Cum sociis natoque penatibus et magnis dis parturient montes, 
 nascetur ridiculus mus.Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem", 10.0, 50.05m, new Guid("4f823e48-aff5-4ed2-9f36-67029382f692"), null, null, "ESSENTIAL STRUCTURED BLAZER", 500.5m, 1, "Nemo enim ipsam voluptatem quia aspernatur aut odit aut loret fugit, sed quia consequuntur magni lores eos qui ratione voluptatem sequi nesciunt.", "1;2;3", @"Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt loret. Neque porro lorem quisquam est, qui dolorem ipsum quia dolor si. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia ipsu consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Nulla consequat massa quis enim. 
 Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem." });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CreatedById",
                table: "Brands",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_ModifiedById",
                table: "Brands",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ModifiedById",
                table: "Categories",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedById",
                table: "Products",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ModifiedById",
                table: "Products",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_CreatedById",
                table: "UploadedFiles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_ModifiedById",
                table: "UploadedFiles",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
