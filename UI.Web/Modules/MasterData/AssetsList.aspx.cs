using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using QRCoder;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.MasterData
{
    public partial class AssetsList : BaseFormAdmin
    {
        #region "Page Members"
        public AssetsRepository objRepository = IoC.Resolve<AssetsRepository>();
        public string _PageTitle = Resources.Pages.CustodyItems;

        #endregion

        #region "Page Events"
        protected void Page_PreInit(object sender, EventArgs e)
        {
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

             
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

        

 
        protected void grdItems_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#e4fbf5\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#FFFFFF\';");
            }

            if ((e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#e4fbf5\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#EFEFEF\';");
            }

        }
 
         protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.FillGrid();
        }
       
        #endregion

        #region "Fill Information"
        private void FillGrid()
        {
            var objList = objRepository.getAssetsList(txtFilterCode.Text,NullDateifEmptyNew( txtTransDate.Text), 
                NullDateifEmptyNew( txtTransactionDateTo.Text), ZeroIntergerIFNull( lstFilterVendor.SelectedValue),
               ZeroIntergerIFNull( lstFilterSatus.SelectedValue), ZeroIntergerIFNull(lstFilterAction.SelectedValue),
               ZeroIntergerIFNull(lstFilterSatus.SelectedValue), ZeroIntergerIFNull( lstFilterItem.SelectedValue),  ZeroIntergerIFNull(lstfilterEmployee.SelectedValue), ZeroIntergerIFNull(lstFilterLocation.SelectedValue)) ;
            lblcount.Text = (Resources.Utilities.foundTotal + (objList.Count.ToString()).ToString() + Resources.Utilities.records);

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
        private void fillLookups()
        {

            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillItemCategory(), lstFilterCategory, Resources.Pages.TitleFiled, "Code" ,Resources.Pages.all);

            FillDllwithoptional_ALL(LooksUpsRepository.ins.fillCategoryItems(ZeroIntergerIFNull( lstFilterCategory.SelectedValue)), lstFilterItem, "ItemNameAr", "Code", Resources.Pages.all);


            FillDllwithoptional_ALL(LooksUpsRepository.ins.Fillvendor(), lstFilterVendor, "VendorNameAr", "Code", Resources.Pages.all);
            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillAssetsTrackingActions(), lstFilterAction, Resources.Pages.TitleFiled, "Code", Resources.Pages.all);
            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillTrackingStatus(), lstFilterSatus, Resources.Pages.TitleFiled, "Code", Resources.Pages.all);

            FillDllwithoptional(LooksUpsRepository.ins.FillLocation(), lstFilterLocation, "LocationNameAr", "Code");

            //if (Session["OraEmpList"] != null)
            //{
            //    FillDllwithoptional((List<EmployeeViewModel>)Session["OraEmpList"], lstfilterEmployee, "EMP_NAME", "EMP_ID");
            //}
            //else
            //{
            //    var Emplist = GetOraEmpList(1);
            //    Session["OraEmpList"] = Emplist;
            //    FillDllwithoptional(Emplist, lstfilterEmployee, "EMP_NAME", "EMP_ID");
            //}

            FillDllwithoptional(LooksUpsRepository.ins.FillEmployee(), lstfilterEmployee, "EmpName", "Code");


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
         
      
        public string getProfileQRCOde(string ItemQrCode)
        {
            string code = "/ItemCardCode.aspx?Qrcode=" + ItemQrCode;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);

            System.IO.MemoryStream ms = new MemoryStream();
            QrBitmap.Save(ms, ImageFormat.Jpeg);

            byte[] byteImage = ms.ToArray();
            var BitmapArray = Convert.ToBase64String(byteImage); // Get Base64

            return string.Format("data:image/png;base64,{0}", BitmapArray);
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
            FillGrid();
        }
        #endregion

        protected void lnkQuick_Click(object sender, EventArgs e)
        {
            this.FillGrid();
        }

        protected void lstFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDllwithoptional_ALL(LooksUpsRepository.ins.fillCategoryItems(ZeroIntergerIFNull(lstFilterCategory.SelectedValue)), lstFilterItem, "ItemNameAr", "Code", Resources.Pages.all);


        }
    }
}