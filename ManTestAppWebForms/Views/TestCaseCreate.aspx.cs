using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Views
{
    [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
    [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
    public partial class TestCaseCreate : System.Web.UI.Page
    {
        private TestCaseController testCaseController;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.testCaseController = new TestCaseController();
            if (!IsPostBack)
            {
                IEnumerable<Project> projects = this.testCaseController.GetAllProjects();
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
            if (Session["projectIdDropDown"] !=null)
            {
                int projectid;
                Int32.TryParse(Session["projectIdDropDown"].ToString(), out projectid);
                item.ProjectId = projectid;
            }
            if (Session["moduleIdDropDown"] != null)
            {
                int moduleid;
                if (Int32.TryParse(Session["moduleIdDropDown"].ToString(), out moduleid) && moduleid != 0)
                {
                    item.ModuleId = moduleid;
                }
                else
                {
                    item.ModuleId = null;
                }
            }
            TryUpdateModel(item);
            if (item.ProjectId == 0)
            {
                ModelState.AddModelError("ProjectId", "The Project field is required.");
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

        protected void DropDownListProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["projectIdDropDown"] = (sender as DropDownList).SelectedValue;
            int projectid;
            Int32.TryParse(Session["projectIdDropDown"].ToString(), out projectid);
            DropDownListModules.Enabled = true;
            DropDownListModules.Items.Clear();
            IEnumerable<Module> modules = this.testCaseController.GetRelatedModules(projectid);
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