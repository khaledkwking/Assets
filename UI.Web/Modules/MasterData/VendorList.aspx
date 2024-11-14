<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="VendorList.aspx.cs" Inherits="UI.Web.Modules.MasterData.VendorList" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="JavaScript" type="text/javascript">
        function chkImage() {

            var txt = document.getElementById("<%=txtVendorNameEn.ClientID %>")
            if (txt.value == "") {

                // Swal.fire("فضلا ، ادخل اسم المورد بالانجليزية");
                Swal.fire("فضلا ، ادخل اسم المورد بالانجليزية")
                txt.focus();
                return false;
            }

            var txt = document.getElementById("<%=txtVendorNameAr.ClientID %>")
            if (txt.value == "") {
                Swal.fire("فضلا ، ادخل اسم المورد بالعربية");
                txt.focus();
                return false;
            }
            var txt = document.getElementById("<%=lstCountry.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("فضلا ، إختر الدولة");
                txt.focus();
                return false;
            }

            var txt = document.getElementById("<%=txtPhone.ClientID %>")
            if (txt.value == "") {
                Swal.fire("فضلا ، ادخل الهاتف    ");
                txt.focus();
                return false;
            }

            var txt = document.getElementById("<%=txtContactPerson.ClientID %>")
            if (txt.value == "") {
                Swal.fire("فضلا ، ادخل الشخص المفوض    ");
                txt.focus();
                return false;
            }
            return true;
        }
    </script>




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

                        <div class="col-md-6">


                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","FullName") %>  (En)</label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtVendorNameEn" placeholder="Enter English Name" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-12 control-label"><%= GetGlobalResourceObject("pages","FullName") %> (Ar)</label>

                                <div class="col-md-12">

                                    <asp:TextBox runat="server" placeholder="Enter Arabic Name" class="form-control" ID="txtVendorNameAr"></asp:TextBox>


                                </div>

                            </div>
                            <div class="form-group">
                                <label class="col-md-12 control-label"><%=GetGlobalResourceObject("pages","RefNo") %></label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" placeholder="Enter Ref No" class="form-control" ID="txtRefCode"></asp:TextBox>

                                </div>

                            </div>

                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%=GetGlobalResourceObject("pages","Country") %> </label>

                                <div class="col-md-12">
                                    <asp:DropDownList ID="lstCountry" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-12 control-label"><%= GetGlobalResourceObject("pages","logo") %></label>

                                <div class="col-md-12">
                                    <div class="input-group">
                                        <asp:Label ID="lblimage" runat="server"></asp:Label>
                                        <asp:FileUpload ID="txtImage" runat="server" />


                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","Phone") %>  </label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtPhone" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","fax") %>  </label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtfax" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","Email") %>  </label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtEmail" class="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","ContactPerson") %>  </label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtContactPerson" class="form-control"></asp:TextBox>
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","ContactpersonPhone") %>  </label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtContactpersonPhone" class="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","AddressDetails") %>  </label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtAddressDetails" class="form-control"></asp:TextBox>
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
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","County") %></label>
                                                            <asp:DropDownList ID="lstFIlterCounty" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                   
                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","RefNo") %></label>
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
                                                    <asp:LinkButton OnClientClick="return checkDelete();" runat="server" ID="LinkButton2" OnClick="btnDelete_Click"><i class="icon ni ni-trash"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %></asp:LinkButton></li>
                                            </ul>

                                        </div>
                                    </div>
                                </li>
                                <li>
                                    <asp:LinkButton runat="server" ID="LinkButton3" class="btn btn-icon btn-primary" OnClick="btnNew_Click"><em class="icon ni ni-plus"></em></asp:LinkButton>

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

                     <asp:DataGrid runat="server" ID="grdVendorList" AutoGenerateColumns="False"
                                AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter" data-auto-responsive="false" OnItemDataBound="grdVendorList_ItemDataBound" OnEditCommand="grdVendorList_EditCommand">

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

                                    <asp:TemplateColumn HeaderText="<%$ Resources:pages,FullName %>">
                                        <ItemTemplate>

                                         
                                          

                                            <div class="user-card">
                                                <div class="user-avatar">

                                                    <%# gets(Eval("logo")).Equals("") ? "<img src='" + Resources.Utilities.resourcespath +"uploads/no.png' height='20px' />": FillImage(gets(Eval("logo")), Resources.Utilities.resourcespath+"uploads/VendorsData/", 35, 25,"")%>

                                                </div>
                                                <div class="user-info">
                                                    <span class="tb-lead">  <%#Eval("VendorNameAr") %> <span class="dot dot-warning d-md-none ml-1"></span></span>
                                                    <div class="cardDetails"> <%#Eval("ContactPerson") %> </div>
                                                </div>
                                            </div>

                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <%-- <asp:BoundColumn DataField="VendorNameAr" HeaderText="<%$ Resources:pages,FullName %>"></asp:BoundColumn>--%>
                                    <asp:BoundColumn DataField="RefCode" HeaderText="<%$ Resources:pages,RefCode %>"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Phone" HeaderText="<%$ Resources:pages,Phone %>"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="fax" HeaderText="<%$ Resources:pages,fax %>"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Email" HeaderText="<%$ Resources:pages,Email %>"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="<%$ Resources:pages,Country %>">
                                        <ItemTemplate>
                                            <%#Eval("D_Country.TitleAr") %>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <%--<asp:BoundColumn DataField="ContactPerson" HeaderText="<%$ Resources:pages,ContactPerson %>"></asp:BoundColumn>--%>
                                    <asp:BoundColumn DataField="ContactpersonPhone" HeaderText="<%$ Resources:pages,ContactpersonPhone %>"></asp:BoundColumn>
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

        <asp:UpdatePanel runat="server" ID="Updatepanel1" ChildrenAsTriggers="true" UpdateMode="conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>

    
</asp:Content>
