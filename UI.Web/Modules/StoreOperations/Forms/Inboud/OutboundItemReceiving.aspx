<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="OutboundItemReceiving.aspx.cs" Inherits="UI.Web.Modules.StoreOperations.Forms.Outboud.OutboundItemReceiving" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>

        function ControlGrid(imgName, rowIndex, rowID) {
            //alert("CONTROL GRID");
            //alert(imgName);
            //alert(rowIndex);
            //alert(rowID);
            rowIndex = rowIndex + 3;

            var myrow = "";
            if (rowIndex < 10)
                myrow = "ctl00_ContentPlaceHolder1_grdOutboundItems_ctl0" + rowIndex;
            else
                myrow = "ctl00_ContentPlaceHolder1_grdOutboundItems_ctl" + rowIndex;
            var row = document.getElementById(myrow);
            //  alert("IMG NAME: "+imgName+" and ROW INDEX: "+rowIndex+" ID: "+rowID);
            //alert("MYROW: "+myrow+" AND VALUE FOUND: "+row);
            if (row.style.display == "") {
                row.style.display = "none";
                document.getElementById(imgName).src = plus.src;
            }
            else {
                row.style.display = "";
                document.getElementById(imgName).src = minus.src;
            }
        }
        function Checklist(obj, list) {
            //  alert("CHECK SYSTEM: "+obj.checked);
            if (list != "") {
                var data = list.split(",");
                //alert("LIST IS: "+data.length)
                for (var i = 0; i < data.length; i++) {
                    document.getElementById(data[i]).checked = obj.checked;
                }
            }
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
    </div>
    <div class="nk-block">
        <asp:Label runat="server" ID="lblerror"></asp:Label>

        <div class="card card-stretch">
            <div class="card-inner-group">
                <div class="card-inner" data-select2-id="22">
                    <div class="card-title-group" data-select2-id="21">
                        <div class="card-title">
                            <h5 class="title"><%=GetGlobalResourceObject("pages","RequestDetails") %></h5>
                        </div>
                        <div class="card-tools mr-n1" data-select2-id="20">
                            <ul class="btn-toolbar gx-1" data-select2-id="19">
                                <li>
                                    <a href="#" class="search-toggle toggle-search btn btn-icon" data-target="search"><em class="icon ni ni-search"></em></a>
                                </li>
                                <li class="btn-toolbar-sep"></li>

                                <li data-select2-id="18" style="display:none">
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

                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","InboundType") %></label>
                                                            <asp:DropDownList ID="lstInboundType" runat="server" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","ReferenceNo") %></label>
                                                            <asp:TextBox runat="server" ID="txtRefNo" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","DeliveryOrder") %></label>
                                                            <asp:TextBox runat="server" ID="txtDeliveryOrder" class="form-control"></asp:TextBox>
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
                                                <li>
                                                    <asp:LinkButton runat="server" ID="btnSave" Visible="false" class=" " OnClick="btnSave_Click1"><i class="icon ni ni-save"></i>&nbsp; <%=GetGlobalResourceObject("pages","ReceiveSelectedItems") %>&nbsp;</asp:LinkButton></li>
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
                <div class="card-inner p-0" id="divShow" runat="server">
                    <div class="row">
                        <div class="col-12 bg-gray-100 border border-warning" style="padding:10px 10px">
                            <div class="gy-3">
                                <div class="row align-center">
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-label" for="site-name"><%=GetGlobalResourceObject("pages","Serial") %></label>
                                            <asp:Label runat="server" ID="lblSerial" CssClass="form-note"></asp:Label>

                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-label" for="site-name"><%=GetGlobalResourceObject("pages","OutboundTypeCode") %></label>
                                            <asp:Label runat="server" ID="lblOutboundType" CssClass="form-note"></asp:Label>
                                        </div>
                                    </div>

                                      <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-label" for="site-name"><%=GetGlobalResourceObject("pages","RefNo") %></label>
                                            <asp:Label runat="server" ID="lblRefNo" CssClass="form-note"></asp:Label>
                                        </div>
                                    </div>

                                     <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-label" for="site-name"><%=GetGlobalResourceObject("pages","RefEmployee") %></label>
                                            <asp:Label runat="server" ID="lblEmpName" CssClass="form-note"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-label" for="site-name"><%=GetGlobalResourceObject("pages","TransDate") %></label>
                                            <asp:Label runat="server" ID="lblTransDate" CssClass="form-note"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-label" for="site-name"><%=GetGlobalResourceObject("pages","RefDate") %></label>
                                            <asp:Label runat="server" ID="txtRefDate" CssClass="form-note"></asp:Label>
                                        </div>
                                    </div>
                                     <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-label" for="site-name"><%=GetGlobalResourceObject("pages","fromLocation") %></label>
                                            <asp:Label runat="server" ID="lblOwnerLocationNameAr" CssClass="form-note"></asp:Label>
                                        </div>
                                    </div>

                                      <div class="col-lg-2">
                                        <div class="form-group">
                                            <label class="form-label" for="site-name"><%=GetGlobalResourceObject("pages","TargetLocationCode") %></label>
                                            <asp:Label runat="server" ID="lblLocationNameAr" CssClass="form-note"></asp:Label>
                                        </div>
                                    </div>

                                  

                                </div>
                               

                            </div>
                        </div>

                    </div>



                    <asp:DataGrid ID="grdOutboundItems" runat="server"
                        DataKeyField="code" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" class="table table-hover table-striped table-bordered table-advanced tablesorter"
                        Width="100%" OnItemDataBound="grdOutboundItems_ItemDataBound">
                        <SelectedItemStyle ForeColor="White" />
                        <ItemStyle CssClass="grdItem" />
                        <AlternatingItemStyle CssClass="grdItem" />
                        <PagerStyle Visible="false" />

                        <Columns>
                            <asp:BoundColumn DataField="outboubdItemId" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OutboundCode" Visible="False"></asp:BoundColumn>




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

                            <asp:BoundColumn DataField="ItemRefCode" HeaderText="<%$ Resources:pages,ItemRefCode %>"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ItemRFIDCode" HeaderText="<%$ Resources:pages,ItemRFIDCode %>"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ItemFinanceCode" HeaderText="<%$ Resources:pages,ItemFinanceCode %>"></asp:BoundColumn>
                            <%--<asp:BoundColumn DataField="ItemNameAr" HeaderText="<%$ Resources:pages,ItemNameAr %>"></asp:BoundColumn>--%>
                            <asp:BoundColumn DataField="ItemNameAr" HeaderText="<%$ Resources:pages,PurchaseItems %>  "></asp:BoundColumn>
                            <asp:BoundColumn DataField="Qty" HeaderText="<%$ Resources:pages,Qty %>">
                                <HeaderStyle BackColor="GreenYellow" />

                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TitleAr" HeaderText="<%$ Resources:pages,QUnit %>"></asp:BoundColumn>
                            <asp:BoundColumn DataField="StatusTitleAr" HeaderText="<%$ Resources:pages,status %>"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EstimatedAmount" HeaderText="<%$ Resources:pages,EstimatedCost %>"></asp:BoundColumn>

                            <asp:TemplateColumn HeaderText="<%$ Resources:pages,ReceivedQty %> ">
                                <ItemStyle HorizontalAlign="left" />

                                <HeaderStyle Wrap="False" HorizontalAlign="left" BackColor="Yellow" />

                                <ItemTemplate>
                                    <asp:TextBox ID="txtQty" Text='<%#Eval("DeliveredQry") %>' runat="server" class="form-control" Width="100px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <%--   <asp:BoundColumn DataField="QtyBalance" HeaderText="Planned">
                                    <HeaderStyle Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="QUnitNameEn" HeaderText="Q. Unit">
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>--%>
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
