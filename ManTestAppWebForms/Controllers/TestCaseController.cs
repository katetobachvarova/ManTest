using ManTestAppWebForms.Controllers.Interfaces;
using ManTestAppWebForms.Models;
using System.Collections.Generic;
using System.Linq;

namespace ManTestAppWebForms.Controllers
{
    public class TestCaseController : ControllerBase<TestCase>, ITestCaseController
    {
        public IEnumerable<Project> GetAllProjects()
        {
            return uof.GetRepository<Project>().All();
        }

        public IEnumerable<Attachment> GetRelatedAttachments(int id)
        {
            return uof.GetRepository<Attachment>().All().Where(e => e.StepId == id);
        }

        public IQueryable<Module> GetRelatedModules(int id)
        {
            return uof.GetRepository<Module>().All().Where(m => m.ProjectId == id).AsQueryable();
        }

        public IQueryable<Step> GetRelatedSteps(int id)
        {
            return uof.GetRepository<Step>().All().Where(i => i.TestCaseId == id).OrderBy(e => e.StepOrder).AsQueryable();
        }

        public Module FindModule(int id)
        {
            return uof.GetRepository<Module>().FindByKey(id);
        }

        public Project FindProject(int id)
        {
            return uof.GetRepository<Project>().FindByKey(id);
        }

        public IEnumerable<TestCase> FindTestCaseByIdTitleOrDescription(string titleOrDescription)
        {
            if (!string.IsNullOrEmpty(titleOrDescription))
            {
                return currentRepository.FindBy(tc => tc.Title.Contains(titleOrDescription) || tc.Description.Contains(titleOrDescription) || tc.Id.ToString().Contains(titleOrDescription));
            }
            else return null;
        }
    }
}