using ManTestAppWebForms.Controllers;
using ManTestAppWebForms.DataAccess;
using ManTestAppWebForms.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTestAppWebForms.Roles
{
    public partial class UserRoles : System.Web.UI.Page
    {
        private ApplicationDbContext applicationDbContext;
        UserManager<ApplicationUser> userMgr;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            applicationDbContext = new ApplicationDbContext();
            userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(applicationDbContext));
            userMgr.UserValidator = new UserValidator<ApplicationUser>(userMgr) { AllowOnlyAlphanumericUserNames = false };
        }

        public IQueryable<ManTestAppWebForms.Models.ApplicationUser> GridViewUsers_GetData()
        {
            return applicationDbContext.Users.AsQueryable();
        }

        protected void GridViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    string  role = (e.Row.DataItem as ApplicationUser).Role; 

                    DropDownList drop = (DropDownList)e.Row.FindControl("DropDownListRoles");
                    if (drop != null)
                    {
                        drop.DataSource = applicationDbContext.Roles.ToList();
                        drop.DataBind();
                        drop.SelectedValue = applicationDbContext.Roles.ToList().Where(r => r.Name == role).ElementAtOrDefault(0)?.Id;
                    }
                }
            }
        }

        public void GridViewUsers_UpdateUser(string email)
        {
            ApplicationUser item = null;
            item = userMgr.FindByEmail(email);
            if (item == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", email));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                item.Roles.Clear();
                userMgr.AddToRole(item.Id, item.Role);
                userMgr.Update(item);
            }
        }

        protected void GridViewUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string role = (GridViewUsers.Rows[e.RowIndex].FindControl("DropDownListRoles") as DropDownList).SelectedItem.Text;
            string userEmail = GridViewUsers.DataKeys[e.RowIndex].Value.ToString();
            userMgr.FindByEmail(userEmail).Role = role;
        }
    }
}