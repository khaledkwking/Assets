using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using UI.Web.Admin.Controller;
using UI.Web.Controllers;
using UI.Web.Core.Enums;
using Newtonsoft.Json;

namespace UI.Web.Modules.Assets
{
    public partial class AssetRegister : BaseFormAdmin
    {
        #region "Page Members"
        public AssetsRepository objRepository = IoC.Resolve<AssetsRepository>();
        public LocationsRepository objLocationRepository = IoC.Resolve<LocationsRepository>();

        public LocationsRepository objRepositorys = IoC.Resolve<LocationsRepository>();
        public string _PageTitles = Resources.Pages.orgChart;

        public List<D_Locations> Building = new List<D_Locations>();
        public List<D_Locations> Floor = new List<D_Locations>();
        public List<D_Locations> Room = new List<D_Locations>();
        public string _PageTitle = Resources.Pages.CustodyAdd;
        public class Product
        {
            public int entitycode { get; set; }
            public string entitytype { get; set; }
            public string entityname { get; set; }
            public int parentcode { get; set; }
        }
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
        //private void PopulateGridView()
        //{
        //    string apiUrl = "http://localhost:26404/api/CustomerAPI";
        //    object input = new
        //    {
        //        Name = txtName.Text.Trim(),
        //    };
        //    string inputJson = (new JavaScriptSerializer()).Serialize(input);
        //    WebClient client = new WebClient();
        //    client.Headers["Content-type"] = "application/json";
        //    client.Encoding = Encoding.UTF8;
        //    string json = client.UploadString(apiUrl + "/GetCustomers", inputJson);

        //    gvCustomers.DataSource = (new JavaScriptSerializer()).Deserialize<List<Customer>>(json);
        //    gvCustomers.DataBind();
        //}
        //public List<ORGANIZATION_CHART> GetAllEntities()
        //{
        //    using (var client = new System.Net.Http.HttpClient())
        //    {
        //        client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        //var response = client.GetAsync(string.Format("orgchart/GetChart").Result();
        //        HttpResponseMessage response =  client.GetAsync(string.Format("orgchart/GetChart")).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string responseString = response.Content.ReadAsStringAsync().Result;
        //            //var responseString = Res.Content.ReadAsStringAsync().Result;

        //            // Newtonsoft.Json.Linq.JArray json = Newtonsoft.Json.Linq.JArray.Parse(responseString);
        //            return JsonConvert.DeserializeObject<List<ORGANIZATION_CHART>>(responseString);
        //            // product = Newtonsoft.Json.JsonConvert.Deserialize<Product>(responseString);
        //        }
        //        else
        //            return null;
        //    }
        //}
        protected void Page_Load(object sender, System.EventArgs e)
        {
           
            string selectedValue = hdnSelectedValue.Value;
            if (Session["OraEmpList"] != null)
            {

            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            lblerror.Text = "";

            btnSave.Attributes.Add("onclick", "return chkImage();");

            if (!IsPostBack)
            {
               // var x = GetAllEntities();
                fillddls();
                fillLookups();
                //ClearForm();

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
                        divLocationPersonal.Visible = true;
                        divLocationOrg.Visible = false;

                        _PageTitle = Resources.Pages.CustodyAdd;
                    }
                    else if (Request.QueryString["t"].ToString() == "2")
                    {
                        custodyType.SelectedValue = Request.QueryString["t"].ToString();
                        divEmployee.Visible = false;
                        divLocationPersonal.Visible = false;
                        divLocationOrg.Visible = true;

                        _PageTitle = Resources.Pages.CustodyAdd1;
                    }
                    else if (Request.QueryString["t"].ToString() == "3")
                    {
                        custodyType.SelectedValue = Request.QueryString["t"].ToString();
                        _PageTitle = Resources.Pages.CustodyAdd1;
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
                    //loadEmpRequest(ZeroIntergerIFNull(Request.QueryString["empid"]));
                }
                fillRequestItems();
            }
            if (IsPostBack && Request["__EVENTTARGET"] == "upGridView")
            {
                // Deserialize the hidden field data to get the grid data
                var gridDataJson = hdnGridData.Value;
                if (!string.IsNullOrEmpty(gridDataJson))
                {
                    var gridData = JsonConvert.DeserializeObject<List<view_CustodyList>>(gridDataJson); // Replace CustodyItem with your data model
                    grdCustodyItems.DataSource = gridData;
                    grdCustodyItems.DataBind();
                    
                    lstRefEmployee.SelectedValue = "329";
                    lstRefEmployee_SelectedIndexChanged(null, null);
                }
            }

        }


        private void loadRequest(int RequestHeaderCode)
        {
            var objHeader = objRepository.getTrackingRequestHeaderDetails(RequestHeaderCode);
            if (objHeader != null)
            {


                hdnType.Value = objHeader.RequestActionType.ToString();
                if (hdnType.Value == "1")
                {
                    custodyType.SelectedValue = hdnType.Value;
                    divEmployee.Visible = true;
                }
                else
                {
                    custodyType.SelectedValue = hdnType.Value;
                    divEmployee.Visible = false;
                }
                selectedLocation.Value = objHeader.ToLocationId.ToString();
                hdnOrgChartRefCode.Value = objHeader.OrgChartRefCode.ToString();

                if (objHeader.EmpRefCode != null && objHeader.EmpRefCode != 0)
                {
                    lstRefEmployee.SelectedValue = gets(objHeader.EmpRefCode);

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
                hdnType.Value = objHeader.RequestActionType.ToString();
                if (hdnType.Value == "1")
                {
                    custodyType.SelectedValue = hdnType.Value;
                    divEmployee.Visible = true;
                }
                else
                {
                    custodyType.SelectedValue = hdnType.Value;
                    divEmployee.Visible = false;
                }
                selectedLocation.Value = objHeader.ToLocationId.ToString();

                hdnOrgChartRefCode.Value = objHeader.OrgChartRefCode.ToString();

                if (objHeader.EmpRefCode != null && objHeader.EmpRefCode != 0)
                {
                    lstRefEmployee.SelectedValue = gets(objHeader.EmpRefCode);

                }

                hdnMasterID.Value = gets(objHeader.Code);
                ViewState["itemID"] = gets(objHeader.Code);

                lnkPrintRequest.HRef = Resources.Utilities.cutureRoute + "/Modules/Reports/AssetReceipt.aspx?docId=" + hdnMasterID.Value;
                viewPrint.Visible = true;
            }
            else
            {
                // set Selected Employee 
                viewPrint.Visible = false;
                lstRefEmployee.SelectedValue = gets(EmpId);
                lstRefEmployee_SelectedIndexChanged(null, null);
            }


        }



        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            string script = "";

            string s = "sebrahim_if";

            try
            {
                int LocationID = 0;
                int? OrgChartRefCode = null;
                //Validated Location Personal
                if (Request.QueryString["t"].ToString() == "1")
                {
                    var selectedLocationOrg = objLocationRepository.GetDetails(ZeroIntergerIFNull(selectedLocation.Value));
                    if (selectedLocationOrg != null)
                    {
                        if (selectedLocationOrg.OrgChartRefCode != null && selectedLocationOrg.OrgChartRefCode != 0)
                        {
                            hdnOrgChartRefCode.Value = selectedLocationOrg.OrgChartRefCode.ToString();
                        }
                        else
                        {
                            script = FormatErrorMSGSwal("عفوا ، يرجى تسجيل تبعية موقع العهدة لجهة ", "1");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                            return;
                        }

                    }
                }
                else if (Request.QueryString["t"].ToString() == "2") //Validated Location Org 
                {
                    //if (ddlDirection.SelectedValue == "0")
                    //{
                    //    script = FormatErrorMSGSwal("عفوا ، يرجى اختيار جهة موقع العهدة  ", "1");
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                    //    return;
                    //}
                    if (Session["IsBuildingSelected"].ToString() == "False" || ddlBuilding.SelectedValue == "0")
                    {
                        script = FormatErrorMSGSwal("عفوا ، يرجى اختيار المبنى الخاص بموقع العهدة  ", "1");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                        return;
                    }
                    else if (Session["FloorRequiredSelect"].ToString() == "True" && ddlFloor.SelectedValue == "0")
                    {
                        script = FormatErrorMSGSwal("عفوا ، يرجى اختيار الدور الخاص بموقع العهدة  ", "1");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                        return;
                    }
                    else if (Session["RoomRequiredSelect"].ToString() == "True" && ddlRoom.SelectedValue == "0")
                    {
                        script = FormatErrorMSGSwal("عفوا ، يرجى اختيار الغرفة الخاص بموقع العهدة  ", "1");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                        return;
                    }

                    LocationID = ZeroIntergerIFNull(Session["LocationCode"].ToString());
                    OrgChartRefCode = objLocationRepository.GetDetails(LocationID).OrgChartRefCode;
                }

                if (gets(ViewState["itemID"]).Equals("0"))
                {//Save

                    // Add Request Header
                    AssetsEventTrackingHeader objHeader = new AssetsEventTrackingHeader();
                    objHeader.RequestDate = NullDateifEmpty(txtFromDate.Text);
                    objHeader.DueDate = NullDateifEmpty(txtReturnDate.Text);
                    objHeader.RequestRefCode = Guid.NewGuid().ToString();
                    objHeader.RequestActionType = ZeroIntergerIFNull(hdnType.Value);  //(int) CustodyRequestType.CheckOut;
                    objHeader.ProcessType = (int)CustodyProcessTypes.CheckOut;
                    objHeader.TMonth = NullDateifEmpty(txtFromDate.Text).Month;
                    objHeader.TYear = NullDateifEmpty(txtFromDate.Text).Year;
                    objHeader.Serial = generateRequestSerial();

                    if (Request.QueryString["t"].ToString() == "1")
                    {
                        objHeader.ToLocationId = ZeroIntergerIFNull(selectedLocation.Value);
                        objHeader.OrgChartRefCode = ZeroIntergerIFNull(hdnOrgChartRefCode.Value);
                    }
                    else if (Request.QueryString["t"].ToString() == "2")
                    {
                        objHeader.ToLocationId = LocationID;
                        objHeader.OrgChartRefCode = OrgChartRefCode;
                    }

                    objHeader.RequestNotes = txtNotes.Text;

                    if (hdnType.Value == "1")
                    {
                        objHeader.EmpName = lstRefEmployee.SelectedItem.Text;
                        objHeader.EmpRefCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);

                        // Check if Emplyee Location Saved 
                        if (lstRefEmployee.SelectedValue != "-1")
                        {
                            var emplocation = objRepository.getEmployeeLocations(ZeroIntergerIFNull(lstRefEmployee.SelectedValue));
                            if (emplocation == null && selectedLocation.Value != "")
                            {
                                D_EmployeeLocations locationObj = new D_EmployeeLocations();
                                locationObj.EmpCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);
                                locationObj.LocationCode = ZeroIntergerIFNull(selectedLocation.Value);
                                objRepository.AddEmployeeLoation(locationObj);

                                //script = FormatpopupErrorMSG("Employee Location added successfully", "3");
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

                            }
                            else if (emplocation.LocationCode != ZeroIntergerIFNull(selectedLocation.Value))
                            {// Update Employee Location
                                D_EmployeeLocations locationObj = objRepository.getEmployeeLocations(ZeroIntergerIFNull(lstRefEmployee.SelectedValue));
                                locationObj.EmpCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);
                                locationObj.LocationCode = ZeroIntergerIFNull(selectedLocation.Value);
                                objRepository.UpdateEmployeeLoation(locationObj);

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
                    objHeader.RequestActionType = ZeroIntergerIFNull(hdnType.Value);  //(int) CustodyRequestType.CheckOut;
                    objHeader.ProcessType = (int)CustodyProcessTypes.CheckOut;

                    objHeader.RequestDate = NullDateifEmpty(txtFromDate.Text);
                    objHeader.DueDate = NullDateifEmpty(txtReturnDate.Text);
                    objHeader.TMonth = NullDateifEmpty(txtFromDate.Text).Month;
                    objHeader.TYear = NullDateifEmpty(txtFromDate.Text).Year;
                    objHeader.ToLocationId = ZeroIntergerIFNull(selectedLocation.Value);
                    objHeader.OrgChartRefCode = ZeroIntergerIFNull(hdnOrgChartRefCode.Value);
                    objHeader.RequestNotes = txtNotes.Text;

                    if (hdnType.Value == "1")
                    {
                        objHeader.EmpName = lstRefEmployee.SelectedItem.Text;
                        objHeader.EmpRefCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);
                    }

                    objHeader.LastModifiedAt = DateTime.Now;
                    objHeader.LastModifiedBy = ZeroIntergerIFNull(ReadSession("userid").ToString());
                    objRepository.UpdateAssetsEventTrackingHeader(objHeader);

                    hdnMasterID.Value = objHeader.Code.ToString();
                    // Add Items Details
                    SaveRequestItems(objHeader.Code);
                }

                script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
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
                        obj.ToLocationId = ZeroIntergerIFNull(selectedLocation.Value);
                        if (hdnType.Value == "1")
                        {
                            obj.EmpName = lstRefEmployee.SelectedItem.Text;
                            obj.EmpRefCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);
                        }
                        obj.Notes = objItemList[i].Notes;
                        obj.StoreRequestRefCode = objItemList[i].StoreRequestRefCode;
                        obj.Qty = objItemList[i].Qty;

                        obj.CreatedAt = DateTime.Now;
                        obj.CreatedBy = ZeroIntergerIFNull(ReadSession("userid").ToString());

                        objRepository.AddEventTracking(obj);

                    }
                    else
                    {//UpdateExisting
                        obj = objRepository.getTrackingDetails(ZeroIntergerIFNull(grdCustodyItems.Items[i].Cells[2].Text));

                        obj.RequestHeaderCode = headerCode;
                        obj.AssetCode = objItemList[i].ItemCode;
                        obj.ToLocationId = ZeroIntergerIFNull(selectedLocation.Value);
                        if (hdnType.Value == "1")
                        {
                            obj.EmpName = lstRefEmployee.SelectedItem.Text;
                            obj.EmpRefCode = ZeroIntergerIFNull(lstRefEmployee.SelectedValue);
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


        
        //[HttpGet]
        //public IHttpActionResult GetEmployeeCustodyItems(int employeeId)
        //{
        //    var custodyItems = YourDataAccessLayer.GetCustodyItems(employeeId); // Replace with your method to get data
        //    return Ok(custodyItems);
        //}
        public void fillRequestItems()
        {
            int refHCode = ZeroIntergerIFNull(hdnMasterID.Value);
            if (Request.QueryString["t"].ToString() == "1")
            {
                refHCode = 0;
            }

            var objList = objRepository.getCustodyListByMasterData(refHCode, ZeroIntergerIFNull(selectedLocation.Value), ZeroIntergerIFNull(lstRefEmployee.SelectedValue));

            if (objList != null && objList.Count > 0)
            {
                // Fill Request Master Information
                hdnMasterID.Value = gets(objList[0].RequestHeaderCode);
                ViewState["itemID"] = gets(objList[0].RequestHeaderCode);
                txtFromDate.Text = NullDateifEmptyText(objList[0].RequestDate);
                txtReturnDate.Text = NullDateifEmptyText(objList[0].DueDate);
                txtNotes.Text = gets(objList[0].RequestNotes);

                //if (txtReturnDate.Text != "")
                //{
                //    chkReturnDate.Checked = true;
                //}
                //else { chkReturnDate.Checked = false; }


            }
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
                    //newItem.EstimatedUnitCost = itemobj.EstimatedUnitCost.Value;
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
            txtReturnDate.Text = "";
            txtNotes.Text = "";
            ViewState["itemID"] = "0";
            selectedLocation.Value = "0";
            lstRefEmployee.SelectedValue = "0";
            Session.Remove("RequestItemList");
            Session["RequestItemList"] = null;
            //divSelectedEmployeeInfo.Visible = false;
        }
        private void FillInboundMasterInformation()
        {

            //var objList = objRepository.FillDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            //if ((objList != null))
            //{


            //    txtSerial.Text = gets(objList.Serial);

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
            //FillDllwithoptional(LooksUpsRepository.ins.FillLocation(), lstOwnerLocationCode, "LocationNameAr", "Code");

            if (Session["OraEmpList"] != null)
            {
                //BindEmployeeLst((List<EmployeeViewModel>)Session["OraEmpList"], lstRefEmployee, "EMP_NAME", "EMP_ID");
            }
            else
            {
                // Request Data From Ora.
               // var Emplist = GetOraEmpList(0); // Get All Active Employee
               // Session["OraEmpList"] = Emplist;
                //BindEmployeeLst(Emplist, lstRefEmployee, "EMP_NAME", "EMP_ID");
            }



            /// FillDllwithoptional(LooksUpsRepository.ins.FillEmployee(), lstRefEmployee, "EmpName", "Code");


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
            var objHeader = objRepository.getTrackingRequestHeaderByCode((txtFilterCode.Text));
            if (objHeader != null)
            {
                hdnMasterID.Value = gets(objHeader.Code);
                ViewState["itemID"] = gets(objHeader.Code);

                hdnType.Value = objHeader.RequestActionType.ToString();
                if (hdnType.Value == "1")
                {
                    custodyType.SelectedValue = hdnType.Value;
                    divEmployee.Visible = true;
                }
                else
                {
                    custodyType.SelectedValue = hdnType.Value;
                    divEmployee.Visible = false;
                }
                selectedLocation.Value = objHeader.ToLocationId.ToString();
                if (objHeader.EmpRefCode != null && objHeader.EmpRefCode != 0)
                {
                    lstRefEmployee.SelectedValue = gets(objHeader.EmpRefCode);
                }

                fillRequestItems();

                lnkPrintRequest.HRef = Resources.Utilities.cutureRoute + "/Modules/Reports/AssetReceipt.aspx?docId=" + hdnMasterID.Value;
                viewPrint.Visible = true;

            }
            else
            {
                viewPrint.Visible = false;
                string script = FormatErrorMSGSwal("عفوا ، كود الإستمارة غير مسجل   ", "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                return;
            }

        }

        private void fillddls()
        {
            //ddlDirection.Items.Add(new ListItem("الجهة", "-1"));
            //ddlAmana.Items.Add(new ListItem("الامانة", "-1"));
            //ddlDepartment.Items.Add(new ListItem("الادارة", "-1"));
            //ddlMorakba.Items.Add(new ListItem("المراقبة", "-1"));
            //ddlSection.Items.Add(new ListItem("القسم", "-1"));
            //ddlBuilding.Items.Add(new ListItem("المبنى", "-1"));
            //ddlFloor.Items.Add(new ListItem("الدور", "-1"));
            //ddlRoom.Items.Add(new ListItem("الغرفة", "-1"));

            //  var query = GetAllEntities().Where(o => o.PARENTCODE == 0 || o.PARENTCODE == null);

            // BindDirections_Location(query, ddlDirection, "--- اختر الجهة ---", "ENTITYNAME", "ENTITYCODE");
        }

        public void BindDirections_Location(object lstData, DropDownList ddl, string ddlName, string txtField, string valueField)
        {

            ddl.DataSource = lstData;
            ddl.DataTextField = txtField;
            ddl.DataValueField = valueField;
            ddl.DataBind();
            //ddl.Items.Add(new ListItem(ddlName, "0"));

            ListItem listItem = new ListItem(ddlName, "0");
            listItem.Attributes.Add("style", "background-color: Gold !important;");
            ddl.Items.Add(listItem);

            try
            {
                ddl.SelectedValue = "0";
            }
            catch (Exception)
            {


            }
        }

        //protected void ddlDirection_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(ddlDirection.SelectedValue) && ddlDirection.SelectedValue != "0")
        //    {
        //        int DirID = int.Parse(ddlDirection.SelectedValue);
        //        var query = GetAllEntities().Where(o => o.PARENTCODE == DirID && o.ENTITYTYPE == "amana");

        //        BindDirections_Location(query, ddlAmana, "--- اختر ---", "ENTITYNAME", "ENTITYCODE");
        //        ClearLocation_ddl();
        //        fillLocations(DirID);
        //    }
        //}

        //protected void ddlAmana_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(ddlAmana.SelectedValue) && ddlAmana.SelectedValue != "0")
        //    {
        //        int AmanaID = int.Parse(ddlAmana.SelectedValue);
        //        var query = GetAllEntities().Where(o => o.PARENTCODE == AmanaID && o.ENTITYTYPE == "dept");

        //        BindDirections_Location(query, ddlDepartment, "--- اختر ---", "ENTITYNAME", "ENTITYCODE");
        //        ClearLocation_ddl();
        //        fillLocations(AmanaID);
        //    }
        //}

        //protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue) && ddlDepartment.SelectedValue != "0")
        //    {
        //        int DeptID = int.Parse(ddlDepartment.SelectedValue);
        //        var query = GetAllEntities().Where(o => o.PARENTCODE == DeptID && o.ENTITYTYPE == "div");

        //        BindDirections_Location(query, ddlMorakba, "--- اختر ---", "ENTITYNAME", "ENTITYCODE");
        //        ClearLocation_ddl();
        //        fillLocations(DeptID);
        //    }
        //}

        //protected void ddlMorakba_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(ddlMorakba.SelectedValue) && ddlMorakba.SelectedValue != "0")
        //    {
        //        int MorakbaID = int.Parse(ddlMorakba.SelectedValue);
        //        var query = GetAllEntities().Where(o => o.PARENTCODE == MorakbaID && o.ENTITYTYPE == "sec");

        //        BindDirections_Location(query, ddlSection, "--- اختر ---", "ENTITYNAME", "ENTITYCODE");
        //        ClearLocation_ddl();
        //        fillLocations(MorakbaID);
        //    }
        //}

        //protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int SecID = int.Parse(ddlSection.SelectedValue);
        //    ClearLocation_ddl();
        //    fillLocations(SecID);
        //}

        protected void ddlBuilding_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["IsBuildingSelected"] = "True";
            var Floorlist = (List<D_Locations>)Session["Floor"];
            int BuildID = int.Parse(ddlBuilding.SelectedValue);
            Session["FloorRequiredSelect"] = "False";
            var queryFloor = Floorlist.Where(o => o.LocationParentId == BuildID);
            if (queryFloor.Count() != 0)
            {
                BindDirections_Location(queryFloor, ddlFloor, "--- اختر ---", "LocationNameAr", "Code");
                Session["FloorRequiredSelect"] = "True";
            }
            else
            {
                var Q = objLocationRepository.GetList(BuildID, "");
                BindDirections_Location(Q, ddlFloor, "--- اختر ---", "LocationNameAr", "Code");

            }
            Session["LocationCode"] = BuildID;
        }

        protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Roomlist = (List<D_Locations>)Session["Room"];

            int FloorID = int.Parse(ddlFloor.SelectedValue);

            Session["RoomRequiredSelect"] = "False";
            var queryRoom = Roomlist.Where(o => o.LocationParentId == FloorID);
            if (queryRoom.Count() != 0)
            {
                BindDirections_Location(queryRoom, ddlRoom, "--- اختر ---", "LocationNameAr", "Code");
                Session["RoomRequiredSelect"] = "True";
            }
            else
            {
                var Q = objLocationRepository.GetList(FloorID, "");
                BindDirections_Location(Q, ddlRoom, "--- اختر ---", "LocationNameAr", "Code");
            }
            Session["LocationCode"] = FloorID;
        }
        protected void ddlRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int RoomID = int.Parse(ddlRoom.SelectedValue);
            Session["LocationCode"] = RoomID;
        }

        private void fillLocations(int OrgChartRefCode)
        {
            Session["LocationCode"] = null;

            Session["IsBuildingSelected"] = "False";
            Session["FloorRequiredSelect"] = "False";
            Session["RoomRequiredSelect"] = "False";

            Session["Building"] = null;
            Session["Floor"] = null;
            Session["Room"] = null;
            Building.Clear();
            Floor.Clear();
            Room.Clear();
            var locationObj = objLocationRepository.getEntityLocations(OrgChartRefCode, "").ToList();
            foreach (var item in locationObj)
            {
                if (item.LocationType == 1)
                {
                    if (Building.Where(o => o.Code == item.Code).Count() == 0)
                        Building.Add(new D_Locations() { Code = item.Code, LocationNameAr = item.LocationNameAr, LocationParentId = item.LocationParentId });
                }
                else if (item.LocationType == 2)
                {
                    if (Floor.Where(o => o.Code == item.Code).Count() == 0)
                    {
                        Floor.Add(new D_Locations() { Code = item.Code, LocationNameAr = item.LocationNameAr, LocationParentId = item.LocationParentId });

                        var queryBuild = objLocationRepository.GetDetails(ZeroIntergerIFNull(item.LocationParentId.ToString()));
                        if (queryBuild != null)
                        {
                            if (Building.Where(o => o.Code == queryBuild.Code).Count() == 0)
                                Building.Add(new D_Locations() { Code = queryBuild.Code, LocationNameAr = queryBuild.LocationNameAr, LocationParentId = queryBuild.LocationParentId });
                        }

                    }
                }
                else if (item.LocationType == 3)
                {
                    if (Room.Where(o => o.Code == item.Code).Count() == 0)
                    {
                        Room.Add(new D_Locations() { Code = item.Code, LocationNameAr = item.LocationNameAr, LocationParentId = item.LocationParentId });


                        var queryFloor = objLocationRepository.GetDetails(ZeroIntergerIFNull(item.LocationParentId.ToString()));
                        if (queryFloor != null)
                        {
                            if (Floor.Where(o => o.Code == queryFloor.Code).Count() == 0)
                            {
                                Floor.Add(new D_Locations() { Code = queryFloor.Code, LocationNameAr = queryFloor.LocationNameAr, LocationParentId = queryFloor.LocationParentId });
                                var queryBuildFloor = objLocationRepository.GetDetails(ZeroIntergerIFNull(queryFloor.LocationParentId.ToString()));
                                if (queryBuildFloor != null)
                                {
                                    if (Building.Where(o => o.Code == queryBuildFloor.Code).Count() == 0)
                                        Building.Add(new D_Locations() { Code = queryBuildFloor.Code, LocationNameAr = queryBuildFloor.LocationNameAr, LocationParentId = queryBuildFloor.LocationParentId });
                                }

                            }
                        }
                    }
                }

                BindDirections_Location(Building, ddlBuilding, "--- اختر ---", "LocationNameAr", "Code");

                Session["Building"] = Building;
                Session["Floor"] = Floor;
                Session["Room"] = Room;

            }
        }

        private void ClearLocation_ddl()
        {
            ddlBuilding.Items.Clear();
            ddlFloor.Items.Clear();
            ddlRoom.Items.Clear();

        }

        protected void lstRefEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set Employee Location
            //var selectedEmpInfo = objRepository.getEmployeeDetails(ZeroIntergerIFNull(lstRefEmployee.SelectedValue));
            //if (selectedEmpInfo != null)
            //{
            //   selectedLocation.Value = gets(selectedEmpInfo.LocationRefCode);
            //    fillRequestItems();
            //}

            List<EmployeeViewModel> Emplist = new List<EmployeeViewModel>();


            if (Session["OraEmpList"] != null)
            {
                Emplist = (List<EmployeeViewModel>)Session["OraEmpList"];
            }
            else
            {
                // Request Data From Ora.
                Emplist = GetOraEmpList(0);
                Session["OraEmpList"] = Emplist;
            }
            var selectedEmpInfo = Emplist.Where(x => x.EMP_ID == lstRefEmployee.SelectedValue).FirstOrDefault();

            if (selectedEmpInfo != null)
            {
                // Get Emploation

                // set Selected Employee Data

                divSelectedEmployeeInfo.Visible = true;


                lblSelectedEmpName.Text = selectedEmpInfo.EMP_NAME;
                lblSelectedjobTitle.Text = selectedEmpInfo.JOB_NAME;
                lblSelectedEmpCode.Text = selectedEmpInfo.EMP_ID;

                lblSelectedEmpLocationName.Text = GetEmp_Location(ZeroIntergerIFNull(selectedEmpInfo.EMP_ID)); //selectedEmpInfo.ENTITYNAME;

                var empLocation = objRepository.getEmployeeLocations(ZeroIntergerIFNull(selectedEmpInfo.EMP_ID));
                if (empLocation != null)
                {
                    selectedLocation.Value = gets(empLocation.LocationCode);
                }
                Session["RequestItemList"] = null;
                fillRequestItems();
            }
            else
            { divSelectedEmployeeInfo.Visible = false; }



        }
    }
}