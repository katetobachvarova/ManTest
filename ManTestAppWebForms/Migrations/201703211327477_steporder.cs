namespace ManTestAppWebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class steporder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Steps", "StepOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.Projects", "Title", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "Title", c => c.String(nullable: false, maxLength: 5));
            DropColumn("dbo.Steps", "StepOrder");
        }
    }
}
