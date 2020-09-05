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
                    ModifiedOn = table.Column<DateTime>(nullable: true),
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
                    CreatedById = table.Column<long>(nullable: true),
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
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<long>(nullable: true),
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
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<long>(nullable: true),
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
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<long>(nullable: true),
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
                columns: new[] { "Id", "Active", "CreatedOn", "ExternalId", "InternalReference", "ModifiedOn", "Name" },
                values: new object[] { 1L, true, new DateTime(2020, 8, 31, 15, 21, 6, 950, DateTimeKind.Utc).AddTicks(6099), new Guid("84baf1d4-f821-4d1d-9632-5b2248d8bd29"), new Guid("ccf5361b-74a9-4b75-8211-37b26e2d1bc0"), null, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedOn", "ExternalId", "InternalReference", "ModifiedOn", "Name" },
                values: new object[] { 2L, true, new DateTime(2020, 8, 31, 15, 21, 6, 950, DateTimeKind.Utc).AddTicks(6863), new Guid("02a5bfae-0899-4016-9448-9a5913ac2474"), new Guid("d25f7d72-e475-4d6f-8ca7-aaf189d322c4"), null, "Retailer" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "ExternalId", "ModifiedById", "ModifiedOn", "Name" },
                values: new object[] { 1L, true, 2L, new DateTime(2020, 8, 31, 15, 21, 6, 952, DateTimeKind.Utc).AddTicks(1574), new Guid("54e6e514-89c5-4839-9cc3-50c79e43882a"), null, null, "SKMEIMore Men Watches from SKMEI" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "ExternalId", "ModifiedById", "ModifiedOn", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1L, true, 1L, new DateTime(2020, 8, 31, 15, 21, 6, 952, DateTimeKind.Utc).AddTicks(908), new Guid("fba63609-c392-4d74-8738-3466399853ef"), null, null, "Women", 0L },
                    { 2L, true, 1L, new DateTime(2020, 8, 31, 15, 21, 6, 952, DateTimeKind.Utc).AddTicks(926), new Guid("70a05440-2d84-4b26-9976-af98e8478725"), null, null, "Men", 0L },
                    { 3L, true, 1L, new DateTime(2020, 8, 31, 15, 21, 6, 952, DateTimeKind.Utc).AddTicks(928), new Guid("42a80e59-0f90-4a3d-b0c3-551342c8fa87"), null, null, "Kid", 0L },
                    { 4L, true, 1L, new DateTime(2020, 8, 31, 15, 21, 6, 952, DateTimeKind.Utc).AddTicks(930), new Guid("b4b2d122-743e-47ef-bf77-322b3a7f720e"), null, null, "Accessories", 0L },
                    { 5L, true, 1L, new DateTime(2020, 8, 31, 15, 21, 6, 952, DateTimeKind.Utc).AddTicks(947), new Guid("b5b8e605-8338-47d6-92b5-bb0082519741"), null, null, "Cosmetic", 0L }
                });

            migrationBuilder.InsertData(
                table: "UploadedFiles",
                columns: new[] { "Id", "Active", "CreatedById", "CreatedOn", "Entity", "EntityId", "ExternalId", "FilePath", "ModifiedById", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1L, true, 1L, new DateTime(2020, 8, 31, 15, 21, 6, 952, DateTimeKind.Utc).AddTicks(8855), 1, 1L, new Guid("41e4863e-dfca-4683-a394-bc2cb43d2d0e"), "/img/product/details/product-1.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 1" },
                    { 2L, true, 1L, new DateTime(2020, 8, 31, 15, 21, 6, 952, DateTimeKind.Utc).AddTicks(8867), 1, 1L, new Guid("3d665eb9-aee2-485a-b5d4-6371848ee41e"), "/img/product/details/product-2.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 2" },
                    { 3L, true, 1L, new DateTime(2020, 8, 31, 15, 21, 6, 952, DateTimeKind.Utc).AddTicks(8870), 1, 1L, new Guid("ef63f815-8eab-4a6b-b3ab-7227798a6ecf"), "/img/product/details/product-3.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 3" },
                    { 4L, true, 1L, new DateTime(2020, 8, 31, 15, 21, 6, 952, DateTimeKind.Utc).AddTicks(8871), 1, 1L, new Guid("d8ee576a-7e27-4771-8478-4ab394cd2576"), "/img/product/details/product-4.jpg", null, null, "ESSENTIAL STRUCTURED BLAZER 4" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Active", "AvailableStock", "BrandId", "CategoryId", "Colors", "CreatedById", "CreatedOn", "Description", "Discount", "DiscountPrice", "ExternalId", "ModifiedById", "ModifiedOn", "Name", "Price", "Promotion", "ShortDescription", "Sizes", "Specification" },
                values: new object[] { 1L, true, 10, 1L, 1L, "1;2;3", 2L, new DateTime(2020, 8, 31, 15, 21, 6, 952, DateTimeKind.Utc).AddTicks(2501), @"Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt loret. Neque porro lorem quisquam est, qui dolorem ipsum quia dolor si.Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia ipsu consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Nulla consequat massa quis enim.  Lorem ipsum dolor sit amet, consectetuer adipiscing elit.Aenean commodo ligula eget dolor.Aenean massa.Cum sociis natoque penatibus et magnis dis parturient montes, 
 nascetur ridiculus mus.Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem", 10.0, 50.05m, new Guid("e3647825-b793-404f-aa6a-992d32cda0cb"), null, null, "ESSENTIAL STRUCTURED BLAZER", 500.5m, 1, "Nemo enim ipsam voluptatem quia aspernatur aut odit aut loret fugit, sed quia consequuntur magni lores eos qui ratione voluptatem sequi nesciunt.", "1;2;3", @"Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt loret. Neque porro lorem quisquam est, qui dolorem ipsum quia dolor si. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut loret fugit, sed quia ipsu consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Nulla consequat massa quis enim. 
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
