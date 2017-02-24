namespace ManTestAppDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestCases", "Project_Id", "dbo.Projects");
            DropIndex("dbo.TestCases", new[] { "Project_Id" });
            RenameColumn(table: "dbo.TestCases", name: "Module_Id", newName: "ModuleId");
            RenameColumn(table: "dbo.TestCases", name: "Project_Id", newName: "ProjectId");
            RenameIndex(table: "dbo.TestCases", name: "IX_Module_Id", newName: "IX_ModuleId");
            AlterColumn("dbo.TestCases", "ProjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.TestCases", "ProjectId");
            AddForeignKey("dbo.TestCases", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestCases", "ProjectId", "dbo.Projects");
            DropIndex("dbo.TestCases", new[] { "ProjectId" });
            AlterColumn("dbo.TestCases", "ProjectId", c => c.Int());
            RenameIndex(table: "dbo.TestCases", name: "IX_ModuleId", newName: "IX_Module_Id");
            RenameColumn(table: "dbo.TestCases", name: "ProjectId", newName: "Project_Id");
            RenameColumn(table: "dbo.TestCases", name: "ModuleId", newName: "Module_Id");
            CreateIndex("dbo.TestCases", "Project_Id");
            AddForeignKey("dbo.TestCases", "Project_Id", "dbo.Projects", "Id");
        }
    }
}
