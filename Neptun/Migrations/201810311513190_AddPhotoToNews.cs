namespace Neptun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoToNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Photo", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Photo");
        }
    }
}
