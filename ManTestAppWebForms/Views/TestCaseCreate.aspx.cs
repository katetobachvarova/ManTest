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
    public partial class TestCaseCreate : System.Web.UI.Page
    {
        private ControllerBase<TestCase> testCaseController;
        private string projectId;
        private string moduleId;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.testCaseController = new ControllerBase<TestCase>();
            moduleId = Request.QueryString["moduleId"];
            projectId = Request.QueryString["projectId"];
        }

        public void InsertItem_TestCase()
        {
            var item = new ManTestAppWebForms.Models.TestCase();
            if (!string.IsNullOrEmpty(moduleId))
            {
                int moduleid;
                Int32.TryParse(moduleId, out moduleid);
                item.ModuleId = moduleid;
                item.ProjectId = this.testCaseController.uof.GetRepository<Module>().FindByKey(moduleid).ProjectId;
            }
            else
            {
                int projectid;
                Int32.TryParse(projectId, out projectid);
                item.ProjectId = projectid;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                testCaseController.Insert(item);
            }
        }

        protected void ItemInserted_TestCase(object sender, FormViewInsertedEventArgs e)
        {
            if (ModelState.IsValid)
            {
                Response.Redirect("~/Views/ModuleIndex.aspx");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ModuleIndex.aspx");
        }
    }
}