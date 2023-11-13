using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Fees_FeeId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_FeeId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "FeeId",
                table: "OrderDetails");

            migrationBuilder.AddColumn<double>(
                name: "Deposit",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RentalPrice",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deposit",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "RentalPrice",
                table: "OrderDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "FeeId",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_FeeId",
                table: "OrderDetails",
                column: "FeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Fees_FeeId",
                table: "OrderDetails",
                column: "FeeId",
                principalTable: "Fees",
                principalColumn: "Id");
        }
    }
}
