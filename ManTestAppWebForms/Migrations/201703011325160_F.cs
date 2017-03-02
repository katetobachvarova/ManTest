namespace ManTestAppWebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Steps", "Description", c => c.String(nullable: false));
            AddColumn("dbo.TestCases", "Description", c => c.String(nullable: false));
            AddColumn("dbo.Modules", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Steps", "Decription");
            DropColumn("dbo.TestCases", "Decription");
            DropColumn("dbo.Modules", "Decription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Modules", "Decription", c => c.String(nullable: false));
            AddColumn("dbo.TestCases", "Decription", c => c.String(nullable: false));
            AddColumn("dbo.Steps", "Decription", c => c.String(nullable: false));
            DropColumn("dbo.Modules", "Description");
            DropColumn("dbo.TestCases", "Description");
            DropColumn("dbo.Steps", "Description");
        }
    }
}
