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

namespace UI.Web.Modules.StoreOperations.Forms.Inboud.Inboud
{
    public partial class InboundOperrations : BaseFormAdmin
    {
        #region "Page Members"
        public InboundRepository objRepository = IoC.Resolve<InboundRepository>();
        public string _PageTitle = Resources.Pages.InboundOperations;

        #endregion

        #region "Page Events"
        protected void Page_PreRender(object sender, System.EventArgs e)
        {



        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            string script = "";
            try
            {

                    Page.Form.Attributes.Add("enctype", "multipart/form-data");
                    lblerror.Text = "";
                    btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
                    btnSave.Attributes.Add("onclick", "return chkImage();");
                    lnkSaveItems.Attributes.Add("onclick", "return ValidateInboundITems();");
                    btnSave2.Attributes.Add("onclick", "return chkImage();");
                    if (!IsPostBack)
                    {
                        //if ((Request.UrlReferrer == null))
                        //{
                        //    Response.Redirect("/admin/pages/main.aspx");
                        //}
                        fillLookups(); 
                        ViewState["itemID"] = "0";
                        ViewState["TransitemID"] = "0";
                        ViewState["NotesitemID"] = "0";
                        ViewState["inboundItemID"] = "0";

                        ViewState["AttachmentitemID"] = "0";
                        ViewState["CustomsEmployeeitemID"] = "0";
                        ViewState["StatusTrackingitemID"] = "0";

                        if (Request.QueryString["type"] != null)
                        {
                            lstInboundTypeCode.SelectedValue = Request.QueryString["type"].ToString();
                            lstInboundTypeCode_SelectedIndexChanged(null, null);

                        }
                        else
                        {
                            lstInboundTypeCode.SelectedValue = "1";
                            lstInboundTypeCode_SelectedIndexChanged(null, null);
                        }

                        if (Request.QueryString["id"] != null)
                        {

                            //lblSubTitle.Text = GetTitle(false);
                            ViewState["itemID"] = gets(Request.QueryString["id"]);
                            hdnMasterID.Value = gets(Request.QueryString["id"]);
                            FillInboundMasterInformation();

                            FillInboundItems();
                            FillInboundNotes();
                            FillInboundAttachment();
                            FillInboundStatusTracking();

                  
                            btnSave2.Visible = false;
                        }
                        else
                        {
                            txtSerial.Text = generateRequestSerial();
                            txtTransDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                            //lblSubTitle.Text = GetTitle(true);
                        }

                    }

            }
            catch (Exception ex)
            {


                script = FormatpopupErrorMSG(Resources.Alerts.LoadFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }


        }
        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            SaveInboundMaster();
            FillInboundStatusTracking();

        }
        protected void btnSave2_Click(object sender, EventArgs e)
        {

            SaveInboundMaster();
           

        }
        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            FillInboundItems();
        }
        #endregion

        #region "Inbound master Information"
        #region "Fill Information"
        private string GetTitle(bool isadd)
        {
            if (isadd)
            {
                return Resources.Pages.addnewrecord1;
            }
            else
            {
                return Resources.Pages.editRecord;
            }
        }
        private void ClearForm()
        {
            txtRefDate.Text = "";
            txtRefNo.Text = "";
            txtSerial.Text = "";
            txtDeliveryDate.Text = "";
            txtTransDate.Text = "";
            txtDeliveryOrderNo.Text = "";
            txtDeliveryDate.Text = "";
            txtDepositeNotes.Text = "";

            // ViewState["itemID"] = 0;
            //tblAdd.Visible = false;
            //tblshow.Visible = true;
           // lblSubTitle.Text = this.GetTitle(true);
        }
        private void FillInboundMasterInformation()
        {

            var objList = objRepository.FillDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            if ((objList != null))
            {


                txtSerial.Text = gets(objList.Serial);

                lstInboundTypeCode.SelectedValue = gets(objList.InboundTypeCode);
                SetInboundType();

               
                try
                {
                    if (objList.FromVendorCode != null)
                    {
                        lstFromVendorCode.SelectedValue = gets(objList.FromVendorCode);
                    }
                    lstTargetLocationCode.SelectedValue = gets(objList.TargetLocationCode);
                    lstOwnerLocationCode.SelectedValue = gets(objList.OwnerLocationCode);

                }
                catch (Exception)
                {

                    
                }
                
            
                txtTransDate.Text = objList.TransDate.Value.ToString("MM/dd/yyyy");

                txtRefNo.Text = gets(objList.RefNo);
                txtRefDate.Text = objList.RefDate.Value.ToString("MM/dd/yyyy");


                txtDeliveryOrderNo.Text = gets(objList.DeliveryOrderNo);
                txtDeliveryDate.Text = NullDateifEmptyText (objList.DeliveryDate);

                txtDepositeNotes.Text = gets(objList.DepositeNotes);
                txtNotes.Text = gets(objList.Notes);
            }

            //tblAdd.Visible = true;
            //lblSubTitle.Text = this.GetTitle(false);

        }
        private void SetInboundType()
        {
            if (lstInboundTypeCode.SelectedValue == "2")
            {
                divOwnerLocation.Visible = true;
                divVendor.Visible = false;
            }
            else if (lstInboundTypeCode.SelectedValue == "1")
            {
                divOwnerLocation.Visible = false;
                divVendor.Visible = true;
            }
            else
            {
                divOwnerLocation.Visible = false;
                divVendor.Visible = false;
            }
        }
        public string getActiveTab(string selectedTab)
        {
            if (hdnActiveTab.Value == "" && selectedTab == "1")
            {
                return "active show";

            }
            else if (hdnActiveTab.Value == selectedTab)
            {
                return "active show";

            }
            return "";
        }
        #endregion

        #endregion

        #region "Fill Inboud Child Information"

        #region "ITEMS"

        private void ClearItemForm()
        {

            ViewState["inboundItemID"] = "0";
            txtTagId.Text = "";
            txtexpireyDate.Text = "";
            txtGoodNotes.Text = "";
             txtQty.Text = "0";
            txtUnitCost.Text = "0";
            lstPurchaseItems.SelectedValue = "0";

            //divinboundItemsAdd.Visible = false;
            //DivinboundItemsShow.Visible = true;
        }
        private void SaveItemInformation()
        {

            string script = "";
            try
            {
                AssetsItemUnit obj = new AssetsItemUnit();
                if (gets(ViewState["inboundItemID"]).Equals("0"))
                {//Save

                    var objPurchaseItem = objRepository.getItemCardDetails(ZeroIntergerIFNull(lstPurchaseItems.SelectedValue));

                    if (objPurchaseItem != null)
                    {
                        obj.InboundCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());
                        obj.ItemCode = objPurchaseItem.Code;
                        obj.QUnitCode = objPurchaseItem.QUnitCode;
                        obj.Qty = ZeroIFNull(txtQty.Text);
                        obj.EstimatedUnitCost = ZeroIFNull(txtUnitCost.Text);
                        obj.UnitStatus = ZeroIntergerIFNull(lstStatusCode.SelectedValue);
                        obj.Notes = txtGoodNotes.Text;

                        if (txtexpireyDate.Text!="")
                        {
                            obj.ExpireDate = NullDateifEmpty(txtexpireyDate.Text);
                        }
                        
                        obj.ItemTag = (txtTagId.Text);
                        obj.CreatedAt = DateTime.Now;
                        obj.CreatedBy = ZeroIntergerIFNull(gets(ReadSession("userid")));
                        objRepository.AddItemsUnit(obj);

                    }



                }
                else
                { //Update 
                    obj = objRepository.GetInboundItemDetails(ZeroIntergerIFNull(ViewState["inboundItemID"].ToString()));

                    obj.InboundCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());
                    obj.QUnitCode = ZeroIntergerIFNull(lstQtyUnitCode.SelectedValue);
                    obj.Qty = ZeroIFNull(txtQty.Text);
                    obj.EstimatedUnitCost = ZeroIFNull(txtUnitCost.Text);
                    obj.Notes = txtGoodNotes.Text;
                    obj.UnitStatus = ZeroIntergerIFNull(lstStatusCode.SelectedValue);
                    obj.ExpireDate = NullDateifEmpty(txtexpireyDate.Text);
                    obj.ItemTag = (txtTagId.Text);

                    obj.LastModifiedAt = DateTime.Now;
                    obj.LastModifiedBy = ZeroIntergerIFNull(gets(ReadSession("userid")));

                    objRepository.UpdateItemunit(obj);


                }

                ClearItemForm();

                FillInboundItems();

                script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }
            catch (Exception ex)
            {


                script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }

        }

        private void FillInboundItems()
        {
            var objList = objRepository.FillInboundItems(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            lblInboundItemsCount.Text = (" Found total <b>" + (objList.Count.ToString() + "</b> records"));
            decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdInboundItems.PageSize));
            if ((c <= grdInboundItems.CurrentPageIndex))
            {
                grdInboundItems.CurrentPageIndex = 0;
            }
            lblItemCount.Text = objList.Count.ToString();
            grdInboundItems.DataSource = objList;
            grdInboundItems.DataBind();


        }

        protected void grdInboundItems_EditCommand(object source, DataGridCommandEventArgs e)
        {
            string id = e.Item.Cells[0].Text;

            ViewState["inboundItemID"] = id;
            fillItemInformation();

            //divinboundItemsAdd.Visible = true;
            //DivinboundItemsShow.Visible = false;
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    Item obj = new Item();
            //    for (int i = 0; i <= grdInboundItems.Items.Count - 1; i++)
            //    {

            //        if ((grdInboundItems.Items[i].FindControl("chkItem") != null))
            //        {
            //            CheckBox check = (CheckBox)grdInboundItems.Items[i].FindControl("chkItem");

            //            if (check.Checked)
            //            {
            //                objRepository.DeleteItems((Item)objRepository.GetInboundItemDetails(ZeroIntergerIFNull(grdInboundItems.Items[i].Cells[0].Text)));
            //            }
            //        }
            //    }
            //    FillInboundItems();

            //}
            //catch (Exception ex)
            //{


            //    string script = FormatpopupErrorMSG(Resources.Alerts.SorryDeleteDataFailed + ex.Message.ToString(), "1");
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            //}




        }



        protected void lstPurchaseItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            string script = "";
            try
            {
                // Set Item Info

                var objPurchaseItem = objRepository.getItemCardDetails(ZeroIntergerIFNull(lstPurchaseItems.SelectedValue));

                if (objPurchaseItem != null)
                {

                    lstQtyUnitCode.SelectedValue = objPurchaseItem.QUnitCode.ToString();


                }
            }
            catch (Exception ex)
            {
                script = FormatpopupErrorMSG(Resources.Alerts.PleaseEnterItemDescription + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }


        }


        protected void lnkSaveItems_Click(object sender, EventArgs e)
        {
            SaveItemInformation();
            ClearItemForm();
            FillInboundItems();

        }

        private void fillItemInformation()
        {

            var objList = objRepository.getInboundItemDetails(ZeroIntergerIFNull(ViewState["inboundItemID"].ToString()));
            if ((objList != null))
            {


                lstPurchaseItems.SelectedValue = gets(objList.ItemCode);
                lstQtyUnitCode.SelectedValue = gets(objList.QUnitCode);
                lstStatusCode.SelectedValue = gets(objList.UnitStatus);
                txtQty.Text = gets(objList.Qty);
                txtTagId.Text = gets(objList.ItemTag);
                txtUnitCost.Text = gets(objList.EstimatedUnitCost);
                txtGoodNotes.Text = gets(objList.Notes);
                     txtexpireyDate.Text = NullDateifEmptyText(objList.ExpireDate);
            }

            //tblAdd.Visible = true;
            //lblSubTitle.Text = this.GetTitle(false);

        }

        #endregion



        #region "Inbound Notes "
        private string GetInbouboundNoteTitle(bool isadd)
        {
            if (isadd)
            {
                return Resources.Pages.AddNewRecord;
            }
            else
            {
                return Resources.Pages.EditRecordMsg;
            }

        }
        private void FillNotesForm()
        {
            var objList = objRepository.GetNotesDeatils(ZeroIntergerIFNull(ViewState["NotesitemID"].ToString()));
            if ((objList != null))
            {
                txtInboundNotes.Text = gets(objList.Notes);

            }

            divNotesShow.Visible = false;
            divInboundNotesAdd.Visible = true;
            lblNotesTitle.Text = this.GetInbouboundNoteTitle(false);

        }
        private void ClearNoteForm()
        {
            txtInboundNotes.Text = "";


            ViewState["NotesitemID"] = "0";
            divInboundNotesAdd.Visible = false;
            divNotesShow.Visible = true;

        }

        private void FillInboundNotes()
        {
            var objList = objRepository.FillInboundNotes(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            lblNotesCound.Text = (" Found total <b>" + (objList.Count.ToString() + "</b> records"));
            decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdInboundNotes.PageSize));
            if ((c <= grdInboundNotes.CurrentPageIndex))
            {
                grdInboundNotes.CurrentPageIndex = 0;
            }

            grdInboundNotes.DataSource = objList;
            grdInboundNotes.DataBind();

            NotePager.ItemCount = objList.Count;

        }
        protected void lnkSaveNotes_Click(object sender, EventArgs e)
        {

            string script = "";
            try
            {
                InboundNote obj = new InboundNote();
                if (ViewState["NotesitemID"].Equals("0"))
                {//Save


                    obj.InboundCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());
                    obj.TransDate = DateTime.Now;
                    obj.Notes = txtInboundNotes.Text;


                    objRepository.AddNotes(obj);
                }
                else
                { //Update 
                    obj = objRepository.GetNotesDeatils(ZeroIntergerIFNull(ViewState["NotesitemID"].ToString()));

                    obj.InboundCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());
                    obj.Notes = txtInboundNotes.Text;

                    objRepository.UpdateNotes(obj);

                }

                ClearNoteForm();
                FillInboundNotes();

                script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }
            catch (Exception ex)
            {


                script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }

        }

        protected void lnkCancelNotes_Click(object sender, EventArgs e)
        {
            ClearNoteForm();
        }

        protected void lnkAddNotes_Click(object sender, EventArgs e)
        {
            divInboundNotesAdd.Visible = true;
            divNotesShow.Visible = false;
        }

        protected void lnkDeleteNotes_Click(object sender, EventArgs e)
        {
            try
            {

                InboundNote obj = new InboundNote();
                for (int i = 0; i <= grdInboundNotes.Items.Count - 1; i++)
                {

                    if ((grdInboundNotes.Items[i].FindControl("chkItem") != null))
                    {
                        CheckBox check = (CheckBox)grdInboundNotes.Items[i].FindControl("chkItem");

                        if (check.Checked)
                        {
                            objRepository.DeleteNotes((InboundNote)objRepository.GetNotesDeatils(ZeroIntergerIFNull(grdInboundNotes.Items[i].Cells[0].Text)));
                        }
                    }
                }
                FillInboundNotes();

            }
            catch (Exception ex)
            {


                string script = FormatpopupErrorMSG(Resources.Alerts.SorryDeleteDataFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }


            divInboundNotesAdd.Visible = false;
            divNotesShow.Visible = true;
        }

        protected void grdInboundNotes_EditCommand(object source, DataGridCommandEventArgs e)
        {
            string id = e.Item.Cells[0].Text;

            ViewState["NotesitemID"] = id;
            FillNotesForm();

            divInboundNotesAdd.Visible = true;
            divNotesShow.Visible = false;
        }

        protected void Pager2_Command(object sender, CommandEventArgs e)
        {
            Int32 currnetPageIndx = ((Int32)(e.CommandArgument));
            if ((currnetPageIndx <= 0))
            {
                currnetPageIndx = 1;
            }

            if ((currnetPageIndx > grdInboundNotes.PageCount))
            {
                currnetPageIndx = (grdInboundNotes.PageCount - 1);
            }

            NotePager.CurrentIndex = currnetPageIndx;
            grdInboundNotes.CurrentPageIndex = (currnetPageIndx - 1);
            this.FillInboundNotes();
        }

        #endregion

        #region "Inbound Attachment"
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
            string script = "";
            try
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
                    lblAttachmentTitle.Text = this.GetInbouboundNoteTitle(false);
            }
            catch (Exception ex)
            {
                script = FormatpopupErrorMSG(Resources.Alerts.LoadFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }
        }
        private void ClearAttachmentForm()
        {
            txtAttachmentNotes.Text = "";
            ViewState["AttachmentitemID"] = "0";
            divAttachmentsAdd.Visible = false;
            DivAttachementShow.Visible = true;
            Session["AttachfileName"] = null;

        }
        private void FillInboundAttachment()
        {
            string script = "";
            try
            {
                var objList = objRepository.FillInboundAttachment(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
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
            catch (Exception ex)
            {
                script = FormatpopupErrorMSG(Resources.Alerts.LoadFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }

        }
        protected void lnkSaveAttachment_Click(object sender, EventArgs e)
        {
            string script = "";
            string fileName = getImage(txtFile);
            try
            {
                InboundAttachment obj = new InboundAttachment();
                if (ViewState["AttachmentitemID"].Equals("0"))
                {//Save


                    obj.InboundCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());
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

                    obj.InboundCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());
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
                FillInboundAttachment();
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
<<<<<<< HEAD
                InboundAttachment obj = new InboundAttachment();
=======
                InboundAttachments obj = new InboundAttachments();
>>>>>>> faa5d2395a94e9dcd93e977631f3e78280d2ec0f
                for (int i = 0; i <= grdAttachment.Items.Count - 1; i++)
                {
                    if ((grdAttachment.Items[i].FindControl("chkItem") != null))
                    {
                        CheckBox check = (CheckBox)grdAttachment.Items[i].FindControl("chkItem");

                        if (check.Checked)
                        {
                            objRepository.DeleteAttachment((InboundAttachment)objRepository.GetAttachmentDeatils(ZeroIntergerIFNull(grdAttachment.Items[i].Cells[0].Text)));
                        }
                    }
                }
                FillInboundAttachment();
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
            FillInboundAttachment();

        }
        #endregion



        #region "Status Tracking"


        private void FillStatusTrackingDetails()
        {
            var objList = objRepository.FillInboundStatusDetails(ZeroIntergerIFNull(ViewState["StatusTrackingitemID"].ToString()));
            if ((objList != null))
            {
                txtStatsuNote.Text = gets(objList.Notes);
                lstDepositStatusTypeCode.SelectedValue = gets(objList.DepositeStatusTypeCode);

            }
            DivStatusTrackinghow.Visible = false;
            DivStatusTrackingAdd.Visible = true;

        }
        private void ClearStatusTracking()
        {
            txtStatsuNote.Text = "";
            ViewState["StatusTrackingitemID"] = "0";
            DivStatusTrackinghow.Visible = true;
            DivStatusTrackingAdd.Visible = false;
        }

        private void FillInboundStatusTracking()
        {
            var objList = objRepository.FillInboundStatusTracking(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
            lblStatusTrackingCount.Text = (" Found total <b>" + (objList.Count.ToString() + "</b> records"));
            decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdStatusTracking.PageSize));
            if ((c <= grdStatusTracking.CurrentPageIndex))
            {
                grdStatusTracking.CurrentPageIndex = 0;
            }

            grdStatusTracking.DataSource = objList;
            grdStatusTracking.DataBind();
            StatusTrackingPager.ItemCount = objList.Count;

        }

        protected void lnkStatusTrackingCancel_Click(object sender, EventArgs e)
        {
            ClearStatusTracking();

        }

        protected void lnkStatusTrackingSave_Click(object sender, EventArgs e)
        {
            string script = "";
            // string fileName = getImage(lblFile);
            try
            {
                InboundStatusTrack obj = new InboundStatusTrack();
                if (ViewState["StatusTrackingitemID"].Equals("0"))
                {//Save


                    obj.InboundCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());
                    obj.TransDate = DateTime.Now;
                    obj.Notes = txtStatsuNote.Text;
                    obj.DepositeStatusTypeCode = ZeroIntergerIFNull(lstDepositStatusTypeCode.SelectedValue);


                    objRepository.AddStatusTracking(obj);
                }
                else
                { //Update 
                    obj = objRepository.FillInboundStatusDetails(ZeroIntergerIFNull(ViewState["StatusTrackingitemID"].ToString()));


                    obj.InboundCode = ZeroIntergerIFNull(ViewState["itemID"].ToString());
                    obj.Notes = txtStatsuNote.Text;
                    obj.DepositeStatusTypeCode = ZeroIntergerIFNull(lstDepositStatusTypeCode.SelectedValue);

                    objRepository.UpdateStatusTracking(obj);

                }

                ClearStatusTracking();
                FillInboundStatusTracking();

                script = FormatpopupErrorMSG(Resources.Alerts.DataSavedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }
            catch (Exception ex)
            {


                script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }
        }

        protected void lnkStatusTrackingAdd_Click(object sender, EventArgs e)
        {
            DivStatusTrackinghow.Visible = false;
            DivStatusTrackingAdd.Visible = true;

        }

        protected void lnkStatusTracking_Click(object sender, EventArgs e)
        {
            try
            {

                InboundStatusTrack obj = new InboundStatusTrack();
                for (int i = 0; i <= grdStatusTracking.Items.Count - 1; i++)
                {

                    if ((grdStatusTracking.Items[i].FindControl("chkItem") != null))
                    {
                        CheckBox check = (CheckBox)grdStatusTracking.Items[i].FindControl("chkItem");

                        if (check.Checked)
                        {
                            objRepository.DeleteStatusTracking((InboundStatusTrack)objRepository.FillInboundStatusDetails(ZeroIntergerIFNull(grdStatusTracking.Items[i].Cells[0].Text)));
                        }
                    }
                }
                FillInboundStatusTracking();

            }
            catch (Exception ex)
            {


                string script = FormatpopupErrorMSG(Resources.Alerts.SorryDeleteDataFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }


            DivStatusTrackingAdd.Visible = false;
            DivStatusTrackinghow.Visible = true;

        }

        protected void grdStatusTracking_EditCommand(object source, DataGridCommandEventArgs e)
        {
            string id = e.Item.Cells[0].Text;

            ViewState["StatusTrackingitemID"] = id;
            FillStatusTrackingDetails();

            DivStatusTrackingAdd.Visible = true;
            DivStatusTrackinghow.Visible = false;


        }

        protected void StatusTrackingPager_Command(object sender, CommandEventArgs e)
        {
            Int32 currnetPageIndx = ((Int32)(e.CommandArgument));
            if ((currnetPageIndx <= 0))
            {
                currnetPageIndx = 1;
            }

            if ((currnetPageIndx > grdStatusTracking.PageCount))
            {
                currnetPageIndx = (grdStatusTracking.PageCount - 1);
            }

            StatusTrackingPager.CurrentIndex = currnetPageIndx;
            grdStatusTracking.CurrentPageIndex = (currnetPageIndx - 1);
            FillInboundStatusTracking();
        }
        #endregion

        #endregion

        #region "Shared Methods"
        #region "Fill Lookups Information"
        private string generateRequestSerial()
        {
            int YearRequestCount = objRepository.getCurrentYearInboundCount(DateTime.Now.Year);
            return "IN/" + string.Format("{0:000000}", YearRequestCount + 1) + "/CMGS" + DateTime.Now.ToString("yy");


        }
        private void fillLookups()
        {
            FillDllwithoptional(LooksUpsRepository.ins.FillInboundTypes(), lstInboundTypeCode, "TitleAr", "Code");
            lstInboundTypeCode.SelectedValue = "1";
            FillDllwithoptional(LooksUpsRepository.ins.Fillvendor(), lstFromVendorCode, "VendorNameAr", "Code");

            var LocationsList = LooksUpsRepository.ins.FillStoreLocations();
            FillDllwithoptional(LocationsList, lstTargetLocationCode, "path", "Code");
            FillDllwithoptional(LocationsList, lstOwnerLocationCode, "path", "Code");

            FillDllwithoptional(LooksUpsRepository.ins.FillAttachmentTypes(), lstAttachmentType, "TitleAr", "Code");

            FillDllwithoptional(LooksUpsRepository.ins.FillDepositeStatusType(), lstDepositStatusTypeCode, "TitleAr", "Code");

            FillDllwithoptional(LooksUpsRepository.ins.FillQuantityCode(), lstQtyUnitCode, Resources.Pages.TitleFiled, "Code");
            FillDllwithoptional(LooksUpsRepository.ins.fillUsedStatus(), lstStatusCode, Resources.Pages.TitleFiled, "Code");

            FillDllwithoptional_ALL(objRepository.fillItems(), lstPurchaseItems, "ItemNameArWithCode", "Code", "اختر ");


        }
        #endregion
        private void SaveInboundMaster()
        {

            string script = "";
            try
            {
                Inbound obj = new Inbound();
                if (ViewState["itemID"].Equals("0"))
                {//Save
                        obj.Serial = generateRequestSerial();
                        obj.TMonth = NullDateifEmpty(txtTransDate.Text).Month;
                        obj.TYear = NullDateifEmpty(txtTransDate.Text).Year;
                        obj.TransDate = NullDateifEmpty(txtTransDate.Text);
                        obj.InboundTypeCode = ZeroIntergerIFNull(lstInboundTypeCode.SelectedValue);
                        if (lstFromVendorCode.SelectedValue != "0")
                        {
                            obj.FromVendorCode = ZeroIntergerIFNull(lstFromVendorCode.SelectedValue);
                        }
                        obj.TargetLocationCode = ZeroIntergerIFNull(lstTargetLocationCode.SelectedValue);
                        obj.OwnerLocationCode = ZeroIntergerIFNull(lstOwnerLocationCode.SelectedValue);
                        obj.RefNo = txtRefNo.Text;
                        obj.RefDate = NullDateifEmpty(txtRefDate.Text);
                        obj.Notes = txtNotes.Text;
                        obj.DeliveryOrderNo = txtDeliveryOrderNo.Text;
                        obj.DeliveryDate = NullDateifEmpty(txtDeliveryDate.Text);
                        obj.DepositeNotes = gets(txtDepositeNotes.Text);
                        obj.Notes = gets(txtNotes.Text);
                        objRepository.AddInbound(obj);
                        hdnMasterID.Value = gets(obj.Code);
                        ViewState["itemID"] = gets(obj.Code);
                            // Save Documenting Status
                            InboundStatusTrack objStatus = new InboundStatusTrack();
                            if (ViewState["StatusTrackingitemID"].Equals("0"))
                            {//Save
                                objStatus.InboundCode = obj.Code;
                                objStatus.TransDate = DateTime.Now;
                                objStatus.Notes = "New Request Documenting Status";
                                objStatus.DepositeStatusTypeCode = 1;
                                objRepository.AddStatusTracking(objStatus);
                            }

                }
                else
                { //Update 

                        hdnMasterID.Value = ViewState["itemID"].ToString();
                        obj = objRepository.GetDetails(ZeroIntergerIFNull(ViewState["itemID"].ToString()));
                        obj.Serial = txtSerial.Text;
                        obj.TMonth = NullDateifEmpty(txtTransDate.Text).Month;
                        obj.TYear = NullDateifEmpty(txtTransDate.Text).Year;
                        obj.TransDate = NullDateifEmpty(txtTransDate.Text);
                        obj.InboundTypeCode = ZeroIntergerIFNull(lstInboundTypeCode.SelectedValue);
                        if (lstFromVendorCode.SelectedValue!="0")
                        {
                            obj.FromVendorCode = ZeroIntergerIFNull(lstFromVendorCode.SelectedValue);
                        }                   
                        obj.TargetLocationCode = ZeroIntergerIFNull(lstTargetLocationCode.SelectedValue);
                        obj.OwnerLocationCode = ZeroIntergerIFNull(lstOwnerLocationCode.SelectedValue);
                        obj.RefNo = txtRefNo.Text;
                        obj.RefDate = NullDateifEmpty(txtRefDate.Text);
                        obj.Notes = txtNotes.Text;
                        obj.DeliveryOrderNo = txtDeliveryOrderNo.Text;
                        obj.DeliveryDate = NullDateifEmpty(txtDeliveryDate.Text);
                        obj.DepositeNotes = gets(txtDepositeNotes.Text);
                        obj.Notes = gets(txtNotes.Text);
                        objRepository.UpdateInbound(obj);
                }
                    ViewState["itemID"] = obj.Code;
                    ClearForm();
                    FillInboundMasterInformation();
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
            //if (Request.QueryString["id"] != null)
            //{
            //    return "display:block";
            //}

            //return "display:none";

            return "display:block";
        }

        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void lstInboundTypeCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetInboundType();

        }

        protected void grdInboundItems_DeleteCommand(object source, DataGridCommandEventArgs e)
        {

            try
            {
                var InboundItemDetails = objRepository.getInboundItemDetails(ZeroIntergerIFNull(e.Item.Cells[0].Text));
                objRepository.DeleteItemsUnit(InboundItemDetails);


                string script = FormatpopupErrorMSG(Resources.Alerts.DataDeletedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }
            catch (Exception ex)
            {

                string script = FormatpopupErrorMSG(Resources.Alerts.SorryDeleteDataFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }


            FillInboundItems();
        }
    }
}

