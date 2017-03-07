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
    public partial class AttachTestCaseToModule : System.Web.UI.Page
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

        protected void Cancel_Click(object sender, EventArgs e)
        {

        }

        protected void TestCase_Inserted(object sender, FormViewInsertedEventArgs e)
        {

        }

        public void InsertItem_TestCase()
        {
            var item = new ManTestAppWebForms.Models.TestCase();
            if (!string.IsNullOrEmpty(moduleId))
            {
                int moduleid;
                Int32.TryParse(moduleId, out moduleid);
                Module existingModule = this.testCaseController.uof.GetRepository<Module>().FindByKey(moduleid);
                if (existingModule != null)
                {
                    item.ModuleId = moduleid;
                    item.ProjectId = this.testCaseController.uof.GetRepository<Module>().FindByKey(moduleid).ProjectId;
                }
                else
                {
                    ModelState.AddModelError("ModuleId", "Module not found!");
                }

            }
            else if (!string.IsNullOrEmpty(projectId))
            {
                int projectid;
                Int32.TryParse(projectId, out projectid);
                Project existingProject = this.testCaseController.uof.GetRepository<Project>().FindByKey(projectid);
                if (existingProject != null)
                {
                    item.ProjectId = projectid;
                }
                else
                {
                    ModelState.AddModelError("ProjectId", "Project not found!");
                }
                item.ProjectId = projectid;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                testCaseController.Insert(item);
            }
        }
    }
}