using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.MasterData
{
    public partial class LocationsList : BaseFormAdmin
    {
        #region "Page Members"
        public LocationsRepository objRepository = IoC.Resolve<LocationsRepository>();
        public string _PageTitle = Resources.Pages.Locations;

        #endregion

        #region "Page Events"


        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["add"] != null)
            {
                this.MasterPageFile = "~/Modules/_shared/MainEmpty.Master";
            }
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            PageUrl = "Locations.aspx";

           

        }

            protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
            btnSave.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {
                //if ((Request.UrlReferrer == null))
                //{
                //    Response.Redirect("/admin/pages/main.aspx");
                //}
                fillLookups();
                ViewState["itemID"] = "0";
                //  FillDll(LooksUpsRepository.ins.FillQuantityCode(), lstQunit, Resources.Pages.TitleFiled, "Code");

                if (Request.QueryString["pid"] !=null)
                {
                    hdnSelectedNode.Value = Request.QueryString["pid"].ToString();
                    LstLocationParent.SelectedValue = Request.QueryString["pid"].ToString();

                }
                FillGrid();
            }

        }
      
        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {

                //D_Locations obj = new D_Locations();
                //for (int i = 0; i <= grdData.Items.Count - 1; i++)
                //{

                //    if ((grdData.Items[i].FindControl("chkItem") != null))
                //    {
                //        CheckBox check = (CheckBox)grdData.Items[i].FindControl("chkItem");

                //        if (check.Checked)
                //        {
                //            objRepository.Delete((D_Locations)objRepository.GetDetails(ZeroIntergerIFNull(grdData.Items[i].Cells[0].Text)));
                //        }
                //    }
                //}

                if (hdnSelectedNode.Value != "")
                {
                    //check Relative Exta
                    if (objRepository.CheckChildExistance(ZeroIntergerIFNull(hdnSelectedNode.Value)))
                    {

                        string script = FormatpopupErrorMSG("عفوا ، يوجد بيانات مرتبطة ", "1");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                        return;
                    }
                    {
                        objRepository.Delete((D_Locations)objRepository.GetDetails(ZeroIntergerIFNull(hdnSelectedNode.Value)));
                        string script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully , "3");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                    }


                }
                FillGrid();
                fillLookups();

            }
            catch (Exception ex)
            {

                string script = FormatpopupErrorMSG(Resources.Alerts.SorryDeleteDataFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }

        }

        protected void grdData_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string id = e.Item.Cells[0].Text;
            this.ClearForm();
            ViewState["itemID"] = id;
            this.FillForm();
            tblshow.Visible = false;
            tblAdd.Visible = true;
        }

        protected void grdData_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#DA9CF1\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#FFFFFF\';");
            }

            if ((e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#DA9CF1\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#EFEFEF\';");
            }

        }
        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            string script = "";
            try
            {
                D_Locations obj = new D_Locations();
                if (ZeroIntergerIFNull(hdnSelectedEditNode.Value)==0)
                {//Save


                    obj.LocationNameEn = txttitleEn.Text;
                    obj.LocationNameAr = txttitleAr.Text;
                    obj.LocationRefCode = txtFinRefCode.Text;
                    obj.LocationParentId = ZeroIntergerIFNull(LstLocationParent.SelectedValue);
                    obj.LocationType = ZeroIntergerIFNull(lstLocationType.SelectedValue);
                    obj.OrgChartRefCode = ZeroIntergerIFNull(gets(Request.QueryString["entityId"]));

                    objRepository.Add(obj);
                }
                else
                { //Update 
                    obj = objRepository.GetDetails(ZeroIntergerIFNull(hdnSelectedEditNode.Value.ToString()));


                    obj.LocationNameEn = txttitleEn.Text;
                    obj.LocationNameAr = txttitleAr.Text;
                    obj.LocationRefCode = txtFinRefCode.Text;
                    obj.LocationType = ZeroIntergerIFNull(lstLocationType.SelectedValue);
                    obj.LocationParentId = ZeroIntergerIFNull(LstLocationParent.SelectedValue);
                    if (ZeroIntergerIFNull(gets(Request.QueryString["entityId"]))!=0)
                    {
                        obj.OrgChartRefCode = ZeroIntergerIFNull(gets(Request.QueryString["entityId"]));
                    }
                    
                    objRepository.Update(obj);

                }

                ClearForm();
                FillGrid();
                fillLookups();
                script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }
            catch (Exception ex)
            {


                script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }

        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.ClearForm();
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.FillGrid();
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {

            this.ClearForm();
            tblAdd.Visible = true;
            tblshow.Visible = false;
        }
        #endregion

        #region "Fill Information"

        private void fillLookups()
        {

            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillLocations(), LstLocationParent, "LocationNameAr", "Code","رئيسى");
            FillDllwithoptional(LooksUpsRepository.ins.FillLocationsTypes(), lstLocationType, Resources.Pages.TitleFiled, "Code");

        }

        private void FillGrid()
        {
            int Pid = ZeroIntergerIFNull(Request.QueryString["pid"] == null ? "0" : Request.QueryString["pid"].ToString());
            var objList = objRepository.GetList(Pid,"");
            lblcount.Text = (Resources.Utilities.foundTotal + (objList.Count.ToString()).ToString() + Resources.Utilities.records);
            decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdData.PageSize));
            if ((c <= grdData.CurrentPageIndex))
            {
                grdData.CurrentPageIndex = 0;
            }

            grdData.DataSource = objList;
            grdData.DataBind();
            int _totalCount = objList.Count;
             

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
            var objList = objRepository.GetDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            if ((objList != null))
            {
                txttitleEn.Text = gets(objList.LocationNameEn);
                txttitleAr.Text = gets(objList.LocationNameAr);
                txtFinRefCode.Text = gets(objList.LocationRefCode);

                lstLocationType.SelectedValue= gets(objList.LocationType);

            }

            tblAdd.Visible = true;
            lblSubTitle.Text = this.GetTitle(false);

        }
        private void ClearForm()
        {
            txttitleEn.Text = "";
            txttitleAr.Text = "";
            txtFinRefCode.Text = "";
            hdnSelectedEditNode.Value = "";

            ViewState["itemID"] = 0;
            tblAdd.Visible = false;
            tblshow.Visible = true;
            lblSubTitle.Text = this.GetTitle(true);
        }

        #endregion


    }
}