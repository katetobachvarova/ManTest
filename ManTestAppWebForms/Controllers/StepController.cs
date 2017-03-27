using ManTestAppWebForms.Controllers.Interfaces;
using ManTestAppWebForms.Models;
using System.Collections.Generic;
using System.Linq;

namespace ManTestAppWebForms.Controllers
{
    public class StepController : ControllerBase<Step>, IStepController
    {
        public IEnumerable<Attachment> GetRelatedAttachments(int id)
        {
            return uof.GetRepository<Attachment>().All().Where(a => a.StepId == id);
        }
        public TestCase FindTestCase(int id)
        {
            return uof.GetRepository<TestCase>().FindByKey(id);
        }
        public void DeleteAttachment(int id)
        {
            uof.GetRepository<Attachment>().Delete(id);
            uof.Save();
        }
    }
}