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
    public partial class StocktakingGenetralReport : BaseFormAdmin
    {
        #region "Page Members"
        public InboundRepository objRepository = IoC.Resolve<InboundRepository>();
        public string _PageTitle = "Inbound Items ";

        #endregion

        #region "Page Events"

        protected void Page_PreRender(object sender, EventArgs e)
        {
          

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "StocktakingReport.aspx";
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            //btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
            //btnSave.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {

                if (Request.QueryString["s"] != null)
                {
                    var objList = objRepository.StocktakingGeneralreportFree();
                    if (objList != null)
                    {
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/WHM/Reports/RDLC/Ar/StocktakingGeneralReportFree.rdlc");

                        //var objlist = objRepository.GetList();
                        ReportDataSource datasource = new ReportDataSource("Ds_StockTaking", objList);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                    }
                    else
                    {
                        string script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData, "1");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                    }
                }
                else
                {

                    var objList = objRepository.StocktakingGeneralreport();
                    if (objList != null)
                    {
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/WHM/Reports/RDLC/Ar/StocktakingGeneralReport.rdlc");

                        //var objlist = objRepository.GetList();
                        ReportDataSource datasource = new ReportDataSource("Ds_StockTaking", objList);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                    }
                    else
                    {
                        string script = FormatpopupErrorMSG(Resources.Alerts.FailToSaveData, "1");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                    }
                }
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

      
 
    }
}