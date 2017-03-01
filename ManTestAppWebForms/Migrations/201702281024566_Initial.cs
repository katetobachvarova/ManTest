namespace ManTestAppWebForms.Migrations
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
                        Title = c.String(nullable: false, maxLength: 50),
                        Decription = c.String(nullable: false),
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
                        Title = c.String(nullable: false, maxLength: 50),
                        Decription = c.String(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        ModuleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.ModuleId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Decription = c.String(nullable: false),
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
                        Title = c.String(nullable: false, maxLength: 50),
                        Decription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Steps", "TestCaseId", "dbo.TestCases");
            DropForeignKey("dbo.TestCases", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.TestCases", "ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Modules", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Attachments", "StepId", "dbo.Steps");
            DropIndex("dbo.Modules", new[] { "ProjectId" });
            DropIndex("dbo.TestCases", new[] { "ModuleId" });
            DropIndex("dbo.TestCases", new[] { "ProjectId" });
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
