using System;
using System.Web.UI;
using Infrastructure;
using Infrastructure.DAL;
using Microsoft.Reporting.WebForms;
using UI.Web.Admin.Controller;



namespace UI.Web.Modules.WHM.Forms
{
    public partial class PurchaseReceiptReport : BaseFormAdmin
    {
        #region "Page Members"
        public PurchaseRepository objRepository = IoC.Resolve<PurchaseRepository>();
        public string _PageTitle = "Purchase";

        #endregion

        #region "Page Events"

        protected void Page_PreRender(object sender, EventArgs e)
        {
          

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "InboundReceiptReport.aspx";
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            //btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
            //btnSave.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {

                if (Request.QueryString["id"] != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/WHM/Reports/RDLC/Ar/PurchaseReceipt.rdlc");

                    var objList = objRepository.FillPurchaseOrderMater(ZeroIntergerIFNull(Request.QueryString["id"].ToString()));
                    ReportDataSource datasource = new ReportDataSource("Ds_Inbound", objList);
                    ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);


                }
                else
                {
                    string script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData  , "1");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                }
              
            }

        }


        #endregion

        #region "Fill Information"
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            var objList = objRepository.FillPurchaseOrderItems(ZeroIntergerIFNull(Request.QueryString["id"].ToString()));
            ReportDataSource datasource = new ReportDataSource("Ds_InboundItems", objList);
            e.DataSources.Add(datasource);

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
      


        #endregion

      
 
    }
}