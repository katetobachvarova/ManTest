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
    public partial class AttachmentOpen : System.Web.UI.Page
    {
        private AttachmentController attachementController;
        private string attachmentId;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.attachementController = new AttachmentController();
            attachmentId = Request.QueryString["attachmentId"];
            int attachmentid;
            if (Int32.TryParse(attachmentId, out attachmentid))
            {
                Attachment attachment = attachementController.FindById(attachmentid);
                if (attachment != null)
                {
                    System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = "text/plain";
                    response.AddHeader("Content-Disposition", "attachment; filename=" + attachment.FileName + ";");
                    response.TransmitFile(Server.MapPath(string.Format("~/Data/{0}", attachment.FileName)));
                    response.Flush();
                    response.End();
                }
            }
            
        }
    }
}