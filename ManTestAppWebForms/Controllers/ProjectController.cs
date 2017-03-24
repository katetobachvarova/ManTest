using ManTestAppWebForms.Controllers.Interfaces;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManTestAppWebForms.Controllers
{
    public class ProjectController : ControllerBase<Project>, IProjectController
    {
        public void DeleteModule(int id)
        {
            uof.GetRepository<Module>().Delete(id);
            uof.Save();
        }
        public void DeleteTestCase(int id)
        {
            uof.GetRepository<TestCase>().Delete(id);
            uof.Save();
        }
        public void UpdateModule(Module item)
        {
            uof.GetRepository<Module>().Update(item);
            uof.Save();
        }

        public void UpdateTestCase(TestCase item)
        {
            uof.GetRepository<TestCase>().Update(item);
            uof.Save();
        }
        public IQueryable<Module> GetRelatedModules(int currentProjectId)
        {
            return uof.GetRepository<Module>().All().Where(i => i.ProjectId == currentProjectId).AsQueryable();
        }
        public IQueryable<TestCase> GetRelatedTestCases(int currentProjectId)
        {
            return uof.GetRepository<TestCase>().All().Where(i => i.ProjectId == currentProjectId && i.ModuleId == null).AsQueryable();
        }

        public Module FindModule(int id)
        {
            return uof.GetRepository<Module>().FindByKey(id);
        }

        public TestCase FindTestCase(int id)
        {
            return uof.GetRepository<TestCase>().FindByKey(id);
        }
    }
}