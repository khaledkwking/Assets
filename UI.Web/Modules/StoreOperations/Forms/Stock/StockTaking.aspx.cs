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

namespace UI.Web.Modules.WHM.Forms
{
    public partial class StockTaking : BaseFormAdmin
    {
        #region "Page Members"
        public OutboundRepository objRepository = IoC.Resolve<OutboundRepository>();
        public InboundRepository InboundobjRepository = IoC.Resolve<InboundRepository>();
        public string _PageTitle = "Outbound Items ";

        #endregion

        #region "Page Events"

        protected void Page_PreRender(object sender, EventArgs e)
        {
          

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
           // btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
             if (!IsPostBack)
            {
                ViewState["SpEdit"] ="0";
                ViewState["NewDesc"] = "";
                ViewState["NewBar"] = "";
                ViewState["NewIsbn"] = "";
                ViewState["SPITEM"] = "";
                 ViewState["NewPrice"] = "0";
                Session["ItemList"] = null;
                //   FillDll(LooksUpsRepository.ins.FillConsignee(), lstConsignee, "FullNameEn", "Code");

                fillLookups();

                ViewState["OutboundItemID"] = "0";

                FillInboundItems();
                

            }

        }


        protected void btnSave_Click(object sender, System.EventArgs e)
        {
           // SaveItemInformation();
        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
           // this.ClearForm();
        }


        //}
        #endregion

        #region "Fill Information"


        private void FillInboundItems()
        {
            var objList = InboundobjRepository.Stocktakingreport(ReverseSerial(txtPArtOfName.Text),txtConsigneeRef.Text,
              ZeroIntergerIFNull(lstItemType.SelectedValue), ZeroIntergerIFNull(lstConsignee.SelectedValue), ZeroIntergerIFNull(lstGoodCategoryCode.SelectedValue),
                ZeroIntergerIFNull(lstQtyUnitCode.SelectedValue), ZeroIntergerIFNull(lstWeightUnitCode.SelectedValue), ZeroIntergerIFNull(lstLocationCode.SelectedValue) );

            lblInboundItemsCount.Text = (" Found total <b>" + (objList.Count.ToString() + "</b> records"));

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
                lnkReportPrint.Visible = true;
                // lnkBack.Visible = true;
                lnkReportPrint.Attributes.Add("class", "iframe btn btn-info btn-xs");

            }

            Session["StocktakingItems"] = objList;


            //var duplicatedList = objList.SelectMany(t =>
            //  Enumerable.Repeat(t,2)).ToList();
             
            grdInboundItems.DataSource = objList;
            grdInboundItems.DataBind();

            pager1.ItemCount = objList.Count;
           
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


    
        private void ClearForm()
        {
           

            ViewState["OutboundItemID"] = "0";

            lblSubTitle.Text = this.GetTitle(true);
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



        public string setmaterstyle()
        {
            //if (Request.QueryString["id"] != null)
            //{
            //    return "display:block";
            //}

            return "display:none";
        }


        private void fillLookups()
        {
            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillConsignee(1), lstConsignee, "FullNameEn", "Code","All");
            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillItemsTypes(), lstItemType, Resources.Pages.TitleFiled, "Code", "All");
            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillGoodCategory(), lstGoodCategoryCode, Resources.Pages.TitleFiled, "Code", "All");


            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillWeightUnitCode(), lstWeightUnitCode, Resources.Pages.TitleFiled, "Code", "All");

            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillQuantityCode(), lstQtyUnitCode, Resources.Pages.TitleFiled, "Code", "All");
            //  FillDll(LooksUpsRepository.ins.FillDepositeDeclaration(), lstDepositeDeclarationType, Resources.Pages.TitleFiled, "Code");

            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillLocation(), lstLocationCode, Resources.Pages.TitleFiled, "Code", "All");





        }
        #endregion

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            FillInboundItems();
        }

        protected void grdInboundItems_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            //if ((e.Item.ItemType == ListItemType.Item))
            //{
            //    // 
            //    HtmlImage im = ((HtmlImage)(e.Item.Cells[2].FindControl("imgControl")));
            //    string imname = im.ClientID;
            //    string rowindex = (e.Item.ItemIndex + 1).ToString();
            //    string rowID = e.Item.ClientID;
            //    im.Attributes.Add("onclick", ("ControlGrid(\'" + (imname + ("\'," + (rowindex + (",\'"  + (rowID + "\')")))))));
            //    //LinkButton lnk = ((LinkButton)(e.Item.Cells[0].Controls[0]));
            //    //lnk.Attributes.Add("onclick", "return confirm(\'Are you sure you want to delete this Invoice?\');");
            //}
            //else if ((e.Item.ItemType == ListItemType.AlternatingItem))
            //{
            //    string rowID = e.Item.ClientID;
            //    string code = e.Item.Cells[3].Text;
            //    string ItemType = e.Item.Cells[4].Text;
            //    //SqlDataReader dr = SellMaster.ins.getInvoiceItemsReader(code);

            //    var objUnitList = objRepository.FillItemUnits(ZeroIntergerIFNull(code));
            //    if (objUnitList!=null)
            //    {
            //        DataGrid grd = ((DataGrid)(e.Item.Cells[1].FindControl("grdUnits")));
            //        grd.DataSource = objUnitList;
            //        grd.DataBind();


            //        switch (ItemType)
            //        {
            //            case "1"://Cargo


            //                grd.Columns[1].Visible = true;
            //                grd.Columns[2].Visible = true;


            //                grd.Columns[3].Visible = false;
            //                grd.Columns[4].Visible = false;
            //                grd.Columns[5].Visible = false;
            //                grd.Columns[6].Visible = false;


            //                break;
            //            case "2"://Container
            //                grd.Columns[1].Visible = true;
            //                grd.Columns[2].Visible = true;

            //                grd.Columns[3].Visible = false;
            //                grd.Columns[4].Visible = false;
            //                grd.Columns[5].Visible = false;
            //                grd.Columns[6].Visible = false;



            //                break;
            //            case "3"://Vehicle 7-11

            //                grd.Columns[1].Visible = false;
            //                grd.Columns[2].Visible = false;

            //                grd.Columns[3].Visible = true;
            //                grd.Columns[4].Visible = true;
            //                grd.Columns[5].Visible = true;
            //                grd.Columns[6].Visible = true;

            //                break;
            //            default:
            //                break;
            //        }


            //    }

              



            //    for (int i = 2; i<= (e.Item.Cells.Count - 1); i++)
            //    {
            //        e.Item.Cells[i].Visible = false;
            //    }

            //    e.Item.Cells[0].Controls[0].Visible = false;
            //    e.Item.Cells[1].Attributes.Add("colspan", ((e.Item.Cells.Count - 2)).ToString());
            //    e.Item.Attributes.Add("style", "display:none");
            //}

            //if (!(e.Item.ItemType == ListItemType.AlternatingItem))
            //{
            //    e.Item.Cells[1].Visible = false;
            //}
        }
 

        
         
    }
}