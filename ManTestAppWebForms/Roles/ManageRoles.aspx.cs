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
        private RoleStore<IdentityRole> roleStore;
        private RoleManager<IdentityRole> roleMgr;
        private UserManager<ApplicationUser> userMgr;


        protected void Page_Load(object sender, EventArgs e)
        {
            applicationDbContext = new ApplicationDbContext();
            roleStore = new RoleStore<IdentityRole>(applicationDbContext);
            roleMgr = new RoleManager<IdentityRole>(roleStore);
            userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(applicationDbContext));
            if (!Page.IsPostBack)
                DisplayRolesInGrid();
        }

        private void DisplayRolesInGrid()
        {
            RoleList.DataSource = applicationDbContext.Roles.ToList();
            RoleList.DataBind();
        }


        protected void CreateRoleButton_Click(object sender, EventArgs e)
        {
            string newRoleName = RoleName.Text.Trim();
            IdentityResult IdRoleResult;
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
            Label RoleNameLabel = RoleList.Rows[e.RowIndex].FindControl("RoleNameLabel") as Label;

            if (roleMgr.RoleExists(RoleNameLabel.Text))
            {
                roleMgr.Delete(roleMgr.FindByName(RoleNameLabel.Text));
                applicationDbContext.SaveChangesAsync();
                DisplayRolesInGrid();
            }
            foreach (var item in userMgr.Users.Where(u => u.Role == RoleNameLabel.Text))
            {
                item.Role = null;
            }
            applicationDbContext.SaveChangesAsync();

        }
    }
}