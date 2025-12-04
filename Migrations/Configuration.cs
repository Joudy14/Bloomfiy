using Bloomfiy.Models;
using System.Linq; // Add this line
using Bloomfiy.Models;
using System.Data.Entity.Migrations;

namespace Bloomfiy.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            // SET THESE TO TRUE:
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; // OK for development
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // Add seed data here if needed
            // Example:
            if (!context.Categories.Any())
            {
                context.Categories.AddOrUpdate(
                    c => c.CategoryName,
                    new Categories { CategoryName = "Flowers" },
                    new Categories { CategoryName = "Plants" },
                    new Categories { CategoryName = "Bouquets" }
                );
                context.SaveChanges();
            }

            if (!context.Colors.Any())
            {
                context.Colors.AddOrUpdate(
                    c => c.ColorName,
                    new Color { ColorName = "Red", ColorCode = "#FF0000", PriceAdjustment = 0, IsAvailable = true },
                    new Color { ColorName = "Blue", ColorCode = "#0000FF", PriceAdjustment = 5.00m, IsAvailable = true },
                    new Color { ColorName = "Yellow", ColorCode = "#FFFF00", PriceAdjustment = 3.00m, IsAvailable = true },
                    new Color { ColorName = "White", ColorCode = "#FFFFFF", PriceAdjustment = 2.00m, IsAvailable = true },
                    new Color { ColorName = "Pink", ColorCode = "#FFC0CB", PriceAdjustment = 4.00m, IsAvailable = true }
                );
                context.SaveChanges();
            }
        }
    }
}
