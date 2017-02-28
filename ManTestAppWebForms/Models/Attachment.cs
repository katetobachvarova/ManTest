using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppWebForms.Models
{
    public class Attachment : IIdentifiableEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int StepId { get; set; }
        public virtual Step Step { get; set; }
    }
}
