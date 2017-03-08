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
            Page.DataBind();
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
            Response.Redirect(string.Format("~/Views/AttechmentCreate.aspx?stepId={0}", Step.Id));
            
        }
    }
}