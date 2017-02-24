using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppModels
{
    public class Project : IIdentifiableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Decription { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
    }
}
