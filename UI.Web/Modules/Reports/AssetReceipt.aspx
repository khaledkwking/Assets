<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/MainEmpty.Master" AutoEventWireup="true" CodeBehind="AssetReceipt.aspx.cs" Inherits="UI.Web.Modules.WHM.Forms.AssetReceipt" %>

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
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","RequestActionType") %></label>
                                                            <asp:DropDownList ID="lstFilterAction" runat="server" class="form-control form-select" data-search="on">
                                                                <asp:ListItem Text="الكل" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="عهدة شخصية" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="عهدة تنضيمية" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","ReceiptDateFrom") %></label>
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
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","ReceiptDateTo") %></label>
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
                                                            <asp:LinkButton runat="server" ID="btnFilter" class="btn btn-secondary" OnClick="btnFilter_Click"><i class="icon ni ni-search"></i>&nbsp;<%=GetGlobalResourceObject("pages","Filter") %> </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
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

                    <div class="card-inner" style="direction: ltr">
                        <asp:Label runat="server" ID="lblerror"></asp:Label>
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Visible="true" AsyncRendering="false" SizeToReportContent="true">
                        </rsweb:ReportViewer>


                    </div>
                     

                </div>

            </div>
        </div>
         
    </div>
     
</asp:Content>

