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
    public partial class EmployeeLocation : BaseFormAdmin
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
                // Get Emplyee Locartion 

                var empLocationsObj = objRepository.GetEmployeeLocation(ZeroIntergerIFNull(Request.QueryString["empid"]));
                if (empLocationsObj!=null)
                {
                    hdnSelectedNode.Value = empLocationsObj.LocationCode.ToString();
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

        protected void btnLink_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnSelectedNode.Value != "")
                {

                    var locationObj = objRepository.GetEmployeeLocation(ZeroIntergerIFNull(Request.QueryString["empid"]));
                    if (locationObj != null)
                    {
                        locationObj.LocationCode = ZeroIntergerIFNull(hdnSelectedNode.Value);
                        objRepository.UpdateD_EmployeeLocations(locationObj);
                    }
                    else
                    {
                        locationObj = new D_EmployeeLocations();
                        locationObj.LocationCode = ZeroIntergerIFNull(hdnSelectedNode.Value);
                        locationObj.EmpCode = ZeroIntergerIFNull(Request.QueryString["empid"]);
                        objRepository.AddD_EmployeeLocations(locationObj);

                    }


                    string script = FormatpopupErrorMSG("تم تحديد موقع الموظف بنجاح    ", "3");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updatepanel1", script, true);
                    return;

                }
                else
                {
                    string script = FormatpopupErrorMSG("عفوا ، إختر موقع الموظف  ", "1");
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