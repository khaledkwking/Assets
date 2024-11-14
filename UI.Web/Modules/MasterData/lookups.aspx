<%@ Page Language="c#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeFile="lookups.aspx.cs" Inherits="UI.Web.Modules.MasterData.lookups" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function chkImage() {
            var txt = document.getElementById("<%=txtNameEn.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please, Enter English Title");
                txt.focus();
                return false;
            }

            var txt = document.getElementById("<%=txtNameAr.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please, Enter English Title");
                txt.focus();
                return false;
            }
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
                    <li><i class="icon ni ni-home"></i>&nbsp;<a href="/admin/pages/home.aspx"><%=GetGlobalResourceObject("pages","home") %></a>&nbsp;&nbsp;<i class="icon ni ni-chevrons-left"></i>&nbsp;&nbsp;</li>
                    <li class="active"><%=_PageTitle %></li>
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
                                                <asp:LinkButton OnClientClick="return checkDelete();" runat="server" ID="btnDelete" OnClick="btnDelete_Click"><i class="icon ni ni-trash"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %></asp:LinkButton></li>

                                        </ul>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <asp:LinkButton runat="server" ID="btnNew" class="btn btn-icon btn-primary" OnClick="btnNew_Click"><em class="icon ni ni-plus"></em></asp:LinkButton></li>

                        </ul>
                    </div>
                </div>
                <!-- .toggle-wrap -->
            </div>
            <!-- .nk-block-head-content -->
        </div>
        <!-- .nk-block-between -->
    </div>

    <div class="nk-block">


        <div class="card card-bordered" id="tblAdd" runat="server" visible="false">
            <div class="card-header border-bottom">
                <asp:Label runat="server" ID="lblSubTitle"><%=GetGlobalResourceObject("pages","AddNewRecord") %></asp:Label>
            </div>
            <div class="card card-stretch">
            <div class="card-inner">
                <asp:Label runat="server" ID="lblerror"></asp:Label>

                <div role="form" class="form-horizontal">
                    <div class="row">
                        <div class="col-md-6">


                            <div class="form-group">
                                <label class="col-md-3 control-label"><%=GetGlobalResourceObject("pages","Title(Ar)") %></label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" placeholder="Enter Arabic Title" class="form-control" ID="txtNameAr"></asp:TextBox>
                                </div>

                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","Title(En)") %></label>

                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtNameEn" placeholder="Enter English title" class="form-control"></asp:TextBox>
                                </div>
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

        <div class="card card-bordered" id="tblshow" runat="server">
              <div class="card card-stretch">
            <%--<div class="card-header border-bottom"><%=GetGlobalResourceObject("pages","DataListing") %></div>--%>
            <div class="card-inner">

                <div class="card-inner p-0">
                    <div class="portlet box">

                        <div class="portlet-body">



                            <asp:DataGrid runat="server" ID="grdData" AutoGenerateColumns="False"
                                AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter" data-auto-responsive="false" OnItemDataBound="grdData_ItemDataBound" OnEditCommand="grdData_EditCommand">

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

                                    <asp:BoundColumn DataField="TitleEn" HeaderText="<%$ Resources:Pages , Title(En) %>"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="TitleAr" HeaderText="<%$ Resources:Pages , Title(Ar) %>"></asp:BoundColumn>


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
    </div>
</asp:Content>

