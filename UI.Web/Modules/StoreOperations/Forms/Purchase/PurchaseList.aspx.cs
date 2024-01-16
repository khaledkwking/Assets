using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.Collections;
using UI.Web.Admin.Controller;
 using Infrastructure.DAL;
using Microsoft.VisualBasic;
using UI.Web.Controler;
using DomainInterface;
using Infrastructure;
using Infrastructure.DAL.Model.DB;

namespace UI.Web.Modules.WHM.Forms
{
 
    partial class PurchaseList : BaseFormAdmin
    {

        public PurchaseRepository objRepository = IoC.Resolve<PurchaseRepository>();

        //private string _TargetTableName = "";
        //public string _PageTitle = "";
        //public string TargetTableName { get; set; }


        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    string Script = "";
        //    if (Request.QueryString["tableName"]!=null)
        //    {
        //        if (Request.QueryString["tableName"] != "")
        //        {
        //            TargetTableName = Request.QueryString["tableName"].ToString();

        //            //  _PageTitle = Resources.Utilities.TargetTableName;

        //            _PageTitle = "System Lookups";
        //        }
        //        else
        //        {
        //            Script = FormatpopupErrorMSG("Fail To Load Table Data, TableName Paramter", "1");
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", Script, true);
        //            lblerror.ForeColor = System.Drawing.Color.Red;
        //            return;
        //        }

        //    }
        //    else
        //    {
        //        Script = FormatpopupErrorMSG("Fail To Load Table Data", "1");
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", Script, true);
        //        lblerror.ForeColor = System.Drawing.Color.Red;
        //        return;
        //    }

        //}

        #region "From Events"
        //protected void btnSave_Click(object sender, System.EventArgs e)
        //{
        //    string Script = "";
        //    try
        //    {

        //        if (ViewState["Item"].ToString().Equals("0"))
        //        {

        //            clsLookup.ins.Insert(TargetTableName, txttitleEn.Text, txttitleAr.Text);
        //        }
        //        else
        //        {
        //            clsLookup.ins.Update(TargetTableName, ViewState["Item"].ToString(), txttitleEn.Text, txttitleAr.Text);
        //        }

        //        Script = FormatpopupErrorMSG("Data Saved Successfully", "3");
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", Script, true);
        //        this.ClearForm();
        //        this.FillGrid();
        //    }
        //    catch (Exception ex)
        //    {
        //        Script = FormatpopupErrorMSG("Fail To Save Data", "1");
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", Script, true);
        //        lblerror.ForeColor = System.Drawing.Color.Red;
        //        lblerror.Text = ("Error :" + ex.Message);
        //    }

        //}

        //protected void btnCancel_Click(object sender, System.EventArgs e)
        //{
        //    this.ClearForm();
        //}
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.FillGrid();
        }


        protected void btnNew_Click1(object sender, EventArgs e)
        {
            Response.Redirect("/"+Resources.Utilities.cutureRoute + "/Modules/WHM/Forms/Purchase/PurchaseOrderOperations.aspx");
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            //btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
            //btnSave.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {
                //if ((Request.UrlReferrer == null))
                //{
                //    Response.Redirect("/admin/pages/main.aspx");
                //}
                fillLookups();
                ViewState["Item"] = 0;
                FillGrid();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "InboundList.aspx";
        }
        
        protected void pager_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            Int32 currnetPageIndx = ((Int32)(e.CommandArgument));
            if ((currnetPageIndx <= 0))
            {
                currnetPageIndx = 1;
            }

            if ((currnetPageIndx > grdData.PageCount))
            {
                currnetPageIndx = (grdData.PageCount - 1);
            }

            pager1.CurrentIndex = currnetPageIndx;
            grdData.CurrentPageIndex = (currnetPageIndx - 1);
            this.FillGrid();
        }

        protected void grdData_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            //string id = e.Item.Cells[0].Text;
            //clsLookup.ins.Delete(TargetTableName, id);
            //this.FillGrid();
        }

        

        protected void grdData_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            //  string id = e.Item.Cells[0].Text;
            //  this.ClearForm();
            //  ViewState["Item"] = id;
            ////  this.FillForm();
            //  tblshow.Visible = false;
            //  tblAdd.Visible = true;
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
        #endregion

        #region "Fill Infotmation"
        private void ClearForm()
        {


            ViewState["Item"] = 0;

        }
        private void fillLookups()
        {
            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillInboundTypes(), lstInboundType, Resources.Pages.TitleFiled, "Code","All");
            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillDepositeTypes(), lstDepositeType, Resources.Pages.TitleFiled, "Code", "All");
            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillCustomsDepartments(), lstcustomsDepartment, Resources.Pages.TitleFiled, "Code", "All");


            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillReferenceTypes(), lstReferanceType, Resources.Pages.TitleFiled, "Code", "All");

            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillDepositeDeclaration(), lstDepositeDeclarationType, Resources.Pages.TitleFiled, "Code", "All");
           

        }
        private void FillGrid()
        {
            var objlist = objRepository.GetList(ReverseSerial(txtFilterSerial.Text), NullDateifEmpty(txtTransDate.Text), NullDateifEmpty(txtTransactionDateTo.Text), ZeroIntergerIFNull(lstInboundType.SelectedValue),
                ZeroIntergerIFNull(lstDepositeType.SelectedValue), ZeroIntergerIFNull(lstcustomsDepartment.SelectedValue), ZeroIntergerIFNull(lstReferanceType.SelectedValue),
                txtRefNo.Text, txtManifestNo.Text, txtDeliveryOrder.Text, ZeroIntergerIFNull(lstDepositeDeclarationType.SelectedValue));

            Session["InboundListResult"] = objlist;

            lblcount.Text = (" Found total <b>" + (objlist.Count.ToString() + "</b> records"));
            decimal c = System.Math.Ceiling(Convert.ToDecimal(objlist.Count / grdData.PageSize));
            if ((c <= grdData.CurrentPageIndex))
            {
                grdData.CurrentPageIndex = 0;
            }

            grdData.DataSource = objlist;
            grdData.DataBind();
            int _totalCount = objlist.Count;
            pager1.ItemCount = _totalCount;
            pager1.Visible = true;


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
        #endregion

        //private void FillForm()
        //{
        //    DataSet ds = clsLookup.ins.GetDetails(TargetTableName, ViewState["Item"].ToString());
        //    if ((ds.Tables[0].Rows.Count > 0))
        //    {


        //        txttitleEn.Text = gets(ds.Tables[0].Rows[0][Resources.Pages.TitleFiled]);
        //        txttitleAr.Text = gets(ds.Tables[0].Rows[0]["TitleAr"]);

        //    }

        //    tblAdd.Visible = true;
        //    lblSubTitle.Text = this.GetTitle(false);

        //}

        //private string getImage(ref FileUpload txtFile)
        //{
        //    string imgname="";
        //    string temp;
        //    string ext;
        //    int inx;
        //    int i;
        //    string RandChar;
        //    string ValueString;
        //  //  Microsoft.VisualBasic.VBMath.Randomize();
        //    imgname = "";
        //    ValueString = "";
        //    if (!(txtFile.PostedFile == null))
        //    {
        //        if ((txtFile.PostedFile.FileName != ""))
        //        {
        //            imgname = txtFile.PostedFile.FileName;
        //            imgname = imgname.Substring((imgname.LastIndexOf("\\") + 1));
        //            inx = imgname.LastIndexOf(".");
        //            temp = imgname.Substring(0, inx);
        //            ext = imgname.Substring((inx + 1));
        //            for (i = 1; (i <= 16); i++)
        //            {
        //                //  RandChar = (string)(Microsoft.VisualBasic.Conversion.Int((26 * Microsoft.VisualBasic.VBMath.Rnd() + 65)).ToString());

        //                RandChar = (new Random().Next(i, 16)).ToString();
        //                ValueString += RandChar;
        //            }

        //            imgname = (ValueString + ("." + ext));
        //            txtFile.PostedFile.SaveAs(Server.MapPath(("/Layout/uploads/Adminprofile/" + imgname)));
        //        }

        //    }


        //    //////string imgname = "";
        //    //////int inx = 0;
        //    //////string temp = "";
        //    //////string ext = "";
        //    //////string RandChar = "";
        //    //////string ValueString = "";
        //    //////Microsoft.VisualBasic.VBMath.Randomize();
        //    //////imgname = "";
        //    //////imgname = txtImage.PostedFile.FileName;
        //    //////imgname = imgname.Substring((imgname.LastIndexOf("\\") + 1));
        //    //////inx = imgname.LastIndexOf('.');
        //    //////temp = imgname.Substring(0, inx);
        //    //////ext = imgname.Substring((inx + 1));
        //    //////for (int i = 1; (i <= 10); i++)
        //    //////{
        //    //////    RandChar = (string)(Microsoft.VisualBasic.Conversion.Int((26 * Microsoft.VisualBasic.VBMath.Rnd() + 65)).ToString());
        //    //////    ValueString += RandChar;
        //    //////}
        //    //////imgname = (temp + (ValueString + ("." + ext)));
        //    //////return imgname;
        //    return imgname;
        //}
         
    }
}