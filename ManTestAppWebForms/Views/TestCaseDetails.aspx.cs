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
    public partial class TestCaseDetails : System.Web.UI.Page
    {
        public string testCaseId;
        private ControllerBase<TestCase> testCaseController;
        protected void Page_Load(object sender, EventArgs e)
        {
            testCaseController = new ControllerBase<TestCase>();
            this.testCaseId = Request.QueryString["testCaseId"];
            if (!IsPostBack)
            {
                PopulateTreeView();
            }
        }

        public void PopulateTreeView()
        {
            int testcasetid;
            Int32.TryParse(testCaseId, out testcasetid);

            TreeNode testCaseNode = new TreeNode(testCaseController.FindById(testcasetid)?.Title + "Test Case");
            TreeViewTestCase.Nodes.Add(testCaseNode);
            IEnumerable<Step> listSteps = testCaseController.uof.GetRepository<Step>().All().Where(s => s.TestCaseId == testcasetid);

            foreach (var step in listSteps)
            {
                TreeNode childNodeStep = new TreeNode(step.Title + "Step", step.Id.ToString());
                childNodeStep.NavigateUrl = string.Format("StepDetails.aspx?stepId={0}", step.Id);
                testCaseNode.ChildNodes.Add(childNodeStep);
                IEnumerable<Attachment> listAttachments = testCaseController.uof.GetRepository<Attachment>().All().Where(i => i.StepId == step.Id);
                foreach (var attachment in listAttachments)
                {
                    TreeNode childNodeAttachment = new TreeNode(string.Format("Attchment{0}", attachment.Id), attachment.Id.ToString());
                    childNodeAttachment.NavigateUrl = string.Format("AttachmentDetails.aspx?attachmentId={0}", attachment.Id);
                    childNodeStep.ChildNodes.Add(childNodeAttachment);
                }

            }
        }
    }
}