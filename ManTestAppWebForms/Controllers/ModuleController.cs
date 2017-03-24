using ManTestAppWebForms.Controllers.Interfaces;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManTestAppWebForms.Controllers
{
    public class ModuleController : ControllerBase<Module>, IModuleController
    {
        public void DeleteTestCase(int id)
        {
            uof.GetRepository<TestCase>().Delete(id);
            uof.Save();
        }

        public Project FindProject(int id)
        {
            return uof.GetRepository<Project>().FindByKey(id);
        }

        public TestCase FindTestCase(int id)
        {
            return uof.GetRepository<TestCase>().FindByKey(id);
        }

        public IQueryable<TestCase> GetRelatedTestCases(int id)
        {
            return uof.GetRepository<TestCase>().All().Where(i => i.ModuleId == id).AsQueryable();
        }

        public void UpdateTestCase(TestCase item)
        {
            uof.GetRepository<TestCase>().Update(item);
            uof.Save();
        }
    }
}