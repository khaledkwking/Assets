<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="AssetsTransferOut.aspx.cs" Inherits="UI.Web.Modules.Assets.AssetsTransferOut" %>


<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
          .pagination {
    display: flex;
    padding-left: 0;
    list-style: none;
    border-radius: 4px;
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    gap: 5px;
    max-width: 100%;
    padding: 0;
}
    </style>
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
        function ValidateITems() {
            var txt = document.getElementById("<%=txtTransDate.ClientID %>")
            if (txt.value == "") {
                Swal.fire("يرجى إدخال تاريخ التحويل");
                return false;
            }

             var txt = document.getElementById("<%=txtNotes.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("يرجى إدخال الملاحظات");
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
                                    <div class="col-md-3 treecontainer"  >

                                        <div class="panel panel-yellow" style="min-height: 70vh">
                                            <div class="panel-heading clearfix">
                                                <span class="mts"><%=GetGlobalResourceObject("pages","OrgChartTitle") %>  </span>
                                                <div style="float: left"><a href="../Reports/OrgChartPrint.aspx" class="btn btn-dim btn-primary  btn-xs iframe75 "><i class="icon ni ni-printer"></i></a></div>
                                            </div>
                                            <div class="panel-body" style="padding: 0px; padding-top: 25px;margin-bottom:10px;">

                                                <div class="form-control-wrap" style="margin-bottom:20px;">
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
                                                <a class="nav-link active" data-toggle="tab" href="#tabItem5"><em class="icon ni ni-users-fill"></em><span>قائمة الموظفين (<asp:Label ID="lblEmpCount" runat="server" ClientIDMode="Static"></asp:Label>)</span></a>
                                            </li>
                                            <li class="nav-item" style="display:none">
                                                <a class="nav-link" data-toggle="tab" href="#tabItem6"><em class="icon ni ni-building"></em><span>المواقع التابعة   (<asp:Label ID="lblcount" runat="server" ClientIDMode="Static">0</asp:Label>)</span></a>
                                            </li>
                                              <li class="nav-item">
                                                <a class="nav-link" data-toggle="tab" href="#tabCustody"><em class="icon ni ni-menu-circled"></em><span>حصر العهد   (<asp:Label ID="lblAssetsCount" runat="server" ClientIDMode="Static">0</asp:Label>)</span></a>
                                            </li>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tabItem5">

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
                                            <div class="tab-pane" id="tabItem6" style="display:none">



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

                                            <div class="tab-pane" id="tabCustody">

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
                                                                                <li><a id="lnkReportOrgCustody"   href="#" class="iframewide"  ><span class='nk-menu-icon'><em class='icon ni ni-files'></em></span><span class='nk-menu-text'>  تقرير مراقبة العهــد  </span></a></li>
                                                                                <li><a id="lnkReportOrgCustody2"  href="#" class="iframewide"  ><span class='nk-menu-icon'><em class='icon ni ni-files'></em></span><span class='nk-menu-text'>  جدول بيانات الأصول      </span></a></li>
                                                                                <li><a id="lnkReportOrgReceiptList"  href="#" class="iframewide text-danger"  ><span class='nk-menu-icon'><em class='icon ni ni-printer'></em></span><span class='nk-menu-text'> سجل إستمارات العهد </span></a></li>
                                                                                 

                                                                                 
                                                                            </ul>

                                                                        </div>
                                                                    </div>
                                                                </li>

                                                            </ul>
                                                        </div>

                                                    </div>
                                                </div>


                                                <div class="card-inner p-0">
                                                    <table id="custodyList-datatable" class="table table-hover table-striped table-bordered table-advanced tablesorter"></table>
                                                </div>
                                            </div>

                                        </div>
                                        <br />
                                        <div id="divTransferDetails" runat="server" style="border: 1px solid #dbdfea;border-radius: 12px;padding: 20px 20px 60px 20px;background-color:#dbdfea">
            <div class="col-lg-12">
                                                                <div class="portlet box portlet-blue">

                                                                    <div class="portlet-body">
                                                                        <div role="form">

                                                                            <div class="row">
                                                                                <div class="col-md-6">
                                                                                 

                                                                                    <div class="form-group">
                                                                                        <label class="control-label" for="">تاريخ التحويل</label>

                                                                                        <div class="form-control-wrap">
                                                                                            <div class="form-icon form-icon-right">
                                                                                                <em class="icon ni ni-calendar-alt"></em>
                                                                                            </div>
                                                                                            <asp:TextBox runat="server" ID="txtTransDate" placeholder="__/__/____" class="form-control date-picker"></asp:TextBox>
                                                                                        </div>


                                                                                    </div>




                                                                                </div>

                                                                                <div class="col-md-6">
                                                                                    <div class="form-group">
                                                                                                    <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","File") %> </label>

                                                                                                    <div class="col-md-9">
                                                                                                        <asp:FileUpload ID="txtFile" runat="server" />

                                                                                                        <%--<asp:AsyncFileUpload ID="txtimages" runat="server" OnUploadedComplete="txtimages_UploadedComplete" OnUploadedFileError="txtimages_UploadedFileError" />--%>
                                                                                                    </div>
                                                                                                </div>



                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label class="control-label" for="">ملاحظات</label>
                                                                                        <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>


                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div class="pull-left" style="margin-top: 10px;">
                                                                 <asp:LinkButton ID="btnSave" runat="server" class="btn btn-secondary" OnClick="btnSave_Click"><i class='icon ni ni-save'></i>&nbsp;  تحويل</asp:LinkButton>
                                                            
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
