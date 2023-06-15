using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerCodeApp.Data.Migrations
{
    public partial class v103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraDetails_BasketDetails",
                table: "ExtraDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraDetails_BasketDetails",
                table: "ExtraDetails",
                column: "BasketDetailID",
                principalTable: "BasketDetails",
                principalColumn: "BasketDetailID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraDetails_BasketDetails",
                table: "ExtraDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraDetails_BasketDetails",
                table: "ExtraDetails",
                column: "BasketDetailID",
                principalTable: "BasketDetails",
                principalColumn: "BasketDetailID");
        }
    }
}
