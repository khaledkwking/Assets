<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/MainEmpty.Master" AutoEventWireup="true" CodeBehind="OrgChart.aspx.cs" Inherits="UI.Web.Modules.MasterData.OrgChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
    <asp:UpdatePanel runat="server" ID="Updatepanel1" ChildrenAsTriggers="true" UpdateMode="conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>


    <div class="chart-container">
        <div id="CMGS-OrgChart">
            <div class='stiff-chart-inner'>
                <%=OrgChartContent %>
            </div>
        </div>
    </div>


    <script src="/wwwroot/assets/js/businessScripts/orgChartTree.js"></script>


</asp:Content>
