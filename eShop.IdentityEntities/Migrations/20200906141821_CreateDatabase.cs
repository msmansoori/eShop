using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShop.IdentityEntities.Migrations
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
                    PersonalEmail = table.Column<string>(nullable: true),
                    BusinessEmail = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    Line1 = table.Column<string>(nullable: true),
                    Line2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zipcode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
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
                    Token = table.Column<string>(nullable: true),
                    IsExpired = table.Column<bool>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "BusinessEmail", "City", "ContactNumber", "CreatedById", "CreatedOn", "ExternalId", "Line1", "Line2", "ModifiedById", "ModifiedOn", "Name", "Password", "PersonalEmail", "State", "UserType", "Zipcode" },
                values: new object[] { 1L, true, null, "Ahmedabad", "7665856521", null, new DateTime(2020, 9, 6, 14, 18, 21, 251, DateTimeKind.Utc).AddTicks(1265), new Guid("ccf5361b-74a9-4b75-8211-37b26e2d1bc0"), "Test line 1", "test line 2", null, null, "Admin", "admin", "admin@gmail.com", "Gujrat", 1, "314038" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "BusinessEmail", "City", "ContactNumber", "CreatedById", "CreatedOn", "ExternalId", "Line1", "Line2", "ModifiedById", "ModifiedOn", "Name", "Password", "PersonalEmail", "State", "UserType", "Zipcode" },
                values: new object[] { 2L, true, null, "Ahmedabad", "7665856521", null, new DateTime(2020, 9, 6, 14, 18, 21, 251, DateTimeKind.Utc).AddTicks(5728), new Guid("d25f7d72-e475-4d6f-8ca7-aaf189d322c4"), "Test line 1", "test line 2", null, null, "Retailer", "retailer", "retailer@gmail.com", "Gujrat", 3, "314038" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "BusinessEmail", "City", "ContactNumber", "CreatedById", "CreatedOn", "ExternalId", "Line1", "Line2", "ModifiedById", "ModifiedOn", "Name", "Password", "PersonalEmail", "State", "UserType", "Zipcode" },
                values: new object[] { 3L, true, null, "Ahmedabad", "7665856521", null, new DateTime(2020, 9, 6, 14, 18, 21, 251, DateTimeKind.Utc).AddTicks(5823), new Guid("9b887027-fd67-45ba-9ad2-edecca67c1b3"), "Test line 1", "test line 2", null, null, "Customer", "customer", "customer@gmail.com", "Gujrat", 2, "314038" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_CreatedById",
                table: "UserTokens",
                column: "CreatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
