using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        public TestCase currentTestCase;

        protected void Page_Load(object sender, EventArgs e)
        {
            testCaseController = new ControllerBase<TestCase>();
            this.testCaseId = Request.QueryString["testCaseId"];
            int testCaseid;
            if (Int32.TryParse(testCaseId, out testCaseid))
            {
                currentTestCase = testCaseController.FindById(testCaseid);
                if (currentTestCase != null)
                {
                    LabelProjectTitle.Text = "Project : " + currentTestCase.Project.Title;
                    LabelModuleTitle.Text = (currentTestCase.Module == null) ? LabelModuleTitle.Text = "No Related Module" : LabelModuleTitle.Text = "Module : " + currentTestCase.Module.Title;
                    LabelTestCaseTitle.Text = "Test Case : " + currentTestCase.Title;
                }
            }
            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["testCaseId"]))
            {
                SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);
                
            }
            //if (!IsPostBack)
            //{
            //    //PopulateTreeView();
            //}
        }

        private void ShowImageFiles(Step currentStep, PlaceHolder PlaceHolderForImages)
        {
            //if (currentStep != null)
            //{
            int testCaseid;
            Int32.TryParse(testCaseId, out testCaseid);
            IEnumerable<Attachment> attachments = testCaseController.uof.GetRepository<Attachment>().All().Where(e => e.StepId == currentStep.Id).AsQueryable();

            //IEnumerable<Attachment> attachments = stepController.uof.GetRepository<Attachment>().All().Where(a => a.StepId == currentStep.Id);
                foreach (var item in attachments)
                {
                    if (item.FileName.EndsWith(".jpg")
                        || item.FileName.EndsWith(".jpeg")
                        || item.FileName.EndsWith(".png")
                        || item.FileName.EndsWith(".bmp")
                        || item.FileName.EndsWith(".gif"))
                    {
                        HyperLink link = new HyperLink();
                        link.NavigateUrl = "../Data/" + item.FileName;
                        link.ImageHeight = 100;
                        link.ApplyStyle(new Style() { CssClass = "forselect" });
                        Image img = new Image();
                        img.ApplyStyle(new Style() { CssClass = "images" });
                        img.Width = 100;
                        img.Height = 100;
                        img.ImageUrl = "../Data/" + item.FileName;
                        img.ID = item.Id.ToString();
                        link.Controls.Add(img);
                        PlaceHolderForImages.Controls.Add(link);
                        PlaceHolderForImages.DataBind();
                    }
                }
            //}
        }


        SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            SiteMap.SiteMapResolve -= new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

            if (SiteMap.CurrentNode != null)
            {
                SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
                currentNode.Title = "TestCaseId" + testCaseId;
                if (currentTestCase.ModuleId.HasValue)
                {
                    currentNode.ParentNode.Title = "ModuleId" + testCaseController.uof.GetRepository<Module>().FindByKey(currentTestCase.ModuleId.Value).Id.ToString();
                    currentNode.ParentNode.Url = string.Format("ModuleDetails.aspx?moduleId={0}", testCaseController.uof.GetRepository<Module>().FindByKey(currentTestCase.ModuleId.Value).Id.ToString());
                }
                else
                {
                    currentNode.ParentNode.Title = "No related Module";
                }
                currentNode.ParentNode.ParentNode.Title = "ProjectId" + currentTestCase.ProjectId.ToString();
                currentNode.ParentNode.ParentNode.Url = string.Format("ProjectDetails.aspx?projectId={0}", currentTestCase.ProjectId.ToString());
                
                //else
                //{
                //    currentNode.ParentNode.Title = "No related Module";
                //    //currentNode.ParentNode.Url = string.Format("ModuleDetails.aspx?moduleId={0}", testCaseController.uof.GetRepository<Module>().FindByKey(currentTestCase.ModuleId.Value).Id.ToString());

                //    currentNode.ParentNode.ParentNode.Title = "ProjectId" + currentTestCase.ProjectId.ToString();
                //    currentNode.ParentNode.ParentNode.Url = string.Format("ProjectDetails.aspx?projectId={0}", currentTestCase.ProjectId.ToString());
                //}
              
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
            testCaseController.uof.GetRepository<Step>().Delete(id);
            testCaseController.uof.Save();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewSteps_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.Step item = null;
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            item = testCaseController.uof.GetRepository<Step>().FindByKey(id);
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
                testCaseController.uof.GetRepository<Step>().Update(item);
                testCaseController.uof.Save();
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

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<ManTestAppWebForms.Models.Attachment> GridViewAttachments_GetData()
        {
            //var stepid = ListViewSteps.SelectedDataKey.Value;
            int testCaseid;
            Int32.TryParse(testCaseId, out testCaseid);
            return testCaseController.uof.GetRepository<Attachment>().All().Where(e => e.Step.TestCaseId == testCaseid).AsQueryable();
        }

        protected void ListViewSteps_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType != ListViewItemType.DataItem)
            {
                return;

            }
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            // Get the ID from the GridView
            Step currentStep = (Step)dataItem.DataItem;
            

            // Bind the supporting documents to the ListView control
            var gridView = (GridView)e.Item.FindControl("GridViewAttachments_GetData");
            int testCaseid;
            Int32.TryParse(testCaseId, out testCaseid);
            gridView.DataSource = testCaseController.uof.GetRepository<Attachment>().All().Where(i => i.StepId == currentStep.Id).AsQueryable();
            gridView.DataBind();

            var placeholderforimages = (PlaceHolder)e.Item.FindControl("PlaceHolderForImages");

            ShowImageFiles(currentStep, placeholderforimages);
        }

        //protected void GridViewSteps_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var stepid = (int)GridViewSteps.SelectedValue;
        //    var step = testCaseController.uof.GetRepository<Step>().FindByKey(stepid);
        //    var att = step.Attachments;
        //    List<string> filenames = new List<string>();
        //    foreach (var item in att)
        //    {
        //        filenames.Add(item.FileName);
        //    }
        //    ImageToStep.ImageUrl = Server.MapPath("~/Data/") + filenames.ElementAtOrDefault(0);
        //    ImageToStep.DataBind();
        //    //string completeUrl = Server.MapPath("~/Data/") + filenames.ElementAtOrDefault(0);
        //    //byte[] contents = File.ReadAllText(completeUrl);
        //    //FileContents.Text = contents;
        //}
    }
}