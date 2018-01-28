namespace Neptun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ButtonDescriptionNameAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productions", "ButtonDescriptionName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productions", "ButtonDescriptionName");
        }
    }
}
