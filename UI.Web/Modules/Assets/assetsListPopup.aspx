<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/MainEmpty.Master" AutoEventWireup="true" CodeBehind="assetsListPopup.aspx.cs" Inherits="UI.Web.Modules.Assets.assetsListPopup" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="JavaScript" type="text/javascript">
        function closeMe() {
            parent.$.fn.colorbox.close();
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


        </div>
        <!-- .nk-block-between -->
    </div>

    <div class="nk-block">

         
        <div class="card card-stretch" id="tblshow" runat="server">
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
                                            <em class="icon ni ni-filter"></em>
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
                                                    <div class="col-12">

                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","Category") %></label>
                                                            <asp:DropDownList ID="lstFilterCategory" runat="server" class="form-control form-select" data-search="on" AutoPostBack="true" OnSelectedIndexChanged="lstFilterCategory_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">

                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","item") %></label>
                                                            <asp:DropDownList ID="lstFilterItem" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">

                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","Vendor") %></label>
                                                            <asp:DropDownList ID="lstFilterVendor" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-12" style="display:none">

                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","Status") %></label>
                                                            <asp:DropDownList ID="lstFilterSatus" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                     <div class="col-12">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","assignedToLocation") %></label>
                                                            <asp:DropDownList ID="lstFilterLocation" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                     <div class="col-12">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","Employee") %></label>
                                                            <asp:DropDownList ID="lstfilterEmployee" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>
                                                        </div>
                                                    </div>


                                                    <div class="col-12">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","Event") %></label>
                                                            <asp:DropDownList ID="lstFilterAction" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","purchaseDateFrom") %></label>
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
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","purchaseDateTo") %></label>
                                                            <div class="form-control-wrap">
                                                                <div class="form-icon form-icon-right">
                                                                    <em class="icon ni ni-calendar-alt"></em>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txtTransactionDateTo" placeholder="__/__/____" class="form-control date-picker"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","ItemRefCode") %></label>
                                                            <asp:TextBox runat="server" ID="txtFilterCode" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>



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
                                                <li><asp:LinkButton runat="server" ID="lnkSelectItem" OnClick="lnkSelectItem_Click"  ><i class="icon ni ni-focus"></i>&nbsp;<%=GetGlobalResourceObject("pages","select") %></asp:LinkButton></li>
                                                 
                                                 
                                            </ul>

                                        </div>
                                    </div>
                                </li>
                                <%--<li>
                                    <asp:LinkButton runat="server" ID="btnNew" class="btn btn-icon btn-primary" OnClick="btnNew_Click"><em class="icon ni ni-plus"></em></asp:LinkButton>

                                </li>--%>
                            </ul>
                        </div>
                        <div class="card-search search-wrap" data-search="search">
                            <div class="search-content">
                                <a href="#" class="search-back btn btn-icon toggle-search" data-target="search"><em class="icon ni ni-arrow-left"></em></a>
                                <asp:TextBox runat="server" ID="txtPartOfName" CssClass="form-control border-transparent form-focus-none" placeholder="بحث "></asp:TextBox>
                                <asp:LinkButton runat="server" ID="lnkQuick" OnClick="lnkQuick_Click" class="search-submit btn btn-icon"> <em class="icon ni ni-search"></em> </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-inner p-0">
                     <asp:Label ID="lblError" runat="server"></asp:Label>
                    <div class="table-responsive">
                    <asp:DataGrid runat="server" ID="grdItems" AutoGenerateColumns="False"  OnItemDataBound="grdItems_ItemDataBound"
                        AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter">
                        <PagerStyle Visible="False" />
                     
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

                                    <%# FillImage(gets(Eval("ItemImage")), Resources.Utilities.resourcespath+"uploads/ItemsData/", 35, 25,"")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                           <asp:TemplateColumn HeaderText="<%$ Resources:pages,PurchaseItems %>">
                                <ItemTemplate>
                                    <div><a href="<%=GetGlobalResourceObject("Utilities","cutureRoute") %>/Modules/Assets/AssetDetails.aspx?aid=<%#Eval("InboubdItemId") %>" class="iframe75"><%#gets(Eval("ItemNameAr"))%></a></div>
                                     <div><%# showAvailability(ZeroIntergerIFNull(gets(Eval("StatusId"))), gets( Eval("AvailabilityStatusAr"))) %></div>
                                 </ItemTemplate>
                            </asp:TemplateColumn>
                             <%--<asp:BoundColumn DataField="ItemRefCode" HeaderText="<%$ Resources:pages,ItemRefCode %>"></asp:BoundColumn>--%>
                            <asp:BoundColumn DataField="ItemTag" HeaderText="<%$ Resources:pages,Tagid %>"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ItemFinanceCode" HeaderText="<%$ Resources:pages,ItemFinanceCode %>"></asp:BoundColumn>
                            <%--<asp:BoundColumn DataField="ItemNameAr" HeaderText="<%$ Resources:pages,ItemNameAr %>"></asp:BoundColumn>--%>
                             
                               
                            <asp:BoundColumn DataField="TransDate" HeaderText="<%$ Resources:pages,purchaseDate %>" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                            
<%--                            <asp:BoundColumn DataField="ReceivedQty" HeaderText="<%$ Resources:pages,ReceivedQty %>"></asp:BoundColumn>--%>
                             
                            <%--<asp:BoundColumn DataField="StatusTitleAr" HeaderText="<%$ Resources:pages,status %>"></asp:BoundColumn>--%>
                            <asp:BoundColumn DataField="EstimatedUnitCost" HeaderText="<%$ Resources:pages,EstimatedCost %>"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Actiondate" HeaderText="<%$ Resources:pages,eventDate %>" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="<%$ Resources:pages,Event %>">
                                <ItemTemplate>
                                    <div style="font-size:12px;" class="text-soft"> <%#( ZeroIntergerIFNull(gets(Eval("EmpRefCode")))==0 && ZeroIntergerIFNull(gets(Eval("ToLocationId")))==0 ?"":ZeroIntergerIFNull(gets(Eval("EmpRefCode")))==0 ?"<em class='icon ni ni-building  text-info'></em>&nbsp;" :"<em class='icon ni ni-user-list  text-info'></em> &nbsp;") +showAction(ZeroIntergerIFNull(gets(Eval("ActionId"))), gets( Eval("LastActiontitleAr"))) %> <%#   gets( Eval("LocationNameAr")) %></div>
                                   <div class="text-warning"><%#  ZeroIntergerIFNull(gets(Eval("EmpRefCode")))==0? gets(Eval("LocationNameAr"))  : gets(Eval("EmpName")) %></div>

                                    
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
                                               <li><a href="<%=GetGlobalResourceObject("Utilities","cutureRoute") %>/Modules/Assets/AssetDetails.aspx?aid=<%#Eval("InboubdItemId") %>"><span class='nk-menu-icon'><em class='icon ni ni-cards'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Pages","viewDetails") %></span></a></li>
                                                
                                            </ul>
                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:TemplateColumn>



                        </Columns>
                    </asp:DataGrid>
                        </div>
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
