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
    public partial class EmployeeList : BaseFormAdmin
    {
        #region "Page Members"
        public EmployeeRepository objRepository = IoC.Resolve<EmployeeRepository>();
        public string _PageTitle = Resources.Pages.EmployeeList;

        #endregion

        #region "Page Events"
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["add"] != null)
            {
                this.MasterPageFile = "~/Modules/_shared/MainEmpty.Master";
            }
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

                ViewState["itemID"] = "0";
                fillLookups();


                FillGrid();
                if (Request.QueryString["add"] !=null)
                {
                    this.ClearForm();
                    tblAdd.Visible = true;
                    tblshow.Visible = false;
                }
            }

        }

        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {

                D_EmployeeList obj = new D_EmployeeList();
                for (int i = 0; i <= grdEmployeeList.Items.Count - 1; i++)
                {

                    if ((grdEmployeeList.Items[i].FindControl("chkItem") != null))
                    {
                        CheckBox check = (CheckBox)grdEmployeeList.Items[i].FindControl("chkItem");

                        if (check.Checked)
                        {
                            objRepository.Delete((D_EmployeeList)objRepository.GetDetails(ZeroIntergerIFNull(grdEmployeeList.Items[i].Cells[0].Text)));
                        }
                    }
                }
                FillGrid();

            }
            catch (Exception ex)
            {


                string script = FormatpopupErrorMSG(Resources.Alerts.SorryDeleteDataFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }

        }

        protected void grdEmployeeList_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string id = e.Item.Cells[0].Text;
            this.ClearForm();
            ViewState["itemID"] = id;
            this.FillForm();
            tblshow.Visible = false;
            tblAdd.Visible = true;
        }

        protected void grdEmployeeList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#d4efe7\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#f5f6fa\';");
            }

            if ((e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#d4efe7\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#ffffff\';");
            }

        }
        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            string script = "";
            try
            {
                D_EmployeeList obj = new D_EmployeeList();


                if (gets(ViewState["itemID"]).Equals("0"))
                {//Save


                    obj.EmpName = txtEmployeeNameAr.Text;
                    obj.EmpCode = ZeroIntergerIFNull(txtRefCode.Text);
                    obj.Phone = txtPhone.Text;
                    obj.Mobile = txtMobile.Text;
                    obj.CivilId =  (txtCivilId.Text);
                    obj.JobTitleId = ZeroIntergerIFNull(lstJobTitle.SelectedValue);
                   // obj.OrgRefId = ZeroIntergerIFNull(lstEntityCode.SelectedValue);
                    obj.LocationRefCode = ZeroIntergerIFNull(selectedLocation.Value);

                    objRepository.Add(obj);
                }
                else
                { //Update 
                    obj = objRepository.GetDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));


                    obj.EmpName = txtEmployeeNameAr.Text;
                    obj.EmpCode = ZeroIntergerIFNull(txtRefCode.Text);
                    obj.CivilId =  (txtCivilId.Text);

                    obj.Phone = txtPhone.Text;
                    obj.Mobile = txtMobile.Text;
                    obj.JobTitleId = ZeroIntergerIFNull(lstJobTitle.SelectedValue);
                   // obj.OrgRefId = ZeroIntergerIFNull(lstEntityCode.SelectedValue);
                    obj.LocationRefCode = ZeroIntergerIFNull(selectedLocation.Value);


                    objRepository.Update(obj);

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
        private void FillGrid()
        {
            var objList = objRepository.GetList(ZeroIntergerIFNull(lstfilterEntityCode.SelectedValue), ZeroIntergerIFNull(lstFilterjobTitle.SelectedValue),txtPartOfName.Text, ZeroIntergerIFNull(txtFilterCode.Text));
            lblcount.Text = (Resources.Utilities.foundTotal + (objList.Count.ToString()).ToString() + Resources.Utilities.records);
            decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdEmployeeList.PageSize));
            if ((c <= grdEmployeeList.CurrentPageIndex))
            {
                grdEmployeeList.CurrentPageIndex = 0;
            }

            grdEmployeeList.DataSource = objList;
            grdEmployeeList.DataBind();
            int _totalCount = objList.Count;
            pager1.ItemCount = objList.Count;

        }
        private void fillLookups()
        {

            //FillDllwithoptional(LooksUpsRepository.ins.FillEntityList(), lstEntityCode, "EntityNameAr", "Code");
            //FillDllwithoptional_ALL(LooksUpsRepository.ins.FillEntityList(), lstfilterEntityCode, "EntityNameAr", "Code", "الكل");

            if (Request.QueryString["RefEntityCode"] != null)
            {
                lstEntityCode.SelectedValue = Request.QueryString["RefEntityCode"].ToString();
                lstfilterEntityCode.SelectedValue = Request.QueryString["RefEntityCode"].ToString();

            }
            FillDllwithoptional(LooksUpsRepository.ins.FIllJobTtile(), lstJobTitle, Resources.Pages.TitleFiled, "Code");
            FillDllwithoptional(LooksUpsRepository.ins.FIllJobTtile(), lstFilterjobTitle, Resources.Pages.TitleFiled, "Code");


        }
        private string GetTitle(bool isadd)
        {
            if (isadd)
            {
                return Resources.Pages.addnewrecord1;
            }
            else
            {
                return "Edit Record Information";
            }

        }
        private void FillForm()
        {
            var objList = objRepository.GetDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            if ((objList != null))
            {


                txtEmployeeNameAr.Text = gets(objList.EmpName);
                txtRefCode.Text = gets(objList.EmpCode);
                txtCivilId.Text = gets(objList.CivilId);
                txtPhone.Text = gets(objList.Phone);
                txtMobile.Text = gets(objList.Mobile);

             //   lstEntityCode.SelectedValue = gets(objList.OrgRefId);
                lstJobTitle.SelectedValue = gets(objList.JobTitleId);
                selectedLocation.Value = gets(objList.LocationRefCode);

            }

            tblAdd.Visible = true;
            lblSubTitle.Text = this.GetTitle(false);

        }
        private void ClearForm()
        {
            txtEmployeeNameAr.Text = "";
            txtRefCode.Text = "";
            txtPhone.Text = "";
            txtMobile.Text = "";
            txtCivilId.Text = "";

            ViewState["itemID"] = 0;
            tblAdd.Visible = false;
            tblshow.Visible = true;
            lblSubTitle.Text = this.GetTitle(true);
        }


        #endregion

        protected void lnkQuick_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void pager_Command(object sender, CommandEventArgs e)
        {
            Int32 currnetPageIndx = ((Int32)(e.CommandArgument));
            if ((currnetPageIndx <= 0))
            {
                currnetPageIndx = 1;
            }

            if ((currnetPageIndx > grdEmployeeList.PageCount))
            {
                currnetPageIndx = (grdEmployeeList.PageCount - 1);
            }

            pager1.CurrentIndex = currnetPageIndx;
            grdEmployeeList.CurrentPageIndex = (currnetPageIndx - 1);
            FillGrid();
        }



    }
}