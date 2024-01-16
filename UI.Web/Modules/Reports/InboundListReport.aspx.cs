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
    public partial class InboundListReport : BaseFormAdmin
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
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            //btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
            //btnSave.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/WHM/Reports/RDLC/InboundList.rdlc");
                if (Session["InboundListResult"] != null)
                {
                    ReportDataSource datasource = new ReportDataSource("Ds_Inbound", (List<View_InboundList>)Session["InboundListResult"]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                }
                else
                {
                    //var objlist = objRepository.GetList("");
                    //ReportDataSource datasource = new ReportDataSource("Ds_Inbound", objlist);
                    //ReportViewer1.LocalReport.DataSources.Clear();
                    //ReportViewer1.LocalReport.DataSources.Add(datasource);
                    string script = FormatpopupErrorMSG(Resources.Alerts.SorryFailToretriveData, "1");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

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