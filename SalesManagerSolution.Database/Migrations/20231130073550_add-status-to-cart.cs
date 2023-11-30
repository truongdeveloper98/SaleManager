using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManagerSolution.Database.Migrations
{
    /// <inheritdoc />
    public partial class addstatustocart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartStatus",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "369cd2f4-0485-4518-b706-7d0824919e30", "AQAAAAIAAYagAAAAEFLtwxejaMqJM//ZBZQ0KsEWAY0qIXkvSGQH2NO0LWF6RUaxDXXz455QWhux6m1VJQ==", "2d4508f1-ac19-4798-9ff4-f7089a6e5872" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c163a6f-2770-490e-990e-c50327882f1b", "AQAAAAIAAYagAAAAEEwuICFbMPp/k7ZXYXBEZOMe1xxa2RBEpHpK1Lmg3wXYbVXpEAiAzp3qOP8Ff+atDg==", "9be79648-6fe8-4606-aa96-e36a26d4385c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartStatus",
                table: "Carts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a373d7f8-72c0-4087-8735-cdee16418b5b", "AQAAAAIAAYagAAAAEOASNfNbzjCWVvx0Wpu7+Vd3H2en5o3wQ0Wdkd77a/hZ+nhNPM3WWzigCH+rp9IcNQ==", "df6c46fc-c77e-410e-bae2-ab41cdc50b63" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "86cf5109-1ea6-4095-b93c-9b27ec272e96", "AQAAAAIAAYagAAAAEHufJTgbJoa1XfVAD0wPsMiFhkrk0vyn3V4M2hFtv+4APPH9EScRg0ldlMDFI+Mdzw==", "1bda180a-b199-44e8-8a5a-4db550b436d7" });
        }
    }
}
