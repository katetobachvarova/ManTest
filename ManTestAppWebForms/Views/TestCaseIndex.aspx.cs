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
    public partial class TestCaseIndex : System.Web.UI.Page
    {
        private TestCaseController testCaseController;

        protected void Page_Load(object sender, EventArgs e)
        {
            testCaseController = new TestCaseController();
            AddNewTestCaseLink.Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
        }

        public IQueryable<ManTestAppWebForms.Models.TestCase> gvTestCases_GetData()
        {
            return testCaseController.GetAll();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void gvTestCases_DeleteItem(int id)
        {
            testCaseController.Delete(id);
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void gvTestCases_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.TestCase item = null;
            item = testCaseController.FindById(id);
            if (item == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            if (Session["projectIdDropDown"] != null)
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
            if (ModelState.IsValid)
            {
                testCaseController.Update(item);
            }
        }

        protected void gvTestCases_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvTestCases.EditIndex)
            {
                gvTestCases.Columns[5].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                gvTestCases.Columns[6].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                gvTestCases.Columns[7].Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
            }
        }

        protected void gvTestCases_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList dropProject = (DropDownList)e.Row.FindControl("DropDownListProject");
                    DropDownList dropModule = (DropDownList)e.Row.FindControl("DropDownListModule");
                    Project project = (e.Row.DataItem as TestCase).Project;
                    Module module = (e.Row.DataItem as TestCase).Module;
                    IEnumerable<Project> projects = testCaseController.GetAllProjects();
                    foreach (var item in projects)
                    {
                        if (item.Id == project.Id)
                        {
                            dropProject.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString(), Selected = true });
                            continue;
                        }
                        dropProject.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString(), Selected = false });
                    }
                    IEnumerable<Module> modules = testCaseController.GetRelatedModules(project.Id).ToList();
                    if (modules != null && modules.Any())
                    {
                        dropModule.Items.Add(new ListItem() { Text = "Select Module", Value = "", Selected = false });
                    }
                    else
                    {
                        dropModule.Items.Add(new ListItem() { Text = "No Module assosiated with this Project", Value = "", Selected = true });
                       
                    }
                    foreach (var item in modules)
                    {
                        if (module != null && item.Id == module.Id)
                        {
                            dropModule.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString(), Selected = true });
                            continue;
                        }
                        dropModule.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString(), Selected = false });
                    }
                }
            }
        }

        protected void DropDownListProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["projectIdDropDown"] = (sender as DropDownList).SelectedValue;
            int projectid;
            Int32.TryParse(Session["projectIdDropDown"].ToString(), out projectid);
            Session["moduleIdDropDown"] = "";
            DropDownList DropDownListModules = (DropDownList)(sender as DropDownList).Parent.Parent.FindControl("DropDownListModule");
            DropDownListModules.Items.Clear();
            IEnumerable<Module> modules = testCaseController.GetRelatedModules(projectid).ToList();
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

        protected void DropDownListModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["moduleIdDropDown"] = (sender as DropDownList).SelectedValue;
        }
    }
}