﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppWebForms.Models
{
    public class Module : IIdentifiableEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000)]
        public string Decription { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

    }
}
