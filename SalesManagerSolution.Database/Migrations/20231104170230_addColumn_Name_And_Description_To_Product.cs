using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManagerSolution.Database.Migrations
{
    /// <inheritdoc />
    public partial class addColumn_Name_And_Description_To_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7905e44b-f15a-4ac3-a468-8ff6aeec035e", "AQAAAAIAAYagAAAAELmam8aXovvKV5P/qWnztgkunckFMUF1cdgAwQuqfM/Wx0VRLekItVkQ107bGVV2Tg==", "ce3d7049-17d5-42b7-bbac-da891bca5683" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60dcccd3-fed8-4cbb-9e8f-1722d0a6345e", "AQAAAAIAAYagAAAAEBdsKPCQrxsTmTJFTuFPbsJ7uQ9yiMKLSrfTXwab5+w3IlLFn+n3TgkVcE0UJq1xtg==", "e96dd7bd-987a-441a-adbf-7e6061113e4c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Carts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e29013e9-e056-4064-968b-a76c88bf7af0", "AQAAAAIAAYagAAAAEHtPfDTBVgQvoQKesgYT0EAGzR0cropCqBHSQr+l8u1rOJBnVfieU+cDmunhoDlX4g==", "09917e41-259f-4d77-999a-e73cfd1c8c3f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e97be48-921d-4c3f-8d8e-411e799fda7c", "AQAAAAIAAYagAAAAEPdt1uNylk3X6swEl8mS9NkhLVIhdl8ISJ9fqb7Fv5OT2vbPDYikiLhAVxF+YQiIJw==", "32ff90cb-dcf2-46d3-90e0-2a5243d7fd92" });
        }
    }
}
