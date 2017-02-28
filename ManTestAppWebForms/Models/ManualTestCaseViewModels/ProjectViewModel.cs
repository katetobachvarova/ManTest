using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManTestAppWebForms.Models.ManualTestCaseViewModels
{
    public class ProjectViewModel
    {
        public Project Project { get; private set; }

        public ProjectViewModel()
        {
            Project = new Project();
        }

        public ProjectViewModel(Project project)
        {
            this.Project = project;
        }
    }
}