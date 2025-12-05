namespace Bloomfiy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitAgain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 200),
                        Role = c.String(nullable: false, maxLength: 50),
                        FullName = c.String(maxLength: 100),
                        Email = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
        }
    }
}
