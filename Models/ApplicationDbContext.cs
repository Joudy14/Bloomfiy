using System.Data.Entity;

namespace Bloomfiy.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("BloomfiyConnection")
        {
            // Disable code first migrations since we have existing database
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }

        // Remove OnModelCreating completely for now
        // protected override void OnModelCreating(DbModelBuilder modelBuilder)
        // {
        // }
    }
}