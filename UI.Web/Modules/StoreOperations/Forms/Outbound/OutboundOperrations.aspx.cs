using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.StoreOperations.Forms.Outbound
{
    public partial class OutboundOperrations : BaseFormAdmin
    {
        #region "Page Members"
        public OutboundRepository objRepository = IoC.Resolve<OutboundRepository>();
        public InboundRepository InboundobjRepository = IoC.Resolve<InboundRepository>();
        public string _PageTitle = Resources.Pages.CustodyAdd;

        #endregion

        #region "Page Events"

        protected void Page_Init(object sender, EventArgs e)
        {
            PageUrl = "OutboundOperrations.aspx?t=" + Request.QueryString["t"].ToString();
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            //btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
            btnSave.Attributes.Add("onclick", "return chkImage();");
            lnkSaveItems.Attributes.Add("onclick", "return validateAddingItem();");

            if (!IsPostBack)
            {

                lnkSaveItems.Text = "<i class='icon ni ni-save'></i>&nbsp; " + Resources.Pages.AddItem;

                if (Request.QueryString["t"] != null)
                {
                    hdnRequestType.Value = Request.QueryString["t"].ToString();
                    if (Request.QueryString["t"] == "1")
                    { //personal Custody
                        _PageTitle = Resources.Pages.CustodyAdd;
                        divEmployee.Visible = true;
                        divToStore.Visible = false;
                        divLocation.Visible = true;
                    }
                    if (Request.QueryString["t"] == "2")
                    { //Org Custody
                        _PageTitle = Resources.Pages.CustodyAdd1;
                        divEmployee.Visible = false;
                        divToStore.Visible = false;
                        divLocation.Visible = true;
                    }
                    if (Request.QueryString["t"] == "3")
                    { //Store Transfer
                        _PageTitle = Resources.Pages.StoreTransfer;
                        divEmployee.Visible = false;
                        divToStore.Visible = true;
                        divLocation.Visible = false;
                    }
                    else { _PageTitle = Resources.Pages.CustodyAdd1; divEmployee.Visible = false;
                        divToStore.Visible = false;
                    }

                }

                fillLookups();
                ViewState["itemID"] = "0";
                ViewState["outboundItemID"] = "0";

                if (Request.QueryString["id"] != null)
                {

                    ViewState["itemID"] = gets(Request.QueryString["id"]);
                    hdnMasterID.Value = gets(Request.QueryString["id"]);
                    FillOutboundMasterInformation();
                    FillOutboundItems();

                }
                else
                {
                    txtSerial.Text = generateRequestSerial();
                    txtTransDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                }
            }

        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            SaveOutboundMaster();

        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.ClearForm();
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.FillGrid();
        }

        #endregion

        #region "Fill Information"
        private string generateRequestSerial()
        {
            int YearRequestCount = objRepository.getCurrentYearOutboundCount(DateTime.Now.Year);

            return "OUT/" + string.Format("{0:000000}", YearRequestCount + 1) + "/CMGS" + DateTime.Now.ToString("yy");


        }
        private void fillLookups()
        {

            FillDllwithoptional_ALL(InboundobjRepository.fillItems(), lstPurchaseItems, "ItemNameAr", "Code", "اختر ");
            var LocationsList = LooksUpsRepository.ins.FillStoreLocations();

            FillDllwithoptional(LocationsList, lstOwnerLocationCode, "path", "Code");
            FillDllwithoptional(LocationsList, lstToStore, "path", "Code");
            

            if (Session["OraEmpList"] != null)
            {
                FillDllwithoptional((List<EmployeeViewModel>)Session["OraEmpList"], lstRefEmployee, "EMP_NAME", "EMP_ID");
            }
            else
            {
                // Request Data From Ora.
                var Emplist = GetOraEmpList(1);
                Session["OraEmpList"] = Emplist;
                FillDllwithoptional(Emplist, lstRefEmployee, "EMP_NAME", "EMP_ID");
            }

            FillDllwithoptional(LooksUpsRepository.ins.FillQUnit(), lstQtyUnitCode, "TitleAr", "Code");
            FillDllwithoptional(LooksUpsRepository.ins.FillDtemStatus(), lstStatusCode, "TitleAr", "Code");


        }

        private void FillGrid()
        {
            //int Pid = ZeroIntergerIFNull(Request.QueryString["pid"] == null ? "0" : Request.QueryString["pid"].ToString());
            //var objList = objRepository.GetList(Pid,"");
            //lblcount.Text = (Resources.Utilities.foundTotal + (objList.Count.ToString()).ToString() + Resources.Utilities.records);
            //decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdData.PageSize));
            //if ((c <= grdData.CurrentPageIndex))
            //{
            //    grdData.CurrentPageIndex = 0;
            //}

            //grdData.DataSource = objList;
            //grdData.DataBind();
            //int _totalCount = objList.Count;


        }
        private string GetTitle(bool isadd)
        {
            if (isadd)
            {
                return Resources.Pages.addnewrecord1;
            }
            else
            {
                return Resources.Pages.editData;
            }

        }
        private void FillForm()
        {
            //var objList = objRepository.GetDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            //if ((objList != null))
            //{
            //    txttitleEn.Text = gets(objList.LocationNameEn);
            //    txttitleAr.Text = gets(objList.LocationNameAr);
            //    txtFinRefCode.Text = gets(objList.LocationRefCode);

            //    lstLocationType.SelectedValue= gets(objList.LocationType);

            //}

            //tblAdd.Visible = true;
            //lblSubTitle.Text = this.GetTitle(false);

        }
        private void ClearForm()
        {
            //txttitleEn.Text = "";
            //txttitleAr.Text = "";
            //txtFinRefCode.Text = "";
            //hdnSelectedEditNode.Value = "";

            //ViewState["itemID"] = 0;
            //tblAdd.Visible = false;
            //tblshow.Visible = true;
            //lblSubTitle.Text = this.GetTitle(true);
        }
        public string getActiveTab(string selectedTab)
        {
            if (hdnActiveTab.Value=="" && selectedTab=="1")
            {
                return "active show";

            }
            else if (hdnActiveTab.Value== selectedTab)
            {
                return "active show";

            }
            return "";
        }


        private void FillOutboundMasterInformation()
        {

            var objList = objRepository.FillDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            if ((objList != null))
            {

                txtSerial.Text = gets(objList.Serial);
                lstOwnerLocationCode.SelectedValue = gets(objList.OwnerLocationCode);
                lstRefEmployee.SelectedValue = gets(objList.EmpRefCode);

                txtRefNo.Text = gets(objList.RefNo);
                txtRefDate.Text = objList.RefDate.Value.ToString("MM/dd/yyyy");
                txtTransDate.Text = objList.TransDate.Value.ToString("MM/dd/yyyy");
                txtNotes.Text = gets(objList.Notes);
                hdnMasterID.Value = gets(objList.Code);
                if (objList.TypeCode == 3)
                {
                    lstToStore.SelectedValue= gets(objList.LocationCode);
                }
                else { hdnSelectedNode.Value = gets(objList.LocationCode); }
                
            }


        }


        #endregion


        #region "CustodyAdd"

        private void GetStoreItemInformation()
        {

            // Set Item Info

            var objInboundunit = objRepository.getItemCardDetails(ZeroIntergerIFNull(lstPurchaseItems.SelectedValue));
            if (objInboundunit != null && objInboundunit.Count > 0 && objInboundunit.Count == 1)
            {

                lstQtyUnitCode.SelectedValue = objInboundunit[0].QUnitCode.ToString();
                lstStatusCode.SelectedValue = objInboundunit[0].UnitStatus.ToString();
                txtBalance.Text = (objInboundunit[0].ReceivedQty - objInboundunit[0].TotalDelivered).ToString();

            }
            else if (objInboundunit.Count > 1)
            {// View Item Unit Popup


            }
            else
            {
                // NO Balance
                lstQtyUnitCode.SelectedValue = "0";
                lstStatusCode.SelectedValue = "0";
                txtBalance.Text = "0";
                txtQty.Text = "0";
            }

        }
        protected void lstPurchaseItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetStoreItemInformation();
        }
        private void fillItemInformation()
        {

            var objList = objRepository.GetOutboundItemDetails(ZeroIntergerIFNull(ViewState["outboundItemID"].ToString()));
            if ((objList != null))
            {


                lstPurchaseItems.SelectedValue = gets(objList.ItemCode);
                // Get Item Current Balance
                var objInboundunit = objRepository.getItemCardDetails(ZeroIntergerIFNull(lstPurchaseItems.SelectedValue));
                if (objInboundunit != null)
                {

                    var selectedItem = objInboundunit.Where(x => x.QUnitCode == objList.QtyUnitCode && x.UnitStatus == objList.ItemStatusCode).FirstOrDefault();
                    if (selectedItem != null)
                    {
                        txtBalance.Text = (selectedItem.ReceivedQty - selectedItem.TotalDelivered).ToString();

                    }


                }
                lstQtyUnitCode.SelectedValue = gets(objList.QtyUnitCode);
                lstStatusCode.SelectedValue = gets(objList.ItemStatusCode);
                txtQty.Text = gets(objList.Qty);
                txtUnitCost.Text = gets(objList.EstimatedAmount);
                //txtNotes.Text = gets(objList.Notes);
                lnkSaveItems.Text = "<i class='icon ni ni-save'></i>&nbsp; " + Resources.Pages.Update;
            }

        }
        protected void grdOutboundItems_EditCommand(object source, DataGridCommandEventArgs e)
        {
            string id = e.Item.Cells[0].Text;

            ViewState["outboundItemID"] = id;
            fillItemInformation();

            //divinboundItemsAdd.Visible = true;
            //DivinboundItemsShow.Visible = false;
        }

        protected void grdOutboundItems_DeleteCommand(object source, DataGridCommandEventArgs e)
        {

            try
            {
                var ItemDetails = objRepository.GetOutboundItemDetails(ZeroIntergerIFNull(e.Item.Cells[0].Text));
                objRepository.DeleteItems(ItemDetails);


                string script = FormatpopupErrorMSG(Resources.Alerts.DataDeletedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }
            catch (Exception ex)
            {

                string script = FormatpopupErrorMSG(Resources.Alerts.SorryDeleteDataFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }


            FillOutboundItems();
        }
        private void SaveOutboundMaster()
        {
            try
            {
                Infrastructure.DAL.Model.DB.Outbound obj = new Infrastructure.DAL.Model.DB.Outbound();
                if (ViewState["itemID"].Equals("0"))
                {//Save


                    obj.Serial = generateRequestSerial();
                    obj.TMonth = NullDateifEmpty(txtTransDate.Text).Month;
                    obj.TYear = NullDateifEmpty(txtTransDate.Text).Year;
                    obj.TransDate = NullDateifEmpty(txtTransDate.Text);
                    obj.TypeCode = ZeroIntergerIFNull(Request.QueryString["t"]);

                    obj.OwnerLocationCode = ZeroIntergerIFNull(lstOwnerLocationCode.SelectedValue);
                    if (hdnRequestType.Value == "3")//Store Transfer
                    {
                        obj.LocationCode = ZeroIntergerIFNull(lstToStore.SelectedValue);
                    }
                    else { obj.LocationCode = ZeroIntergerIFNull(hdnSelectedNode.Value); }
                    

                    
                   
                    if (lstRefEmployee.SelectedValue == "0")
                    {
                        obj.EmpName = "";
                        obj.EmpRefCode = 0;
                    }
                    else { obj.EmpName = lstRefEmployee.SelectedItem.Text;
                        obj.EmpRefCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);
                    }


                    obj.RefNo = txtRefNo.Text;
                    obj.RefDate = NullDateifEmpty(txtRefDate.Text);
                    obj.Notes = txtNotes.Text;

                    objRepository.AddOutbound(obj);
                    hdnMasterID.Value = gets(obj.Code);
                    ViewState["itemID"] = gets(obj.Code);

                    // Save Documenting Status

                    OutboundStatusTrack objStatus = new OutboundStatusTrack();

                    objStatus.OutboundCode = obj.Code;
                    objStatus.TransDate = DateTime.Now;
                    objStatus.Notes = "New Request Documenting Status";
                    objStatus.WithdrawStatusTypeCode = 1;
                    objRepository.AddStatusTracking(objStatus);


                }
                else
                { //Update 

                    hdnMasterID.Value = ViewState["itemID"].ToString();
                    obj = objRepository.GetDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));


                    obj.Serial = txtSerial.Text;

                    obj.TMonth = NullDateifEmpty(txtTransDate.Text).Month;
                    obj.TYear = NullDateifEmpty(txtTransDate.Text).Year;
                    obj.TransDate = NullDateifEmpty(txtTransDate.Text);
                    obj.TypeCode = ZeroIntergerIFNull(Request.QueryString["t"]);

                    obj.OwnerLocationCode = ZeroIntergerIFNull(lstOwnerLocationCode.SelectedValue);
                    obj.EmpRefCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);
                    if (lstRefEmployee.SelectedValue == "0")
                    {
                        obj.EmpName = "";
                    }
                    else { obj.EmpName = lstRefEmployee.SelectedItem.Text; }
                    if (hdnRequestType.Value == "3")//Store Transfer
                    {
                        obj.LocationCode = ZeroIntergerIFNull(lstToStore.SelectedValue);
                    }
                    else { obj.LocationCode = ZeroIntergerIFNull(hdnSelectedNode.Value); }
                    obj.RefNo = txtRefNo.Text;
                    obj.RefDate = NullDateifEmpty(txtRefDate.Text);
                    obj.Notes = txtNotes.Text;

                    objRepository.UpdateOutbound(obj);

                }

                ViewState["itemID"] = obj.Code;

                ClearForm();
                FillOutboundMasterInformation();
                string script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }
            catch (Exception ex)
            {
                string script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);


            }

        }

        private void SaveItemInformation()
        {

            string script = "";
            try
            {
                //Check Master Existance
                // SaveOutboundMaster();

                Outbound_Items obj = new Outbound_Items();
                if (gets(ViewState["outboundItemID"]).Equals("0"))
                {//Save

                    obj.OutboundCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());

                    obj.ItemCode = ZeroIntergerIFNull(lstPurchaseItems.SelectedValue);
                    obj.QtyUnitCode = ZeroIntergerIFNull(lstQtyUnitCode.SelectedValue);
                    obj.ItemStatusCode = ZeroIntergerIFNull(lstStatusCode.SelectedValue);
                    obj.Qty = ZeroIFNull(txtQty.Text);
                    //obj.DeliveredQry = ZeroIFNull(txtQty.Text);
                    obj.EstimatedAmount = ZeroIFNull(txtUnitCost.Text);
                    obj.OutTransDate = DateTime.Now;
                    objRepository.AddOutboundItems(obj);





                }
                else
                { //Update 
                    obj = objRepository.GetOutboundItemDetails(ZeroIntergerIFNull(ViewState["outboundItemID"].ToString()));


                    obj.ItemCode = ZeroIntergerIFNull(lstPurchaseItems.SelectedValue);
                    obj.QtyUnitCode = ZeroIntergerIFNull(lstQtyUnitCode.SelectedValue);
                    obj.ItemStatusCode = ZeroIntergerIFNull(lstStatusCode.SelectedValue);
                    obj.Qty = ZeroIFNull(txtQty.Text);
                    //obj.DeliveredQry = ZeroIFNull(txtQty.Text);
                    obj.EstimatedAmount = ZeroIFNull(txtUnitCost.Text);

                    objRepository.UpdateOutboundItems(obj);


                }

                ClearOutBoundForm();
                FillOutboundItems();


                script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }
            catch (Exception ex)
            {


                script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }

        }

        private void ClearOutBoundForm()
        {
            txtBalance.Text = "0";
            txtQty.Text = "";
            txtUnitCost.Text = "";
            lstPurchaseItems.SelectedValue = "0";

            ViewState["outboundItemID"] = 0;
            //tblAdd.Visible = false;
            //tblshow.Visible = true;
            //lblSubTitle.Text = this.GetTitle(true);
        }

        private void FillOutboundItems()
        {
            var objList = objRepository.FillOutboundItems(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            lblItemCount.Text = objList.Count.ToString();

            grdOutboundItems.DataSource = objList;
            grdOutboundItems.DataBind();


        }



        #endregion

        protected void lnkSaveItems_Click(object sender, EventArgs e)
        {

            SaveItemInformation();
            FillOutboundItems();

        }


    }
}