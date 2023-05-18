using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerCodeApp.Data.Migrations
{
    public partial class v106 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketDetails_Baskets",
                table: "BasketDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketDetails_Baskets",
                table: "BasketDetails",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "BasketId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketDetails_Baskets",
                table: "BasketDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketDetails_Baskets",
                table: "BasketDetails",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "BasketId");
        }
    }
}
