using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ComboService.Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class DataForCombo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("4d0939d2-a00e-4e35-88de-212b4686b238"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("74c46a37-8d6e-444b-817d-59caf1e61b9d"));

            migrationBuilder.DeleteData(
                table: "Shop",
                keyColumn: "Id",
                keyValue: new Guid("938e1e63-2757-4ab7-b61f-c55fb5eb906a"));

            migrationBuilder.InsertData(
                table: "Combo",
                columns: new[] { "Id", "ComboName", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Quantity", "ShopId", "Status", "TotalValue" },
                values: new object[] { new Guid("3458fc6a-42b6-4d27-93de-53e40b900670"), "Combo Dự Tiệc Cuối Năm", null, new DateTime(2023, 10, 13, 20, 43, 17, 148, DateTimeKind.Local).AddTicks(6569), null, null, false, null, null, 5, new Guid("a8447a39-91eb-41d6-b872-bdc2ffd89af4"), "Active", 7300000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Combo",
                keyColumn: "Id",
                keyValue: new Guid("3458fc6a-42b6-4d27-93de-53e40b900670"));

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Color", "Compensation", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "Description", "IsDeleted", "Material", "ModificationBy", "ModificationDate", "Price", "ProductName", "RootProductId", "Size", "Status" },
                values: new object[,]
                {
                    { new Guid("4d0939d2-a00e-4e35-88de-212b4686b238"), new Guid("9bfc1f6a-6e19-4444-beb6-81dbe04786bd"), "Red", 400000m, null, new DateTime(2023, 10, 13, 19, 30, 34, 760, DateTimeKind.Local).AddTicks(5935), null, null, "Áo thun siêu mát", false, "Cotton", null, null, 550000m, "Áo thun", new Guid("00000000-0000-0000-0000-000000000000"), "XXL", "Active" },
                    { new Guid("74c46a37-8d6e-444b-817d-59caf1e61b9d"), new Guid("33640b11-e63b-4326-a7ec-df1e9514aa80"), "Black", 700000m, null, new DateTime(2023, 10, 13, 19, 30, 34, 760, DateTimeKind.Local).AddTicks(5958), null, null, "Áo thun siêu nóng", false, "Cotton", null, null, 1000000m, "Áo khoác", new Guid("00000000-0000-0000-0000-000000000000"), "XXL", "Active" }
                });

            migrationBuilder.InsertData(
                table: "Shop",
                columns: new[] { "Id", "Address", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "OwnerId", "ShopCode", "ShopEmail", "ShopName", "ShopPhone", "Status" },
                values: new object[] { new Guid("938e1e63-2757-4ab7-b61f-c55fb5eb906a"), "D1, Thu Duc, HCM", null, new DateTime(2023, 10, 13, 19, 30, 34, 760, DateTimeKind.Local).AddTicks(5772), null, null, false, null, null, new Guid("d6f7b578-1530-43a1-b5a5-7cb02a047381"), "D1FPT0123", "fptstore@gmail.com", "FPT Shop", "0923123123", "Active" });
        }
    }
}
