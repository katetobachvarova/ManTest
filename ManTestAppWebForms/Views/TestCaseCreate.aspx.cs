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
    public partial class TestCaseCreate : System.Web.UI.Page
    {
        private ControllerBase<TestCase> testCaseController;
        private string projectId;
        private string moduleId;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.testCaseController = new ControllerBase<TestCase>();
            if (!IsPostBack)
            {
                moduleId = Request.QueryString["moduleId"];
                projectId = Request.QueryString["projectId"];
                IEnumerable<Project>  projects = this.testCaseController.uof.GetRepository<Project>().All();
                DropDownListProjects.Items.Clear();
                DropDownListProjects.Items.Add(new ListItem() { Text = "Select Project", Value = "", Selected = false });
                foreach (var item in projects)
                {
                    DropDownListProjects.Items.Add(new ListItem() {  Text = item.Title, Value = item.Id.ToString(), Selected = false});
                }
            }

        }

        public void InsertItem_TestCase()
        {
            var item = new ManTestAppWebForms.Models.TestCase();
            if (!string.IsNullOrEmpty(moduleId) || !string.IsNullOrEmpty(projectId))
            {
                int moduleid;
                Int32.TryParse(moduleId, out moduleid);
                item.ModuleId = moduleid;
                item.ProjectId = this.testCaseController.uof.GetRepository<Module>().FindByKey(moduleid).ProjectId;

                if (!string.IsNullOrEmpty(projectId))
                {
                    int projectid;
                    Int32.TryParse(projectId, out projectid);
                    item.ProjectId = projectid;
                }
            }
            else
            {
                if (Session["projectIdDropDown"] !=null)
                {
                    int projectid;
                    Int32.TryParse(Session["projectIdDropDown"].ToString(), out projectid);
                    item.ProjectId = projectid;
                }
                if (Session["moduleIdDropDown"] != null)
                {
                    int moduleid;
                    Int32.TryParse(Session["moduleIdDropDown"].ToString(), out moduleid);
                    item.ModuleId = moduleid;
                }
            }
            TryUpdateModel(item);
            if (item.ProjectId == 0)
            {
                ModelState.AddModelError("ProjectId", "Project is required");
            }
            if (ModelState.IsValid)
            {
                testCaseController.Insert(item);
            }
        }

        protected void ItemInserted_TestCase(object sender, FormViewInsertedEventArgs e)
        {
            if (ModelState.IsValid)
            {
                Response.Redirect("~/Views/TestCaseIndex.aspx");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/TestCaseIndex.aspx");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["projectIdDropDown"] = (sender as DropDownList).SelectedValue;
            int projectid;
            Int32.TryParse(Session["projectIdDropDown"].ToString(), out projectid);
            DropDownListModules.Enabled = true;
            DropDownListModules.Items.Clear();
            IEnumerable<Module> modules = this.testCaseController.uof.GetRepository<Module>().All().Where(m => m.ProjectId == projectid);
            if (modules != null && modules.Any())
            {
                DropDownListModules.Items.Add(new ListItem() { Text = "Select Module", Value = "", Selected = false });
            }
            else
            {
                DropDownListModules.Items.Add(new ListItem() { Text = "No Module assosiated with this Project", Value = "", Selected = false });
            }
            foreach (var item in modules)
            {
                DropDownListModules.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString(), Selected = false });
            }
        }

        protected void DropDownListModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["moduleIdDropDown"] = (sender as DropDownList).SelectedValue;
        }
    }
}