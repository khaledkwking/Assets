<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="OutboundList.aspx.cs" Inherits="UI.Web.Modules.StoreOperations.Forms.Outbound.OutboundList" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function chkImage() {

            return true;
        }
    </script>

    <asp:UpdatePanel runat="server" ID="Updatepanel1" ChildrenAsTriggers="true" UpdateMode="conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>


    <div class="nk-block-head nk-block-head-sm">
        <div class="nk-block-between">
            <div class="nk-block-head-content">
                <h3 class="nk-block-title page-title"><%=_PageTitle %></h3>
                <ul class="breadcrumb breadcrumb-arrow">
                    <li><i class="fa fa-home"></i>&nbsp;<a href="/admin/pages/home.aspx"><%=GetGlobalResourceObject("pages","home") %></a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
                    <li class="active"><%=_PageTitle %></li>
                </ul>
            </div>

            <!-- .nk-block-head-content -->

        </div>
    </div>


    <div class="nk-block">
        <asp:Label runat="server" ID="Label1"></asp:Label>





        <div class="card card-stretch" id="Div1" runat="server">
            <div class="card-inner-group">
                <div class="card-inner" data-select2-id="22">
                    <div class="card-title-group" data-select2-id="21">
                        <div class="card-title">
                            <h5 class="title"><%=GetGlobalResourceObject("pages","DataListing") %></h5>
                        </div>
                        <div class="card-tools mr-n1" data-select2-id="20">
                            <ul class="btn-toolbar gx-1" data-select2-id="19">
                                <li>
                                    <a href="#" class="search-toggle toggle-search btn btn-icon" data-target="search"><em class="icon ni ni-search"></em></a>
                                </li>
                                <li class="btn-toolbar-sep"></li>

                                <li data-select2-id="18">
                                    <div class="dropdown" data-select2-id="17">
                                        <a href="#" class="btn btn-trigger btn-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                            <em class="icon ni ni-filter-alt"></em>
                                        </a>
                                        <div class="filter-wg dropdown-menu dropdown-menu-xl dropdown-menu-right" style="" data-select2-id="16">
                                            <div class="dropdown-head">
                                                <span class="sub-title dropdown-title">بحث متقدم</span>
                                                <div class="dropdown">
                                                    <a href="#" class="link link-light">
                                                        <em class="icon ni ni-more-h"></em>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class="dropdown-body dropdown-body-rg" data-select2-id="15">
                                                <div class="row gx-6 gy-4" data-select2-id="14">
                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","TransactionDate") %></label>
                                                            <div class="form-control-wrap">
                                                                <div class="form-icon form-icon-right">
                                                                    <em class="icon ni ni-calendar-alt"></em>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txtTransDate" placeholder="__/__/____" class="form-control date-picker"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","TransactionDateTo") %></label>
                                                            <div class="form-control-wrap">
                                                                <div class="form-icon form-icon-right">
                                                                    <em class="icon ni ni-calendar-alt"></em>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txtTransactionDateTo" placeholder="__/__/____" class="form-control date-picker"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="col-12">

                                                          <div class="form-group">
                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","outboundTypeCode") %>  </label>

                                                                <asp:DropDownList ID="lstOutboundTypeCode" runat="server" class="form-control">
                                                                </asp:DropDownList>

                                                            </div>

 
                                                    </div>
                                                    
                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","Refcode") %></label>
                                                            <asp:TextBox runat="server" ID="txtRefNo" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <%--   <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","serial") %></label>
                                                            <asp:TextBox runat="server" ID="txtFilterSerial" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>--%>



                                                    <div class="col-12">
                                                        <div class="form-group">
                                                            <asp:LinkButton runat="server" ID="btnFilter" class="btn btn-secondary" OnClick="btnFilter_Click"><i class="icon ni ni-search"></i>&nbsp;<%=GetGlobalResourceObject("pages","Filter") %> </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </li>
                                <li>
                                    <div class="dropdown">
                                        <a href="#" class="btn btn-trigger btn-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                            <em class="icon ni ni-setting"></em>
                                        </a>
                                        <div class="dropdown-menu  dropdown-menu-right" style="">
                                            <ul class="link-check">
                                                <li>
                                                    <asp:LinkButton OnClientClick="return checkDelete();" runat="server" ID="btnDelete" OnClick="btnDelete_Click"><i class="icon ni ni-trash"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %></asp:LinkButton></li>
                                            </ul>

                                        </div>
                                    </div>
                                </li>
                                <%-- <li>
                                    <asp:LinkButton runat="server" ID="btnNew" class="btn btn-icon btn-primary" OnClick="btnNew_Click"><em class="icon ni ni-plus"></em></asp:LinkButton>

                                </li>--%>
                            </ul>
                        </div>
                        <div class="card-search search-wrap" data-search="search">
                            <div class="search-content">
                                <a href="#" class="search-back btn btn-icon toggle-search" data-target="search"><em class="icon ni ni-arrow-left"></em></a>
                                <asp:TextBox ID="txtFilterSerial" runat="server" CssClass="form-control border-transparent form-focus-none" placeholder="OUT/Serial/CMGSYY "></asp:TextBox>
                                <asp:LinkButton runat="server" ID="lnkQuick" OnClick="lnkQuick_Click" class="search-submit btn btn-icon"> <em class="icon ni ni-search"></em> </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-inner p-0">
                    <asp:Label ID="lblerror" runat="server"></asp:Label>
                    <asp:DataGrid runat="server" ID="grdData" AutoGenerateColumns="False"
                        AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter" data-auto-responsive="false" OnItemDataBound="grdData_ItemDataBound">

                        <Columns>
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

                            <asp:TemplateColumn HeaderText="">
                                <ItemStyle HorizontalAlign="left"  />
                                <ItemTemplate>

                                    <div style="direction: rtl; text-align: right">
                                        <asp:Label Font-Bold="true" runat="server" ID="Label7" CssClass="black_Lable">
				                           الأصناف
                                        </asp:Label>
                                        <asp:DataGrid runat="server" ID="grdOutboundItems" AutoGenerateColumns="False"
                                            AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter">
                                            <PagerStyle Visible="False" />
                                            <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                                            <Columns>


                                               

                                                <asp:BoundColumn DataField="ItemRefCode" HeaderText="<%$ Resources:pages,ItemRefCode %>"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="ItemRFIDCode" HeaderText="<%$ Resources:pages,ItemRFIDCode %>"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="ItemFinanceCode" HeaderText="<%$ Resources:pages,ItemFinanceCode %>"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="ItemNameAr" HeaderText="<%$ Resources:pages,PurchaseItems %>  "></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Qty" HeaderText="<%$ Resources:pages,Qty %>"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="unitCodeTitleAr" HeaderText="<%$ Resources:pages,QUnit %>"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="StatusNameAr" HeaderText="<%$ Resources:pages,Status %>"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="EstimatedAmount" HeaderText="<%$ Resources:pages,EstimatedCost %>"></asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="">
                                <ItemStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <img style="cursor: pointer;" src="/wwwroot/assets/images/plus.gif" alt="" border="0" runat="server" id="imgControl" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="code" Visible="False"></asp:BoundColumn>

  <asp:BoundColumn DataField="OutboundTypeTitleAr" HeaderText="<%$ Resources:pages,outboundTypeCode %> "></asp:BoundColumn>

                            <asp:BoundColumn DataField="Serial" HeaderText="<%$ Resources:pages,Serial %> "></asp:BoundColumn>
                            <asp:BoundColumn DataField="TransDate" HeaderText=" <%$ Resources:pages,TransDate %>  " DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>

                          
                            <asp:BoundColumn DataField="RefNo" HeaderText="<%$ Resources:pages,RefNo %> "></asp:BoundColumn>
                            <asp:BoundColumn DataField="RefDate" HeaderText="<%$ Resources:pages,RefDate %> " DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FromLocationNameEn" HeaderText="<%$ Resources:pages,OutOwnerLocation %>  "></asp:BoundColumn>

 
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="2%" />
                                <ItemTemplate>
                                    <div class="drodown">
                                        <a href="#" class="btn btn-sm btn-icon btn-trigger dropdown-toggle" data-toggle="dropdown"><em class="icon ni ni-more-h"></em></a>
                                        <div class="dropdown-menu dropdown-menu-right">
                                            <ul class="link-list-opt no-bdr">
                                                <li>
                                                    <a href="OutboundOperations.aspx?t=<%#Eval("TypeCode") %>&id=<%#Eval("code") %>" class="btn btn-default btn-xs"><i class="icon ni ni-edit"></i>&nbsp; <%=GetGlobalResourceObject("pages","RequestDetails") %> </a>
                                                </li>

                                                <%--<li>
                                                    <a href="../reportsOutbound.aspx?t=<%#Eval("TypeCode") %>&id=<%#Eval("code") %>" class="btn btn-default btn-xs"><i class="icon ni ni-printer"></i>&nbsp; <%=GetGlobalResourceObject("pages","PrintCustosy") %> </a>
                                                </li>
                                                 

                                                <li>
                                                    <a href="OutboundItemDelivery.aspx?t=<%#Eval("TypeCode") %>&serial=<%#Eval("serial") %>" class="btn btn-default btn-xs"><i class="icon ni ni-wallet-out"></i>&nbsp; <%=GetGlobalResourceObject("pages","OutboundItemDelivery") %> </a>
                                                </li>--%>


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






</asp:Content>
