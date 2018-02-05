namespace Neptun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeePhotoLengthIncreased : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Photo", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Photo", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
