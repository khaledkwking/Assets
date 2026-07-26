using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using QRCoder;
using UI.Web.Admin.Controller;
using UI.Web.Helper;

namespace UI.Web.Modules.MasterData
{
    public partial class AssetsRequestList : BaseFormAdmin
    {
        #region "Page Members"
        public AssetsRepository objRepository = IoC.Resolve<AssetsRepository>();
        public string _PageTitle = "سجل استمارات العهد (الشخصية / التنظيمة)";

        #endregion

        #region "Page Events"
        protected void Page_PreInit(object sender, EventArgs e)
        {
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {


            if (!IsPostBack)
            {
                //if ((Request.UrlReferrer == null))
                //{
                //    Response.Redirect("/admin/pages/main.aspx");
                //}
                //txtTransDate.Text = DateTime.Now.AddYears(-1).ToString();
                //txtTransactionDateTo.Text = DateTime.Now.ToString();
                ViewState["itemID"] = "0";
                fillLookups();
                FillGrid();
            }

        }




        protected void grdData_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {

            if ((e.Item.ItemType == ListItemType.Item))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#d5c08e6e\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#FFFFFF\';");
            }

            if ((e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#d5c08e6e\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#FFFFFF\';");
            }


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {


                    var dataItem = e.Item.DataItem;
                    int requestType = ZeroIntergerIFNull(gets(DataBinder.Eval(dataItem, "RequestActionType")));
                    if (requestType != 2)
                    {
                        int empRefCode = Convert.ToInt32(DataBinder.Eval(dataItem, "Ora_EmpRefCode"));

                        string empName = ZeroIntergerIFNull(gets(DataBinder.Eval(dataItem, "EmpRefCode"))) == 0
                            ? ""
                            : (ZeroIntergerIFNull(gets(DataBinder.Eval(dataItem, "Ora_EmpRefCode"))) == 0
                                ? gets(DataBinder.Eval(dataItem, "EmpName"))
                                : gets(DataBinder.Eval(dataItem, "Ora_EmpName")));

                        string code = gets(DataBinder.Eval(dataItem, "Code"));

                        // توليد HTML للـ badge مع ID فريد
                        string html = string.Format("{0} <span id='empBadge_{1}_{2}' class='badge badge-dim badge-secondary'>جاري التحميل...</span>", empName, empRefCode, code);

                        // ربط الـ Literal
                        Literal lit = (Literal)e.Item.FindControl("litEmpNameBadge");
                        if (lit != null)
                        {
                            lit.Text = html;
                        }
                    }
                    else if (requestType == 2)
                    {
                        int? empRefCode = 0;
                        int emp_Id = Convert.ToInt32(DataBinder.Eval(dataItem, "OrgEmpRefCode"));

                        if (emp_Id != 0)
                        {
                            using (var db = new AssetsEntitiesNew())
                            {
                                int? x = emp_Id;
                                var emp = db.Employee_tbl.FirstOrDefault(o => o.Emp_Id == x);
                                empRefCode = emp.Ora_EmpRefCode;
                            }
                        }
                        string empName = gets(DataBinder.Eval(dataItem, "OrgEmpName"));
                        string code = gets(DataBinder.Eval(dataItem, "Code"));
                        // توليد HTML للـ badge مع ID فريد
                        string html = string.Format("{0} <span id='empBadge_{1}_{2}' class='badge badge-dim badge-secondary'>جاري التحميل...</span>", empName, empRefCode, code);

                        // ربط الـ Literal
                        Literal lit = (Literal)e.Item.FindControl("litEmpNameBadge");
                        if (lit != null)
                        {
                            lit.Text = html;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                //if ((e.Item.ItemType == ListItemType.Item))
                //{
                //    //
                //    HtmlImage im = ((HtmlImage)(e.Item.Cells[2].FindControl("imgControl")));
                //    string imname = im.ClientID;
                //    string rowindex = (e.Item.ItemIndex + 1).ToString();
                //    string rowID = e.Item.ClientID;
                //    im.Attributes.Add("onclick", ("ControlGrid(\'" + (imname + ("\'," + (rowindex + (",\'" + (rowID + "\')")))))));
                //    //LinkButton lnk = ((LinkButton)(e.Item.Cells[0].Controls[0]));
                //    //lnk.Attributes.Add("onclick", "return confirm(\'Are you sure you want to delete this Invoice?\');");




                //}
                //else if ((e.Item.ItemType == ListItemType.AlternatingItem))
                //{
                //    string rowID = e.Item.ClientID;
                //    string Filecode = e.Item.Cells[3].Text;

                //    var objUnitList = objRepository.getRequestAssets(ZeroIntergerIFNull(Filecode));
                //    if (objUnitList != null)
                //    {
                //        DataGrid grd = ((DataGrid)(e.Item.Cells[1].FindControl("grdItems")));
                //        grd.DataSource = objUnitList;
                //        grd.DataBind();


                //    }


                //    for (int i = 2; i <= (e.Item.Cells.Count - 1); i++)
                //    {
                //        e.Item.Cells[i].Visible = false;
                //    }

                //    e.Item.Cells[0].Controls[0].Visible = false;
                //    e.Item.Cells[1].Attributes.Add("colspan", ((e.Item.Cells.Count - 2)).ToString());
                //    e.Item.Attributes.Add("style", "display:none");
                //    e.Item.Cells[0].Visible = false;
                //}

                if (!(e.Item.ItemType == ListItemType.AlternatingItem))
                {
                    e.Item.Cells[1].Visible = false;


                }


            }

        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.FillGrid();
        }

        #endregion

        #region "Fill Information"
        private void FillGrid()
        {
            var objlist = objRepository.getAssetsRequestList(txtPartOfName.Text,ZeroIntergerIFNull(lstFilterAction.SelectedValue), 
                NullDateifEmptyNew(txtTransDate.Text), NullDateifEmptyNew(txtTransactionDateTo.Text), ZeroIntergerIFNull(lstToLocation.SelectedValue), 0,0,ZeroIntergerIFNull(lstFilterEmpStatus.SelectedValue), txtItemDesc.Text);
            //var duplicatedList = objlist.SelectMany(t =>
            //     Enumerable.Repeat(t, 2)).ToList();

            lblcount.Text = (Resources.Utilities.foundTotal + " (" + (objlist.Count.ToString()).ToString() + ") " + Resources.Utilities.records);
            lblCountTop.Text = objlist.Count.ToString();
            decimal c = System.Math.Ceiling(Convert.ToDecimal(objlist.Count / grdAssets.PageSize));
            if ((c <= grdAssets.CurrentPageIndex))
            {
                grdAssets.CurrentPageIndex = 0;
            }

            grdAssets.DataSource = objlist; //;
            grdAssets.DataBind();

            //int _totalCount = objlist.Count;
            pager1.ItemCount = objlist.Count;



        }
        private void fillLookups()
        {
            var LocationsList = LooksUpsRepository.ins.FillLocationTree(0);
            FillDllwithoptional(LocationsList, lstToLocation, "path", "Code");
            //FillDllwithoptional_ALL(LooksUpsRepository.ins.FillItemCategory(), lstFilterCategory, Resources.Pages.TitleFiled, "Code", Resources.Pages.all);

            //FillDllwithoptional_ALL(LooksUpsRepository.ins.fillCategoryItems(ZeroIntergerIFNull(lstFilterCategory.SelectedValue)), lstFilterItem, "ItemNameAr", "Code", Resources.Pages.all);


            //FillDllwithoptional_ALL(LooksUpsRepository.ins.Fillvendor(), lstFilterVendor, "VendorNameAr", "Code", Resources.Pages.all);
            //FillDllwithoptional_ALL(LooksUpsRepository.ins.FillAssetsTrackingActions(), lstFilterAction, Resources.Pages.TitleFiled, "Code", Resources.Pages.all);
            //FillDllwithoptional_ALL(LooksUpsRepository.ins.FillTrackingStatus(), lstFilterSatus, Resources.Pages.TitleFiled, "Code", Resources.Pages.all);

            //FillDllwithoptional(LooksUpsRepository.ins.FillLocation(), lstFilterLocation, "LocationNameAr", "Code");

            //if (Session["OraEmpList"] != null)
            //{
            //    FillDllwithoptional((List<EmployeeViewModel>)Session["OraEmpList"], lstfilterEmployee, "EMP_NAME", "EMP_ID");
            //}
            //else
            //{
            //    var Emplist = GetOraEmpList(1);
            //    Session["OraEmpList"] = Emplist;
            //    FillDllwithoptional(Emplist, lstfilterEmployee, "EMP_NAME", "EMP_ID");
            //}

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

            if ((currnetPageIndx > grdAssets.PageCount))
            {
                currnetPageIndx = (grdAssets.PageCount - 1);
            }

            pager1.CurrentIndex = currnetPageIndx;
            grdAssets.CurrentPageIndex = (currnetPageIndx - 1);
            FillGrid();
        }
        #endregion

        protected void lnkQuick_Click(object sender, EventArgs e)
        {
            this.FillGrid();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                D_ItemCard obj = new D_ItemCard();
                for (int i = 0; i <= grdAssets.Items.Count - 1; i++)
                {

                    if ((grdAssets.Items[i].FindControl("chkItem") != null))
                    {
                        CheckBox check = (CheckBox)grdAssets.Items[i].FindControl("chkItem");

                        if (check.Checked)
                        {
                            //objRepository.Delete((view_AssetsEventTrackingHeader)objRepository.getTrackingRequestHeaderDetails(ZeroIntergerIFNull(grdAssets.Items[i].Cells[0].Text)));
                            AssetsEventTrackingHeader objs= new AssetsEventTrackingHeader();

                            objs = objRepository.getTrackingRequestHeaderByCodeNew(ZeroIntergerIFNull(grdAssets.Items[i].Cells[1].Text));
                            objs.IsDeleted = true;

                            int headId = objs.Code;
                            objRepository.UpdateAssetsEventTrackingHeader(objs);

                            using (var DC = new AssetsEntitiesNew())
                            {
                                var q = DC.AssetsEventTrackings.Where(o => o.RequestHeaderCode == headId).ToList();
                                foreach (var item in q)
                                {
                                    item.IsDeleted = true;
                                    DC.SaveChanges();
                                }
                            }
                                Logger.Log(
                                     userId: ReadSession("userId").ToString(),
                                     userName: ReadSession("AdminName").ToString(),
                                     tableName: "AssetsEventTrackingHeader",
                                     action: "Delete",
                                     recordId: grdAssets.Items[i].Cells[0].Text
                                     );
                        }
                    }
                }
                FillGrid();
                string script11 = FormatpopupErrorMSG(Resources.Alerts.DataDeletedSuccessfully, "3");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Updatepanel1", script11, true);
            }
            catch (Exception ex)
            {


                string script = FormatpopupErrorMSG(Resources.Alerts.SorryDeleteDataFailed + ex.Message.ToString(), "1");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Updatepanel1", script, true);
            }
        }
    }
}