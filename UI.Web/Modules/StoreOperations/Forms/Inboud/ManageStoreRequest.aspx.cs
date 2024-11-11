using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using UI.Web.Admin.Controller;


namespace UI.Web.Modules.StoreOperations.Forms.Inboud
{
    public partial class ManageStoreRequest : BaseFormAdmin
    {
        #region "Page Members"
        public AssetsRepository objRepository = IoC.Resolve<AssetsRepository>();
        public InboundRepository objInboundRepository = IoC.Resolve<InboundRepository>();
        public string _PageTitle = Resources.Pages.InboundOperations;

        #endregion
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            lblerror.Text = "";
            btnSave.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {
                fillLookups();
                ViewState["itemID"] = "0";
                fillRequestItems();
            }
        }
        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            string script = "";
            try
            {
                AssetsEventTracking obj = new AssetsEventTracking();
                for (int i = 0; i <= grdItems.Items.Count - 1; i++)
                {

                    var selectedItem = (AssetsItemUnit)objRepository.getItemDetailsForEdit(ZeroIntergerIFNull(grdItems.Items[i].Cells[0].Text));
                    obj.AssetCode = selectedItem.Code;
                    obj.ActionDate = NullDateifEmpty(txtFromDate.Text);


                    obj.actionId = 3;// check In;
                    obj.statusId = 1;// Avilable ;
                    obj.ToLocationId = ZeroIntergerIFNull(selectedLocation.Value);

                    obj.Notes = txtNotes.Text;

                    obj.CreatedAt = DateTime.Now;
                    obj.CreatedBy = ZeroIntergerIFNull(ReadSession("userId").ToString());

                    objRepository.AddEventTracking(obj);


                    //update
                    selectedItem.LastEventTrackingId = obj.Code;
                    objInboundRepository.UpdateItemunit(selectedItem);
                }
                Session["selectedItems"] = null;
                fillRequestItems();
                ClearForm();

                script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }
            catch (Exception ex)
            {

                script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }
        }

        private void fillRequestItems()
        {

            if (Session["selectedItems"] != null)
            {
                var objList = (List<view_AssetsList>)Session["selectedItems"];
                decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdItems.PageSize));
                if ((c <= grdItems.CurrentPageIndex))
                {
                    grdItems.CurrentPageIndex = 0;
                }
                grdItems.Visible = true;
                grdItems.DataSource = objList;
                grdItems.DataBind();
                int _totalCount = objList.Count;
                pager1.ItemCount = objList.Count;
               // hdnItemCount.Value = objList.Count.ToString();

                if (objList != null && objList.Count > 0)
                {
                    pager1.Visible = true;

                }
                else { pager1.Visible = false; }

            }
            else
            {
                grdItems.Visible = false;
                pager1.Visible = false;
            }



        }
        protected void pager_Command(object sender, CommandEventArgs e)
        {
            Int32 currnetPageIndx = ((Int32)(e.CommandArgument));
            if ((currnetPageIndx <= 0))
            {
                currnetPageIndx = 1;
            }

            if ((currnetPageIndx > grdItems.PageCount))
            {
                currnetPageIndx = (grdItems.PageCount - 1);
            }

            pager1.CurrentIndex = currnetPageIndx;
            grdItems.CurrentPageIndex = (currnetPageIndx - 1);
            fillRequestItems();
        }
        protected void grdItems_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {

                if (Session["selectedItems"] != null)
                {
                    var objList = (List<view_AssetsList>)Session["selectedItems"];
                    view_AssetsList selected = objList.Where(x => x.InboubdItemId == ZeroIntergerIFNull(e.Item.Cells[0].Text)).FirstOrDefault();
                    objList.Remove(selected);

                    Session["selectedItems"] = objList;
                    fillRequestItems();
                }

            }

        }
        protected void btnReload_Click(object sender, EventArgs e)
        {
            fillRequestItems();
        }

        #endregion
        #region "Fill Information"
        private string GetTitle(bool isadd)
        {
            if (isadd)
            {
                return Resources.Pages.addnewrecord1;
            }
            else
            {
                return Resources.Pages.editRecord;
            }
        }
        private void ClearForm()
        {
            txtFromDate.Text = "";
            txtNotes.Text = "";


        }  
        public string getActiveTab(string selectedTab)
        {
            if (hdnActiveTab.Value == "" && selectedTab == "1")
            {
                return "active show";

            }
            else if (hdnActiveTab.Value == selectedTab)
            {
                return "active show";

            }
            return "";
        }
        #endregion
        #region "Shared Methods"
        #region "Fill Lookups Information"
        private void fillLookups()
        {
        }
        #endregion
        public string setmaterstyle()
        {
            return "display:block";
        }

        #endregion

    }
}