using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Views
{
    public partial class StepDetails : System.Web.UI.Page
    {
        private ControllerBase<Step> stepController;
        private string stepId;
        public Step currentStep;

        protected void Page_Load(object sender, EventArgs e)
        {
            stepController = new ControllerBase<Step>();
            stepId = Request.QueryString["stepId"];
            int stepid;
            if (Int32.TryParse(stepId, out stepid))
            {
                currentStep = stepController.FindById(stepid);
              
                //if (currentStep != null)
                //{
                //    LabelProjectTitle.Text = "Project : " + currentStep.TestCase.Project.Title;
                //    LabelModuleTitle.Text = (currentStep.TestCase.Module == null) ? LabelModuleTitle.Text = "No Related Module" : LabelModuleTitle.Text = "Module : " + currentStep.TestCase.Module.Title;
                //    LabelTestCaseTitle.Text = "Test Case : " + currentStep.TestCase.Title;
                //    LabelStepTitle.Text = "Step : " + currentStep.Title;
                //}
            }
            if (!IsPostBack && currentStep != null)
            {
                FormViewStep.DataSource = new List<Step>() { currentStep };
                FormViewStep.DataBind();
                ShowImageFiles();
                SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);
            }
        }

        private void ShowImageFiles()
        {
            if (currentStep != null)
            {
                IEnumerable<Attachment> attachments = stepController.uof.GetRepository<Attachment>().All().Where(a => a.StepId == currentStep.Id);
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
                        img.ApplyStyle(new Style() { CssClass="images" });
                        img.Width = 100;
                        img.Height = 100;
                        img.ImageUrl = "../Data/" + item.FileName;
                        img.ID = item.Id.ToString();
                        link.Controls.Add(img);
                        PlaceHolderForImages.Controls.Add(link);
                        PlaceHolderForImages.DataBind();
                    }
                }
            }
        }

        SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            // Only need one execution in one request.
            SiteMap.SiteMapResolve -= new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

            if (SiteMap.CurrentNode != null)
            {
                // SiteMap.CurrentNode is readonly, so we need to clone one to operate.
                SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
                currentNode.Title = "StepId" + stepId;
                currentNode.ParentNode.Title = "TestCaseId" + currentStep.TestCaseId;
                
                currentNode.ParentNode.Url = string.Format("TestCaseDetails.aspx?testCaseId={0}", stepController.uof.GetRepository<TestCase>().FindByKey(currentStep.TestCaseId).Id);

                if (currentStep.TestCase.ModuleId.HasValue)
                {
                    currentNode.ParentNode.ParentNode.Title = "ModuleId" + stepController.uof.GetRepository<Module>().FindByKey(currentStep.TestCase.ModuleId.Value).Id.ToString();
                    currentNode.ParentNode.ParentNode.Url = string.Format("ModuleDetails.aspx?moduleId={0}", stepController.uof.GetRepository<Module>().FindByKey(currentStep.TestCase.ModuleId.Value).Id);
                }
                else
                {
                    currentNode.ParentNode.ParentNode.Title = "No related Module";
                }
               
                currentNode.ParentNode.ParentNode.ParentNode.Title = "ProjectId" + currentStep.TestCase.ProjectId;
                currentNode.ParentNode.ParentNode.ParentNode.Url = string.Format("ProjectDetails.aspx?projectId={0}", currentStep.TestCase.ProjectId);
                // Use the changed one in the breadcrumb.
                return currentNode;
            }
            return null;
        }

        protected void btn_AddAttachment(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/Views/AttachmentCreate.aspx?stepId={0}", currentStep.Id));
            
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<ManTestAppWebForms.Models.Attachment> gvAttachments_GetData()
        {
            int stepid;
            Int32.TryParse(stepId, out stepid);
            return stepController.uof.GetRepository<Attachment>().All().Where(a => a.StepId == stepid).AsQueryable();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void gvAttachments_DeleteItem(int id)
        {
            stepController.uof.GetRepository<Attachment>().Delete(id);
            stepController.uof.Save();
            ShowImageFiles();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void gvAttachments_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.Attachment item = null;
            item = stepController.uof.GetRepository<Attachment>().FindByKey(id);
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (TryUpdateModel(item))
            {
                
            }
            if (ModelState.IsValid)
            {
                stepController.uof.GetRepository<Attachment>().Update(item);
                stepController.uof.Save();
            }
        }

        protected void gvAttachments_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fullFileName = (int)gvAttachments.SelectedValue;
            var filename = stepController.uof.GetRepository<Attachment>().FindByKey(fullFileName).FileName;
            string completeUrl = Server.MapPath("~/Data/") + filename;
            string contents = File.ReadAllText(completeUrl);
            //FileContents.Text = contents;
        }
    }
}