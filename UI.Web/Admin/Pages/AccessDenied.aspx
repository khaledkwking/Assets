<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="UI.Web.Admin.Pages.AccessDenied" %>

<!DOCTYPE html>
<html lang="en">
<head>
	 
	 <title>CMGS | Assets Managment System</title>

     <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!--Loading bootstrap css-->
    <link type="text/css" href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,800italic,400,700,800">
    <link type="text/css" rel="stylesheet" href="http://fonts.googleapis.com/css?family=Oswald:400,700,300">
    <link type="text/css" rel="stylesheet" href="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>vendors/jquery-ui-1.10.3.custom/css/ui-lightness/jquery-ui-1.10.3.custom.css">
    <link type="text/css" rel="stylesheet" href="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>vendors/font-awesome/css/font-awesome.min.css">
    <link type="text/css" rel="stylesheet" href="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>vendors/bootstrap/css/bootstrap.min.css">
    <!--Loading style vendors-->
    <link type="text/css" rel="stylesheet" href="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>vendors/animate.css/animate.css">
    <link type="text/css" rel="stylesheet" href="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>vendors/iCheck/skins/all.css">
    <!--Loading style-->
    <link type="text/css" rel="stylesheet" href="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>css/themes/style1/pink-violet.css" id="theme-change" class="style-change color-change">
    <link type="text/css" rel="stylesheet" href="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>css/style-responsive.css">
</head>
<body id="lock-screen-page">
    <form id="form1" runat="server">
    

        <div class="page-form">
    <div class="body-content">
        <div id="lock-screen-avatar"><img src="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>images/option_Password.gif" alt="" class="img-responsive img-circle"></div>
        <div id="lock-screen-info"><h1>
 Assets Managment System - CMGS 
</h1>

            <div class="email">You are not authorized to access this page, your session has been terminated to re-login
                            please click login.</div>
            <div class="mtl mbl">
                <div class="input-icon right"><i class="fa fa-unlock"></i><asp:LinkButton runat="server" ID="lnkGo" OnClick="lnkGo_Click"  class="form-control">Login</asp:LinkButton>
                    </div>
            </div>
        </div>
    </div>
</div>
<script src="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>js/jquery-1.10.2.min.js"></script>
<script src="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>js/jquery-migrate-1.2.1.min.js"></script>
<script src="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>js/jquery-ui.js"></script>
<!--loading bootstrap js-->
<script src="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>vendors/bootstrap/js/bootstrap.min.js"></script>
<script src="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>vendors/bootstrap-hover-dropdown/bootstrap-hover-dropdown.js"></script>
<script src="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>js/html5shiv.js"></script>
<script src="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>js/respond.min.js"></script>
<script src="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>vendors/iCheck/icheck.min.js"></script>
<script src="<%= GetGlobalResourceObject("Utilities", "resourcespath")%>vendors/iCheck/custom.min.js"></script>
<script>//BEGIN CHECKBOX & RADIO
    $('input[type="checkbox"]').iCheck({
        checkboxClass: 'icheckbox_minimal-grey',
        increaseArea: '20%' // optional
    });
    $('input[type="radio"]').iCheck({
        radioClass: 'iradio_minimal-grey',
        increaseArea: '20%' // optional
    });
    //END CHECKBOX & RADIO
</script>

    
    
    </form>
</body>
</html>
