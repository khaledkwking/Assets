using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public partial class StocktakingReport : BaseFormAdmin
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
            if (Request.QueryString["entityId"] != null)
            {
                this.MasterPageFile = "~/Modules/_shared/MainEmpty.Master";
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            lblerror.Text = "";
            if (!IsPostBack)
            {
                fillReport();
            }

        }
        private void fillReport()
        {
            int EntityId = 0;
            int ReportType = 1;
            if (Request.QueryString["entityId"] != null)
            {
                EntityId = ZeroIntergerIFNull(Request.QueryString["entityId"]);

            }
            if (Request.QueryString["ReportType"] != null)
            {
                ReportType = ZeroIntergerIFNull(Request.QueryString["ReportType"]);

            }


            List<OrgChartViewModel> nodeChildren = new List<OrgChartViewModel>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = client.GetAsync(string.Format("OrgChart/NodeChildrenTree/{0}", (EntityId == 0 ? 1 : EntityId))).Result;

                if (!Res.IsSuccessStatusCode)
                    throw new Exception(Res.ToString());

                var result = Res.Content.ReadAsStringAsync().Result;

                // Get Employee Location 
                //var assignedLocations =   objRepository.GetLocationList();

                nodeChildren = JsonConvert.DeserializeObject<List<OrgChartViewModel>>(result);

                switch (ReportType)
                {
                    case 1:
                        {
                            _PageTitle = "تقرير مراقبة العهــــــد حسب الجهــــة";
                            var objList = objRepository.getCustodyListGrouped(nodeChildren.Select(x => x.ENTITYCODE).ToArray());
                            if (objList != null && nodeChildren != null)
                            {
                                foreach (var item in objList)
                                {
                                    item.ORG_NAME = nodeChildren.Where(x => x.ENTITYCODE == item.OrgChartRefCode).FirstOrDefault().ENTITYNAME;
                                    item.groupname = nodeChildren.Where(x => x.ENTITYCODE == item.OrgChartRefCode).FirstOrDefault().ENTITYNAME;

                                }

                                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_Inventory.rdlc");
                                ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                                //var objlist = objRepository.GetList();
                                ReportDataSource datasource = new ReportDataSource("Ds_StockTaking", objList);
                                ReportViewer1.LocalReport.DataSources.Clear();
                                ReportViewer1.LocalReport.DataSources.Add(datasource);
                            }
                            else
                            {
                                string script = FormatErrorMSGSwal(Resources.Alerts.FailToSaveData, "1");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                            }
                            break;
                        }
                    case 2:
                        { 
                            _PageTitle = "جدول بيانات الأصول";
                            var objList = objRepository.getCustodyListHera(nodeChildren.Select(x => x.ENTITYCODE).ToArray());
                            if (objList != null && nodeChildren != null)
                            {
                                foreach (var item in objList)
                                {
                                    item.ORG_NAME = nodeChildren.Where(x => x.ENTITYCODE == item.OrgChartRefCode).FirstOrDefault().ENTITYNAME;
                                    item.groupname = nodeChildren.Where(x => x.ENTITYCODE == item.OrgChartRefCode).FirstOrDefault().ENTITYNAME;

                                }

                                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_AssetsListWithServicePeriod.rdlc");
                                ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                                //var objlist = objRepository.GetList();
                                ReportDataSource datasource = new ReportDataSource("Ds_StockTaking", objList);
                                ReportViewer1.LocalReport.DataSources.Clear();
                                ReportViewer1.LocalReport.DataSources.Add(datasource);
                            }
                            else
                            {
                                string script = FormatErrorMSGSwal(Resources.Alerts.FailToSaveData, "1");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                            }
                            break;
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