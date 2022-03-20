namespace WCD_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Thumbnail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Thumbnail");
        }
    }
}
