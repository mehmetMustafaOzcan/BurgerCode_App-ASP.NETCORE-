using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerCodeApp.Data.Migrations
{
    public partial class v101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BasketDetails",
                table: "BasketDetails");

            migrationBuilder.CreateIndex(
                name: "IX_BasketDetails_BasketId",
                table: "BasketDetails",
                column: "BasketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BasketDetails_BasketId",
                table: "BasketDetails");

            migrationBuilder.CreateIndex(
                name: "IX_BasketDetails",
                table: "BasketDetails",
                columns: new[] { "BasketId", "MenuId" },
                unique: true);
        }
    }
}
