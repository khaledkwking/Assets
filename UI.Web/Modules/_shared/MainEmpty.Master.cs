using Infrastructure.DAL.Model.DB;
using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;
using UI.Web.Core;

namespace UI.Web.Modules._shared
{
    public partial class MainEmpty : System.Web.UI.MasterPage
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
                //PrfilePhoto = Resources.Utilities.Assetspath + "uploads/Adminprofile/" + user.AdminPhoto.ToString();
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



            if (ShowSystem("3"))
            {//Inbound
                _out += " <li class='nk-menu-item'>";
                _out += " <a href = '#' class='nk-menu-link nk-menu-switch' data-target='navInbound'>";
                _out += "<span class='nk-menu-icon'><em class='icon ni ni-chevrons-right'></em></span>";
                _out += "</a>";
                _out += "</li>";

            }
            if (ShowSystem("4"))
            {//OutBound
                _out += " <li class='nk-menu-item'>";
                _out += " <a href = '#' class='nk-menu-link nk-menu-switch' data-target='navOutbound'>";
                _out += "<span class='nk-menu-icon'><em class='icon ni ni-chevrons-left'></em></span>";
                _out += "</a>";
                _out += "</li>";

            }

            if (ShowSystem("5"))
            {//Reports
                _out += " <li class='nk-menu-item'>";
                _out += " <a href = '#' class='nk-menu-link nk-menu-switch' data-target='navReports'>";
                _out += "<span class='nk-menu-icon'><em class='icon ni ni-files'></em></span>";
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
            {


                strmenu.Append(("\r\n" + "<div class='nk-menu-content  menu-active' data-content='navMaster'>"));
                strmenu.Append(("\r\n" + "<h5 class='title'>" + Resources.Pages.PortalSetting + "</h5>"));
                strmenu.Append(("\r\n" + "<ul class='nk-menu'>"));

                if (ShowPage("ItemsCard.aspx"))
                {
                    strmenu.Append(("\r\n" + "<li class='nk-menu-item'>"));
                    strmenu.Append(("\r\n" + "<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/ItemsCard.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("\r\n" + "    <span class='nk-menu-icon'><em class='icon ni ni-template-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.ItemsCard + "</span>"));
                    strmenu.Append(("\r\n" + "</a>"));
                    strmenu.Append(("\r\n" + "</li>"));
                }

                if (ShowPage("ItemsCategory.aspx"))
                {

                    strmenu.Append(("\r\n" + "<li class='nk-menu-item'>"));
                    strmenu.Append(("\r\n" + "<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/ItemsCategory.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("\r\n" + "    <span class='nk-menu-icon'><em class='icon ni ni-tile-thumb-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.ItemsCategory + "</span>"));
                    strmenu.Append(("\r\n" + "</a>"));
                    strmenu.Append(("\r\n" + "</li>"));
                }

                if (ShowPage("lookups.aspx?tableName=D_ItemStatus"))
                {

                    strmenu.Append(("\r\n" + "<li class='nk-menu-item'>"));
                    strmenu.Append(("\r\n" + "<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_ItemStatus' class='nk-menu-link'>"));
                    strmenu.Append(("\r\n" + "    <span class='nk-menu-icon'><em class='icon ni ni-table-view-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.D_ItemStatus + "</span>"));
                    strmenu.Append(("\r\n" + "</a>"));
                    strmenu.Append(("\r\n" + "</li>"));
                }


                if (ShowPage("lookups.aspx?tableName=D_QtyUnit"))
                {
                    strmenu.Append(("\r\n" + "<li class='nk-menu-item'>"));
                    strmenu.Append(("\r\n" + "<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_QtyUnit' class='nk-menu-link'>"));
                    strmenu.Append(("\r\n" + "    <span class='nk-menu-icon'><em class='icon ni ni-table-view-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.D_QtyUnit + "</span>"));
                    strmenu.Append(("\r\n" + "</a>"));
                    strmenu.Append(("\r\n" + "</li>"));

                }

                strmenu.Append("<li class='nk-menu-hr'></li>");


                //**********************************ITEMS Details
                if (ShowPage("VendorList.aspx"))
                {

                    strmenu.Append(("\r\n" + "<li class='nk-menu-item'>"));
                    strmenu.Append(("\r\n" + "<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/VendorList.aspx?t=1' class='nk-menu-link'>"));
                    strmenu.Append(("\r\n" + "    <span class='nk-menu-icon'><em class='icon ni ni-users-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.VendorList + "</span>"));
                    strmenu.Append(("\r\n" + "</a>"));
                    strmenu.Append(("\r\n" + "</li>"));

                }


                if (ShowPage("lookups.aspx?tableName=D_Country"))
                {

                    strmenu.Append(("\r\n" + "<li class='nk-menu-item'>"));
                    strmenu.Append(("\r\n" + "<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_Country' class='nk-menu-link'>"));
                    strmenu.Append(("\r\n" + "    <span class='nk-menu-icon'><em class='icon ni ni-table-view-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.D_Country + "</span>"));
                    strmenu.Append(("\r\n" + "</a>"));
                    strmenu.Append(("\r\n" + "</li>"));

                }



                if (ShowPage("lookups.aspx?tableName=D_AttachmentType"))
                {
                    strmenu.Append(("\r\n" + "<li class='nk-menu-item'>"));
                    strmenu.Append(("\r\n" + "<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_AttachmentType' class='nk-menu-link'>"));
                    strmenu.Append(("\r\n" + "    <span class='nk-menu-icon'><em class='icon ni ni-table-view-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.AttachmentTypes + "</span>"));
                    strmenu.Append(("\r\n" + "</a>"));
                    strmenu.Append(("\r\n" + "</li>"));
                }

                strmenu.Append("<li class='nk-menu-hr'></li>");


                if (ShowPage("lookups.aspx?tableName=D_InboundType"))
                {

                    strmenu.Append(("\r\n" + "<li class='nk-menu-item'>"));
                    strmenu.Append(("\r\n" + "<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_InboundType' class='nk-menu-link'>"));
                    strmenu.Append(("\r\n" + "    <span class='nk-menu-icon'><em class='icon ni ni-table-view-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.D_InboundType + "</span>"));
                    strmenu.Append(("\r\n" + "</a>"));
                    strmenu.Append(("\r\n" + "</li>"));

                }
                if (ShowPage("lookups.aspx?tableName=D_InboundDepositeStatusType"))
                {

                    strmenu.Append(("\r\n" + "<li class='nk-menu-item'>"));
                    strmenu.Append(("\r\n" + "<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_InboundDepositeStatusType' class='nk-menu-link'>"));
                    strmenu.Append(("\r\n" + "    <span class='nk-menu-icon'><em class='icon ni ni-table-view-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.D_InboundDepositeStatusType + "</span>"));
                    strmenu.Append(("\r\n" + "</a>"));
                    strmenu.Append(("\r\n" + "</li>"));

                }
                strmenu.Append("<li class='nk-menu-hr'></li>");

                if (ShowPage("lookups.aspx?tableName=D_LocationType"))
                {

                    strmenu.Append(("\r\n" + "<li class='nk-menu-item'>"));
                    strmenu.Append(("\r\n" + "<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_LocationType' class='nk-menu-link'>"));
                    strmenu.Append(("\r\n" + "    <span class='nk-menu-icon'><em class='icon ni ni-table-view-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.D_LocationTypes + "</span>"));
                    strmenu.Append(("\r\n" + "</a>"));
                    strmenu.Append(("\r\n" + "</li>"));

                }

                if (ShowPage("Locations.aspx"))
                {

                    strmenu.Append(("\r\n" + "<li class='nk-menu-item'>"));
                    strmenu.Append(("\r\n" + "<a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/Locations.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("\r\n" + "    <span class='nk-menu-icon'><em class='icon ni ni-brick-fill'></em></span><span class='nk-menu-text'>" + Resources.Pages.orgChart + "</span>"));
                    strmenu.Append(("\r\n" + "</a>"));
                    strmenu.Append(("\r\n" + "</li>"));

                }


                strmenu.Append(("\r\n" + "</ul>"));
                strmenu.Append(("\r\n" + "</div>"));


            }

            // End of module
            if (ShowSystem("3")) // Inbound Opertaon
            {


                strmenu.Append(("\r\n" + "<div class='nk-menu-content' data-content='navInbound'>"));
                strmenu.Append(("\r\n" + "<h5 class='title'>" + Resources.Pages.ReceivingOperations + "</h5>"));
                strmenu.Append(("\r\n" + "<ul class='nk-menu'>"));



                //if (ShowPage("InboundOperrations.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Purchase/PurchaseOrderOperations.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                //        "tle\'>" + Resources.Pages.NewPurchaseOrder + "</span></a></li>"));
                //}

                //if (ShowPage("InboundOperrations.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Inboud/InboundOperrations.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                //        "tle\'>" + Resources.Pages.NewReceiving + "</span></a></li>"));
                //}

                //if (ShowPage("InboundItemReceving.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Inboud/InboundItemReceving.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                //        "tle\'>" + Resources.Pages.RequestsReceiving + " </span></a></li>"));
                //}




                //if (ShowPage("InboundList.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Inboud/InboundList.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                //        "tle\'> " + Resources.Pages.ReceivingList + " </span></a></li>"));
                //}
                //if (ShowPage("InboundList.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Purchase/PurchaseList.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                //        "tle\'> " + Resources.Pages.PurchaseList + " </span></a></li>"));
                //}


                strmenu.Append(("\r\n" + "</ul>"));
                strmenu.Append(("\r\n" + "</div>"));
            }

            if (ShowSystem("4")) // outbound Opertaon
            {

                strmenu.Append(("\r\n" + "<div class='nk-menu-content' data-content='navOutbound'>"));
                strmenu.Append(("\r\n" + "<h5 class='title'>" + Resources.Pages.DeliverOperations + "</h5>"));
                strmenu.Append(("\r\n" + "<ul class='nk-menu'>"));



                ////if (ShowPage("OutboundItems.aspx"))
                ////{
                ////    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+"/Modules/StoreOperations/Forms/Outbound/OutboundItems.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                ////        "tle\'>Deliver Goods </span></a></li>"));
                ////}

                //if (ShowPage("OutboundOperrations.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Outbound/OutboundOperrations.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                //        "tle\'>" + Resources.Pages.NewDeliveryRequest + "  </span></a></li>"));
                //}

                //if (ShowPage("OutboundList.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Forms/Outbound/OutboundList.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                //        "tle\'>" + Resources.Pages.DeliveredList + "</span></a></li>"));
                //}


                strmenu.Append(("\r\n" + "</ul>"));
                strmenu.Append(("\r\n" + "</div>"));
            }

            // End of module
            if (ShowSystem("5"))
            {

                strmenu.Append(("\r\n" + "<div class='nk-menu-content' data-content='navReports'>"));
                strmenu.Append(("\r\n" + "<h5 class='title'>" + Resources.Pages.SystemReports + "</h5>"));
                strmenu.Append(("\r\n" + "<ul class='nk-menu'>"));


                //if (ShowPage("InboundListReport.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/reports/CustomerShareReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                //        Resources.Pages.CustomerShare + " </span></a></li>"));
                //}
                //if (ShowPage("InboundListReport.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/reports/InboundItemsReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                //        Resources.Pages.ReceivingItemsListReport + " </span></a></li>"));
                //}

                //if (ShowPage("InboundListReport.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/reports/OutboundItemsReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                //        Resources.Pages.DeliverItemsListReport + " </span></a></li>"));
                //}



                //if (ShowPage("InboundListReport.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/reports/InboundListReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                //        Resources.Pages.ReceivingListReport + " </span></a></li>"));
                //}

                //if (ShowPage("InboundListReport.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/reports/OutboundListReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                //        Resources.Pages.DeliveryListReport + "</span></a></li>"));
                //}

                //if (ShowPage("StockTaking.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Reports/StocktakingReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                //         Resources.Pages.StockTakingReport + " </span></a></li>"));
                //}


                //if (ShowPage("StockTaking.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Reports/StocktakingGenetralReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                //         Resources.Pages.StockTakingGeneralReport + " </span></a></li>"));


                //    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/StoreOperations/Reports/StocktakingGenetralReport.aspx?s=3\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                //        Resources.Pages.freelocation + " </span></a></li>"));
                //}

                ////if (ShowPage("rptHome.aspx"))
                ////{
                ////    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+"/Modules/StoreOperations/Reports/rptHome.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                ////        "Reports Home </span></a></li>"));
                ////}


                strmenu.Append(("\r\n" + "</ul>"));
                strmenu.Append(("\r\n" + "</div>"));
            }

            // End of module
            if (ShowSystem("1"))
            {


                strmenu.Append(("\r\n" + "<div class='nk-menu-content' data-content='navUsers'>"));
                strmenu.Append(("\r\n" + "<h5 class='title'>" + Resources.Pages.SystemSecurity + "</h5>"));
                strmenu.Append(("\r\n" + "<ul class='nk-menu'>"));



                if (ShowPage("AdminManager.aspx"))
                {

                    strmenu.Append(("\r\n" + "<li class='nk-menu-item'>"));
                    strmenu.Append(("\r\n" + "<a href=\'" + Resources.Utilities.cutureRoute + "/admin/pages/AdminManager.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("\r\n" + "    <span class='nk-menu-icon'><em class='icon ni ni-user-list'></em></span><span class='nk-menu-text'>" + Resources.Pages.userList + "</span>"));
                    strmenu.Append(("\r\n" + "</a>"));
                    strmenu.Append(("\r\n" + "</li>"));

                }


                if (ShowPage("PermissionsNew.aspx"))
                {
                    strmenu.Append(("\r\n" + "<li class='nk-menu-item'>"));
                    strmenu.Append(("\r\n" + "<a href=\'" + Resources.Utilities.cutureRoute + "/admin/pages/PermissionsNew.aspx' class='nk-menu-link'>"));
                    strmenu.Append(("\r\n" + "    <span class='nk-menu-icon'><em class='icon ni ni-unlock'></em></span><span class='nk-menu-text'>" + Resources.Pages.UserPermissions + "</span>"));
                    strmenu.Append(("\r\n" + "</a>"));
                    strmenu.Append(("\r\n" + "</li>"));

                }

                strmenu.Append(("\r\n" + "</ul>"));
                strmenu.Append(("\r\n" + "</div>"));
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
            //        _out = (_out + ("\r\n" + ("<li>" 
            //                    + (gets(ds.Tables[0].Rows[i][Resources.Pages.TitleFiled]) + "</li>"))));
            //    }

            //}
            //else {
            //    _out = (_out + ("\r\n" + " <li>Welcome to �Portal - Restaurant Management System</li>"));
            //    _out = (_out + ("\r\n" + "  <li>You can manage your business in simple way .</li>"));
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
            //        _outList = (_outList + ("\r\n" + (" <li><a href=\"javascript:void(0)\" onclick=\"window.open(\'MessageDetails.aspx?id="
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