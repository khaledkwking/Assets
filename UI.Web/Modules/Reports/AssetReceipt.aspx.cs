using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.UI;
using UI.Web.Admin.Controller;
using UI.Web.Controllers;



namespace UI.Web.Modules.WHM.Forms
{
    public partial class AssetReceipt : BaseFormAdmin
    {
        #region "Page Members"
        public AssetsRepository objRepository = IoC.Resolve<AssetsRepository>();
        public string _PageTitle = Resources.Pages.CustodyRecepit;

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
            if (!IsPostBack)
            {
                fillReportAsync(false);
            }
        }
        private List<view_CustodyList> GetOrgData(List<view_CustodyList> objList , int nodeId)
        {

            var orglistInfo = GetorgChartList(nodeId).Where(o=> o.ENTITYCODE==nodeId).FirstOrDefault();
            //Load Employee List
            if (orglistInfo != null)
            {
                foreach (var item in objList)
                {

                    if (orglistInfo != null)
                    {
                        item.AMANA_NAME = orglistInfo.ENTITYNAME;
                        //item.AMANA_NAME = Empinfo.AMANA_NAME;
                        //item.DEPT_NAME = Empinfo.DEPT_NAME;
                        //item.DIV_NAME = Empinfo.DIV_NAME;
                        //item.SEC_NAME = Empinfo.SEC_NAME;
                        //item.SUB_SEC_NAME = Empinfo.SUB_SEC_NAME;
                        //item.JOB_NAME = Empinfo.JOB_NAME;

                    }


                }

            }
            return objList;
        }
        private List<view_CustodyList> setChartInfo(List<view_CustodyList> objList, int EmpId)
        {

            var Empinfo = GetOraEmpDetails(EmpId);
            //Load Employee List
            if (Empinfo != null)
            {
                foreach (var item in objList)
                {

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
            fillReportAsync(true);
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            fillReportAsync(true);
        }

        private async Task fillReportAsync(bool ForceFilter)
        {
            if (ForceFilter)
            {
                var objlist = objRepository.getAssetsRequestList(txtPartOfName.Text, ZeroIntergerIFNull(lstFilterAction.SelectedValue),
                                                                     NullDateifEmpty(txtTransDate.Text), NullDateifEmpty(txtTransactionDateTo.Text), 0, 0, 0, -1,"");

                if (objlist != null && objlist.Count > 0)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/Sub_AssetsReceipt.rdlc");
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                    ReportDataSource datasource = new ReportDataSource("ds_AssetsList", objlist);

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
            else
            {

                try
                {

                    if (Request.QueryString["docId"] != null && Request.QueryString["docId"] != "0")
                    {
                        var objList = objRepository.getAssetReceiptbyRequestCode(ZeroIntergerIFNull(Request.QueryString["docId"].ToString()), 0);
                        if (objList != null && objList.Count > 0)
                        {
                            var expandedList = new List<view_CustodyList>();
                            foreach (var item in objList)
                            {
                                // Add the same item multiple times based on its quantity
                                for (int i = 0; i < item.Qty; i++) // Assuming 'Quantity' is the property name
                                {
                                    var clonedItem = CloneObject(item);

                                    expandedList.Add(clonedItem);
                                }
                            }
                            var s = GetOrgData(expandedList , expandedList[0].OrgChartRefCode.Value);
                            //setChartInfo(expandedList, expandedList[0].EmpRefCode.Value);
                            int? empRefCode = 0;
                            int emp_Id = expandedList[0].OrgEmpRefCode.Value;

                            if (emp_Id != 0)
                            {
                                using (var db = new AssetsEntitiesNew())
                                {
                                    int? x = emp_Id;
                                    var emp = db.Employee_tbl.FirstOrDefault(o => o.Emp_Id == x);
                                    empRefCode = emp.Ora_EmpRefCode;
                                }
                            }
                            setChartInfo(expandedList,int.Parse(empRefCode.ToString()));

                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            if (Request.QueryString["assetinv"] != null)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_AssetsInventory.rdlc");
                            else //طباعة استمارة عهد
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_OrgCustodDetailedDept.rdlc");

                            ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter("AssetType", "بطاقة عهدة اصل وحدة تنظيمية"));

                            //var objlist = objRepository.GetList();
                            ReportDataSource datasource = new ReportDataSource("ds_RequestList", expandedList);

                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(datasource);
                        }
                        else
                        {
                            string script = FormatErrorMSGSwal(Resources.Alerts.nodatafound, "1");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                        }

                    }
                    else if (Request.QueryString["requestCode"] != null && Request.QueryString["requestCode"] != "0")
                    {
                        var objList = objRepository.getAssetReceiptbyRequestCode(ZeroIntergerIFNull(Request.QueryString["requestCode"].ToString()), 0);
                        if (objList != null && objList.Count > 0)
                        {
                            var expandedList = new List<view_CustodyList>();
                            foreach (var item in objList)
                            {
                                // Add the same item multiple times based on its quantity
                                for (int i = 0; i < item.Qty; i++) // Assuming 'Quantity' is the property name
                                {
                                    var clonedItem = CloneObject(item);

                                    expandedList.Add(clonedItem);
                                }
                            }
                            setChartInfo(expandedList, expandedList[0].EmpRefCode.Value); // set information of organization

                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            if (Request.QueryString["assetinv"] != null)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_AssetsInventory.rdlc");
                            else
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_OrgCustodDetailed.rdlc");

                            ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter("AssetType", "بطاقة عهدة أصل شخصية"));

                            //var objlist = objRepository.GetList();
                            ReportDataSource datasource = new ReportDataSource("ds_RequestList", expandedList);

                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(datasource);
                        }
                        else
                        {
                            string script = FormatErrorMSGSwal(Resources.Alerts.nodatafound, "1");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                        }
                    }
                    else if (Request.QueryString["empid"] != null && Request.QueryString["empid"] != "0")
                    {

                        var objList = objRepository.getAssetReceiptbyRequestCode(0, ZeroIntergerIFNull(Request.QueryString["empid"].ToString()));
                        if (objList != null && objList.Count > 0)
                        {
                            var expandedList = new List<view_CustodyList>();
                            foreach (var item in objList)
                            {
                                // Add the same item multiple times based on its quantity
                                for (int i = 0; i < item.Qty; i++) // Assuming 'Quantity' is the property name
                                {
                                    var clonedItem = CloneObject(item);

                                    expandedList.Add(clonedItem);
                                }
                            }
                            setChartInfo(expandedList, expandedList[0].EmpRefCode.Value);

                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            if(Request.QueryString["assetinv"] != null)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_AssetsInventory.rdlc");

                            else
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_OrgCustodDetailed.rdlc");

                            ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter("AssetType", "بطاقة عهدة أصل شخصية"));

                            //var objlist = objRepository.GetList();
                            ReportDataSource datasource = new ReportDataSource("ds_RequestList", expandedList);

                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(datasource);
                        }
                        else
                        {
                            string script = FormatErrorMSGSwal(Resources.Alerts.nodatafound, "1");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                        }
                    }
                    else if(Request.QueryString["nodeId"] != null && Request.QueryString["EmpFlag"] == "1")
                    {


                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            int nodeId = ZeroIntergerIFNull(Request.QueryString["nodeId"].ToString());
                            HttpResponseMessage Res = client.GetAsync($"OrgChart/EmployeeHierarchy/{nodeId}").Result;

                            if (!Res.IsSuccessStatusCode)
                                throw new Exception(Res.ToString());

                            var result = Res.Content.ReadAsStringAsync().Result;

                            var objList = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(result);

                            //var objList = objRepository.getEntityEmployeeList(0, ZeroIntergerIFNull(Request.QueryString["nodeId"].ToString()));
                            if (objList != null && objList.Count > 0)
                            {
                                

                                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_Employees.rdlc");
                                                              

                                ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                                ReportViewer1.LocalReport.SetParameters(new ReportParameter("OrgName", objList.FirstOrDefault().ENTITYNAME));

                                //var objlist = objRepository.GetList();
                                ReportDataSource datasource = new ReportDataSource("dsEmployees", objList.ToList());

                                ReportViewer1.LocalReport.DataSources.Clear();
                                ReportViewer1.LocalReport.DataSources.Add(datasource);
                            }
                            else
                            {
                                string script = FormatErrorMSGSwal(Resources.Alerts.nodatafound, "1");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                            }
                        }
                    }
                    else if (Request.QueryString["nodeId"] != null && Request.QueryString["EmpFlag"] == "2")
                    {

                        int nodeId = ZeroIntergerIFNull(Request.QueryString["nodeId"].ToString());
                        HeplerController obj = new HeplerController();
                        //var orgList = await obj.orgChart(nodeId);
                        var orgList= obj.GetOrgChartCustodyHeader(nodeId);
                        //int[] nodeIds = orgList
                        //    .Select(x => x.ENTITYCODE)
                        //    .ToArray();

                        //var headers = objRepository.GetTrackingHeaderByNodeIds(nodeIds);
                                               

                        int[] HeaderIds = orgList
                            .Select(x => x.Code)
                            .ToArray();

                        var objList = objRepository.getAssetReceiptbyHeaderIds(HeaderIds);
                        if (objList != null && objList.Count > 0)
                        {
                            var expandedList = new List<view_CustodyList>();
                            int EmpCode = -1;
                            foreach (var item in objList)
                            {
                                // Add the same item multiple times based on its quantity
                                for (int i = 0; i < item.Qty; i++) // Assuming 'Quantity' is the property name
                                {
                                    var clonedItem = CloneObject(item);
                                    //var Orgitem = orgList.Find(x => x.Code == item.OrgChartRefCode);
                                    //.
                                    //clonedItem.ORG_NAME = Orgitem.OrgEmpName;
                                    //clonedItem.AMANA_NAME = Orgitem.AMANA_NAME;
                                    //clonedItem.DEPT_NAME = Orgitem.DEPT_NAME;
                                    //clonedItem.DIV_NAME = Orgitem.DIV_NAME;
                                    //clonedItem.SEC_NAME = Orgitem.SEC_NAME;
                                    //clonedItem.SUB_SEC_NAME = Orgitem.SUB_SEC_NAME;
                                    //clonedItem.JOB_NAME = Orgitem.;
                                    
                                    if (clonedItem.EmpRefCode.HasValue)
                                    {
                                
                                        EmpCode = clonedItem.EmpRefCode.Value;
                                        var Empinfo = GetOraEmpDetails(EmpCode);
                                        if (Empinfo != null)
                                        {
                                            clonedItem.ORG_NAME = Empinfo.ORG_NAME;
                                            clonedItem.AMANA_NAME = Empinfo.AMANA_NAME;
                                            clonedItem.DEPT_NAME = Empinfo.DEPT_NAME;
                                            clonedItem.DIV_NAME = Empinfo.DIV_NAME;
                                            clonedItem.SEC_NAME = Empinfo.SEC_NAME;
                                            clonedItem.SUB_SEC_NAME = Empinfo.SUB_SEC_NAME;
                                            clonedItem.JOB_NAME = Empinfo.JOB_NAME;
                                        }
                                        expandedList.Add(clonedItem);

                                        //setChartInfo(expandedList, EmpCode);
                                    }
                                    



                                }
                            }
                           // setChartInfo(expandedList, expandedList[0].EmpRefCode.Value); // set information of organization

                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            //if (Request.QueryString["assetinv"] != null)
                            //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_AssetsInventory.rdlc");
                            //else
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_OrgCustodDetailed.rdlc");

                            ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter("AssetType", "بطاقة عهدة أصل شخصية"));

                            //var objlist = objRepository.GetList();
                            ReportDataSource datasource = new ReportDataSource("ds_RequestList", expandedList);

                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(datasource);
                        }
                        else
                        {
                            string script = FormatErrorMSGSwal(Resources.Alerts.nodatafound, "1");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                        }
                        
                    }
                    else
                    {
                        // User FIlter Critriea

                    }
                }
                catch (Exception ex)
                {

                    string script = FormatErrorMSGSwal(Resources.Alerts.SorryFailToretriveData, "1");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                }
            }


        }
        private T CloneObject<T>(T obj) where T : new()
        {
            var newObj = new T();
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.CanWrite)
                {
                    property.SetValue(newObj, property.GetValue(obj));
                }
            }
            return newObj;
        }

        public void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
        {

            var objList = objRepository.getAssetReceiptbyRequestCode(ZeroIntergerIFNull(((ReportParameterInfo)e.Parameters["RequestHeaderCode"]).Values[0]), 0);
            if (objList != null && objList.Count > 0)
            {
                ReportDataSource datasource = new ReportDataSource("ds_RequestList", objList);
                e.DataSources.Add(datasource);
            }
        }

        #endregion



    }
}