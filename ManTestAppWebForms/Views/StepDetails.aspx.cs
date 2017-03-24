using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Views
{
    public partial class StepDetails : System.Web.UI.Page
    {
        private StepController stepController;
        private string stepId;
        public Step currentStep;

        protected void Page_Load(object sender, EventArgs e)
        {
            stepController = new StepController();
            stepId = Request.QueryString["stepId"];
            int stepid;
            if (Int32.TryParse(stepId, out stepid))
            {
                currentStep = stepController.FindById(stepid);
            }
            if (!IsPostBack && currentStep != null)
            {
                ShowImageFiles();
                SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);
            }
        }

        private void ShowImageFiles()
        {
            if (currentStep != null)
            {
                IEnumerable<Attachment> attachments = stepController.GetRelatedAttachments(currentStep.Id);
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
             SiteMap.SiteMapResolve -= new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

            if (SiteMap.CurrentNode != null)
            {
                SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
                currentNode.Title = "Step " + currentStep.Title;
                currentNode.ParentNode.Title = "TestCase " + currentStep.TestCase.Title;
                currentNode.ParentNode.Url = string.Format("TestCaseDetails.aspx?testCaseId={0}", currentStep.TestCaseId);

                if (currentStep.TestCase.ModuleId.HasValue)
                {
                    currentNode.ParentNode.ParentNode.Title = "Module " + currentStep.TestCase.Module.Title;
                    currentNode.ParentNode.ParentNode.Url = string.Format("ModuleDetails.aspx?moduleId={0}", currentStep.TestCase.ModuleId);
                }
                else
                {
                    currentNode.ParentNode.ParentNode.Title = "No related Module";
                }
                currentNode.ParentNode.ParentNode.ParentNode.Title = "Project " + currentStep.TestCase.Project.Title;
                currentNode.ParentNode.ParentNode.ParentNode.Url = string.Format("ProjectDetails.aspx?projectId={0}", currentStep.TestCase.ProjectId);
                return currentNode;
            }
            return null;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        protected void btn_AddAttachment(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/Views/AttachmentCreate.aspx?stepId={0}", currentStep.Id));
        }

        public IQueryable<ManTestAppWebForms.Models.Attachment> gvAttachments_GetData()
        {
            if (currentStep != null)
            {
                return stepController.GetRelatedAttachments(currentStep.Id).AsQueryable();
            }
            return null;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void gvAttachments_DeleteItem(int id)
        {
            stepController.DeleteAttachment(id);
            Response.Redirect(string.Format("~/Views/StepDetails.aspx?stepId={0}", currentStep.Id));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void FormViewStep_DeleteItem(int id)
        {
            stepController.Delete(id);
            Step deleted = currentStep;
            if (deleted != null)
            {
                Response.Redirect(String.Format("TestCaseDetails.aspx?testCaseId={0}", currentStep.TestCaseId));
            }
        }

        public ManTestAppWebForms.Models.Step FormViewStep_GetItem([QueryString]int? stepId)
        {
            if (stepId.HasValue)
            {
                gvAttachments.DataBind();
                return stepController.FindById(stepId.Value);
            }
            return null;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void FormViewStep_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.Step item = null;
            item = stepController.FindById(id);
            if (item == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                stepController.Update(item);
                Response.Redirect(String.Format("StepDetails.aspx?stepId={0}", item.Id));
            }
        }

        protected void btn_StepCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("StepDetails.aspx?stepId={0}", currentStep.Id));
        }

        protected void FormViewStep_DataBound(object sender, EventArgs e)
        {
            var formView = (FormViewStep as FormView);
            if (formView != null)
            {
                Button edit = (Button)FormViewStep.FindControl("ButtonEdit");
                if (edit != null)
                {
                    edit.Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                }
                Button delete = (Button)FormViewStep.FindControl("ButtonDelete");
                if (edit != null)
                {
                    delete.Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                }
                btnAddAttachment.Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
            }
        }

        protected void gvAttachments_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvAttachments.EditIndex)
            {
                gvAttachments.Columns[3].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
            }
        }
    }
}