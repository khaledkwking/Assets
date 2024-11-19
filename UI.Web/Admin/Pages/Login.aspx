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
                Swal.fire("فظلا ، يرجى ادخال اسم المستخدم   ");
                txt.focus();
                return false;
            }

            txt = document.getElementById("<%=txtPass.ClientID %>")
            if (txt.value == "") {
                Swal.fire("فظلا ، يرجى ادخال كلمة المرور     ");
                 
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
                        <div class="nk-block nk-block-middle nk-auth-body  wide-xs">
                            <div class="brand-logo pb-4 text-center" style="margin-bottom: 30px;">
                                <a href="#" class="logo-link">

                                    <img class="logo-dark" src="/wwwroot/images/logo/cmgs2@2x.png" srcset="/wwwroot/images/logo/cmgs2@2x.png" alt="logo-dark">
                                </a>
                            </div>
                            <div class="card card-bordered">
                                <div class="card-inner card-inner-lg" style="direction:rtl;text-align:center">
                                    <div class="nk-block-head">
                                        <div class="nk-block-head-content text-center">
                                            <h4 class="nk-block-title" style="color:#cba052"> نظام إدارة العهد والمخازن</h4>
                                            <div class="nk-block-des" style="color:#cba052">
                                                تسجيل الدخول
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <div class="form-label-group">
                                            <label class="form-label" for="default-01">  إسم المستخدم  </label>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtUsername" class="form-control form-control-lg" placeholder="أدخل اسم المستخدم" required></asp:TextBox>

                                    </div>
                                    <div class="form-group">
                                        <div class="form-label-group">
                                            <label class="form-label" for="password">كلمة المرور</label>
                                             
                                        </div>
                                        <div class="form-control-wrap">
                                            <a href="#" class="form-icon form-icon-right passcode-switch" data-target="password">
                                                <em class="passcode-icon icon-show icon ni ni-eye"></em>
                                                <em class="passcode-icon icon-hide icon ni ni-eye-off"></em>
                                            </a>
                                            <asp:TextBox runat="server" ID="txtPass" class="form-control form-control-lg" placeholder="أدخل كلمة المرور" name="password" TextMode="Password" required></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="field-group" style="display: none">
                                        <asp:RadioButtonList ID="lstLang" runat="server" RepeatDirection="Horizontal" Font-Bold="False" Font-Size="10pt" CellPadding="20" CellSpacing="20" Width="50%">

                                            <asp:ListItem Selected="True" Value="2">English</asp:ListItem>
                                            <asp:ListItem Value="1">العربية</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group">

                                        <asp:LinkButton ID="imbtnlogin" runat="server" CssClass="btn btn-lg btn-primary btn-block bg-black" OnClick="LoginButton_Click" Width="300px">تسجيل الدخول   </asp:LinkButton>
                                        <asp:Label ID="lblError" runat="server"></asp:Label>
                                    </div>




                                </div>
                            </div>
                        </div>
                        <div class="nk-footer nk-auth-footer-full">
                            <div class="container wide-lg">
                                <div class="row g-3">
                                    <div class="col-lg-6 order-lg-last">
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="nk-block-content text-center text-lg-left">
                                            <p class="text-soft">&copy; 2024 مركز نظم المعلومات.</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
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
