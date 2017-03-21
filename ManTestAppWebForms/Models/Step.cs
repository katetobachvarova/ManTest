using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppWebForms.Models
{
    public class Step : IIdentifiableEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(10000)]
        public string Description { get; set; }
        [Required]
        public int StepOrder { get; set; }

        public int TestCaseId { get; set; }
        public virtual TestCase TestCase { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
