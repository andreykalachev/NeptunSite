namespace Neptun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bugFix : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ProductInfoDtoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductInfoDtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 100),
                        ProductType = c.Int(nullable: false),
                        Photo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
