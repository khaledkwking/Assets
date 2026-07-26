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
using UI.Web.Helper;

namespace UI.Web.Modules.Assets
{
    public partial class AssetCheckin : BaseFormAdmin
    {
        #region "Page Members"
        public AssetsRepository objRepository = IoC.Resolve<AssetsRepository>();
        public InboundRepository objInboundRepository = IoC.Resolve<InboundRepository>(); 
        public ItemRepository objItemRepository = IoC.Resolve<ItemRepository>(); 
        public string _PageTitle = Resources.Pages.CustodyCheckIn;

        #endregion

        #region "Page Events"
        protected void Page_PreRender(object sender, System.EventArgs e)
        {



        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            lblerror.Text = "";


            btnSave.Attributes.Add("onclick", "return chkImage();");

            if (!IsPostBack)
            {
                //if ((Request.UrlReferrer == null))
                //{
                //    Response.Redirect("/admin/pages/main.aspx");
                //}
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

                    Logger.Log(
                       userId: ReadSession("userId").ToString(),
                       userName: ReadSession("AdminName").ToString(),
                       tableName: "AssetsEventTrackings",
                       action: "Insert",
                       recordId: obj.Code.ToString()
                       );

                    //update
                    selectedItem.LastEventTrackingId = obj.Code;
                    objInboundRepository.UpdateItemunit(selectedItem);

                    Logger.Log(
                     userId: ReadSession("userId").ToString(),
                     userName: ReadSession("AdminName").ToString(),
                     tableName: "AssetsItemUnits",
                     action: "Update",
                     recordId: selectedItem.Code.ToString()
                     );
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

        //private void fillRequestItems()
        //{

        //    if (Session["selectedItems"] != null)
        //    {
        //        var item = Session["selectedItems"];
        //        var objList = (List<view_AssetsList>)Session["selectedItems"];
        //        decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdItems.PageSize));
        //        if ((c <= grdItems.CurrentPageIndex))
        //        {
        //            grdItems.CurrentPageIndex = 0;
        //        }
        //        grdItems.Visible = true;
        //        grdItems.DataSource = objList;
        //        grdItems.DataBind();
        //        int _totalCount = objList.Count;
        //        pager1.ItemCount = objList.Count;
        //        hdnItemCount.Value= objList.Count.ToString();

        //        if (objList != null && objList.Count > 0)
        //        {
        //            pager1.Visible = true;

        //        }
        //        else { pager1.Visible = false; }


        //    }
        //    else
        //    {
        //        grdItems.Visible = false;
        //        pager1.Visible = false;
        //    }



        //}
        private void fillRequestItems()
        {
            if (Session["selectedItems"] is List<view_AssetsList> objList)
            {
                decimal c = Math.Ceiling((decimal)objList.Count / grdItems.PageSize);

                if (c <= grdItems.CurrentPageIndex)
                {
                    grdItems.CurrentPageIndex = 0;
                }

                grdItems.Visible = true;
                grdItems.DataSource = objList;
                grdItems.DataBind();

                int _totalCount = objList.Count;
                pager1.ItemCount = _totalCount;
                hdnItemCount.Value = _totalCount.ToString();

                pager1.Visible = _totalCount > 0;
            }
            else
            {
                // Optional: clear the session or log type issue
                // Session.Remove("selectedItems");

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

                //if (Session["selectedItems"] != null)
                //{
                //    var objList = (List<view_AssetsList>)Session["selectedItems"];
                //    view_AssetsList selected = objList.Where(x => x.InboubdItemId == ZeroIntergerIFNull(e.Item.Cells[0].Text)).FirstOrDefault();
                //    objList.Remove(selected);

                //    Session["selectedItems"] = objList;
                //    fillRequestItems();
                //}

            }

        }
        protected void grdItems_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.AlternatingItem | e.Item.ItemType == ListItemType.Item)
            //{
            //    string EventCode = e.Item.Cells[2].Text.Replace("&nbsp;", " ").Trim();
            //    int catID = ZeroIntergerIFNull(e.Item.Cells[8].Text);

            //        e.Item.Cells[7].Text = "";

            //    var q = objItemRepository.GetDetailsCategory(catID);
            //    if(q.)


            //}
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
        private void FillInboundMasterInformation()
        {

            //var objList = objRepository.FillDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            //if ((objList != null))txtSerial
            //{


               // txtSerial.Text = gets(objList.Serial);

            //    lstInboundTypeCode.SelectedValue = gets(objList.InboundTypeCode);
            //    SetInboundType();


            //    try
            //    {
            //        if (objList.FromVendorCode != null)
            //        {
            //            lstFromVendorCode.SelectedValue = gets(objList.FromVendorCode);
            //        }
            //        lstTargetLocationCode.SelectedValue = gets(objList.TargetLocationCode);
            //        lstOwnerLocationCode.SelectedValue = gets(objList.OwnerLocationCode);

            //    }
            //    catch (Exception)
            //    {


            //    }


            //    txtTransDate.Text = objList.TransDate.Value.ToString("MM/dd/yyyy");

            //    txtRefNo.Text = gets(objList.RefNo);
            //    txtRefDate.Text = objList.RefDate.Value.ToString("MM/dd/yyyy");


            //    txtDeliveryOrderNo.Text = gets(objList.DeliveryOrderNo);
            //    txtDeliveryDate.Text = NullDateifEmptyText (objList.DeliveryDate);

            //    txtDepositeNotes.Text = gets(objList.DepositeNotes);
            //    txtNotes.Text = gets(objList.Notes);
            //}

            ////tblAdd.Visible = true;
            ////lblSubTitle.Text = this.GetTitle(false);

        }

        private void SetInboundType()
        {
            //if (lstInboundTypeCode.SelectedValue == "2")
            //{
            //    divOwnerLocation.Visible = true;
            //    divVendor.Visible = false;
            //}
            //else if (lstInboundTypeCode.SelectedValue == "1")
            //{
            //    divOwnerLocation.Visible = false;
            //    divVendor.Visible = true;
            //}
            //else
            //{
            //    divOwnerLocation.Visible = false;
            //    divVendor.Visible = false;
            //}
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

           //var LocationsList = LooksUpsRepository.ins.FillStoreLocations();
           // FillDllwithoptional(LocationsList, lstOwnerLocationCode, "path", "Code");

           

        }
        #endregion
        public string setmaterstyle()
        {
            //if (Request.QueryString["id"] != null)
            //{
            //    return "display:block";
            //}

            //return "display:none";

            return "display:block";
        }


        #endregion

      
    }
}

