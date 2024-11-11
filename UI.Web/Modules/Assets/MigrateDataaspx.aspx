<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="MigrateDataaspx.aspx.cs" Inherits="UI.Web.Modules.Assets.MigrateDataaspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnMigrate" runat="server" OnClick="btnMigrate_Click" Text=" نقل البيانات عن طريق الموظفين" />
    <br /><br /><br /><br />
    <asp:Button ID="btnMigrateRoom" runat="server" OnClick="btnMigrateRoom_Click" Text=" نقل البيانات عن طريق الغرفة" />
</asp:Content>
