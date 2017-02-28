using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppWebForms.Models
{
    public class Module : IIdentifiableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Decription { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

    }
}
