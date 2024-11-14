<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/MainEmpty.Master" AutoEventWireup="true" CodeBehind="EmployeeLocation.aspx.cs" Inherits="UI.Web.Modules.MasterData.EmployeeLocation" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="JavaScript" type="text/javascript">
        function chkImage() {


            return true;
        }
       
    </script>

      <asp:HiddenField runat="server" ID="hdnSelectedNode" ClientIDMode="Static" />


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




        <div class="card card-bordered" id="tblshow" runat="server">
            <div class="card card-stretch">

                <div class="card-inner">

                    <div class="card-inner p-0">
                        <div class="portlet box">
                            <div class="portlet-body">
                                <div class="row">

                                    <div class="pull-left" style="direction:ltr">
                                        <ul class="link-list-opt no-bdr">
                                            <li>
                                                <asp:LinkButton CssClass="btn btn-primary text-white lnkselected" runat="server"  ID="btnLink" OnClick="btnLink_Click"><i class="icon ni ni-link"></i>&nbsp;<%=GetGlobalResourceObject("pages","EmployeeLocation") %></asp:LinkButton></li>
                                        </ul>
                                    </div>
                                    <div class="col-md-12" style="background: #f2f2f2; padding: 10px; border-radius: 5px;">



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
                                                <div id="EmployeelocationTree" class=""></div>
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

    <script src="/Layout/Assets/businessScripts/EmployeeLocarion.js"></script>
</asp:Content>
