namespace Neptun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductMetadataFieldsSizeIncreaed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productions", "PageTitle", c => c.String(maxLength: 100));
            AlterColumn("dbo.Productions", "PageDescription", c => c.String(maxLength: 300));
            AlterColumn("dbo.Productions", "PageKeywords", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productions", "PageKeywords", c => c.String(maxLength: 200));
            AlterColumn("dbo.Productions", "PageDescription", c => c.String(maxLength: 200));
            AlterColumn("dbo.Productions", "PageTitle", c => c.String(maxLength: 60));
        }
    }
}
