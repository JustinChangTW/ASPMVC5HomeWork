namespace Session1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountBook",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Categoryyy = c.Int(nullable: false),
                        Amounttt = c.Int(nullable: false),
                        Dateee = c.DateTime(nullable: false),
                        Remarkkk = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountBook");
        }
    }
}
