using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.Models;
using System;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Views
{
    public partial class AttachTestCase : System.Web.UI.Page
    {
        private TestCaseController testCaseController;
        public Project currentProject;
        public Module currentModule;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.testCaseController = new TestCaseController();
            string moduleId = Request.QueryString["moduleId"];
            string projectId = Request.QueryString["projectId"];
            int projectid;
            if (Int32.TryParse(projectId, out projectid))
            {
                currentProject = testCaseController.FindProject(projectid);
            }
            int moduleid;
            if (Int32.TryParse(moduleId, out moduleid))
            {
                currentModule = testCaseController.FindModule(moduleid);
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            if (currentModule != null)
            {
                Response.Redirect(String.Format("ModuleDetails.aspx?moduleId={0}", currentModule.Id));
            }
            else if (currentProject != null)
            {
                Response.Redirect(String.Format("ProjectDetails.aspx?projectId={0}", currentProject.Id));
            }
        }

        protected void TestCase_Inserted(object sender, FormViewInsertedEventArgs e)
        {
            if (currentModule != null && ModelState.IsValid)
            {
                Response.Redirect(String.Format("ModuleDetails.aspx?moduleId={0}", currentModule.Id));
            }
            else if (currentProject != null && ModelState.IsValid)
            {
                Response.Redirect(String.Format("ProjectDetails.aspx?projectId={0}", currentProject.Id));
            }
        }

        public void InsertItem_TestCase()
        {
            var item = new ManTestAppWebForms.Models.TestCase();
            if (currentModule != null)
            {
                item.ModuleId = currentModule.Id;
                item.ProjectId = currentModule.ProjectId;
            }
            else if (currentProject != null)
            {
                item.ProjectId = currentProject.Id;
            }
            else
            {
                ModelState.AddModelError("ProjectId", "Project not found!");
                ModelState.AddModelError("ModuleId", "Module not found!");
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                testCaseController.Insert(item);
            }
        }
    }
}