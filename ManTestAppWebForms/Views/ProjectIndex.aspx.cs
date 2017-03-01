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

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<Project> GetData_ProjectIndex()
        {
            return projectController.GetAllProjects();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void gv_ProjectIndex_DeleteItem(int id)
        {
            projectController.DeleteProject(id);
        }

        protected void gv_ProjectIndex_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            gv_ProjectIndex.DataBind();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void gv_ProjectIndex_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.Project item = null;
            item = projectController.FindProjectById(id);
            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                projectController.UpdateProject(item);

            }
        }

        public override void Dispose()
        {
            if (projectController != null)
            {
                projectController.Dispose();
            }
            base.Dispose();
        }
    }
}