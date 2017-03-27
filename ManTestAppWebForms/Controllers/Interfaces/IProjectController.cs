using ManTestAppWebForms.Models;
using System.Linq;

namespace ManTestAppWebForms.Controllers.Interfaces
{
    public interface IProjectController
    {
        IQueryable<Module> GetRelatedModules(int id);
        IQueryable<TestCase> GetRelatedTestCases(int id);
        Module FindModule(int id);
        TestCase FindTestCase(int id);
        void UpdateModule(Module item);
        void UpdateTestCase(TestCase item);
        void DeleteModule(int id);
        void DeleteTestCase(int id);
    }
}
