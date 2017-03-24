using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManTestAppWebForms.Controllers.Interfaces
{
    public interface IStepController
    {
        IEnumerable<Attachment> GetRelatedAttachments(int id);
        void DeleteAttachment(int id);
        TestCase FindTestCase(int id);
    }
}