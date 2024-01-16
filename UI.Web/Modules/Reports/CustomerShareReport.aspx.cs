using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.Collections;
using UI.Web.Admin.Controller;

using Infrastructure.DAL;
using Microsoft.VisualBasic;
using UI.Web.Controler;
using DomainInterface;
using Infrastructure;
using Infrastructure.DAL.Model.DB;
using Microsoft.Reporting.WebForms;

namespace UI.Web.Modules.WHM.Forms
{
 
    partial class CustomerShareReport : BaseFormAdmin
    {

        public OutboundRepository objRepository = IoC.Resolve<OutboundRepository>();

       
 
        #region "From Events"
        


        
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            //btnCancel.Attributes.Add("onclick", "Page_ValidationActive=false;");
            //btnSave.Attributes.Add("onclick", "return chkImage();");
            if (!IsPostBack)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/WHM/Reports/RDLC/ar/CustomerShare.rdlc");

                var objCustomerShare = objRepository.GetCustomerShareReports();

                if (objCustomerShare != null)
                {
                    ReportDataSource datasource = new ReportDataSource("ds_CustomerShare", objCustomerShare);
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

        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "InboundListReport.aspx";
        }
        


        #endregion

        #region "Fill Infotmation"
        private void ClearForm()
        {


            ViewState["Item"] = 0;

        }
        private void fillLookups()
        {

 

           

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