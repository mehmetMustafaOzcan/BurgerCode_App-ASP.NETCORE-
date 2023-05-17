using BurgerCodeApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BurgerCodeApp.Data
{
    public class BurgerDbContext : IdentityDbContext<IdentityUser>
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
                optionsBuilder.UseSqlServer("Server=.\\;Database=BurgerCode_DB;Trusted_Connection=True;");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BasketDetails_Baskets");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.BasketDetails)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BasketDetails_Menus");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CateogryId);

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

                entity.Property(e => e.MenuName).HasMaxLength(50);

                entity.Property(e => e.MenüPrice).HasMaxLength(50);

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

            base.OnModelCreating(modelBuilder);
        }

      
    }


}





