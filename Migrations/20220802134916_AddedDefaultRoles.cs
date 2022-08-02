using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListingAPI.VSCode.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0b5b38f3-4284-44ce-b8dd-0fd1f2112d7e", "eb18b5c1-0c96-4e8e-a0d3-415eb9c6aea9", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2bf1454d-b9b6-48a8-9314-1cf493067adc", "e701e4e3-b6ab-4fa2-8bd5-a14bc6c741d9", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b5b38f3-4284-44ce-b8dd-0fd1f2112d7e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bf1454d-b9b6-48a8-9314-1cf493067adc");
        }
    }
}
