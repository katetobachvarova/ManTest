namespace ManTestAppWebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Projects", "Decription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Decription", c => c.String(nullable: false));
            DropColumn("dbo.Projects", "Description");
        }
    }
}
