using ManTestAppWebForms.Controllers.Interfaces;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManTestAppWebForms.Controllers
{
    public class StepController : ControllerBase<Step>, IStepController
    {
        public void DeleteAttachment(int id)
        {
            uof.GetRepository<Attachment>().Delete(id);
            uof.Save();
        }

        public TestCase FindTestCase(int id)
        {
            return uof.GetRepository<TestCase>().FindByKey(id);
        }

        public IEnumerable<Attachment> GetRelatedAttachments(int id)
        {
            return uof.GetRepository<Attachment>().All().Where(a => a.StepId == id);
        }

    }
}