using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using QRCoder;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.MasterData
{
    public partial class AssetsRequestList : BaseFormAdmin
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
                //txtTransDate.Text = DateTime.Now.AddYears(-1).ToString();
                //txtTransactionDateTo.Text = DateTime.Now.ToString();
                ViewState["itemID"] = "0";
                fillLookups();
                FillGrid();
            }

        }




        protected void grdData_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {

            if ((e.Item.ItemType == ListItemType.Item))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#d5c08e6e\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#FFFFFF\';");
            }

            if ((e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#d5c08e6e\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#FFFFFF\';");
            }


            //if ((e.Item.ItemType == ListItemType.Item))
            //{
            //    //
            //    HtmlImage im = ((HtmlImage)(e.Item.Cells[2].FindControl("imgControl")));
            //    string imname = im.ClientID;
            //    string rowindex = (e.Item.ItemIndex + 1).ToString();
            //    string rowID = e.Item.ClientID;
            //    im.Attributes.Add("onclick", ("ControlGrid(\'" + (imname + ("\'," + (rowindex + (",\'" + (rowID + "\')")))))));
            //    //LinkButton lnk = ((LinkButton)(e.Item.Cells[0].Controls[0]));
            //    //lnk.Attributes.Add("onclick", "return confirm(\'Are you sure you want to delete this Invoice?\');");




            //}
            //else if ((e.Item.ItemType == ListItemType.AlternatingItem))
            //{
            //    string rowID = e.Item.ClientID;
            //    string Filecode = e.Item.Cells[3].Text;

            //    var objUnitList = objRepository.getRequestAssets(ZeroIntergerIFNull(Filecode));
            //    if (objUnitList != null)
            //    {
            //        DataGrid grd = ((DataGrid)(e.Item.Cells[1].FindControl("grdItems")));
            //        grd.DataSource = objUnitList;
            //        grd.DataBind();


            //    }


            //    for (int i = 2; i <= (e.Item.Cells.Count - 1); i++)
            //    {
            //        e.Item.Cells[i].Visible = false;
            //    }

            //    e.Item.Cells[0].Controls[0].Visible = false;
            //    e.Item.Cells[1].Attributes.Add("colspan", ((e.Item.Cells.Count - 2)).ToString());
            //    e.Item.Attributes.Add("style", "display:none");
            //    e.Item.Cells[0].Visible = false;
            //}

            if (!(e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Cells[1].Visible = false;


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
            var objlist = objRepository.getAssetsRequestList(txtPartOfName.Text,ZeroIntergerIFNull(lstFilterAction.SelectedValue), 
                NullDateifEmpty(txtTransDate.Text), NullDateifEmpty(txtTransactionDateTo.Text),0,0,0,ZeroIntergerIFNull(lstFilterEmpStatus.SelectedValue));
            //var duplicatedList = objlist.SelectMany(t =>
            //     Enumerable.Repeat(t, 2)).ToList();

            lblcount.Text = (Resources.Utilities.foundTotal + " (" + (objlist.Count.ToString()).ToString() + ") " + Resources.Utilities.records);
            lblCountTop.Text = objlist.Count.ToString();
            decimal c = System.Math.Ceiling(Convert.ToDecimal(objlist.Count / grdAssets.PageSize));
            if ((c <= grdAssets.CurrentPageIndex))
            {
                grdAssets.CurrentPageIndex = 0;
            }

            grdAssets.DataSource = objlist; //;
            grdAssets.DataBind();

            //int _totalCount = objlist.Count;
            pager1.ItemCount = objlist.Count;



        }
        private void fillLookups()
        {

            //FillDllwithoptional_ALL(LooksUpsRepository.ins.FillItemCategory(), lstFilterCategory, Resources.Pages.TitleFiled, "Code", Resources.Pages.all);

            //FillDllwithoptional_ALL(LooksUpsRepository.ins.fillCategoryItems(ZeroIntergerIFNull(lstFilterCategory.SelectedValue)), lstFilterItem, "ItemNameAr", "Code", Resources.Pages.all);


            //FillDllwithoptional_ALL(LooksUpsRepository.ins.Fillvendor(), lstFilterVendor, "VendorNameAr", "Code", Resources.Pages.all);
            //FillDllwithoptional_ALL(LooksUpsRepository.ins.FillAssetsTrackingActions(), lstFilterAction, Resources.Pages.TitleFiled, "Code", Resources.Pages.all);
            //FillDllwithoptional_ALL(LooksUpsRepository.ins.FillTrackingStatus(), lstFilterSatus, Resources.Pages.TitleFiled, "Code", Resources.Pages.all);

            //FillDllwithoptional(LooksUpsRepository.ins.FillLocation(), lstFilterLocation, "LocationNameAr", "Code");

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

            if ((currnetPageIndx > grdAssets.PageCount))
            {
                currnetPageIndx = (grdAssets.PageCount - 1);
            }

            pager1.CurrentIndex = currnetPageIndx;
            grdAssets.CurrentPageIndex = (currnetPageIndx - 1);
            FillGrid();
        }
        #endregion

        protected void lnkQuick_Click(object sender, EventArgs e)
        {
            this.FillGrid();
        }

       
    }
}