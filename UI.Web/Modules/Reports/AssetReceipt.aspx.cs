using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using Microsoft.Reporting.WebForms;
using UI.Web.Admin.Controller;



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
                fillReport(false);
            }
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
            fillReport(true);
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            fillReport(true);
        }

        private void fillReport(bool ForceFilter)
        {
            if (ForceFilter)
            {
                var objlist = objRepository.getAssetsRequestList(txtPartOfName.Text, ZeroIntergerIFNull(lstFilterAction.SelectedValue),
                                                                     NullDateifEmpty(txtTransDate.Text), NullDateifEmpty(txtTransactionDateTo.Text), 0, 0, 0, -1);

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
                            setChartInfo(objList, objList[0].EmpRefCode.Value);

                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_AssetsReceipt.rdlc");
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                            //var objlist = objRepository.GetList();
                            ReportDataSource datasource = new ReportDataSource("ds_AssetsList", objList);

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
                            setChartInfo(objList, objList[0].EmpRefCode.Value);

                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_AssetsReceipt.rdlc");
                            ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                            //var objlist = objRepository.GetList();
                            ReportDataSource datasource = new ReportDataSource("ds_AssetsList", objList);

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