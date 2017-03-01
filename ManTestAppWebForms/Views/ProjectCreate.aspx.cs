using ManTestAppWebForms.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Views
{
    public partial class ProjectCreate : System.Web.UI.Page
    {
        private ProjectController projectController;

        protected void Page_Load(object sender, EventArgs e)
        {
            projectController = new ProjectController();
        }

        public void InsertItem_Project()
        {
            var item = new ManTestAppWebForms.Models.Project();
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                projectController.InsertProject(item);
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ProjectIndex.aspx");
        }

        protected void ItemInserted_Project(object sender, FormViewInsertedEventArgs e)
        {
            if (ModelState.IsValid)
            {
                Response.Redirect("~/Views/ProjectIndex.aspx");
            }
        }
    }
}