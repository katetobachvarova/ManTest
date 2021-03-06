﻿using System;
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
        [Range(1, 100, ErrorMessage = "Please provide integer value between 1 and 100 for Step Order Field")]
        public int StepOrder { get; set; }

        public int TestCaseId { get; set; }
        public virtual TestCase TestCase { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
