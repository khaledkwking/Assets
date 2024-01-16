<%@ Page Language="c#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeFile="PurchaseList.aspx.cs" Inherits="UI.Web.Modules.WHM.Forms.PurchaseList" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function chkImage() {

            return true;
        }
    </script>


    <div id="title-breadcrumb-option-demo" class="page-title-breadcrumb">
        <div class="page-header pull-left">
            <div class="page-title"><%=GetGlobalResourceObject("pages","ReceivingList") %></div>
        </div>
        <ol class="breadcrumb page-breadcrumb pull-right">
            <li><i class="fa fa-home"></i>&nbsp;<a href="/admin/pages/home.aspx"><%=GetGlobalResourceObject("pages","home") %></a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li><a href="#"><%=GetGlobalResourceObject("pages","ReceivingOperations") %>  </a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><%=GetGlobalResourceObject("pages","ReceivingList") %></li>
        </ol>
        <div class="clearfix"></div>
    </div>


    <asp:UpdatePanel runat="server" ID="Updatepanel1" ChildrenAsTriggers="true" UpdateMode="conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!--END TITLE & BREADCRUMB PAGE-->
    <!--BEGIN CONTENT-->
    <div class="page-content">

        <div class="row">
            <asp:Label runat="server" ID="lblerror"></asp:Label>
        </div>

        <div class="row" id="tblshow" runat="server">
            <div class="col-lg-12">
                <div class="portlet box">
                    <div class="portlet-header">
                        <div class="caption"><%=GetGlobalResourceObject("pages","DataListing") %></div>
                        <div class="actions">

                            <asp:LinkButton runat="server" ID="btnNew" class="btn btn-info btn-xs" OnClick="btnNew_Click1"><i class="fa fa-plus"></i>&nbsp; <%=GetGlobalResourceObject("pages","AddNewRecord") %>&nbsp;</asp:LinkButton>

                             

                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="row mbm">

                            <div class="col-lg-12">
                                <div class="tb-group-actions">

                                    <span><%=GetGlobalResourceObject("pages","serial") %>:</span>
                                    <asp:TextBox ID="txtFilterSerial" runat="server" class="table-group-action-select form-control input-inline"></asp:TextBox>

                                    <asp:LinkButton runat="server" ID="btnFilter" class="btn btn-success dropdown-toggle" OnClick="btnFilter_Click"><i class="fa fa-search"></i>&nbsp;
                                                <%=GetGlobalResourceObject("pages","Search") %></asp:LinkButton>
                                </div>
                            </div>
                        </div>


                        <div class="row">

                            <div class="col-lg-12">
                                <div class="portlet box portlet-blue2" id="divAddTransportation" runat="server">
                                    <div class="portlet-header">
                                        <div class="caption" style="float:left">
                                            <asp:Label runat="server" ID="Label4"><%=GetGlobalResourceObject("pages","AdvancedSearch") %></asp:Label>
                                        </div>
                                        <div class="tools" style="float: left"><i class="fa fa-chevron-down"></i></div>
                                    </div>
                                    <div class="portlet-body" style='display: none'>
                                        <div role="form" class="form-horizontal">
                                            <div class="row">

                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","RequestDate") %></label>

                                                        <div class="col-md-9">

                                                            <div class="input-group datetimepicker-disable-time date">
                                                                <asp:TextBox runat="server" ID="txtTransDate" placeholder="__/__/____" class="form-control"></asp:TextBox>
                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","InboundType") %></label>
                                                        <div class="col-md-9">
                                                            <asp:DropDownList ID="lstInboundType" runat="server" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","DepositType") %></label>
                                                        <div class="col-md-9">
                                                            <asp:DropDownList ID="lstDepositeType" runat="server" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    

                                                    <div class="form-group" style="display:none">
                                                        <label class="col-md-3 control-label" for="">Customs Department</label>
                                                        <div class="col-md-9">
                                                            <asp:DropDownList ID="lstcustomsDepartment" runat="server" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-4">

                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","To") %></label>

                                                        <div class="col-md-9">

                                                            <div class="input-group datetimepicker-disable-time date">
                                                                <asp:TextBox runat="server" ID="txtTransactionDateTo" placeholder="__/__/____" class="form-control"></asp:TextBox>
                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","ReferenceType") %></label>
                                                        <div class="col-md-9">
                                                            <asp:DropDownList ID="lstReferanceType" runat="server" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","ReferenceNo") %></label>

                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="txtRefNo" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                     

                                                    <div class="form-group" style="display:none">
                                                        <label class="col-md-3 control-label" for="">Manifest No</label>

                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="txtManifestNo" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                     

                                                </div>
                                                <div class="col-md-4" style="display:none">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label" for="">Delivery Order</label>

                                                        <div class="col-md-9">
                                                            <asp:TextBox runat="server" ID="txtDeliveryOrder" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                     

                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label" for="">Deposite Declaration Type</label>
                                                        <div class="col-md-9">
                                                            <asp:DropDownList ID="lstDepositeDeclarationType" runat="server" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                     
                                                     

                                                </div>






                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>


                                <%--<div style="float:left"><a class="btn btn-green" href="/Modules/WHM/reports/InboundListReport.aspx"><i class="fa fa-print"></i></a></div>--%>
                        <asp:DataGrid runat="server" ID="grdData" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" class="table table-hover table-striped table-bordered table-advanced tablesorter" OnDeleteCommand="grdData_DeleteCommand" OnItemDataBound="grdData_ItemDataBound" OnEditCommand="grdData_EditCommand">
                            <PagerStyle Visible="False" />
                            <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                            <Columns>
                                <asp:BoundColumn DataField="Code" Visible="False"></asp:BoundColumn>
                                 

                                   <asp:TemplateColumn HeaderText="<%$ Resources:pages ,Serial %>">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                         <%#GetPurchaseSerialText(ZeroIntergerIFNull(gets(Eval("Serial"))),NullDateFromDB( Eval("TransDate"))) %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="TransDate" HeaderText="<%$ Resources:pages ,RequestDate %>" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>

                               <%-- <asp:BoundColumn DataField="TypeTitleEn" HeaderText="<%$ Resources:pages ,InboundType %>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DepositeTypeTitleEn" HeaderText="<%$ Resources:pages ,DepositType %>"></asp:BoundColumn>--%>
                                <%--<asp:BoundColumn DataField="CustomsDepartmnetEn" HeaderText="Customs Departmnet"></asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="supplierFullNameAr" HeaderText="<%$ Resources:pages ,Vendor %>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RefTitleEn" HeaderText="<%$ Resources:pages ,ReferenceType %>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RefNo" HeaderText="<%$ Resources:pages ,ReferenceNo %>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="RefDate" HeaderText="<%$ Resources:pages ,ReferenceDate %>" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                             <%--   <asp:BoundColumn DataField="ManifestNo" HeaderText="Man. No"></asp:BoundColumn>--%>

                                <%--<asp:BoundColumn DataField="DeliveryOrderNo" HeaderText="Del. No"></asp:BoundColumn>--%>
                                <%--<asp:TemplateColumn HeaderText="<%$ Resources:pages ,Status %>">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <a href="frm_InboundStatusTrack.aspx?id=<%#Eval("code") %>" class="iframe"><%# GetGlobalResourceObject("pages","Status") %></a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>

                                <asp:TemplateColumn HeaderText="<%$ Resources:pages ,Print %>">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <a href="/Modules/WHM/Reports/PurchaseReceiptReport.aspx?id=<%#Eval("code") %>" class="iframe btn btn-default btn-xs"><i class="fa fa-print"></i>&nbsp; <%# GetGlobalResourceObject("pages","Print") %></a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

<asp:TemplateColumn HeaderText="<%$ Resources:pages ,ReceiveList %>">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <a href="<%# GetGlobalResourceObject("Utilities","cutureRoute") %>/Modules/WHM/Forms/Inboud/InboundList.aspx?pid=<%#Eval("code") %>" class="btn btn-default btn-xs"><i class="fa fa-file"></i>&nbsp; <%# GetGlobalResourceObject("pages","ReceiveList") %></a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>


                                <asp:TemplateColumn HeaderText="<%$ Resources:pages ,Details %>">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <a href="PurchaseOrderOperations.aspx?id=<%#Eval("code") %>" class="btn btn-default btn-xs"><i class="fa fa-edit"></i>&nbsp; <%# GetGlobalResourceObject("pages","Details") %></a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Edit" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>

                                        <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" class="btn btn-default btn-xs">
                                                 <i class="fa fa-edit"></i>&nbsp;
                                                Edit
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <input id="chkAllItems" class="checkall" style="border-style: none;" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkItem', this.checked)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkItem" CssClass="check" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        <div class="row mbm">
                            <div class="col-lg-12">
                                <div class="pagination-panel">

                                    <cc1:Pager CurrentIndex="1" OnCommand="pager_Command" ShowFirstLast="true" Visible="false"  ID="pager1"
                                        runat="server" Width="100%" PageSize="20"></cc1:Pager>
                                    &nbsp;
                                            records |<asp:Label ID="lblcount" runat="server"></asp:Label>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



    </div>


    <!--END CONTENT-->
    <!--BEGIN FOOTER-->

</asp:Content>

