using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppWebForms.Models
{
    public class TestCase : IIdentifiableEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int? ModuleId { get; set; }
        public virtual Module Module { get; set; }

        public virtual ICollection<Step> Steps { get; set; }
    }
}
