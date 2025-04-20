using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using OfficeOpenXml;
using UI.Web.Admin.Controller;



namespace UI.Web.Modules.WHM.Forms
{
    public partial class StocktakingReport : BaseFormAdmin
    {
        #region "Page Members"
        public AssetsRepository objRepository = IoC.Resolve<AssetsRepository>();
        public string _PageTitle = Resources.Pages.CustodyItems;
        #endregion
        private const int PageSize = 50; // Number of records per page
        private int CurrentPage
        {
            get
            {
                // Store the current page in ViewState
                return ViewState["CurrentPage"] != null ? (int)ViewState["CurrentPage"] : 1;
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

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
                fillLookups();

                ViewState["parent"] = "0";
                ViewState["main"] = "0";
                ViewState["sub"] = "0";
                fillReport();
            }

        }
        private void fillLookups()
        {
            var firstLevel = LooksUpsRepository.ins.GetCategoriesByLevel(0);
            var secondLevel = firstLevel.SelectMany(f => LooksUpsRepository.ins.GetCategoriesByLevel(f.Code)).ToList();
            var thirdLevel = secondLevel.SelectMany(s => LooksUpsRepository.ins.GetCategoriesByLevel(s.Code)).ToList();
            FillDllwithoptional_ALL(firstLevel, ddlParent, "FinanceRefCode", "Code", "--- اختر ---");
            FillDllwithoptional_ALL(secondLevel, ddlMain, "FinanceRefCode", "Code", "--- اختر ---");
            FillDllwithoptional_ALL(thirdLevel, ddlSub, "FinanceRefCode", "Code", "--- اختر ---");

            List<OrgChartViewModel> nodeChildren = new List<OrgChartViewModel>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = client.GetAsync(string.Format("orgchart/GetChart")).Result;

                if (!Res.IsSuccessStatusCode)
                    throw new Exception(Res.ToString());

                var result = Res.Content.ReadAsStringAsync().Result;

                // Get Employee Location 
                //var assignedLocations =   objRepository.GetLocationList();

                nodeChildren = JsonConvert.DeserializeObject<List<OrgChartViewModel>>(result);
                FillDllwithoptional_ALL(nodeChildren.Where(x => x.PARENTCODE == 0 || x.PARENTCODE == null).ToList(), ddlGov, "ENTITYNAME", "ENTITYCODE", "--- اختر ---");
            }
            //lstToLocation.SelectedValue = "2381";




        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                LoadData(CurrentPage);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            int totalRecords = objRepository.GetCustodyListCount(ViewState["parent"].ToString(), ViewState["main"].ToString(), ViewState["sub"].ToString());
            int totalPages = (int)Math.Ceiling((double)totalRecords / PageSize);

            if (CurrentPage < totalPages)
            {
                CurrentPage++;
                LoadData(CurrentPage);
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
                HttpResponseMessage Res = client.GetAsync(string.Format("orgchart/GetChart")).Result;

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
                            ddlParent.Visible = false; ddlMain.Visible = false; ddlSub.Visible = false;btnSearch.Visible = false;
                            _PageTitle = "تقرير مراقبة العهــــــد حسب الجهــــة";
                            var objList = objRepository.getCustodyListGrouped(nodeChildren.Select(x => x.ENTITYCODE).ToArray());
                            if (objList != null && nodeChildren != null)
                            {
                                //FillDllwithoptional_ALL(nodeChildren.Where(x => x.PARENTCODE == 0 || x.PARENTCODE == null).ToList(), ddlGov, "ENTITYNAME", "ENTITYCODE", "--- اختر ---");

                                ViewState["ORG"] = ddlGov.SelectedItem.Text;
                                string ORG = ViewState["ORG"].ToString();

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
                            //_PageTitle = "جدول بيانات الأصول";
                            ////var objList = objRepository.getCustodyListHera(nodeChildren.Select(x => x.ENTITYCODE).ToArray());
                            //var objList = objRepository.getCustodyList();

                            //if (objList != null && nodeChildren != null)
                            //{
                            //    foreach (var item in objList)
                            //    {
                            //        item.ORG_NAME = nodeChildren.Where(x => x.ENTITYCODE == item.OrgChartRefCode).FirstOrDefault().ENTITYNAME;
                            //        item.groupname = nodeChildren.Where(x => x.ENTITYCODE == item.OrgChartRefCode).FirstOrDefault().ENTITYNAME;

                            //    }
                            //    setChartInfos(objList);


                            //    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_AssetsListWithServicePeriod.rdlc");
                            //    ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                            //    //var objlist = objRepository.GetList();
                            //    ReportDataSource datasource = new ReportDataSource("Ds_StockTaking", objList);
                            //    ReportViewer1.LocalReport.DataSources.Clear();
                            //    ReportViewer1.LocalReport.DataSources.Add(datasource);
                            //}
                            //else
                            //{
                            //    string script = FormatErrorMSGSwal(Resources.Alerts.FailToSaveData, "1");
                            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                            //}

                            _PageTitle = "جدول بيانات الأصول";

                            ViewState["parent"] = ddlParent.SelectedItem.Text;
                            ViewState["main"] = ddlMain.SelectedItem.Text;
                            ViewState["sub"] = ddlSub.SelectedItem.Text;
                             ViewState["ORG"] = ddlGov.SelectedItem.Text;

                            // Define pagination parameters
                            int pageSize = 50; // Number of records per page
                            int pageNumber = 1; // Current page number (you can set this based on user input)

                            //// Fetch the total count of records (for pagination purposes)
                            //int totalRecords = objRepository.GetCustodyListCount(ViewState["parent"].ToString(), ViewState["main"].ToString(), ViewState["sub"].ToString()); // Implement this method to return the total count
                            //int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                            var objList = objRepository.GetCustodyListWithFilter(ViewState["parent"].ToString(), ViewState["main"].ToString(), ViewState["sub"].ToString()); // Implement this method to return the total count
                           if (objList != null && nodeChildren != null)
                            {

                                string ORG = ViewState["ORG"].ToString();


                                //foreach (var item in objList)
                                //{
                                //    item.ORG_NAME = nodeChildren.Where(x => x.ENTITYCODE == item.OrgChartRefCode).FirstOrDefault()?.ENTITYNAME;
                                //    item.groupname = nodeChildren.Where(x => x.ENTITYCODE == item.OrgChartRefCode).FirstOrDefault()?.ENTITYNAME;
                                //}

                                setChartInfos(objList);

                                if (ORG == "--- اختر ---")
                                    ORG = "0";
                                objList = objList.Where(obj => (ORG != "0" ? obj.ORG_NAME == ORG : 1 == 1)).ToList();

                                int totalRecords = objList.Count; // Implement this method to return the total count
                                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                                lblCurrentPage.Text = $"صفحة {pageNumber} من {totalPages}";

                                // Fetch the paged data
                                //    var objList = objRepository.getCustodyListPaged(pageNumber, pageSize, ViewState["parent"].ToString(), ViewState["main"].ToString(), ViewState["sub"].ToString() , ViewState["ORG"].ToString()); // Implement this method to return paged data
                                int skip = (pageNumber - 1) * pageSize;
                                objList = objList.OrderBy(obj => obj.EmpName).Skip(skip).Take(pageSize).ToList(); 
                          
                                

                                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                                ReportViewer1.Reset();
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_AssetsListWithServicePeriod.rdlc");
                                ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));

                                // Create the report data source
                                ReportDataSource datasource = new ReportDataSource("Ds_StockTaking", objList);
                                ReportViewer1.LocalReport.DataSources.Clear();
                                
                                ReportViewer1.LocalReport.DataSources.Add(datasource);

                                
                                // Optionally, you can set parameters for total pages and current page
                                ReportViewer1.LocalReport.SetParameters(new ReportParameter("TotalPages", totalRecords.ToString()));
                                ReportViewer1.LocalReport.SetParameters(new ReportParameter("CurrentPage", pageNumber.ToString()));

                                ReportViewer1.LocalReport.Refresh();
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
        private void LoadData(int pageNumber)
        {
            List<OrgChartViewModel> nodeChildren = new List<OrgChartViewModel>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["centeralApi"].ToString());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = client.GetAsync(string.Format("orgchart/GetChart")).Result;

                if (!Res.IsSuccessStatusCode)
                    throw new Exception(Res.ToString());

                var result = Res.Content.ReadAsStringAsync().Result;

                // Get Employee Location 
                //var assignedLocations =   objRepository.GetLocationList();

                nodeChildren = JsonConvert.DeserializeObject<List<OrgChartViewModel>>(result);
                // Fetch the paged data
                var objList = objRepository.GetCustodyListWithFilter(ViewState["parent"].ToString(), ViewState["main"].ToString(), ViewState["sub"].ToString()); // Implement this method to return the total count
                if (objList != null && nodeChildren != null)
                {
                    FillDllwithoptional_ALL(nodeChildren.Where(x => x.PARENTCODE == 0 || x.PARENTCODE == null).ToList(), ddlGov, "ENTITYNAME", "ENTITYCODE", "--- اختر ---");


                    string ORG = ViewState["ORG"].ToString();


                    //foreach (var item in objList)
                    //{
                    //    item.ORG_NAME = nodeChildren.Where(x => x.ENTITYCODE == item.OrgChartRefCode).FirstOrDefault()?.ENTITYNAME;
                    //    item.groupname = nodeChildren.Where(x => x.ENTITYCODE == item.OrgChartRefCode).FirstOrDefault()?.ENTITYNAME;
                    //}

                    setChartInfos(objList);

                    if (ORG == "--- اختر ---")
                        ORG = "0";
                    objList = objList.Where(obj => (ORG != "0" ? obj.ORG_NAME == ORG : 1 == 1)).ToList();

                    // Get the total number of records for pagination
                    //    int totalRecords = objRepository.GetCustodyListCount(ViewState["parent"].ToString(), ViewState["main"].ToString(), ViewState["sub"].ToString());
                    //int totalPages = (int)Math.Ceiling((double)totalRecords / PageSize);

                    int totalRecords = objList.Count; // Implement this method to return the total count
                    int totalPages = (int)Math.Ceiling((double)totalRecords / 50);

                    // Set up the report data source
                    ReportDataSource rds = new ReportDataSource("Ds_StockTaking", objList);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_AssetsListWithServicePeriod.rdlc");

                    // Set parameters if needed
                    ReportParameter param = new ReportParameter("username", "YourUsername"); // Replace with actual username
                    ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { param });

                    // Refresh the report
                    ReportViewer1.LocalReport.Refresh();

                    // Update the current page label
                    lblCurrentPage.Text = $"صفحة {pageNumber} من {totalPages}";
                }
            }
        }

        #endregion

        private List<view_CustodyList> setChartInfos(List<view_CustodyList> objList)
        {

                foreach (var item in objList)
                {
                    var Empinfo = GetOraEmpDetails(item.EmpRefCode.Value);
                    if (Empinfo != null)
                    {
                        item.ORG_NAME = Empinfo.ORG_NAME;
                        item.groupname = Empinfo.ORG_NAME;
                        item.AMANA_NAME = Empinfo.AMANA_NAME;
                        item.DEPT_NAME = Empinfo.DEPT_NAME;
                        item.DIV_NAME = Empinfo.DIV_NAME;
                        item.SEC_NAME = Empinfo.SEC_NAME;
                        item.SUB_SEC_NAME = Empinfo.SUB_SEC_NAME;
                        item.JOB_NAME = Empinfo.JOB_NAME;

                    }


                }

            return objList;

        }


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

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
           // ExportToPDF();
        }
        protected void btnViewPDF_Click(object sender, EventArgs e)
        {
            // Create LocalReport instance
           
        }

        //private void ExportDataToCSV()
        //{
        //    // Fetch the data from your repository
        //    List<view_CustodyList> dataList = objRepository.getCustodyList(); // Adjust this method as needed

        //    // Set the response to be a CSV file
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment;filename=CustodyList.csv");
        //    Response.ContentType = "text/csv";

        //    // Create a StringBuilder to hold the CSV data
        //    StringBuilder sb = new StringBuilder();

        //    // Add the header row
        //    sb.AppendLine("EmpName,OrgChartRefCode"); // Adjust these headers based on your properties

        //    // Add data rows
        //    foreach (var item in dataList)
        //    {
        //        sb.AppendLine($"{item.EmpName},{item.OrgChartRefCode}"); // Adjust these based on your properties
        //    }

        //    // Write the CSV data to the response
        //    Response.Output.Write(sb.ToString());
        //    Response.Flush();
        //    Response.End();
        //}
        private void ExportReportToCSV()
        {
            // Set up the report
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_AssetsListWithServicePeriod.rdlc");

            // Fetch your data (this should match the data source used in the RDLC)
            List<view_CustodyList> dataList = objRepository.getCustodyList(); // Adjust this method as needed

            // Create a report data source
            ReportDataSource rds = new ReportDataSource("Ds_StockTaking", dataList);
            localReport.DataSources.Clear();
            localReport.DataSources.Add(rds);

            // Render the report to a byte array
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;

            byte[] renderedBytes = localReport.Render(
                "Excel", null, out mimeType, out encoding, out fileNameExtension,
                out streams, out warnings);

            // Convert the Excel byte array to CSV format
            string csvData = ConvertExcelToCSV(renderedBytes);

            // Set the response to be a CSV file
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=CustodyList.csv");


            Response.ContentType = "text/csv";

            // Write the CSV data to the response
            Response.Output.Write(csvData);
            Response.Flush();
            Response.End();
        }
        private string ConvertExcelToCSV(byte[] excelData)
        {
            // Use a library like EPPlus or NPOI to read the Excel data and convert it to CSV
            using (var package = new ExcelPackage(new MemoryStream(excelData)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Get the first worksheet
                StringBuilder sb = new StringBuilder();

                // Loop through the rows and columns to build the CSV
                for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
                {
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        if (col > 1)
                            sb.Append(","); // Add comma for CSV format
                        sb.Append(worksheet.Cells[row, col].Text); // Get cell value
                    }
                    sb.AppendLine(); // New line for the next row
                }

                return sb.ToString();
            }
        }
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportReportToCSV();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ViewState["parent"] = ddlParent.SelectedItem.Text;
            ViewState["main"] = ddlMain.SelectedItem.Text;
            ViewState["sub"] = ddlSub.SelectedItem.Text;
            ViewState["ORG"] = ddlGov.SelectedItem.Text;

            fillReport();
        }

        protected void ddlParent_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            var query = LooksUpsRepository.ins.GetCategoriesByLevel(ZeroIntergerIFNull(ddlParent.SelectedValue));
            FillDllwithoptional_ALL(query, ddlMain, "FinanceRefCode", "Code", "--- اختر ---");

            ddlSub.Items.Clear();
            ddlSub.Items.Add(new ListItem("--- اختر ---", "0"));
        }

        protected void ddlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var query = LooksUpsRepository.ins.GetCategoriesByLevel(ZeroIntergerIFNull(ddlMain.SelectedValue));


            FillDllwithoptional_ALL(query, ddlSub, "FinanceRefCode", "Code", "--- اختر ---");

        }
    }
}