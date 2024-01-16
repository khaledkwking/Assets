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
    public partial class Locationslink : BaseFormAdmin
    {
        #region "Page Members"
        public LocationsRepository objRepository = IoC.Resolve<LocationsRepository>();
        public string _PageTitle = Resources.Pages.Locations;

        #endregion

        #region "Page Events"

        protected void Page_Init(object sender, EventArgs e)
        {
            PageUrl = "Locations.aspx";
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {

            if (!IsPostBack)
            {
                // Load Entity Locations
                LoadEntityLocations();
            }

        }



        #endregion

        #region "Fill Information"

        private void LoadEntityLocations()
        {
            var locationObj = objRepository.getEntityLocations(ZeroIntergerIFNull(Request.QueryString["entityId"]), "");
            if (locationObj != null)
            {
                string Ids = "";
                foreach (var item in locationObj)
                {

                    Ids += item.Code + ",";
                }
                hdnSelectedNode.Value = Ids;
            }
        }


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

        protected void btnLink_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnSelectedNode.Value != "" && hdnSelectedNode.Value != "[]") 
                {
                    objRepository.ResetEntityLocation(ZeroIntergerIFNull(Request.QueryString["entityId"]));

                    //string[] lstIds = hdnSelectedNode.Value.Substring(1, hdnSelectedNode.Value.Length - 2).Split(',');
                    //for (int i = 0; i < lstIds.Length - 1; i++)
                    //{
                    //    var locationObj = objRepository.GetDetails(ZeroIntergerIFNull(lstIds[i].Replace('"', ' ')));
                    //    if (locationObj != null)
                    //    {
                    //        locationObj.OrgChartRefCode = ZeroIntergerIFNull(Request.QueryString["entityId"]);
                    //        objRepository.Update(locationObj);
                    //    }
                    //}

                    
                    objRepository.SetEntityLocation(ZeroIntergerIFNull(Request.QueryString["entityId"]), hdnSelectedNode.Value.Substring(1, hdnSelectedNode.Value.Length - 2));
                    LoadEntityLocations();

                    string script = FormatpopupErrorMSG("تم الربط بنجاح ", "3");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                    return;

                }
                else
                {
                    string script = FormatpopupErrorMSG("عفوا ، إختر المواقع للربط بالحهة ", "1");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                    return;

                }
            }
            catch (Exception ex)
            {

                string script = FormatErrorMSGSwal("Error," + ex.Message, "1");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                return;
            }


        }
    }
}