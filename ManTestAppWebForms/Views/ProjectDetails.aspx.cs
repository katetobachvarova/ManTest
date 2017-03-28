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
    public partial class ProjectDetails : System.Web.UI.Page
    {
        private ProjectController projectController;
        public Project currentProject;

        protected void Page_Load(object sender, EventArgs e)
        {
            string projectId = Request.QueryString["projectId"];
            this.projectController = new ProjectController();
            int projectid;
            if (Int32.TryParse(projectId, out projectid))
            {
                currentProject = projectController.FindById(projectid);
                if (currentProject != null)
                {
                    if (currentProject.Modules != null && currentProject.Modules.Any())
                    {
                        LabelRelatedModules.Text = "Related Modules";
                    }
                    if (projectController.GetRelatedTestCases(currentProject.Id).Any())
                    {
                        LabelRelatedTestCases.Text = "Related Test Cases";
                    }
                    ShowBreadCrumb(currentProject);
                }
                else
                {
                    Response.Redirect("~/Views/ProjectIndex.aspx");
                }
            }
        }

        private void ShowBreadCrumb(Project currentProject)
        {
            Label lblP = new Label();
            lblP.Text = string.Format(" {0}", currentProject.Title);
            PlaceHolderForRoot.Controls.Add(lblP);
            PlaceHolderForRoot.DataBind();
        }

        //SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        //{
        //    SiteMap.SiteMapResolve -= new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

        //    if (SiteMap.CurrentNode != null)
        //    {
        //        SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
        //        currentNode.Title = "Project " + currentProject.Title;
        //        return currentNode;
        //    }
        //    return null;
        //}

        public IQueryable<Module> GetModules()
        {
            if (currentProject != null)
            {
                return projectController.GetRelatedModules(currentProject.Id);
            }
            return null;
        }

        public IQueryable<TestCase> GetTestCases()
        {
            if (currentProject != null)
            {
                return projectController.GetRelatedTestCases(currentProject.Id);
            }
            return null;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void GridViewModules_DeleteItem(int id)
        {
            projectController.DeleteModule(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void GridViewModules_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.Module item = null;
            item = projectController.FindModule(id);
            if (item == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                projectController.UpdateModule(item);
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void GridViewTestCases_DeleteItem(int id)
        {
            projectController.DeleteTestCase(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void GridViewTestCases_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.TestCase item = null;
            item = projectController.FindTestCase(id);
            if (item == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                projectController.UpdateTestCase(item);
            }
        }

        protected void GridViewModules_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridViewModules.EditIndex)
            {
                GridViewModules.Columns[3].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                GridViewModules.Columns[4].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                GridViewModules.Columns[5].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
            }
        }

        protected void GridViewTestCases_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridViewModules.EditIndex)
            {
                GridViewTestCases.Columns[4].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                GridViewTestCases.Columns[3].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
            }
        }
    }
}