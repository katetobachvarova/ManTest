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
    public partial class ProjectDetails : System.Web.UI.Page
    {
        private string projectId;
        private ControllerBase<Project> projectController;
        public Project currentProject;


        protected void Page_Load(object sender, EventArgs e)
        {
            projectId = Request.QueryString["projectId"];
            this.projectController = new ControllerBase<Project>();
            int projectid;

            if (Int32.TryParse(projectId, out projectid))
            {
                currentProject = projectController.FindById(projectid);
                if (currentProject != null)
                {
                    lblCurrentProjectTitle.Text = "Project : " + currentProject.Title;
                }
            }
            
            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["projectId"]))
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
            SiteMap.SiteMapResolve -= new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

            if (SiteMap.CurrentNode != null)
            {
                SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
                //currentNode.Title = "ProjectId"+ projectId;
                currentNode.Title = "ProjectId" + Request.QueryString["projectId"];

                return currentNode;
            }
            return null;
        }


        public IQueryable<Module> GetModules()
        {
            if (currentProject != null)
            {
                return projectController.uof.GetRepository<Module>().All().Where(i => i.ProjectId == currentProject.Id).AsQueryable();
            }
            return null;
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<ManTestAppWebForms.Models.TestCase> GetTestCases()
        {
            if (currentProject != null)
            {
                return projectController.uof.GetRepository<TestCase>().All().Where(i => i.ProjectId == currentProject.Id && i.ModuleId == null).AsQueryable();
            }
            return null;
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewModules_DeleteItem(int id)
        {
            projectController.uof.GetRepository<Module>().Delete(id);
            projectController.uof.Save();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewModules_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.Module item = null;
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            item = projectController.uof.GetRepository<Module>().FindByKey(id);
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
                projectController.uof.GetRepository<Module>().Update(item);
                projectController.uof.Save();
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewTestCases_DeleteItem(int id)
        {
            projectController.uof.GetRepository<TestCase>().Delete(id);
            projectController.uof.Save();

        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewTestCases_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.TestCase item = null;
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            item = projectController.uof.GetRepository<TestCase>().FindByKey(id);

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
                projectController.uof.GetRepository<TestCase>().Update(item);
                projectController.uof.Save();
            }
        }
    }
}