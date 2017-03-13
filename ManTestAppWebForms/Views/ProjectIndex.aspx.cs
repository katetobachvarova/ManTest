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
        private ControllerBase<Project> projectController;
        private ControllerBase<Module> moduleController;
        private ControllerBase<TestCase> testCaseController;


        protected void Page_Load(object sender, EventArgs e)
        {
            projectController = new ControllerBase<Project>();
            moduleController = new ControllerBase<Module>();
            testCaseController = new ControllerBase<TestCase>();

        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<Project> GetData_ProjectIndex()
        {
            return projectController.GetAll();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void gv_ProjectIndex_DeleteItem(int id)
        {
            projectController.Delete(id);
        }

        protected void gv_ProjectIndex_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            gv_ProjectIndex.DataBind();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void gv_ProjectIndex_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.Project item = null;
            item = projectController.FindById(id);
            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                projectController.Update(item);

            }
        }

        protected void gv_ProjectIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = 1;
           
        }

        protected void parent_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView childgv = (GridView)e.Row.FindControl("Module");
                if (childgv != null)
                {
                    childgv.DataSource = moduleController.GetAll().Where(i => i.ProjectId == (e.Row.DataItem as Project).Id);
                    childgv.DataBind();
                }
                GridView childgv2 = (GridView)e.Row.FindControl("TestCase2");
                if (childgv2 != null)
                {
                    childgv2.DataSource = testCaseController.GetAll().Where(i => i.ModuleId == null &&  i.ProjectId == (e.Row.DataItem as Project).Id);
                    childgv2.DataBind();
                }
                //GridView grandchildgv = (GridView)((GridView)e.Row.FindControl("Kat")).SelectedRow?.FindControl("TestCase");
                ////GridView grandchildgv = (GridView)e.Row.FindControl("TestCase");
                //if (grandchildgv != null)
                //{
                //    grandchildgv.DataSource = moduleController.GetAll().Where(i => i.ProjectId == (e.Row.DataItem as Project).Id);
                //    grandchildgv.DataBind();
                //}
            }
            
        }
        protected void child_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView childgv = (GridView)e.Row.FindControl("TestCase");
                if (childgv != null)
                {
                    
                    childgv.DataSource = (testCaseController.GetAll().Where(i => i.ProjectId == (e.Row.DataItem as Module).ProjectId && i.ModuleId != null && i.ModuleId == (e.Row.DataItem as Module).Id));
                    childgv.DataBind();
                }

            }
            //if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            //{
            //    GridView childgv = (GridView)e.Row.FindControl("Kat");
            //    if (childgv != null)
            //    {
            //        childgv.DataSource = moduleController.GetAll().Where(i => i.ProjectId == (e.Row.DataItem as Project).Id);
            //        childgv.DataBind();
            //    }
            //    GridView grandchildgv = (GridView)e.Row.FindControl("TestCase");
            //    if (grandchildgv != null)
            //    {
            //        grandchildgv.DataSource = moduleController.GetAll().Where(i => i.ProjectId == (e.Row.DataItem as Project).Id);
            //        grandchildgv.DataBind();
            //    }
            //}

        }
        protected void grandchild_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void NoModuleDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView childgv = (GridView)e.Row.FindControl("TestCase");
                if (childgv != null)
                {
                    childgv.DataSource = testCaseController.GetAll().Where(i => i.ModuleId == null);
                    childgv.DataBind();
                }
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