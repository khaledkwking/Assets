using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UI.Web.Admin.Controller;
using UI.Web.Core.Enums;

namespace UI.Web.Modules.Assets
{
    public partial class AssetDetails : BaseFormAdmin
    {

        #region "Page Members"

        public AssetsRepository assetsRepository = IoC.Resolve<AssetsRepository>();
        public InboundRepository objInboundRepository = IoC.Resolve<InboundRepository>();

        public string _PageTitle = Resources.menu.CustodyDetails;


        public string ItemNameAr = "";
        public string ItemImage = "";
        public string ItemNamedesc = "";
        public string TransDate = "";


        public string VendorNameAr = "";
        public string CategoryName = "";
        public string FinanceRefCode = "";
        public string LastActiontitleAr = "";
        public string AvailabilityStatusAr = "";
        public string Notes = "";
        public string UnitRefCode = "";
        public string ExpireDate = "";
        public string ItemRefCode = "";
        public string ActionDate = "";
        public string LocationName = "";
        public string EmpName = "";
        public string locationId = "";
        public string EmpRef = "";
        public int actionId = 0;
        public int statusId = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                fillLookups();
                fillItemDetails(ZeroIntergerIFNull(Request.QueryString["aid"].ToString()));
            }


        }
        #region "Fill Informations"
        private void fillItemDetails(int itemId)
        {

            var objList = assetsRepository.getItemDetails(itemId);
            if ((objList != null))
            {


                ItemNameAr = gets(objList.ItemNameAr);
                ItemImage = gets(objList.ItemImage);
                TransDate = NullDateifEmptyText(objList.TransDate);
                ExpireDate = NullDateifEmptyText(objList.ExpireDate);
                VendorNameAr = gets(objList.VendorNameAr);
                CategoryName = gets(objList.ItemsCategoryTitleAr);
                FinanceRefCode = gets(objList.FinanceRefCode);
                ItemRefCode = gets(objList.ItemRefCode);
                LastActiontitleAr = (ZeroIntergerIFNull(gets(objList.EmpRefCode)) == 0 ? "<em class='icon ni ni-building text-info'></em> &nbsp;" : "<em class='icon ni ni-user-list  text-info'></em> &nbsp;").ToString() + showAction(ZeroIntergerIFNull(gets(objList.actionId)), gets(objList.LastActiontitleAr));
                AvailabilityStatusAr = showAvailability(ZeroIntergerIFNull(gets(objList.statusId)), gets(objList.AvailabilityStatusAr));
                UnitRefCode = gets(objList.UnitRefCode);
                Notes = gets(objList.Notes);

                ActionDate = objList.ActionDate.Value.ToString("MM/dd/yyyy");
                LocationName = gets(objList.LocationNameAr);
                EmpName = gets(objList.EmpName);
                actionId = objList.actionId.Value;
                statusId = objList.statusId.Value;
                locationId = gets(objList.ToLocationId);
                EmpRef = gets(objList.EmpRefCode);
                if (actionId == 2)
                {
                    checkout.Visible = false;
                }
                else { checkout.Visible = true; }


                FillEventLog(objList.InboubdItemId);

            }


        }


        private void FillEventLog(int AssetCode)
        {
            var objList = assetsRepository.getAssetEventLog(AssetCode);
            grdEventLog.DataSource = objList;
            grdEventLog.DataBind();
            int _totalCount = objList.Count;
        }

        #endregion


        #region" Helper"

        private void fillLookups()
        {

            ////var LocationsList = LooksUpsRepository.ins.FillStoreLocations();
            ////  FillDllwithoptional(LooksUpsRepository.ins.FillLocation(), lstOwnerLocationCode, "LocationNameAr", "Code");

            //if (Session["OraEmpList"] != null)
            //{
            //    FillDllwithoptional((List<EmployeeViewModel>)Session["OraEmpList"], lstRefEmployee, "EMP_NAME", "EMP_ID");
            //}
            //else
            //{
            //    // Request Data From Ora.
            //    var Emplist = GetOraEmpList(1);
            //    Session["OraEmpList"] = Emplist;
            //    FillDllwithoptional(Emplist, lstRefEmployee, "EMP_NAME", "EMP_ID");
            //}

            FillDllwithoptional(LooksUpsRepository.ins.FillEmployee(), lstRefEmployee, "EmpName", "Code");

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

        protected void lnkSaveAction_Click(object sender, EventArgs e)
        {
            // CheckOUt Or Transfer
            string script = "";
            try
            {
                AssetsEventTracking obj = new AssetsEventTracking();
                var selectedItem = (AssetsItemUnit)assetsRepository.getItemDetailsForEdit(ZeroIntergerIFNull(Request.QueryString["aid"].ToString()));

                // Set Last EVent Information if data Not Chanaged
                //if (selectedItem.LastEventTrackingId != 0)
                //{
                //    var LastTrackingHistory = assetsRepository.getTrackingDetails(selectedItem.LastEventTrackingId.Value);
                //    if (hdnType.Value == "1" || hdnType.Value == "")
                //    {
                //        obj.EmpName = lstRefEmployee.SelectedItem.Text;
                //        obj.EmpRefCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);
                //    }
                //    else {
                //        obj.EmpRefCode = LastTrackingHistory.EmpRefCode;
                //        obj.EmpName = LastTrackingHistory.EmpName;
                //    }

                //    if (ZeroIntergerIFNull(selectedToLocation.Value) != 0)
                //    {
                //        obj.ToLocationId = ZeroIntergerIFNull(selectedToLocation.Value);

                //    }
                //    else { obj.ToLocationId = LastTrackingHistory.ToLocationId; }

                //    obj.RequestHeaderCode = LastTrackingHistory.RequestHeaderCode;
                //}

                // Add Action Header
                // Add Request Header
                AssetsEventTrackingHeader objHeader = new AssetsEventTrackingHeader();
                objHeader.RequestDate = DateTime.Now;
                objHeader.RequestRefCode = Guid.NewGuid().ToString();
                objHeader.RequestActionType = ZeroIntergerIFNull(hdnType.Value);  //(int) CustodyRequestType.CheckOut;
                objHeader.ProcessType = (int)CustodyProcessTypes.CheckOut;
                objHeader.TMonth = DateTime.Now.Month;
                objHeader.TYear = DateTime.Now.Year;
                objHeader.Serial = generateRequestSerial();
                objHeader.CreatedAt = DateTime.Now;
                objHeader.CreatedBy = ZeroIntergerIFNull(ReadSession("userid").ToString());
                assetsRepository.AddAssetsEventTrackingHeader(objHeader);


                obj.RequestHeaderCode = objHeader.Code;
                obj.AssetCode = selectedItem.Code;
                obj.ActionDate = NullDateifEmpty(txtFromDate.Text);
                if (txtReturnDate.Text != "")
                {
                    obj.DueDate = NullDateifEmpty(txtReturnDate.Text);
                }

                obj.actionId = 2;// checkout;
                obj.statusId = 2;// CHecked OUt ;

                if (hdnType.Value == "1" || hdnType.Value == "")
                {
                    obj.EmpName = lstRefEmployee.SelectedItem.Text;
                    obj.EmpRefCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);
                }
                obj.ToLocationId = ZeroIntergerIFNull(selectedToLocation.Value);
                obj.Notes = txtNotes.Text;

                obj.CreatedAt = DateTime.Now;
                obj.CreatedBy = ZeroIntergerIFNull(ReadSession("userId").ToString());

                assetsRepository.AddEventTracking(obj);

                //update
                selectedItem.LastEventTrackingId = obj.Code;
                objInboundRepository.UpdateItemunit(selectedItem);



                Session["selectedItems"] = null;
                fillItemDetails(ZeroIntergerIFNull(Request.QueryString["aid"].ToString()));

                script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }
            catch (Exception ex)
            {

                script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }


        }

        protected void lnkOtherActions_Click(object sender, EventArgs e)
        {
            string script = "";
            try
            {
                AssetsEventTracking obj = new AssetsEventTracking();


                var selectedItem = (AssetsItemUnit)assetsRepository.getItemDetailsForEdit(ZeroIntergerIFNull(Request.QueryString["aid"].ToString()));
                if (selectedItem!=null)
                {
                    if (selectedItem.LastEventTrackingId != 0)
                    {
                        var LastTrackingHistory = assetsRepository.getTrackingDetails(selectedItem.LastEventTrackingId.Value);
                        //obj.EmpRefCode = LastTrackingHistory.EmpRefCode;
                        //obj.EmpName = LastTrackingHistory.EmpName;
                        if (ZeroIntergerIFNull(selectedToLocation.Value) != 0)
                        {
                            obj.ToLocationId = ZeroIntergerIFNull(selectedToLocation.Value);

                        }
                        else { obj.ToLocationId = LastTrackingHistory.ToLocationId; }

                        obj.RequestHeaderCode = LastTrackingHistory.RequestHeaderCode;
                    }
                    


                obj.AssetCode = selectedItem.Code;
                obj.ActionDate = NullDateifEmpty(txtOtherAction.Text);

                obj.actionId = hdnActionType.Value == "2" ? 3 : 2;// check In:CheckOUt;
                obj.statusId = hdnActionType.Value == "2" ? 1 : ZeroIntergerIFNull(hdnActionType.Value);// Avilable ;

                obj.Notes = txtNotes.Text;
                obj.CreatedAt = DateTime.Now;
                obj.CreatedBy = ZeroIntergerIFNull(ReadSession("userId").ToString());

                assetsRepository.AddEventTracking(obj);


                //update
                selectedItem.LastEventTrackingId = obj.Code;
                objInboundRepository.UpdateItemunit(selectedItem);

                fillItemDetails(ZeroIntergerIFNull(Request.QueryString["aid"].ToString()));


                script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                }
            }
            catch (Exception ex)
            {

                script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }


        }
    }
}
