namespace Bloomfiy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        category_id = c.Int(nullable: false, identity: true),
                        category_name = c.String(nullable: false, maxLength: 50),
                        description = c.String(maxLength: 200),
                        image_url = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.category_id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        product_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        description = c.String(maxLength: 500),
                        base_price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        stock_quantity = c.Int(nullable: false),
                        image_url = c.String(maxLength: 500),
                        category_id = c.Int(nullable: false),
                        is_available = c.Boolean(nullable: false),
                        date_created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.product_id)
                .ForeignKey("dbo.Categories", t => t.category_id, cascadeDelete: true)
                .Index(t => t.category_id);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        color_id = c.Int(nullable: false, identity: true),
                        color_name = c.String(nullable: false, maxLength: 50),
                        color_code = c.String(maxLength: 10),
                        price_adjustment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        is_available = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.color_id);
            
            CreateTable(
                "dbo.ProductColors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_id = c.Int(nullable: false),
                        color_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Colors", t => t.color_id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.product_id, cascadeDelete: true)
                .Index(t => t.product_id)
                .Index(t => t.color_id);
            
            CreateTable(
                "dbo.ColorProducts",
                c => new
                    {
                        Color_ColorId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Color_ColorId, t.Product_ProductId })
                .ForeignKey("dbo.Colors", t => t.Color_ColorId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductId, cascadeDelete: true)
                .Index(t => t.Color_ColorId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductColors", "product_id", "dbo.Products");
            DropForeignKey("dbo.ProductColors", "color_id", "dbo.Colors");
            DropForeignKey("dbo.ColorProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.ColorProducts", "Color_ColorId", "dbo.Colors");
            DropForeignKey("dbo.Products", "category_id", "dbo.Categories");
            DropIndex("dbo.ColorProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.ColorProducts", new[] { "Color_ColorId" });
            DropIndex("dbo.ProductColors", new[] { "color_id" });
            DropIndex("dbo.ProductColors", new[] { "product_id" });
            DropIndex("dbo.Products", new[] { "category_id" });
            DropTable("dbo.ColorProducts");
            DropTable("dbo.ProductColors");
            DropTable("dbo.Colors");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
