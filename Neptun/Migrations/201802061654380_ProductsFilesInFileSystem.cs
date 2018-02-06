namespace Neptun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsFilesInFileSystem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productions", "FullDescriptionPdf", c => c.String(maxLength: 200));
            AlterColumn("dbo.Productions", "Photo", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productions", "Photo", c => c.Binary());
            AlterColumn("dbo.Productions", "FullDescriptionPdf", c => c.Binary());
        }
    }
}
