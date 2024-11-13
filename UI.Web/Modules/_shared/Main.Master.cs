using Infrastructure.DAL.Model.DB;
using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;
using UI.Web.Core;

namespace UI.Web.Modules._shared
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            ToolkitScriptManager1.AsyncPostBackErrorMessage = e.Exception.Message;
        }

        #region "Private Members"



        private Security_pr_admin user;

        public string mainMenu = "";
        public StringBuilder strmenu = new StringBuilder();


        public string AdminName = "";
        public string PrfilePhoto = "";
        public string _NewMessagesCount = "0";
        public string _NewMessagesText = "";

        #endregion

        #region Data Elements


        string applicationPath;
        private const string
            strLangEnglish = "en-US",
            strLangArabic = "ar-EG";

        #endregion Data Elements

        #region "Events Handlers"
        protected void Page_Init(object sender, EventArgs e)
        {
            //applicationPath = (Request.ApplicationPath.Length == 1) ? "" : Request.ApplicationPath;
            //if (Session["Language"] == null || Session["Language"].ToString() == "")
            //{
            //    Response.Redirect(applicationPath + "/default.aspx", false);
            //    return;
            //}


            if (!IsPostBack)
            {


            }



            MemberShip_Permission.isAuthenticationCookie();
            user = MemberShipConstantUI.CurrentUser;
            if (user != null)
            {
                ViewState["userid"] = user.id.ToString();
                Session["userid"] = user.id.ToString();
                ViewState["AdminName"] = user.name.ToString();
                Session["AdminName"] = user.name.ToString();
                AdminName = user.name.ToString();
                PrfilePhoto = Resources.Utilities.Assetspath + "uploads/Adminprofile/" + user.AdminPhoto.ToString();
                FillPermissions(user.AdminType, user.id);
                ShowAlerts();
                FillMenu();

            }
            else
            { Response.Redirect("~/Admin/Pages/Login.aspx"); }


            //    SetModuleDefultpagelnk();

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            string URLPath;
            applicationPath = (Request.ApplicationPath.Length == 1) ? "" : Request.ApplicationPath;

            try
            {
                //Label2.Text = Page.Title;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                URLPath = Request.AppRelativeCurrentExecutionFilePath;


                Session["URLPath"] = URLPath;

                string UrlPathName = URLPath.Replace("~", "");
                UrlPathName = UrlPathName.Replace(".aspx", "");



            }
            catch (Exception exp)
            {
                // Updating the Error details into Log file
                //dbErrorHandler.ErrorMessage = exp.Message;
                //objLogController.LogErrordetails(CommonProperties.User, dbErrorHandler, Request.AppRelativeCurrentExecutionFilePath + ".Page_Load", Request.ServerVariables["LOGON_USER"] + " - " + Request.UserHostAddress);
            }
        }
        #endregion

        #region "Local Methods"





        private string getCurrentUrl()
        {
            string url = "";

            url = Request.RawUrl.ToLower().Replace("_ar", "");
            url = url.Substring(url.LastIndexOf("/") + 1);
            if (url.IndexOf("?") != -1)
            {
                url = url.Substring(0, url.IndexOf("?"));
            }

            return url;
        }
        public string FillPermittedModules()
        {
            string _out = "";



            //if (ShowSystem("3"))
            //{//Inbound
            //    _out += " <li class='nk-menu-item'>";
            //    _out += " <a href = '#' class='nk-menu-link nk-menu-switch' data-target='navInbound'>";
            //    _out += "<span class='nk-menu-icon'><em class='icon ni ni-exchange'></em></span>";
            //    _out += "</a>";
            //    _out += "</li>";

            //}
            if (ShowSystem("4"))
            {//OutBound
                _out += " <li class='nk-menu-item'>";
                _out += " <a href = '#' class='nk-menu-link nk-menu-switch' data-target='navOutbound'>";
                _out += "<span class='nk-menu-icon'><em class='icon ni ni-wallet-saving'></em></span>";
                _out += "</a>";
                _out += "</li>";

            }



            if (ShowSystem("2"))
            {//Users
                _out += " <li class='nk-menu-item' >";
                _out += " <a href = '#' class='nk-menu-link nk-menu-switch' data-target='navMaster'>";
                _out += "<span class='nk-menu-icon'><em class='icon ni ni-menu-circled'></em></span>";
                _out += "</a>";
                _out += "</li>";

            }
            if (ShowSystem("5"))
            {//Reports
                _out += " <li class='nk-menu-item'>";
                _out += " <a href = '#' class='nk-menu-link nk-menu-switch' data-target='navReports'>";
                _out += "<span class='nk-menu-icon'><em class='icon ni ni-reports'></em></span>";
                _out += "</a>";
                _out += "</li>";

            }

            if (ShowSystem("1"))
            {//Mater
                _out += " <li class='nk-menu-item'>";
                _out += " <a href = '#' class='nk-menu-link nk-menu-switch' data-target='navUsers'>";
                _out += "<span class='nk-menu-icon'><em class='icon ni ni-users'></em></span>";
                _out += "</a>";
                _out += "</li>";

            }


            return _out;

        }


        public string FillMenu()
        {
            StringBuilder strmenu = new StringBuilder();
            // /************************New Menu**************************/
            if (ShowSystem("2"))
            {//Master


                strmenu.Append(("<div class='nk-menu-content' data-content='navMaster'>"));
                strmenu.Append(("<h5 class='title'>" + Resources.Pages.PortalSetting + "</h5>"));
                strmenu.Append(("<ul class='nk-menu'>"));


                if (ShowPage("Locations.aspx"))
                {


                    strmenu.Append("<li class='nk-menu-item has-sub'>");
                    strmenu.Append("<a href = '#' class='nk-menu-link nk-menu-toggle' data-original-title='' title=''>");
                    strmenu.Append("<span class='nk-menu-icon'><em class='icon ni ni-layers'></em></span>");
                    strmenu.Append(" <span class='nk-menu-text'>" + Resources.Pages.orgChartmanagement + "</span>");
                    strmenu.Append("</a>");
                    strmenu.Append("<ul class='nk-menu-sub'>");



                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/OrgChartTree.aspx' class='nk-menu-link'>"));
                    strmenu.Append((" <span class='nk-menu-text'>" + Resources.Pages.orgChart + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));




                    //strmenu.Append(("<li class='nk-menu-item'>"));
                    //strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/EntityChart.aspx' class='nk-menu-link'>"));
                    //strmenu.Append((" <span class='nk-menu-text'>" + Resources.Pages.orgChart + "</span>"));
                    //strmenu.Append(("</a>"));
                    //strmenu.Append(("</li>"));



                    //if (ShowPage("EmployeeList.aspx"))
                    //{
                    //    strmenu.Append(("<li class='nk-menu-item'>"));
                    //    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/EmployeeList.aspx' class='nk-menu-link'>"));
                    //    strmenu.Append(("<span class='nk-menu-text'>" + Resources.Pages.EmployeeList + "</span>"));
                    //    strmenu.Append(("</a>"));
                    //    strmenu.Append(("</li>"));

                    //}

                    //if (ShowPage("lookups.aspx?tableName=D_JobTitle"))
                    //{
                    //    strmenu.Append(("<li class='nk-menu-item'>"));
                    //    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_JobTitle' class='nk-menu-link'>"));
                    //    strmenu.Append(("<span class='nk-menu-text'>" + Resources.Pages.D_JobTitle + "</span>"));
                    //    strmenu.Append(("</a>"));
                    //    strmenu.Append(("</li>"));

                    //}

                    strmenu.Append("</ul> ");
                    strmenu.Append("</li>");

                }

                if (ShowPage("Locations.aspx"))
                {


                    strmenu.Append("<li class='nk-menu-item has-sub'>");
                    strmenu.Append("<a href = '#' class='nk-menu-link nk-menu-toggle' data-original-title='' title=''>");
                    strmenu.Append("<span class='nk-menu-icon'><em class='icon ni ni-map-pin'></em></span>");
                    strmenu.Append(" <span class='nk-menu-text'>" + Resources.Pages.LocationManagement + "</span>");
                    strmenu.Append("</a>");
                    strmenu.Append("<ul class='nk-menu-sub'>");


                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/LocationsList.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("<span class='nk-menu-text'>" + Resources.Pages.LocationsList + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));



                    if (ShowPage("lookups.aspx?tableName=D_LocationType"))
                    {
                        strmenu.Append(("<li class='nk-menu-item'>"));
                        strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_LocationType' class='nk-menu-link'>"));
                        strmenu.Append(("<span class='nk-menu-text'>" + Resources.Pages.D_LocationTypes + "</span>"));
                        strmenu.Append(("</a>"));
                        strmenu.Append(("</li>"));

                    }

                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/StoreList.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("<span class='nk-menu-text'>" + Resources.Pages.StoreList + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));


                    strmenu.Append("</ul> ");
                    strmenu.Append("</li>");


                }

                if (ShowPage("ItemsCard.aspx"))
                {


                    strmenu.Append("<li class='nk-menu-item has-sub'>");
                    strmenu.Append("<a href = '#' class='nk-menu-link nk-menu-toggle' data-original-title='' title=''>");
                    strmenu.Append("<span class='nk-menu-icon'><em class='icon ni ni-grid-add-c'></em></span>");
                    strmenu.Append(" <span class='nk-menu-text'>" + Resources.Pages.ItemsCardDef + "</span>");
                    strmenu.Append("</a>");
                    strmenu.Append("<ul class='nk-menu-sub'>");



                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/ItemsCard.aspx' class='nk-menu-link'>"));
                    strmenu.Append((" <span class='nk-menu-text'>" + Resources.Pages.ItemsCard + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));


                    if (ShowPage("ItemsCategory.aspx"))
                    {
                        strmenu.Append(("<li class='nk-menu-item'>"));
                        strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/ItemsCategory.aspx' class='nk-menu-link'>"));
                        strmenu.Append((" <span class='nk-menu-text'>" + Resources.Pages.ItemsCategory + "</span>"));
                        strmenu.Append(("</a>"));
                        strmenu.Append(("</li>"));
                    }

                    //if (ShowPage("lookups.aspx?tableName=D_ItemStatus"))
                    //{

                    //    strmenu.Append(("<li class='nk-menu-item'>"));
                    //    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_ItemStatus' class='nk-menu-link'>"));
                    //    strmenu.Append(("<span class='nk-menu-text'>" + Resources.Pages.D_ItemStatus + "</span>"));
                    //    strmenu.Append(("</a>"));
                    //    strmenu.Append(("</li>"));
                    //}

                    if (ShowPage("lookups.aspx?tableName=D_QtyUnit"))
                    {
                        strmenu.Append(("<li class='nk-menu-item'>"));
                        strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_QtyUnit' class='nk-menu-link'>"));
                        strmenu.Append(("<span class='nk-menu-text'>" + Resources.Pages.D_QtyUnit + "</span>"));
                        strmenu.Append(("</a>"));
                        strmenu.Append(("</li>"));

                    }


                    if (ShowPage("ItemsCard.aspx"))
                    {
                        strmenu.Append(("<li class='nk-menu-item'>"));
                        strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/ItemsCardMasterPrice.aspx' class='nk-menu-link'>"));
                        strmenu.Append(("<span class='nk-menu-text'>" + Resources.Pages.ItemCardMasterPrice + "</span>"));
                        strmenu.Append(("</a>"));
                        strmenu.Append(("</li>"));

                    }



                    strmenu.Append("</ul> ");
                    strmenu.Append("</li>");

                }



                //**********************************ITEMS Details
                if (ShowPage("VendorList.aspx"))
                {

                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/VendorList.aspx?t=1' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-users-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.VendorList + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));

                }


                if (ShowPage("lookups.aspx?tableName=D_Country"))
                {

                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_Country' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-property-add'></em></span><span class='nk-menu-text'>" + Resources.Pages.D_Country + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));

                }



                if (ShowPage("lookups.aspx?tableName=D_AttachmentType"))
                {
                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_AttachmentType' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-property-add'></em></span><span class='nk-menu-text'>" + Resources.Pages.AttachmentTypes + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));
                }

                //strmenu.Append("<li class='nk-menu-hr'></li>");


                //if (ShowPage("lookups.aspx?tableName=D_InboundType"))
                //{

                //    strmenu.Append(("<li class='nk-menu-item'>"));
                //    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_InboundType' class='nk-menu-link'>"));
                //    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-property-add'></em></span><span class='nk-menu-text'>" + Resources.Pages.D_InboundType + "</span>"));
                //    strmenu.Append(("</a>"));
                //    strmenu.Append(("</li>"));

                //}
                //if (ShowPage("lookups.aspx?tableName=D_InboundDepositeStatusType"))
                //{

                //    strmenu.Append(("<li class='nk-menu-item'>"));
                //    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_InboundDepositeStatusType' class='nk-menu-link'>"));
                //    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-property-add'></em></span><span class='nk-menu-text'>" + Resources.Pages.D_InboundDepositeStatusType + "</span>"));
                //    strmenu.Append(("</a>"));
                //    strmenu.Append(("</li>"));

                //}


                //if (ShowPage("lookups.aspx?tableName=D_OutboundType"))
                //{

                //    strmenu.Append(("<li class='nk-menu-item'>"));
                //    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_OutboundType' class='nk-menu-link'>"));
                //    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-property-add'></em></span><span class='nk-menu-text'>" + Resources.Pages.D_OutboundType + "</span>"));
                //    strmenu.Append(("</a>"));
                //    strmenu.Append(("</li>"));

                //}
                //if (ShowPage("lookups.aspx?tableName=D_OutboundwithdrawStatus"))
                //{

                //    strmenu.Append(("<li class='nk-menu-item'>"));
                //    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_OutboundwithdrawStatus' class='nk-menu-link'>"));
                //    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-property-add'></em></span><span class='nk-menu-text'>" + Resources.Pages.D_OutboundwithdrawStatus + "</span>"));
                //    strmenu.Append(("</a>"));
                //    strmenu.Append(("</li>"));

                //}




                strmenu.Append(("</ul>"));
                strmenu.Append(("</div>"));


            }

            // End of module
            if (ShowSystem("3")) // Inbound Opertaon
            {


                strmenu.Append(("<div class='nk-menu-content' data-content='navInbound'>"));
                strmenu.Append(("<h5 class='title'>" + Resources.Pages.ReceivingOperations + "</h5>"));
                strmenu.Append(("<ul class='nk-menu'>"));





                if (ShowPage("InboundOperrations.aspx"))
                {

                    strmenu.Append("<li class='nk-menu-item has-sub'>");
                    strmenu.Append("<a href = '#' class='nk-menu-link nk-menu-toggle' data-original-title='' title=''>");
                    strmenu.Append("<span class='nk-menu-icon'><em class='icon ni ni-minimize-alt'></em></span>");
                    strmenu.Append("<span class='nk-menu-text'>" + Resources.Pages.InboundOperationsMenu + "</span>");
                    strmenu.Append("</a>");
                    strmenu.Append("<ul class='nk-menu-sub'>");

                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Inboud/InboundOperrations.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-property-add'></em></span><span class='nk-menu-text'>" + Resources.Pages.InboundOperations + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));

                    if (ShowPage("InboundItemReceving.aspx"))
                    {
                        strmenu.Append(("<li class='nk-menu-item'>"));
                        strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Inboud/InboundItemReceving.aspx' class='nk-menu-link'>"));
                        strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-calc'></em></span><span class='nk-menu-text'>" + Resources.Pages.InboundItemReceving + "</span>"));
                        strmenu.Append(("</a>"));
                        strmenu.Append(("</li>"));
                    }
                    if (ShowPage("OutboundItemReceiving.aspx"))
                    {
                        strmenu.Append(("<li class='nk-menu-item'>"));
                        strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Inboud/OutboundItemReceiving.aspx' class='nk-menu-link'>"));
                        strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-calc'></em></span><span class='nk-menu-text'>" + Resources.Pages.OutboundItemReceiving + "</span>"));
                        strmenu.Append(("</a>"));
                        strmenu.Append(("</li>"));
                    }

                    if (ShowPage("InboundOperrations.aspx"))
                    {
                        strmenu.Append(("<li class='nk-menu-item'>"));
                        strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Inboud/InboundOperrations.aspx?type=3' class='nk-menu-link'>"));
                        strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-wallet-in'></em></span><span class='nk-menu-text'>" + Resources.Pages.CustodyReceive + "</span>"));
                        strmenu.Append(("</a>"));
                        strmenu.Append(("</li>"));
                    }

                    if (ShowPage("InboundList.aspx"))
                    {
                        strmenu.Append(("<li class='nk-menu-item'>"));
                        strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Inboud/InboundList.aspx' class='nk-menu-link'>"));
                        strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-list-index-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.InboundList + "</span>"));
                        strmenu.Append(("</a>"));
                        strmenu.Append(("</li>"));
                    }

                    if (ShowPage("ManageStoreRequest.aspx"))
                    {
                        strmenu.Append(("<li class='nk-menu-item'>"));
                        strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Inboud/ManageStoreRequest.aspx' class='nk-menu-link'>"));
                        strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-list-index-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.ManageStoreRequest + "</span>"));
                        strmenu.Append(("</a>"));
                        strmenu.Append(("</li>"));
                    }

                    strmenu.Append("</ul>");
                    strmenu.Append("</li>");

                }


                strmenu.Append("<li class='nk-menu-item has-sub'>");
                strmenu.Append("<a href = '#' class='nk-menu-link nk-menu-toggle' data-original-title='' title=''>");
                strmenu.Append("<span class='nk-menu-icon'><em class='icon ni ni-maximize-alt'></em></span>");
                strmenu.Append("<span class='nk-menu-text'>" + Resources.Pages.OutBoundOperationMenu + "</span>");
                strmenu.Append("</a>");
                strmenu.Append("<ul class='nk-menu-sub'>");

                if (ShowPage("InboundOperrations.aspx"))
                {
                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Outbound/OutboundOperrations.aspx?t=3' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-tranx-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.StoreTransfer + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));
                }




                if (ShowPage("OutboundItemDelivery.aspx"))
                {
                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/outbound/OutboundItemDelivery.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-wallet-out'></em></span><span class='nk-menu-text'>" + Resources.Pages.OutboundItemDelivery + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));
                }


                if (ShowPage("OutboundList.aspx"))
                {
                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Outbound/OutboundListAll.aspx?all=1' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-list-index-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.OutboundList + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));
                }


                strmenu.Append("</ul>");
                strmenu.Append("</li>");








                strmenu.Append(("</ul>"));
                strmenu.Append(("</div>"));
            }

            if (ShowSystem("4")) // outbound Opertaon
            {

                strmenu.Append(("<div class='nk-menu-content' data-content='navOutbound'>"));
                strmenu.Append(("<h5 class='title'>" + Resources.Pages.DeliverOperations + "</h5>"));
                strmenu.Append(("<ul class='nk-menu'>"));




                strmenu.Append("<li class='nk-menu-item has-sub'>");
                strmenu.Append("<a href = '#' class='nk-menu-link nk-menu-toggle' data-original-title='' title=''>");
                strmenu.Append("<span class='nk-menu-icon'><em class='icon ni ni-minimize-alt'></em></span>");
                strmenu.Append("<span class='nk-menu-text'>" + Resources.Pages.DeliverOperations + "</span>");
                strmenu.Append("</a>");
                strmenu.Append("<ul class='nk-menu-sub'>");




                if (ShowPage("AssetDetails.aspx"))
                {
                    strmenu.Append(("<li class='nk-menu-item' style='display:none' >"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/Assets/AssetDetails.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-slack'></em></span><span class='nk-menu-text'>" + Resources.Pages.CustodyItems + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));
                }


                if (ShowPage("AssetCheckout.aspx"))
                {
                    //strmenu.Append(("<li class='nk-menu-item'>"));
                    //strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/Assets/AssetCheckout.aspx?t=1' class='nk-menu-link'>"));
                    //strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-user-list-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.CustodyAddAll + "</span>"));
                    //strmenu.Append(("</a>"));
                    //strmenu.Append(("</li>"));

                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/Assets/AssetCheckout.aspx?t=1' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-user-list-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.CustodyAdd + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));


                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/Assets/AssetCheckout.aspx?t=2' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-focus'></em></span><span class='nk-menu-text'>" + Resources.Pages.CustodyAdd1 + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));

                }


                if (ShowPage("AssetCheckin.aspx"))
                {
                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/Assets/AssetCheckin.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-article'></em></span><span class='nk-menu-text'>" + Resources.Pages.CustodyCheckIn + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));
                }

                if (ShowPage("AssetTransfer.aspx"))
                {
                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/Assets/AssetTransfer.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-exchange'></em></span><span class='nk-menu-text'>" + Resources.Pages.AssetTransfer + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));
                }


                if (ShowPage("AssetsRequestList.aspx"))
                {
                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/Assets/AssetsRequestList.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-list-thumb-alt'></em></span><span class='nk-menu-text'>" + Resources.Pages.AssetsRequestList + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));
                }
                //if (ShowPage("AssetsList.aspx"))
                //{
                //    strmenu.Append(("<li class='nk-menu-item'>"));
                //    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/AssetsList.aspx' class='nk-menu-link'>"));
                //    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-slack'></em></span><span class='nk-menu-text'>" + Resources.Pages.CustodyItems + "</span>"));
                //    strmenu.Append(("</a>"));
                //    strmenu.Append(("</li>"));
                //}
                strmenu.Append("</ul>");
                strmenu.Append("</li>");



                if (ShowPage("InboundOperrations.aspx"))
                {

                    strmenu.Append("<li class='nk-menu-item has-sub'>");
                    strmenu.Append("<a href = '#' class='nk-menu-link nk-menu-toggle' data-original-title='' title=''>");
                    strmenu.Append("<span class='nk-menu-icon'><em class='icon ni ni-minimize-alt'></em></span>");
                    strmenu.Append("<span class='nk-menu-text'>" + Resources.Pages.InboundOperationsMenu + "</span>");
                    strmenu.Append("</a>");
                    strmenu.Append("<ul class='nk-menu-sub'>");

                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Inboud/InboundOperrations.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-property-add'></em></span><span class='nk-menu-text'>" + Resources.Pages.InboundOperations + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));



                    if (ShowPage("InboundItemReceving.aspx"))
                    {
                        strmenu.Append(("<li class='nk-menu-item'>"));
                        strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Inboud/InboundItemReceving.aspx' class='nk-menu-link'>"));
                        strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-calc'></em></span><span class='nk-menu-text'>" + Resources.Pages.InboundItemReceving + "</span>"));
                        strmenu.Append(("</a>"));
                        strmenu.Append(("</li>"));
                    }


                    if (ShowPage("InboundList.aspx"))
                    {
                        strmenu.Append(("<li class='nk-menu-item'>"));
                        strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Inboud/InboundList.aspx' class='nk-menu-link'>"));
                        strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-list-index-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.InboundList + "</span>"));
                        strmenu.Append(("</a>"));
                        strmenu.Append(("</li>"));
                    }

                    if (ShowPage("ManageStoreRequest.aspx"))
                    {
                        strmenu.Append(("<li class='nk-menu-item'>"));
                        strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Inboud/ManageStoreRequest.aspx' class='nk-menu-link'>"));
                        strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-list-index-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.ManageStoreRequest + "</span>"));
                        strmenu.Append(("</a>"));
                        strmenu.Append(("</li>"));
                    }

                    strmenu.Append("</ul>");
                    strmenu.Append("</li>");

                }





                strmenu.Append(("</ul>"));
                strmenu.Append(("</div>"));
            }

            // End of module
            if (ShowSystem("5"))
            {

                strmenu.Append(("<div class='nk-menu-content' data-content='navReports'>"));
                strmenu.Append(("<h5 class='title'>" + Resources.Pages.SystemReports + "</h5>"));
                strmenu.Append(("<ul class='nk-menu'>"));

                if (ShowPage("StocktakingReport.aspx"))
                {
                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/Reports/StocktakingReport.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-property-add'></em></span><span class='nk-menu-text'>" + Resources.Pages.StocktakingReporttitle + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));
                }
                if (ShowPage("AssetsCostDetails.aspx"))
                {
                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/Reports/AssetsCostDetails.aspx' class='nk-menu-link'>")); 
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-property-add'></em></span><span class='nk-menu-text'>" + "البيانات التفصيلية للاصول الواردة" + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));
                }
                if (ShowPage("AssetsCostCompare.aspx"))
                {
                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/Reports/AssetsCostCompare.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-property-add'></em></span><span class='nk-menu-text'>" + "مقارنة البيانات التفصيلية للاصول الواردة " + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));
                }


                strmenu.Append(("</ul>"));
                strmenu.Append(("</div>"));
            }

            // End of module
            if (ShowSystem("1"))
            {


                strmenu.Append(("<div class='nk-menu-content' data-content='navUsers'>"));
                strmenu.Append(("<h5 class='title'>" + Resources.Pages.SystemSecurity + "</h5>"));
                strmenu.Append(("<ul class='nk-menu'>"));



                if (ShowPage("AdminManager.aspx"))
                {

                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/admin/pages/AdminManager.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-user-list'></em></span><span class='nk-menu-text'>" + Resources.Pages.userList + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));

                }


                if (ShowPage("PermissionsNew.aspx"))
                {
                    strmenu.Append(("<li class='nk-menu-item'>"));
                    strmenu.Append(("<a href=\'" + Resources.Utilities.cutureRoute + "/admin/pages/PermissionsNew.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("    <span class='nk-menu-icon'><em class='icon ni ni-unlock'></em></span><span class='nk-menu-text'>" + Resources.Pages.UserPermissions + "</span>"));
                    strmenu.Append(("</a>"));
                    strmenu.Append(("</li>"));

                }

                strmenu.Append(("</ul>"));
                strmenu.Append(("</div>"));
            }

            return strmenu.ToString();
        }

        public string FIllPublicAnnouncements()
        {
            string _out = "";
            //string CompanyID = "0";
            //if ((!(Session("CompanyID") == null) 
            //            && !Session("CompanyID").Equals("0"))) {
            //    CompanyID = Session("CompanyID");
            //}

            //DataSet ds = NewsData.ins.GetItems(CompanyID, ,, "1");
            //if ((!(ds == null) 
            //            && !(ds.Tables[0].Rows.Count == 0))) {
            //    for (int i = 0; (i 
            //                <= (ds.Tables[0].Rows.Count - 1)); i++) {
            //        _out = (_out + (("<li>" 
            //                    + (gets(ds.Tables[0].Rows[i][Resources.Pages.TitleFiled]) + "</li>"))));
            //    }

            //}
            //else {
            //    _out = (_out + (" <li>Welcome to �Portal - Restaurant Management System</li>"));
            //    _out = (_out + ("  <li>You can manage your business in simple way .</li>"));
            //}
            //////_out +=" <li>Welcome to   Portal - Port Management System</li>";
            //////  _out +="<li>You can manage your business in simple way .</li>";
            return _out;
        }

        private void FillPermissions(int? adminType, int adminID)
        {
            ArrayList UserPermssionDetails = MemberShip_Permission.ins.SetUserPermission(adminType, adminID);


            Hashtable tbl = new Hashtable();
            Hashtable sys = new Hashtable();

            ViewState["Permission"] = (Hashtable)UserPermssionDetails[0];
            Session["Permission"] = (Hashtable)UserPermssionDetails[0];
            ViewState["System"] = (Hashtable)UserPermssionDetails[1];
            Session["System"] = (Hashtable)UserPermssionDetails[1];
        }

        public string ShowAlerts()
        {
            string _outList = "";


            //DataSet ds = CompaniesFeedbacks.ins.getNewMessages(CompanyID);
            //if ((!(ds == null)
            //            && !(ds.Tables[0].Rows.Count == 0)))
            //{
            //    _NewMessagesCount = gets(ds.Tables[0].Rows.Count);
            //    _NewMessagesText = ("You have "
            //                + (gets(ds.Tables[0].Rows.Count) + " new messages"));
            //    for (int i = 0; (i
            //                <= (ds.Tables[0].Rows.Count - 1)); i++)
            //    {
            //        _outList = (_outList + ((" <li><a href=\"javascript:void(0)\" onclick=\"window.open(\'MessageDetails.aspx?id="
            //                    + (gets(ds.Tables[0].Rows[i]["code"]) + ("\', \'order\', \'height=600,width=800,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=no,menubar=no" +
            //                    ",location=no,directories=no, status=no\');\" ><span class=\'avatar\'><img src=\'/uploads/"
            //                    + (gets(ds.Tables[0].Rows[i]["Com_logo"]) + ("\' alt=\'\' class=\'img-responsive img-circle\' /></span><span class=\'info\'><span class=\'name\'>"
            //                    + (gets(ds.Tables[0].Rows[i]["CustomerName"]) + ("</span><span class=\'desc\'>"
            //                    + (StringLimit(gets(ds.Tables[0].Rows[i]["CustomerMsg"]), 30) + "</span></span></a></li>"))))))))));
            //    }

            //}

            return _outList;
        }
        protected string gets(object obj)
        {
            if ((obj == DBNull.Value))
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }

        }
        public bool ShowPage(string url)
        {
            Hashtable tbl = ((Hashtable)(ViewState["Permission"]));
            url = url.ToLower();
            //if ((url.IndexOf("?") != -1))
            //{
            //    url = url.Substring(0, url.IndexOf("?"));
            //}

            if ((tbl[url] == null))
            {
                return false;
            }

            return bool.Parse(tbl[url].ToString());
        }
        public bool ShowSystem(string systemid)
        {
            Hashtable tbl = ((Hashtable)(ViewState["System"]));
            if ((tbl[systemid] == null))
            {
                return false;
            }

            return bool.Parse(tbl[systemid].ToString());
        }

        public string StringLimit(string str, int limit)
        {
            if ((str.Length <= limit))
            {
                return (str + " ");
            }

            string s = str.Substring(0, limit);
            if ((s.LastIndexOf(" ") != -1))
            {
                s = s.Substring(0, s.LastIndexOf(" "));
            }

            s += " ...";
            return s;
        }

        protected void lnkEnglish_Click(object sender, EventArgs e)
        {

            Response.Redirect("/" + Resources.Utilities.langrouter + Request.Url.PathAndQuery);

        }

        protected void lnkArabic_Click(object sender, EventArgs e)
        {

            Response.Redirect("/" + Resources.Utilities.langrouter + Request.Url.PathAndQuery);
        }

        protected void lnkLang_Click(object sender, EventArgs e)
        {
            Response.Redirect("/" + Resources.Utilities.langrouter + Request.Url.PathAndQuery);
        }

        public bool GetBool(object st)
        {
            if ((st == DBNull.Value))
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(Convert.ToInt16(st));
            }

        }
        #endregion
    }
}