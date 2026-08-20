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
using UI.Web.Helper;

namespace UI.Web.Modules.Inventory
{
    public partial class InventoryItemsList : BaseFormAdmin
    {
        #region "Page Members"
        public ItemRepository itemRepository = IoC.Resolve<ItemRepository>();
        public AssetsRepository assetsRepository = IoC.Resolve<AssetsRepository>();
        public string _PageTitle = Resources.Pages.InventoryItems;
        public string _StoreName = "";
        #endregion

        #region "Page Events"
        protected void Page_PreInit(object sender, EventArgs e)
        {
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
            //btnSave.Attributes.Add("onclick", "return chkImage();");

            _StoreName=Request.QueryString["LocationName"]!=null? Server.UrlDecode(Request.QueryString["LocationName"]) : "";
            if (!IsPostBack)
            {
                //if ((Request.UrlReferrer == null))
                //{
                //    Response.Redirect("/admin/pages/main.aspx");
                //}

                 ViewState["StockID"] = "0";
                fillLookups();
                FillGrid();
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                D_ItemCard obj = new D_ItemCard();

                using (var DC = new AssetsEntitiesNew())
                {
                    for (int i = 0; i < grdItems.Items.Count; i++)
                    {
                        CheckBox chkItem = grdItems.Items[i].FindControl("chkItem") as CheckBox;

                        if (chkItem != null && chkItem.Checked)
                        {
                            int assetCode = ZeroIntergerIFNull(grdItems.Items[i].Cells[0].Text);

                            // Delete related AssetsEventTrackings
                            var relatedEvents = DC.AssetsEventTrackings
                                                  .Where(o => o.AssetCode == assetCode)
                                                  .ToList();

                            if (relatedEvents.Any())
                            {
                                DC.AssetsEventTrackings.RemoveRange(relatedEvents);
                                DC.SaveChanges();
                            }

                            // Delete from D_ItemCard repository
                            var itemCard = itemRepository.GetDetails(assetCode);
                            if (itemCard != null)
                            {
                                itemRepository.Delete((D_ItemCard)itemCard);
                            }

                            // Log the deletion
                            Logger.Log(
                                userId: ReadSession("userId").ToString(),
                                userName: ReadSession("AdminName").ToString(),
                                tableName: "D_ItemCard",
                                action: "Delete : "+ grdItems.Items[i].Cells[5].Text,
                                recordId: assetCode.ToString()
                            );
                        }
                    }

                    // Commit all changes once
                    DC.SaveChanges();
                }

                // Refresh grid after delete
                FillGrid();
                string script11 = FormatpopupErrorMSG(Resources.Alerts.DataDeletedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Updatepanel1", script11, true);
            }
            catch (Exception ex)
            {
                string script = FormatpopupErrorMSG(Resources.Alerts.SorryDeleteDataFailed + " " + ex.Message, "1");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Updatepanel1", script, true);
            }
        }


        protected void grdItems_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string id = e.Item.Cells[0].Text;
            ClearForm();
            ViewState["StockID"] = id;
            FillForm();
            tblshow.Visible = false;
            tblAdd.Visible = true;
        }
        public string FormatDateForTextbox(object dateObj)
        {
            if (dateObj == null || dateObj == DBNull.Value)
                return "";

            DateTime dt;
            // نحاول نفهمه كـ DateTime عادي (هيفهم الاتنين)
            if (DateTime.TryParse(dateObj.ToString(), out dt))
            {
                return dt.ToString("dd/MM/yyyy");
            }

            return "";
        }

        protected void grdItems_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item))
            {
                e.Item.Attributes.Add("onmouseover", "style.backgroundColor=\'#DA9CF1\';");
                e.Item.Attributes.Add("onmouseout", "style.backgroundColor=\'#FFFFFF\';");
            }

            if ((e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "style.backgroundColor=\'#DA9CF1\';");
                e.Item.Attributes.Add("onmouseout", "style.backgroundColor=\'#EFEFEF\';");
            }

        }
        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            string script = "";
            try
            {
                AssetsEventTracking obj = new AssetsEventTracking();

                //string _img = UploadFileoServer(txtImage, Server.MapPath("/Layout/uploads/ItemsData/"));

                if (gets(ViewState["StockID"]).Equals("0"))
                {//Save

                    obj.ItemQrCode = Guid.NewGuid().ToString();

                    obj.Item_BarCode = txtBarcode.Text;

                    obj.ItemRFID = txtItemRFIDCode.Text;
                    obj.Age = ZerodecimalIFNull(txtAge.Text);
                    obj.Qty = ZeroIFNull(txtQty.Text);
                    obj.Item_Stock_Limit = ZerodecimalIFNull(txtMinQty.Text);
                    //obj.ItemDescEn = txtItemDescEn.Text;
                    //obj.ItemDescAr = txtItemDescAr.Text;
                    obj.RequestItemPrice = ZeroIFNull(txtPrice.Text);
                    //obj.ItemCategoryId = ZeroIntergerIFNull(lstCategory.SelectedValue);
                    //obj.QUnitCode = ZeroIntergerIFNull(lstQunit.SelectedValue);

                    //obj.CreatedAt = NullDateifEmptyNew(txtItemDate.Text);

                    //obj.MinQty = ZeroIntergerIFNull(txtMinQty.Text);

                    //obj.ScrapPeriod = ZeroIFNull(txtScrapPeriod.Text);
                    //obj.ScrapAmount = ZeroIFNull(txtScrapAmount.Text);

                    //obj.isActive = chkisactive.Checked;
                    //if (_img != "")
                    //{
                    //    obj.ItemImage = _img;
                    //}

                    assetsRepository.AddEventTracking(obj);

                    Logger.Log(
                            userId: ReadSession("userId").ToString(),
                            userName: ReadSession("AdminName").ToString(),
                            tableName: "AssetsEventTracking",
                            action: "Insert",
                            recordId: obj.Code.ToString()
                            );
                }
                else
                { //Update 
                    if (ViewState["StockID"] == null || ViewState["StockID"].ToString() == "0")
                    {
                        throw new Exception("Invalid Stock ID for update.");
                    }
                    else
                    {
                        int stockId = ZeroIntergerIFNull(ViewState["StockID"].ToString());
                        obj = assetsRepository.getTrackingDetails(stockId);

                        // Check if object exists before updating
                        if (obj == null)
                        {
                            throw new Exception("Stock item not found for update.");
                        }

                        obj.Item_BarCode = txtBarcode.Text;

                        obj.ItemRFID = txtItemRFIDCode.Text;
                        obj.Age = ZerodecimalIFNull(txtAge.Text);
                        obj.Qty = ZeroIFNull(txtQty.Text);
                        obj.Item_Stock_Limit = ZerodecimalIFNull(txtMinQty.Text);
                        //obj.ItemDescEn = txtItemDescEn.Text;
                        //obj.ItemDescAr = txtItemDescAr.Text;
                        obj.RequestItemPrice = ZeroIFNull(txtPrice.Text);
                        //obj.ItemDescEn = txtItemDescEn.Text;
                        //obj.ItemDescAr = txtItemDescAr.Text;
                        //obj.ItemMasterPrice = ZeroIFNull(txtPrice.Text);
                        //obj.ItemCategoryId = ZeroIntergerIFNull(lstCategory.SelectedValue);
                        //obj.QUnitCode = ZeroIntergerIFNull(lstQunit.SelectedValue);

                        //obj.CreatedAt = NullDateifEmptyNew(txtItemDate.Text);

                        //obj.MinQty = ZeroIntergerIFNull(txtMinQty.Text);

                        //obj.ScrapPeriod = ZeroIFNull(txtScrapPeriod.Text);
                        //obj.ScrapAmount = ZeroIFNull(txtScrapAmount.Text);


                        //obj.isActive = chkisactive.Checked;
                        //if (_img != "")
                        //{
                        //    obj.ItemImage = _img;
                        //}

                        assetsRepository.UpdateEventTracking(obj);

                        Logger.Log(
                               userId: ReadSession("userId").ToString(),
                               userName: ReadSession("AdminName").ToString(),
                               tableName: "AssetsEventTracking",
                               action: "Update",
                               recordId: obj.Code.ToString()
                               );
                    }

                }

                ClearForm();
                FillGrid();

                script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Updatepanel1", script, true);

            }
            catch (Exception ex)
            {

                script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Updatepanel1", script, true);
            }

        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            ClearForm();
            FillGrid();
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            FillGrid();
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {

            ClearForm();
            tblAdd.Visible = true;
            tblshow.Visible = false;
        }
        #endregion

        #region "Fill Information"
        private void FillGrid()
        {
            if (Request.QueryString["InvCode"] != null)
            {
                int InvCode = ZeroIntergerIFNull(Request.QueryString["InvCode"].ToString());
                var objList = assetsRepository.GetInventoryItems(txtPartOfName.Text, ZeroIntergerIFNull(lstFilterCategory.SelectedValue), ZeroIntergerIFNull(lstFilterQUnit.SelectedValue), txtFilterCode.Text, InvCode);
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

        }
        private void fillLookups()
        {

            //FillDllwithoptional(LooksUpsRepository.ins.FillItemCategory(), lstCategory, Resources.Pages.TitleFiled, "Code");
            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillItemCategory(), lstFilterCategory, Resources.Pages.TitleFiled, "Code" ,Resources.Pages.all);
            //FillDllwithoptional(LooksUpsRepository.ins.FillQUnit(), lstQunit, Resources.Pages.TitleFiled, "Code");
            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillQUnit(), lstFilterQUnit, Resources.Pages.TitleFiled, "Code", Resources.Pages.all);

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
            var objList = assetsRepository.GetInventoryItemById(ZeroIntergerIFNull( ViewState["StockID"].ToString()));
            if ((objList != null))
            {


                //txtItemDescAr.Text = gets(objList.ItemDescAr);
               
                txtItemNameAr.Text = gets(objList.ItemNameAr);
                txtItemNameEn.Text = gets(objList.ItemNameEn);
                txtMinQty.Text = gets(objList.Item_Stock_Limit);
                txtItemRefCode.Text = gets(objList.AssetCode);
                txtItemRFIDCode.Text = gets(objList.ItemRFID);
                txtItemFinanceCode.Text = gets(objList.ItemFinanceCode);
                txtPrice.Text = gets(objList.RequestItemPrice.ToString());
                //chkisactive.Checked = getBool(objList.isActive);

                txtItemDate.Text = getDBDate(objList.OperatedDate);
                txtAge.Text = gets(objList.Age);
                txtUnitName.Text = objList.D_QtyUnitTitleAr;
                txtCategoryName.Text = gets(objList.D_ItemsCategoryTitleAr);


                //if (objList.ItemImage != "")
                //{
                //    lblimage.Text = "<img width='50px' src='" + Resources.Utilities.resourcespath + "uploads/ItemsData/" + gets(objList.ItemImage) + "'>";

                //}
                try
                {

                    //lstCategory.SelectedValue = gets(objList.ItemCategoryId);
                    //lstQunit.SelectedValue = gets(objList.QUnitCode);
                }
                catch (Exception)
                {

                    throw;
                }


            }

            tblAdd.Visible = true;
            lblSubTitle.Text = GetTitle(false);

        }
        private void ClearForm()
        {
            //txtItemDescAr.Text = "";
            //txtItemDescEn.Text = "";
            txtItemNameAr.Text = "";
            txtItemNameEn.Text = "";
            txtMinQty.Text = "";
            txtItemDate.Text = "";
            txtItemRefCode.Text = "";
            txtItemRFIDCode.Text = "";
            txtItemFinanceCode.Text = "";
            //chkisactive.Checked = true;
             ViewState["StockID"] = 0;
            tblAdd.Visible = false;
            tblshow.Visible = true;
            lblSubTitle.Text = GetTitle(true);
            //lblimage.Text = "";

           // txtScrapAmount.Text = "";
            //txtScrapPeriod.Text = "";

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
            FillGrid();
        }

        /// <summary>
        /// Get display text and styling for CountableFlag column
        /// </summary>
        public string GetCountableFlagDisplay(object countableFlagObj)
        {
            try
            {
                bool? countableFlag = countableFlagObj as bool?;

                if (countableFlag == true)
                {
                    return "<span style='background-color: #90EE90; color: #000; padding: 5px 10px; border-radius: 3px; font-weight: bold;'>نثري</span>";
                }
                else
                {
                    return "<span style='background-color: #FFB6C1; color: #000; padding: 5px 10px; border-radius: 3px; font-weight: bold;'>غير نثري</span>";
                }
            }
            catch
            {
                return "<span style='background-color: #CCCCCC; color: #000; padding: 5px 10px; border-radius: 3px;'>غير محدد</span>";
            }
        }

        /// <summary>
        /// Get styling for MinQty column - highlight when stock is low
        /// </summary>
        public string GetMinQtyStyle(object qtyObj, object minQtyObj)
        {
            try
            {
                double qty = ZeroIFNull(qtyObj.ToString());
                decimal minQty = ZerodecimalIFNull(minQtyObj.ToString());

                // If stock quantity is less than or equal to minimum threshold, highlight in red
                if (qty <= (double)minQty && minQty > 0)
                {
                    return "background-color: #FFE0E0; color: #FF0000; font-weight: bold; padding: 5px; border-radius: 3px;";
                }

                return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Get formatted date from ActionDate field
        /// </summary>
        public string GetFormattedDate(object dateObj)
        {
            try
            {
                if (dateObj == null || dateObj == DBNull.Value)
                    return "-";

                if (DateTime.TryParse(dateObj.ToString(), out DateTime date))
                {
                    return date.ToString("dd/MM/yyyy");
                }

                return "-";
            }
            catch
            {
                return "-";
            }
        }

    }
}