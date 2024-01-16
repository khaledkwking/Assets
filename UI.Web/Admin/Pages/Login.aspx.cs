using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using UI.Web.Core;

namespace UI.Web.Admin.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            imbtnlogin.Attributes.Add("onclick", "return chkImage();");


            if (!Page.IsPostBack)
            {

                if (Request.QueryString["out"] == null)
                {

                    if (MemberShip_Permission.isAuthenticationCookie())
                    {

                        if (Request.QueryString["ReturnUrl"] != null)
                        {
                            Response.Redirect(Request.QueryString["ReturnUrl"]);
                        }
                        else
                        { Response.Redirect(Resources.Utilities.cutureRoute +"/Admin/Pages/Home.aspx"); }
                      
                    }
                }
            }
        }

        

        protected void LoginButton_Click(object sender, EventArgs e)
        {

            bool blnIsValidUser =
            MemberShip_Permission.IsValidUser(txtUsername.Text.ToLower().TrimEnd(), txtPass.Text);
            if (blnIsValidUser)
            {
                try
                {
                    string UserInfo = txtUsername.Text;
                    // Create the authetication ticket
                    FormsAuthenticationTicket authTicket =
                         new FormsAuthenticationTicket(1, txtUsername.Text, DateTime.Now, DateTime.Now.AddDays(1),
                                                         true, UserInfo, FormsAuthentication.FormsCookiePath);
                    FormsIdentity identitiy = new FormsIdentity(authTicket);
                    // Now encrypt the ticket.
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    // Add the encrypted ticket to the cookie collection.
                    HttpCookie authCookie = new HttpCookie("WHMPORTAL", encryptedTicket);
                    //Add Cookie With User Name 
                    authCookie["WHMPORTAL"] = UserInfo;
                    authCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(authCookie);
                    lblError.Visible = false;
                    lblError.Text = "";


                    //check user Permission

                }

                catch (System.Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = "<div class='alert alert-danger'>Error, You are not authorized to access this site!</div>";
                }

                if (Request.QueryString["ReturnUrl"] != null)
                {
                    Response.Redirect(Request.QueryString["ReturnUrl"]);
                }
                else
                { Response.Redirect(Resources.Utilities.cutureRoute  +"/Admin/Pages/Home.aspx"); }

            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "<div class='alert alert-danger'>Error, "+ txtUsername.Text + " is invalid user Or Wrong Password</div>";
            }



        }
    }
}