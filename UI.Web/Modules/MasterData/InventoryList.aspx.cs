using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using QRCoder;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.MasterData
{
    public partial class InventoryList : BaseFormAdmin
    {
        #region "Page Members"
        public LocationsRepository objRepository = IoC.Resolve<LocationsRepository>();
        public string _PageTitle = Resources.Pages.StoreLocations;
        #endregion

        #region "Page Events"
        protected void Page_PreInit(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["itemID"] = "0";
                fillLookups();
                FillGrid();
            }
        }

        protected void grdItems_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string id = e.Item.Cells[0].Text;
            ViewState["itemID"] = id;
            tblshow.Visible = false;
        }

        protected void grdItems_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item))
            {
                e.Item.Attributes.Add("onmouseover", "style.backgroundColor='#DA9CF1';");
                e.Item.Attributes.Add("onmouseout", "style.backgroundColor='#FFFFFF';");
            }

            if ((e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "style.backgroundColor='#DA9CF1';");
                e.Item.Attributes.Add("onmouseout", "style.backgroundColor='#EFEFEF';");
            }
        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            FillGrid();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            FillGrid();
        }
        #endregion

        #region "Fill Information"
        private void FillGrid()
        {
            try
            {
                // Get locations with parent path
                int ? LocationId = ZeroIntergerIFNull(lstFilterlocation.SelectedValue);
                var locationsWithPath = objRepository.GetLocationsByTypeWithParentPathString(4, LocationId);

                // Create a custom view model for binding
                var gridDataSource = locationsWithPath.Select(item => new
                {
                    Code = item.location.Code,
                    LocationNameAr = item.location.LocationNameAr ?? item.location.LocationNameEn,
                    ParentLocationNameAr = item.parentPath,
                    LocationNameEn = item.location.LocationNameEn,
                    LocationType = item.location.LocationType,
                    OrgChartRefCode = item.location.OrgChartRefCode
                }).ToList();

                lblcount.Text = (Resources.Utilities.foundTotal + (gridDataSource.Count.ToString()) + Resources.Utilities.records);

                decimal pageCount = System.Math.Ceiling(Convert.ToDecimal(gridDataSource.Count) / grdItems.PageSize);
                if ((pageCount <= grdItems.CurrentPageIndex))
                {
                    grdItems.CurrentPageIndex = 0;
                }

                grdItems.DataSource = gridDataSource;
                grdItems.DataBind();
                pager1.ItemCount = gridDataSource.Count;
            }
            catch (Exception ex)
            {
                string script = FormatpopupErrorMSG(Resources.Alerts.SorryAnErrorOccurred + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Updatepanel1", script, true);
            }
        }

        private void fillLookups()
        {
            FillDllwithoptional(objRepository.GetList(0,""), lstFilterlocation, "LocationNameAr", "Code");


            //FillDllwithoptional_ALL(LooksUpsRepository.ins.FillQUnit(), lstFilterQUnit, Resources.Pages.TitleFiled, "Code", Resources.Pages.all);
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
            var BitmapArray = Convert.ToBase64String(byteImage);

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

        protected void lnkQuick_Click(object sender, EventArgs e)
        {
            FillGrid();
        }
        #endregion
    }
}