<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="OrgChartTree.aspx.cs" Inherits="UI.Web.Modules.MasterData.OrgChartTree" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script language="JavaScript" type="text/javascript">
        function chkImage() {

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
                call_cbox('LocationsList.aspx?entityId=' + txt.value);
            } else {
                call_cboxSmall('Locationslink.aspx?entityId=' + txt.value);
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

        </div>
        <!-- .nk-block-between -->
    </div>

    <div class="nk-block">


        <div class="card card-bordered">
            <div class="card card-stretch">
                <div class="card-inner">

                    <div class="card-inner p-0">
                        <div class="portlet box">
                            <div class="portlet-body">
                                <div class="row">
                                    <div class="col-md-3 treecontainer">

                                        <div class="panel panel-yellow" style="min-height: 70vh">

                                            <div class="p-3 " style="background-color: #0C476B">
                                                <div class="d-flex">
                                                    <div class="align-self-center me-3">
                                                        <img src="/wwwroot/assets/images/logo/KuwaitLogo.png" class="avatar-xs rounded-circle" width="80px" alt="avatar-2">
                                                    </div>
                                                    <div class="flex-1" style="padding-top: 10px; padding-right: 10px;">
                                                        <h5 class="font-size-15 mb-1" style="color: #E4DAC1;">الأمانة العامة لمجلس الوزراء</h5>
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

                                        <ul class="nav nav-tabs">
                                            <li class="nav-item">
                                                <a class="nav-link active" data-toggle="tab" href="#tabCustody"><em class="icon ni ni-menu-circled"></em><span>حصر استمارات العهد   (<asp:Label ID="lblAssetsCountHeader" runat="server" ClientIDMode="Static">0</asp:Label>)</span></a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link " data-toggle="tab" href="#tabItem5"><em class="icon ni ni-users-fill"></em><span>قائمة الموظفين (<asp:Label ID="lblEmpCount" runat="server" ClientIDMode="Static"></asp:Label>)</span></a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link" data-toggle="tab" href="#tabItem6"><em class="icon ni ni-building"></em><span>المواقع التابعة   (<asp:Label ID="lblcount" runat="server" ClientIDMode="Static">0</asp:Label>)</span></a>
                                            </li>

                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tabCustody">

                                                <div class="card-inner  p-0 pull-left" data-select2-id="22">
                                                    <div class="card-title-group" data-select2-id="21">

                                                        <div class="card-tools mr-n1" data-select2-id="20">
                                                            <ul class="btn-toolbar gx-1" data-select2-id="19">
                                                                <li>
                                                                    <div class="dropdown">
                                                                        <a href="#" class="btn btn-round btn-icon btn-dim btn-primary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                                                            <em class="icon ni ni-more-h"></em>
                                                                        </a>
                                                                        <div class="dropdown-menu  dropdown-menu-right" style="">
                                                                            <ul class="link-check">
                                                                                <li><a id="lnkReportOrgCustody" href="#" class="iframewide"><span class='nk-menu-icon'><em class='icon ni ni-files'></em></span><span class='nk-menu-text'>تقرير مراقبة العهــد  </span></a></li>
                                                                                <li><a id="lnkReportOrgCustody2" href="#" class="iframewide"><span class='nk-menu-icon'><em class='icon ni ni-files'></em></span><span class='nk-menu-text'>جدول بيانات الأصول      </span></a></li>
                                                                                <li><a id="lnkReportOrgReceiptList" href="#" class="iframewide text-danger"><span class='nk-menu-icon'><em class='icon ni ni-printer'></em></span><span class='nk-menu-text'>سجل إستمارات العهد </span></a></li>



                                                                            </ul>

                                                                        </div>
                                                                    </div>
                                                                </li>

                                                            </ul>
                                                        </div>

                                                    </div>
                                                </div>


                                                <div class="card-inner p-0">
                                                    <table id="custodyList-datatableHeader" class="table table-hover table-striped table-bordered table-advanced tablesorter"></table>
                                                </div>
                                            </div>
                                            <div class="tab-pane " id="tabItem5">

                                                <div class="card-inner  p-0 pull-left" data-select2-id="22">
                                                    <div class="card-title-group" data-select2-id="21">

                                                        <div class="card-tools mr-n1" data-select2-id="20">
                                                            <ul class="btn-toolbar gx-1" data-select2-id="19">

                                                                <li>
                                                                    <div class="dropdown">
                                                                        <a href="OrgChart.aspx?entityId=<%=hdnSelectedNode.Value %>" id="lnkOrgChart" class="btn btn-round btn-icon btn-dim btn-primary dropdown-toggle iframewide">
                                                                            <em class="icon ni ni-network"></em>
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
                                                                        <a href="#" class="btn btn-round btn-icon btn-dim btn-primary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                                                            <em class="icon ni ni-setting"></em>
                                                                        </a>
                                                                        <div class="dropdown-menu  dropdown-menu-right" style="">
                                                                            <ul class="link-check">
                                                                                <li><a onclick="return validateselected(2);" href="javascript:void(0)" id="lnkLocationlink"><span class='nk-menu-icon'><em class='icon ni ni-link'></em></span><span class='nk-menu-text'>ربط الجهات بالمواقع</span></a></li>
                                                                                <li>
                                                                                    <asp:LinkButton runat="server" ID="lnkDeleteOrgLocation" OnClick="lnkDeleteOrgLocation_Click"><em class='icon ni ni-trash'></em><span class='nk-menu-text'>حذف مواقع الجهة  </span></asp:LinkButton>
                                                                                </li>

                                                                                <li><a onclick="return validateselected(1);" href="javascript:void(0)" id="lnkAddLocation"><span class='nk-menu-icon'><em class='icon ni ni-plus'></em></span><span class='nk-menu-text'>إضافة جديد &nbsp;</span> </a></li>
                                                                            </ul>

                                                                        </div>
                                                                    </div>
                                                                </li>

                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="card-inner">
                                                    <asp:HiddenField runat="server" ID="hdnSelectedNode" ClientIDMode="Static" />
                                                    <%--<table id="locationList-datatable" class="table table-hover table-striped table-bordered table-advanced tablesorter"></table>--%>
                                                    <iframe style="width: 100%; height: 1500px" id="entityLocationFrame" frameborder="0"></iframe>

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

    <script src="/wwwroot/assets/js/businessScripts/orgchart.js"></script>

</asp:Content>
