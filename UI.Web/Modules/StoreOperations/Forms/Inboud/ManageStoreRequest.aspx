<%@ Page Title="" Language="C#" AutoEventWireup="true"  MasterPageFile="~/Modules/_shared/Main.Master" CodeBehind="ManageStoreRequest.aspx.cs" Inherits="UI.Web.Modules.StoreOperations.Forms.Inboud.ManageStoreRequest" %>
<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="asp" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="hdnMasterID" runat="server" type="hidden" />
    <input id="hdnActiveTab" runat="server" type="hidden" />
        <script language="JavaScript" type="text/javascript">
            function chkImage() {
            function setActiveTab(tab) {
                var txt = document.getElementById("<%=hdnActiveTab.ClientID %>")
                txt.value = tab;

            }
        </script>

  <div class="nk-block-head nk-block-head-sm">
        <div class="nk-block-between">
            <div class="nk-block-head-content">
                <%--<h3 class="nk-block-title page-title"><%=_PageTitle %></h3>--%>
                <ul class="breadcrumb breadcrumb-arrow">
                    <li><i class="icon ni ni-home"></i>&nbsp;<a href="/admin/pages/home.aspx"><%=GetGlobalResourceObject("pages","home") %></a>&nbsp;&nbsp;<i class="icon ni ni-chevrons-left"></i>&nbsp;&nbsp;</li>
                    <%--<li class="active"><%=_PageTitle %></li>--%>
                </ul>
            </div>

            <!-- .nk-block-head-content -->
            <div class="nk-block-head-content">
                <div class="toggle-wrap nk-block-tools-toggle">
                    <a href="#" class="btn btn-icon btn-trigger toggle-expand mr-n1" data-target="pageMenu"><em class="icon ni ni-menu-alt-r"></em></a>
                    <div class="toggle-expand-content" data-content="pageMenu">
                        <ul class="nk-block-tools g-3">
                            <li class="nk-block-tools-opt">
                                <div class="drodown">
                                    <a href="#" class="dropdown-toggle btn btn-white btn-outline-light" data-toggle="dropdown"><em class="icon ni ni-setting"></em></a>
                                    <div class="dropdown-menu dropdown-menu-right">
                                        <ul class="link-list-opt no-bdr">
                                            <li>
                                                <%--<asp:LinkButton OnClientClick="return checkDelete();" runat="server" ID="LinkButton1" OnClick="btnDelete_Click"><i class="icon ni ni-trash"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %></asp:LinkButton>--%>
                                                <asp:LinkButton ID="btnSave" runat="server" OnClientClick="return chkImage();" OnClick="btnSave_Click"><i class='icon ni ni-save'></i>&nbsp; &nbsp;<%=GetGlobalResourceObject("pages","Submit") %> </asp:LinkButton>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </li>
                            <%--<li>
                                <asp:LinkButton runat="server" ID="btnNew" class="btn btn-icon btn-primary" OnClick="btnNew_Click"><em class="icon ni ni-plus"></em></asp:LinkButton></li>--%>
                        </ul>
                    </div>
                </div>
                <!-- .toggle-wrap -->
            </div>
            <!-- .nk-block-head-content -->
        </div>
        <!-- .nk-block-between -->
    </div>
  <asp:UpdatePanel runat="server" ID="Updatepanel1" ChildrenAsTriggers="true" UpdateMode="conditional">
  </asp:UpdatePanel>

     <div class="nk-block">




        <div class="card card-bordered">
            <div class="card card-stretch">
                <%--   <div class="card-title">
                    <h5 class="title"><%=GetGlobalResourceObject("pages","RequestDetail") %></h5>
                </div>--%>

                <div class="card-inner">
                    <div class="card-inner p-0">
                        <div class="portlet box">

                            <div class="portlet-body">

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="portlet box">

                                            <div class="portlet-body">
                                                <div class="col-lg-12">



                                                    <ul class="nav nav-tabs">
<%--                                                        <li class="nav-item" onclick="setActiveTab('1')"><a class="nav-link <%=getActiveTab("1") %>" data-toggle="tab" href="#MasterData"><em class="icon ni ni-list-thumb-alt"></em><span><%=GetGlobalResourceObject("pages","CustodyDetails") %> </span></a></li>--%>
                                                             <li class="nav-item" onclick="setActiveTab('1')"><a class="nav-link" data-toggle="tab" href="#MasterData"><em class="icon ni ni-list-thumb-alt"></em><span><%=GetGlobalResourceObject("pages","CustodyDetails") %> </span></a></li>

                                                    </ul>
                                                    <div class="tab-content">
                                                        <!--<div class="tab-pane fade in getActiveTab("1")%>" id="MasterData">-->
                                                        <div class="tab-pane fade in" id="MasterData">


                                                            <asp:Label runat="server" ID="lblerror"></asp:Label>
                                                            <div class="col-lg-12">
                                                                <div class="portlet box portlet-blue">

                                                                    <div class="portlet-body">
                                                                        <div role="form">
                                                                            <div class="row" style="margin-bottom: 20px;">
                                                                                <div class="col-md-12">
                                                                                    <div style="text-align: left">
                                                                                        <a href="assetsListPopup.aspx?eventId=1" class="btn btn-primary iframe75callback"><em class="icon ni ni-plus"></em><span>إضافة المواد</span></a>
                                                                                        <asp:Button runat="server" ID="btnReload" OnClick="btnReload_Click" CssClass="hide" />

                                                                                    </div>

                                                                                
                                                                                </div>
                                                                            </div>

                                                                            <div class="row">
                                                                                <div class="col-md-4">

                                                                                    

                                                                                    <div class="form-group">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","checkinDate") %></label>

                                                                                        <div class="form-control-wrap">
                                                                                            <div class="form-icon form-icon-right">
                                                                                                <em class="icon ni ni-calendar-alt"></em>
                                                                                            </div>
                                                                                            <asp:TextBox runat="server" ID="txtFromDate" placeholder="__/__/____" class="form-control date-picker"></asp:TextBox>
                                                                                        </div>


                                                                                    </div>

                                                                                   



                                                                                </div>

                                                                                <div class="col-md-4">
                                                                                    <div class="form-group" runat="server">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","returnTostore") %>  </label>

                                                                                     <%--   <asp:DropDownList ID="lstOwnerLocationCode" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>--%>

                                                                                           <input type="text" id="txtOwnerLocationCode" class="form-control" placeholder="Type to filter" autocomplete="off" />
                                                                                         <input id="selectedLocation" runat="server" value="0" type="hidden" class="selectedLocation" />

                                                                                    </div>
                                                                                      <div class="form-group">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Serial") %></label>
                                                                                        <asp:TextBox runat="server" ID="txtSerial" placeholder="IN\S.N\CMGSYY" class="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                  

                                                                                </div>


                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Notes") %></label>
                                                                                        <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" class="form-control" Height="115px"></asp:TextBox>
                                                                                    </div>


                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="card-inner">

                                                                <asp:DataGrid runat="server" ID="grdItems" AutoGenerateColumns="False" OnItemCommand="grdItems_ItemCommand"
                                                                    AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter">
                                                                    <PagerStyle Visible="False" />
                                                                    <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                                                                    <Columns>
                                                                        <asp:BoundColumn DataField="InboubdItemId" Visible="False"></asp:BoundColumn>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                                            <ItemStyle Width="2%" />
                                                                            <HeaderTemplate>
                                                                                <input id="chkAllItems" class="checkall" style="border-style: none;" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkItem', this.checked)" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" ID="chkItem" CssClass="check" />

                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>

                                                                        <asp:TemplateColumn HeaderText="<%$ Resources:pages,image %>">
                                                                            <ItemStyle Width="5%" />
                                                                            <ItemTemplate>

                                                                                <%--<%# FillImage(gets(Eval("ItemImage")), Resources.Utilities.resourcespath+"uploads/ItemsData/", 35, 25,"")%>--%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>

                                                                        <asp:BoundColumn DataField="ItemRefCode" HeaderText="<%$ Resources:pages,ItemRefCode %>"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="ItemTag" HeaderText="<%$ Resources:pages,Tagid %>"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="ItemFinanceCode" HeaderText="<%$ Resources:pages,ItemFinanceCode %>"></asp:BoundColumn>
                                                                        <%--<asp:BoundColumn DataField="ItemNameAr" HeaderText="<%$ Resources:pages,ItemNameAr %>"></asp:BoundColumn>--%>
                                                                        <asp:BoundColumn DataField="ItemNameAr" HeaderText="<%$ Resources:pages,PurchaseItems %>  "></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="TransDate" HeaderText="<%$ Resources:pages,purchaseDate %>" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                                                                        <%--                            <asp:BoundColumn DataField="ReceivedQty" HeaderText="<%$ Resources:pages,ReceivedQty %>"></asp:BoundColumn>--%>
                                                                        <asp:BoundColumn DataField="QtyUnitTitleAr" HeaderText="<%$ Resources:pages,QUnit %>"></asp:BoundColumn>
                                                                        <%--<asp:BoundColumn DataField="StatusTitleAr" HeaderText="<%$ Resources:pages,status %>"></asp:BoundColumn>--%>
                                                                        <asp:BoundColumn DataField="EstimatedUnitCost" HeaderText="<%$ Resources:pages,EstimatedCost %>"></asp:BoundColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$ Resources:pages,status %>">
                                                                            <ItemStyle Width="5%" />
                                                                            <ItemTemplate>
                                                                               <%-- <div><%# showAction(ZeroIntergerIFNull(gets(Eval("ActionId"))), gets( Eval("LastActiontitleAr"))) %></div>
                                                                                <div><%# showAvailability(ZeroIntergerIFNull(gets(Eval("StatusId"))), gets( Eval("AvailabilityStatusAr"))) %></div>--%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>


                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle Width="2%" />
                                                                            <ItemTemplate>
                                                                                <div class="drodown">
                                                                                    <a href="#" class="btn btn-sm btn-icon btn-trigger dropdown-toggle" data-toggle="dropdown"><em class="icon ni ni-more-h"></em></a>
                                                                                    <div class="dropdown-menu dropdown-menu-right">
                                                                                        <ul class="link-list-opt no-bdr">
                                                                                            <li><a href="<%=GetGlobalResourceObject("Utilities","cutureRoute") %>/Modules/Assets/AssetDetails.aspx?aid=<%#Eval("InboubdItemId") %>" class="iframe75"><span class='nk-menu-icon'><em class='icon ni ni-cards'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Pages","viewDetails") %></span></a></li>
                                                                                            <li>
                                                                                                <asp:LinkButton runat="server" ID="lnkDelete" CommandName="delete"><i class="icon ni ni-trash"></i>&nbsp;<%=GetGlobalResourceObject("pages","RemoveItem") %></asp:LinkButton></li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>

                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>



                                                                    </Columns>
                                                                </asp:DataGrid>
                                                                <div class="datatable-footer">
                                                                    <div class="dataTables_info" id="DataTables_Table_3_info" role="status" aria-live="polite">
                                                                        <asp:Label ID="lblcount" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="dataTables_paginate paging_simple_numbers" id="DataTables_Table_3_paginate">

                                                                        <cc1:Pager CurrentIndex="1" OnCommand="pager_Command" ShowFirstLast="False" ID="pager1"
                                                                            runat="server" Width="100%" PageSize="20" AlternativeTextEnabled="False" BackToFirstClause="" BackToPageClause="" EnableSmartShortCuts="True" EnableTheming="True" FirstClause="" FromClause="" GoClause="" GoToLastClause="" LastClause="" NextClause="التالى" OfClause="من" PageClause="صفحة" PreviousClause="السابق" RTL="True" ShowingResultClause="" ShowResultClause=""></cc1:Pager>


                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="/Layout/Assets/businessScripts/locationCombo.js"></script>

</asp:Content>


