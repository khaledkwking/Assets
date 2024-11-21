using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.Dashboard
{
    public partial class Dashboard : BaseFormAdmin
    {
        //public AssetsRepository objRepository = IoC.Resolve<AssetsRepository>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindData();
            }
        }
        private void BindData()
        {
            using (AssetsEntitiesNew en = new AssetsEntitiesNew())
            {
                var EmpData = en.sp_GetAllEmpsHaveAssetsDetails().ToList();

                lblActiveEmpWithoutAsset.Text = EmpData.Where(o => o.EmployeeId == null && o.Emp_Active == true).Count().ToString();
                lblNotActiveEmpHaveAssets.Text = EmpData.Where(o => o.EmployeeId != null && o.Emp_Active == false).Count().ToString();

                var AssetsH = en.AssetsEventTrackingHeaders.ToList();
                lblEmpAssets.Text = AssetsH.Where(o => o.EmpRefCode != 0 && o.EmpRefCode != null).Count().ToString();
                lblOrgAssets.Text = AssetsH.Where(o => o.EmpRefCode == 0 || o.EmpRefCode == null).Count().ToString();
                lblNoEmpAssets.Text = AssetsH.Where(o => !string.IsNullOrEmpty(o.EmpName) && o.EmpName.Contains("بدون")).Count().ToString();
            }
        }
        [WebMethod]
        public static string GetChartDataEmps()
        {

            using (AssetsEntitiesNew en = new AssetsEntitiesNew())
            {
                var chartData = en.sp_GetAllEmployeesCount();

                return JsonConvert.SerializeObject(chartData.ToList());
            }
        }
        [WebMethod] 
        public static string GetChartDataAssetsType()
        {

            using (AssetsEntitiesNew en = new AssetsEntitiesNew())
            {
                var chartData = en.sp_GetAllAssetsTypeCount();

                return JsonConvert.SerializeObject(chartData.ToList());
            }
        }
        [WebMethod] 
        public static string GetChartDataEmpHaveAssets()
        {

            using (AssetsEntitiesNew en = new AssetsEntitiesNew())
            {
                var chartData = en.sp_GetAllEmpsHaveAssetsCount();

                return JsonConvert.SerializeObject(chartData.ToList());
            }
        }

        private void FillGridAssets(string Type)
        {
            using (AssetsEntitiesNew en = new AssetsEntitiesNew())
            {
                var objlist = en.AssetsEventTrackingHeaders.ToList();
                if(Type== "NoEmpAssets")
                {
                    objlist = objlist.Where(o => !string.IsNullOrEmpty(o.EmpName) && o.EmpName.Contains("بدون")).ToList();
                }
                else if (Type == "EmpAssets")
                {
                    objlist = objlist.Where(o => o.EmpRefCode != 0 && o.EmpRefCode != null).ToList();
                }
                else if (Type == "OrgAssets")
                {
                    objlist = objlist.Where(o => o.EmpRefCode == 0 || o.EmpRefCode == null).ToList();
                }
                //var duplicatedList = objlist.SelectMany(t =>
                //     Enumerable.Repeat(t, 2)).ToList();

                lblcountAssets.Text = (Resources.Utilities.foundTotal + " (" + (objlist.Count.ToString()).ToString() + ") " + Resources.Utilities.records);
                decimal c = System.Math.Ceiling(Convert.ToDecimal(objlist.Count / grdData.PageSize));
                if ((c <= grdDataAssets.CurrentPageIndex))
                {
                    grdDataAssets.CurrentPageIndex = 0;
                }

                grdDataAssets.DataSource = objlist; //;
                grdDataAssets.DataBind();

                //int _totalCount = objlist.Count;
                pager1.ItemCount = objlist.Count;
            }

        }
        protected void pager_Command(object sender, CommandEventArgs e)
        {
            Int32 currnetPageIndx = ((Int32)(e.CommandArgument));
            if ((currnetPageIndx <= 0))
            {
                currnetPageIndx = 1;
            }

            if ((currnetPageIndx > grdDataAssets.PageCount))
            {
                currnetPageIndx = (grdDataAssets.PageCount - 1);
            }

            pager1.CurrentIndex = currnetPageIndx;
            grdDataAssets.CurrentPageIndex = (currnetPageIndx - 1);
            FillGridAssets();
        }
            private void FillGrid(string Type)
        {
            using (AssetsEntitiesNew en = new AssetsEntitiesNew())
            {
                grdData.DataSource = null;
                grdData.DataBind();
                var EmpData = en.sp_GetAllEmpsHaveAssetsDetails().ToList();
                lblActiveEmpWithoutAsset.Text = EmpData.Where(o => o.EmployeeId == null && o.Emp_Active == true).Count().ToString();
                lblNotActiveEmpHaveAssets.Text = EmpData.Where(o => o.EmployeeId != null && o.Emp_Active == false).Count().ToString();
                if (EmpData!=null)
                {
                    if(Type== "ActiveEmpWithoutAsset")
                        EmpData = EmpData.Where(o => o.EmployeeId == null && o.Emp_Active == true).ToList();


                    else if (Type== "NotActiveEmpHaveAssets")
                        EmpData = EmpData.Where(o => o.EmployeeId != null && o.Emp_Active == false).ToList();

                    lblcount.Text = (Resources.Utilities.foundTotal + (EmpData.Count.ToString() + Resources.Utilities.records));
                    decimal c = System.Math.Ceiling(Convert.ToDecimal(EmpData.Count / grdData.PageSize));
                    if ((c <= grdData.CurrentPageIndex))
                    {
                        grdData.CurrentPageIndex = 0;
                    }

                    grdData.DataSource = EmpData;
                    grdData.DataBind();
                }
              

            }
        }
        protected void grdData_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#DA9CF1\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#FFFFFF\';");
            }

            if ((e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor=\'#DA9CF1\';");
                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=\'#EFEFEF\';");
            }

        }

        protected void lnkbtnActiveEmpWithoutAsset_Click(object sender, EventArgs e)
        {
            card2.Attributes.Add("class", "selected2");

            card1.Attributes.Add("class", "cardbox");
            card3.Attributes.Add("class", "cardbox3");

            string script659 = "ViewDiv();";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallHide", script659, true);
            FillGrid("ActiveEmpWithoutAsset");
        }

        protected void lnkbtnNotActiveEmpHaveAssets_Click(object sender, EventArgs e)
        {
            card3.Attributes.Add("class", "selected3");

            card2.Attributes.Add("class", "cardbox2");
            card1.Attributes.Add("class", "cardbox");

            string script659 = "ViewDiv();";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallHide", script659, true);
            FillGrid("NotActiveEmpHaveAssets");
        }
        public string ShowYesNo(bool isYes)
        {
            if (isYes)
            {
                return "<span class=\'label label-sm label-success\'>فعال</span>";
            }
            else
            {
                return "<span class=\'label label-sm label-danger\'>غير فعال</span>";
            }

        }

        protected void lnkbtnNoEmpAssets_Click(object sender, EventArgs e)
        {
            card4.Attributes.Add("class", "selected3");

            card2.Attributes.Add("class", "cardbox2");
            card1.Attributes.Add("class", "cardbox");

            card3.Attributes.Add("class", "cardbox2");
            card5.Attributes.Add("class", "cardbox2");
            card6.Attributes.Add("class", "cardbox2");

            string script659 = "ViewDiv();";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallHide", script659, true);
            FillGridAssets("NoEmpAssets");
        }

        protected void lnkbtnEmpAssets_Click(object sender, EventArgs e)
        {
            card5.Attributes.Add("class", "selected3");

            card2.Attributes.Add("class", "cardbox2");
            card1.Attributes.Add("class", "cardbox");

            card3.Attributes.Add("class", "cardbox2");
            card4.Attributes.Add("class", "cardbox2");
            card6.Attributes.Add("class", "cardbox2");

            string script659 = "ViewDiv();";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallHide", script659, true);
            FillGridAssets("EmpAssets");
        }

        protected void lnkbtnOrgAssets_Click(object sender, EventArgs e)
        {
            card6.Attributes.Add("class", "selected3");

            card2.Attributes.Add("class", "cardbox2");
            card1.Attributes.Add("class", "cardbox");

            card3.Attributes.Add("class", "cardbox2");
            card5.Attributes.Add("class", "cardbox2");
            card4.Attributes.Add("class", "cardbox2");

            string script659 = "ViewDiv();";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallHide", script659, true);
            FillGridAssets("OrgAssets");
        }
    }
}