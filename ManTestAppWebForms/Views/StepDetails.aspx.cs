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
    public partial class StepDetails : System.Web.UI.Page
    {
        private ControllerBase<Step> stepController;
        private string stepId;
        public Step currentStep;
        TreeView currentTreeView;
        ContentPlaceHolder content;

        protected void Page_Load(object sender, EventArgs e)
        {
            stepController = new ControllerBase<Step>();
            stepId = Request.QueryString["stepId"];
            int stepid;
            if (Int32.TryParse(stepId, out stepid))
            {
                currentStep = stepController.FindById(stepid);
            }
            else
            {
                currentStep = new Step();
            }
            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["stepId"]))
            {
                SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

            }


            //if (!IsPostBack)
            //{
            //    content = (ContentPlaceHolder)this.Master.FindControl("TreeContent");
            //    TreeView prtrv = (TreeView)Session["tree"];
            //    if (prtrv != null)
            //    {
            //        content.Controls.Add(prtrv);

            //    }
            //    currentTreeView = prtrv;
            //    Page.DataBind();
            //}
            //else
            //{
            //    ContentPlaceHolder content = (ContentPlaceHolder)this.Master.FindControl("TreeContent");
            //    TreeView prtrv = (TreeView)Session["tree"];
            //    content.Controls.Clear();
            //    if (prtrv != null && !content.Controls.Contains(prtrv))
            //    {
            //        content.Controls.Add(prtrv);

            //    }
            //    Page.DataBind();

            //}
            //if (currentTreeView != null)
            //{
            //    ContentPlaceHolder content = (ContentPlaceHolder)this.Master.FindControl("TreeContent");
            //    TreeView prtrv = (TreeView)Session["tree"];
            //    if (prtrv != null)
            //    {
            //        content.Controls.Add(prtrv);

            //    }
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
    }
}