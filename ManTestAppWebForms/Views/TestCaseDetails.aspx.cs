﻿using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Views
{
    public partial class TestCaseDetails : System.Web.UI.Page
    {
        private TestCaseController testCaseController;
        public TestCase currentTestCase;

        protected void Page_Load(object sender, EventArgs e)
        {
            testCaseController = new TestCaseController();
            string testCaseId = Request.QueryString["testCaseId"];
            int testCaseid;
            if (Int32.TryParse(testCaseId, out testCaseid))
            {
                currentTestCase = testCaseController.FindById(testCaseid);
                if (currentTestCase != null && currentTestCase.Steps != null && currentTestCase.Steps.Any())
                {
                    LabelRelatedSteps.Text = "Related Steps";
                }
            }
            if (!IsPostBack && currentTestCase != null)
            {
                ShowBreadCrumb(currentTestCase);
            }
        }

        private void ShowBreadCrumb(TestCase currentTestCase)
        {
            HyperLink linkForProject = new HyperLink();
            linkForProject.Text = string.Format("{0} >", currentTestCase.Project.Title);
            linkForProject.NavigateUrl = string.Format("ProjectDetails.aspx?projectId={0}", currentTestCase.ProjectId);
            PlaceHolderForLinks.Controls.Add(linkForProject);
            if (currentTestCase.ModuleId.HasValue)
            {
                HyperLink linkForModule = new HyperLink();
                linkForModule.Text = string.Format(" {0} >", currentTestCase.Module.Title);
                linkForModule.NavigateUrl = string.Format("ModuleDetails.aspx?moduleId={0}", currentTestCase.ModuleId);
                PlaceHolderForLinks.Controls.Add(linkForModule);
            }
            Label lblTC = new Label();
            lblTC.Text = string.Format(" {0}", currentTestCase.Title);
            PlaceHolderForLinks.Controls.Add(lblTC);
            PlaceHolderForLinks.DataBind();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        protected void AddStepToTestCase(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("StepCreate.aspx?testCaseId={0}", currentTestCase.Id));
        }


        private void ShowImageFiles(Step currentStep, PlaceHolder PlaceHolderForImages)
        {
            if (currentStep != null)
            {
                IEnumerable<Attachment> attachments = testCaseController.GetRelatedAttachments(currentStep.Id);
                foreach (var item in attachments)
                {
                    if (item.FileName.EndsWith(".jpg")
                        || item.FileName.EndsWith(".jpeg")
                        || item.FileName.EndsWith(".png")
                        || item.FileName.EndsWith(".bmp")
                        || item.FileName.EndsWith(".gif"))
                    {
                        HyperLink link = new HyperLink();
                        link.NavigateUrl = "../Data/" + item.FileName;
                        link.ImageHeight = 100;
                        link.ApplyStyle(new Style() { CssClass = "forselect" });
                        Image img = new Image();
                        img.ApplyStyle(new Style() { CssClass = "images" });
                        img.Width = 100;
                        img.Height = 100;
                        img.ImageUrl = "../Data/" + item.FileName;
                        img.ID = item.Id.ToString();
                        link.Controls.Add(img);
                        PlaceHolderForImages.Controls.Add(link);
                        PlaceHolderForImages.DataBind();
                    }
                }
            }
        }

        //SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        //{
        //    SiteMap.SiteMapResolve -= new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);
        //    if (SiteMap.CurrentNode != null)
        //    {
        //        //SiteMapPath1.PathSeparator = ">";
        //        SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
        //        currentNode.Title = "TestCase " + currentTestCase?.Title;
        //        if (currentTestCase.ModuleId.HasValue)
        //        {
        //            currentNode.ParentNode.Title = "Module " + currentTestCase.Module.Title;
        //            currentNode.ParentNode.Url = string.Format("ModuleDetails.aspx?moduleId={0}", currentTestCase.ModuleId);
        //            currentNode.ParentNode.ParentNode.Title = "Project " + currentTestCase.Project.Title;
        //            currentNode.ParentNode.ParentNode.Url = string.Format("ProjectDetails.aspx?projectId={0}", currentTestCase.ProjectId);
        //        }
        //        else
        //        {
        //            //currentNode.ParentNode.Title = "No related Module";
        //            currentNode.ParentNode.Title = "Project " + currentTestCase.Project.Title;
        //            currentNode.ParentNode.Url = string.Format("ProjectDetails.aspx?projectId={0}", currentTestCase.ProjectId);
        //        }
        //        return currentNode;
        //    }
        //    return null;
        //}

        public IQueryable<ManTestAppWebForms.Models.Step> ListViewSteps_GetData()
        {
            if (currentTestCase != null)
            {
                return testCaseController.GetRelatedSteps(currentTestCase.Id);
            }
            return null;
        }

        protected void ListViewSteps_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType != ListViewItemType.DataItem)
            {
                return;
            }
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            Step currentStep = (Step)dataItem.DataItem;
            GridView gridView = (GridView)e.Item.FindControl("GridViewAttachments_GetData");
            gridView.DataSource = testCaseController.GetRelatedAttachments(currentStep.Id);
            gridView.DataBind();
            PlaceHolder placeholderforimages = (PlaceHolder)e.Item.FindControl("PlaceHolderForImages");
            ShowImageFiles(currentStep, placeholderforimages);
        }

        protected void btn_StepDetails_Click(object sender, EventArgs e)
        {
            var stepId = (sender as Button).CommandArgument;
            Response.Redirect(String.Format("StepDetails.aspx?stepId={0}", stepId));

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void FormViewCurrentTestCase_UpdateItem(int id)
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
            if (item.ProjectId == 0)
            {
                ModelState.AddModelError("ProjectId", "Project is required");
            }
             if (ModelState.IsValid)
            {
                testCaseController.Update(item);
                ShowBreadCrumb(item);
            }
        }

        protected void btn_TestCaseCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("TestCaseDetails.aspx?testCaseId={0}", currentTestCase.Id));
        }

        public ManTestAppWebForms.Models.TestCase FormViewCurrentTestCase_GetItem([QueryString]int? testCaseId)
        {
            if (testCaseId.HasValue)
            {
                ListViewSteps.DataBind();
                return testCaseController.FindById(testCaseId.Value);
            }
            return null;
        }

        protected void FormViewCurrentTestCase_DataBound(object sender, EventArgs e)
        {
            var formView = (FormViewCurrentTestCase as FormView);
            if (formView != null)
            {
                DropDownList dropListProjects = FormViewCurrentTestCase.FindControl("DropDownListProjectsEdit") as DropDownList;
                if (dropListProjects != null)
                {
                    IEnumerable<Project> projects = this.testCaseController.GetAllProjects();
                    foreach (var item in projects)
                    {
                        if (item.Id == currentTestCase.ProjectId)
                        {
                            dropListProjects.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString(), Selected = true });
                            continue;
                        }
                        dropListProjects.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString(), Selected = false });
                    }
                }

                DropDownList dropListsModules = (DropDownList)FormViewCurrentTestCase.FindControl("DropDownListModulesEdit");

                if (dropListsModules != null)
                {
                    IEnumerable<Module> modules = this.testCaseController.GetRelatedModules(currentTestCase.ProjectId);
                    foreach (var item in modules)
                    {
                        if (item.Id == currentTestCase.ModuleId)
                        {
                            dropListsModules.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString(), Selected = true });
                            continue;
                        }
                        dropListsModules.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString(), Selected = false });
                    }
                    if (currentTestCase.ModuleId == null)
                    {
                        dropListsModules.Items.Add(new ListItem() { Text = "No related Module", Value = null, Selected = true });
                    }
                    else
                    {
                        dropListsModules.Items.Add(new ListItem() { Text = "No related Module", Value = null, Selected = false });
                    }
                }
                Button edit = (Button)FormViewCurrentTestCase.FindControl("ButtonEdit");
                if (edit != null)
                {
                    edit.Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                }
                Button delete = (Button)FormViewCurrentTestCase.FindControl("ButtonDelete");
                if (edit != null)
                {
                    delete.Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                }
                Button addStep = (Button)FormViewCurrentTestCase.FindControl("ButtonAddStep");
                if (addStep != null)
                {
                    addStep.Visible = (User.IsInRole("Admin") || User.IsInRole("QA"));
                }
            }
        }

        protected void DropDownListProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["projectIdDropDown"] = (sender as DropDownList).SelectedValue;
            int projectid;
            Int32.TryParse(Session["projectIdDropDown"].ToString(), out projectid);
            Session["moduleIdDropDown"] = "";
            DropDownList dropModules =(DropDownList)FormViewCurrentTestCase.FindControl("DropDownListModulesEdit");
            if (dropModules != null)
            {
                dropModules.Enabled = true;
                dropModules.Items.Clear();
                IEnumerable<Module> modules = this.testCaseController.uof.GetRepository<Module>().All().Where(m => m.ProjectId == projectid);
                if (modules != null && modules.Any())
                {
                    dropModules.Items.Add(new ListItem() { Text = "Select Module", Value = "", Selected = false });
                }
                else
                {
                    dropModules.Items.Add(new ListItem() { Text = "No Module assosiated with this Project", Value = "", Selected = false });
                }
                foreach (var item in modules)
                {
                    dropModules.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString(), Selected = false });
                }
            }
        }

        protected void DropDownListModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["moduleIdDropDown"] = (sender as DropDownList).SelectedValue;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        [PrincipalPermission(SecurityAction.Demand, Role = "QA")]
        public void FormViewCurrentTestCase_DeleteItem(int id)
        {
            testCaseController.Delete(id);
            TestCase deleted = currentTestCase;
            if (deleted != null && currentTestCase.ModuleId != null)
            {
                Response.Redirect(String.Format("ModuleDetails.aspx?moduleId={0}", currentTestCase.ModuleId));
            }
            else
            {
                Response.Redirect(String.Format("ProjectDetails.aspx?projectId={0}", currentTestCase.ProjectId));
            }
        }
    }
}