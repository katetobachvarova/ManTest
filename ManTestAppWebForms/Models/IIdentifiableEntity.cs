using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppWebForms.Models
{
    public interface IIdentifiableEntity
    {
        int Id { get; set; }
    }
}
