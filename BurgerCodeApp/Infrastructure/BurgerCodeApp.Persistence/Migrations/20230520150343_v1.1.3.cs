using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerCodeApp.Data.Migrations
{
    public partial class v113 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7d00b31a-7ce8-44c5-8438-fa0143b6eb07", "ee5e7e9b-8d64-468a-bce5-4ea24fcec4af", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b2a3773d-2392-4df1-933f-9ecc63ad2c71", "50caed70-4891-4de6-9ee5-3f84d13c986f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dec634ab-9129-4bba-959e-5180c05cf941", "be8f0706-63e6-446b-836d-f40f023deaa8", "Editor", "EDITOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d00b31a-7ce8-44c5-8438-fa0143b6eb07");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2a3773d-2392-4df1-933f-9ecc63ad2c71");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dec634ab-9129-4bba-959e-5180c05cf941");
        }
    }
}
