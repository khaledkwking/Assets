using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.UI;
using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using UI.Web.Admin.Controller;



namespace UI.Web.Modules.WHM.Forms
{
    public partial class OrgChartPrint : BaseFormAdmin
    {
        #region "Page Members"
        public AssetsRepository objRepository = IoC.Resolve<AssetsRepository>();
        public string _PageTitle = Resources.Pages.CustodyItems;

        #endregion
        #region "Page Events"

        protected void Page_PreRender(object sender, EventArgs e)
        {


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "Locations.aspx";
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            if (!IsPostBack)
            {
                var objList = orgChart(0);
                if (objList != null)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/OrgChart.rdlc");

                   
                    ReportDataSource datasource = new ReportDataSource("dsOrgChart", objList);
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



        public List<ORGANIZATION_CHART> orgChart(int nodeid)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = client.GetAsync(string.Format("orgchart/GetChart")).Result;

                if (!Res.IsSuccessStatusCode)
                    throw new Exception(Res.ToString());

                var result = Res.Content.ReadAsStringAsync().Result;
 
                return JsonConvert.DeserializeObject<List<ORGANIZATION_CHART>>(result);
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