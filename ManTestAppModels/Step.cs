using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppModels
{
    public class Step : IIdentifiableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Decription { get; set; }

        public int TestCaseId { get; set; }
        public virtual TestCase TestCase { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
