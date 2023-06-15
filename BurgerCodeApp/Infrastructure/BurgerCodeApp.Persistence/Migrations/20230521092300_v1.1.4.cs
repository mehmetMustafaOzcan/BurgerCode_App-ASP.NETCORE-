using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerCodeApp.Data.Migrations
{
    public partial class v114 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "CateogryId",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2277d729-2126-4e60-9219-0edb2b5c7afd", "7e4bd7d7-162f-4de2-8dce-6e2367029228", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b7a25ab4-3967-40c3-a952-a83898eb366d", "d47cbec7-f070-4e2d-845d-ec73a0e79cfa", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c2ad1167-50d2-4d8d-8978-a7992283a686", "9aac848b-3154-4c71-9277-1dd4b40abacf", "Editor", "EDITOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2277d729-2126-4e60-9219-0edb2b5c7afd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7a25ab4-3967-40c3-a952-a83898eb366d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2ad1167-50d2-4d8d-8978-a7992283a686");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "CateogryId");

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
    }
}
