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
    public partial class ProjectDetails : System.Web.UI.Page
    {
        private string projectId;
        private ControllerBase<Project> projectController;
        public Project currentProject;


        protected void Page_Load(object sender, EventArgs e)
        {
            projectId = Request.QueryString["projectId"];
            this.projectController = new ControllerBase<Project>();
            int projectid;

            if (Int32.TryParse(projectId, out projectid))
            {
                currentProject = projectController.FindById(projectid);
            }
            else
            {
                currentProject = new Project();
            }
            Label1.Text = currentProject.Title;
            
            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["projectId"]))
            {
                SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

            }
            //if (!IsPostBack)
            //{
            //    Page.DataBind();
            //}




        }
        SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            // Only need one execution in one request.
            SiteMap.SiteMapResolve -= new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

            if (SiteMap.CurrentNode != null)
            {
                // SiteMap.CurrentNode is readonly, so we need to clone one to operate.
                SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
                currentNode.Title = "ProjectId"+ projectId;
                //currentNode.Title = Request.QueryString["projectId"];

                // Use the changed one in the breadcrumb.
                return currentNode;
            }
            return null;
        }


        public IQueryable<Module> GetModules()
        {
            if (currentProject != null)
            {
                return projectController.uof.GetRepository<Module>().All().Where(i => i.ProjectId == currentProject.Id).AsQueryable();
            }
            return null;
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<ManTestAppWebForms.Models.TestCase> GetTestCases()
        {
            if (currentProject != null)
            {
                return projectController.uof.GetRepository<TestCase>().All().Where(i => i.ProjectId == currentProject.Id && i.ModuleId == null).AsQueryable();
            }
            return null;
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewModules_DeleteItem(int id)
        {
            projectController.uof.GetRepository<Module>().Delete(id);
            projectController.uof.Save();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewModules_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.Module item = null;
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            item = projectController.uof.GetRepository<Module>().FindByKey(id);
            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                // Save changes here, e.g. MyDataLayer.SaveChanges();
                projectController.uof.GetRepository<Module>().Update(item);
                projectController.uof.Save();
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewTestCases_DeleteItem(int id)
        {
            projectController.uof.GetRepository<TestCase>().Delete(id);
            projectController.uof.Save();

        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewTestCases_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.TestCase item = null;
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            item = projectController.uof.GetRepository<TestCase>().FindByKey(id);

            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                // Save changes here, e.g. MyDataLayer.SaveChanges();
                projectController.uof.GetRepository<TestCase>().Update(item);
                projectController.uof.Save();
            }
        }

        //public void PopulateTreeView(IEnumerable<Module> listModules)
        //{
        //    int projectid;
        //    Int32.TryParse(projectId, out projectid);

        //    TreeNode projectNode = new TreeNode(projectController.FindById(projectid)?.Title + "Project");
        //    TreeViewModules.Nodes.Add(projectNode);
        //    if (listModules != null && listModules.Any())
        //    {
        //        foreach (var module in listModules)
        //        {
        //            TreeNode childNodeModule = new TreeNode(module.Title + "Module", string.Format("ModuleDetails.aspx?moduleId={0}", module.Id));
        //            projectNode.ChildNodes.Add(childNodeModule);
        //            IEnumerable<TestCase> listTestCase = projectController.uof.GetRepository<TestCase>().All().Where(i => i.ModuleId == module.Id);
        //            foreach (var testCase in listTestCase)
        //            {
        //                TreeNode childNodeTestCase = new TreeNode(testCase.Title + "Test Case", string.Format("TestCaseDetails.aspx?testCaseId={0}", testCase.Id));
        //                //childNodeTestCase.NavigateUrl = string.Format("TestCaseDetails.aspx?testCaseId={0}", testCase.Id);
        //                IEnumerable<Step> liststeps = projectController.uof.GetRepository<Step>().All().Where(s => s.TestCaseId == testCase.Id);
        //                foreach (var step in liststeps)
        //                {
        //                    TreeNode childNodeStep = new TreeNode(step.Title + "Step", string.Format("StepDetails.aspx?stepId={0}", step.Id));
        //                    //childNodeStep.NavigateUrl = string.Format("StepDetails.aspx?stepId={0}", step.Id);
        //                    IEnumerable<Attachment> listAttchments = projectController.uof.GetRepository<Attachment>().All().Where(a => a.StepId == step.Id);
        //                    foreach (var attachment in listAttchments)
        //                    {
        //                        TreeNode childNodeAttachment = new TreeNode(attachment.Id + "Attachment", string.Format("AttachmentDetails.aspx?attachmentId={0}", attachment.Id));
        //                        //childNodeAttachment.NavigateUrl = string.Format("AttachmentDetails.aspx?attachmentId={0}", attachment.Id);
        //                        childNodeStep.ChildNodes.Add(childNodeAttachment);
        //                    }
        //                    childNodeTestCase.ChildNodes.Add(childNodeStep);
        //                }

        //                childNodeModule.ChildNodes.Add(childNodeTestCase);
        //            }

        //        }
        //    }

        //    IEnumerable<TestCase> listTestCaseNoRelatedModule = projectController.uof.GetRepository<TestCase>().All().Where(i => i.ProjectId == projectid && i.ModuleId == null);
        //    foreach (var testCase in listTestCaseNoRelatedModule)
        //    {
        //        TreeNode childNodeTestCase = new TreeNode(testCase.Title + "Test Case", string.Format("TestCaseDetails.aspx?testCaseId={0}", testCase.Id));
        //        //childNodeTestCase.NavigateUrl = string.Format("TestCaseDetails.aspx?testCaseId={0}", testCase.Id);
        //        IEnumerable<Step> liststeps = projectController.uof.GetRepository<Step>().All().Where(s => s.TestCaseId == testCase.Id);
        //        foreach (var step in liststeps)
        //        {
        //            TreeNode childNodeStep = new TreeNode(step.Title + "Step", string.Format("StepDetails.aspx?stepId={0}", step.Id));
        //            //childNodeStep.NavigateUrl = string.Format("StepDetails.aspx?stepId={0}", step.Id);
        //            IEnumerable<Attachment> listAttchments = projectController.uof.GetRepository<Attachment>().All().Where(a => a.StepId == step.Id);
        //            foreach (var attachment in listAttchments)
        //            {
        //                TreeNode childNodeAttachment = new TreeNode(attachment.Id + "Attachment", string.Format("AttachmentDetails.aspx?attachmentId={0}", attachment.Id));
        //                //childNodeAttachment.NavigateUrl = string.Format("AttachmentDetails.aspx?attachmentId={0}", attachment.Id);
        //                childNodeStep.ChildNodes.Add(childNodeAttachment);
        //            }
        //            childNodeTestCase.ChildNodes.Add(childNodeStep);
        //        }

        //        projectNode.ChildNodes.Add(childNodeTestCase);
        //    }
        //}

        protected void TreeViewModules_SelectedNodeChanged(object sender, EventArgs e)
        {
            string redirect = (sender as TreeView).SelectedNode.Value;
            Response.Redirect(redirect);
        }

        protected void TreeViewModules_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            string redirect = (sender as TreeView).SelectedNode.NavigateUrl;
        }
    }
}