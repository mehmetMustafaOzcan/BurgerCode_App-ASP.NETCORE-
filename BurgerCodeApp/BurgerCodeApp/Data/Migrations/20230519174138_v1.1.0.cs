using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerCodeApp.Data.Migrations
{
    public partial class v110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenüPrice",
                table: "Menus");

            migrationBuilder.RenameColumn(
                name: "Photopath",
                table: "Menus",
                newName: "PicturePath");

            migrationBuilder.RenameColumn(
                name: "MenuName",
                table: "Menus",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SaleStatus",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Menus",
                type: "decimal(18,2)",
                maxLength: 50,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Menus",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Extras",
                type: "money",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "Extras",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Extras",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleStatus",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Extras");

            migrationBuilder.RenameColumn(
                name: "PicturePath",
                table: "Menus",
                newName: "Photopath");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Menus",
                newName: "MenuName");

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MenüPrice",
                table: "Menus",
                type: "decimal(18,2)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Extras",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");
        }
    }
}
