using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComboService.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Combo_Shop_ShopId",
                table: "Combo");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceList_Combo_ComboId",
                table: "PriceList");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_RootProductId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Shop_ShopId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCombo_Combo_ComboId",
                table: "ProductCombo");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCombo_Product_ProductId",
                table: "ProductCombo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shop",
                table: "Shop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCombo",
                table: "ProductCombo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceList",
                table: "PriceList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Combo",
                table: "Combo");

            migrationBuilder.RenameTable(
                name: "Shop",
                newName: "Shops");

            migrationBuilder.RenameTable(
                name: "ProductCombo",
                newName: "ProductCombos");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "PriceList",
                newName: "PriceLists");

            migrationBuilder.RenameTable(
                name: "Combo",
                newName: "Combos");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCombo_ProductId",
                table: "ProductCombos",
                newName: "IX_ProductCombos_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCombo_ComboId",
                table: "ProductCombos",
                newName: "IX_ProductCombos_ComboId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ShopId",
                table: "Products",
                newName: "IX_Products_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_RootProductId",
                table: "Products",
                newName: "IX_Products_RootProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PriceList_ComboId",
                table: "PriceLists",
                newName: "IX_PriceLists_ComboId");

            migrationBuilder.RenameIndex(
                name: "IX_Combo_ShopId",
                table: "Combos",
                newName: "IX_Combos_ShopId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shops",
                table: "Shops",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCombos",
                table: "ProductCombos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceLists",
                table: "PriceLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Combos",
                table: "Combos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModificationBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Combos_Shops_ShopId",
                table: "Combos",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceLists_Combos_ComboId",
                table: "PriceLists",
                column: "ComboId",
                principalTable: "Combos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCombos_Combos_ComboId",
                table: "ProductCombos",
                column: "ComboId",
                principalTable: "Combos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCombos_Products_ProductId",
                table: "ProductCombos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Products_RootProductId",
                table: "Products",
                column: "RootProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Combos_Shops_ShopId",
                table: "Combos");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceLists_Combos_ComboId",
                table: "PriceLists");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCombos_Combos_ComboId",
                table: "ProductCombos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCombos_Products_ProductId",
                table: "ProductCombos");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Products_RootProductId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shops_ShopId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shops",
                table: "Shops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCombos",
                table: "ProductCombos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceLists",
                table: "PriceLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Combos",
                table: "Combos");

            migrationBuilder.RenameTable(
                name: "Shops",
                newName: "Shop");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "ProductCombos",
                newName: "ProductCombo");

            migrationBuilder.RenameTable(
                name: "PriceLists",
                newName: "PriceList");

            migrationBuilder.RenameTable(
                name: "Combos",
                newName: "Combo");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ShopId",
                table: "Product",
                newName: "IX_Product_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_RootProductId",
                table: "Product",
                newName: "IX_Product_RootProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCombos_ProductId",
                table: "ProductCombo",
                newName: "IX_ProductCombo_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCombos_ComboId",
                table: "ProductCombo",
                newName: "IX_ProductCombo_ComboId");

            migrationBuilder.RenameIndex(
                name: "IX_PriceLists_ComboId",
                table: "PriceList",
                newName: "IX_PriceList_ComboId");

            migrationBuilder.RenameIndex(
                name: "IX_Combos_ShopId",
                table: "Combo",
                newName: "IX_Combo_ShopId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shop",
                table: "Shop",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCombo",
                table: "ProductCombo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceList",
                table: "PriceList",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Combo",
                table: "Combo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Combo_Shop_ShopId",
                table: "Combo",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceList_Combo_ComboId",
                table: "PriceList",
                column: "ComboId",
                principalTable: "Combo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_RootProductId",
                table: "Product",
                column: "RootProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Shop_ShopId",
                table: "Product",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCombo_Combo_ComboId",
                table: "ProductCombo",
                column: "ComboId",
                principalTable: "Combo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCombo_Product_ProductId",
                table: "ProductCombo",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
