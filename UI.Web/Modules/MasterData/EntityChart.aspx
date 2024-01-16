<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="EntityChart.aspx.cs" Inherits="UI.Web.Modules.MasterData.EntityChart" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="JavaScript" type="text/javascript">
        function chkImage() {
          <%--  var txt = document.getElementById("<%=txttitleEn.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please, Enter English Name");
                txt.focus();
                return false;
            }--%>

            var txt = document.getElementById("<%=txttitleAr.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please, Enter Entity Name");
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

        function validateselected(type) {

            var txt = document.getElementById("<%=hdnSelectedNode.ClientID %>")

            if (txt.value == "" || txt.value == "0") {
                Swal.fire("فظلا ، اختر الجهة من الهيكل التنظيمي  ");
                txt.focus();
                return false;
            }
            if (type == 1) {
                call_cbox('LocationsList.aspx?add=1&entityId=' + txt.value);
            } else {
                call_cboxSmall('Locationslink.aspx?entityId=' + txt.value);
            }
            return true;
        }
        function AddNewEmp() {

            var txt = document.getElementById("<%=hdnSelectedNode.ClientID %>")

            if (txt.value == "" || txt.value == "0") {
                Swal.fire("فظلا ، اختر الجهة من الهيكل التنظيمي  ");
                txt.focus();
                return false;
            }
            call_cbox('EmployeeList.aspx?Add=1&RefEntityCode=' + txt.value);
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
                    <li><i class="fa fa-home"></i>&nbsp;<a href="/admin/pages/home.aspx"><%=GetGlobalResourceObject("pages","home") %></a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
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
                                    <div class="col-md-4" style="background: #f2f2f2; padding: 10px; border-radius: 5px;">

                                        <div class="panel panel-yellow" style="min-height: 70vh">
                                            <div class="panel-heading clearfix">

                                                <div class="toolbars">
                                                    <div class="input-icon left">
                                                        <i class="icon ni ni-search"></i>
                                                        <input id="treeSearch" type="text" placeholder="بحث" class="form-control input-medium">
                                                    </div>
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
                                    <div class="col-md-8">
                                        <ul class="nav nav-tabs">
                                            <li class="nav-item">
                                                <a class="nav-link active" data-toggle="tab" href="#tabItem0"><em class="icon ni ni-users-fill"></em><span>بيانات الجهة </span></a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link" data-toggle="tab" href="#tabItem5"><em class="icon ni ni-users-fill"></em><span>قائمة الموظفين (<asp:Label ID="lblEmpCount" runat="server" ClientIDMode="Static"></asp:Label>)</span></a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link" data-toggle="tab" href="#tabItem6"><em class="icon ni ni-building"></em><span>قائمة الاماكن (<asp:Label ID="lblLocatoinCount" runat="server" ClientIDMode="Static"></asp:Label>)</span></a>
                                            </li>

                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tabItem0">



                                                <div class="card-inner  p-0 pull-left" data-select2-id="22" style="display: none">
                                                    <div class="card-title-group" data-select2-id="21">

                                                        <div class="card-tools mr-n1" data-select2-id="20">
                                                            <ul class="btn-toolbar gx-1" data-select2-id="19">


                                                                <li>
                                                                    <div class="dropdown">
                                                                        <a href="#" class="btn btn-dim btn-primary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                                                            <em class="icon ni ni-setting"></em>
                                                                        </a>
                                                                        <div class="dropdown-menu  dropdown-menu-right" style="">
                                                                            <ul class="link-check">
                                                                                <li>
                                                                                    <a onclick="return validateselected(1);" href="javascript:void(0)" id="lnkAddLocation"><span class='nk-menu-icon'><em class='icon ni ni-plus'></em></span><span class='nk-menu-text'>إضافة جديد &nbsp;</span> </a>
                                                                                </li>
                                                                                <li><a onclick="return validateselected(2);" href="javascript:void(0)" id="lnkLocationlink"><span class='nk-menu-icon'><em class='icon ni ni-link'></em></span><span class='nk-menu-text'>ربط الجهات بالمواقع</span></a></li>

                                                                            </ul>

                                                                        </div>
                                                                    </div>
                                                                </li>

                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="card-inner">
                                                    <asp:Label runat="server" ID="lblerror"></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hdnSelectedNode" ClientIDMode="Static" />
                                                    <asp:HiddenField runat="server" ID="hdnSelectedEditNode" ClientIDMode="Static" />
                                                    <div role="form" class="form-horizontal">
                                                        <div class="row">

                                                            <div class="col-md-12">

                                                                <div class="form-group">
                                                                    <label class="col-md-12 control-label" for=""><%=GetGlobalResourceObject("pages","SubFrom") %> </label>

                                                                    <div class="col-md-12">
                                                                        <asp:DropDownList ID="LstLocationParent" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" style="display: none">
                                                                    <label class="col-md-12 control-label" for=""><%=GetGlobalResourceObject("pages","EntityName") %> (En)</label>

                                                                    <div class="col-md-12">
                                                                        <asp:TextBox runat="server" ID="txttitleEn" placeholder="Enter English Name" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-md-12 control-label"><%=GetGlobalResourceObject("pages","EntityName") %></label>

                                                                    <div class="col-md-12">

                                                                        <asp:TextBox runat="server" placeholder="Enter Arabic Name" class="form-control" ID="txttitleAr" ClientIDMode="Static"></asp:TextBox>


                                                                    </div>

                                                                </div>
                                                                <div class="form-group" style="display: none">
                                                                    <label class="col-md-12 control-label"><%=GetGlobalResourceObject("pages","FinRefCode") %>  </label>
                                                                    <div class="col-md-12">
                                                                        <asp:TextBox runat="server" class="form-control" ID="txtFinRefCode" ClientIDMode="Static"></asp:TextBox>
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


                                                </div>
                                            </div>

                                            <div class="tab-pane" id="tabItem5">

                                                <div class="card-inner  p-0 pull-left" data-select2-id="22">
                                                    <div class="card-title-group" data-select2-id="21">

                                                        <div class="card-tools mr-n1" data-select2-id="20">
                                                            <ul class="btn-toolbar gx-1" data-select2-id="19">

                                                                <li>
                                                                    <div class="dropdown">
                                                                        <a onclick="return AddNewEmp();" href="javascript:void(0)" id="lnkOrgChart" class="btn btn-dim btn-primary">
                                                                            <em class="icon ni ni-plus"></em>
                                                                        </a>

                                                                    </div>
                                                                </li>

                                                            </ul>
                                                        </div>

                                                    </div>
                                                </div>


                                                <div class="card-inner p-0">
                                                    <table id="employeeList-datatable" class="table table-hover table-striped table-bordered table-advanced tablesorter"></table>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="tabItem6">



                                                <div class="card-inner  p-0 pull-left" data-select2-id="22">
                                                    <div class="card-title-group" data-select2-id="21">

                                                        <div class="card-tools mr-n1" data-select2-id="20">
                                                            <ul class="btn-toolbar gx-1" data-select2-id="19">


                                                                <li>
                                                                    <div class="dropdown">
                                                                        <a href="#" class="btn btn-dim btn-primary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                                                            <em class="icon ni ni-setting"></em>
                                                                        </a>
                                                                        <div class="dropdown-menu  dropdown-menu-right" style="">
                                                                            <ul class="link-check">
                                                                                <li>
                                                                                    <a onclick="return validateselected(1);" href="javascript:void(0)" id="lnkAddLocation"><span class='nk-menu-icon'><em class='icon ni ni-plus'></em></span><span class='nk-menu-text'>إضافة جديد &nbsp;</span> </a>
                                                                                </li>
                                                                                <li><a onclick="return validateselected(2);" href="javascript:void(0)" id="lnkLocationlink"><span class='nk-menu-icon'><em class='icon ni ni-link'></em></span><span class='nk-menu-text'>ربط الجهات بالمواقع</span></a></li>

                                                                            </ul>

                                                                        </div>
                                                                    </div>
                                                                </li>

                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="card-inner p-0">

                                                    <asp:HiddenField runat="server" ID="HiddenField1" ClientIDMode="Static" />
                                                    <table id="locationList-datatable" class="table table-hover table-striped table-bordered table-advanced tablesorter"></table>
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

    <script src="/Layout/Assets/businessScripts/EntityChart.js"></script>
</asp:Content>
