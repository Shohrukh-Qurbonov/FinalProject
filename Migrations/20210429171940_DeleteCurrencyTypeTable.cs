using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class DeleteCurrencyTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Home_AspNetUsers_UserId",
                table: "Home");

            migrationBuilder.DropForeignKey(
                name: "FK_Home_Category_CategoryId",
                table: "Home");

            migrationBuilder.DropForeignKey(
                name: "FK_Home_Cities_CityId",
                table: "Home");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeImage_Home_HomeId",
                table: "HomeImage");

            migrationBuilder.DropTable(
                name: "CurrencyType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeImage",
                table: "HomeImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Home",
                table: "Home");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "HomeImage",
                newName: "HomeImages");

            migrationBuilder.RenameTable(
                name: "Home",
                newName: "Homes");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_HomeImage_HomeId",
                table: "HomeImages",
                newName: "IX_HomeImages_HomeId");

            migrationBuilder.RenameIndex(
                name: "IX_Home_UserId",
                table: "Homes",
                newName: "IX_Homes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Home_CityId",
                table: "Homes",
                newName: "IX_Homes_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Home_CategoryId",
                table: "Homes",
                newName: "IX_Homes_CategoryId");

            migrationBuilder.AddColumn<double>(
                name: "YearlyPercent",
                table: "Homes",
                type: "float",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeImages",
                table: "HomeImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Homes",
                table: "Homes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FIO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeId = table.Column<int>(type: "int", nullable: false),
                    Aim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditTerm = table.Column<int>(type: "int", nullable: true),
                    CreditSumm = table.Column<double>(type: "float", nullable: true),
                    Prepayment = table.Column<double>(type: "float", nullable: true),
                    CurrencyTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_HomeImages_Homes_HomeId",
                table: "HomeImages",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_AspNetUsers_UserId",
                table: "Homes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_Categories_CategoryId",
                table: "Homes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_Cities_CityId",
                table: "Homes",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeImages_Homes_HomeId",
                table: "HomeImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Homes_AspNetUsers_UserId",
                table: "Homes");

            migrationBuilder.DropForeignKey(
                name: "FK_Homes_Categories_CategoryId",
                table: "Homes");

            migrationBuilder.DropForeignKey(
                name: "FK_Homes_Cities_CityId",
                table: "Homes");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Homes",
                table: "Homes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeImages",
                table: "HomeImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "YearlyPercent",
                table: "Homes");

            migrationBuilder.RenameTable(
                name: "Homes",
                newName: "Home");

            migrationBuilder.RenameTable(
                name: "HomeImages",
                newName: "HomeImage");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Homes_UserId",
                table: "Home",
                newName: "IX_Home_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Homes_CityId",
                table: "Home",
                newName: "IX_Home_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Homes_CategoryId",
                table: "Home",
                newName: "IX_Home_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_HomeImages_HomeId",
                table: "HomeImage",
                newName: "IX_HomeImage_HomeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Home",
                table: "Home",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeImage",
                table: "HomeImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CurrencyType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeId = table.Column<int>(type: "int", nullable: false),
                    YearlyPercent = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyType_Home_HomeId",
                        column: x => x.HomeId,
                        principalTable: "Home",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyType_HomeId",
                table: "CurrencyType",
                column: "HomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Home_AspNetUsers_UserId",
                table: "Home",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Home_Category_CategoryId",
                table: "Home",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Home_Cities_CityId",
                table: "Home",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeImage_Home_HomeId",
                table: "HomeImage",
                column: "HomeId",
                principalTable: "Home",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
