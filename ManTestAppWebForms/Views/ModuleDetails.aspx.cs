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
        private ControllerBase<Module> moduleController;
        public Module currentModule;

        protected void Page_Load(object sender, EventArgs e)
        {
            moduleController = new ControllerBase<Module>();
            string moduleId = Request.QueryString["moduleId"];
            int moduleid;
            if (Int32.TryParse(moduleId, out moduleid))
            {
                currentModule = moduleController.FindById(moduleid);
                if (currentModule != null)
                {
                    if (moduleController.uof.GetRepository<TestCase>().All().Where(i => i.ModuleId == currentModule.Id).Any())
                    {
                        LabelRelatedTestCases.Text = "Related Test Cases";
                    }
                }
                else
                {
                    Response.Redirect("~/Views/ModuleIndex.aspx");
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
                return moduleController.uof.GetRepository<TestCase>().All().Where(i => i.ModuleId == currentModule.Id).AsQueryable();
            }
            return null;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void GridViewTestCases_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.TestCase item = null;
            item = moduleController.uof.GetRepository<TestCase>().FindByKey(id);
            if (item == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                moduleController.uof.GetRepository<TestCase>().Update(item);
                moduleController.uof.Save();
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void GridViewTestCases_DeleteItem(int id)
        {
            moduleController.uof.GetRepository<TestCase>().Delete(id);
            moduleController.uof.Save();
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