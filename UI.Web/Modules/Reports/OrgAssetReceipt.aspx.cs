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
    public partial class OrgAssetReceipt : BaseFormAdmin
    {
        #region "Page Members"
        public AssetsRepository objRepository = IoC.Resolve<AssetsRepository>();
        public string _PageTitle = "إستمارات حصر العهد";

        #endregion
        #region "Page Events"

        protected void Page_PreRender(object sender, EventArgs e)
        {


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "AssetReceipt.aspx";

        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            lblerror.Text = "";
            if (!IsPostBack)
            {
                fillReport();
            }
        }

        private List<view_CustodyList> setChartInfo(List<view_CustodyList> objList,int selectedEntityId)
        {

            var EmpList = new List<EmployeeViewModel>();
            //Load Employee List
            //if (Session["OraEmpList"] != null)
            //{
            //    EmpList = (List<EmployeeViewModel>)Session["OraEmpList"];
            //}
            //else
            //{
            //    // Request Data From Ora.

            //    Session["OraEmpList"] = EmpList;

            //}
            EmpList = GetOraEmpList(selectedEntityId);

            foreach (var item in objList)
            {
                if (item.OrgChartRefCode != null && item.OrgChartRefCode != 0 )
                {//Set Emp iNFORMATION by Entity information this option also applicaple for Org Assets

                    var Empinfo = EmpList.Where(x => x.ENTITYCODE == item.OrgChartRefCode).FirstOrDefault();
                    if (Empinfo != null)
                    {
                        item.ORG_NAME = Empinfo.ORG_NAME;
                        item.AMANA_NAME = Empinfo.AMANA_NAME;
                        item.DEPT_NAME = Empinfo.DEPT_NAME;
                        item.DIV_NAME = Empinfo.DIV_NAME;
                        item.SEC_NAME = Empinfo.SEC_NAME;
                        item.SUB_SEC_NAME = Empinfo.SUB_SEC_NAME;
                        item.JOB_NAME = Empinfo.JOB_NAME;

                    }

                }
            }

            return objList;

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

        protected void lnkQuick_Click(object sender, EventArgs e)
        {
            fillReport();
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            fillReport();
        }

        private void fillReport()
        {
            //var objlist = objRepository.getAssetsRequestList(txtPartOfName.Text, ZeroIntergerIFNull(lstFilterAction.SelectedValue),
            //                                                          NullDateifEmpty(txtTransDate.Text), NullDateifEmpty(txtTransactionDateTo.Text), 0, 0, 0);


            int entityCode = ZeroIntergerIFNull(Request.QueryString["entityId"].ToString());
            List<OrgChartViewModel> nodeChildren = new List<OrgChartViewModel>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = client.GetAsync(string.Format("OrgChart/NodeChildrenTree/{0}", (entityCode == 0 ? 1 : entityCode))).Result;

                if (!Res.IsSuccessStatusCode)
                    throw new Exception(Res.ToString());

                var result = Res.Content.ReadAsStringAsync().Result;

                // Get Employee Location 
                //var assignedLocations =   objRepository.GetLocationList();

                nodeChildren = JsonConvert.DeserializeObject<List<OrgChartViewModel>>(result);
            }


            // Get All Related Entities
 
            var objlist = objRepository.getFilteredCustodyList(txtPartOfName.Text, ZeroIntergerIFNull(lstFilterAction.SelectedValue),
                                                                NullDateifEmpty(txtTransDate.Text), NullDateifEmpty(txtTransactionDateTo.Text), 0, 0, nodeChildren.Select(x => x.ENTITYCODE).ToArray());

            if (objlist != null && objlist.Count > 0)
            {
                setChartInfo(objlist.ToList(), entityCode);

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_OrgCustodDetailed.rdlc");
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                ReportDataSource datasource = new ReportDataSource("ds_RequestList", objlist);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);

                //ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
                //this.ReportViewer1.LocalReport.Refresh();


            }
            else
            {
                string script = FormatErrorMSGSwal(Resources.Alerts.nodatafound, "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
            }
        }


        public void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
 
            var objList = objRepository.getAssetReceiptbyRequestCode(ZeroIntergerIFNull(((ReportParameterInfo)e.Parameters["RequestHeaderCode"]).Values[0]), 0);
            if (objList != null && objList.Count > 0)
            {
                ReportDataSource datasource = new ReportDataSource("ds_AssetsList", objList);
                e.DataSources.Add(datasource);
            }
        }

        #endregion



    }
}