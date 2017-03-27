using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.Models;
using System;
using System.IO;
using System.Security.Permissions;
using System.Threading;

namespace ManTestAppWebForms.Views
{
    [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
    [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
    public partial class AttechmentCreate : System.Web.UI.Page
    {
        private AttachmentController attachementController;
        private string stepId;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.attachementController = new AttachmentController();
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
                    if (!string.IsNullOrEmpty(Request.QueryString["stepId"]) && Int32.TryParse(Request.QueryString["stepId"], out stepid) && attachementController.FindStep(stepid) != null)
                    {
                        att.StepId = stepid;
                        att.FileName = filename;
                        att.Url = completeUrl;
                        attachementController.Insert(att);
                        Response.Redirect(string.Format("~/Views/StepDetails.aspx?stepId={0}", stepId));
                    }
                    else
                    {
                        StatusLabel.Text = "The file could not be uploaded. The following error occured: Step not found!";
                    }
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