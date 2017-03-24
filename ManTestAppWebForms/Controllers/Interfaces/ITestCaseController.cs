using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppWebForms.Controllers.Interfaces
{
    public interface ITestCaseController
    {
        IEnumerable<Project> GetAllProjects();
        IQueryable<Module> GetRelatedModules(int id);
        IEnumerable<Attachment> GetRelatedAttachments(int id);
        IQueryable<Step> GetRelatedSteps(int id);
        Project FindProject(int id);
        Module FindModule(int id);
    }
}
