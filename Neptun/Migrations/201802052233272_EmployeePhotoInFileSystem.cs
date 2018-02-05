namespace Neptun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeePhotoInFileSystem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Photo", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Photo", c => c.Binary());
        }
    }
}
