<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="StocktakingReport.aspx.cs" Inherits="UI.Web.Modules.WHM.Forms.StocktakingReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- <style>
        .ToolbarPageNav.WidgetSet {
            /*display: none !important;*/
        }
    </style>--%>
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
    <asp:Button ID="btnExportPDF" runat="server" Text="Export to PDF" OnClick="btnExportPDF_Click" Visible="false" />
    <br />
    <div class="nk-block">
        <div class="card card-stretch" id="tblshow" runat="server">
            <div class="card-inner-group">
                <div class="card-inner" style="direction: ltr">
                    <asp:Label runat="server" ID="lblerror"></asp:Label>
                   
                    <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>--%>

                    <div class="row" id="divFilter" runat="server">

                        <div class="col-md-2" style="text-align: left; margin-right: -30px;">
                            <asp:Button ID="btnNext" runat="server" Text="التالي" OnClick="btnNext_Click" />
                            <asp:Label ID="lblCurrentPage" runat="server" Text="صفحة 1" />
                            <asp:Button ID="btnPrevious" runat="server" Text="السابق" OnClick="btnPrevious_Click" />
                        </div>
                        <div class="col-md-1" style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text=" " />
                            <br />
                           <asp:Button ID="btnExportToExcel" runat="server" Text="كل العهد Excel" OnClick="btnExportToExcel_Click" Visible="true" />
                        </div>
                        <div class="col-md-1" style="text-align: right">
                            <asp:Label ID="Label4" runat="server" Text=" " />
                            <br />
                            <asp:Button ID="btnSearch" runat="server" Text="بحث" OnClick="btnSearch_Click" />
                        </div>

                        <div class="col-md-2" style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="البند" />
                            <asp:DropDownList ID="ddlSub" runat="server" class="form-control form-select"
                                data-search="on" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2" style="text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="الباب" />
                            <asp:DropDownList ID="ddlMain" runat="server" class="form-control form-select"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlMain_SelectedIndexChanged"
                                data-search="on" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2" style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="المجموعة" />
                            <asp:DropDownList ID="ddlParent" runat="server" class="form-control form-select"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlParent_SelectedIndexChanged"
                                data-search="on" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2" style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="الجهة" />
                            <asp:DropDownList ID="ddlGov" runat="server" class="form-control form-select"
                                data-search="on" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <%--                              <rsweb:ReportViewer ID="ReportViewer1" runat="server" Visible="true" AsyncRendering="false" SizeToReportContent="true">
  </rsweb:ReportViewer>
                        </ContentTemplate>

                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlMain" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlParent" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>--%>


                    <div>
                    </div>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Visible="true" AsyncRendering="false" SizeToReportContent="true">
                    </rsweb:ReportViewer>

                    <rsweb:ReportViewer ID="rptProjects"
                        runat="server"
                        Width="200%"
                        Height="200px"
                        ProcessingMode="Local"
                        ShowToolBar="true"
                        ShowPageNavigationControls="true"
                        ShowZoomControl="true"
                        ShowFindControls="true"
                        ShowParameterPrompts="true"
                        ShowPrintButton="true"
                        ShowRefreshButton="true"
                        AsyncRendering="false"
                        SizeToReportContent="true"
                        Visible="true" />

                    <%-- <rsweb:ReportViewer 
    ID="ReportViewer2" 
    runat="server" 
    ProcessingMode="Local" 
    ShowRefreshButton="false"
    EnableViewState="false">
</rsweb:ReportViewer>--%>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

