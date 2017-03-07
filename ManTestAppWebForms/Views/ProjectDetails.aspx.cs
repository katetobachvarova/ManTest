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



        protected void Page_Load(object sender, EventArgs e)
        {
            projectId = Request.QueryString["projectId"];
            this.projectController = new ControllerBase<Project>();
            if (!IsPostBack)
            {
                PopulateTreeView(GetModules());
            }
        }

        public void ListView1_InsertItem()
        {
            
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable ListViewModules_GetData()
        {
            int projectid;
            Int32.TryParse(projectId, out projectid);

            return projectController.uof.GetRepository<Module>().All().Where(i => i.ProjectId == projectid).AsQueryable();
        }

        public IEnumerable<Module> GetModules()
        {
            int projectid;
            Int32.TryParse(projectId, out projectid);

            return projectController.uof.GetRepository<Module>().All().Where(i => i.ProjectId == projectid);
        }

        public void PopulateTreeView(IEnumerable<Module> listModules)
        {
            int projectid;
            Int32.TryParse(projectId, out projectid);

            TreeNode projectNode = new TreeNode(projectController.FindById(projectid)?.Title + "Project");
            TreeViewModules.Nodes.Add(projectNode);
            if (listModules != null && listModules.Any())
            {
                foreach (var module in listModules)
                {
                    TreeNode childNodeModule = new TreeNode(module.Title + "Module", module.Id.ToString());
                    projectNode.ChildNodes.Add(childNodeModule);
                    IEnumerable<TestCase> listTestCase = projectController.uof.GetRepository<TestCase>().All().Where(i => i.ModuleId == module.Id);
                    foreach (var testCase in listTestCase)
                    {
                        TreeNode childNodeTestCase = new TreeNode(testCase.Title + "Test Case", testCase.Id.ToString());
                        childNodeTestCase.NavigateUrl = string.Format("ModuleCreate.aspx?projectId={0}", projectId);
                        IEnumerable<Step> liststeps = projectController.uof.GetRepository<Step>().All().Where(s => s.TestCaseId == testCase.Id);
                        foreach (var step in liststeps)
                        {
                            TreeNode childNodeStep = new TreeNode(step.Title + "Step", step.Id.ToString());
                            childNodeStep.NavigateUrl = string.Format("StepDetails.aspx?stepId={0}", step.Id);
                            IEnumerable<Attachment> listAttchments = projectController.uof.GetRepository<Attachment>().All().Where(a => a.StepId == step.Id);
                            foreach (var attachment in listAttchments)
                            {
                                TreeNode childNodeAttachment = new TreeNode(attachment.Id + "Attachment", attachment.Id.ToString());
                                childNodeAttachment.NavigateUrl = string.Format("AttachmentDetails.aspx?attachmentId={0}", attachment.Id);
                                childNodeStep.ChildNodes.Add(childNodeAttachment);
                            }
                            childNodeTestCase.ChildNodes.Add(childNodeStep);
                        }

                        childNodeModule.ChildNodes.Add(childNodeTestCase);
                    }

                }
            }

            IEnumerable<TestCase> listTestCaseNoRelatedModule = projectController.uof.GetRepository<TestCase>().All().Where(i => i.ProjectId == projectid && i.ModuleId == null);
            foreach (var testCase in listTestCaseNoRelatedModule)
            {
                TreeNode childNodeTestCase = new TreeNode(testCase.Title + "Test Case", testCase.Id.ToString());
                childNodeTestCase.NavigateUrl = string.Format("ModuleCreate.aspx?projectId={0}", projectId);
                IEnumerable<Step> liststeps = projectController.uof.GetRepository<Step>().All().Where(s => s.TestCaseId == testCase.Id);
                foreach (var step in liststeps)
                {
                    TreeNode childNodeStep = new TreeNode(step.Title + "Step", step.Id.ToString());
                    childNodeStep.NavigateUrl = string.Format("StepDetails.aspx?stepId={0}", step.Id);
                    IEnumerable<Attachment> listAttchments = projectController.uof.GetRepository<Attachment>().All().Where(a => a.StepId == step.Id);
                    foreach (var attachment in listAttchments)
                    {
                        TreeNode childNodeAttachment = new TreeNode(attachment.Id + "Attachment", attachment.Id.ToString());
                        childNodeAttachment.NavigateUrl = string.Format("AttachmentDetails.aspx?attachmentId={0}", attachment.Id);
                        childNodeStep.ChildNodes.Add(childNodeAttachment);
                    }
                    childNodeTestCase.ChildNodes.Add(childNodeStep);
                }

                projectNode.ChildNodes.Add(childNodeTestCase);
            }
        }
    }
}