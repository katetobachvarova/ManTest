using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Views
{
    public partial class AttechmentCreate : System.Web.UI.Page
    {
        private ControllerBase<Attachment> attachementController;
        private string stepId;


        protected void Page_Load(object sender, EventArgs e)
        {
            this.attachementController = new ControllerBase<Attachment>();
            stepId = Request.QueryString["stepId"];
        }

        protected void btn_Upload_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                try
                {
                    long len = FileUploadControl.FileContent.Length;
                    string filename = Path.GetFileName(FileUploadControl.FileName);
                    string completeUrl = Server.MapPath("~/Data/") + filename;
                    FileUploadControl.SaveAs(completeUrl);
                    StatusLabel.Text = "Upload status: File uploaded!";
                    Attachment att = new Attachment();
                    int stepid;
                    if (Int32.TryParse(stepId, out stepid))
                    {
                        att.StepId = stepid;
                        att.FileName = filename;
                        att.Url = completeUrl;
                    }
                    attachementController.Insert(att);
                    Response.Redirect(string.Format("~/Views/StepDetails.aspx?stepId={0}", stepId));

                }
                catch (ThreadAbortException ex)
                {
                    //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }
    }
}