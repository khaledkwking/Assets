<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="StoreList.aspx.cs" Inherits="UI.Web.Modules.MasterData.StoreList" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="StoreList.aspx.cs" Inherits="UI.Web.Modules.MasterData.StoreList" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card card-bordered" id="tblshow" runat="server">
        <div class="card card-stretch">
            <%--<div class="card-header border-bottom"><%=GetGlobalResourceObject("pages","DataListing") %></div>--%>
            <div class="card-inner">

                <div class="card-inner p-0">
                    <div class="portlet box">

                        <div class="portlet-body">

                            <asp:DataGrid runat="server" ID="grdData" AutoGenerateColumns="False" AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter" data-auto-responsive="false">
                                <Columns>
                                    <asp:BoundColumn DataField="code" Visible="False"></asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                        <ItemStyle Width="2%" />
                                        <HeaderTemplate>
                                            <input id="chkAllItems" class="checkall" style="border-style: none;" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkItem', this.checked)" />
                                        </HeaderTemplate>

                                    </asp:TemplateColumn>

                                    <asp:BoundColumn DataField="LocationNameEn" HeaderText="<%$ Resources:Pages , LocationNameEn %>"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="LocationNameAr" HeaderText="<%$ Resources:Pages , LocationNameArabic %>"></asp:BoundColumn>


                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="2%" />
                                        <ItemTemplate>
                                            <div class="drodown" style="display: none">
                                                <a href="#" class="btn btn-sm btn-icon btn-trigger dropdown-toggle" data-toggle="dropdown"><em class="icon ni ni-more-h"></em></a>
                                                <div class="dropdown-menu dropdown-menu-right">
                                                    <ul class="link-list-opt no-bdr">
                                                        <li>
                                                            <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" class="btn btn-default btn-xs"><em class="icon ni ni-cards-fill"></em><span> <%=GetGlobalResourceObject("pages","Edit") %></span> </asp:LinkButton></li>

                                                    </ul>
                                                </div>
                                            </div>

                                        </ItemTemplate>
                                    </asp:TemplateColumn>


                                </Columns>
                            </asp:DataGrid>
                            <div class="row mbm">
                                <div class="col-lg-12">
                                    <div class="pagination-panel">
                                        &nbsp;
                                            <asp:Label ID="lblcount" runat="server"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>




</asp:Content>
