using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Views
{
    public partial class ProjectIndex : System.Web.UI.Page
    {
        private ProjectController projectController;

        protected void Page_Load(object sender, EventArgs e)
        {
            projectController = new ProjectController();
        }

        public IQueryable<Project> GetProjects()
        {
            return projectController.Get();
        }
    }
}