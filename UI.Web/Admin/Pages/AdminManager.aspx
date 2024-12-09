<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="AdminManager.aspx.cs" Inherits="UI.Web.Admin.Pages.AdminManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <script language="JavaScript" type="text/javascript">
        function chkImage() {

            txt = document.getElementById("<%=lstadminType.ClientID %>")
            if (txt.value == "0") {

                Swal.fire("Please select User Type");
                txt.focus();
                return false;
            }

            var txt = document.getElementById("<%=txtfullName.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please select User FullName");
                txt.focus();
                return false;
            }

            var txt = document.getElementById("<%=txtName.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please select  User Name");
                txt.focus();
                return false;
            }

            txt = document.getElementById("<%=txtPassword.ClientID%>")
            if (txt.value == "") {
                Swal.fire("Please select User Password");
                txt.focus();
                return false;
            }

            txt = document.getElementById("<%=txtmobile.ClientID%>")
            if (txt.value == "") {
                Swal.fire("Please select user Mobile");
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

           <div class="row">
               <div class="col-md-3 treecontainer">

                                        <div class="panel panel-yellow" style="min-height: 70vh">

                                            <div class="p-1 " style="background-color: #0C476B;margin-bottom:20px">
                                                <div class="d-flex">
                                                    <div class="align-self-center me-2">
                                                        <img src="/wwwroot/assets/images/logo/KuwaitLogo.png" class="avatar-xs rounded-circle" width="50px" alt="avatar-2">
                                                    </div>
                                                    <div class="flex-1" style="padding-top: 10px; padding-right: 10px;">
                                                        <h5 class="font-size-15 mb-1" style="color: #E4DAC1;font-size:16px;">الأمانة العامة لمجلس الوزراء</h5>
                                                        <p class="text-muted text-truncate mb-0" style="line-height: 20px; font-size: 13px; color: #E4DAC1 !important;">الهيكل التنظيمي  </p>

                                                    </div>


                                                </div>
                                            </div>

                                            <%--  <div class="panel-heading clearfix">
                                                <span class="mts"><%=GetGlobalResourceObject("pages","OrgChartTitle") %>  </span>
                                                <div style="float: left"><a href="../Reports/OrgChartPrint.aspx" class="btn btn-dim btn-primary  btn-xs iframe75 "><i class="icon ni ni-printer"></i></a></div>
                                            </div>--%>
                                            <div class="panel-body" style="padding: 0px; padding-top: 0px; margin-bottom: 10px;">

                                                <div class="form-control-wrap" style="margin-bottom: 20px;">
                                                    <div class="form-icon form-icon-right">
                                                        <em class="icon ni ni-search"></em>
                                                    </div>
                                                    <input type="text" class="form-control" id="treeSearch" placeholder="  بحث الهيكل التنظيمي">
                                                </div>

                                                <div id="progressbar">
                                                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                                    <span class="sr-only">Loading...</span>
                                                </div>

                                                <div id="orgTree" class=""></div>
                                            </div>
                                        </div>
                                    </div>
             
               <div class="col-md-9">
        <div class="card card-bordered" id="tblAdd" runat="server" visible="false">
            <div class="card-header border-bottom">
                <asp:Label runat="server" ID="lblSubTitle"><%=GetGlobalResourceObject("pages","AddNewRecord") %></asp:Label>
            </div>
            <div class="card-inner">
                <asp:Label runat="server" ID="lblerror"></asp:Label>

                <div role="form" class="form-horizontal">
                    <div class="row">
                        <div class="col-md-6">


                            <div class="form-group" style="display: none">
                                <label class="col-md-12 control-label" for="">Profile </label>

                                <div class="col-md-12">
                                    <asp:DropDownList runat="server" ID="lstComany" class="form-control" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""><%= GetGlobalResourceObject("pages","AdminType") %></label>

                                <div class="col-md-12">

                                    <asp:DropDownList ID="lstadminType" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-12 control-label" for=""> <%= GetGlobalResourceObject("pages","FullName") %></label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtfullName" placeholder="Enter Full Name" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-12 control-label"> <%= GetGlobalResourceObject("pages","UserName") %></label>

                                <div class="col-md-12">
                                    <div class="input-icon">
                                        <i class="fa fa-user"></i>


                                        <asp:TextBox runat="server" placeholder="Enter User Name" class="form-control" ID="txtName"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-12 control-label"> <%= GetGlobalResourceObject("pages","password") %>  </label>

                                <div class="col-md-12">
                                    <div class="input-icon">
                                        <i class="fa fa-fa-chain"></i>
                                        <asp:TextBox runat="server" TextMode="Password" placeholder="Enter Password" class="form-control" ID="txtPassword"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-12 control-label"> <%= GetGlobalResourceObject("pages","photo") %></label>

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
                                <label class="col-md-12 control-label"><%= GetGlobalResourceObject("pages","Email") %></label>

                                <div class="col-md-12">
                                    <div class="input-icon">
                                        <i class="fa fa-envelope"></i>
                                        <asp:TextBox runat="server" placeholder="email@yourcompany.com" class="form-control" ID="txtEmail"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-12 control-label"><%= GetGlobalResourceObject("pages","Mobile") %> </label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" placeholder="Enter User Contact mobile" class="form-control" ID="txtmobile"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group" style="display:none">
                                <label class="col-md-12 control-label">Address</label>

                                <div class="col-md-12">
                                    <asp:TextBox runat="server" placeholder="Enter User Address" TextMode="MultiLine" class="form-control" ID="txtaddress"></asp:TextBox>
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="col-md-12 control-label"><%= GetGlobalResourceObject("pages","Active") %>   </label>

                                <div class="col-md-12">
                                    <asp:CheckBox ID="chkisactive" runat="server" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-12 control-label"><%= GetGlobalResourceObject("pages","IsOperation") %>  </label>

                                <div class="col-md-12">
                                    <asp:CheckBox ID="chkOperation" runat="server" />
                                    <span class="text-warning mts help-block-right">Has permission to manage Store Operation</span>
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
            <div class="card-header border-bottom"><%=GetGlobalResourceObject("pages","DataListing") %></div>
            <div class="card-inner">

                <div class="card-inner p-0">
                    <div class="portlet box">

                        <div class="portlet-body">



                            <asp:DataGrid runat="server" ID="grdData" AutoGenerateColumns="False"
                                AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter" data-auto-responsive="false" OnItemDataBound="grdData_ItemDataBound" OnEditCommand="grdData_EditCommand">

                                <Columns>
                                    <asp:BoundColumn DataField="id" Visible="False"></asp:BoundColumn>
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
                                    <asp:TemplateColumn HeaderText="image">
                                        <ItemStyle Width="5%" />
                                        <ItemTemplate>

                                            <%# FillImage(gets(Eval("AdminPhoto")), Resources.Utilities.resourcespath+"uploads/Adminprofile/", 35, 25,"")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="<%$ Resources:pages,AdminType %>">

                                        <ItemTemplate>
                                            <%# Eval("Security_pr_AdminType.NameEn")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="name" HeaderText="<%$ Resources:pages,fullname %>"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="username" HeaderText="<%$ Resources:pages,UserName %>"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="<%$ Resources:pages,Active %>">

                                        <ItemTemplate>
                                            <%#ShowYesNo(getBool(Eval("IsActive")))%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="<%$ Resources:pages,isOperation %>">

                                        <ItemTemplate>
                                            <%#ShowYesNo(getBool(Eval("isOperation")))%>
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

                                                    <asp:HiddenField runat="server" ID="hdnSelectedNode" ClientIDMode="Static" />

           </div>

    </div>

    <script src="/wwwroot/assets/js/businessScripts/orgchart.js"></script>

</asp:Content>
