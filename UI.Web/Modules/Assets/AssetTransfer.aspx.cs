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
                FillSelectedItems();
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

                    var selectedItem = (AssetsItemUnits)objRepository.getItemDetailsForEdit(ZeroIntergerIFNull(grdSelectedItems.Items[i].Cells[0].Text));
                    obj.AssetCode = selectedItem.Code;
                    obj.ActionDate = NullDateifEmpty(txtFromDate.Text);


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
                FillSelectedItems();
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

            var objList = objRepository.getAssetsWithLastAction(2, ZeroIntergerIFNull(selectedLocation.Value), ZeroIntergerIFNull(lstRefEmployee.SelectedValue));
            lblcount.Text = (Resources.Utilities.foundTotal + (objList.Count.ToString()).ToString() + Resources.Utilities.records);

            if (Session["selectedItems"] != null)
            {
                var excluded = (List<view_AssetsList>)Session["selectedItems"];

                objList = (from obj in objList
                                    where !(from objexec in excluded select objexec.InboubdItemId).ToList().Contains(obj.InboubdItemId)
                                    select obj).ToList();


            }

            decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdItems.PageSize));
            if ((c <= grdItems.CurrentPageIndex))
            {
                grdItems.CurrentPageIndex = 0;
            }



            grdItems.DataSource = objList;
            grdItems.DataBind();
            int _totalCount = objList.Count;
            pager1.ItemCount = objList.Count;


        }

        private void FillSelectedItems()
        {

            if (Session["selectedItems"] != null)
            {
                var objList = (List<view_AssetsList>)Session["selectedItems"];
                decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdSelectedItems.PageSize));
                if ((c <= grdSelectedItems.CurrentPageIndex))
                {
                    grdSelectedItems.CurrentPageIndex = 0;
                }
                grdSelectedItems.Visible = true;
                grdSelectedItems.DataSource = objList;
                grdSelectedItems.DataBind();
                int _totalCount = objList.Count;
                pager2.ItemCount = objList.Count;

                hdnItemCount.Value = objList.Count.ToString();


            }
            else
            {
                grdSelectedItems.Visible = false;
                lblSelectedCount.Visible = false;
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
            filterItem();
        }

 

        protected void btnReload_Click(object sender, EventArgs e)
        {
            filterItem();
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

            FillDllwithoptional(LooksUpsRepository.ins.FillEmployee(), lstRefEmployee, "EmpName", "Code");

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

            pager2.CurrentIndex = currnetPageIndx;
            grdSelectedItems.CurrentPageIndex = (currnetPageIndx - 1);
 
            FillSelectedItems();
        }

        protected void lnkRemove_Click(object sender, EventArgs e)
        {
            List<view_AssetsList> objList = new List<view_AssetsList>();
            if (Session["selectedItems"] != null)
            {
                objList = (List<view_AssetsList>)Session["selectedItems"];

            }

            for (int i = 0; i <= grdSelectedItems.Items.Count - 1; i++)
            {
                if ((grdSelectedItems.Items[i].FindControl("chkItem") != null))
                {
                    CheckBox check = (CheckBox)grdSelectedItems.Items[i].FindControl("chkItem");
                    if (check.Checked)
                    {
                        objList.Remove(objList.Where(x => x.InboubdItemId == ZeroIntergerIFNull(grdSelectedItems.Items[i].Cells[0].Text)).FirstOrDefault());
                    }
                }
            }
            Session["selectedItems"] = objList;
            filterItem();
            FillSelectedItems();

        }

        protected void lnkAddItem_Click(object sender, EventArgs e)
        {
            List<view_AssetsList> objList = new List<view_AssetsList>();
            if (Session["selectedItems"] != null)
            {
                objList = (List<view_AssetsList>)Session["selectedItems"];

            }

            for (int i = 0; i <= grdItems.Items.Count - 1; i++)
            {
                if ((grdItems.Items[i].FindControl("chkItem") != null))
                {
                    CheckBox check = (CheckBox)grdItems.Items[i].FindControl("chkItem");

                    if (check.Checked)
                    {
                        objList.Add(objRepository.getItemDetails(ZeroIntergerIFNull(grdItems.Items[i].Cells[0].Text)));
                    }
                }
            }
            Session["selectedItems"] = objList;
            filterItem();
            FillSelectedItems();



        }
    }
}

