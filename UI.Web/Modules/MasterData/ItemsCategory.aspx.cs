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
using UI.Web.Helper;

namespace UI.Web.Modules.MasterData
{
    public partial class ItemsCategory : BaseFormAdmin
    {
        #region "Page Members"
        public GoodsCategoryRepository objRepository = IoC.Resolve<GoodsCategoryRepository>();
        public string _PageTitle = "تصنيف المواد";

        #endregion

        #region "Page Events"

        protected void Page_PreInit(object sender, EventArgs e)
        {
            

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

                if (Request.QueryString["pid"] != null)
                {
                    hdnSelectedNode.Value = Request.QueryString["pid"].ToString();
                    lstParentCategory.SelectedValue = Request.QueryString["pid"].ToString();

                }else if (Request.QueryString["id"] != null)
                {
                    hdnSelectedNode.Value = Request.QueryString["id"].ToString();
                    hdnSelectedEditNode.Value = Request.QueryString["id"].ToString();
                    //var objDetails = objRepository.GetDetails(ZeroIntergerIFNull(Request.QueryString["id"].ToString()));

                    //if (objDetails != null)
                    //{

                    //    ViewState["itemID"] = gets(objDetails.Code);

                    //    txttitleEn.Text = gets(objDetails.TitleEn);
                    //    txttitleAr.Text = gets(objDetails.TitleAr);
                    //    txtFinRefCode.Text = gets(objDetails.FinanceRefCode);
                    //    txtScrapPeriod.Text = gets(objDetails.ServicePeriod);
                    //    txtScrapAmount.Text = gets(objDetails.ScrapPrice);
                    //    lstParentCategory.SelectedValue = gets(objDetails.Cat_ParentId);

                    //}
                    //else {
                    //    string script = FormatErrorMSGSwal(Resources.Alerts.SorryFailToretriveData  , "1");
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                    //}
                    // Set Item Parent
                    //lstParentCategory.SelectedValue = Request.QueryString["id"].ToString();
                }

               // FillGrid();
            }

        }

        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {

                //D_ItemsCategory obj = new D_ItemsCategory();
                //for (int i = 0; i <= grdData.Items.Count - 1; i++)
                //{

                //    if ((grdData.Items[i].FindControl("chkItem") != null))
                //    {
                //        CheckBox check = (CheckBox)grdData.Items[i].FindControl("chkItem");

                //        if (check.Checked)
                //        {
                //            objRepository.Delete((D_ItemsCategory)objRepository.GetDetails(ZeroIntergerIFNull(grdData.Items[i].Cells[0].Text)));
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
                        objRepository.Delete((D_ItemsCategory)objRepository.GetDetails(ZeroIntergerIFNull(hdnSelectedNode.Value)));

                        Logger.Log(
                     userId: ReadSession("userId").ToString(),
                     userName: ReadSession("AdminName").ToString(),
                     tableName: "D_ItemsCategory",
                     action: "Delete",
                     recordId: hdnSelectedNode.Value
                     );
                        string script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
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
                D_ItemsCategory obj = new D_ItemsCategory();
                if (ZeroIntergerIFNull(hdnSelectedEditNode.Value) == 0)
                {//Save


                    obj.TitleEn = txttitleEn.Text;
                    obj.TitleAr = txttitleAr.Text;
                    obj.FinanceRefCode = txtFinRefCode.Text;
                    obj.Cat_ParentId = ZeroIntergerIFNull(lstParentCategory.SelectedValue);


                    obj.ScrapPrice = ZeroIFNull(txtScrapAmount.Text);
                    obj.ServicePeriod = ZeroIFNull(txtScrapPeriod.Text);

                    objRepository.Add(obj);

                    Logger.Log(
                 userId: ReadSession("userId").ToString(),
                 userName: ReadSession("AdminName").ToString(),
                 tableName: "D_ItemsCategory",
                 action: "Insert",
                 recordId: obj.Code.ToString()
                 );
                }
                else
                { //Update 
                    obj = objRepository.GetDetails(ZeroIntergerIFNull(hdnSelectedEditNode.Value.ToString()));

                    obj.TitleEn = txttitleEn.Text;
                    obj.TitleAr = txttitleAr.Text;
                    obj.FinanceRefCode = txtFinRefCode.Text;
                    obj.Cat_ParentId = ZeroIntergerIFNull(lstParentCategory.SelectedValue);

                    obj.ScrapPrice = ZeroIFNull(txtScrapAmount.Text);
                    obj.ServicePeriod = ZeroIFNull(txtScrapPeriod.Text);

                    objRepository.Update(obj);

                    Logger.Log(
              userId: ReadSession("userId").ToString(),
              userName: ReadSession("AdminName").ToString(),
              tableName: "D_ItemsCategory",
              action: "Update",
              recordId: obj.Code.ToString()
              );

                }

                ClearForm();
                FillGrid();

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

            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillItemCategory(), lstParentCategory, Resources.Pages.TitleFiled, "Code", "تصنيف رئيسى");


        }

        private void FillGrid()
        {
            int Pid = ZeroIntergerIFNull(Request.QueryString["pid"] == null ? "0" : Request.QueryString["pid"].ToString());
            var objList = objRepository.GetList(Pid, "");
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
                txttitleEn.Text = gets(objList.TitleEn);
                txttitleAr.Text = gets(objList.TitleAr);
                txtFinRefCode.Text = gets(objList.FinanceRefCode);


                txtScrapPeriod.Text = gets(objList.ServicePeriod);
                txtScrapAmount.Text = gets(objList.ScrapPrice);

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

            txtScrapPeriod.Text = "0";
            txtScrapAmount.Text = "0";


            ViewState["itemID"] = 0;
            tblAdd.Visible = false;
            tblshow.Visible = true;
            lblSubTitle.Text = this.GetTitle(true);
        }

        #endregion


    }
}