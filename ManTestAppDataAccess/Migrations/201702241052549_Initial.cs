namespace ManTestAppDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        StepId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Steps", t => t.StepId, cascadeDelete: true)
                .Index(t => t.StepId);
            
            CreateTable(
                "dbo.Steps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Decription = c.String(),
                        TestCaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TestCases", t => t.TestCaseId, cascadeDelete: true)
                .Index(t => t.TestCaseId);
            
            CreateTable(
                "dbo.TestCases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Decription = c.String(),
                        Module_Id = c.Int(),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.Module_Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Module_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Decription = c.String(),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Decription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Steps", "TestCaseId", "dbo.TestCases");
            DropForeignKey("dbo.TestCases", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.TestCases", "Module_Id", "dbo.Modules");
            DropForeignKey("dbo.Modules", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Attachments", "StepId", "dbo.Steps");
            DropIndex("dbo.Modules", new[] { "ProjectId" });
            DropIndex("dbo.TestCases", new[] { "Project_Id" });
            DropIndex("dbo.TestCases", new[] { "Module_Id" });
            DropIndex("dbo.Steps", new[] { "TestCaseId" });
            DropIndex("dbo.Attachments", new[] { "StepId" });
            DropTable("dbo.Projects");
            DropTable("dbo.Modules");
            DropTable("dbo.TestCases");
            DropTable("dbo.Steps");
            DropTable("dbo.Attachments");
        }
    }
}
