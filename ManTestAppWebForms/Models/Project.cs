using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppWebForms.Models
{
    public class Project : IIdentifiableEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(10000)]
        public string Description { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
    }
}
