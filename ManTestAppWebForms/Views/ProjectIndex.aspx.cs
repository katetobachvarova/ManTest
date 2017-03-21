using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Views
{
    public partial class ProjectIndex : System.Web.UI.Page
    {
        private ControllerBase<Project> projectController;


        protected void Page_Load(object sender, EventArgs e)
        {
            projectController = new ControllerBase<Project>();
        }

        public IQueryable<Project> GetData_ProjectIndex()
        {
            return projectController.GetAll();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void gv_ProjectIndex_DeleteItem(int id)
        {
            projectController.Delete(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        protected void gv_ProjectIndex_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            gv_ProjectIndex.DataBind();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void gv_ProjectIndex_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.Project item = null;
            item = projectController.FindById(id);
            if (item == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                projectController.Update(item);

            }
        }

        protected void gv_ProjectIndex_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gv_ProjectIndex.EditIndex)
            {
                // Programmatically reference the Edit and Delete LinkButtons
                LinkButton EditButton = e.Row.FindControl("LinkButtonEdit") as LinkButton;
                LinkButton DeleteButton = e.Row.FindControl("LinkButtonDelete") as LinkButton;
                gv_ProjectIndex.Columns[6].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                gv_ProjectIndex.Columns[7].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                gv_ProjectIndex.Columns[3].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                gv_ProjectIndex.Columns[4].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                //EditButton.Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                // DeleteButton.Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
            }
        }


        //public override void Dispose()
        //{
        //    if (projectController != null)
        //    {
        //        projectController.Dispose();
        //    }
        //    base.Dispose();
        //}
    }
}