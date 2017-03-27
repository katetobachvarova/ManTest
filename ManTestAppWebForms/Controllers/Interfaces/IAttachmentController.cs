using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppWebForms.Controllers.Interfaces
{
    public interface IAttachmentController
    {
        Step FindStep(int id);
    }
}
