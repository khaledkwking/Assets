using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infrastructure;
using Infrastructure.DAL;
using Infrastructure.DAL.Model.DB;
using UI.Web.Admin.Controller;

namespace UI.Web.Modules.MasterData
{
    public partial class OrgChartTree : BaseFormAdmin
    {
        #region "Page Members"
        public LocationsRepository objRepository = IoC.Resolve<LocationsRepository>();
        public string _PageTitle = "سجل العهد";

        #endregion

        #region "Page Events"
        protected void Page_PreInit(object sender, EventArgs e)
        {
            PageUrl = "Locations.aspx";

        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
 
            if (!IsPostBack)
            {
                //if ((Request.UrlReferrer == null))
                //{
                //    Response.Redirect("/admin/pages/main.aspx");
                //}
                
                ViewState["itemID"] = "0";
                //  FillDll(LooksUpsRepository.ins.FillQuantityCode(), lstQunit, Resources.Pages.TitleFiled, "Code");

                if (Request.QueryString["pid"] !=null)
                {
                    hdnSelectedNode.Value = Request.QueryString["pid"].ToString();
                  

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
                return Resources.Pages.editData;
            }

        }




        #endregion

        protected void lnkDeleteOrgLocation_Click(object sender, EventArgs e)
        {
            if (hdnSelectedNode.Value != "" && hdnSelectedNode.Value != "[]")
            {
                objRepository.ResetEntityLocation(ZeroIntergerIFNull(hdnSelectedNode.Value));
                string script = FormatpopupErrorMSG("تم فك الربط بنجاح ", "3");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                return;
            }
        }
    }
}