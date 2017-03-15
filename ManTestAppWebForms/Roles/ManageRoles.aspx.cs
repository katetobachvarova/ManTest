using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.DataAccess;
using ManTestAppWebForms.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Roles
{
    public partial class ManageRoles : System.Web.UI.Page
    {
        private ApplicationDbContext applicationDbContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            applicationDbContext = new ApplicationDbContext();
            if (!Page.IsPostBack)
                DisplayRolesInGrid();
        }

        private void DisplayRolesInGrid()
        {
            //RoleList.DataSource = System.Web.Security.Roles.GetAllRoles();
            RoleList.DataSource = applicationDbContext.Roles.ToList();

            RoleList.DataBind();
        }


        protected void CreateRoleButton_Click(object sender, EventArgs e)
        {
            string newRoleName = RoleName.Text.Trim();

            //if (!System.Web.Security.Roles.RoleExists(newRoleName))
            //{
            //    // Create the role
            //    System.Web.Security.Roles.CreateRole(newRoleName);

            //    // Refresh the RoleList Grid
            //    DisplayRolesInGrid();
            //}



            // Access the application context and create result variables.
           
            IdentityResult IdRoleResult;

            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(applicationDbContext);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Then, you create the "canEdit" role if it doesn't already exist.
            if (!roleMgr.RoleExists(newRoleName))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = newRoleName });
                applicationDbContext.SaveChangesAsync();
                
                DisplayRolesInGrid();
            }
            RoleName.Text = string.Empty;

            

         


        }

        protected void RoleList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the RoleNameLabel
            Label RoleNameLabel = RoleList.Rows[e.RowIndex].FindControl("RoleNameLabel") as Label;

            // Delete the role
            System.Web.Security.Roles.DeleteRole(RoleNameLabel.Text, false);

            // Rebind the data to the RoleList grid
            DisplayRolesInGrid();
        }
    }
}