using ManTestAppWebForms.Controllers.Interfaces;
using ManTestAppWebForms.Models;
using System.Linq;

namespace ManTestAppWebForms.Controllers
{
    public class ModuleController : ControllerBase<Module>, IModuleController
    {
        public IQueryable<TestCase> GetRelatedTestCases(int id)
        {
            return uof.GetRepository<TestCase>().All().Where(i => i.ModuleId == id).AsQueryable();
        }

        public TestCase FindTestCase(int id)
        {
            return uof.GetRepository<TestCase>().FindByKey(id);
        }

        public void UpdateTestCase(TestCase item)
        {
            uof.GetRepository<TestCase>().Update(item);
            uof.Save();
        }

        public void DeleteTestCase(int id)
        {
            uof.GetRepository<TestCase>().Delete(id);
            uof.Save();
        }

        public Project FindProject(int id)
        {
            return uof.GetRepository<Project>().FindByKey(id);
        }
    }
}