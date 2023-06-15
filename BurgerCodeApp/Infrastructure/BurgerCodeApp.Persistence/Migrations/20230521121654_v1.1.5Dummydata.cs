using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerCodeApp.Data.Migrations
{
    public partial class v115Dummydata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "837e66c1-cff7-4d32-b8b1-778c618547c8", "b5e585fb-0ca1-48b4-9b8a-f006ba5c6a51", "User", "USER" },
                    { "b156b9b6-d3ad-4d06-a052-b1bded7126cf", "a2c0975b-b0aa-40e4-98c4-b646f47d575e", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "182188f2-ba89-460f-9a1f-f3ad6a270c04", 0, "929ceba6-9263-4859-82a5-0447b75f9a89", "john.doe@example.com", true, false, null, "JOHN.DOE@EXAMPLE.COM", "JOHN_DOE", "AQAAAAEAACcQAAAAEP60nZykCyv3kFn3x+A0YDjvqj37I89giWk9Ki5d5A/O5C7Q/gJGctafO2yT2LBZCw==", null, false, "", false, "John_Doe" },
                    { "e7bb73c8-a445-4c5c-9aa9-15cb7d84363c", 0, "7cfc3033-73ed-4de0-9e8b-9f661a9b4431", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEIbZCSMy8gEkX/KM0GNHcrz3xyAGR3PnIngYCXoTYgigDxYPD15GIWSK614ryhYgJQ==", null, false, "", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Delicious hamburgers", "Hamburger" },
                    { 2, "Mouth-watering cheeseburgers", "Cheeseburger" },
                    { 3, "Tasty chicken burgers", "Chicken Burger" },
                    { 4, "Refreshing soft drinks", "Soft Drink" },
                    { 5, "Crispy and golden French fries", "French Fries" }
                });

            migrationBuilder.InsertData(
                table: "Extras",
                columns: new[] { "ExtraId", "Description", "Name", "PicturePath", "Price", "SaleStatus", "Stock" },
                values: new object[,]
                {
                    { 1, "Add extra cheese", "Cheese", null, 1.99m, 0, null },
                    { 2, "Add crispy bacon", "Bacon", null, 2.99m, 0, null },
                    { 3, "Add flavorful guacamole", "Guacamole", null, 1.99m, 0, null },
                    { 4, "Classic tomato ketchup", "Ketchup", null, 0.99m, 0, null },
                    { 5, "Tangy mustard sauce", "Mustard", null, 0.99m, 0, null },
                    { 6, "Creamy mayonnaise", "Mayonnaise", null, 0.99m, 0, null },
                    { 7, "Smoky barbecue sauce", "BBQ Sauce", null, 1.49m, 0, null },
                    { 8, "Creamy ranch dressing", "Ranch Dressing", null, 1.49m, 0, null },
                    { 9, "Spicy buffalo sauce", "Buffalo Sauce", null, 1.49m, 0, null },
                    { 10, "Flavorful garlic aioli sauce", "Garlic Aioli", null, 1.49m, 0, null }
                });

            migrationBuilder.InsertData(
                table: "MenuCategories",
                columns: new[] { "MenuCategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Combo Menus", "Menu" },
                    { 2, "Delicious Burgers", "Burger" },
                    { 3, "Refreshing Beverages", "Drink" },
                    { 4, "Side Items", "Side" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "837e66c1-cff7-4d32-b8b1-778c618547c8", "182188f2-ba89-460f-9a1f-f3ad6a270c04" },
                    { "b156b9b6-d3ad-4d06-a052-b1bded7126cf", "e7bb73c8-a445-4c5c-9aa9-15cb7d84363c" }
                });

            migrationBuilder.InsertData(
                table: "Baskets",
                columns: new[] { "BasketId", "AppUserId", "ComplateDate", "Stage", "TotalPrice" },
                values: new object[,]
                {
                    { 1, "e7bb73c8-a445-4c5c-9aa9-15cb7d84363c", null, 0, 0m },
                    { 2, "182188f2-ba89-460f-9a1f-f3ad6a270c04", null, 0, 0m }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuID", "Description", "MenuCategoryID", "Name", "PicturePath", "Price", "SaleStatus", "Stock" },
                values: new object[,]
                {
                    { 1, "Delicious classic burger with juicy beef patty, lettuce, tomato, and pickles.", 2, "Classic Burger", "/img/fordummy/classic_burger.png", 8.99m, 0, 50 },
                    { 2, "Satisfying combo featuring a cheeseburger, crispy fries, and a refreshing cola.", 1, "Cheeseburger Combo", "/img/fordummy/cheeseburger_combo.png", 12.99m, 0, 30 },
                    { 3, "Tasty chicken burger with grilled chicken breast, lettuce, and mayo.", 2, "Chicken Burger", "/img/fordummy/chicken_burger.png", 9.99m, 0, 40 },
                    { 4, "Flavorful veggie burger made with fresh vegetables and special seasonings.", 2, "Veggie Burger", "/img/fordummy/veggie_burger.png", 7.99m, 0, 20 },
                    { 5, "Crispy and golden french fries, perfect as a side or snack.", 4, "Fries", "/img/fordummy/fries.png", 3.99m, 0, 100 },
                    { 6, "Refreshing cola to quench your thirst.", 3, "Cola", "/img/fordummy/cola.png", 1.99m, 0, 80 },
                    { 7, "Mouthwatering double cheeseburger with two juicy beef patties and melted cheese.", 1, "Double Cheeseburger", "/img/fordummy/double_cheeseburger.png", 10.99m, 0, 25 },
                    { 8, "Delicious fish burger with breaded fish fillet, lettuce, and tartar sauce.", 2, "Fish Burger", "/img/fordummy/fish_burger.png", 8.99m, 0, 35 },
                    { 9, "Satisfying vegan burger made with plant-based patty, fresh veggies, and vegan mayo.", 2, "Vegan Burger", "/img/fordummy/vegan_burger.png", 9.99m, 0, 15 },
                    { 10, "Crunchy and flavorful onion rings, perfect as a side or snack.", 4, "Onion Rings", "/img/fordummy/onion_rings.png", 4.99m, 0, 90 },
                    { 11, "Creamy and delicious milkshake available in various flavors.", 3, "Milkshake", "/img/fordummy/milkshake.png", 3.99m, 0, 70 },
                    { 12, "Tasty burger with BBQ sauce, caramelized onions, and melted cheese.", 1, "BBQ Burger", "/img/fordummy/bbq_burger.png", 9.99m, 0, 30 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryId", "Name", "PicturePath", "Price", "SaleStatus", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "Classic Burger", "/images/classic-burger.jpg", 9.99m, 0, 50 },
                    { 2, 1, "BBQ Burger", "/images/bbq-burger.jpg", 10.99m, 0, 30 },
                    { 3, 1, "Cheeseburger Deluxe", "/images/cheeseburger-deluxe.jpg", 11.99m, 0, 20 },
                    { 4, 2, "Double Cheeseburger", "/images/double-cheeseburger.jpg", 12.99m, 0, 15 },
                    { 5, 2, "Bacon Cheeseburger", "/images/bacon-cheeseburger.jpg", 11.99m, 0, 25 },
                    { 6, 2, "Mushroom Swiss Burger", "/images/mushroom-swiss-burger.jpg", 11.99m, 0, 18 },
                    { 7, 3, "Crispy Chicken Burger", "/images/crispy-chicken-burger.jpg", 9.99m, 0, 40 },
                    { 8, 3, "Spicy Chicken Burger", "/images/spicy-chicken-burger.jpg", 10.99m, 0, 35 },
                    { 9, 3, "Grilled Chicken Burger", "/images/grilled-chicken-burger.jpg", 10.99m, 0, 30 },
                    { 10, 4, "Coca-Cola", "/images/coca-cola.jpg", 2.99m, 0, 100 },
                    { 11, 4, "Sprite", "/images/sprite.jpg", 2.99m, 0, 90 },
                    { 12, 4, "Fanta", "/images/fanta.jpg", 2.99m, 0, 80 },
                    { 13, 5, "Regular French Fries", "/images/regular-french-fries.jpg", 3.99m, 0, 60 },
                    { 14, 5, "Curly French Fries", "/images/curly-french-fries.jpg", 4.99m, 0, 50 },
                    { 15, 5, "Sweet Potato Fries", "/images/sweet-potato-fries.jpg", 4.99m, 0, 45 }
                });

            migrationBuilder.InsertData(
                table: "MenuDetails",
                columns: new[] { "MenuId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 8.99m },
                    { 2, 2, 1, 12.99m },
                    { 2, 3, 1, 3.99m },
                    { 3, 4, 1, 9.99m },
                    { 4, 5, 1, 7.99m },
                    { 5, 6, 1, 3.99m },
                    { 6, 7, 1, 1.99m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "837e66c1-cff7-4d32-b8b1-778c618547c8", "182188f2-ba89-460f-9a1f-f3ad6a270c04" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b156b9b6-d3ad-4d06-a052-b1bded7126cf", "e7bb73c8-a445-4c5c-9aa9-15cb7d84363c" });

            migrationBuilder.DeleteData(
                table: "Baskets",
                keyColumn: "BasketId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Baskets",
                keyColumn: "BasketId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Extras",
                keyColumn: "ExtraId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MenuDetails",
                keyColumns: new[] { "MenuId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "MenuDetails",
                keyColumns: new[] { "MenuId", "ProductId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "MenuDetails",
                keyColumns: new[] { "MenuId", "ProductId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "MenuDetails",
                keyColumns: new[] { "MenuId", "ProductId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "MenuDetails",
                keyColumns: new[] { "MenuId", "ProductId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "MenuDetails",
                keyColumns: new[] { "MenuId", "ProductId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "MenuDetails",
                keyColumns: new[] { "MenuId", "ProductId" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "837e66c1-cff7-4d32-b8b1-778c618547c8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b156b9b6-d3ad-4d06-a052-b1bded7126cf");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "182188f2-ba89-460f-9a1f-f3ad6a270c04");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e7bb73c8-a445-4c5c-9aa9-15cb7d84363c");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "MenuCategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "MenuCategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "MenuCategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "MenuCategoryId",
                keyValue: 4);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
