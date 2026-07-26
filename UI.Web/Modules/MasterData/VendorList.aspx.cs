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
    public partial class VendorList : BaseFormAdmin
    {
        #region "Page Members"
        public VendorRepository objRepository = IoC.Resolve<VendorRepository>();
        public string _PageTitle = Resources.Pages.CustomerList;

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

                ViewState["itemID"] = "0";
                fillLookups();
                FillGrid();
            }

        }
     
        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {

                D_VendorData obj = new D_VendorData();
                for (int i = 0; i <= grdVendorList.Items.Count - 1; i++)
                {

                    if ((grdVendorList.Items[i].FindControl("chkItem") != null))
                    {
                        CheckBox check = (CheckBox)grdVendorList.Items[i].FindControl("chkItem");

                        if (check.Checked)
                        {
                            objRepository.Delete((D_VendorData)objRepository.GetDetails(ZeroIntergerIFNull(grdVendorList.Items[i].Cells[0].Text)));

                            Logger.Log(
                              userId: ReadSession("userId").ToString(),
                              userName: ReadSession("AdminName").ToString(),
                              tableName: "D_VendorData",
                              action: "Delete",
                              recordId: grdVendorList.Items[i].Cells[0].Text
                              );
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

        protected void grdVendorList_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string id = e.Item.Cells[0].Text;
            this.ClearForm();
            ViewState["itemID"] = id;
            this.FillForm();
            tblshow.Visible = false;
            tblAdd.Visible = true;
        }

        protected void grdVendorList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
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
                D_VendorData obj = new D_VendorData();
               
                string _img = UploadFileoServer(txtImage, Server.MapPath("/Layout/uploads/VendorsData/"));

                if (gets(ViewState["itemID"]).Equals("0"))
                {//Save


                    obj.VendorNameEn = txtVendorNameEn.Text;
                    obj.VendorNameAr = txtVendorNameAr.Text;
                    obj.RefCode = txtRefCode.Text;
                    obj.Phone = txtPhone.Text;
                    obj.fax = txtfax.Text;
                    obj.Email = txtEmail.Text;

                    obj.Country = ZeroIntergerIFNull( lstCountry.SelectedValue);
                    obj.ContactPerson = txtContactPerson.Text;
                    obj.ContactpersonPhone = txtContactpersonPhone.Text;
                    obj.AddressDetails=txtAddressDetails.Text;
                    if (_img!="")
                    {
                        obj.logo = _img;
                    }

                   objRepository.Add(obj);

                    Logger.Log(
                             userId: ReadSession("userId").ToString(),
                             userName: ReadSession("AdminName").ToString(),
                             tableName: "D_VendorData",
                             action: "Insert",
                             recordId: obj.Code.ToString()
                             );
                }
                else
                { //Update 
                    obj = objRepository.GetDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));

                    obj.VendorNameEn = txtVendorNameEn.Text;
                    obj.VendorNameAr = txtVendorNameAr.Text;
                    obj.RefCode = txtRefCode.Text;
                    obj.Phone = txtPhone.Text;
                    obj.fax = txtfax.Text;
                    obj.Email = txtEmail.Text;

                    obj.Country = ZeroIntergerIFNull(lstCountry.SelectedValue);
                    obj.ContactPerson = txtContactPerson.Text;
                    obj.ContactpersonPhone = txtContactpersonPhone.Text;
                    obj.AddressDetails = txtAddressDetails.Text;
                    if (_img != "")
                    {
                        obj.logo = _img;
                    }

                    objRepository.Update(obj);

                    Logger.Log(
                          userId: ReadSession("userId").ToString(),
                          userName: ReadSession("AdminName").ToString(),
                          tableName: "D_VendorData",
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
        private void FillGrid()
        {
            var objList = objRepository.GetList(ZeroIntergerIFNull(Request.QueryString["t"]), txtPartOfName.Text,ZeroIntergerIFNull( lstFIlterCounty.SelectedValue));
            lblcount.Text = (Resources.Utilities.foundTotal + (objList.Count.ToString()).ToString() + Resources.Utilities.records);
            decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdVendorList.PageSize));
            if ((c <= grdVendorList.CurrentPageIndex))
            {
                grdVendorList.CurrentPageIndex = 0;
            }

            grdVendorList.DataSource = objList;
            grdVendorList.DataBind();
            int _totalCount = objList.Count;
            pager1.ItemCount = objList.Count;

        }
        private void fillLookups()
        {

            FillDllwithoptional(LooksUpsRepository.ins.FillCountries(), lstCountry, Resources.Pages.TitleFiled, "Code" );
            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillCountries(), lstFIlterCounty, Resources.Pages.TitleFiled, "Code",Resources.Pages.all);


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


                txtVendorNameEn.Text = gets(objList.VendorNameEn);
                txtVendorNameAr.Text = gets(objList.VendorNameAr);
                txtRefCode.Text = gets(objList.RefCode);
                txtPhone.Text = gets(objList.Phone);
                txtfax.Text = gets(objList.fax);
                txtEmail.Text = gets(objList.Email);
                txtContactPerson.Text = gets(objList.ContactPerson);
                txtContactpersonPhone.Text = gets(objList.ContactpersonPhone);
                txtAddressDetails.Text = gets(objList.AddressDetails);

                if (objList.logo !="")
                {
                    lblimage.Text = "<img width='50px' src='" + Resources.Utilities.resourcespath + "uploads/VendorsData/"+ gets(objList.logo) + "'>";

                }

            }

            tblAdd.Visible = true;
            lblSubTitle.Text = this.GetTitle(false);

        }
        private void ClearForm()
        {
            txtVendorNameEn.Text = "";
            txtVendorNameEn.Text = "";
            txtRefCode.Text = "";

            txtPhone.Text = "";
            txtfax.Text = "";
            txtEmail.Text = "";
            txtContactPerson.Text = "";
            txtContactpersonPhone.Text = "";
            txtAddressDetails.Text = "";

            ViewState["itemID"] = 0;
            tblAdd.Visible = false;
            tblshow.Visible = true;
            lblSubTitle.Text = this.GetTitle(true);
            lblimage.Text = "";
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

            if ((currnetPageIndx > grdVendorList.PageCount))
            {
                currnetPageIndx = (grdVendorList.PageCount - 1);
            }

            pager1.CurrentIndex = currnetPageIndx;
            grdVendorList.CurrentPageIndex = (currnetPageIndx - 1);
            FillGrid();
        }



    }
}