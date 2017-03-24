using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.Models;
using System;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Views
{
    public partial class ModuleDetails : System.Web.UI.Page
    {
        private ModuleController moduleController;
        public Module currentModule;

        protected void Page_Load(object sender, EventArgs e)
        {
            moduleController = new ModuleController();
            string moduleId = Request.QueryString["moduleId"];
            int moduleid;
            if (Int32.TryParse(moduleId, out moduleid))
            {
                currentModule = moduleController.FindById(moduleid);
                if (currentModule != null)
                {
                    if (moduleController.GetRelatedTestCases(currentModule.Id).Any())
                    {
                        LabelRelatedTestCases.Text = "Related Test Cases";
                    }
                }
                else
                {
                    Response.Redirect("~/Views/ProjectIndex.aspx");
                }
            }
            if (!IsPostBack && currentModule != null)
            {
                SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);
            }
        }

        SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            SiteMap.SiteMapResolve -= new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);
            if (SiteMap.CurrentNode != null)
            {
                SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
                currentNode.Title = "Module " + currentModule.Title;
                currentNode.ParentNode.Title = "Project " + currentModule.Project.Title;
                currentNode.ParentNode.Url = string.Format("ProjectDetails.aspx?projectId={0}", currentModule.ProjectId);
                return currentNode;
            }
            return null;
        }

        public IQueryable<ManTestAppWebForms.Models.TestCase> GridViewTestCases_GetData()
        {
            if (currentModule != null)
            {
                return moduleController.GetRelatedTestCases(currentModule.Id);
            }
            return null;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void GridViewTestCases_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.TestCase item = null;
            item = moduleController.FindTestCase(id);
            if (item == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                moduleController.UpdateTestCase(item);
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void GridViewTestCases_DeleteItem(int id)
        {
            moduleController.DeleteTestCase(id);
        }

        protected void GridViewTestCases_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridViewTestCases.EditIndex)
            {
                GridViewTestCases.Columns[4].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                GridViewTestCases.Columns[5].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
            }
        }
    }
}