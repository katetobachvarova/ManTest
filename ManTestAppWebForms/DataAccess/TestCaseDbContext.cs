using ManTestAppWebForms.Models;
using System.Data.Entity;

namespace ManTestAppWebForms.DataAccess
{
    public class TestCaseDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<TestCase> TestCase { get; set; }
        public DbSet<Step> Step { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
    }
}
