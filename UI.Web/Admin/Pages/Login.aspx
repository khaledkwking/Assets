<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UI.Web.Admin.Pages.Login" %>

<!DOCTYPE html>
<html lang="zxx" class="js">

<head>

    <meta charset="utf-8">
    <meta name="author" content="CMGS">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content=" CMGS | Assets Managment System">
    <!-- Fav Icon  -->
    <link rel="shortcut icon" href="/wwwroot/images/favicon.png">
    <!-- Page Title  -->
    <title>Login | Assets Managment System</title>
    <!-- StyleSheets  -->
    <link rel="stylesheet" href="/wwwroot/assets/css/dashlite.rtl.css?ver=2.4.0">
    <link id="skin-default" rel="stylesheet" href="/wwwroot/assets/css/theme.css?ver=2.4.0">
    <script language="JavaScript" type="text/javascript">
        function chkImage() {
            var txt = document.getElementById("<%=txtUsername.ClientID %>")
            if (txt.value == "") {

                alert("<%=GetGlobalResourceObject("Alerts", "PleaseEnter")%> User name");
                txt.focus();
                return false;
            }

            txt = document.getElementById("<%=txtPass.ClientID %>")
            if (txt.value == "") {
                alert("<%=GetGlobalResourceObject("Alerts", "PleaseEnter")%> password");
                txt.focus();
                return false;
            }



            return true;
        }
    </script>
</head>
<body onload="document.getElementById('txtUsername').focus();" class="nk-body ui-rounder npc-default pg-auth">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

      
        <div class="nk-app-root">
            <!-- main @s -->
            <div class="nk-main ">
                <!-- wrap @s -->
                <div class="nk-wrap nk-wrap-nosidebar">
                    <!-- content @s -->
                    <div class="nk-content ">
                        <div class="nk-split nk-split-page nk-split-md">

                              <!-- .nk-split-content -->
                            <div class="nk-split-content nk-split-stretch bg-lighter d-flex toggle-break-lg toggle-slide toggle-slide-right" data-content="athPromo" data-toggle-screen="lg" data-toggle-overlay="true">
                                <div class="slider-wrap w-100 w-max-550px p-3 p-sm-5 m-auto">
                                    <div class="slider-init" data-slick='{"dots":true, "arrows":false}'>
                                         
                                        <!-- .slider-item -->
                                        <div class="slider-item">
                                            <div class="nk-feature nk-feature-center">
                                                <div class="nk-feature-img">
                                                    <img class="round" src="/wwwroot/images/slides/saif.png" srcset="/wwwroot/images/slides/saif.png= 2x" alt="">
                                                </div>
                                               <%-- <div class="nk-feature-content py-4 p-sm-5">
                                                    <h4>Dashlite</h4>
                                                    <p>You can start to create your products easily with its user-friendly design & most completed responsive layout.</p>
                                                </div>--%>
                                            </div>
                                        </div>
                                        <!-- .slider-item -->
                                      <%--  <div class="slider-item">
                                            <div class="nk-feature nk-feature-center">
                                                <div class="nk-feature-img">
                                                    <img class="round" src="/wwwroot/images/slides/promo-c.png" srcset="/wwwroot/images/slides/promo-c2x.png 2x" alt="">
                                                </div>
                                                <div class="nk-feature-content py-4 p-sm-5">
                                                    <h4>Dashlite</h4>
                                                    <p>You can start to create your products easily with its user-friendly design & most completed responsive layout.</p>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- .slider-item -->--%>
                                    </div>
                                    <!-- .slider-init -->
                                    <div class="slider-dots"></div>
                                    <div class="slider-arrows"></div>
                                </div>
                                <!-- .slider-wrap -->
                            </div>
                            <!-- .nk-split-content -->

                            <div class="nk-split-content nk-block-area nk-block-area-column nk-auth-container bg-black">
                                <div class="absolute-top-right d-lg-none p-3 p-sm-5">
                                    <a href="#" class="toggle btn-white btn btn-icon btn-light" data-target="athPromo"><em class="icon ni ni-info"></em></a>
                                </div>
                                <div class="nk-block nk-block-middle nk-auth-body" style="text-align:center">
                                    <div class="brand-logo pb-5">
                                        <a href="#" class="logo-link">
                                            <img class="logo-dark" src="/wwwroot/images/logo/CMGSLogo.png" srcset="/wwwroot/images/logo/CMGSLogo.png 2x" alt="logo-dark">
                                        </a>
                                    </div>
                                    <div class="nk-block-head">
                                        <div class="nk-block-head-content">
                                            <h5 class="nk-block-title" style="color:#fff;">نظام إدارة وجرد العهد الشخصية والتنظيمية - AMS</h5>
                                            <%--<div class="nk-block-des">
                                            <p>Access the DashLite panel using your email and passcode.</p>
                                        </div>--%>
                                        </div>
                                    </div>
                                    <!-- .nk-block-head -->
                                    <div style="direction:rtl">
                                        <div class="form-group">
                                            <div class="form-label-group">
                                                <%--<label class="form-label" for="default-01">Email or Username</label>--%>
                                            </div>
                                            <asp:TextBox runat="server"  ID="txtUsername" class="form-control form-control-lg" placeholder="أدخل اسم المستخدم" required></asp:TextBox>

                                        </div>
                                        <!-- .foem-group -->
                                        <div class="form-group">
                                            <div class="form-label-group">
                                                <%--<label class="form-label" for="password">Passcode</label>--%>
                                            </div>
                                            <div class="form-control-wrap">
                                            <%--    <a tabindex="-1" href="#" class="form-icon form-icon-right passcode-switch" data-target="password">
                                                    <em class="passcode-icon icon-show icon ni ni-eye"></em>
                                                    <em class="passcode-icon icon-hide icon ni ni-eye-off"></em>
                                                </a>--%>
                                                <%--   <input type="password" class="form-control form-control-lg" id="password" placeholder="Enter your passcode">--%>
                                                <asp:TextBox runat="server" ID="txtPass" class="form-control form-control-lg" placeholder="أدخل كلمة المرور" name="password" TextMode="Password" required></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="field-group" style="display: none">
                                            <asp:RadioButtonList ID="lstLang" runat="server" RepeatDirection="Horizontal" Font-Bold="False" Font-Size="10pt" CellPadding="20" CellSpacing="20" Width="50%">

                                                <asp:ListItem Selected="True" Value="2">English</asp:ListItem>
                                                <asp:ListItem Value="1">العربية</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <!-- .foem-group -->
                                        <div class="form-group">
                                             <asp:LinkButton ID="imbtnlogin" runat="server" CssClass="btn btn-lg btn-primary btn-block" OnClick="LoginButton_Click" Width="300px">تسجيل الدخول   </asp:LinkButton>
                                        </div>
                                           <div class="form-note-s2 pt-4"><asp:Label ID="lblError" runat="server"></asp:Label></div>
                                    </div>
                                    <!-- form -->


                                </div>
                                <!-- .nk-block -->

                            </div>
                          
                        </div>
                        <!-- .nk-split -->
                    </div>
                    <!-- wrap @e -->
                </div>
                <!-- content @e -->
            </div>
            <!-- main @e -->
        </div>




        <!-- app-root @e -->
        <!-- JavaScript -->
        <script src="/wwwroot/assets/js/bundle.js?ver=2.4.0"></script>
        <script src="/wwwroot/assets/js/scripts.js?ver=2.4.0"></script>

    </form>
</body>
</html>
