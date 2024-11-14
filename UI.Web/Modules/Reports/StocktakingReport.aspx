<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="StocktakingReport.aspx.cs" Inherits="UI.Web.Modules.WHM.Forms.StocktakingReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="nk-block-head nk-block-head-sm">
        <div class="nk-block-between">
            <div class="nk-block-head-content">
                <h3 class="nk-block-title page-title"><%=_PageTitle %></h3>
                <ul class="breadcrumb breadcrumb-arrow">
                    <li><i class="icon ni ni-home"></i>&nbsp;<a href="/admin/pages/home.aspx"><%=GetGlobalResourceObject("pages","home") %></a>&nbsp;&nbsp;<i class="icon ni ni-chevrons-left"></i>&nbsp;&nbsp;</li>
                    <li class="active"><%=_PageTitle %></li>
                </ul>
            </div>


        </div>
        <!-- .nk-block-between -->
    </div>

    <div class="nk-block">
        <div class="card card-stretch" id="tblshow" runat="server">
            <div class="card-inner-group">
                  <div class="card-inner" style="direction: ltr">
                        <asp:Label runat="server" ID="lblerror"></asp:Label>
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Visible="true" AsyncRendering="false" SizeToReportContent="true"  >
                        </rsweb:ReportViewer>
                    </div>
            </div>
        </div>

    </div>
</asp:Content>
 
