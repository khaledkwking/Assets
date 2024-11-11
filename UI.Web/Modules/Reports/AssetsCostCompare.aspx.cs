using Infrastructure.DAL.Model.DB;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.Reports
{
    public partial class AssetsCostCompare : BaseFormAdmin
    {
        public string _PageTitle = Resources.Pages.CustodyItems;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblerror.Text = "";
            if (!IsPostBack)
            {
                fillReport();
            }
        }
        private void fillReport()
        {
            using (var en = new AssetsEntitiesNew())
            {
                float? Land = 0;
                float? LandUsed = 0;

                float? Building = 0;
                float? BuildingUsed = 0;

                float? Cars_current = 0;
                float? Cars_Last = 0;

                float? Devices_current = 0;
                float? Devices_Last = 0;

                float? Furniture_current = 0;
                float? Furniture_Last = 0;

                float? Other_current = 0;
                float? Other_Last = 0;


                var Query = en.GetAssetUnitsCostCompare().ToList();
                foreach (var item in Query)
                {

                    if (item.ParentCatCode == 1 || item.ParentCatCode == 2) // Devices and machines
                    {
                        Devices_current += float.Parse(item.Value_Current_Year.ToString());
                        Devices_Last += float.Parse(item.Value_Last_Year.ToString());
                    }
                    else if (item.ParentCatCode == 4) //Furniture
                    {
                        Furniture_current = float.Parse(item.Value_Current_Year.ToString());
                        Furniture_Last = float.Parse(item.Value_Last_Year.ToString());
                    }
                    else if (item.ParentCatCode == 63) //Cars
                    {if (item.Value_Current_Year != null && item.Value_Last_Year != null)
                        {
                            Cars_current = float.Parse(item.Value_Current_Year.ToString());
                            Cars_Last = float.Parse(item.Value_Last_Year.ToString());
                        }
                    }
                    else if (item.ParentCatCode == 64) //Building
                    {
                        Building = float.Parse(item.TotalCost.ToString());
                        //BuildingUsed = float.Parse(item.TotalUsed.ToString());
                    }
                    else if (item.ParentCatCode == 66) //Land
                    {
                        Land = float.Parse(item.TotalCost.ToString());
                        //LandUsed = float.Parse(item.TotalUsed.ToString());
                    }
                    else  //Other
                    {
                        Other_current += float.Parse(item.Value_Current_Year.ToString());
                        Other_Last += float.Parse(item.Value_Last_Year.ToString());
                    }
                }

                float? TotalRealState = Land + Building;
                float? TotalDevicesOther_current = Cars_current + Devices_current + Furniture_current + Other_current;
                float? TotalDevicesOther_Last = Cars_Last + Devices_Last + Furniture_Last + Other_Last;
                //int? TotalAll = null;

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_AssetsList_No9_Compare.rdlc");

                ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("Land", (Land == null) ? "0" : Land.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("LandUsed", (LandUsed == null) ? "0" : LandUsed.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("Building", (Building == null) ? "0" : Building.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("BuildingUsed", (BuildingUsed == null) ? "0" : BuildingUsed.ToString()));
    
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("TotalRealState", (TotalRealState == null) ? "0" : TotalRealState.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("TotalDevicesOther_current", (TotalDevicesOther_current == null) ? "0" : TotalDevicesOther_current.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("TotalDevicesOther_Last", (TotalDevicesOther_Last == null) ? "0" : TotalDevicesOther_Last.ToString()));
                //var objlist = objRepository.GetList();
                ReportDataSource datasource = new ReportDataSource("DataSet1", Query.Take(1));
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
        }
    }
}