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

namespace UI.Web.Modules.StoreOperations.Forms.Outboud
{
    public partial class OutboundItemDelivery : BaseFormAdmin
    {
        #region "Page Members"
     
        public OutboundRepository  objRepository = IoC.Resolve<OutboundRepository>();
        public string _PageTitle = Resources.Pages.OutboundItemDelivery  ;

        #endregion

        #region "Page Events"

        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "OutboundItemDelivery.aspx";
        }
        
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            btnSave.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {
                divShow.Visible = false;
                if (Request.QueryString["serial"]!=null)
                {
                    txtFilterSerial.Text = Request.QueryString["serial"];
                    FillOutboundItems();

                }
                ViewState["SpEdit"] ="0";
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
        private void FillOutboundItems()
        {
            divShow.Visible = true;
            FillOutboundMasterInformation();

            var objList = objRepository.FillOutboundItemsByRequest(txtFilterSerial.Text  );
            lblcount.Text = (" Found total <b>" + (objList.Count.ToString() + "</b> records"));

            decimal c = System.Math.Ceiling(Convert.ToDecimal(objList.Count / grdOutboundItems.PageSize));
            if ((c <= grdOutboundItems.CurrentPageIndex))
            {
                grdOutboundItems.CurrentPageIndex = 0;
            }

            //List Duplication
            //List<View_OutboundItems> duplicatedList = new List<View_OutboundItems>();
            //duplicatedList = DuplicatedList(objList);

            if (objList.Count > 0)
            {
                btnSave.Visible = true;
                
            }
            else
            {
               string  script = FormatpopupErrorMSG("No Item Found ", "2");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }

            //var duplicatedList = objList.SelectMany(t =>
            //  Enumerable.Repeat(t,2)).ToList();
             
            grdOutboundItems.DataSource = objList;
            grdOutboundItems.DataBind();

            pager1.ItemCount = objList.Count;

        }


        private void FillOutboundMasterInformation()
        {

            var objList = objRepository.FillOutboundMasterbySerial(txtFilterSerial.Text);
            if ((objList != null))
            {


                lblSerial.Text = gets(objList.Serial);
                lblOutboundType.Text = gets(objList.TypeTitlear);
                lblTransDate.Text = objList.TransDate.Value.ToString("MM/dd/yyyy");
                lblEmpName.Text = gets(objList.EmpName);
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

            if ((currnetPageIndx > grdOutboundItems.PageCount))
            {
                currnetPageIndx = (grdOutboundItems.PageCount - 1);
            }

            pager1.CurrentIndex = currnetPageIndx;
            grdOutboundItems.CurrentPageIndex = (currnetPageIndx - 1);
            FillOutboundItems();
        }







        #endregion

        #region "Helper Methods"

        #endregion

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            FillOutboundItems();
        }

        protected void grdOutboundItems_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
           
        
 
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            string script = "";

            try
            {


                List<Outbound_Items> objList = new List<Outbound_Items>();
                ArrayList UnitsList = new ArrayList();
                string OutboundMasterID = gets(grdOutboundItems.Items[0].Cells[1].Text);
                for (int i = 0; i < grdOutboundItems.Items.Count ; i++)
                {
                    if (ZeroIFNull(((TextBox)grdOutboundItems.Items[i].FindControl("txtQty")).Text) > 0)
                    {


                        // Update Item Actual Quanonty
                        Outbound_Items obj = new Outbound_Items();
                        obj = objRepository.GetOutboundItemDetails(ZeroIntergerIFNull(grdOutboundItems.Items[i].Cells[0].Text));
                        obj.DeliveredQry = ZeroIFNull(((TextBox)grdOutboundItems.Items[i].FindControl("txtQty")).Text);

                        objList.Add(obj);
                    }
                }


                //Check operation Status ' Out Bound Insert or uPDATE


                for (int i = 0; i < objList.Count; i++)
                {
                    // Upodate Item Information??????????????/
                    objRepository.UpdateOutboundItems(objList[i]);
                }

                //Update Outbound Status
                if (OutboundMasterID != "")
                {

                    //var ObjOutboundDetails = objRepository.GetDetails(ZeroIntergerIFNull( Request.QueryString["id"].ToString()));
                    //ObjOutboundDetails.sts
                    //objRepository.UpdateOutbound(ObjOutboundDetails);


                    // Save Documenting Status

                    OutboundStatusTrack objStatus = new OutboundStatusTrack();


                    objStatus.OutboundCode = ZeroIntergerIFNull(OutboundMasterID);
                    objStatus.TransDate = DateTime.Now;
                    objStatus.Notes = "Item  Delivery";
                    objStatus.WithdrawStatusTypeCode = 2;
                    objRepository.AddStatusTracking(objStatus);



                }



                FillOutboundItems();

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
            { Response.Redirect("OutboundOperrations.aspx?id="+ Request.QueryString["id"].ToString()); }
            else
            { Response.Redirect("OutboundOperrations.aspx"); }
               
        }

        protected void grdUnits_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType==ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
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
            FillOutboundItems();


        }
    }
}