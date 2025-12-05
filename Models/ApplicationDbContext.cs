// Models/ApplicationDbContext.cs
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Bloomfiy.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("BloomfiyConnection")
        {
            // Optional: Disable lazy loading for better performance
            // this.Configuration.LazyLoadingEnabled = false;

        }
        public DbSet<User> Users { get; set; }

        // DbSet properties for each entity
        public DbSet<Product> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Remove pluralizing table names (optional)
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Configure relationships if needed
            // ProductColor has composite key (optional)
            modelBuilder.Entity<ProductColor>()
                .HasKey(pc => pc.Id);

            modelBuilder.Entity<ProductColor>()
                .HasRequired(pc => pc.Product)
                .WithMany(p => p.ProductColors)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductColor>()
                .HasRequired(pc => pc.Color)
                .WithMany(c => c.ProductColors)
                .HasForeignKey(pc => pc.ColorId);
        }
    }
}