using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.WHM.Forms.Purchase
{
    public partial class PurchaseOrderOperations : BaseFormAdmin
    {
        #region "Page Members"
        public PurchaseRepository objRepository = IoC.Resolve<PurchaseRepository>();
        public string _PageTitle = "Purchase Order";

        #endregion

        #region "Page Events"
        protected void Page_PreRender(object sender, System.EventArgs e)
        {

            if (hdnMasterID.Value != "" && hdnMasterID.Value != "0")
            {
                //lnkaddnewItem.Attributes.Add("class", "iframe btn btn-info btn-xs");
                //lnkaddnewItem.Attributes.Add("href", "PurchaseOrderItems.aspx?id=" + hdnMasterID.Value);
            }


        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            lblerror.Text = "";
            btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");


            btnSave.Attributes.Add("onclick", "return chkImage();");

            btnSave2.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {
                //if ((Request.UrlReferrer == null))
                //{
                //    Response.Redirect("/admin/pages/main.aspx");
                //}
                fillLookups();

                fillItemsLookups();

                ViewState["itemID"] = "0";
                ViewState["TransitemID"] = "0";
                ViewState["NotesitemID"] = "0";

                ViewState["AttachmentitemID"] = "0";
                ViewState["CustomsEmployeeitemID"] = "0";
                ViewState["StatusTrackingitemID"] = "0";
                ViewState["PurchaseOrderItemID"] = "0";

                txtTransDate.Text = gets(DateTime.Now.ToString("MM/dd/yyyy"));
                txtinboundNum.Text = GeenratePurchaseOrderSerial();

                if (Request.QueryString["id"] != null)
                {

                    lblSubTitle.Text = GetTitle(false);
                    ViewState["itemID"] = gets(Request.QueryString["id"]);
                    hdnMasterID.Value = gets(Request.QueryString["id"]);
                    FillPurchaseOrderMasterInformation();

                    FillPurchaseOrderItems();
                    FillPurchaseOrderAttachment();
                    

                    tblshow.Visible = true;
                    btnSave2.Visible = false;
                }
                else
                { lblSubTitle.Text = GetTitle(true); }

            }




        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "InboundOperrations.aspx";
        }
        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            SavePurchaseOrderMaster();
          //   tblAdd.Visible = false;
            tblshow.Visible = true;
        }

        protected void btnSave2_Click(object sender, EventArgs e)
        {

            SavePurchaseOrderMaster();
            tblAdd.Visible = false;
            tblshow.Visible = true;

        }
       

        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            FillPurchaseOrderItems();
        }



        #endregion

        #region "PurchaseOrder master Information"
        #region "Fill Information"
        private string GetTitle(bool isadd)
        {
            if (isadd)
            {
                return "Add New Receiving Request";
            }
            else
            {
                return "Request Header Information - Edit";
            }

        }
        private void ClearForm()
        {
            txtinboundNum.Text = "";
            txtNotes.Text = "";
            txtDepositNote.Text = "";
            txtDeliveryDate.Text = "";
            txtDeliveryOrder.Text = "";
            txtDepositDeclarationDate.Text = "";
            txtDepositDeclarationNo.Text = "";

            txtManifestDate.Text = "";
            txtManifestNo.Text = "";

            txtRefDate.Text = "";
            txtRefNo.Text = "";

            txtTransDate.Text = "";


            // ViewState["itemID"] = 0;
            //tblAdd.Visible = false;
            //tblshow.Visible = true;
            lblSubTitle.Text = this.GetTitle(true);
        }
        private void FillPurchaseOrderMasterInformation()
        {

            var objList = objRepository.FillDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            if ((objList != null))
            {


                txtinboundNum.Text = GetPurchaseSerialText(objList.Serial.Value,objList.TransDate.Value);

                lstInboundType.SelectedValue = gets(objList.TypeCode);
                lstDepositType.SelectedValue = gets(objList.DepositeTypeCode);
                lstcustomsDepartment.SelectedValue = gets(objList.CustomsDepartmentCode);
                lstReferanceType.SelectedValue = gets(objList.RefTypeCode);
                lstDepositDeclarationType.SelectedValue = gets(objList.DepositeDeclarationTypeCode);
              lstConsignee.SelectedValue = gets(objList.ConsigneeCode);
                lstSupplier.SelectedValue = gets(objList.SupplierId);


                txtTransDate.Text = getDBDate(objList.TransDate);

                txtRefNo.Text = gets(objList.RefNo);
                txtRefDate.Text = getDBDate(objList.RefDate);

                txtManifestNo.Text = gets(objList.ManifestNo);
                txtManifestDate.Text = getDBDate(objList.ManifestDate);

                txtNotes.Text = gets(objList.Notes);
                txtDeliveryOrder.Text = gets(objList.DeliveryOrderNo);
                txtDeliveryDate.Text = getDBDate(objList.DeliveryDate);

                txtDepositDeclarationNo.Text = gets(objList.DepositeDeclarationNo);

                txtDepositDeclarationDate.Text = gets(objList.DepositeDeclarationDate);
                txtDepositNote.Text = gets(objList.DepositeNotes);
                txtNotes.Text = gets(objList.Notes);
            }

            tblAdd.Visible = true;
            lblSubTitle.Text = this.GetTitle(false);

        }

        #endregion

        #endregion

        #region "Fill Inboud Child Information"

        #region "ITEMS"

        private void FillPurchaseOrderItems()
        {
            var objList = objRepository.FillPurchaseOrderItems(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            lblInboundItemsCount.Text = (" Found total <b>" + (objList.Count.ToString() + "</b> records"));
            decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdInboundItems.PageSize));
            if ((c <= grdInboundItems.CurrentPageIndex))
            {
                grdInboundItems.CurrentPageIndex = 0;
            }

            grdInboundItems.DataSource = objList;
            grdInboundItems.DataBind();

            pager1.ItemCount = objList.Count;

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                Item obj = new Item();
                for (int i = 0; i <= grdInboundItems.Items.Count - 1; i++)
                {

                    if ((grdInboundItems.Items[i].FindControl("chkItem") != null))
                    {
                        CheckBox check = (CheckBox)grdInboundItems.Items[i].FindControl("chkItem");

                        if (check.Checked)
                        {
                            objRepository.DeleteItems((PurchaseOrder_Item)objRepository.GetPurchaseOrderItemDetails(ZeroIntergerIFNull(grdInboundItems.Items[i].Cells[0].Text)));
                        }
                    }
                }
                FillPurchaseOrderItems();

            }
            catch (Exception ex)
            {


                string script = FormatpopupErrorMSG(Resources.Alerts.SorryDeleteDataFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }




        }
        protected void pager1_Command(object sender, CommandEventArgs e)
        {
            Int32 currnetPageIndx = ((Int32)(e.CommandArgument));
            if ((currnetPageIndx <= 0))
            {
                currnetPageIndx = 1;
            }

            if ((currnetPageIndx > grdInboundItems.PageCount))
            {
                currnetPageIndx = (grdInboundItems.PageCount - 1);
            }

            pager1.CurrentIndex = currnetPageIndx;
            grdInboundItems.CurrentPageIndex = (currnetPageIndx - 1);
            FillPurchaseOrderItems();
        }


        private void fillItemsLookups()
        {
            FillDll(LooksUpsRepository.ins.FillItemsTypes(), lstItemType, Resources.Pages.TitleFiled, "Code");
            FillDll(LooksUpsRepository.ins.FillGoodCategory(), lstGoodCategoryCode, Resources.Pages.TitleFiled, "Code");
            FillDll(LooksUpsRepository.ins.FillGoodCategoryTypes(ZeroIntergerIFNull(lstGoodCategoryCode.SelectedValue)), lstGoodCategoryTypeCode, Resources.Pages.TitleFiled, "Code");

            FillDll(LooksUpsRepository.ins.FillWeightUnitCode(), lstWeightUnitCode, Resources.Pages.TitleFiled, "Code");

            FillDll(LooksUpsRepository.ins.FillQuantityCode(), lstQtyUnitCode, Resources.Pages.TitleFiled, "Code");
            FillDll(LooksUpsRepository.ins.FillQuantityCode(), lstPackingUnit, Resources.Pages.TitleFiled, "Code");

            FillDll(LooksUpsRepository.ins.FillLocation(), lstLocationCode, Resources.Pages.TitleFiled, "Code");
            FillDll(LooksUpsRepository.ins.Fillcurrency(), lstCurrency, Resources.Pages.TitleFiled, "Code");



        }


        private void fillItemInformation()
        {
            var objList = objRepository.showPurchaseOrderItemDetails(ZeroIntergerIFNull(ViewState["PurchaseOrderItemID"].ToString()));
            if ((objList != null))
            {
 
                lstItemType.SelectedValue = gets(objList.ItemType);
                txtAlertParty.Text = objList.AlertParty;
                lstGoodCategoryCode.SelectedValue = gets(objList.GoodCategoryCode);
                FillDll(LooksUpsRepository.ins.FillGoodCategoryTypes(ZeroIntergerIFNull(lstGoodCategoryCode.SelectedValue)), lstGoodCategoryTypeCode, Resources.Pages.TitleFiled, "Code");

                lstGoodCategoryTypeCode.SelectedValue = gets(objList.GoodCategoryTypeCode);

                txtexpireyDate.Text = getDBDate(objList.ExpiryDate);

                txtConsiderations.Text = objList.Considerations;
                 lstWeightUnitCode.SelectedValue = gets(objList.WeightUnitCode);

                txtNetWeight.Text = gets(objList.NetWeight);
                txtGrossWeight.Text = gets(objList.GrossWeight);
                lstQtyUnitCode.SelectedValue = gets(objList.QtyUnitCode);

                txtQty.Text = gets(objList.Qty);
                txtEstimatedAmount.Text = gets(objList.EstimatedAmount);
                lstCurrency.SelectedValue = gets(objList.CurrencyCode);
                txtQtyActualReceived.Text = gets(objList.QtyActualReceived);

                txtNetWeightActualReceived.Text = gets(objList.NetWeightActualReceived);

                txtGrossWeightActualReceived.Text = gets(objList.GrossWeightActualReceived);


                lstPackingUnit.SelectedValue = gets(objList.PurchaseQunit);
                txtPacking.Text = gets(objList.Packing);
                txtPackingQty.Text = gets(objList.PurchaseQty);



                txtNotes.Text = gets(objList.Notes);
                txtGoodNotes.Text = gets(objList.GoodNotes);


            }


            lblSubTitle.Text = this.GetTitle(false);

            // Fill Item Unites


        }
        private void ClearItemForm()
        {
            //txttitleEn.Text = "";
            //txttitleAr.Text = "";
            //txtRef.Text = "";

            ViewState["PurchaseOrderItemID"] = "0";

            txtAlertParty.Text = "";
            txtPacking.Text = "";
            txtPackingQty.Text = "";
            txtGoodNotes.Text = ""; 
            txtQty.Text = "0";
            txtEstimatedAmount.Text = "0";
            txtNetWeight.Text = "0";
            txtConsiderations.Text = "";

            divinboundItemsAdd.Visible = false;
            DivinboundItemsShow.Visible = true;
        }
        private void SaveItemInformation()
        {

            string script = "";
            try
            {
                PurchaseOrder_Item obj = new PurchaseOrder_Item();
                if (gets(ViewState["PurchaseOrderItemID"]).Equals("0"))
                {//Save
                    obj.PurchaseOrderCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());
                    
                      obj.ConsigneeCode = ZeroIntergerIFNull(lstConsignee.SelectedValue);
                    obj.ItemType = ZeroIntergerIFNull(lstItemType.SelectedValue);

                    obj.AlertParty = txtAlertParty.Text;
                    obj.GoodCategoryCode = ZeroIntergerIFNull(lstGoodCategoryCode.SelectedValue);
                    obj.GoodCategoryTypeCode = ZeroIntergerIFNull(lstGoodCategoryTypeCode.SelectedValue);
                    obj.Considerations = txtConsiderations.Text;
                     obj.WeightUnitCode = ZeroIntergerIFNull(lstWeightUnitCode.SelectedValue);
                    obj.NetWeight = ZeroIFNull(txtNetWeight.Text);
                    obj.GrossWeight = ZeroIFNull(txtGrossWeight.Text);

                    obj.QtyUnitCode = ZeroIntergerIFNull(lstQtyUnitCode.SelectedValue);
                    obj.Qty = ZeroIFNull(txtQty.Text);
                    obj.EstimatedAmount = ZeroIFNull(txtEstimatedAmount.Text);
                    obj.CurrencyCode = ZeroIntergerIFNull(lstCurrency.SelectedValue);
                    obj.QtyActualReceived = ZeroIFNull(txtQtyActualReceived.Text);

                    obj.NetWeightActualReceived = ZeroIFNull(txtNetWeightActualReceived.Text);
                    obj.GrossWeightActualReceived = ZeroIFNull(txtGrossWeightActualReceived.Text);
                    obj.ExpiryDate = NullDateifEmpty(txtexpireyDate.Text);

                    obj.PurchaseQunit = ZeroIntergerIFNull(lstPackingUnit.SelectedValue);
                    obj.Packing = ZeroIFNull(txtPacking.Text);
                    obj.PurchaseQty = ZeroIFNull(txtPackingQty.Text);


                    //obj.LocationCode = ZeroIntergerIFNull(lstLocationCode.SelectedValue);
                    //obj.LocationNo = txtLocationNo.Text;
                    obj.Notes = txtNotes.Text;
                    obj.GoodNotes = txtGoodNotes.Text;

                    objRepository.AddItems(obj);



                }
                else
                { //Update 
                    obj = objRepository.GetPurchaseOrderItemDetails(ZeroIntergerIFNull(ViewState["PurchaseOrderItemID"].ToString()));
                    obj.PurchaseOrderCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());

                    
 
                    obj.ConsigneeCode = ZeroIntergerIFNull(lstConsignee.SelectedValue);
                    obj.ItemType = ZeroIntergerIFNull(lstItemType.SelectedValue);

                    obj.AlertParty = txtAlertParty.Text;
                    obj.GoodCategoryCode = ZeroIntergerIFNull(lstGoodCategoryCode.SelectedValue);
                    obj.GoodCategoryTypeCode = ZeroIntergerIFNull(lstGoodCategoryTypeCode.SelectedValue);

                    obj.Considerations = txtConsiderations.Text;
                     
                    obj.WeightUnitCode = ZeroIntergerIFNull(lstWeightUnitCode.SelectedValue);
                    obj.NetWeight = ZeroIFNull(txtNetWeight.Text);
                    obj.GrossWeight = ZeroIFNull(txtGrossWeight.Text);

                    obj.QtyUnitCode = ZeroIntergerIFNull(lstQtyUnitCode.SelectedValue);
                    obj.Qty = ZeroIFNull(txtQty.Text);
                    obj.EstimatedAmount = ZeroIFNull(txtEstimatedAmount.Text);
                    obj.CurrencyCode = ZeroIntergerIFNull(lstCurrency.SelectedValue);
                    obj.QtyActualReceived = ZeroIFNull(txtQtyActualReceived.Text);

                    obj.NetWeightActualReceived = ZeroIFNull(txtNetWeightActualReceived.Text);
                    obj.GrossWeightActualReceived = ZeroIFNull(txtGrossWeightActualReceived.Text);

                    //obj.LocationCode = ZeroIntergerIFNull(lstLocationCode.SelectedValue);
                    //obj.LocationNo = txtLocationNo.Text;
                    obj.Notes = txtNotes.Text;
                    obj.GoodNotes = txtGoodNotes.Text;
                    obj.ExpiryDate = NullDateifEmpty(txtexpireyDate.Text);


                    obj.PurchaseQunit = ZeroIntergerIFNull(lstPackingUnit.SelectedValue);
                    obj.Packing = ZeroIFNull(txtPacking.Text);
                    obj.PurchaseQty = ZeroIFNull(txtPackingQty.Text);

                    objRepository.UpdateItem(obj);


                }

                ClearItemForm();


                script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }
            catch (Exception ex)
            {


                script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }

        }


        protected void lnkaddnewItem_Click(object sender, EventArgs e)
        {
            divinboundItemsAdd.Visible = true;
            DivinboundItemsShow.Visible = false;
        }

        protected void lstGoodCategoryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDll(LooksUpsRepository.ins.FillGoodCategoryTypes(ZeroIntergerIFNull(lstGoodCategoryCode.SelectedValue)), lstGoodCategoryTypeCode, Resources.Pages.TitleFiled, "Code");


            // Get Default Measure Unit
            var CategoryDetails = LooksUpsRepository.ins.GetCategotyDetails(ZeroIntergerIFNull(lstGoodCategoryCode.SelectedValue));

            if (CategoryDetails!=null)
            {
                try
                {
                    lstQtyUnitCode.SelectedValue = CategoryDetails.QtyUnitCode.ToString();
                    lstQtyUnitCode.Enabled = false;
                }
                catch (Exception)
                {

                    
                }
              
            }
        }

        protected void lnkSaveItems_Click(object sender, EventArgs e)
        {
            SaveItemInformation();
            FillPurchaseOrderItems();

        }

        protected void lnkCancelItem_Click(object sender, EventArgs e)
        {
            ClearItemForm();
        }

        protected void grdInboundItems_EditCommand(object source, DataGridCommandEventArgs e)
        {
            string id = e.Item.Cells[0].Text;

            ViewState["PurchaseOrderItemID"] = id;
            fillItemInformation();

            divinboundItemsAdd.Visible = true;
            DivinboundItemsShow.Visible = false;
        }
        #endregion

        #region "PurchaseOrder Attachment"



        protected void txtimages_UploadedFileError(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            string script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData, "1");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

        }

        //protected void txtimages_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        //{
        //    try
        //    {
        //        string filename = GetFileName(txtimages);
        //        if (!(Session["AttachfileName"] == null))
        //        {
        //            //try
        //            //{
        //            //    if (File.Exists(Server.MapPath(("/Layout/uploads/Attachments/" + ViewState["itemID"].ToString() + "/" + Session["AttachfileName"]))))
        //            //    {
        //            //        File.Delete(Server.MapPath(("/Layout/uploads/Attachments/" + ViewState["itemID"].ToString() + "/" + Session["AttachfileName"])));
        //            //    }
        //            //}
        //            //catch (Exception ex)
        //            //{
        //            //}
        //        }
        //        txtimages.SaveAs(Server.MapPath(("/Layout/uploads/Attachments/" + ViewState["itemID"].ToString() + "/" + filename)));
        //        // ScriptManager.RegisterClientScriptBlock(categoryimage, categoryimage.GetType(), "img", "top.document.getElementById('imgUpload').src='image.jpg';", True)
        //        Session["AttachfileName"] = filename;
        //        //lblimageview.Text = ("<img src=\'UIResouces/uploads/empFiles/" + (filename + "\' width=\'85px\'  height=\'65px\' alt=\'\'/>"));
        //    }
        //    catch (Exception ex)
        //    {
        //        Session["AttachfileName"] = "";
        //    }
        //}
        private string getImage(FileUpload txtFile)
        {
            string imgname;
            string temp;
            string ext;
            int inx;
            int i;
            int RandChar;
            string ValueString;
            Random rnd = new Random();
            imgname = "";
            ValueString = "";
            if (!System.IO.Directory.Exists(Server.MapPath(("/Layout/uploads/Attachments/" + ViewState["itemID"]))))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(("/Layout/uploads/Attachments/" + ViewState["itemID"])));
            }

            if (!(txtFile.PostedFile == null))
            {
                if ((txtFile.PostedFile.FileName != ""))
                {
                    imgname = txtFile.PostedFile.FileName;
                    imgname = imgname.Substring((imgname.LastIndexOf("\\") + 1));
                    inx = imgname.LastIndexOf(".");
                    temp = imgname.Substring(0, inx);
                    ext = imgname.Substring((inx + 1));
                    for (i = 1; (i <= 24); i++)
                    {
                        RandChar = rnd.Next(0, i) + 65;
                        ValueString += RandChar.ToString();
                    }

                    imgname = (ValueString + ("." + ext));
                    txtFile.PostedFile.SaveAs(Server.MapPath(("/Layout/uploads/Attachments/" + ViewState["itemID"] + "/" + imgname)));
                }

            }

            return imgname;
        }


        private void FillAttachmentForm()
        {
            var objList = objRepository.GetAttachmentDeatils(ZeroIntergerIFNull(ViewState["AttachmentitemID"].ToString()));
            if ((objList != null))
            {
                txtAttachmentNotes.Text = gets(objList.Notes);

                lstAttachmentType.SelectedValue = gets(objList.AttachmentTypCode);
                ViewState["fileName"] = gets(objList.FileName);
                Session["AttachfileName"] = gets(objList.FileName);
            }

            DivAttachementShow.Visible = false;
            divAttachmentsAdd.Visible = true;
 
        }
        private void ClearAttachmentForm()
        {
            txtAttachmentNotes.Text = "";


            ViewState["AttachmentitemID"] = "0";
            divAttachmentsAdd.Visible = false;
            DivAttachementShow.Visible = true;
            Session["AttachfileName"] = null;

        }

        private void FillPurchaseOrderAttachment()
        {
            var objList = objRepository.FillPurchaseOrderAttachment(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            lblAttachmentCount.Text = (" Found total <b>" + (objList.Count.ToString() + "</b> records"));
            decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdAttachment.PageSize));
            if ((c <= grdAttachment.CurrentPageIndex))
            {
                grdAttachment.CurrentPageIndex = 0;
            }

            grdAttachment.DataSource = objList;
            grdAttachment.DataBind();

            AttachmentPager.ItemCount = objList.Count;

        }
        protected void lnkSaveAttachment_Click(object sender, EventArgs e)
        {

            string script = "";
            string fileName = getImage(txtFile);
            try
            {
                PurchaseOrderAttachments obj = new PurchaseOrderAttachments();
                if (ViewState["AttachmentitemID"].Equals("0"))
                {//Save


                    obj.PurchaseOrderCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());
                    obj.TransDate = DateTime.Now;
                    obj.Notes = txtAttachmentNotes.Text;
                    // obj.FileName = ReadSession("AttachfileName").ToString();
                    obj.FileName = fileName;
                    obj.AttachmentTypCode = ZeroIntergerIFNull(lstAttachmentType.SelectedValue);

                    objRepository.AddAttachments(obj);
                }
                else
                { //Update 
                    obj = objRepository.GetAttachmentDeatils(ZeroIntergerIFNull(ViewState["AttachmentitemID"].ToString()));

                    obj.PurchaseOrderCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());
                    obj.Notes = txtAttachmentNotes.Text;
                    //  obj.FileName = ReadSession("AttachfileName").ToString();
                    if (fileName != "")
                    {
                        obj.FileName = fileName;
                    }
                    obj.AttachmentTypCode = ZeroIntergerIFNull(lstAttachmentType.SelectedValue);

                    objRepository.UpdateAttachment(obj);

                }

                ClearAttachmentForm();
                FillPurchaseOrderAttachment();

                script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }
            catch (Exception ex)
            {


                script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }

        }

        protected void lnkCancelAttachement_Click(object sender, EventArgs e)
        {
            ClearAttachmentForm();
        }

        protected void lnkAttachmentAdd_Click(object sender, EventArgs e)
        {
            divAttachmentsAdd.Visible = true;
            DivAttachementShow.Visible = false;
        }

        protected void lnkDeleteAttachment_Click(object sender, EventArgs e)
        {
            try
            {

                PurchaseOrderAttachments obj = new PurchaseOrderAttachments();
                for (int i = 0; i <= grdAttachment.Items.Count - 1; i++)
                {

                    if ((grdAttachment.Items[i].FindControl("chkItem") != null))
                    {
                        CheckBox check = (CheckBox)grdAttachment.Items[i].FindControl("chkItem");

                        if (check.Checked)
                        {
                            objRepository.DeleteAttachment((PurchaseOrderAttachments)objRepository.GetAttachmentDeatils(ZeroIntergerIFNull(grdAttachment.Items[i].Cells[0].Text)));
                        }
                    }
                }
                FillPurchaseOrderAttachment();

            }
            catch (Exception ex)
            {


                string script = FormatpopupErrorMSG(Resources.Alerts.SorryDeleteDataFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }


            divAttachmentsAdd.Visible = false;
            DivAttachementShow.Visible = true;
        }

        protected void grdAttachment_EditCommand(object source, DataGridCommandEventArgs e)
        {

            string id = e.Item.Cells[0].Text;

            ViewState["AttachmentitemID"] = id;
            FillAttachmentForm();

            divAttachmentsAdd.Visible = true;
            DivAttachementShow.Visible = false;



        }

        protected void AttachmentPager_Command(object sender, CommandEventArgs e)
        {
            Int32 currnetPageIndx = ((Int32)(e.CommandArgument));
            if ((currnetPageIndx <= 0))
            {
                currnetPageIndx = 1;
            }

            if ((currnetPageIndx > grdAttachment.PageCount))
            {
                currnetPageIndx = (grdAttachment.PageCount - 1);
            }

            AttachmentPager.CurrentIndex = currnetPageIndx;
            grdAttachment.CurrentPageIndex = (currnetPageIndx - 1);
            FillPurchaseOrderAttachment();

        }
        #endregion

         

         

        #endregion

        #region "Shared Methods"
        #region "Fill Lookups Information"
        private void fillLookups()
        {
            FillDll(LooksUpsRepository.ins.FillInboundTypes(), lstInboundType, Resources.Pages.TitleFiled, "Code");
            FillDll(LooksUpsRepository.ins.FillDepositeTypes(), lstDepositType, Resources.Pages.TitleFiled, "Code");
            FillDll(LooksUpsRepository.ins.FillCustomsDepartments(), lstcustomsDepartment, Resources.Pages.TitleFiled, "Code");


            FillDll(LooksUpsRepository.ins.FillReferenceTypes(), lstReferanceType, Resources.Pages.TitleFiled, "Code");

            FillDll(LooksUpsRepository.ins.FillDepositeDeclaration(), lstDepositDeclarationType, Resources.Pages.TitleFiled, "Code");
            //  FillDll(LooksUpsRepository.ins.FillDepositDeclaration(), lstDepositDeclarationType, Resources.Pages.TitleFiled, "Code");

            FillDll(LooksUpsRepository.ins.FillAttachmentTypes(), lstAttachmentType, Resources.Pages.TitleFiled, "Code");


             FillDll(LooksUpsRepository.ins.FillConsignee(1), lstConsignee, "FullNameEn", "Code");//الجمعيات
            FillDll(LooksUpsRepository.ins.FillConsignee(3), lstSupplier, "FullNameEn", "Code");//الجهات المورده

 
 
        }
        #endregion
        private void SavePurchaseOrderMaster()
        {

            string script = "";
            try
            {
                PurchaseOrder obj = new PurchaseOrder();
                if (ViewState["itemID"].Equals("0"))
                {//Save

                    obj.Serial = objRepository.getCurrentYearPurchaseOrderCount(DateTime.Now.Year) + 1; 
                   // obj.Serial = txtPurchaseOrderNum.Text;
                    obj.TMonth = NullDateifEmpty(txtTransDate.Text).Month;
                    obj.TYear = NullDateifEmpty(txtTransDate.Text).Year;

                    obj.TransDate = NullDateifEmpty(txtTransDate.Text);

                    obj.TypeCode = ZeroIntergerIFNull(lstInboundType.SelectedValue);
                    obj.DepositeTypeCode = ZeroIntergerIFNull(lstDepositType.SelectedValue);
                    obj.CustomsDepartmentCode = ZeroIntergerIFNull(lstcustomsDepartment.SelectedValue);
                    obj.RefTypeCode = ZeroIntergerIFNull(lstReferanceType.SelectedValue);
                    obj.RefNo = txtRefNo.Text;


                    obj.RefDate = NullDateifEmpty(txtRefDate.Text);
                    obj.ManifestNo = txtManifestNo.Text;

                    obj.ManifestDate = NullDateifEmpty(txtManifestDate.Text);
                    obj.Notes = txtNotes.Text;
                    obj.DeliveryOrderNo = txtDeliveryOrder.Text;
                    obj.DeliveryDate = NullDateifEmpty(txtDeliveryDate.Text);
                    obj.DepositeDeclarationTypeCode = ZeroIntergerIFNull(lstDepositDeclarationType.SelectedValue);
                    obj.DepositeDeclarationNo = txtDepositDeclarationNo.Text;
                    obj.DepositeDeclarationDate = NullDateifEmpty(txtDepositDeclarationDate.Text);
                    obj.DepositeNotes = gets(txtDepositNote.Text);
                    obj.Notes = gets(txtNotes.Text);

                    obj.ConsigneeCode = ZeroIntergerIFNull(lstConsignee.SelectedValue);
                    obj.SupplierId = ZeroIntergerIFNull(lstSupplier.SelectedValue);

                    objRepository.AddPurchaseOrder(obj);
                    hdnMasterID.Value = gets(obj.Code);
                    ViewState["itemID"] = gets(obj.Code);

                    // Save Documenting Status

                    //PurchaseOrderStatusTrack objStatus = new PurchaseOrderStatusTrack();
                    //if (ViewState["StatusTrackingitemID"].Equals("0"))
                    //{//Save


                    //    objStatus.PurchaseOrderCode = obj.Code;
                    //    objStatus.TransDate = DateTime.Now;
                    //    objStatus.Notes ="New Request Documenting Status";
                    //    objStatus.DepositeStatusTypeCode = 1;


                    //    objRepository.AddStatusTracking(objStatus);
                    //}

                }
                else
                { //Update 

                    hdnMasterID.Value = ViewState["itemID"].ToString();
                    obj = objRepository.GetDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));

                   
                    obj.TMonth = NullDateifEmpty(txtTransDate.Text).Month;
                    obj.TYear = NullDateifEmpty(txtTransDate.Text).Year;

                    obj.TransDate = NullDateifEmpty(txtTransDate.Text);

                    obj.TypeCode = ZeroIntergerIFNull(lstInboundType.SelectedValue);
                    obj.DepositeTypeCode = ZeroIntergerIFNull(lstDepositType.SelectedValue);
                    obj.CustomsDepartmentCode = ZeroIntergerIFNull(lstcustomsDepartment.SelectedValue);
                    obj.RefTypeCode = ZeroIntergerIFNull(lstReferanceType.SelectedValue);
                    obj.RefNo = txtRefNo.Text;


                    obj.RefDate = NullDateifEmpty(txtRefDate.Text);
                    obj.ManifestNo = txtManifestNo.Text;

                    obj.ManifestDate = NullDateifEmpty(txtManifestDate.Text);
                    obj.Notes = txtNotes.Text;
                    obj.DeliveryOrderNo = txtDeliveryOrder.Text;
                    obj.DeliveryDate = NullDateifEmpty(txtDeliveryDate.Text);
                    obj.DepositeDeclarationTypeCode = ZeroIntergerIFNull(lstDepositDeclarationType.SelectedValue);
                    obj.DepositeDeclarationNo = txtDepositDeclarationNo.Text;
                    obj.DepositeDeclarationDate = NullDateifEmpty(txtDepositDeclarationDate.Text);
                    obj.DepositeNotes = gets(txtDepositNote.Text);
                    obj.Notes = gets(txtNotes.Text);
                    obj.ConsigneeCode = ZeroIntergerIFNull(lstConsignee.SelectedValue);
                    obj.SupplierId = ZeroIntergerIFNull(lstSupplier.SelectedValue);
                    objRepository.UpdatePurchaseOrder(obj);

                }

                ViewState["itemID"] = obj.Code;

                ClearForm();
                FillPurchaseOrderMasterInformation();

                script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }
            catch (Exception ex)
            {


                script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }

        }

        public string setmaterstyle()
        {
            //if (hdnMasterID.Value != "" && hdnMasterID.Value != "0")
            //{
            //    return "display:none";
            //}
            return "display:block";
           

         }

        private string GeenratePurchaseOrderSerial()
        {
            int _serial = objRepository.getCurrentYearPurchaseOrderCount(DateTime.Now.Year) + 1;
            return "PUR/" + _serial.ToString("0###") + "/GCS" + DateTime.Now.ToString("yy");
        }


        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void txtPackingQty_TextChanged(object sender, EventArgs e)
        {
            if (txtPackingQty.Text!="" && txtPacking.Text != "")
            {

                txtQty.Text = (ZeroIFNull(txtPackingQty.Text) * ZeroIFNull(txtPacking.Text)).ToString();
            }

        }

        protected void txtPacking_TextChanged(object sender, EventArgs e)
        {

            if (txtPackingQty.Text != "" && txtPacking.Text != "")
            {

                txtQty.Text = (ZeroIFNull(txtPackingQty.Text) * ZeroIFNull(txtPacking.Text)).ToString();
            }
        }
    }
}

