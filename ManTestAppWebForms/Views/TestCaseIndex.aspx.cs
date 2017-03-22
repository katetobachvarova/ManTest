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
    public partial class TestCaseIndex : System.Web.UI.Page
    {
        private ControllerBase<TestCase> testCaseController;

        protected void Page_Load(object sender, EventArgs e)
        {
            testCaseController = new ControllerBase<TestCase>();
            //if (!IsPostBack)
            //{
            //    gvTestCases.DataSource = testCaseController.GetAll();
            //    gvTestCases.DataBind();
            //}
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<ManTestAppWebForms.Models.TestCase> gvTestCases_GetData()
        {
            return testCaseController.GetAll();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void gvTestCases_DeleteItem(int id)
        {
            testCaseController.Delete(id);
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void gvTestCases_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.TestCase item = null;
            item = testCaseController.FindById(id);
            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                testCaseController.Update(item);
            }
        }

        protected void gvTestCases_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvTestCases.EditIndex)
            {
                gvTestCases.Columns[6].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                gvTestCases.Columns[5].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
            }
        }
    }
}