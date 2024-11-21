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
using UI.Web.Core.Enums;

namespace UI.Web.Modules.Assets
{
    public partial class AssetCheckout : BaseFormAdmin
    {
        #region "Page Members"
        public AssetsRepository objRepository = IoC.Resolve<AssetsRepository>();
        public LocationsRepository objLocationRepository = IoC.Resolve<LocationsRepository>();
        public string _PageTitle = "إدارة العهد";
        public string _PageSubTitle = "إدارة العهد";

        #endregion

        #region "Page Events"
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["empid"] != null)
            {
                this.MasterPageFile = "~/Modules/_shared/MainEmpty.Master";
            }
        }

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

                fillLookups();
                ClearForm();

                ViewState["SpEdit"] = "0";
                ViewState["NewDesc"] = "";
                ViewState["NewBar"] = "";
                ViewState["NewIsbn"] = "";
                ViewState["itemID"] = "0";

                if (Request.QueryString["t"] != null)
                {
                    hdnType.Value = Request.QueryString["t"].ToString();
                    if (Request.QueryString["t"].ToString() == "1")
                    {
                        custodyType.SelectedValue = Request.QueryString["t"].ToString();
                        divEmployee.Visible = true;
                        _PageSubTitle = "تسجيل عهدة فردية";
                    }
                    else
                    {
                        custodyType.SelectedValue = Request.QueryString["t"].ToString();
                        divEmployee.Visible = false;
                        _PageSubTitle = "تسجيل عهدة تنظيمية";
                    }

                }

                if (Request.QueryString["requestCode"] != null)
                {// Load Request Header Information
                    hdnMasterID.Value = gets(Request.QueryString["requestCode"]);
                    ViewState["itemID"] = gets(Request.QueryString["requestCode"]);
                 
                    loadRequest(ZeroIntergerIFNull(Request.QueryString["requestCode"]));
                }
                else if (Request.QueryString["empid"] != null)
                {
                    loadEmpRequest(ZeroIntergerIFNull(Request.QueryString["empid"]));
                }
               // fillRequestItems();
            }


        }


        private void loadRequest(int RequestHeaderCode)
        {
            var objHeader = objRepository.getTrackingRequestHeaderDetails(RequestHeaderCode);
            if (objHeader != null)
            {
               
                hdnType.Value = objHeader.ProcessType.ToString();
                if (hdnType.Value == "1")
                {
                    custodyType.SelectedValue = hdnType.Value;
                    divEmployee.Visible = true;
                    hdnEmployeeId.Value = gets(objHeader.Ora_EmpRefCode);
                }
                else
                {
                    custodyType.SelectedValue = hdnType.Value;
                    divEmployee.Visible = false;
                    hdnEmployeeId.Value = "0";
                }
                txtRequestDate.Text = NullDateifEmptyText(objHeader.RequestDate);

                lstToLocation.SelectedValue = gets(objHeader.ToLocationId);

                if (objHeader.OraEntityRefCode != null && objHeader.OraEntityRefCode != 0)
                {
                    hdnSelectedNode.Value = gets(objHeader.OraEntityRefCode);
                }
                else
                {
                    fillRequestItems();
                    string script = FormatErrorMSGSwal("عفوا ، يرجي مطابقة بيانات  الموظف مع بيانات الإدارية  ", "1");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

                    //  return;
                }


                lnkPrintRequest.HRef = Resources.Utilities.cutureRoute + "/Modules/Reports/AssetReceipt.aspx?docId=" + hdnMasterID.Value;
                viewPrint.Visible = true;
            }
            else { viewPrint.Visible = false; }


        }

        private void loadEmpRequest(int EmpId)
        {
            var objHeader = objRepository.getTrackingRequestHeaderByEmpCode(EmpId);
            if (objHeader != null)
            {
                if (objHeader.OraEntityRefCode != null && objHeader.OraEntityRefCode != 0)
                {
                    hdnSelectedNode.Value = gets(objHeader.OraEntityRefCode);
                }
                else
                {
                    string script = FormatErrorMSGSwal("عفوا ، يرجي مطابقة بيانات  الموظف مع بيانات الإدارية  ", "1");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

                    return;


                }

                hdnType.Value = objHeader.ProcessType.ToString();
                if (hdnType.Value == "1")
                {
                    custodyType.SelectedValue = hdnType.Value;
                    divEmployee.Visible = true;
                    hdnEmployeeId.Value = gets(objHeader.Ora_EmpRefCode);
                }
                else
                {
                    custodyType.SelectedValue = hdnType.Value;
                    divEmployee.Visible = false;
                    hdnEmployeeId.Value = "0";
                }
                try
                {
                    lstToLocation.SelectedValue = objHeader.ToLocationId.ToString();

                }
                catch (Exception)
                {


                }





                lnkPrintRequest.HRef = Resources.Utilities.cutureRoute + "/Modules/Reports/AssetReceipt.aspx?docId=" + hdnMasterID.Value;
                viewPrint.Visible = true;
            }
            else { 
                // set Selected Employee 
                viewPrint.Visible = false;
                lstRefEmployee.SelectedValue = gets(EmpId);
                
            }


        }



        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            string script = "";



            try
            {

               

                if (gets(ViewState["itemID"]).Equals("0"))
                {//Save


                    // Add Request Header
                    AssetsEventTrackingHeader objHeader = new AssetsEventTrackingHeader();
                    objHeader.RequestDate = NullDateifEmpty(txtRequestDate.Text);
                    objHeader.DueDate = NullDateifEmpty(txtReturnDate.Text);
                    objHeader.RequestRefCode = Guid.NewGuid().ToString();
                    objHeader.RequestActionType = (int)CustodyProcessTypes.CheckOut;
                    objHeader.ProcessType = ZeroIntergerIFNull(hdnType.Value);
                    objHeader.TMonth = NullDateifEmpty(txtRequestDate.Text).Month;
                    objHeader.TYear = NullDateifEmpty(txtRequestDate.Text).Year;
                    objHeader.Serial = generateRequestSerial();
                    objHeader.ToLocationId = ZeroIntergerIFNull(lstToLocation.SelectedValue);
                    objHeader.OrgChartRefCode = ZeroIntergerIFNull(hdnSelectedNode.Value); // Ora Org Chart
                    objHeader.RequestNotes = txtNotes.Text;

                    if (hdnType.Value == "1")
                    {
                        //Check Employee Esxiatance 
                        var OraEmpMapping = objRepository.checkOraEmployeeExitance(ZeroIntergerIFNull( hdnEmployeeId.Value));
                        if (OraEmpMapping != null)
                        {
                            objHeader.EmpName = OraEmpMapping.Ora_EmpName;
                            objHeader.EmpRefCode = OraEmpMapping.Emp_Id;
                        }
                        else { 
                            //Map Employee Informations\
                            Employee_tbl oraEmp=new Employee_tbl();
                            oraEmp.Emp_Name = lstRefEmployee.SelectedItem.Text;
                            oraEmp.Ora_EmpName = lstRefEmployee.SelectedItem.Text;
                            oraEmp.OraImported = true;
                            oraEmp.OraActionDate = DateTime.Now;
                            oraEmp.Ora_EmpRefCode = ZeroIntergerIFNull(hdnEmployeeId.Value);
                            oraEmp.OraEntityRefCode = ZeroIntergerIFNull(hdnSelectedNode.Value);

                            objRepository.AddEmployee(oraEmp);

                            objHeader.EmpName = oraEmp.Ora_EmpName;
                            objHeader.EmpRefCode = oraEmp.Emp_Id;

                        }

                     

                        // Check if Emplyee Location Saved 
                        if (lstRefEmployee.SelectedValue != "-1")
                        {
                            var emplocation = objRepository.getEmployeeLocations(ZeroIntergerIFNull(lstRefEmployee.SelectedValue));
                            if (emplocation != null )
                            {// Update Employee Location
                                emplocation.EmpCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);
                                emplocation.LocationCode = ZeroIntergerIFNull(lstToLocation.SelectedValue);
                                objRepository.UpdateEmployeeLoation(emplocation);

 
                            }
                            else 
                            {

                                D_EmployeeLocations locationObj = new D_EmployeeLocations();
                                locationObj.EmpCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);
                                locationObj.LocationCode = ZeroIntergerIFNull(lstToLocation.SelectedValue);
                                objRepository.AddEmployeeLoation(locationObj);

                            }

                        }


                    }

                    objHeader.CreatedAt = DateTime.Now;
                    objHeader.CreatedBy = ZeroIntergerIFNull(ReadSession("userid").ToString());
                    objRepository.AddAssetsEventTrackingHeader(objHeader);
                    hdnMasterID.Value = objHeader.Code.ToString();
                    // Add Items Details
                    SaveRequestItems(objHeader.Code);






                }
                else
                {
                    //Update

                    // Add Request Header
                    var objHeader = objRepository.getTrackingRequestHeaderDetails(ZeroIntergerIFNull(gets(ViewState["itemID"])));
                    objHeader.RequestActionType = (int)CustodyProcessTypes.CheckOut;
                    objHeader.ProcessType = ZeroIntergerIFNull(hdnType.Value);

                    objHeader.RequestDate = NullDateifEmpty(txtRequestDate.Text);
                    objHeader.DueDate = NullDateifEmpty(txtReturnDate.Text);
                    objHeader.TMonth = NullDateifEmpty(txtRequestDate.Text).Month;
                    objHeader.TYear = NullDateifEmpty(txtRequestDate.Text).Year;
                    objHeader.ToLocationId = ZeroIntergerIFNull(lstToLocation.SelectedValue);
                    objHeader.OrgChartRefCode = ZeroIntergerIFNull(hdnSelectedNode.Value);
                    objHeader.RequestNotes = txtNotes.Text;

                    if (hdnType.Value == "1")
                    {
                        //Check Employee Esxiatance 
                        var OraEmpMapping = objRepository.checkOraEmployeeExitance(ZeroIntergerIFNull(hdnEmployeeId.Value));
                        if (OraEmpMapping != null)
                        {
                            objHeader.EmpName = OraEmpMapping.Ora_EmpName;
                            objHeader.EmpRefCode = OraEmpMapping.Emp_Id;
                        }
                        else
                        {
                            //Map Employee Informations\
                            Employee_tbl oraEmp = new Employee_tbl();
                            oraEmp.Emp_Name = lstRefEmployee.SelectedItem.Text;
                            oraEmp.Ora_EmpName = lstRefEmployee.SelectedItem.Text;
                            oraEmp.OraImported = true;
                            oraEmp.OraActionDate = DateTime.Now;
                            oraEmp.Ora_EmpRefCode = ZeroIntergerIFNull(hdnEmployeeId.Value);
                            oraEmp.OraEntityRefCode = ZeroIntergerIFNull(hdnSelectedNode.Value);

                            objRepository.AddEmployee(oraEmp);

                            objHeader.EmpName = oraEmp.Ora_EmpName;
                            objHeader.EmpRefCode = oraEmp.Emp_Id;

                        }


                        if (lstRefEmployee.SelectedValue != "-1")
                        {
                            var emplocation = objRepository.getEmployeeLocations(ZeroIntergerIFNull(lstRefEmployee.SelectedValue));
                            if (emplocation != null)
                            {// Update Employee Location
                                emplocation.EmpCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);
                                emplocation.LocationCode = ZeroIntergerIFNull(lstToLocation.SelectedValue);
                                objRepository.UpdateEmployeeLoation(emplocation);


                            }
                            else
                            {

                                D_EmployeeLocations locationObj = new D_EmployeeLocations();
                                locationObj.EmpCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);
                                locationObj.LocationCode = ZeroIntergerIFNull(lstToLocation.SelectedValue);
                                objRepository.AddEmployeeLoation(locationObj);

                            }

                        }

                    }


                    // Check if Emplyee Location Saved 
                



                    objHeader.LastModifiedAt = DateTime.Now;
                    objHeader.LastModifiedBy = ZeroIntergerIFNull(ReadSession("userid").ToString());
                    objRepository.UpdateAssetsEventTrackingHeader(objHeader);

                    hdnMasterID.Value = objHeader.Code.ToString();
                    // Add Items Details
                    SaveRequestItems(objHeader.Code);
                }

                script = FormatErrorMSGSwal(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                //Session["RequestItemList"] = null;
                //ClearForm();
                loadRequest(ZeroIntergerIFNull(hdnMasterID.Value));
                fillRequestItems();




            }
            catch (Exception ex)
            {

                script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }

        }

        private void SaveRequestItems(int headerCode)
        {
            AssetsEventTracking obj = new AssetsEventTracking();
            if (Session["RequestItemList"] != null)
            {
                List<view_CustodyList> objItemList = (List<view_CustodyList>)Session["RequestItemList"];
                for (int i = 0; i <= objItemList.Count - 1; i++)
                {
                    if (objItemList[i].EventCode == -1 || objItemList[i].EventCode == 0)
                    {//New Added Item
                        obj.RequestHeaderCode = headerCode;
                        obj.AssetCode = objItemList[i].ItemCode;
                        obj.RequestItemPrice = objItemList[i].EstimatedUnitCost;
                        obj.ActionDate = objItemList[i].ActionDate;
                        obj.actionId = 2;// checkout;
                        obj.statusId = 2;// CHecked OUt ;
                        obj.ToLocationId = ZeroIntergerIFNull(lstToLocation.SelectedValue);
                        if (hdnType.Value == "1")
                        {
                            var OraEmpMapping = objRepository.checkOraEmployeeExitance(ZeroIntergerIFNull(hdnEmployeeId.Value));
                            if (OraEmpMapping != null)
                            {
                                obj.EmpName = OraEmpMapping.Ora_EmpName;
                                obj.EmpRefCode = OraEmpMapping.Emp_Id;
                            }
                        }
                        obj.Notes = objItemList[i].Notes;
                        obj.StoreRequestRefCode = objItemList[i].StoreRequestRefCode;
                        obj.Qty = objItemList[i].Qty;

                        obj.CreatedAt = DateTime.Now;
                        obj.CreatedBy = ZeroIntergerIFNull(ReadSession("userId").ToString());

                        objRepository.AddEventTracking(obj);

                    }
                    else
                    {//UpdateExisting
                        obj = objRepository.getTrackingDetails(ZeroIntergerIFNull(grdCustodyItems.Items[i].Cells[2].Text));

                        obj.RequestHeaderCode = headerCode;
                        obj.AssetCode = objItemList[i].ItemCode;
                        obj.ToLocationId = ZeroIntergerIFNull(lstToLocation.SelectedValue);
                        if (hdnType.Value == "1")
                        {
                            var OraEmpMapping = objRepository.checkOraEmployeeExitance(ZeroIntergerIFNull(hdnEmployeeId.Value));
                            if (OraEmpMapping != null)
                            {
                                obj.EmpName = OraEmpMapping.Ora_EmpName;
                                obj.EmpRefCode = OraEmpMapping.Emp_Id;
                            }
                        }
                        obj.Notes = objItemList[i].Notes;
                        obj.StoreRequestRefCode = objItemList[i].StoreRequestRefCode;
                        obj.Qty = objItemList[i].Qty;
                        obj.ActionDate = objItemList[i].ActionDate;
                        obj.CreatedAt = DateTime.Now;
                        obj.CreatedBy = ZeroIntergerIFNull(ReadSession("userId").ToString());

                        objRepository.UpdateEventTracking(obj);
                    }


                }
            }

        }

        private void fillRequestItems()
        {
            var objList = objRepository.getCustodyListByMasterData(ZeroIntergerIFNull(hdnMasterID.Value), ZeroIntergerIFNull(lstToLocation.SelectedValue), ZeroIntergerIFNull(hdnEmployeeId.Value));

            
           

            if (Session["RequestItemList"] != null && ((List<view_CustodyList>)Session["RequestItemList"]).Count > 0)
            {
                objList = (List<view_CustodyList>)Session["RequestItemList"];
            }
            else
            {
                Session["RequestItemList"] = objList;
            }

            hdnItemCount.Value = objList.Count.ToString();

            objList = AddDefaultItems(objList.ToList());


            grdCustodyItems.DataSource = objList;
            grdCustodyItems.EditItemIndex = getNewRowIndex(objList.ToList());
            grdCustodyItems.DataBind();
        }

        private int getNewRowIndex(List<view_CustodyList> dt)
        {
            for (int i = 0; i <= dt.Count - 1; i++)
            {
                string code = gets(dt[i].EventCode);
                if (code.Equals("") || code.Equals("-1"))
                    return i;
            }
            return -1;
        }

        private List<view_CustodyList> AddDefaultItems(List<view_CustodyList> _sourceList)
        {
            List<view_CustodyList> _out = new List<view_CustodyList>();
            foreach (var item in _sourceList)
            {
                _out.Add(item);
            }
            int _targetRows = 10;
            int _diff = _targetRows - _sourceList.Count;
            if (_diff > 0)
            {
                for (int i = 0; i < _diff; i++)
                {
                    var newItem = new view_CustodyList();
                    newItem.EventCode = -1;
                    _out.Add(newItem);
                }
            }
            else
            {
                var newItem = new view_CustodyList();
                newItem.EventCode = -1;
                _out.Add(newItem);
            }
            return _out;
        }




        protected void grdCustodyItems_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "AddNew")
            {
                ViewState["SpEdit"] = "0";

                string isbn = ((TextBox)e.Item.FindControl("txtItemCode")).Text;
                string desc = ((TextBox)e.Item.FindControl("txtItemDesc")).Text;
                string Qty = ((TextBox)e.Item.FindControl("txtFooterQuantity")).Text;
                string notes = ((TextBox)e.Item.FindControl("txtNotes")).Text;
                string StoreRequestRefCode = ((TextBox)e.Item.FindControl("txtStoreRequestRefCode")).Text;
                string txtCustodyDate = ((TextBox)e.Item.FindControl("txtCustodyDate")).Text;
                if (isbn.Equals("") && desc.Equals(""))
                {
                    string script = FormatErrorMSGSwal("عفوا ، ادخل بيانات المادة    ", "1");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel2", script, true);
                    return;
                }

                view_ItemCard itemobj = objRepository.getItemMaster(isbn, desc);

                if (itemobj == null)
                {
                    //string script = FormatpopupErrorMSG("Error, There's no  item stored with this Number, Bar Code or Description!", "1");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                    string script = FormatErrorMSGSwal("عفوا ، المادة غير مسجلة ", "2");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel2", script, true);


                    return;
                }


                if (Session["RequestItemList"] != null)
                {
                    var RequestItemList = (List<view_CustodyList>)Session["RequestItemList"];

                    var newItem = new view_CustodyList();
                    newItem.ItemCode = itemobj.Code;
                    newItem.EstimatedUnitCost = itemobj.EstimatedUnitCost.Value;
                    newItem.ItemRefCode = itemobj.ItemRefCode;
                    newItem.ItemNameAr = itemobj.ItemNameAr;
                    newItem.ItemNameEn = itemobj.ItemNameEn;
                    newItem.QtyUnitTitleAr = itemobj.D_QtyUnitTitleAr;
                    newItem.QtyUnitTitleEn = itemobj.D_QtyUnitTitleEn;
                    newItem.Qty = ZeroIntergerIFNull(Qty);
                    newItem.Notes = notes;
                    newItem.StoreRequestRefCode = StoreRequestRefCode;
                    newItem.ActionDate = NullDateifEmpty(txtCustodyDate);
                    RequestItemList.Add(newItem);

                    fillRequestItems();
                }

            }
            else if (e.CommandName.Equals("Delete"))
            {
                ViewState["SpEdit"] = "0";
                string code = e.Item.Cells[2].Text.Replace("&nbsp;", " ").Trim();
                List<view_CustodyList> RequestItemList = new List<view_CustodyList>();
                if (Session["RequestItemList"] != null)
                {
                    RequestItemList = (List<view_CustodyList>)Session["RequestItemList"];
                }
                if (code.ToString() != "0" && code.ToString() != "-1")
                {
                    //Get Person Delatils
                    var objforDelete = objRepository.getTrackingDetails(ZeroIntergerIFNull(code));
                    objRepository.DeleteEventTracking(objforDelete);

                }
                RequestItemList.RemoveAt(e.Item.ItemIndex);
                Session["RequestItemList"] = RequestItemList;

                fillRequestItems();
            }
            else if (e.CommandName.Equals("Edit"))
            {
                ViewState["SpEdit"] = 1;
                grdCustodyItems.EditItemIndex = e.Item.ItemIndex;

                List<view_CustodyList> RequestItemList = new List<view_CustodyList>();
                if (Session["RequestItemList"] != null)
                {
                    RequestItemList = (List<view_CustodyList>)Session["RequestItemList"];
                }
                //var result = AddDefaultItems(RequestItemList);

                grdCustodyItems.DataSource = AddDefaultItems(RequestItemList);
                grdCustodyItems.DataBind();
            }
            else if (e.CommandName.Equals("Cancel"))
            {
                ViewState["SpEdit"] = 0;
                fillRequestItems();
            }
            else if (e.CommandName.Equals("Update"))
            {
                int selectedIndex = e.Item.ItemIndex;  //grdCustodyItems.Items.Count > 10 ? e.Item.ItemIndex - 1 : (grdCustodyItems.Items.Count - e.Item.ItemIndex) - 1;
                List<view_CustodyList> RequestItemList = new List<view_CustodyList>();
                if (Session["RequestItemList"] != null)
                {
                    RequestItemList = (List<view_CustodyList>)Session["RequestItemList"];
                }

                view_CustodyList _Itemobj = new view_CustodyList();
                _Itemobj = RequestItemList[selectedIndex]; // Get Item Index From End Of List

                //Recheck Item

                string Qty = ((TextBox)e.Item.FindControl("txtQuantity")).Text;
                string notes = ((TextBox)e.Item.FindControl("txtNotes")).Text;
                string txtCustodyDate = ((TextBox)e.Item.FindControl("txtCustodyDate")).Text;
                string StoreRequestRefCode = ((TextBox)e.Item.FindControl("txtStoreRequestRefCode")).Text;

                _Itemobj.Qty = ZeroIntergerIFNull(Qty);
                _Itemobj.Notes = notes;
                _Itemobj.StoreRequestRefCode = StoreRequestRefCode;
                _Itemobj.ActionDate = NullDateifEmpty(txtCustodyDate);



                RequestItemList[selectedIndex] = _Itemobj;
                Session["RequestItemList"] = RequestItemList;
                ViewState["SpEdit"] = 0;
                fillRequestItems();
                string script = FormatpopupErrorMSG("Item Updated Successfully ", "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);


            }

        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            //clear user session 
            Session["RequestItemList"] = null;
            Session.Remove("RequestItemList");
            
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
            txtRequestDate.Text = "";
            txtReturnDate.Text = "";
            txtNotes.Text = "";
            ViewState["itemID"] = "0";
            
            lstRefEmployee.SelectedValue = "0";
            Session.Remove("RequestItemList");
            Session["RequestItemList"] = null;
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

            var LocationsList = LooksUpsRepository.ins.FillLocationTree(0);
            FillDllwithoptional(LocationsList, lstToLocation, "path", "Code");

            // lstToLocation.SelectedValue = "2381";




        }


        public void BindEmployeeLst(object lstData, DropDownList ddl, string txtField, string valueField)
        {

            ddl.DataSource = lstData;
            ddl.DataTextField = txtField;
            ddl.DataValueField = valueField;
            ddl.DataBind();
            ddl.Items.Add(new ListItem("إختر", "0"));

            ListItem listItem = new ListItem("بدون موظف", "-1");
            listItem.Attributes.Add("style", "background-color: Gold !important;");
            lstRefEmployee.Items.Add(listItem);

            try
            {
                ddl.SelectedValue = "0";
            }
            catch (Exception)
            {


            }
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

        protected void grdCustodyItems_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.AlternatingItem | e.Item.ItemType == ListItemType.Item)
            {
                string EventCode = e.Item.Cells[2].Text.Replace("&nbsp;", " ").Trim();

                if (EventCode.Equals("") || EventCode.Equals("-1"))
                {
                    e.Item.Cells[0].Controls[0].Visible = false;
                    e.Item.Cells[0].Controls[1].Visible = false;
                    e.Item.Cells[1].Controls[0].Visible = false;
                    e.Item.Cells[1].Controls[1].Visible = false;
                }
                else
                {
                    LinkButton lnkDelete = (LinkButton)e.Item.Cells[1].FindControl("lnkDelete");
                    lnkDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this item??');");
                }
                if (e.Item.Cells[5].Text == "0")
                {
                    e.Item.Cells[5].Text = "";
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {

                int col = 1;
                for (int i = 6; i <= e.Item.Cells.Count - 1; i++)
                {
                    if (e.Item.Cells[i].Visible)
                    {
                        col = col + 1;
                        e.Item.Cells[i].Visible = false;
                    }
                }
                // e.Item.Cells[5].Attributes.Add("colspan", System.Convert.ToString(col));

            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                e.Item.Cells[1].Visible = false;


                e.Item.Cells[0].Attributes.Add("colspan", "2");


                LinkButton lnkUpdate = (LinkButton)e.Item.Cells[0].FindControl("lnkUpdate");
                LinkButton lnkAdd = (LinkButton)e.Item.Cells[0].FindControl("lnkAdd");
                LinkButton lnkCancel = (LinkButton)e.Item.Cells[0].FindControl("lnkCancel");

                TextBox txtQuantity = (TextBox)e.Item.FindControl("txtQuantity");
                TextBox txtFooterQuantity = (TextBox)e.Item.FindControl("txtFooterQuantity");

                TextBox txtItemCost = (TextBox)e.Item.FindControl("txtItemCost");

                TextBox txtFooterCost = (TextBox)e.Item.FindControl("txtFooterCost");

                TextBox txtItemCode = (TextBox)e.Item.FindControl("txtItemCode");
                Label lblItemCode = (Label)e.Item.FindControl("lblItemCode");

                //TextBox txtBar = (TextBox)e.Item.Cells[6].FindControl("txtBar");
                //Label lblBar = (Label)e.Item.Cells[6].FindControl("lblBar");

                TextBox txtItemDesc = (TextBox)e.Item.FindControl("txtItemDesc");
                Label lblDesc = (Label)e.Item.FindControl("lblDesc");

                TextBox txtNotes = (TextBox)e.Item.FindControl("txtNotes");
                Label lblNotes = (Label)e.Item.FindControl("lblNotes");

                if (ViewState["SpEdit"].ToString() == "0")
                {
                    lnkUpdate.Visible = false;
                    lnkCancel.Visible = false;
                    lnkAdd.Visible = true;

                    txtItemCode.Text = System.Convert.ToString(ViewState["NewIsbn"]);
                    //  txtBar.Text = System.Convert.ToString(ViewState["NewBar"]);
                    txtItemDesc.Text = System.Convert.ToString(ViewState["NewDesc"]);

                    // LinkAddClick
                    lnkAdd.Attributes.Add("onclick", "return LinkAddClick();");


                }
                else
                {
                    lnkUpdate.Attributes.Add("onclick", "return CheckQuantity('" + txtQuantity.ClientID + "','" + txtItemCost.ClientID + "');");
                    txtFooterQuantity.Visible = false;
                    txtQuantity.Visible = true;
                    txtItemCode.Visible = false;
                    lblItemCode.Visible = true;
                    //txtBar.Visible = false;
                    //lblBar.Visible = true;
                    txtItemDesc.Visible = false;
                    lblDesc.Visible = true;

                    txtNotes.Visible = true;
                    lblNotes.Visible = false;

                    txtFooterCost.Visible = false;
                    txtItemCost.Visible = true;

                }
            }


        }

        protected void btnHide_Click(object sender, EventArgs e)
        {
            btnHide.Visible = false;

        }

        protected void btnAddNewItem_Click(object sender, System.EventArgs e)
        {
            //if (!CheckForItem())
            //    return;

            //ViewState["SpEdit"] = "0";
            //grdCustodyItems.EditItemIndex = -1;  

            //if (Session["RequestItemList"] != null)
            //{
            //    var RequestItemList = (List<view_CustodyList>)Session["RequestItemList"];
            //    var newItem = new view_CustodyList();
            //    newItem.ItemCode = ZeroIntergerIFNull(hidItemID.Value);
            //    newItem.ItemRefCode = gets(ViewState["NewIsbn"]);
            //    newItem.ItemNameAr = gets(ViewState["NewDesc"]);
            //    newItem.ItemNameEn = gets(ViewState["NewDesc"]);
            //    newItem.Qty = ZeroIntergerIFNull(hidQty.Value);


            //    RequestItemList.Add(newItem);
            //    fillRequestItems();
            //}



            //string found = IsItemStored(hidItemID.Value);
            //if (found.Trim().Equals(""))
            //{
            //    string code = VoucherItems.ins.Code();
            //    VoucherItems.ins.Insert(code, System.Convert.ToString(ViewState("Item")), hidItemID.Value, hidQty.Value, hidPrice.Value, hidContentID.Value, hidCurrency.Value);
            //}
            //else
            //{
            //    string[] data = found.Split(',');
            //    string code = data[0];
            //    int curr = System.Convert.ToInt32(data[1]);
            //    curr = curr + System.Convert.ToDouble(hidQty.Value);
            //    VoucherItems.ins.Update(code, System.Convert.ToString(curr), hidPrice.Value, hidCurrency.Value);
            //}



        }


        private bool CheckForItem()
        {


            string isbn = hidIsbn.Value;
            string bar = hidBar.Value;
            string desc = hidDesc.Value;

            view_ItemCard itemobj = objRepository.getItemMaster(isbn, desc);

            if (itemobj == null)
            {
                string script = FormatErrorMSGSwal("عفوا ، المادة غير مسجلة ", "2");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel2", script, true);

                return false;
            }

            string itemid = getDBString(itemobj.Code);
            string newDesc = getDBString(itemobj.ItemNameAr);
            string newIsbn = getDBString(itemobj.ItemRefCode);

            hidItemID.Value = itemid;
            hidContentID.Value = "0";
            ViewState["NewDesc"] = newDesc;
            ViewState["NewBar"] = "";
            ViewState["NewIsbn"] = newIsbn;
            hidBar.Value = "";
            hidIsbn.Value = newIsbn;
            hidDesc.Value = newDesc;

            return true;
        }

       

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            fillRequestItems();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
            fillRequestItems();

        }

        protected void lnkQuick_Click(object sender, EventArgs e)
        {
            //var objHeader = objRepository.getTrackingRequestHeaderByCode((txtFilterCode.Text));
            //if (objHeader != null)
            //{
            //    hdnMasterID.Value = gets(objHeader.Code);
            //    ViewState["itemID"] = gets(objHeader.Code);

            //    hdnType.Value = objHeader.ProcessType.ToString();
            //    if (hdnType.Value == "1")
            //    {
            //        custodyType.SelectedValue = hdnType.Value;
            //        divEmployee.Visible = true;
            //    }
            //    else
            //    {
            //        custodyType.SelectedValue = hdnType.Value;
            //        divEmployee.Visible = false;
            //    }
            //    selectedLocation.Value = objHeader.ToLocationId.ToString();
            //    if (objHeader.EmpRefCode != null && objHeader.EmpRefCode != 0)
            //    {
            //        lstRefEmployee.SelectedValue = gets(objHeader.EmpRefCode);
            //    }

            //    fillRequestItems();

            //    lnkPrintRequest.HRef = Resources.Utilities.cutureRoute + "/Modules/Reports/AssetReceipt.aspx?docId=" + hdnMasterID.Value;
            //    viewPrint.Visible = true;

            //}
            //else
            //{
            //    viewPrint.Visible = false;
            //    string script = FormatErrorMSGSwal("عفوا ، كود الإستمارة غير مسجل   ", "1");
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            //    return;
            //}

        }
    }
}


