using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppModels
{
    public class TestCase : IIdentifiableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Decription { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int? ModuleId { get; set; }
        public virtual Module Module { get; set; }

        public virtual ICollection<Step> Steps { get; set; }
    }
}
