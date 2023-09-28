using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComboService.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class FixProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_RootProductId",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_RootProductId",
                table: "Product",
                column: "RootProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Product_RootProductId",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Product_RootProductId",
                table: "Product",
                column: "RootProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
