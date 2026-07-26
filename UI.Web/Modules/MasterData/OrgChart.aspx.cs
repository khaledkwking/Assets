using AssetsManament.ViewModels;
using Infrastructure;
using Infrastructure.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.MasterData
{
    public partial class OrgChart : BaseFormAdmin
    {

        #region "Page Members"
        public LocationsRepository objRepository = IoC.Resolve<LocationsRepository>();
        public string _PageTitle = Resources.Pages.orgChart;
        public string OrgChartContent = "";

        #endregion

        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "Locations.aspx";

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["entityId"] != null && Request.QueryString["entityId"].ToString() != "")
                {
                    var empList = new List<EmployeeViewModel>();

                    empList = GetOraEmpList(ZeroIntergerIFNull(Request.QueryString["entityIds"])).Where(x=>x.EMP_STATUS== "active").ToList();

                    if (empList != null && empList.Count > 0)
                    {
                        int lastLevel = 0;
                        int LasParentId = 0;

                        foreach (var item in empList.Select((value, i) => new { i, value }))
                        {

                            if (item.value.EMPMANAGERID == null)
                                item.value.EMPMANAGERID = 0;

                            if (item.i == 0)
                            {
                                OrgChartContent += "<div Class='stiff-chart-level' data-level='" + lastLevel + "'>";
                                OrgChartContent += "<div Class='stiff-main-parent'>";
                                OrgChartContent += "<ul>";
                                OrgChartContent += "<li data-parent='" + @item.value.EMP_ID + "'>";
                                OrgChartContent += "<div class='the-chart'>";
                                OrgChartContent += "<img src='/wwwroot/assets/images/svg/person.svg' alt=''>";
                                OrgChartContent += "<p style='min-height:35px'> " + @item.value.EMP_NAME + "</p>";
                                OrgChartContent += "</div>";
                                OrgChartContent += "</li>";
                                LasParentId = @item.value.EMPMANAGERID.Value;
                            }
                            else
                            {
                                if (item.value.EMPMANAGERID.Value != LasParentId)
                                {
                                    lastLevel++;
                                    OrgChartContent += "</ul>";
                                    OrgChartContent += "</div>";
                                    OrgChartContent += "</div>";
                                    OrgChartContent += "<div Class='stiff-chart-level' data-level='" + lastLevel + "'>";
                                    OrgChartContent += "<div Class='stiff-child' data-child-from='" + @item.value.EMPMANAGERID.Value + "'>";
                                    OrgChartContent += "<ul>";
                                }

                                OrgChartContent += "<li data-parent='" + item.value.EMP_ID + "'>";
                                OrgChartContent += "<div class='the-chart'>";
                                OrgChartContent += "<img src='/wwwroot/assets/images/svg/person.svg' alt=''>";
                                OrgChartContent += "<p style='min-height:35px'>" + @item.value.EMP_NAME + "</p>";
                                OrgChartContent += "</div>";
                                OrgChartContent += "</li>";

                                if (item.i == empList.Count() - 1)
                                {
                                    OrgChartContent += " </ul>";
                                    OrgChartContent += " </div>";
                                    OrgChartContent += "</div>";
                                }
                            }

                            LasParentId = @item.value.EMPMANAGERID.Value;



                        }

                    }


                }
                else
                {
                   string  script = FormatpopupErrorMSG("please select org entity", "2");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);

                }

            }

        }
    }
}