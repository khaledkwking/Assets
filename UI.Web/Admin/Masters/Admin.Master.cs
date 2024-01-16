using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Runtime.Remoting.Contexts;
 using Infrastructure.DAL.Model.DB;
using UI.Web.Core;

namespace UI.Web.Admin.Masters
{

    public partial class Admin : System.Web.UI.MasterPage
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

        private void SetModuleDefultpagelnk()
        {

            if (Session["System"]!=null)
            {


                Hashtable tbl = ((Hashtable)(ViewState["System"]));
                ArrayList PermitedModules = new ArrayList();
                //check if table has TRUE Key of vaild Module , if it has more than one return Defult
                //Else check Module KEY 

                //Get Permited Module
                for (int i = 1; i <= tbl.Count ; i++)
                {
                    if (tbl[i.ToString()] != null)
                    {
                        if (Convert.ToBoolean(tbl[i.ToString()]))
                        {
                            PermitedModules.Add(i.ToString());
                        }
                    }
                }
               

                var moduleKeys = tbl.Keys;

                
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCulture();
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


        private void SetCulture()
        {
            //applicationPath = (Request.ApplicationPath.Length == 1) ? "" : Request.ApplicationPath;
            //if (Session["Language"] == null || Session["Language"].ToString() == "")
            //{
            //    Response.Redirect(applicationPath + "/Default.aspx", false);
            //    return;
            //}
            //string culture = Session["Language"].ToString();
            //System.Globalization.CultureInfo MyCltr = new System.Globalization.CultureInfo(culture);
            //System.Threading.Thread.CurrentThread.CurrentCulture = MyCltr;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = MyCltr;
            //if (Session["Language"].ToString() == strLangEnglish)
            //{
            //   // MasterPageID.Style[HtmlTextWriterStyle.Direction] = "ltr";
            //    menuTree.Style[HtmlTextWriterStyle.Direction] = "ltr";
            //    lblUserName.Text = CommonProperties.User.FirstNameEN + " " + CommonProperties.User.LastNameAR;
            //    imgGoogle.ImageUrl = "~/Images/Google.gif";
            //    imgMyprofile.ImageUrl = "~/Images/myprofile-on.gif";
            //    imgAbountUs.ImageUrl = "~/Images/aboutus-on.gif";
            //    imgContactus.ImageUrl = "~/Images/contactus-on.gif";
            //    imgHelp.ImageUrl = "~/Images/help-on.gif";
            //    imgArabic.ImageUrl = "~/Images/arabi-on.gif";

            //}
            //else
            //{
            //   // MasterPageID.Style[HtmlTextWriterStyle.Direction] = "rtl";
            //    menuTree.Style.Add(HtmlTextWriterStyle.Direction, "rtl");
            //    lblUserName.Text = CommonProperties.User.FirstNameEN + " " + CommonProperties.User.LastNameAR; 
            //    imgGoogle.ImageUrl = "~/Images/Google-a.gif";
            //    imgMyprofile.ImageUrl = "~/Images/myprofile-on-a.gif";
            //    imgAbountUs.ImageUrl = "~/Images/aboutus-on-a.gif";
            //    imgContactus.ImageUrl = "~/Images/contactus-on-a.gif";
            //    imgHelp.ImageUrl = "~/Images/help-on-a.gif";
            //    imgArabic.ImageUrl = "~/Images/arabi-on-a.gif";
            //}

            //lblDateTime.Text = System.DateTime.Now.ToString("dd-MM-yyyy h:mm tt");
        }

     
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


        public void FillMenu()
        {
            // /************************New Menu**************************/
            if (ShowSystem("2"))
            {
                strmenu.Append(("\r\n" + "<li><a href=\'#\'><i class=\'fa fa-desktop fa-fw\'>"));
                strmenu.Append(("\r\n" + "<div class=\'icon-bg bg-pink\'></div>"));
                strmenu.Append(("\r\n" + " </i><span class=\'menu-title\'>"+ Resources.Pages.PortalSetting + "</span><span class=\'fa arrow\'></span></a>"));
                strmenu.Append(("\r\n" + "  <ul class=\'nav nav-second-level\'>"));


                //**********************************ITEMS Details
                if (ShowPage("ItemsCard.aspx"))
                {
                    strmenu.Append(("\r\n" + "  <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/ItemsCard.aspx'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-title\'" +
                        ">" + Resources.Pages.ItemsCard + "</span></a></li>"));
                }

                if (ShowPage("ItemsCategory.aspx"))
                {
                    strmenu.Append(("\r\n" + "  <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/ItemsCategory.aspx'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-title\'" +
                        ">" + Resources.Pages.ItemsCategory + "</span></a></li>"));
                }

                if (ShowPage("lookups.aspx?tableName=D_ItemStatus"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_ItemStatus'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-title\'" +
                        ">" + Resources.Pages.D_ItemStatus + " </span></a></li>"));
                }
                //**********************************ITEMS Details

                //**********************************Inbound Master

                if (ShowPage("VendorList.aspx"))
                {
                    strmenu.Append(("\r\n" + "  <li><a href=\'"+ Resources.Utilities.cutureRoute+ "/Modules/MasterData/VendorList.aspx?t=1'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-title\'" +
                        ">"+Resources.Pages.VendorList + "</span></a></li>"));
                }


                if (ShowPage("lookups.aspx?tableName=D_Country"))
                {
                    strmenu.Append(("\r\n" + "  <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_Country'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-title\'" +
                        ">" + Resources.Pages.D_Country + " </span></a></li>"));
                }


                if (ShowPage("lookups.aspx?tableName=D_QtyUnit"))
                {
                    strmenu.Append(("\r\n" + "  <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_QtyUnit'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-title\'" +
                        ">"+ Resources.Pages.D_QtyUnit + " </span></a></li>"));
                }

                if (ShowPage("lookups.aspx?tableName=D_AttachmentType"))
                {
                    strmenu.Append(("\r\n" + "  <li><a href=\'"+ Resources.Utilities.cutureRoute+"/Modules/MasterData/lookups.aspx?tableName=D_AttachmentType'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-title\'" +
                        "> "+Resources.Pages.AttachmentTypes + " </span></a></li>"));
                }

                if (ShowPage("lookups.aspx?tableName=D_InboundType"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+ "/Modules/MasterData/lookups.aspx?tableName=D_InboundType'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-title\'" +
                        ">"+Resources.Pages.D_InboundType + " </span></a></li>"));
                }
                if (ShowPage("lookups.aspx?tableName=D_InboundDepositeStatusType"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_InboundDepositeStatusType'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-title\'" +
                        ">" + Resources.Pages.D_InboundDepositeStatusType + " </span></a></li>"));
                }
                //**********************************Inbound Master


                //**********************************Locations
                if (ShowPage("lookups.aspx?tableName=D_LocationTypes"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/lookups.aspx?tableName=D_LocationTypes'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-title\'" +
                        ">" + Resources.Pages.D_LocationTypes + " </span></a></li>"));
                }

                if (ShowPage("Locations.aspx"))
                {
                    strmenu.Append(("\r\n" + "  <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/MasterData/Locations.aspx'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-title\'" +
                        ">" + Resources.Pages.Locations + "</span></a></li>"));
                }

                //**********************************Locations

                
               

                strmenu.Append(("\r\n" + "  </ul>"));
                strmenu.Append(("\r\n" + " </li>"));
            }

            // End of module
            if (ShowSystem("3")) // Inbound Opertaon
            {
                strmenu.Append(("\r\n" + "  <li><a href=\'#\'><i class=\'fa fa-angle-double-right fa-fw\'>"));
                strmenu.Append(("\r\n" + "   <div class=\'icon-bg bg-green\'></div>"));
                strmenu.Append(("\r\n" + "  </i><span class=\'menu-title\'>"+Resources.Pages.ReceivingOperations + "</span><span class=\'fa arrow\'></span></a>"));
                strmenu.Append(("\r\n" + "  <ul class=\'nav nav-second-level\'>"));

                if (ShowPage("InboundOperrations.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/WHM/Forms/Purchase/PurchaseOrderOperations.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                        "tle\'>" + Resources.Pages.NewPurchaseOrder + "</span></a></li>"));
                }

                if (ShowPage("InboundOperrations.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+"/Modules/WHM/Forms/Inboud/InboundOperrations.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                        "tle\'>"+Resources.Pages.NewReceiving + "</span></a></li>"));
                }

                if (ShowPage("InboundItemReceving.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/WHM/Forms/Inboud/InboundItemReceving.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                        "tle\'>" + Resources.Pages.RequestsReceiving + " </span></a></li>"));
                }


                //if (ShowPage("InboundOperrations.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+"/Modules/WHM/Forms/Inboud/InboundItemReceving.aspx?d=1\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                //        "tle\'>Deposit </span></a></li>"));
                //}

                if (ShowPage("InboundList.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+"/Modules/WHM/Forms/Inboud/InboundList.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                        "tle\'> "+Resources.Pages.ReceivingList + " </span></a></li>"));
                }
                if (ShowPage("InboundList.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/WHM/Forms/Purchase/PurchaseList.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                        "tle\'> " + Resources.Pages.PurchaseList + " </span></a></li>"));
                }


                strmenu.Append(("\r\n" + "  </ul>"));
                strmenu.Append(("\r\n" + "  </li>"));
            }

            if (ShowSystem("4")) // outbound Opertaon
            {
                strmenu.Append(("\r\n" + "  <li><a href=\'#\'><i class=\'fa fa-angle-double-left fa-fw\'>"));
                strmenu.Append(("\r\n" + "   <div class=\'icon-bg bg-green\'></div>"));
                strmenu.Append(("\r\n" + "  </i><span class=\'menu-title\'>  "+Resources.Pages.DeliverOperations + "</span><span class=\'fa arrow\'></span></a>"));
                strmenu.Append(("\r\n" + "  <ul class=\'nav nav-second-level\'>"));


                //if (ShowPage("OutboundItems.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+"/Modules/WHM/Forms/Outbound/OutboundItems.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                //        "tle\'>Deliver Goods </span></a></li>"));
                //}

                if (ShowPage("OutboundOperrations.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+"/Modules/WHM/Forms/Outbound/OutboundOperrations.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                        "tle\'>"+Resources.Pages.NewDeliveryRequest + "  </span></a></li>"));
                }

                if (ShowPage("OutboundList.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+"/Modules/WHM/Forms/Outbound/OutboundList.aspx\'><i class=\'fa fa-angle-left\'></i><span class=\'submenu-ti" +
                        "tle\'>"+ Resources.Pages.DeliveredList + "</span></a></li>"));
                }

              
                strmenu.Append(("\r\n" + "  </ul>"));
                strmenu.Append(("\r\n" + "  </li>"));
            }

            // End of module
            if (ShowSystem("5"))
            {
                strmenu.Append(("\r\n" + "  <li><a href=\'#\'><i class=\'fa fa-file-o fa-fw\'>"));
                strmenu.Append(("\r\n" + "   <div class=\'icon-bg bg-grey\'></div>"));
                strmenu.Append(("\r\n" + "  </i><span class=\'menu-title\'>  "+Resources.Pages.SystemReports + "</span><span class=\'fa arrow\'></span></a>"));
                strmenu.Append(("\r\n" + "  <ul class=\'nav nav-second-level\'>"));

                if (ShowPage("InboundListReport.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/WHM/reports/CustomerShareReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                        Resources.Pages.CustomerShare + " </span></a></li>"));
                }
                if (ShowPage("InboundListReport.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+ "/Modules/WHM/reports/InboundItemsReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                        Resources.Pages.ReceivingItemsListReport + " </span></a></li>"));
                }

 if (ShowPage("InboundListReport.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+ "/Modules/WHM/reports/OutboundItemsReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                        Resources.Pages.DeliverItemsListReport + " </span></a></li>"));
                }



                if (ShowPage("InboundListReport.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+"/Modules/WHM/reports/InboundListReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                        Resources.Pages.ReceivingListReport + " </span></a></li>"));
                }

                if (ShowPage("InboundListReport.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+"/Modules/WHM/reports/OutboundListReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                        Resources.Pages.DeliveryListReport + "</span></a></li>"));
                }
                 
                if (ShowPage("StockTaking.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+ "/Modules/WHM/Reports/StocktakingReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                         Resources.Pages.StockTakingReport + " </span></a></li>"));
                }


   if (ShowPage("StockTaking.aspx"))
                {
                    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+ "/Modules/WHM/Reports/StocktakingGenetralReport.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                         Resources.Pages.StockTakingGeneralReport + " </span></a></li>"));


                    strmenu.Append(("\r\n" + " <li><a href=\'" + Resources.Utilities.cutureRoute + "/Modules/WHM/Reports/StocktakingGenetralReport.aspx?s=3\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                        Resources.Pages.freelocation + " </span></a></li>"));
                }

                //if (ShowPage("rptHome.aspx"))
                //{
                //    strmenu.Append(("\r\n" + " <li><a href=\'"+ Resources.Utilities.cutureRoute+"/Modules/WHM/Reports/rptHome.aspx\'><i class=\'fa fa-edit\'></i><span class=\'submenu-title\'>" +
                //        "Reports Home </span></a></li>"));
                //}

             
                strmenu.Append(("\r\n" + "  </ul>"));
                strmenu.Append(("\r\n" + "  </li>"));
            }

            // End of module
            if (ShowSystem("1"))
            {
                strmenu.Append(("\r\n" + " <li><a href=\'#\'><i class=\'fa fa-lock fa-fw\'>"));
                strmenu.Append(("\r\n" + " <div class=\'icon-bg bg-yellow\'></div>"));
                strmenu.Append(("\r\n" + " </i><span class=\'menu-title\'>"+Resources.Pages.SystemSecurity + "   </span><span class=\'fa arrow\'></span></a>"));
                strmenu.Append(("\r\n" + "<ul class=\'nav nav-second-level\'>"));
                if (ShowPage("AdminManager.aspx"))
                {
                    strmenu.Append(("\r\n" + "  <li><a href=\'" + Resources.Utilities.cutureRoute + "/admin/pages/AdminManager.aspx\'><i class=\'fa fa-male\'></i><span class=\'submenu-title\'>" +
                        Resources.Pages.userList+"</span></a></li>"));
                }

                // If ShowPage("AdminManager.aspx") Then
                //     strmenu.Append(vbNewLine + " <li><a href='AdminManager.aspx?type=3'><i class='fa fa-paperclip'></i><span class='submenu-title'>Call Center Users Manager</span></a></li>")
                // End If
                // If ShowPage("AdminManager.aspx") Then
                //     strmenu.Append(vbNewLine + " <li><a href='AdminManager.aspx?type=4'><i class='fa fa-paperclip'></i><span class='submenu-title'>Delivery Users Manager</span></a></li>")
                // End If
                if (ShowPage("PermissionsNew.aspx"))
                {
                    strmenu.Append(("\r\n" + "  <li><a href=\'" + Resources.Utilities.cutureRoute + "/admin/pages/PermissionsNew.aspx\'><i class=\'fa fa-unlock\'></i><span class=\'submenu-title" +
                        "\'> "+Resources.Pages.UserPermissions + " </span></a></li>"));
                }

                strmenu.Append(("\r\n" + "   </ul>"));
                strmenu.Append(("\r\n" + "   </li>"));
            }

            
        }
        //public void FillMenu()
        //{
        //    if (ShowSystem("4"))
        //    {

        //        strmenu.Append("<li class='dropdown'>");
        //        strmenu.Append("<a href = '#' class='dropdown-toggle' data-toggle='dropdown'><i class='icon-stats-dots position-left'></i>"+Resources.menu.GDB + " <span class='caret'></span></a>");
        //        strmenu.Append("<ul class='dropdown-menu width-250'>");


        //        strmenu.Append("<li class='dropdown-header'>"+ Resources.menu.GDBAnalytics +"</li>");


        //        if (ShowPage("DailyRevenueFilter.aspx"))
        //        {
        //            strmenu.Append("<li ><a href='" + Resources.Utilities.cutureRoute + ""+ Resources.Utilities.cutureRoute+"/Modules/GDB/Panels/DailyRevenueFilter.aspx' ><i class='icon-calendar'></i>  "+ Resources.menu.DailyRevenuePanel + "  </a></li>");
        //        }


        //        if (ShowPage("MonthlyRevenueRange.aspx"))
        //        {
        //            strmenu.Append("<li ><a href='" + Resources.Utilities.cutureRoute + ""+ Resources.Utilities.cutureRoute+"/Modules/GDB/Panels/MonthlyRevenueRange.aspx' ><i class='icon-calendar'></i> "+ Resources.menu.MonthlyRevenuePanel + "</a></li>");
        //        }


        //        if (ShowPage("OperationView.aspx"))
        //        {
        //            strmenu.Append(("\r\n" + "  <li><a href='" + Resources.Utilities.cutureRoute + ""+ Resources.Utilities.cutureRoute+"/Modules/GDB/Panels/OperationView.aspx'><i class='icon-cube'></i> "+Resources.menu.MonthlyVolumePanel+"</a></li>"));
        //        }

        //        if (ShowPage("DynamicFinance.aspx"))
        //        {
        //            strmenu.Append(("\r\n" + "  <li><a href='" + Resources.Utilities.cutureRoute + ""+ Resources.Utilities.cutureRoute+"/Modules/GDB/Panels/DynamicFinance.aspx'><i class=' icon-filter3'></i> "+Resources.menu.CustomizedRevenuePanel + "</a></li>"));
        //        }

        //        if (ShowPage("BudgetQuarterView.aspx"))
        //        {
        //            strmenu.Append(("\r\n" + "  <li><a href='" + Resources.Utilities.cutureRoute + ""+ Resources.Utilities.cutureRoute+"/Modules/GDB/Panels/BudgetQuarterView.aspx'><i class='icon-coins'></i> "+Resources.menu.BudgetPanel + "</a></li>"));
        //        }

        //        strmenu.Append("</ul>");
        //        strmenu.Append("</li>");


        //    }


        //    if (ShowSystem("2"))
        //    {

        //        strmenu.Append("<li class='dropdown'>");
        //        strmenu.Append("<a href = '#' class='dropdown-toggle' data-toggle='dropdown'><i class='icon-make-group position-left'></i>"+ Resources.menu.ContainerTerminal + "<span class='caret'></span></a>");
        //        strmenu.Append("<ul class='dropdown-menu width-250'>");


        //        strmenu.Append("<li class='dropdown-header'>"+Resources.menu.CTAnalytics + "</li>");


        //        if (ShowPage("CTOS_Default.aspx"))
        //        {
        //            strmenu.Append("<li ><a href='"+ Resources.Utilities.cutureRoute+""+ Resources.Utilities.cutureRoute+"/Modules/CTOS/Panels/CTOS_Default.aspx' ><i class='icon-pie5'></i> "+Resources.menu.TerminalSummery + " </a></li>");
        //        }


        //        if (ShowPage("containerAnalysis.aspx"))
        //        {
        //            strmenu.Append("<li ><a href='" + Resources.Utilities.cutureRoute + ""+ Resources.Utilities.cutureRoute+"/Modules/CTOS/Panels/containerAnalysis.aspx' ><i class='icon-grid4'></i> "+Resources.menu.ContainerAnalysis + "</a></li>");
        //        }
        //        if (ShowPage("YardAnalysis.aspx"))
        //        {
        //            strmenu.Append(("\r\n" + "  <li><a href='" + Resources.Utilities.cutureRoute + ""+ Resources.Utilities.cutureRoute+"/Modules/CTOS/Panels/YardAnalysis.aspx\'><i class='icon-list-unordered'></i> "+Resources.menu.YardCapacity + "</a></li>"));
        //        }

        //        if (ShowPage("AgentServices.aspx"))
        //        {
        //            strmenu.Append("<li ><a href='" + Resources.Utilities.cutureRoute + ""+ Resources.Utilities.cutureRoute+"/Modules/CTOS/Panels/AgentServices.aspx' ><i class='icon-people'></i>"+Resources.menu.AgentAnalysis + "</a></li>");
        //        }
        //        if (ShowPage("VesselsAnalysis.aspx"))
        //        {
        //            strmenu.Append("<li ><a href='" + Resources.Utilities.cutureRoute + ""+ Resources.Utilities.cutureRoute+"/Modules/CTOS/Panels/VesselsAnalysis.aspx' ><i class='icon-ship'></i>"+Resources.menu.VesselsAnalysis + "</a></li>");
        //        }

        //        strmenu.Append("</ul>");
        //        strmenu.Append("</li>");


        //    }



        //    if (ShowSystem("3"))
        //    {

        //        strmenu.Append("<li class='dropdown'>");
        //        strmenu.Append("<a href = '#' class='dropdown-toggle' data-toggle='dropdown'><i class='icon-strategy position-left'></i>"+Resources.menu.RT+"<span class='caret'></span></a>");
        //        strmenu.Append("<ul class='dropdown-menu width-250'>");


        //        strmenu.Append("<li class='dropdown-header'>"+Resources.menu.KPIAnalytics + "</li>");


        //        if (ShowPage("RTKPI_Default.aspx"))
        //        {
        //            strmenu.Append("<li ><a href='" + Resources.Utilities.cutureRoute + ""+ Resources.Utilities.cutureRoute+"/Modules/RTKPI/Panels/RTKPI_Default.aspx' ><i class='icon-folder-download3'></i> "+Resources.menu.ImportAnalytics + "  </a></li>");
        //        }


        //        if (ShowPage("exportpanel.aspx"))
        //        {
        //            strmenu.Append("<li ><a href='" + Resources.Utilities.cutureRoute + ""+ Resources.Utilities.cutureRoute+"/Modules/RTKPI/Panels/exportpanel.aspx' ><i class='icon-folder-upload3'></i>"+Resources.menu.ExportAnalytics + "</a></li>");
        //        }
        //        if (ShowPage("CustomPerformance.aspx"))
        //        {
        //            strmenu.Append(("\r\n" + "  <li><a href='" + Resources.Utilities.cutureRoute + ""+ Resources.Utilities.cutureRoute+"/Modules/RTKPI/Panels/CustomPerformance.aspx\'><i class='icon-stats-growth'></i> "+Resources.menu.KPI + "</a></li>"));
        //        }



        //        strmenu.Append("</ul>");
        //        strmenu.Append("</li>");


        //    }






        //    if (ShowSystem("1"))
        //    {
        //        strmenu.Append("<li class='dropdown'>");
        //        strmenu.Append("<a href = '#' class='dropdown-toggle' data-toggle='dropdown'><i class='icon-grid52'></i> "+Resources.menu.UserManagement + " <span class='caret'></span></a>");
        //        strmenu.Append("<ul class='dropdown-menu width-250'>");


        //        strmenu.Append("<li class='dropdown-header'>"+Resources.menu.PortalAccess + "</li>");



        //        if (ShowPage("AdminManager.aspx"))
        //        {
        //            strmenu.Append("<li ><a href='" + Resources.Utilities.cutureRoute + "/admin/pages/AdminManager.aspx' ><i class='icon-user-lock'></i> "+Resources.menu.AccessUsersList + "</a></li>");
        //        }
        //        if (ShowPage("ModuleManager.aspx"))
        //        {
        //            strmenu.Append("<li ><a href='" + Resources.Utilities.cutureRoute + "/admin/pages/ModuleManager.aspx' ><i class='icon-popout'></i> "+Resources.menu.ModulesList + "</a></li>");
        //        }


        //        if (ShowPage("PermissionsNew.aspx"))
        //        {
        //            strmenu.Append(("\r\n" + "  <li><a href='" + Resources.Utilities.cutureRoute + "/admin/pages/PermissionsNew.aspx\'><i class='icon-lock2'></i> "+Resources.menu.userPermission + "</a></li>"));
        //        }

        //        strmenu.Append("</ul>");
        //        strmenu.Append("</li>");
        //    }

        //}
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

        private void FillPermissions(int? adminType,int adminID)
        {
            ArrayList UserPermssionDetails = MemberShip_Permission.ins.SetUserPermission(adminType, adminID);

            
            Hashtable tbl = new Hashtable();
            Hashtable sys = new Hashtable();
           
            ViewState["Permission"] =(Hashtable) UserPermssionDetails[0];
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

            Response.Redirect("/"+Resources.Utilities.langrouter + Request.Url.PathAndQuery);

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