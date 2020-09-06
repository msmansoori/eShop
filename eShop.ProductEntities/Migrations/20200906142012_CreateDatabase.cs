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
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    CreatedById = table.Column<long>(nullable: true),
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
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    CreatedById = table.Column<long>(nullable: true),
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
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    CreatedById = table.Column<long>(nullable: true),
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
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    CreatedById = table.Column<long>(nullable: true),
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
                table: "Brands",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "ExternalId", "ModifiedById", "ModifiedOn", "Name" },
                values: new object[] { 1L, true, null, new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(5179), new Guid("a903aad9-91e4-4621-b849-e04412431df8"), null, null, "SKMEIMore Men Watches from SKMEI" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "ExternalId", "ModifiedById", "ModifiedOn", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1L, true, null, new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(4399), new Guid("a992095e-7c12-4e89-9ee4-1b68edab29fe"), null, null, "Women", 0L },
                    { 2L, true, null, new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(4482), new Guid("7218611b-3951-4655-aac1-462b4f5a4bcc"), null, null, "Men", 0L },
                    { 3L, true, null, new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(4485), new Guid("c7020e64-5b03-424e-8095-973d7bbe6f92"), null, null, "Kid", 0L },
                    { 4L, true, null, new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(4487), new Guid("9efec92d-7592-41ec-93d6-33cd662ae733"), null, null, "Accessories", 0L },
                    { 5L, true, null, new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(4500), new Guid("03b42fab-192e-4f64-9e52-d39a66a04d60"), null, null, "Cosmetic", 0L }
                });

            migrationBuilder.InsertData(
                table: "UploadedFiles",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "Entity", "EntityId", "ExternalId", "FilePath", "ModifiedById", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1L, true, null, new DateTime(2020, 9, 6, 14, 20, 12, 131, DateTimeKind.Utc).AddTicks(3522), 1, 1L, new Guid("e375bfcb-e02d-4a94-9347-d49a3992125d"), "/img/product/details/product-1.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 1" },
                    { 2L, true, null, new DateTime(2020, 9, 6, 14, 20, 12, 131, DateTimeKind.Utc).AddTicks(3571), 1, 1L, new Guid("63ab7c45-df8b-4486-a592-19b066e5cd56"), "/img/product/details/product-2.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 2" },
                    { 3L, true, null, new DateTime(2020, 9, 6, 14, 20, 12, 131, DateTimeKind.Utc).AddTicks(3574), 1, 1L, new Guid("89614092-208e-4fe0-b1f7-c942efe49d20"), "/img/product/details/product-3.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 3" },
                    { 4L, true, null, new DateTime(2020, 9, 6, 14, 20, 12, 131, DateTimeKind.Utc).AddTicks(3576), 1, 1L, new Guid("2093a383-bb29-4c5b-8992-f3800c85213a"), "/img/product/details/product-4.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 4" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "ExternalId", "InternalReference", "ModifiedById", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1L, true, null, new DateTime(2020, 9, 6, 14, 20, 12, 129, DateTimeKind.Utc).AddTicks(580), new Guid("5324a678-83cd-42a2-be4f-0d06241e9917"), new Guid("ccf5361b-74a9-4b75-8211-37b26e2d1bc0"), null, null, "Admin" },
                    { 2L, true, null, new DateTime(2020, 9, 6, 14, 20, 12, 129, DateTimeKind.Utc).AddTicks(1565), new Guid("1ce948ba-ee3e-48eb-aeae-b1ef3e98297d"), new Guid("d25f7d72-e475-4d6f-8ca7-aaf189d322c4"), null, null, "Retailer" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Active", "AvailableStock", "BrandId", "CategoryId", "Colors", "CreatedById", "CreatedOn", "Description", "Discount", "DiscountPrice", "ExternalId", "ModifiedById", "ModifiedOn", "Name", "Price", "Promotion", "ShortDescription", "Sizes", "Specification" },
                values: new object[] { 1L, true, 10, 1L, 1L, "1;2;3", null, new DateTime(2020, 9, 6, 14, 20, 12, 130, DateTimeKind.Utc).AddTicks(6237), @"Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt loret. Neque porro lorem quisquam est, qui dolorem ipsum quia dolor si.Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia ipsu consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Nulla consequat massa quis enim.  Lorem ipsum dolor sit amet, consectetuer adipiscing elit.Aenean commodo ligula eget dolor.Aenean massa.Cum sociis natoque penatibus et magnis dis parturient montes, 
 nascetur ridiculus mus.Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem", 10.0, 50.05m, new Guid("59a8ac0a-050d-478b-8ea6-49b53a220d2b"), null, null, "ESSENTIAL STRUCTURED BLAZER", 500.5m, 1, "Nemo enim ipsam voluptatem quia aspernatur aut odit aut loret fugit, sed quia consequuntur magni lores eos qui ratione voluptatem sequi nesciunt.", "1;2;3", @"Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt loret. Neque porro lorem quisquam est, qui dolorem ipsum quia dolor si. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia ipsu consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Nulla consequat massa quis enim. 
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
