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
        //public Step currentStep;
        public TestCase currentTestCase;

        protected void Page_Load(object sender, EventArgs e)
        {
            testCaseController = new ControllerBase<TestCase>();
            this.testCaseId = Request.QueryString["testCaseId"];
            int testCaseid;
            if (Int32.TryParse(testCaseId, out testCaseid))
            {
                currentTestCase = testCaseController.FindById(testCaseid);
            }
            else
            {
                currentTestCase = new TestCase();
            }
            LabelTestCase.Text = currentTestCase.Title;

            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["testCaseId"]))
            {
                SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

            }
            //if (!IsPostBack)
            //{
            //    //PopulateTreeView();
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
                currentNode.Title = "TestCaseId" + testCaseId;
                if (currentTestCase.ModuleId.HasValue)
                {
                    currentNode.ParentNode.Title = "ModuleId" + testCaseController.uof.GetRepository<Module>().FindByKey(currentTestCase.ModuleId.Value).Id.ToString();
                    currentNode.ParentNode.Url = string.Format("ModuleDetails.aspx?moduleId={0}", testCaseController.uof.GetRepository<Module>().FindByKey(currentTestCase.ModuleId.Value).Id.ToString());

                    currentNode.ParentNode.ParentNode.Title = "ProjectId" + currentTestCase.ProjectId.ToString();
                    currentNode.ParentNode.ParentNode.Url = string.Format("ProjectDetails.aspx?projectId={0}", currentTestCase.ProjectId.ToString());
                }
                else
                {
                    currentNode.ParentNode.Title = "No related Module";
                    //currentNode.ParentNode.Url = string.Format("ModuleDetails.aspx?moduleId={0}", testCaseController.uof.GetRepository<Module>().FindByKey(currentTestCase.ModuleId.Value).Id.ToString());

                    currentNode.ParentNode.ParentNode.Title = "ProjectId" + currentTestCase.ProjectId.ToString();
                    currentNode.ParentNode.ParentNode.Url = string.Format("ProjectDetails.aspx?projectId={0}", currentTestCase.ProjectId.ToString());
                }
              
                // Use the changed one in the breadcrumb.
                return currentNode;
            }
            return null;
        }

        //public void PopulateTreeView()
        //{
        //    int testcasetid;
        //    Int32.TryParse(testCaseId, out testcasetid);

        //    TreeNode testCaseNode = new TreeNode(testCaseController.FindById(testcasetid)?.Title + "Test Case");
        //    TreeViewTestCase.Nodes.Add(testCaseNode);
        //    IEnumerable<Step> listSteps = testCaseController.uof.GetRepository<Step>().All().Where(s => s.TestCaseId == testcasetid);

        //    foreach (var step in listSteps)
        //    {
        //        TreeNode childNodeStep = new TreeNode(step.Title + "Step", step.Id.ToString());
        //        childNodeStep.NavigateUrl = string.Format("StepDetails.aspx?stepId={0}", step.Id);
        //        testCaseNode.ChildNodes.Add(childNodeStep);
        //        IEnumerable<Attachment> listAttachments = testCaseController.uof.GetRepository<Attachment>().All().Where(i => i.StepId == step.Id);
        //        foreach (var attachment in listAttachments)
        //        {
        //            TreeNode childNodeAttachment = new TreeNode(string.Format("Attchment{0}", attachment.Id), attachment.Id.ToString());
        //            childNodeAttachment.NavigateUrl = string.Format("AttachmentDetails.aspx?attachmentId={0}", attachment.Id);
        //            childNodeStep.ChildNodes.Add(childNodeAttachment);
        //        }

        //    }
        //}

        //protected void TreeViewTestCase_SelectedNodeChanged(object sender, EventArgs e)
        //{
        //    currentStep = new Step();
        //}

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewSteps_DeleteItem(int id)
        {

        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewSteps_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.TestCase item = null;
            // Load the item here, e.g. item = MyDataLayer.Find(id);
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

            }
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<ManTestAppWebForms.Models.Step> GridViewSteps_GetData()
        {
            if (currentTestCase != null)
            {
                return testCaseController.uof.GetRepository<Step>().All().Where(i => i.TestCaseId == currentTestCase.Id).AsQueryable();
            }
            return null;
        }
    }
}