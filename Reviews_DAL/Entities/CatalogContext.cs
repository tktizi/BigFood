using Microsoft.EntityFrameworkCore;
using Orders.DAL.Bogus;

namespace Catalog_DAL.Entities
{
    public partial class CatalogContext : DbContext
    {
        public CatalogContext()
        {
        }

        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Catalog;Username=your_user;Password=your_password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("products_pkey");

                entity.ToTable("products");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");
                entity.Property(e => e.Price)
                    .HasPrecision(10, 2)
                    .HasColumnName("price");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.HasOne(d => d.Category).WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("products_category_id_fkey");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("categories_pkey");

                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            //var databaseSeeder = new DatabaseSeeder();
            //modelBuilder.Entity<Category>().HasData(databaseSeeder.Categories);
            //modelBuilder.Entity<Product>().HasData(databaseSeeder.Products);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
