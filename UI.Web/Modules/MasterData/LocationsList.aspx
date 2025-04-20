<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="LocationsList.aspx.cs" Inherits="UI.Web.Modules.MasterData.LocationsList" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="JavaScript" type="text/javascript">
        function chkImage() {
            var txt = document.getElementById("<%=txttitleEn.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please, Enter English Name");
                txt.focus();
                return false;
            }

            var txt = document.getElementById("<%=txttitleAr.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please, Enter English Name");
                txt.focus();
                return false;
            }
            return true;
        }
        function checkCategoryDelete() {

            var txt = document.getElementById("<%=hdnSelectedNode.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("فظلا ، اختر البيان للحذف");
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
    <!--END TITLE & BREADCRUMB PAGE-->
    <!--BEGIN CONTENT-->

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
                                                <asp:LinkButton OnClientClick="return checkCategoryDelete();" runat="server" ID="btnDelete" OnClick="btnDelete_Click"><i class="icon ni ni-trash"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %></asp:LinkButton></li>

                                        </ul>
                                    </div>
                                </div>
                            </li>
                            <%--<li>
                                <asp:LinkButton runat="server" ID="btnNew" class="btn btn-icon btn-primary" OnClick="btnNew_Click"><em class="icon ni ni-plus"></em></asp:LinkButton>

                            </li>--%>
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
            <div class="card-inner">
            </div>
            <div class="card-footer border-top text-muted">
            </div>
        </div>


        <div class="card card-bordered" id="tblshow" runat="server">
            <div class="card card-stretch">

                <div class="card-inner">

                    <div class="card-inner p-0">
                        <div class="portlet box">
                            <div class="portlet-body">
                                <div class="row">
                                    <div class="col-md-6" style="background: #f2f2f2; padding: 10px; border-radius: 5px;">

                                        <div class="panel panel-yellow" style="min-height: 70vh">
                                            <div class="panel-heading clearfix">



                                                <div class="form-control-wrap">
                                                    <div class="form-icon form-icon-right">
                                                        <em class="icon ni ni-search"></em>
                                                    </div>
                                                    <input type="text" class="form-control" id="treeSearch" placeholder="  بحث    ">
                                                </div>

                                            </div>
                                            <div class="panel-body" style="padding: 0px; padding-top: 25px;">
                                                <div id="progressbar">
                                                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"><span class="sr-only">Loading...</span></span>

                                                </div>
                                                <div id="locationTree" class=""></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">

                                        <asp:Label runat="server" ID="lblerror"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnSelectedNode" ClientIDMode="Static" />
                                        <asp:HiddenField runat="server" ID="hdnSelectedEditNode" ClientIDMode="Static" />
                                        <div role="form" class="form-horizontal">
                                            <div class="row">

                                                <div class="col-md-12">

                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label" for=""><%=GetGlobalResourceObject("pages","ParentLocation") %> </label>

                                                        <div class="col-md-12">
                                                            <asp:DropDownList ID="LstLocationParent" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label" for=""><%=GetGlobalResourceObject("pages","locationType") %> </label>

                                                        <div class="col-md-12">
                                                            <asp:DropDownList ID="lstLocationType" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static"></asp:DropDownList>
                                                        </div>
                                                    </div>


                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label" for=""><%=GetGlobalResourceObject("pages","LocationName") %> (En)</label>

                                                        <div class="col-md-12">
                                                            <asp:TextBox runat="server" ID="txttitleEn" placeholder="Enter English Name" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label"><%=GetGlobalResourceObject("pages","LocationName") %> (Ar)</label>

                                                        <div class="col-md-12">

                                                            <asp:TextBox runat="server" placeholder="Enter Arabic Name" class="form-control" ID="txttitleAr" ClientIDMode="Static"></asp:TextBox>


                                                        </div>

                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label"><%=GetGlobalResourceObject("pages","FinRefCode") %>  </label>
                                                        <div class="col-md-12">
                                                            <asp:TextBox runat="server" class="form-control" ID="txtFinRefCode" ClientIDMode="Static"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">المدينة </label>
                                                        <div class="col-md-12">
                                                            <asp:TextBox runat="server" class="form-control" ID="txtCity" ClientIDMode="Static"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="pull-left">
                                                        <asp:LinkButton ID="btnSave" runat="server" class="btn btn-primary" OnClientClick="return chkImage();" OnClick="btnSave_Click"><i class='fa fa-save'></i>&nbsp; <%=GetGlobalResourceObject("pages","Submit") %> </asp:LinkButton>
                                                        &nbsp;
				                                                <asp:Button runat="server" ID="btnCancel" class="btn btn-default" Text=" <%$ Resources: Pages, Cancel %> " OnClick="btnCancel_Click" />
                                                    </div>


                                                </div>


                                            </div>
                                        </div>

                                        <div style="display: none">

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
            </div>
        </div>


    </div>

    <script src="/wwwroot/assets/js/businessScripts/locationChart.js"></script>
</asp:Content>
