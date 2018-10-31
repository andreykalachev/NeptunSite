namespace Neptun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BugTest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "Photo", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "Photo", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
