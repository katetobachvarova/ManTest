using ManTestAppWebForms.Controllers;
using System;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Views
{
    [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
    [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
    public partial class StepCreate : System.Web.UI.Page
    {
        private StepController stepController;

        protected void Page_Load(object sender, EventArgs e)
        {
            stepController = new StepController();
        }

        public void FormViewStep_InsertItem()
        {
            var item = new ManTestAppWebForms.Models.Step();
            int testCaseid;
            if (!string.IsNullOrEmpty(Request.QueryString["testCaseId"]) && Int32.TryParse(Request.QueryString["testCaseId"], out testCaseid) && stepController.FindTestCase(testCaseid) != null)
            {
                item.TestCaseId = testCaseid;
            }
            else
            {
                ModelState.AddModelError("TestCaseId", "Test Case not Found!");
            }
            TryUpdateModel(item);
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