using ManTestAppWebForms.Models;
using System.Collections.Generic;

namespace ManTestAppWebForms.Controllers.Interfaces
{
    public interface IStepController
    {
        IEnumerable<Attachment> GetRelatedAttachments(int id);
        TestCase FindTestCase(int id);
        void DeleteAttachment(int id);
    }
}