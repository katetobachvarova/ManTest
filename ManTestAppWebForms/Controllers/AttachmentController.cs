using ManTestAppWebForms.Controllers.Interfaces;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManTestAppWebForms.Controllers
{
    public class AttachmentController : ControllerBase<Attachment>, IAttachmentController
    {
        public Step FindStep(int id)
        {
            return uof.GetRepository<Step>().FindByKey(id);
        }
    }
}