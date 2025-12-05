using System.Linq;
using System.Data.Entity.Migrations;
using Bloomfiy.Models;

namespace Bloomfiy.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // Seed Categories
            context.Categories.AddOrUpdate(
                c => c.CategoryName,
                new Categories { CategoryName = "Flowers" },
                new Categories { CategoryName = "Plants" },
                new Categories { CategoryName = "Bouquets" }
            );

            // Seed Colors
            context.Colors.AddOrUpdate(
                c => c.ColorName,
                new Color { ColorName = "Red", ColorCode = "#FF0000", PriceAdjustment = 0, IsAvailable = true },
                new Color { ColorName = "Blue", ColorCode = "#0000FF", PriceAdjustment = 5.00m, IsAvailable = true },
                new Color { ColorName = "Yellow", ColorCode = "#FFFF00", PriceAdjustment = 3.00m, IsAvailable = true },
                new Color { ColorName = "White", ColorCode = "#FFFFFF", PriceAdjustment = 2.00m, IsAvailable = true },
                new Color { ColorName = "Pink", ColorCode = "#FFC0CB", PriceAdjustment = 4.00m, IsAvailable = true }
            );

            // Seed Admin User
            context.Users.AddOrUpdate(
                u => u.Username,
                new User
                {
                    Username = "admin",
                    Password = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                    Role = "Admin",
                    FullName = "Site Admin",
                    Email = "admin@bloomfiy.local"
                }
            );

            // Seed Regular User
            context.Users.AddOrUpdate(
                u => u.Username,
                new User
                {
                    Username = "user1",
                    Password = "04f8996da763b7b7375f3c4b8e8f8b9d8f9a2b6f7e3a2d1c0b0e7b1b1b1b1b1",
                    Role = "User",
                    FullName = "Demo User",
                    Email = "user1@bloomfiy.local"
                }
            );
        }
    }
}
