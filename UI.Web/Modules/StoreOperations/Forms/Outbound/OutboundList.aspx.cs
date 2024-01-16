using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.StoreOperations.Forms.Outbound
{
    public partial class OutboundList : BaseFormAdmin
    {
        public string _PageTitle = Resources.Pages.OutboundList;
        public OutboundRepository objRepository = IoC.Resolve<OutboundRepository>();

        #region "From Events"

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.FillGrid();
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            if (!IsPostBack)
            {

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

                Infrastructure.DAL.Model.DB.Outbound obj = new Infrastructure.DAL.Model.DB.Outbound();
                for (int i = 0; i <= grdData.Items.Count - 1; i++)
                {

                    if ((grdData.Items[i].FindControl("chkItem") != null))
                    {
                        CheckBox check = (CheckBox)grdData.Items[i].FindControl("chkItem");

                        if (check.Checked)
                        {
                            objRepository.DeleteOutbound((Infrastructure.DAL.Model.DB.Outbound)objRepository.GetDetails(ZeroIntergerIFNull(grdData.Items[i].Cells[3].Text)));
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

        protected void lnkQuick_Click(object sender, EventArgs e)
        {
            FillGrid();

        }

        protected void grdData_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {

            if ((e.Item.ItemType == ListItemType.Item))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#f2d575\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#FFFFFF\';");
            }

            //if ((e.Item.ItemType == ListItemType.AlternatingItem))
            //{
            //    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#f2d575\';");
            //    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#FFFFFF\';");
            //}


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

                var objUnitList = objRepository.FillOutboundItems(ZeroIntergerIFNull(Filecode));
                if (objUnitList != null)
                {
                    DataGrid grd = ((DataGrid)(e.Item.Cells[1].FindControl("grdOutboundItems")));
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
        public string showRequestStatus(int requestStatusId, string StatusText){
            string _out = "";
            switch (requestStatusId)
            {
                case 1: {
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
        private void fillLookups()
        {
            if (Session["OraEmpList"] != null)
            {
                FillDllwithoptional((List<EmployeeViewModel>)Session["OraEmpList"], lstFilterRefEmployee, "EMP_NAME", "EMP_ID");
            }
            else
            {
                // Request Data From Ora.
                var Emplist = GetOraEmpList(1);
                Session["OraEmpList"] = Emplist;
                FillDllwithoptional_ALL(Emplist, lstFilterRefEmployee, "EMP_NAME", "EMP_ID", Resources.Pages.all);
            }


        }
        private void FillGrid()
        {
            var objlist = objRepository.GetCustodyList(txtFilterSerial.Text, NullDateifEmpty(txtTransDate.Text), NullDateifEmpty(txtTransactionDateTo.Text), ZeroIntergerIFNull(lstFilterOutType.SelectedValue), ZeroIntergerIFNull(lstFilterRefEmployee.SelectedValue), 0, 0, txtRefNo.Text);

            Session["OutboundListResult"] = objlist;

            var duplicatedList = objlist.SelectMany(t =>
                 Enumerable.Repeat(t, 2)).ToList();

            lblcount.Text = (Resources.Utilities.foundTotal + (objlist.Count.ToString()).ToString() + Resources.Utilities.records);
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
        #endregion

    }
}