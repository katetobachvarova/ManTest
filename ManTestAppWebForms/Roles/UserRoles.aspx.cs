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
            if (!IsPostBack)
            {
            }
            
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
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

        protected void DropDownListRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            var role = (sender as DropDownList).SelectedItem.Text;

            IdentityResult IdUserResult;
            var t = 1;
            // Create a UserManager object based on the UserStore object and the ApplicationDbContext
            // object. Note that you can create new objects and use them as parameters in
            // a single line of code, rather than using multiple lines of code, as you did
            // for the RoleManager object.
            
            //var appUser = new ApplicationUser
            //{
            //    UserName = "canEditUser@wingtiptoys.com",
            //    Email = "canEditUser@wingtiptoys.com"
            //};
            //IdUserResult = userMgr.Create(appUser, "Pa$$word1");

            // If the new "canEdit" user was successfully created, 
            // add the "canEdit" user to the "canEdit" role. 
            //if (!userMgr.IsInRole(userMgr.FindByEmail("canEditUser@wingtiptoys.com").Id, "canEdit"))
            //{
            //    IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("canEditUser@wingtiptoys.com").Id, "canEdit");
            //}
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewUsers_UpdateItem(int? id)
        {
            ManTestAppWebForms.Models.ApplicationUser item = null;
            
            item = userMgr.FindById(id.ToString());

            // Load the item here, e.g. item = MyDataLayer.Find(id);
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
                userMgr.Update(item);
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewUsers_UpdateItem1(string email)
        {
            ManTestAppWebForms.Models.ApplicationUser item = null;
            item = userMgr.FindByEmail(email);

            // Load the item here, e.g. item = MyDataLayer.Find(id);
            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", email));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                item.Roles.Clear();
                var IdUserResult = userMgr.AddToRole(userMgr.FindByEmail(item.Email).Id, userMgr.FindByEmail(item.Email).Role);
                // Save changes here, e.g. MyDataLayer.SaveChanges();
                userMgr.Update(item);
                var test = item.Roles;
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