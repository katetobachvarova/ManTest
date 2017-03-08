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
        public Step Step;

        protected void Page_Load(object sender, EventArgs e)
        {
            stepController = new ControllerBase<Step>();
            stepId = Request.QueryString["stepId"];
            int stepid;
            if (Int32.TryParse(stepId, out stepid))
            {
                Step = stepController.FindById(stepid);
            }
            else
            {
                Step = new Step();
            }
            if (!IsPostBack)
            {
               Page.DataBind();
            }
           
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable ListViewAttachments_GetData()
        {
            int stepid;
            Int32.TryParse(stepId, out stepid);
             return stepController.uof.GetRepository<Attachment>().All().Where(a => a.StepId == stepid).AsQueryable();
        }

        protected void btn_AddAttachment(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/Views/AttachmentCreate.aspx?stepId={0}", Step.Id));
            
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