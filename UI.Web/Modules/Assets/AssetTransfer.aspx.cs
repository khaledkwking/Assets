using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using Newtonsoft.Json;
using UI.Web.Admin.Controller;
using System.Web.Http.Results;
using System.Configuration;
using UI.Web.Helper;

namespace UI.Web.Modules.Assets
{
    public partial class AssetTransfer : BaseFormAdmin
    {
        #region "Page Members"
        public AssetsRepository objRepository = IoC.Resolve<AssetsRepository>();
        public InboundRepository objInboundRepository = IoC.Resolve<InboundRepository>();
        public string _PageTitle = Resources.Pages.AssetTransfer;

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

                //if (Request.QueryString["t"] != null)
                //{
                //    hdnType.Value = Request.QueryString["t"].ToString();
                //    if (Request.QueryString["t"].ToString() == "1")
                //    {
                //        custodyType.SelectedValue = Request.QueryString["t"].ToString();
                //        divEmployee.Visible = true;
                //    }
                //    else
                //    {
                //        custodyType.SelectedValue = Request.QueryString["t"].ToString();
                //        divEmployee.Visible = false;
                //    }

                //}
                filterItem();
                filterToItem();
                filterStoreItem();

                //FillSelectedItems();
            }


        }

        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            string script = "";
            try
            {
                AssetsEventTracking obj = new AssetsEventTracking();
                for (int i = 0; i <= grdSelectedItems.Items.Count - 1; i++)
                {

                    var selectedItem = (AssetsItemUnit)objRepository.getItemDetailsForEdit(ZeroIntergerIFNull(grdSelectedItems.Items[i].Cells[0].Text));
                    obj.AssetCode = selectedItem.Code;
                    obj.ActionDate = NullDateifEmptyNew(txtFromDate.Text);


                    obj.actionId = 2;// Tranfered;
                    obj.statusId = 2;// CHecked OUt ;
                    obj.ToLocationId = ZeroIntergerIFNull(selectedToLocation.Value);

                    if (lstToEmpRefCode.SelectedValue != "0")
                    {
                        obj.EmpName = lstToEmpRefCode.SelectedItem.Text;
                        obj.EmpRefCode = ZeroIntergerIFNull(lstToEmpRefCode.SelectedValue);
                    }
                    obj.Notes = txtNotes.Text;

                    obj.CreatedAt = DateTime.Now;
                    obj.CreatedBy = ZeroIntergerIFNull(ReadSession("userId").ToString());

                    objRepository.AddEventTracking(obj);


                    //update
                    selectedItem.LastEventTrackingId = obj.Code;
                    objInboundRepository.UpdateItemunit(selectedItem);
                }
                Session["selectedItems"] = null;
                filterItem();
                filterToItem();
                filterStoreItem();


                //FillSelectedItems();
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

        private void filterItem()
        {
            //getTrackingRequestHeaderByEmpCode
            var objList = objRepository.getCustodyListByMasterData(0,0, ZeroIntergerIFNull(lstRefEmployee.SelectedValue));
            lblcount.Text = (Resources.Utilities.foundTotal + (objList.Count.ToString()).ToString() + Resources.Utilities.records);


            decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdItems.PageSize));
            if ((c <= grdItems.CurrentPageIndex))
            {
                grdItems.CurrentPageIndex = 0;
            }



            grdItems.DataSource = objList;
            grdItems.DataBind();
            int _totalCount = objList.Count;
            //.pager1.ItemCount = objList.Count;


        }

        private void filterToItem()
        {
            //getTrackingRequestHeaderByEmpCode
            var objList = objRepository.getCustodyListByMasterData(0, 0, ZeroIntergerIFNull(lstToEmpRefCode.SelectedValue));
            lblSelectedCount.Text = (Resources.Utilities.foundTotal + (objList.Count.ToString()).ToString() + Resources.Utilities.records);


            decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdSelectedItems.PageSize));
            if ((c <= grdSelectedItems.CurrentPageIndex))
            {
                grdSelectedItems.CurrentPageIndex = 0;
            }



            grdSelectedItems.DataSource = objList;
            grdSelectedItems.DataBind();
            int _totalCount = objList.Count;
            //.pager1.ItemCount = objList.Count;


        }

        private void FillSelectedItems()
        {

            if (Session["selectedItems"] != null)
            {
                var objList = (List<view_CustodyList>)Session["selectedItems"];
                decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdSelectedItems.PageSize));
                if ((c <= grdSelectedItems.CurrentPageIndex))
                {
                    grdSelectedItems.CurrentPageIndex = 0;
                }
                grdSelectedItems.Visible = true;
                grdSelectedItems.DataSource = objList;
                grdSelectedItems.DataBind();
                int _totalCount = objList.Count;
                //pager2.ItemCount = objList.Count;

                hdnItemCount.Value = objList.Count.ToString();


            }
            else
            {
                //grdSelectedItems.Visible = false;
                //lblSelectedCount.Visible = false;
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

            //pager1.CurrentIndex = currnetPageIndx;
            grdItems.CurrentPageIndex = (currnetPageIndx - 1);
            filterItem();
        }

 

        protected void btnReload_Click(object sender, EventArgs e)
        {
            filterItem();
            filterToItem();
            filterStoreItem();
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
            
            selectedToLocation.Value = "0";
            lstToEmpRefCode.SelectedValue = "0";
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
            //  FillDllwithoptional(LooksUpsRepository.ins.FillLocation(), lstOwnerLocationCode, "LocationNameAr", "Code");

            //if (Session["OraEmpList"] != null)
            //{
            //    FillDllwithoptional((List<EmployeeViewModel>)Session["OraEmpList"], lstRefEmployee, "EMP_NAME", "EMP_ID");
            //    FillDllwithoptional((List<EmployeeViewModel>)Session["OraEmpList"], lstToEmpRefCode, "EMP_NAME", "EMP_ID");
            //}
            //else
            //{
            //    // Request Data From Ora.
            //    var Emplist = GetOraEmpList(1);
            //    Session["OraEmpList"] = Emplist;
            //    FillDllwithoptional(Emplist, lstRefEmployee, "EMP_NAME", "EMP_ID");
            //}
      

            FillDllwithoptional(objRepository.getStores(), lstToStore, "Name", "Id");

            FillDllwithoptional(GetEmployeeHierarchy(1), lstToEmpRefCode, "EMP_NAME", "EMP_ID");

            FillDllwithoptional(GetEmployeeHierarchy(1), lstRefEmployee, "EMP_NAME", "EMP_ID");

        }
        public List<EmployeeViewModel> GetEmployeeHierarchy(int nodeId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Set the Base Address from configuration
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["centeralApi"].ToString());

                    // Clear and set request headers
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Send GET request to the API endpoint
                    HttpResponseMessage response = client.GetAsync(string.Format("OrgChart/EmployeeHierarchy/{0}", nodeId)).Result;

                    // Check if the request was successful
                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Failed to retrieve data. Status Code: {response.StatusCode}");

                    // Read the response as a string
                    var result = response.Content.ReadAsStringAsync().Result;

                    // Deserialize the JSON response into a list of EmployeeViewModel
                    var empList = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(result);

                    return empList; // Return the list
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                throw new Exception($"An error occurred while retrieving employee hierarchy: {ex.Message}");
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

        protected void lnkFilter_Click(object sender, EventArgs e)
        {
            filterItem();
        }

        protected void pager2_Command(object sender, CommandEventArgs e)
        {
            Int32 currnetPageIndx = ((Int32)(e.CommandArgument));
            if ((currnetPageIndx <= 0))
            {
                currnetPageIndx = 1;
            }

            if ((currnetPageIndx > grdSelectedItems.PageCount))
            {
                currnetPageIndx = (grdSelectedItems.PageCount - 1);
            }

            //pager2.CurrentIndex = currnetPageIndx;
            grdSelectedItems.CurrentPageIndex = (currnetPageIndx - 1);
 
            //FillSelectedItems();
        }

        protected void lnkRemove_Click(object sender, EventArgs e)
        {
            List<view_CustodyList> objList = new List<view_CustodyList>();
            if (Session["selectedItems"] != null)
            {
                objList = (List<view_CustodyList>)Session["selectedItems"];

            }
            bool checkItem = false;
            bool? IsTransfered = null;

            for (int i = 0; i <= grdSelectedItems.Items.Count - 1; i++)
            {
                if ((grdSelectedItems.Items[i].FindControl("chkItem") != null))
                {
                    CheckBox check = (CheckBox)grdSelectedItems.Items[i].FindControl("chkItem");
                    if (check.Checked)
                    {
                        using (AssetsEntitiesNew en = new AssetsEntitiesNew())
                        {
                            try
                            {
                                int code = ZeroIntergerIFNull(grdSelectedItems.Items[i].Cells[0].Text);
                                var q = en.AssetsEventTrackings.Where(o => o.Code == code).FirstOrDefault();

                                if (q == null)
                                {
                                    throw new Exception($"No record found in AssetsEventTrackings for Code = {code}");
                                }
                                int empID = GetEmpId(ZeroIntergerIFNull(lstRefEmployee.SelectedValue));
                                string ConvertedName = "";
                                if (rblTransferType.SelectedValue == "Employee")
                                {

                                    if (empID != 0)
                                    {
                                        var AssHeader = en.AssetsEventTrackingHeaders.Where(o => o.EmpRefCode == empID).ToList();
                                        if (AssHeader.Count > 0)
                                        {
                                            q.EmpRefCode = empID;
                                            q.EmpName = lstToEmpRefCode.SelectedItem.Text;
                                            q.RequestHeaderCode = AssHeader.FirstOrDefault().Code;
                                            q.LastModifiedAt = DateTime.Now;
                                            q.LastModifiedBy = ZeroIntergerIFNull(ReadSession("userId").ToString());
                                            q.Notes = txtNotes.Text;
                                            q.ActionDate = NullDateifEmptyNew(txtFromDate.Text);
                                            q.statusId = 6;
                                            en.SaveChanges();
                                            IsTransfered = true;
                                            ConvertedName = lstToEmpRefCode.SelectedItem.Text;
                                        }
                                        else
                                            IsTransfered = false;
                                    }

                                }
                                else if (rblTransferType.SelectedValue == "Store")
                                {
                                    if (string.IsNullOrEmpty(lstToStore.SelectedValue) || lstToStore.SelectedValue == "0")
                                    {
                                        lblerror.Text = "يرجى اختيار المخزن .";
                                        return;
                                    }
                                    using (var transaction = en.Database.BeginTransaction())
                                    {
                                        try
                                        {
                                            // تحديث الأصل
                                            q.IsDeleted = false;
                                            q.EmpRefCode = empID;
                                            q.EmpName = lstRefEmployee.SelectedItem.Text;

                                            // حذف السجل إذا موجود
                                            var AssStore = en.AssetsStores.FirstOrDefault(o => o.AssetId == code);

                                            if (AssStore != null)
                                            {
                                                en.AssetsStores.Remove(AssStore);
                                            }
                                            else
                                            {
                                                throw new Exception("السجل غير موجود."); // هيرجع rollback
                                            }

                                            // حفظ كل التغييرات دفعة واحدة
                                            en.SaveChanges();

                                            // إذا كل حاجة نجحت
                                            transaction.Commit();
                                            IsTransfered = true;
                                            ConvertedName = lstToStore.SelectedItem.Text;

                                        }
                                        catch (Exception ex)
                                        {
                                            transaction.Rollback();
                                            lblerror.Text = "حدث خطأ أثناء التحويل: " + ex.Message;
                                        }
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                throw new Exception("An error occurred while updating the records: " + ex.Message, ex);
                            }

                        }
                        checkItem = true;

                    }
                }
            }
            if (checkItem && IsTransfered==true)
            {
                Session["selectedItems"] = objList;
                filterItem();
                filterToItem();
                filterStoreItem();

                Logger.Log(
                   userId: ReadSession("userId").ToString(),
                   userName: ReadSession("AdminName").ToString(),
                   tableName: "AssetsEventTrackings",
                   action: "Transfer",
                   recordId:"From : " +lstToEmpRefCode.SelectedItem.Text + "    " +"To : " +lstRefEmployee.SelectedItem.Text
                   );
                string scriptAdd = "Swal.fire('تم التحويل بنجاح ');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertVendor", scriptAdd, true);
                //FillSelectedItems();
            }
            else if (checkItem && IsTransfered==false)
            {
                string scriptAdd22 = "Swal.fire('عفوا , يجب ادخال عهدة للموظف حتى يتم التحويل اليه');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertVendor", scriptAdd22, true);
            }
            else
            {
                string script22 = "Swal.fire('يرجى اختيار المواد المراد تحويلها ');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertVendor", script22, true);
            }

        }

        protected void lnkAddItem_Click(object sender, EventArgs e)
        {
            List<view_CustodyList> objList = new List<view_CustodyList>();
            if (Session["selectedItems"] != null)
            {
                objList = (List<view_CustodyList>)Session["selectedItems"];

            }
            bool checkItem = false;
            bool? IsTransfered = null;

            for (int i = 0; i <= grdItems.Items.Count - 1; i++)
            {
                if ((grdItems.Items[i].FindControl("chkItem") != null))
                {
                    CheckBox check = (CheckBox)grdItems.Items[i].FindControl("chkItem");

                    if (check.Checked)
                    {
                        using (AssetsEntitiesNew en = new AssetsEntitiesNew())
                        {
                            try
                            {
                                int code = ZeroIntergerIFNull(grdItems.Items[i].Cells[0].Text);
                                var q = en.AssetsEventTrackings.Where(o => o.Code == code).FirstOrDefault();

                                if (q == null)
                                {
                                    throw new Exception($"No record found in AssetsEventTrackings for Code = {code}");
                                }

                                if (rblTransferType.SelectedValue == "Employee")
                                {
                                    int empID = GetEmpId(ZeroIntergerIFNull(lstToEmpRefCode.SelectedValue));

                                    if (empID != 0)
                                    {
                                        var AssHeader = en.AssetsEventTrackingHeaders.Where(o => o.EmpRefCode == empID).ToList();
                                        if (AssHeader.Count > 0)
                                        {
                                            q.EmpRefCode = empID;
                                            q.EmpName = lstToEmpRefCode.SelectedItem.Text;
                                            q.RequestHeaderCode = AssHeader.FirstOrDefault().Code;
                                            q.LastModifiedAt = DateTime.Now;
                                            q.LastModifiedBy = ZeroIntergerIFNull(ReadSession("userId").ToString());
                                            q.Notes = txtNotes.Text;
                                            q.ActionDate = NullDateifEmptyNew(txtFromDate.Text);
                                            q.statusId = 6;
                                            en.SaveChanges();

                                            IsTransfered = true;
                                        }
                                        else
                                            IsTransfered = false;
                                    }
                                }
                                else if (rblTransferType.SelectedValue == "Store")
                                {
                                    if (string.IsNullOrEmpty(lstToStore.SelectedValue) || lstToStore.SelectedValue == "0")
                                    {
                                        lblerror.Text = "يرجى اختيار المخزن .";
                                        return;
                                    }
                                    q.IsDeleted = true;

                                    var AssStore = new AssetsStore();
                                    AssStore.AssetId = code;
                                    AssStore.StoreId = ZeroIntergerIFNull(lstToStore.SelectedValue);
                                    AssStore.CreatedBy = ReadSession("userId").ToString();
                                    AssStore.CreatedAt = DateTime.Now;

                                    en.AssetsStores.Add(AssStore);
                                    en.SaveChanges();

                                    IsTransfered = true;
                                }

                            }
                            catch (Exception ex)
                            {
                                throw new Exception("An error occurred while updating the records: " + ex.Message, ex);
                            }

                        }
                        checkItem = true;
                        //  objList.(objRepository.getItemDetails(ZeroIntergerIFNull(grdItems.Items[i].Cells[0].Text)));
                    }
                }
            }
            //if (checkItem)
            //{
            //    Session["selectedItems"] = objList;
            //    filterItem();
            //    filterToItem();
            //    string scriptAdd = "Swal.fire('تم التحويل بنجاح ');";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertVendor", scriptAdd, true);
            //    //FillSelectedItems();
            //}
            //else
            //{
            //    string script22 = "Swal.fire('يرجى اختيار المواد المراد تحويلها ');";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertVendor", script22, true);
            //}
            if (checkItem && IsTransfered == true)
            {
                Session["selectedItems"] = objList;
                filterItem();
                filterToItem();
                filterStoreItem();

                Logger.Log(
                userId: ReadSession("userId").ToString(),
                userName: ReadSession("AdminName").ToString(),
                tableName: "AssetsEventTrackings",
                action: "Transfer",
                recordId: "From : " + lstRefEmployee.SelectedItem.Text + "    " + "To : " + lstToEmpRefCode.SelectedItem.Text
                );

                string scriptAdd = "Swal.fire('تم التحويل بنجاح ');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertVendor", scriptAdd, true);
                //FillSelectedItems();
            }
            else if (checkItem && IsTransfered == false)
            {
                string scriptAdd22 = "Swal.fire('عفوا , يجب ادخال عهدة للموظف حتى يتم التحويل اليه');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertVendor", scriptAdd22, true);
            }
            else
            {
                string script22 = "Swal.fire('يرجى اختيار المواد المراد تحويلها ');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertVendor", script22, true);
            }
        }
        public int GetEmpId(int id)
        {
            using (AssetsEntitiesNew en = new AssetsEntitiesNew())
            {
                var query = en.Employee_tbl.Where(o => o.Ora_EmpRefCode == id).ToList();
                if(query.Count>0)
                {
                    return query.FirstOrDefault().Emp_Id;
                }
                return 0;   
            }
        }

        protected void lstRefEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterItem();
        }

        protected void lstToEmpRefCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterToItem();
        }

        protected void rblTransferType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleTransferType();
        }

        private void ToggleTransferType()
        {
            if (rblTransferType.SelectedValue == "Employee")
            {
                divEmployeeTarget.Style["display"] = "block";
                divStore.Style["display"] = "none";
                grdSelectedItems.DataSource = null;
                grdSelectedItems.DataBind();
                lblSelectedCount.Text = string.Empty;
                lstToStore.SelectedValue = "0";
            }
            else if (rblTransferType.SelectedValue == "Store")
            {
                divEmployeeTarget.Style["display"] = "none";
                divStore.Style["display"] = "block";
                grdSelectedItems.DataSource = null;
                grdSelectedItems.DataBind();
                lblSelectedCount.Text = string.Empty;
                lstToEmpRefCode.SelectedValue = "0";
            }
        }
        protected void lstToStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterStoreItem();
        }
        private void filterStoreItem()
        {
            using (AssetsEntitiesNew en = new AssetsEntitiesNew())
            {
                int storeId = ZeroIntergerIFNull(lstToStore.SelectedValue);
                if (storeId == 0) return;

                // جلب قائمة AssetIds من المخزن

                var storeAssets = en.AssetsStores
                                .Where(a => a.StoreId == storeId)
                                .Select(a => a.AssetId)
                                .ToList();

                // جلب الـ CustodyList فقط للعناصر الموجودة في القائمة
                var filteredList = objRepository.getCustodyListByAssets(storeAssets);

                // ربط النتائج بالـ Grid
                grdSelectedItems.DataSource = filteredList;
                grdSelectedItems.DataBind();

                lblSelectedCount.Text = Resources.Utilities.foundTotal + filteredList.Count.ToString() + Resources.Utilities.records;
            }
        }
    }
}

