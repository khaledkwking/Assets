<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="ItemsCardMasterPrice.aspx.cs" Inherits="UI.Web.Modules.MasterData.ItemsCardMasterPrice" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="JavaScript" type="text/javascript">
        function chkImage() {

            var txt = document.getElementById("<%=txtItemNameEn.ClientID %>")
            if (txt.value == "") {

                Swal.fire("فضلا ، ادخل اسم الصنف بالانجليزية");
                txt.focus();
                return false;
            }

            var txt = document.getElementById("<%=txtItemNameAr.ClientID %>")
            if (txt.value == "") {
                Swal.fire("فضلا ، ادخل اسم الصنف بالعربية");
                txt.focus();
                return false;
            }
            var txt = document.getElementById("<%=lstCategory.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("فضلا ، إختر التصنيف");
                txt.focus();
                return false;
            }

            var txt = document.getElementById("<%=lstQunit.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("فضلا ، إختر ,وحدة القياس");
                txt.focus();
                return false;
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
                    <li><i class="icon ni ni-home"></i>&nbsp;<a href="/admin/pages/home.aspx"><%=GetGlobalResourceObject("pages","home") %></a>&nbsp;&nbsp;<i class="icon ni ni-chevrons-left"></i>&nbsp;&nbsp;</li>
                    <li class="active"><%=_PageTitle %></li>
                </ul>
            </div>


        </div>
        <!-- .nk-block-between -->
    </div>

    <div class="nk-block">


        <div class="card card-bordered" id="tblAdd" runat="server" visible="false">
            <div class="card-header border-bottom">
                <asp:Label runat="server" ID="lblSubTitle"><%=GetGlobalResourceObject("pages","AddNewRecord") %></asp:Label>
            </div>
            <div class="card-inner">
                <asp:Label runat="server" ID="lblerror"></asp:Label>

                <div role="form" class="form-horizontal">
                    <div class="row">
                        <div class="col-md-4">

                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%=GetGlobalResourceObject("pages","Category") %> </label>
                                <div class="col-md-12">
                                    <asp:DropDownList ID="lstCategory" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group" style="display: none">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","ItemRefCode") %></label>
                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtItemRefCode" class="form-control"></asp:TextBox>
                                </div>
                            </div>



                            <div class="form-group" style="display: none">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","ItemRFIDCode") %></label>
                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtItemRFIDCode" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","ItemFinanceCode") %></label>
                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtItemFinanceCode" class="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%=GetGlobalResourceObject("pages","QUnitCode") %> </label>
                                <div class="col-md-12">
                                    <asp:DropDownList ID="lstQunit" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                            </div>




                            <div class="form-group">
                                <label class="col-md-12 control-label"><%= GetGlobalResourceObject("pages","ItemImage") %></label>

                                <div class="col-md-12">
                                    <div class="input-group">
                                        <asp:Label ID="lblimage" runat="server"></asp:Label>
                                        <asp:FileUpload ID="txtImage" runat="server" />


                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","ItemNameEn") %>  </label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtItemNameEn" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","ItemNameAr") %>  </label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtItemNameAr" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","MinQty") %></label>
                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtMinQty" class="form-control"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-4">

                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","ItemDescEn") %>  </label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtItemDescEn" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","ItemDescAr") %>  </label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtItemDescAr" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group" style="display: none">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","ScrapPeriod") %></label>
                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtScrapPeriod" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group" style="display: none">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","ScrapAmount") %></label>
                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtScrapAmount" class="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","Active") %>  </label>

                                <div class="col-md-12">
                                    <asp:CheckBox ID="chkisactive" runat="server" class=" " ClientIDMode="Static" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-footer border-top text-muted">
                <div class="pull-left">
                    <asp:LinkButton ID="btnSave" runat="server" class="btn btn-primary" OnClientClick="return chkImage();" OnClick="btnSave_Click"><i class='fa fa-save'></i>&nbsp; <%=GetGlobalResourceObject("pages","Submit") %> </asp:LinkButton>
                    &nbsp;
				            <asp:Button runat="server" ID="btnCancel" class="btn btn-default" Text=" <%$ Resources: Pages, Cancel %> " OnClick="btnCancel_Click" />
                </div>

            </div>
        </div>



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
                                                            <asp:DropDownList ID="lstFilterCategory" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","QUnitCode") %> </label>
                                                            <asp:DropDownList ID="lstFilterQUnit" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>
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
                                                <li>
                                                    <asp:LinkButton OnClientClick="return checkDelete();" runat="server" ID="btnDelete" OnClick="btnDelete_Click"><i class="icon ni ni-trash"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %></asp:LinkButton></li>
                                                <li>
                                                    <asp:LinkButton   runat="server" ID="lnkUpdatePrice" OnClick="lnkUpdatePrice_Click" CssClass="text-danger"><i class="icon ni ni-edit"></i>&nbsp;<%=GetGlobalResourceObject("pages","UpdateMasterPrice") %></asp:LinkButton></li>
                                            </ul>

                                        </div>
                                    </div>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="btnNew" class="btn btn-icon btn-primary" OnClick="btnNew_Click"><em class="icon ni ni-plus"></em></asp:LinkButton>

                                </li>
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

                    <asp:DataGrid runat="server" ID="grdItems" AutoGenerateColumns="False"
                        AllowPaging="true" PageSize="20" class="table table-hover table-striped table-bordered table-advanced tablesorter table-responsive" data-auto-responsive="false" OnItemDataBound="grdItems_ItemDataBound" OnEditCommand="grdItems_EditCommand">
                        <PagerStyle Visible="false" />
                        <Columns>
                            <asp:BoundColumn DataField="code" Visible="False"></asp:BoundColumn>
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
                                    <%# gets(Eval("ItemImage")).Equals("") ? "<img src='" + Resources.Utilities.resourcespath +"uploads/no.png' height='20px' />": FillImage(gets(Eval("ItemImage")), Resources.Utilities.resourcespath+"uploads/ItemsData/", 35, 25,"")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="D_ItemsCategoryTitleAr" HeaderText="<%$ Resources:pages,Category %>"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ItemRefCode" HeaderText="<%$ Resources:pages,ItemRefCode %>"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ItemNameEn" HeaderText="<%$ Resources:pages,ItemNameAr %>"></asp:BoundColumn>
                            <asp:BoundColumn DataField="D_QtyUnitTitleAr" HeaderText="<%$ Resources:pages,QUnit %>"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EstimatedUnitCost" HeaderText="<%$ Resources:pages,LastPrice %>">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MinQty" HeaderText="<%$ Resources:pages,MinQty %>">
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="<%$ Resources:pages,MasterPrice %>">
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtmasterPrice" Text='<%#Eval("ItemMasterPrice") %>' class="form-control"></asp:TextBox>

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
                                                <li>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" class="btn btn-default btn-xs"><em class="icon ni ni-cards-fill"></em><span> <%=GetGlobalResourceObject("pages","Edit") %></span> </asp:LinkButton></li>

                                                <li>
                                                    <a><em class="icon ni ni-histroy"></em><span><%=GetGlobalResourceObject("pages","ItemTracking") %></span> </a></li>

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
