using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using DomainInterface;
using Infrastructure;
using System.Web.Http;
using System.Web.Routing;
using System.Net.Http.Formatting;

namespace UI.Web
{
    public class Global : System.Web.HttpApplication
    {
        //IAdminRepository Reposatory = IoC.Resolve<IAdminRepository>();

        protected void Application_Start(object sender, EventArgs e)
        {    
            //initialize IOC 
          IoC.InitializeWith(new DependencyResolverFactory());


            RouteTable.Routes.MapHttpRoute(
                            name: "DefaultApi",
                            routeTemplate: "api/{controller}/{action}/{id}",
                            defaults: new { id = System.Web.Http.RouteParameter.Optional });

         
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());
      

            // IAdminRepository objRepository = IoC.Resolve<IAdminRepository>();

            try
            {
                //var ObjVisitor = objRepository.GetTAW_Access_Levels();
                //if (ObjVisitor != null)
                //{
                //    if (ObjVisitor.Count == null)
                //    {
                //        ObjVisitor.Count = 0;
                //        objRepository.UpdateTAW_Access_Level(ObjVisitor);
                //    }
                //    else
                //    {
                //        ObjVisitor.Count = ObjVisitor.Count + 1;
                //        objRepository.UpdateTAW_Access_Level(ObjVisitor);
                //    }

                //}
            }
            catch (System.Exception)
            { }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //Session["masterpage"] = "~/Masters/WHSite.Master";
            //System.Globalization.CultureInfo CI =
            //   new System.Globalization.CultureInfo("en-US");
            //Settings.Language = CI;


        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            ////Try to log the user enter at this page
            //try
            //{
            //    System.Exception _Exception = Server.GetLastError();
            //    string strErrorr = string.Empty;
            //    if (_Exception != null && _Exception.InnerException != null)
            //        strErrorr = _Exception.InnerException.Message;

            //    HttpContext ctx = HttpContext.Current;
            //    string PageName = ctx.Request.Url.ToString();

            //    //  PageName = PageName.Substring(PageName.LastIndexOf('/') + 1);
            //    //string _strConLogger =
            //    //    ConfigurationManager.ConnectionStrings["sqlConnLogger"].ConnectionString;
            //    //Logger _Logger = new Logger(_strConLogger);

            //    //  string strUser = string.Empty;
            //    // if (ConstantUI.CurrentUser != null)
            //    //     strUser = ConstantUI.CurrentUser.Name;

            //    //  _Logger.LoggerException(PageName, strUser, strErrorr, "", "");

            //    //   if (strUser != string.Empty)
            //    string strLang = Settings.Language.Name;

            //    if (strLang.Equals("ar"))
            //    {
            //        //Response.Redirect(@"~\UIPages\ARError.aspx");
            //    }
            //    else
            //    {
            //        // Response.Redirect(@"~\UIPages\Error.aspx");
            //    }

            //}
            //catch (System.Exception)
            //{ }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request != null && HttpContext.Current.Request.Cookies["strCookieeLanguage"] != null)
                HttpContext.Current.Request.Cookies["strCookieeLanguage"].Value = null;


            if (HttpContext.Current.Request != null && HttpContext.Current.Request.Cookies["WHMPORTAL"] != null)
                HttpContext.Current.Request.Cookies["WHMPORTAL"].Value = null;

        }
    }
}