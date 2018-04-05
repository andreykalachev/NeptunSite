namespace Neptun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductionPageInfoAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productions", "PageTitle", c => c.String(maxLength: 60));
            AddColumn("dbo.Productions", "PageDescription", c => c.String(maxLength: 200));
            AddColumn("dbo.Productions", "PageKeywords", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productions", "PageKeywords");
            DropColumn("dbo.Productions", "PageDescription");
            DropColumn("dbo.Productions", "PageTitle");
        }
    }
}
