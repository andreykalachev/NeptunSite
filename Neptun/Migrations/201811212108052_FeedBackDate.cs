namespace Neptun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeedBackDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeedBacks", "Date", c => c.DateTime());
            AlterColumn("dbo.FeedBacks", "LastName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FeedBacks", "LastName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.FeedBacks", "Date");
        }
    }
}
