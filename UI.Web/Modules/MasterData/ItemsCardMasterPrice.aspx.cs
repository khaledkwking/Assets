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
    public partial class ItemsCardMasterPrice : BaseFormAdmin
    {
        #region "Page Members"
        public ItemRepository objRepository = IoC.Resolve<ItemRepository>();
        public string _PageTitle = Resources.Pages.ItemsList;

        #endregion

        #region "Page Events"
        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "ItemsCard.aspx";
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

                D_ItemCard obj = new D_ItemCard();
                for (int i = 0; i <= grdItems.Items.Count - 1; i++)
                {

                    if ((grdItems.Items[i].FindControl("chkItem") != null))
                    {
                        CheckBox check = (CheckBox)grdItems.Items[i].FindControl("chkItem");

                        if (check.Checked)
                        {
                            objRepository.Delete((D_ItemCard)objRepository.GetDetails(ZeroIntergerIFNull(grdItems.Items[i].Cells[0].Text)));
                        }
                    }
                }
                FillGrid();

            }
            catch (Exception ex)
            {


                string script = FormatpopupErrorMSG(Resources.Alerts.SorryDeleteDataFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Updatepanel1", script, true);
            }

        }

        protected void grdItems_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string id = e.Item.Cells[0].Text;
            ClearForm();
            ViewState["itemID"] = id;
            FillForm();
            tblshow.Visible = false;
            tblAdd.Visible = true;
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
                D_ItemCard obj = new D_ItemCard();

                string _img = UploadFileoServer(txtImage, Server.MapPath("/Layout/uploads/ItemsData/"));

                if (gets(ViewState["itemID"]).Equals("0"))
                {//Save

                    obj.ItemQrCode = Guid.NewGuid().ToString();

                    obj.ItemRefCode = txtItemRefCode.Text;
                    // obj.ItemBarCode = txtItemBarCode.Text;
                    obj.ItemRFIDCode = txtItemRFIDCode.Text;
                    obj.ItemFinanceCode = txtItemFinanceCode.Text;
                    obj.ItemNameEn = txtItemNameEn.Text;
                    obj.ItemNameAr = txtItemNameAr.Text;
                    obj.ItemDescEn = txtItemDescEn.Text;
                    obj.ItemDescAr = txtItemDescAr.Text;

                    obj.ItemCategoryId = ZeroIntergerIFNull(lstCategory.SelectedValue);
                    obj.QUnitCode = ZeroIntergerIFNull(lstQunit.SelectedValue);



                    obj.MinQty = ZeroIntergerIFNull(txtMinQty.Text);

                    obj.ScrapPeriod = ZeroIFNull(txtScrapPeriod.Text);
                    obj.ScrapAmount = ZeroIFNull(txtScrapAmount.Text);

                    obj.isActive = chkisactive.Checked;
                    if (_img != "")
                    {
                        obj.ItemImage = _img;
                    }

                    objRepository.Add(obj);
                }
                else
                { //Update 
                    obj = objRepository.GetDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));

                    obj.ItemRefCode = txtItemRefCode.Text;
                    // obj.ItemBarCode = txtItemBarCode.Text;
                    obj.ItemRFIDCode = txtItemRFIDCode.Text;
                    obj.ItemFinanceCode = txtItemFinanceCode.Text;
                    obj.ItemNameEn = txtItemNameEn.Text;
                    obj.ItemNameAr = txtItemNameAr.Text;
                    obj.ItemDescEn = txtItemDescEn.Text;
                    obj.ItemDescAr = txtItemDescAr.Text;

                    obj.ItemCategoryId = ZeroIntergerIFNull(lstCategory.SelectedValue);
                    obj.QUnitCode = ZeroIntergerIFNull(lstQunit.SelectedValue);

                    obj.MinQty = ZeroIntergerIFNull(txtMinQty.Text);

                    obj.ScrapPeriod = ZeroIFNull(txtScrapPeriod.Text);
                    obj.ScrapAmount = ZeroIFNull(txtScrapAmount.Text);


                    obj.isActive = chkisactive.Checked;
                    if (_img != "")
                    {
                        obj.ItemImage = _img;
                    }


                    objRepository.Update(obj);

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
            var objList = objRepository.GetList(txtPartOfName.Text,ZeroIntergerIFNull( lstFilterCategory.SelectedValue), ZeroIntergerIFNull(lstFilterQUnit.SelectedValue),txtFilterCode.Text);
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

            FillDllwithoptional(LooksUpsRepository.ins.FillItemCategory(), lstCategory, Resources.Pages.TitleFiled, "Code");
            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillItemCategory(), lstFilterCategory, Resources.Pages.TitleFiled, "Code" ,Resources.Pages.all);
            FillDllwithoptional(LooksUpsRepository.ins.FillQUnit(), lstQunit, Resources.Pages.TitleFiled, "Code");
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
            var objList = objRepository.GetDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            if ((objList != null))
            {


                txtItemDescAr.Text = gets(objList.ItemDescAr);
                txtItemDescEn.Text = gets(objList.ItemDescAr);
                txtItemNameAr.Text = gets(objList.ItemNameAr);
                txtItemNameEn.Text = gets(objList.ItemNameEn);
                txtMinQty.Text = gets(objList.MinQty);
                txtItemRefCode.Text = gets(objList.ItemRefCode);
                txtItemRFIDCode.Text = gets(objList.ItemRFIDCode);
                txtItemFinanceCode.Text = gets(objList.ItemFinanceCode);

                chkisactive.Checked = getBool(objList.isActive);


                txtScrapPeriod.Text = gets(objList.ScrapPeriod);
                txtScrapAmount.Text = gets(objList.ScrapAmount);


                if (objList.ItemImage != "")
                {
                    lblimage.Text = "<img width='50px' src='" + Resources.Utilities.resourcespath + "uploads/ItemsData/" + gets(objList.ItemImage) + "'>";

                }
                try
                {

                    lstCategory.SelectedValue = gets(objList.ItemCategoryId);
                    lstQunit.SelectedValue = gets(objList.QUnitCode);
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
            txtItemDescAr.Text = "";
            txtItemDescEn.Text = "";
            txtItemNameAr.Text = "";
            txtItemNameEn.Text = "";
            txtMinQty.Text = "";
            txtItemRefCode.Text = "";
            txtItemRFIDCode.Text = "";
            txtItemFinanceCode.Text = "";
            chkisactive.Checked = true;
            ViewState["itemID"] = 0;
            tblAdd.Visible = false;
            tblshow.Visible = true;
            lblSubTitle.Text = GetTitle(true);
            lblimage.Text = "";

            txtScrapAmount.Text = "";
            txtScrapPeriod.Text = "";

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

        protected void lnkUpdatePrice_Click(object sender, EventArgs e)
        {
            try
            {

                D_ItemCard obj = new D_ItemCard();
                for (int i = 0; i <= grdItems.Items.Count - 1; i++)
                {

                    if ((grdItems.Items[i].FindControl("txtmasterPrice") != null))
                    {
                        TextBox txtPrice = (TextBox)grdItems.Items[i].FindControl("txtmasterPrice");
                        var objDetails = objRepository.GetDetails(ZeroIntergerIFNull(grdItems.Items[i].Cells[0].Text));
                        if (objDetails!=null)
                        {
                            objDetails.ItemMasterPrice = ZeroIFNull(txtPrice.Text);
                            objRepository.Update(objDetails);
                        }
                    }
                }
                FillGrid();

            }
            catch (Exception ex)
            {


                string script = FormatErrorMSGSwal(Resources.Alerts.SorryDeleteDataFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Updatepanel1", script, true);
            }


        }
    }
}