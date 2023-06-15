using BurgerCodeApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BurgerCodeApp.Persistence.Context
{
    public class BurgerDbContext : IdentityDbContext<AppUser>
    {
        public BurgerDbContext(DbContextOptions<BurgerDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Basket> Baskets { get; set; } = null!;
        public virtual DbSet<BasketDetail> BasketDetails { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Extra> Extras { get; set; } = null!;
        public virtual DbSet<ExtraDetail> ExtraDetails { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<MenuCategory> MenuCategories { get; set; } = null!;
        public virtual DbSet<MenuDetail> MenuDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\;Database=BurgerCode_DBdummy;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasIndex(e => e.AppUserId, "IX_Baskets_AppUserId");
            });

            modelBuilder.Entity<BasketDetail>(entity =>
            {


                entity.HasIndex(e => e.MenuId, "IX_BasketDetails_MenuId");

                entity.Property(e => e.BasketDetailId).HasColumnName("BasketDetailID");

                entity.HasOne(d => d.Basket)
                    .WithMany(p => p.BasketDetails)
                    .HasForeignKey(d => d.BasketId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_BasketDetails_Baskets");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.BasketDetails)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BasketDetails_Menus");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Extra>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<ExtraDetail>(entity =>
            {
                entity.HasKey(e => new { e.BasketDetailId, e.ExtraId });

                entity.HasIndex(e => e.ExtraId, "IX_ExtraDetails_ExtraId");

                entity.Property(e => e.BasketDetailId).HasColumnName("BasketDetailID");

                entity.HasOne(d => d.BasketDetail)
                    .WithMany(p => p.ExtraDetails)
                    .HasForeignKey(d => d.BasketDetailId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ExtraDetails_BasketDetails");

                entity.HasOne(d => d.Extra)
                    .WithMany(p => p.ExtraDetails)
                    .HasForeignKey(d => d.ExtraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExtraDetails_Extras");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasIndex(e => e.MenuCategoryId, "IX_Menus_MenuCategoryID");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.MenuCategoryId).HasColumnName("MenuCategoryID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasMaxLength(50);

                entity.HasOne(d => d.MenuCategory)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.MenuCategoryId)
                    .HasConstraintName("FK_Menus_MenuCategories");
            });

            modelBuilder.Entity<MenuCategory>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<MenuDetail>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.ProductId });

                entity.HasIndex(e => e.ProductId, "IX_MenuDetails_ProductId");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuDetails)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuDetails_Menus");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.MenuDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuDetails_Products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PicturePath).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");
            });
            modelBuilder.Entity<AppUser>()
            .HasIndex(e => e.Email)
            .IsUnique();

            // modelBuilder.Entity<IdentityRole>().HasData(
            //    new IdentityRole()
            //    {
            //        Name = "Admin",
            //        NormalizedName = "ADMIN",
            //    },
            //    new IdentityRole()
            //    {
            //        Name = "Editor",
            //        NormalizedName = "EDITOR",
            //    },
            //    new IdentityRole()
            //    {
            //        Name = "User",
            //        NormalizedName = "USER",
            //    }
            //);

            base.OnModelCreating(modelBuilder);
            AddDumyUser(modelBuilder);
            AddDumyData(modelBuilder);
        }
        public void AddDumyUser(ModelBuilder modelBuilder)
        {
            var adminRoleId = Guid.NewGuid().ToString();
            var userRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            var hasher = new PasswordHasher<AppUser>();

            var adminUserId = Guid.NewGuid().ToString();
            var userUserId = Guid.NewGuid().ToString();

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = adminUserId,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "@dmiN!"),
                    SecurityStamp = string.Empty,
                    

                },
                new AppUser
                {
                    Id = userUserId,
                    UserName = "John_Doe",
                    NormalizedUserName = "JOHN_DOE",
                    Email = "john.doe@example.com",
                    NormalizedEmail = "JOHN.DOE@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "P@ssword!"),
                    SecurityStamp = string.Empty
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = userUserId,
                    RoleId = userRoleId
                }
            );

            //basketleri
            modelBuilder.Entity<Basket>().HasData(
    new Basket
    {
        BasketId = 1,
        AppUserId = adminUserId,
        Stage=0
    },
    new Basket
    {
        BasketId = 2,
        AppUserId = userUserId,
        Stage=0
    }
);


        }
        public void AddDumyData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
    new Category { CategoryId = 1, Name = "Hamburger", Description = "Delicious hamburgers" },
    new Category { CategoryId = 2, Name = "Cheeseburger", Description = "Mouth-watering cheeseburgers" },
    new Category { CategoryId = 3, Name = "Chicken Burger", Description = "Tasty chicken burgers" },
    new Category { CategoryId = 4, Name = "Soft Drink", Description = "Refreshing soft drinks" },
    new Category { CategoryId = 5, Name = "French Fries", Description = "Crispy and golden French fries" }
);
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, CategoryId = 1, Name = "Classic Burger", Price = 9.99m, PicturePath = "/images/classic-burger.jpg", Stock = 50 },
                new Product { ProductId = 2, CategoryId = 1, Name = "BBQ Burger", Price = 10.99m, PicturePath = "/images/bbq-burger.jpg", Stock = 30 },
                new Product { ProductId = 3, CategoryId = 1, Name = "Cheeseburger Deluxe", Price = 11.99m, PicturePath = "/images/cheeseburger-deluxe.jpg", Stock = 20 },
                new Product { ProductId = 4, CategoryId = 2, Name = "Double Cheeseburger", Price = 12.99m, PicturePath = "/images/double-cheeseburger.jpg", Stock = 15 },
                new Product { ProductId = 5, CategoryId = 2, Name = "Bacon Cheeseburger", Price = 11.99m, PicturePath = "/images/bacon-cheeseburger.jpg", Stock = 25 },
                new Product { ProductId = 6, CategoryId = 2, Name = "Mushroom Swiss Burger", Price = 11.99m, PicturePath = "/images/mushroom-swiss-burger.jpg", Stock = 18 },
                new Product { ProductId = 7, CategoryId = 3, Name = "Crispy Chicken Burger", Price = 9.99m, PicturePath = "/images/crispy-chicken-burger.jpg", Stock = 40 },
                new Product { ProductId = 8, CategoryId = 3, Name = "Spicy Chicken Burger", Price = 10.99m, PicturePath = "/images/spicy-chicken-burger.jpg", Stock = 35 },
                new Product { ProductId = 9, CategoryId = 3, Name = "Grilled Chicken Burger", Price = 10.99m, PicturePath = "/images/grilled-chicken-burger.jpg", Stock = 30 },
                new Product { ProductId = 10, CategoryId = 4, Name = "Coca-Cola", Price = 2.99m, PicturePath = "/images/coca-cola.jpg", Stock = 100 },
                new Product { ProductId = 11, CategoryId = 4, Name = "Sprite", Price = 2.99m, PicturePath = "/images/sprite.jpg", Stock = 90 },
                new Product { ProductId = 12, CategoryId = 4, Name = "Fanta", Price = 2.99m, PicturePath = "/images/fanta.jpg", Stock = 80 },
                new Product { ProductId = 13, CategoryId = 5, Name = "Regular French Fries", Price = 3.99m, PicturePath = "/images/regular-french-fries.jpg", Stock = 60 },
                new Product { ProductId = 14, CategoryId = 5, Name = "Curly French Fries", Price = 4.99m, PicturePath = "/images/curly-french-fries.jpg", Stock = 50 },
                new Product { ProductId = 15, CategoryId = 5, Name = "Sweet Potato Fries", Price = 4.99m, PicturePath = "/images/sweet-potato-fries.jpg", Stock = 45 }
            );
            modelBuilder.Entity<MenuCategory>().HasData(
    new MenuCategory { MenuCategoryId = 1, Name = "Menu", Description = "Combo Menus" },
    new MenuCategory { MenuCategoryId = 2, Name = "Burger", Description = "Delicious Burgers" },
    new MenuCategory { MenuCategoryId = 3, Name = "Drink", Description = "Refreshing Beverages" },
    new MenuCategory { MenuCategoryId = 4, Name = "Side", Description = "Side Items" }
);
            modelBuilder.Entity<Menu>().HasData(
     new Menu { MenuId = 1, Name = "Classic Burger", Description = "Delicious classic burger with juicy beef patty, lettuce, tomato, and pickles.", MenuCategoryId = 2, PicturePath = "/img/fordummy/classic_burger.png", Price = 8.99m, Stock = 50, SaleStatus = 0 },
     new Menu { MenuId = 2, Name = "Cheeseburger Combo", Description = "Satisfying combo featuring a cheeseburger, crispy fries, and a refreshing cola.", MenuCategoryId = 1, PicturePath = "/img/fordummy/cheeseburger_combo.png", Price = 12.99m, Stock = 30, SaleStatus = 0 },
     new Menu { MenuId = 3, Name = "Chicken Burger", Description = "Tasty chicken burger with grilled chicken breast, lettuce, and mayo.", MenuCategoryId = 2, PicturePath = "/img/fordummy/chicken_burger.png", Price = 9.99m, Stock = 40, SaleStatus = 0 },
     new Menu { MenuId = 4, Name = "Veggie Burger", Description = "Flavorful veggie burger made with fresh vegetables and special seasonings.", MenuCategoryId = 2, PicturePath = "/img/fordummy/veggie_burger.png", Price = 7.99m, Stock = 20, SaleStatus = 0 },
     new Menu { MenuId = 5, Name = "Fries", Description = "Crispy and golden french fries, perfect as a side or snack.", MenuCategoryId = 4, PicturePath = "/img/fordummy/fries.png", Price = 3.99m, Stock = 100, SaleStatus = 0 },
     new Menu { MenuId = 6, Name = "Cola", Description = "Refreshing cola to quench your thirst.", MenuCategoryId = 3, PicturePath = "/img/fordummy/cola.png", Price = 1.99m, Stock = 80, SaleStatus = 0 },
     new Menu { MenuId = 7, Name = "Double Cheeseburger", Description = "Mouthwatering double cheeseburger with two juicy beef patties and melted cheese.", MenuCategoryId = 1, PicturePath = "/img/fordummy/double_cheeseburger.png", Price = 10.99m, Stock = 25, SaleStatus = 0 },
     new Menu { MenuId = 8, Name = "Fish Burger", Description = "Delicious fish burger with breaded fish fillet, lettuce, and tartar sauce.", MenuCategoryId = 2, PicturePath = "/img/fordummy/fish_burger.png", Price = 8.99m, Stock = 35, SaleStatus = 0 },
     new Menu { MenuId = 9, Name = "Vegan Burger", Description = "Satisfying vegan burger made with plant-based patty, fresh veggies, and vegan mayo.", MenuCategoryId = 2, PicturePath = "/img/fordummy/vegan_burger.png", Price = 9.99m, Stock = 15, SaleStatus = 0 },
     new Menu { MenuId = 10, Name = "Onion Rings", Description = "Crunchy and flavorful onion rings, perfect as a side or snack.", MenuCategoryId = 4, PicturePath = "/img/fordummy/onion_rings.png", Price = 4.99m, Stock = 90, SaleStatus = 0 },
     new Menu { MenuId = 11, Name = "Milkshake", Description = "Creamy and delicious milkshake available in various flavors.", MenuCategoryId = 3, PicturePath = "/img/fordummy/milkshake.png", Price = 3.99m, Stock = 70, SaleStatus = 0 },
     new Menu { MenuId = 12, Name = "BBQ Burger", Description = "Tasty burger with BBQ sauce, caramelized onions, and melted cheese.", MenuCategoryId = 1, PicturePath = "/img/fordummy/bbq_burger.png", Price = 9.99m, Stock = 30, SaleStatus = 0 }
 );



            modelBuilder.Entity<MenuDetail>().HasData(
                new MenuDetail { MenuId = 1, ProductId = 1, Quantity = 1, UnitPrice = 8.99m },
                new MenuDetail { MenuId = 2, ProductId = 2, Quantity = 1, UnitPrice = 12.99m },
                new MenuDetail { MenuId = 2, ProductId = 3, Quantity = 1, UnitPrice = 3.99m },
                new MenuDetail { MenuId = 3, ProductId = 4, Quantity = 1, UnitPrice = 9.99m },
                new MenuDetail { MenuId = 4, ProductId = 5, Quantity = 1, UnitPrice = 7.99m },
                new MenuDetail { MenuId = 5, ProductId = 6, Quantity = 1, UnitPrice = 3.99m },
                new MenuDetail { MenuId = 6, ProductId = 7, Quantity = 1, UnitPrice = 1.99m }
            );

            modelBuilder.Entity<Extra>().HasData(
      new Extra { ExtraId = 1, Name = "Cheese", Description = "Add extra cheese", Price = 1.99m, SaleStatus = 0 },
      new Extra { ExtraId = 2, Name = "Bacon", Description = "Add crispy bacon", Price = 2.99m, SaleStatus = 0 },
      new Extra { ExtraId = 3, Name = "Guacamole", Description = "Add flavorful guacamole", Price = 1.99m, SaleStatus = 0 },
      new Extra { ExtraId = 4, Name = "Ketchup", Description = "Classic tomato ketchup", Price = 0.99m, SaleStatus = 0 },
      new Extra { ExtraId = 5, Name = "Mustard", Description = "Tangy mustard sauce", Price = 0.99m, SaleStatus = 0 },
      new Extra { ExtraId = 6, Name = "Mayonnaise", Description = "Creamy mayonnaise", Price = 0.99m, SaleStatus = 0 },
      new Extra { ExtraId = 7, Name = "BBQ Sauce", Description = "Smoky barbecue sauce", Price = 1.49m, SaleStatus = 0 },
      new Extra { ExtraId = 8, Name = "Ranch Dressing", Description = "Creamy ranch dressing", Price = 1.49m, SaleStatus = 0 },
      new Extra { ExtraId = 9, Name = "Buffalo Sauce", Description = "Spicy buffalo sauce", Price = 1.49m, SaleStatus = 0 },
      new Extra { ExtraId = 10, Name = "Garlic Aioli", Description = "Flavorful garlic aioli sauce", Price = 1.49m, SaleStatus = 0 }
  );



        }
    }


}





