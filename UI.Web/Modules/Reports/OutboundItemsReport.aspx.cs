using System;
using System.Collections.Generic;
using System.Web.UI;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using Microsoft.Reporting.WebForms;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.WHM.Forms
{
    public partial class OutboundItemsReport : BaseFormAdmin
    {
        #region "Page Members"
        public OutboundRepository objRepository = IoC.Resolve<OutboundRepository>();
        public string _PageTitle = "Inbound Items ";

        #endregion

        #region "Page Events"

        protected void Page_PreRender(object sender, EventArgs e)
        {
          

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "InboundListReport.aspx";
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            //btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
            //btnSave.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {
               
              
             
              
            }

        }
   
  
        #endregion

        #region "Fill Information"
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

        protected void btnFilter_Click(object sender, EventArgs e)
        {

            var InboundItemList = objRepository.FilloutboundItemReport(NullDateifEmpty(txtTransDate.Text), NullDateifEmpty(txtTransactionDateTo.Text));

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/WHM/Reports/RDLC/ar/OutboundItemsReport.rdlc");

         ReportViewer1.LocalReport.SetParameters(new ReportParameter("reportTitle", txtTransDate.Text + " الي " + txtTransactionDateTo.Text ));

            if (InboundItemList != null)
            {
                ReportDataSource datasource = new ReportDataSource("ds_Inbound", InboundItemList);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
            else
            {
                
                string script = FormatpopupErrorMSG(Resources.Alerts.SorryFailToretriveData, "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

            }
        }
    }
}