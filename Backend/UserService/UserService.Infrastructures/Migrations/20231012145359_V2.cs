using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Payments_OwnerId",
                table: "Payments",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_OwnerId",
                table: "Payments",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_OwnerId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_OwnerId",
                table: "Payments");
        }
    }
}
