using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.DataAccess;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Views
{
    public partial class ModuleCreate : System.Web.UI.Page
    {
        private ControllerBase<Module> moduleController;
        private string projectId;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.moduleController = new ControllerBase<Module>();
            projectId = Request.QueryString["projectId"];
        }

        public void InsertItem_Module()
        {
            var item = new ManTestAppWebForms.Models.Module();
            int id;
            Int32.TryParse(projectId, out id);
            item.ProjectId = id;
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                moduleController.Insert(item);
            }
        }

        protected void ItemInserted_Module(object sender, FormViewInsertedEventArgs e)
        {
            if (ModelState.IsValid)
            {
                Response.Redirect("~/Views/ModuleIndex.aspx");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ModuleIndex.aspx");
        }
    }
}