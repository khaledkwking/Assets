using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.StoreOperations.Forms.Inboud
{
    public partial class InboundList : BaseFormAdmin
    {
        public string _PageTitle = Resources.Pages.InboundList;
        public InboundRepository objRepository = IoC.Resolve<InboundRepository>();




        #region "From Events"

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.FillGrid();
        }


        protected void btnNew_Click1(object sender, EventArgs e)
        {
            Response.Redirect("InboundOperrations.aspx");
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            //btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
            //btnSave.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {
                //if ((Request.UrlReferrer == null))
                //{
                //    Response.Redirect("/admin/pages/main.aspx");
                //}
                fillLookups();
                ViewState["Item"] = 0;
                FillGrid();
            }

        }
        protected void pager_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            Int32 currnetPageIndx = ((Int32)(e.CommandArgument));
            if ((currnetPageIndx <= 0))
            {
                currnetPageIndx = 1;
            }

            if ((currnetPageIndx > grdData.PageCount))
            {
                currnetPageIndx = (grdData.PageCount - 1);
            }


            grdData.CurrentPageIndex = (currnetPageIndx - 1);
            this.FillGrid();
        }

        protected void grdData_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            //string id = e.Item.Cells[0].Text;
            //clsLookup.ins.Delete(TargetTableName, id);
            //this.FillGrid();
        }

        protected void btnDelete_Click(object sender, System.EventArgs e)
        {

            try
            {

                Inbound obj = new Inbound();
                for (int i = 0; i <= grdData.Items.Count - 1; i++)
                {

                    if ((grdData.Items[i].FindControl("chkItem") != null))
                    {
                        CheckBox check = (CheckBox)grdData.Items[i].FindControl("chkItem");

                        if (check.Checked)
                        {
                            objRepository.DeleteInbound((Inbound)objRepository.GetDetails(ZeroIntergerIFNull(grdData.Items[i].Cells[3].Text)));
                        }
                    }
                }
                FillGrid();

            }
            catch (Exception ex)
            {


                string script = FormatpopupErrorMSG(Resources.Alerts.SorryDeleteDataFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }


        }

        protected void grdData_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            //  string id = e.Item.Cells[0].Text;
            //  this.ClearForm();
            //  ViewState["Item"] = id;
            ////  this.FillForm();
            //  tblshow.Visible = false;
            //  tblAdd.Visible = true;
        }

        protected void grdData_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {

            if ((e.Item.ItemType == ListItemType.Item))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#d5c08e6e\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#FFFFFF\';");
            }

           

            if ((e.Item.ItemType == ListItemType.Item))
            {
                //
                HtmlImage im = ((HtmlImage)(e.Item.Cells[2].FindControl("imgControl")));
                string imname = im.ClientID;
                string rowindex = (e.Item.ItemIndex + 1).ToString();
                string rowID = e.Item.ClientID;
                im.Attributes.Add("onclick", ("ControlGrid(\'" + (imname + ("\'," + (rowindex + (",\'" + (rowID + "\')")))))));
                //LinkButton lnk = ((LinkButton)(e.Item.Cells[0].Controls[0]));
                //lnk.Attributes.Add("onclick", "return confirm(\'Are you sure you want to delete this Invoice?\');");




            }
            else if ((e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string rowID = e.Item.ClientID;
                string Filecode = e.Item.Cells[3].Text;

                var objUnitList = objRepository.FillInboundItems(ZeroIntergerIFNull(Filecode));
                if (objUnitList != null)
                {
                    DataGrid grd = ((DataGrid)(e.Item.Cells[1].FindControl("grdInboundItems")));
                    grd.DataSource = objUnitList;
                    grd.DataBind();


                }


                for (int i = 2; i <= (e.Item.Cells.Count - 1); i++)
                {
                    e.Item.Cells[i].Visible = false;
                }

                e.Item.Cells[0].Controls[0].Visible = false;
                e.Item.Cells[1].Attributes.Add("colspan", ((e.Item.Cells.Count - 2)).ToString());
                e.Item.Attributes.Add("style", "display:none");
                e.Item.Cells[0].Visible = false;
            }

            if (!(e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Cells[1].Visible = false;


            }




        }
        #endregion

        #region "Fill Infotmation"
        private void ClearForm()
        {


            ViewState["Item"] = 0;

        }
        private void fillLookups()
        {
            FillDllwithoptional_ALL(LooksUpsRepository.ins.FillInboundTypes(), lstInboundType, "TitleAr", "Code", "All");




        }
        private void FillGrid()
        {
            var objlist = objRepository.GetList(txtFilterSerial.Text, NullDateifEmpty(txtTransDate.Text), NullDateifEmpty(txtTransactionDateTo.Text), ZeroIntergerIFNull(lstInboundType.SelectedValue), 0, 0, 0, txtRefNo.Text, "", txtDeliveryOrder.Text, 0, 0);

            Session["InboundListResult"] = objlist;

            var duplicatedList = objlist.SelectMany(t =>
                 Enumerable.Repeat(t, 2)).ToList();

            lblcount.Text = (Resources.Utilities.foundTotal + " (" + (objlist.Count.ToString()).ToString() + ") " + Resources.Utilities.records);
            decimal c = System.Math.Ceiling(Convert.ToDecimal(objlist.Count / grdData.PageSize));
            if ((c <= grdData.CurrentPageIndex))
            {
                grdData.CurrentPageIndex = 0;
            }



            grdData.DataSource = duplicatedList;
            grdData.DataBind();




            int _totalCount = objlist.Count;
            pager1.ItemCount = objlist.Count;



        }
        private string GetTitle(bool isadd)
        {
            if (isadd)
            {
                return "Add New Record Information";
            }
            else
            {
                return "Edit Record Information";
            }

        }

        public string showRequestStatus(int requestStatusId, string StatusText)
        {
            string _out = "";
            switch (requestStatusId)
            {
                case 1:
                    {
                        _out = "<span class='badge badge-dot badge-warning'>" + StatusText + "</span>";
                        break;
                    }
                case 2:
                    {
                        _out = "<span class='badge badge-dot badge-primary'>" + StatusText + "</span>";
                        break;
                    }
                default:
                    _out = "<span class='badge badge-dot badge-secondary'>" + StatusText + "</span>";
                    break;
            }


            return _out;
        }

        public string showRequestType(int TypeCode, string TypeText)
        {
            string _out = "";
            switch (TypeCode)
            {
                case 1:
                    {
                        _out = "<span class='badge badge-dim badge-success'><em class='icon ni ni-arrow-up-left'></em><span>" + TypeText + "</span></span>";
                        break;
                    }
                case 2:
                    {
                        _out = "<span class='badge badge-dim badge-light text-warning'><em class='icon ni ni-exchange'></em><span>" + TypeText + "</span></span>";
                        break;
                    }
                case 3:
                    {
                        _out = "<span class='badge badge-dim badge-light text-secondary'><em class='icon ni ni-shrink'></em><span>" + TypeText + "</span></span>";
                        break;
                    }
                default:
                    _out = "";
                    break;
            }


            return _out;
        }


        #endregion

        //private void FillForm()
        //{
        //    DataSet ds = clsLookup.ins.GetDetails(TargetTableName, ViewState["Item"].ToString());
        //    if ((ds.Tables[0].Rows.Count > 0))
        //    {


        //        txtTitleAr.Text = gets(ds.Tables[0].Rows[0]["TitleAr"]);
        //        txttitleAr.Text = gets(ds.Tables[0].Rows[0]["TitleAr"]);

        //    }

        //    tblAdd.Visible = true;
        //    lblSubTitle.Text = this.GetTitle(false);

        //}

        //private string getImage(ref FileUpload txtFile)
        //{
        //    string imgname="";
        //    string temp;
        //    string ext;
        //    int inx;
        //    int i;
        //    string RandChar;
        //    string ValueString;
        //  //  Microsoft.VisualBasic.VBMath.Randomize();
        //    imgname = "";
        //    ValueString = "";
        //    if (!(txtFile.PostedFile == null))
        //    {
        //        if ((txtFile.PostedFile.FileName != ""))
        //        {
        //            imgname = txtFile.PostedFile.FileName;
        //            imgname = imgname.Substring((imgname.LastIndexOf("\\") + 1));
        //            inx = imgname.LastIndexOf(".");
        //            temp = imgname.Substring(0, inx);
        //            ext = imgname.Substring((inx + 1));
        //            for (i = 1; (i <= 16); i++)
        //            {
        //                //  RandChar = (string)(Microsoft.VisualBasic.Conversion.Int((26 * Microsoft.VisualBasic.VBMath.Rnd() + 65)).ToString());

        //                RandChar = (new Random().Next(i, 16)).ToString();
        //                ValueString += RandChar;
        //            }

        //            imgname = (ValueString + ("." + ext));
        //            txtFile.PostedFile.SaveAs(Server.MapPath(("/Layout/uploads/Adminprofile/" + imgname)));
        //        }

        //    }


        //    //////string imgname = "";
        //    //////int inx = 0;
        //    //////string temp = "";
        //    //////string ext = "";
        //    //////string RandChar = "";
        //    //////string ValueString = "";
        //    //////Microsoft.VisualBasic.VBMath.Randomize();
        //    //////imgname = "";
        //    //////imgname = txtImage.PostedFile.FileName;
        //    //////imgname = imgname.Substring((imgname.LastIndexOf("\\") + 1));
        //    //////inx = imgname.LastIndexOf('.');
        //    //////temp = imgname.Substring(0, inx);
        //    //////ext = imgname.Substring((inx + 1));
        //    //////for (int i = 1; (i <= 10); i++)
        //    //////{
        //    //////    RandChar = (string)(Microsoft.VisualBasic.Conversion.Int((26 * Microsoft.VisualBasic.VBMath.Rnd() + 65)).ToString());
        //    //////    ValueString += RandChar;
        //    //////}
        //    //////imgname = (temp + (ValueString + ("." + ext)));
        //    //////return imgname;
        //    return imgname;
        //}


        protected void lnkQuick_Click(object sender, EventArgs e)
        {
            FillGrid();

        }
    }
}