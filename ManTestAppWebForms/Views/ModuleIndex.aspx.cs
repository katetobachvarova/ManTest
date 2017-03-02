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
    public partial class ModuleIndex : System.Web.UI.Page
    {
        private ControllerBase<Module> moduleController;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.moduleController = new ControllerBase<Module>();
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<ManTestAppWebForms.Models.Module> gv_ModuleIndex_GetData()
        {
            return moduleController.GetAll();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void gv_ModuleIndex_DeleteItem(int id)
        {
            moduleController.Delete(id);
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void gv_ModuleIndex_UpdateItem(int id)
        {
            ManTestAppWebForms.Models.Module item = null;
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
                moduleController.Update(item);
            }
        }

        protected void gv_ModuleIndex_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            gv_ModuleIndex.DataBind();
        }
    }
}