using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComboService.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class FixDbInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceList_Combo_PriceListId",
                table: "PriceList");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCombo_Combo_ComboId",
                table: "ProductCombo");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "ShopLogo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceList",
                table: "PriceList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Combo",
                table: "Combo");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Shop");

            migrationBuilder.DropColumn(
                name: "ProductComboId",
                table: "ProductCombo");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                table: "PriceList");

            migrationBuilder.DropColumn(
                name: "ComboId",
                table: "Combo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceList",
                table: "PriceList",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Combo",
                table: "Combo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceList_Combo_Id",
                table: "PriceList",
                column: "Id",
                principalTable: "Combo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCombo_Combo_ComboId",
                table: "ProductCombo",
                column: "ComboId",
                principalTable: "Combo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceList_Combo_Id",
                table: "PriceList");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCombo_Combo_ComboId",
                table: "ProductCombo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceList",
                table: "PriceList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Combo",
                table: "Combo");

            migrationBuilder.AddColumn<Guid>(
                name: "ShopId",
                table: "Shop",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductComboId",
                table: "ProductCombo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PriceListId",
                table: "PriceList",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ComboId",
                table: "Combo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceList",
                table: "PriceList",
                column: "PriceListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Combo",
                table: "Combo",
                column: "ComboId");

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModificationBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopLogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModificationBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopLogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopLogo_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopLogo_ShopId",
                table: "ShopLogo",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceList_Combo_PriceListId",
                table: "PriceList",
                column: "PriceListId",
                principalTable: "Combo",
                principalColumn: "ComboId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCombo_Combo_ComboId",
                table: "ProductCombo",
                column: "ComboId",
                principalTable: "Combo",
                principalColumn: "ComboId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
