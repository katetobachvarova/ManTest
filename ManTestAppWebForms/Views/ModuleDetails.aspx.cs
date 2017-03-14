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
    public partial class ModuleDetails : System.Web.UI.Page
    {
        private string moduleId;
        private ControllerBase<Module> moduleController;
        public Module currentModule;

        protected void Page_Load(object sender, EventArgs e)
        {
            moduleController = new ControllerBase<Module>();
            moduleId = Request.QueryString["moduleId"];
            int moduleid;

            if (Int32.TryParse(moduleId, out moduleid))
            {
                currentModule = moduleController.FindById(moduleid);
                if (currentModule != null)
                {
                    LabelProjectTitle.Text = "Project : " + currentModule.Project.Title;
                    LabelModuleTitle.Text = "Module : " + currentModule.Title;
                }
            }

            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["moduleId"]))
            {
                SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

            }
            //if (!IsPostBack)
            //{
            //    Page.DataBind();
            //}
        }

        SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            // Only need one execution in one request.
            SiteMap.SiteMapResolve -= new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

            if (SiteMap.CurrentNode != null)
            {
                // SiteMap.CurrentNode is readonly, so we need to clone one to operate.
                SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
                currentNode.Title = "ModuleId" + moduleId;
                currentNode.ParentNode.Title = "Project" + moduleController.uof.GetRepository<Project>().FindByKey(currentModule.ProjectId).Id.ToString();
                currentNode.ParentNode.Url = string.Format("ProjectDetails.aspx?projectId={0}", moduleController.uof.GetRepository<Project>().FindByKey(currentModule.ProjectId).Id.ToString());

                // Use the changed one in the breadcrumb.
                return currentNode;
            }
            return null;
        }


        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<ManTestAppWebForms.Models.TestCase> GridViewTestCases_GetData()
        {
            if (currentModule != null)
            {
                return moduleController.uof.GetRepository<TestCase>().All().Where(i => i.ModuleId == currentModule.Id).AsQueryable();
            }
            return null;
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewTestCases_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.TestCase item = null;
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            item = moduleController.uof.GetRepository<TestCase>().FindByKey(id);

            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                // Save changes here, e.g. MyDataLayer.SaveChanges();
                moduleController.uof.GetRepository<TestCase>().Update(item);
                moduleController.uof.Save();
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewTestCases_DeleteItem(int id)
        {
            moduleController.uof.GetRepository<TestCase>().Delete(id);
            moduleController.uof.Save();
        }
    }
}