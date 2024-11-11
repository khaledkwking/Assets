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
    public partial class AssetsCostDetails : BaseFormAdmin
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
                int? Land = 0;
                float? LandUsed = 0;

                int? Building = 0;
                float? BuildingUsed = 0;

                int? Cars = 0;
                float? CarsUsed = 0;

                int? Devices = 0;
                float? DevicesUsed = 0;

                int? Furniture = 0;
                float? FurnitureUsed = 0;

                int? Other = 0;
                float? OtherUsed = 0;

              
                var Query = en.GetAssetUnitsCostDetails().ToList();
                foreach (var item in Query)
                {

                    if (item.ParentCatCode == 1 || item.ParentCatCode == 2) // Devices and machines
                    {
                        Devices += int.Parse(item.TotalCost.ToString());
                        DevicesUsed += float.Parse(item.TotalUsed.ToString());
                    }
                    else if (item.ParentCatCode == 4) //Furniture
                    {
                        Furniture = int.Parse(item.TotalCost.ToString());
                        FurnitureUsed = float.Parse(item.TotalUsed.ToString());
                    }
                    else if (item.ParentCatCode == 63) //Cars
                    {
                        Cars = int.Parse(item.TotalCost.ToString());
                        if (item.TotalUsed != null)
                            CarsUsed = float.Parse(item.TotalUsed.ToString());
                    }
                    else if (item.ParentCatCode == 64) //Building
                    {
                        Building = int.Parse(item.TotalCost.ToString());
                        //BuildingUsed = float.Parse(item.TotalUsed.ToString());
                    }
                    else if (item.ParentCatCode == 66) //Land
                    {
                        Land = int.Parse(item.TotalCost.ToString());
                        //LandUsed = float.Parse(item.TotalUsed.ToString());
                    }
                    else  //Other
                    {
                        Other += int.Parse(item.TotalCost.ToString());
                        OtherUsed += float.Parse(item.TotalUsed.ToString());
                    }
                }

                int? TotalRealState = Land+Building;
                int? TotalDevicesOther = Cars+Devices+Furniture+Other;
                //int? TotalAll = null;

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Modules/Reports/RDLC/rpt_AssetsList_No9.rdlc");

                ReportViewer1.LocalReport.SetParameters(new ReportParameter("username", gets(ReadSession("AdminName"))));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("Land", (Land == null) ? "0" : Land.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("LandUsed", (LandUsed == null) ? "0" : LandUsed.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("Building", (Building == null) ? "0" : Building.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("BuildingUsed", (BuildingUsed == null) ? "0" : BuildingUsed.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("Cars", (Cars == null) ? "0" : Cars.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("CarsUsed", (CarsUsed == null) ? "0" : CarsUsed.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("Devices", (Devices == null) ? "0" : Devices.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("DevicesUsed", (DevicesUsed == null) ? "0" : DevicesUsed.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("Furniture", (Furniture == null) ? "0" : Furniture.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("FurnitureUsed", (FurnitureUsed == null) ? "0" : FurnitureUsed.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("Other", (Other == null) ? "0" : Other.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("OtherUsed", (OtherUsed == null) ? "0" : OtherUsed.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("TotalRealState", (TotalRealState == null) ? "0" : TotalRealState.ToString()));
                ReportViewer1.LocalReport.SetParameters(new ReportParameter("TotalDevicesOther", (TotalDevicesOther == null) ? "0" : TotalDevicesOther.ToString()));
                //var objlist = objRepository.GetList();
                ReportDataSource datasource = new ReportDataSource("DataSet1", Query.Take(1));
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
        }
    }
}