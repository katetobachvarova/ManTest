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
    public partial class StepCreate : System.Web.UI.Page
    {
        private string testCaseId;
        
        private StepController stepController;

        protected void Page_Load(object sender, EventArgs e)
        {
            stepController = new StepController();
            testCaseId = Request.QueryString["testCaseId"];
        }

        public void FormViewStep_InsertItem()
        {
            var item = new ManTestAppWebForms.Models.Step();
            TryUpdateModel(item);
            if (!string.IsNullOrEmpty(testCaseId))
            {
                int testCaseid;
                Int32.TryParse(testCaseId, out testCaseid);
                TestCase existingTestCase = stepController.FindTestCase(testCaseid);
                if (existingTestCase != null)
                {
                    item.TestCaseId = testCaseid;
                }
                else
                {
                    ModelState.AddModelError("TestCaseId","Test Case not Found!");
                }
            }
            if (ModelState.IsValid)
            {
                stepController.Insert(item);
            }
        }

        protected void FormViewStep_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            if (ModelState.IsValid)
            {
                Response.Redirect(String.Format("~/Views/TestCaseDetails.aspx?testCaseId={0}", Request.QueryString["testCaseId"]));
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("~/Views/TestCaseDetails.aspx?testCaseId={0}", Request.QueryString["testCaseId"]));
        }
    }
}