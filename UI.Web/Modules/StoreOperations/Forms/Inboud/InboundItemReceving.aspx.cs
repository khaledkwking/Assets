using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.StoreOperations.Forms.Inboud
{
    public partial class InboundItemReceving : BaseFormAdmin
    {
        #region "Page Members"

        public InboundRepository objRepository = IoC.Resolve<InboundRepository>();
        public AssetsRepository  assetsRepository = IoC.Resolve<AssetsRepository>();
        public ItemRepository ItemRepository = IoC.Resolve<ItemRepository>();
        public string _PageTitle = Resources.Pages.InboundItemsReceving;

        #endregion
        #region "Page Events"
        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "InboundItemReceving.aspx";
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            btnSave.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {
                divShow.Visible = false;
                if (Request.QueryString["serial"] != null)
                {
                    txtFilterSerial.Text = Request.QueryString["serial"];
                    FillInboundItems();

                }
                ViewState["SpEdit"] = "0";
                ViewState["NewDesc"] = "";
                ViewState["NewBar"] = "";
                ViewState["NewIsbn"] = "";
                ViewState["SPITEM"] = "";
                ViewState["NewPrice"] = "0";
                Session["ItemList"] = null;

                if (Request.QueryString["id"] == null)
                {

                    ViewState["itemID"] = "0";
                }
                else
                {
                    ViewState["itemID"] = Request.QueryString["id"].ToString();

                }
                ViewState["OutboundItemID"] = "0";
            }

        }
        #endregion
        #region "Fill Information"
        private void FillInboundItems()
        {
            divShow.Visible = true;
            FillInboundMasterInformation();

            var objList = objRepository.FilterInboundItems(txtFilterSerial.Text);
            lblcount.Text = (" Found total <b>" + (objList.Count.ToString() + "</b> records"));

            decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdInboundItems.PageSize));
            if ((c <= grdInboundItems.CurrentPageIndex))
            {
                grdInboundItems.CurrentPageIndex = 0;
            }

            //List Duplication
            //List<View_InboundItems> duplicatedList = new List<View_InboundItems>();
            //duplicatedList = DuplicatedList(objList);

            if (objList.Count > 0)
            {
                btnSave.Visible = true;

            }
            else
            {
                string script = FormatpopupErrorMSG("No Item Found ", "2");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }

            //var duplicatedList = objList.SelectMany(t =>
            //  Enumerable.Repeat(t,2)).ToList();

            grdInboundItems.DataSource = objList;
            grdInboundItems.DataBind();

            pager1.ItemCount = objList.Count;

        }
        private void FillInboundMasterInformation()
        {

            var objList = objRepository.getInboundMasterBySerial(txtFilterSerial.Text);
            if ((objList != null))
            {


                lblSerial.Text = gets(objList.Serial);
                lblInboundType.Text = gets(objList.TypeTitleAr);
                lblTransDate.Text = objList.TransDate.Value.ToString("MM/dd/yyyy");
                lblVendorNameEn.Text = gets(objList.VendorNameAr);
                lblRefNo.Text = gets(objList.RefNo);
                lblLocationNameAr.Text = gets(objList.LocationNameAr);
                txtRefDate.Text = objList.RefDate.Value.ToString("MM/dd/yyyy");



                //lblDepositeNotes.Text = gets(objList.DepositeNotes);
                //lblNotes.Text = gets(objList.Notes);
            }

            //tblAdd.Visible = true;
            //lblSubTitle.Text = this.GetTitle(false);

        }
        protected void pager_Command(object sender, CommandEventArgs e)
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
            FillInboundItems();
        }

        #endregion
        #region "Helper Methods"

        #endregion

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            FillInboundItems();
        }

        protected void grdInboundItems_ItemDataBound(object sender, DataGridItemEventArgs e)
        {



        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            string script = "";

            try
            {


                List<AssetsItemUnit> objList = new List<AssetsItemUnit>();
                ArrayList UnitsList = new ArrayList();
                string InboundMasterID = gets(grdInboundItems.Items[0].Cells[1].Text);
                int StoreId = ZeroIntergerIFNull(grdInboundItems.Items[0].Cells[2].Text);
                for (int i = 0; i < grdInboundItems.Items.Count; i++)
                {
                    if (ZeroIFNull(((TextBox)grdInboundItems.Items[i].FindControl("txtQty")).Text) > 0)
                    {

                        // Update Item Actual Quanonty
                        AssetsItemUnit obj = new AssetsItemUnit();
                        obj = objRepository.GetInboundItemDetails(ZeroIntergerIFNull(grdInboundItems.Items[i].Cells[0].Text));
                        obj.ReceivedQty = ZeroIFNull(((TextBox)grdInboundItems.Items[i].FindControl("txtQty")).Text);


                        //// Add Tracking
                        //AssetsEventTracking eventobj = new AssetsEventTracking();
                        //eventobj.AssetCode = obj.Code;
                        //eventobj.ActionDate = DateTime.Now;
                        //eventobj.actionId = 1;// Active
                        //eventobj.statusId = 1;// Available
                        //eventobj.ToLocationId = StoreId;
                        //eventobj.Notes = "Purchase Receiving Action";
                        //eventobj.CreatedAt = DateTime.Now;
                        //eventobj.CreatedBy = ZeroIntergerIFNull(gets(ReadSession("userid")));
                        //assetsRepository.AddEventTracking(eventobj);


                        // Update Asset Last Enent
                        //obj.LastEventTrackingId = eventobj.Code;

                        obj.LastModifiedAt = DateTime.Now;
                        obj.LastModifiedBy = ZeroIntergerIFNull(gets(ReadSession("userid")));
                        //obj.= 2;

                        objRepository.UpdateItemunit(obj);


                        //TODO
                        //Update Item Base Price on Reciving Items
                        var itemMasterObj = objRepository.getItemCardDetails(obj.ItemCode.Value);
                        if (itemMasterObj!=null)
                        {
                            itemMasterObj.ItemBasePrice = obj.EstimatedUnitCost;
                            itemMasterObj.LastModifiedAt = DateTime.Now;
                            itemMasterObj.LastModifiedBy = ZeroIntergerIFNull(gets(ReadSession("userid")));
                            ItemRepository.Update(itemMasterObj);

                        }
                        //TODO
                        //Update DepositeStatusTypeCode
                        //var _InboundMasterIDObj = objRepository.FillInboundItems(Convert.ToInt32(InboundMasterID));
                        var _DepositeStatusTypeCodeObj = objRepository.GetInboundStatusDetails(Convert.ToInt32(InboundMasterID));
                        if (_DepositeStatusTypeCodeObj != null)
                        {
                            _DepositeStatusTypeCodeObj.DepositeStatusTypeCode = 2 ; // Goods Arrival

                            objRepository.UpdateStatusTracking(_DepositeStatusTypeCodeObj);

                        }



                    }
                }


                //Check operation Status ' Out Bound Insert or uPDATE


                ////Update Inbound Status
                //if (InboundMasterID != "")
                //{

                //    //var ObjInboundDetails = objRepository.GetDetails(ZeroIntergerIFNull( Request.QueryString["id"].ToString()));
                //    //ObjInboundDetails.sts
                //    //objRepository.UpdateInbound(ObjInboundDetails);


                //    // Save Documenting Status

                //    InboundStatusTrack objStatus = new InboundStatusTrack();


                //    objStatus.InboundCode = ZeroIntergerIFNull(InboundMasterID);
                //    objStatus.TransDate = DateTime.Now;
                //    objStatus.Notes = "Good Receiving";
                //    objStatus.DepositeStatusTypeCode = 2;
                //    objRepository.AddStatusTracking(objStatus);



                //}



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




        protected void lnkBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            { Response.Redirect("OutboundOperrations.aspx?id=" + Request.QueryString["id"].ToString()); }
            else
            { Response.Redirect("OutboundOperrations.aspx"); }

        }

        protected void grdUnits_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (gets(e.Item.Cells[9].Text).Equals("3"))
                {
                    ((CheckBox)e.Item.FindControl("chkItem")).Visible = false;
                }

                if (gets(e.Item.Cells[9].Text).Equals("1"))
                {
                    ((CheckBox)e.Item.FindControl("chkItem")).Checked = true;
                }

            }
        }

        protected void lnkQuick_Click(object sender, EventArgs e)
        {
            FillInboundItems();


        }
    }
}