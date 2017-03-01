namespace ManTestAppWebForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Title", c => c.String(nullable: false, maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "Title", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
