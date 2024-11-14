<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="InboundOperrations.aspx.cs" Inherits="UI.Web.Modules.StoreOperations.Forms.Inboud.Inboud.InboundOperrations" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="hdnMasterID" runat="server" type="hidden" />
    <input id="hdnActiveTab" runat="server" type="hidden" />

    <script language="JavaScript" type="text/javascript">
        function chkImage() {

            var txt = document.getElementById("<%=txtSerial.ClientID %>")
            if (txt.value == "") {
                Swal.fire("يرجى إدخال رقم المسلسل");
                return false;
            }

            var txt = document.getElementById("<%=txtTransDate.ClientID %>")
            if (txt.value == "") {
                Swal.fire("يرجى إدخال تاريخ الطلب");
                return false;
            }

            var txt = document.getElementById("<%=lstFromVendorCode.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("يرجى إختيار الـمورد");
                return false;
            }


            var txt = document.getElementById("<%=lstTargetLocationCode.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("يرجى إختيار الـمخزن");
                return false;
            }



            var txt = document.getElementById("<%=txtRefNo.ClientID %>")
            if (txt.value == "") {
                Swal.fire("يرجى إدخال رقم طلب التسليم");
                return false;
            }

            var txt = document.getElementById("<%=txtRefDate.ClientID %>")
            if (txt.value == "") {
                Swal.fire("يرجى إدخال تاريخ الـمرجـع");
                return false;
            }

            return true;
        }
        function ValidateInboundITems() {
            var txt = document.getElementById("<%=hdnMasterID.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("يرجى حفظ البيانات الأساسية الخاصة بقائمة المواد");
                return false;
            }

            var txt = document.getElementById("<%=lstPurchaseItems.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("يرجى إختيار وصف المـادة");
                return false;
            }
            var txt = document.getElementById("<%=txtQty.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("يرجى إدخال كمية المـادة");
                return false;
            }

            var txt = document.getElementById("<%=txtUnitCost.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("يرجى إدخال سعر الوحدة");
                return false;
            }
            return true;
        }
        function ValidateInboundNotes() {
            var txt = document.getElementById("<%=hdnMasterID.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("يرجى حفظ البيانات الأساسية الخاصة بقائمة المواد");
                return false;
            }


            return true;
        }
        function setActiveTab(tab) {
            var txt = document.getElementById("<%=hdnActiveTab.ClientID %>")
            txt.value = tab;

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

            <!-- .nk-block-head-content -->
            <div class="nk-block-head-content" style="display:none">
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
                                                <asp:LinkButton OnClientClick="return checkDelete();" runat="server" ID="LinkButton1" OnClick="btnDelete_Click"><i class="icon ni ni-trash"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %></asp:LinkButton></li>

                                        </ul>
                                    </div>
                                </div>
                            </li>
                            <%--<li>
                                <asp:LinkButton runat="server" ID="btnNew" class="btn btn-icon btn-primary" OnClick="btnNew_Click"><em class="icon ni ni-plus"></em></asp:LinkButton></li>--%>
                        </ul>
                    </div>
                </div>
                <!-- .toggle-wrap -->
            </div>
            <!-- .nk-block-head-content -->
        </div>
        <!-- .nk-block-between -->
    </div>




    <asp:UpdatePanel runat="server" ID="Updatepanel1" ChildrenAsTriggers="true" UpdateMode="conditional">
    </asp:UpdatePanel>


    <div class="nk-block">




        <div class="card card-bordered">
            <div class="card card-stretch">
                <%--   <div class="card-title">
                    <h5 class="title"><%=GetGlobalResourceObject("pages","RequestDetail") %></h5>
                </div>--%>

                <div class="card-inner">
                    <div class="card-inner p-0">
                        <div class="portlet box">

                            <div class="portlet-body">

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="portlet box">

                                            <div class="portlet-body" style="min-height: 600px">
                                                <div class="col-lg-12">

                                                    <ul class="nav nav-tabs">
                                                        <li class="nav-item" onclick="setActiveTab('1')"><a class="nav-link <%=getActiveTab("1") %>" data-toggle="tab" href="#MasterData"><em class="icon ni ni-list-thumb-alt"></em><span><%=GetGlobalResourceObject("pages","RequestData") %> </span></a></li>
                                                        <li class="nav-item" onclick="setActiveTab('2')"><a class="nav-link <%=getActiveTab("2") %> " data-toggle="tab" href="#Items"><em class="icon ni ni-list-thumb-alt"></em><span><%=GetGlobalResourceObject("pages","ItemsList") %> </span>(<asp:Label ID="lblItemCount" runat="server" ClientIDMode="Static">0</asp:Label>)</a></li>
                                                        <li class="nav-item" onclick="setActiveTab('3')"><a class="nav-link <%=getActiveTab("3") %>" data-toggle="tab" href="#StatusTracking"><em class="icon ni ni-tranx"></em><span><%=GetGlobalResourceObject("pages","StatusTracking") %> </span></a></li>
                                                        <li class="nav-item" onclick="setActiveTab('4')"><a class="nav-link <%=getActiveTab("4") %>" data-toggle="tab" href="#Attachments"><em class="icon ni ni-folders"></em><span><%=GetGlobalResourceObject("pages","Attachments") %> </span></a></li>
                                                        <li class="nav-item" onclick="setActiveTab('5')"><a class="nav-link <%=getActiveTab("5") %>" data-toggle="tab" href="#Notes"><em class="icon ni ni-list-round"></em><span><%=GetGlobalResourceObject("pages","Notes") %> </span></a></li>
                                                    </ul>
                                                    <div class="tab-content">
                                                        <div class="tab-pane fade in <%=getActiveTab("1") %>" id="MasterData">
                                                            <asp:Label runat="server" ID="lblerror"></asp:Label>
                                                            <div class="col-lg-12">
                                                                <div class="portlet box portlet-blue">

                                                                    <div class="portlet-body" style='<%=setmaterstyle()%>'>
                                                                        <div role="form">

                                                                            <div class="row">
                                                                                <div class="col-md-4">
                                                                                    <div class="form-group" runat="server" id="divVendor" visible="false">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","FromVendorCode") %>  </label>

                                                                                        <asp:DropDownList ID="lstFromVendorCode" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>

                                                                                    </div>
                                                                                    <div class="form-group" style="display: none">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","InboundTypeCode") %>  </label>

                                                                                        <asp:DropDownList ID="lstInboundTypeCode" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="lstInboundTypeCode_SelectedIndexChanged">
                                                                                        </asp:DropDownList>

                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Serial") %></label>
                                                                                        <asp:TextBox runat="server" ID="txtSerial" placeholder="IN\S.N\CMGSYY" class="form-control"></asp:TextBox>
                                                                                    </div>

                                                                                    <div class="form-group">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","TransDate") %></label>

                                                                                        <div class="form-control-wrap">
                                                                                            <div class="form-icon form-icon-right">
                                                                                                <em class="icon ni ni-calendar-alt"></em>
                                                                                            </div>
                                                                                            <asp:TextBox runat="server" ID="txtTransDate" placeholder="__/__/____" class="form-control date-picker"></asp:TextBox>
                                                                                        </div>


                                                                                    </div>




                                                                                </div>

                                                                                <div class="col-md-4">
                                                                                    <div class="form-group" runat="server" id="divOwnerLocation" visible="false">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","OwnerLocationCode") %>  </label>

                                                                                        <asp:DropDownList ID="lstOwnerLocationCode" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>

                                                                                    </div>

                                                                                    <div class="form-group">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","TargetLocationCode") %>  </label>

                                                                                        <asp:DropDownList ID="lstTargetLocationCode" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>

                                                                                    </div>




                                                                                    <div class="form-group">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","RefNo") %> </label>
                                                                                        <asp:TextBox runat="server" ID="txtRefNo" class="form-control"></asp:TextBox>

                                                                                    </div>

                                                                                    <div class="form-group">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","RefDate") %> </label>

                                                                                        <div class="form-control-wrap">
                                                                                            <div class="form-icon form-icon-right">
                                                                                                <em class="icon ni ni-calendar-alt"></em>
                                                                                            </div>
                                                                                            <asp:TextBox runat="server" ID="txtRefDate" placeholder="__/__/____" class="form-control date-picker"></asp:TextBox>
                                                                                        </div>


                                                                                    </div>



                                                                                </div>
                                                                                <div class="col-md-4">
                                                                                    <div class="form-group">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","DeliveryOrderNo") %></label>


                                                                                        <asp:TextBox runat="server" ID="txtDeliveryOrderNo" class="form-control"></asp:TextBox>

                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","DeliveryDate") %> </label>
                                                                                        <div class="form-control-wrap">
                                                                                            <div class="form-icon form-icon-right">
                                                                                                <em class="icon ni ni-calendar-alt"></em>
                                                                                            </div>
                                                                                            <asp:TextBox runat="server" ID="txtDeliveryDate" placeholder="__/__/____" class="form-control date-picker"></asp:TextBox>
                                                                                        </div>





                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","DepositeNotes") %></label>
                                                                                        <asp:TextBox runat="server" ID="txtDepositeNotes" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <div class="form-group">
                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Notes") %></label>
                                                                                        <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>


                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div class="pull-left" style="margin-top: 10px;">
                                                                <asp:LinkButton ID="btnSave" runat="server" class="btn btn-primary" OnClientClick="return chkImage();" OnClick="btnSave_Click"><i class='icon ni ni-save'></i>&nbsp; &nbsp;<%=GetGlobalResourceObject("pages","Submit") %> </asp:LinkButton>
                                                                &nbsp;
                                                                 <asp:LinkButton ID="btnSave2" runat="server" class="btn btn-secondary" OnClick="btnSave2_Click" Visible="false"><i class='icon ni ni-save'></i>&nbsp;  <%=GetGlobalResourceObject("pages","SubmitAddItems") %> </asp:LinkButton>
                                                                &nbsp;
				                                                         <asp:Button runat="server" ID="btnCancel" class="btn btn-default" Text=" <%$ Resources: Pages, Cancel %> " OnClick="btnCancel_Click" />
                                                            </div>

                                                        </div>

                                                        <div class="tab-pane fade <%=getActiveTab("2") %>" id="Items">

                                                           
                                                                    <div class="row">

                                                                        <div class="col-lg-12">
                                                                            <div class="portlet box portlet-blue" id="divinboundItemsAdd" runat="server">

                                                                                <div class="portlet-body">
                                                                                    <div role="form" class="form-body pal">
                                                                                        <div class="row">
                                                                                            <div class="col-md-4">
                                                                                                <div class="form-group">
                                                                                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","PurchaseItems") %></label>

                                                                                                    <asp:DropDownList ID="lstPurchaseItems" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstPurchaseItems_SelectedIndexChanged" class="form-control form-select" data-search="on"></asp:DropDownList>

                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2" style="display:none">
                                                                                                <div class="form-group">
                                                                                                    <div class="col-md-12">
                                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","TagId") %></label>
                                                                                                        <asp:TextBox runat="server" ID="txtTagId" Text="0" class="form-control"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2"  >
                                                                                                <div class="form-group">
                                                                                                    <div class="col-md-12">
                                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Qty") %></label>
                                                                                                        <asp:TextBox runat="server" ID="txtQty" Text="1" class="form-control"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <div class="form-group">
                                                                                                    <div class="col-md-12">
                                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","EstimatedCost") %></label>
                                                                                                        <asp:TextBox runat="server" ID="txtUnitCost" Text="0" class="form-control"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <div class="form-group">
                                                                                                    <div class="col-md-12">
                                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Status") %> </label>
                                                                                                        <asp:DropDownList ID="lstStatusCode" runat="server" class="form-control"></asp:DropDownList>
                                                                                                    </div>

                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <div class="form-group">
                                                                                                    <div class="col-md-12">
                                                                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","QUnit") %> </label>
                                                                                                        <asp:DropDownList ID="lstQtyUnitCode" Enabled="false" runat="server" class="form-control"></asp:DropDownList>
                                                                                                    </div>

                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <div class="form-group">
                                                                                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","ExpiryDate") %></label>

                                                                                                    <div class="form-control-wrap">
                                                                                                        <div class="form-icon form-icon-right">
                                                                                                            <em class="icon ni ni-calendar-alt"></em>
                                                                                                        </div>
                                                                                                        <asp:TextBox runat="server" ID="txtexpireyDate" placeholder="__/__/____" class="form-control date-picker"></asp:TextBox>
                                                                                                    </div>


                                                                                                </div>
                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="row">

                                                                                            <div class="col-md-12">
                                                                                                <div class="form-group">
                                                                                                    <label class="control-label" f><%=GetGlobalResourceObject("pages","notes") %> </label>
                                                                                                    <asp:TextBox runat="server" ID="txtGoodNotes" TextMode="MultiLine" class="form-control"></asp:TextBox>

                                                                                                </div>
                                                                                            </div>
                                                                                        </div>


                                                                                        <div class="row">
                                                                                            <div class="col-md-12">
                                                                                                <div class="form-actions">
                                                                                                    <div class="col-md-12">
                                                                                                        <asp:LinkButton ID="lnkSaveItems" runat="server" class="btn btn-primary" OnClick="lnkSaveItems_Click"><i class='icon ni ni-save'></i>&nbsp; <%= GetGlobalResourceObject("pages","AddItem") %> </asp:LinkButton>

                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>

                                                                    <div class="row" id="DivinboundItemsShow" runat="server">
                                                                        <div class="col-lg-12">
                                                                            <div class="portlet box">

                                                                                <div class="portlet-body">
                                                                                    <asp:DataGrid runat="server" ID="grdInboundItems" AutoGenerateColumns="False" OnEditCommand="grdInboundItems_EditCommand" OnDeleteCommand="grdInboundItems_DeleteCommand"
                                                                                        AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter">
                                                                                        <PagerStyle Visible="False" />
                                                                                        <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                                                                                        <Columns>
                                                                                            <asp:BoundColumn DataField="InboubdItemId" Visible="False"></asp:BoundColumn>
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

                                                                                          <%--  <asp:TemplateColumn HeaderText="<%$ Resources:pages,image %>">
                                                                                                <ItemStyle Width="5%" />
                                                                                                <ItemTemplate>

                                                                                                    <%# FillImage(gets(Eval("ItemImage")), Resources.Utilities.resourcespath+"uploads/ItemsData/", 35, 25,"")%>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>--%>
                                                                                            <asp:BoundColumn DataField="TitleAr" HeaderText="<%$ Resources:pages,Category %>"></asp:BoundColumn>

                                                                                            <asp:BoundColumn DataField="ItemRefCode" HeaderText="<%$ Resources:pages,ItemRefCode %>"></asp:BoundColumn>
                                                                                            <%--<asp:BoundColumn DataField="ItemTag" HeaderText="<%$ Resources:pages,Tagid %>"></asp:BoundColumn>--%>
                                                                                            <asp:BoundColumn DataField="ItemFinanceCode" HeaderText="<%$ Resources:pages,ItemFinanceCode %>"></asp:BoundColumn>
                                                                                            <%--<asp:BoundColumn DataField="ItemNameAr" HeaderText="<%$ Resources:pages,ItemNameAr %>"></asp:BoundColumn>--%>
                                                                                            <asp:BoundColumn DataField="ItemNameAr" HeaderText="<%$ Resources:pages,PurchaseItems %>  "></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="Qty" HeaderText="<%$ Resources:pages,Qty %>"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="EstimatedUnitCost" HeaderText="<%$ Resources:pages,EstimatedCost %>"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="ReceivedQty" HeaderText="<%$ Resources:pages,ReceivedQty %>"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="UnitNameAr" HeaderText="<%$ Resources:pages,QUnit %>"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="StatusTitleAr" HeaderText="<%$ Resources:pages,status %>"></asp:BoundColumn>

                                                                                            <asp:TemplateColumn>
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemStyle Width="2%" />
                                                                                                <ItemTemplate>
                                                                                                    <div class="drodown">
                                                                                                        <a href="#" class="btn btn-sm btn-icon btn-trigger dropdown-toggle" data-toggle="dropdown"><em class="icon ni ni-more-h"></em></a>
                                                                                                        <div class="dropdown-menu dropdown-menu-right">
                                                                                                            <ul class="link-list-opt no-bdr">
                                                                                                                <li>
                                                                                                                    <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" class="btn"><em class="icon ni ni-cards-fill"></em><span> <%=GetGlobalResourceObject("pages","EditR") %></span> </asp:LinkButton>

                                                                                                                </li>
                                                                                                                <li>
                                                                                                                    <asp:LinkButton runat="server" ID="lnkDelete" CommandName="Delete" class="btn text-danger"><em class="icon ni ni-delete"></em><span> <%=GetGlobalResourceObject("pages","Delete") %></span> </asp:LinkButton>

                                                                                                                </li>

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
                                                                                              <asp:Label ID="lblInboundItemsCount" runat="server"></asp:Label>

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                              
                                                        </div>

                                                        <div class="tab-pane fade <%=getActiveTab("3") %>" id="Attachments">
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="true" UpdateMode="conditional">
                                                                <ContentTemplate>

                                                                    <div class="row">

                                                                        <div class="col-lg-12">
                                                                            <div class="portlet box portlet-blue" id="divAttachmentsAdd" runat="server" visible="false">
                                                                                <div class="portlet-header">
                                                                                    <div class="caption">
                                                                                        <asp:Label runat="server" ID="lblAttachmentTitle">Add New Record</asp:Label>
                                                                                    </div>

                                                                                </div>
                                                                                <div class="portlet-body">
                                                                                    <div role="form" class="form-horizontal">
                                                                                        <div class="row">

                                                                                            <div class="col-md-6">


                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","AttachmentType") %> </label>

                                                                                                    <div class="col-md-9">
                                                                                                        <asp:DropDownList ID="lstAttachmentType" runat="server" class="form-control"></asp:DropDownList>

                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","File") %> </label>

                                                                                                    <div class="col-md-9">
                                                                                                        <asp:FileUpload ID="txtFile" runat="server" />

                                                                                                        <%--<asp:AsyncFileUpload ID="txtimages" runat="server" OnUploadedComplete="txtimages_UploadedComplete" OnUploadedFileError="txtimages_UploadedFileError" />--%>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","Notes") %> </label>

                                                                                                    <div class="col-md-9">

                                                                                                        <asp:TextBox ID="txtAttachmentNotes" runat="server" class="form-control"></asp:TextBox>

                                                                                                    </div>
                                                                                                </div>


                                                                                            </div>

                                                                                            <div class="col-md-12">
                                                                                                <div class="form-actions">
                                                                                                    <div class="col-md-offset-3 col-md-9">
                                                                                                        <asp:LinkButton ID="lnkSaveAttachment" runat="server" class="btn btn-primary" OnClick="lnkSaveAttachment_Click"><i class='icon ni ni-save'></i>&nbsp;<%=GetGlobalResourceObject("pages","Submit") %> </asp:LinkButton>

                                                                                                        &nbsp;
				                                                   <asp:Button runat="server" ID="lnkCancelAttachement" class="btn btn-default" Test="<%$ Resources: Pages, Cancel %>" OnClick="lnkCancelAttachement_Click" />

                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>

                                                                    <div class="row" id="DivAttachementShow" runat="server">
                                                                        <div class="col-lg-12">
                                                                            <div class="portlet box">



                                                                                <div class="nk-block-head nk-block-head-sm">
                                                                                    <div class="nk-block-between">
                                                                                        <div class="nk-block-head-content">
                                                                                        </div>
                                                                                        <div class="nk-block-head-content">
                                                                                            <div class="toggle-wrap nk-block-tools-toggle">
                                                                                                <a href="#" class="btn  btn-icon btn-trigger toggle-expand mr-n1" data-target="pageMenu"><em class="icon ni ni-menu-alt-r"></em></a>
                                                                                                <div class="toggle-expand-content" data-content="pageMenu">
                                                                                                    <ul class="nk-block-tools g-3">
                                                                                                        <li class="nk-block-tools-opt">
                                                                                                            <div class="drodown">
                                                                                                                <a href="#" class="dropdown-toggle btn btn-info btn-outline-light text-white" data-toggle="dropdown"><em class="icon ni ni-setting"></em></a>
                                                                                                                <div class="dropdown-menu dropdown-menu-right">
                                                                                                                    <ul class="link-list-opt no-bdr">

                                                                                                                        <li>
                                                                                                                            <asp:LinkButton OnClientClick="return checkAttachementDelete();" runat="server" ID="lnkDeleteAttachment" OnClick="lnkDeleteAttachment_Click"><i class="icon ni ni-trash"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %></asp:LinkButton></li>
                                                                                                                        <li>
                                                                                                                            <asp:LinkButton runat="server" ID="lnkAttachmentAdd" OnClick="lnkAttachmentAdd_Click"><i class="icon ni ni-plus-sm"></i>&nbsp; <%=GetGlobalResourceObject("pages","AddNewRecord") %>&nbsp;</asp:LinkButton></li>

                                                                                                                    </ul>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </li>

                                                                                                    </ul>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>



                                                                                <div class="portlet-body">



                                                                                    <asp:DataGrid runat="server" ID="grdAttachment" AutoGenerateColumns="False"
                                                                                        AllowPaging="True" PageSize="20" class="table table-hover table-striped table-bordered table-advanced tablesorter" OnEditCommand="grdAttachment_EditCommand">
                                                                                        <PagerStyle Visible="False" />
                                                                                        <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                                                                                        <Columns>
                                                                                            <asp:BoundColumn DataField="code" Visible="False"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="TransDate" HeaderText="<%$ Resources:pages,TransDate %>" DataFormatString="{0:dd/MM/yyyy}">
                                                                                                <ItemStyle Width="10%" />
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="Notes" HeaderText="<%$ Resources:pages,Notes %>"></asp:BoundColumn>
                                                                                            <asp:TemplateColumn HeaderText="<%$ Resources:pages,File %> ">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <a href="/layout/Uploads/Attachments/<%#Eval("InboundCode") %>/<%#Eval("FileName") %>" class="iframe btn btn-default btn-xs" target="_blank">
                                                                                                        <i class="fa fa-download"></i>&nbsp;
                                                                                                                   View
                                                                                                    </a>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>

                                                                                            <asp:TemplateColumn HeaderText="<%$ Resources:pages,EditR %>">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" class="btn"><em class="icon ni ni-cards-fill"></em><span> <%=GetGlobalResourceObject("pages","EditR") %></span> </asp:LinkButton>

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

                                                                                                <cc1:Pager CurrentIndex="1" ShowFirstLast="true" ID="AttachmentPager"
                                                                                                    runat="server" Width="100%" PageSize="20" OnCommand="AttachmentPager_Command"></cc1:Pager>
                                                                                                &nbsp;
                                                                                          <asp:Label ID="lblAttachmentCount" runat="server"></asp:Label>

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>

                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="lnkSaveAttachment" />
                                                                </Triggers>

                                                            </asp:UpdatePanel>
                                                        </div>

                                                        <div class="tab-pane fade <%=getActiveTab("4") %>" id="StatusTracking">
                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="true" UpdateMode="conditional">
                                                                <ContentTemplate>

                                                                    <div class="row">

                                                                        <div class="col-lg-12">
                                                                            <div class="portlet box portlet-blue" id="DivStatusTrackingAdd" runat="server" visible="false">
                                                                                <div class="portlet-header">
                                                                                    <div class="caption">
                                                                                        <asp:Label runat="server" ID="lblStatusTrackingTitle"></asp:Label>
                                                                                    </div>

                                                                                </div>
                                                                                <div class="portlet-body">
                                                                                    <div role="form" class="form-horizontal">
                                                                                        <div class="row">

                                                                                            <div class="col-md-6">


                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","Status") %> </label>

                                                                                                    <div class="col-md-9">
                                                                                                        <asp:DropDownList ID="lstDepositStatusTypeCode" runat="server" class="form-control"></asp:DropDownList>

                                                                                                    </div>
                                                                                                </div>


                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","Notes") %> </label>

                                                                                                    <div class="col-md-9">

                                                                                                        <asp:TextBox ID="txtStatsuNote" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>

                                                                                                    </div>
                                                                                                </div>


                                                                                            </div>

                                                                                            <div class="col-md-12">
                                                                                                <div class="form-actions">
                                                                                                    <div class="col-md-offset-3 col-md-9">
                                                                                                        <asp:LinkButton ID="lnkStatusTrackingSave" runat="server" class="btn btn-primary" OnClick="lnkStatusTrackingSave_Click"><i class='icon ni ni-save'></i> &nbsp;<%=GetGlobalResourceObject("pages","Submit") %> </asp:LinkButton>

                                                                                                        &nbsp;
				                                                                                   <asp:Button runat="server" ID="lnkStatusTrackingCancel" class="btn btn-default" Test="<%$ Resources: Pages, Cancel %>" OnClick="lnkStatusTrackingCancel_Click" />

                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>

                                                                    <div class="row" id="DivStatusTrackinghow" runat="server">
                                                                        <div class="col-lg-12">
                                                                            <div class="portlet box">
                                                                                <div class="portlet-header">

                                                                                    <div class="actions">

                                                                                        <asp:LinkButton runat="server" ID="lnkStatusTrackingAdd" class="btn btn-info btn-xs" OnClick="lnkStatusTrackingAdd_Click" Visible="false"><i class="fa fa-plus"></i>&nbsp; Add New Record&nbsp;</asp:LinkButton>

                                                                                        <asp:LinkButton OnClientClick="return checkStatusTrackingDelete();" runat="server" ID="lnkStatusTracking" class="btn btn-danger btn-xs" OnClick="lnkStatusTracking_Click" Visible="false"><i class="icon ni ni-trash"></i>&nbsp;Delete Selected Data</asp:LinkButton>

                                                                                    </div>
                                                                                </div>
                                                                                <div class="portlet-body">



                                                                                    <asp:DataGrid runat="server" ID="grdStatusTracking" AutoGenerateColumns="False"
                                                                                        AllowPaging="True" PageSize="20" class="table table-hover table-striped table-bordered table-advanced tablesorter" OnEditCommand="grdStatusTracking_EditCommand">
                                                                                        <PagerStyle Visible="False" />
                                                                                        <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                                                                                        <Columns>
                                                                                            <asp:BoundColumn DataField="code" Visible="False"></asp:BoundColumn>

                                                                                            <asp:TemplateColumn HeaderText="<%$ Resources:pages,DepositStatus %>">

                                                                                                <ItemTemplate>
                                                                                                    <%#Eval("D_InboundDepositeStatusType.Titlear") %>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>

                                                                                            <asp:BoundColumn DataField="TransDate" HeaderText="<%$ Resources:pages,TransDate %> " DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="Notes" HeaderText="<%$ Resources:pages,Notes %>"></asp:BoundColumn>


                                                                                            <asp:TemplateColumn HeaderText="<%$ Resources:pages,EditR %>" Visible="false">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" class="btn"><em class="icon ni ni-cards-fill"></em><span> <%=GetGlobalResourceObject("pages","EditR") %></span> </asp:LinkButton>

                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>

                                                                                            <asp:TemplateColumn Visible="false">
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

                                                                                                <cc1:Pager CurrentIndex="1" ShowFirstLast="true" ID="StatusTrackingPager"
                                                                                                    runat="server" Width="100%" PageSize="20" OnCommand="StatusTrackingPager_Command"></cc1:Pager>
                                                                                                &nbsp;
                                                            <asp:Label ID="lblStatusTrackingCount" runat="server"></asp:Label>

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>

                                                            </asp:UpdatePanel>
                                                        </div>

                                                        <div class="tab-pane fade <%=getActiveTab("5") %>" id="Notes">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>

                                                                    <div class="row">

                                                                        <div class="col-lg-12">
                                                                            <div class="portlet box portlet-blue" id="divInboundNotesAdd" runat="server" visible="false">
                                                                                <div class="portlet-header">
                                                                                    <div class="caption">
                                                                                        <asp:Label runat="server" ID="lblNotesTitle"> </asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="portlet-body">
                                                                                    <div role="form" class="form-horizontal">
                                                                                        <div class="row">

                                                                                            <div class="col-md-12">


                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-12 control-label" for=""><%=GetGlobalResourceObject("pages","InboundNotes") %> </label>

                                                                                                    <div class="col-md-12">
                                                                                                        <asp:TextBox runat="server" ID="txtInboundNotes" placeholder="Record Notes" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>



                                                                                            </div>

                                                                                            <div class="col-md-12">
                                                                                                <div class="form-actions">
                                                                                                    <div class="col-md-offset-3 col-md-9">
                                                                                                        <asp:LinkButton ID="lnkSaveNotes" runat="server" OnClientClick="return ValidateInboundNotes();" class="btn btn-primary" OnClick="lnkSaveNotes_Click"><i class='icon ni ni-save'></i>&nbsp;&nbsp;<%=GetGlobalResourceObject("pages","Submit") %> </asp:LinkButton>

                                                                                                        &nbsp;
				                                                                                          <asp:Button runat="server" ID="lnkCancelNotes" class="btn btn-default" Test="<%$ Resources: Pages, Cancel %>" OnClick="lnkCancelNotes_Click" />

                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>

                                                                    <div class="row" id="divNotesShow" runat="server">
                                                                        <div class="col-lg-12">
                                                                            <div class="portlet box">


                                                                                <div class="nk-block-head nk-block-head-sm">
                                                                                    <div class="nk-block-between">
                                                                                        <div class="nk-block-head-content">
                                                                                        </div>
                                                                                        <div class="nk-block-head-content">
                                                                                            <div class="toggle-wrap nk-block-tools-toggle">
                                                                                                <a href="#" class="btn  btn-icon btn-trigger toggle-expand mr-n1" data-target="pageMenu"><em class="icon ni ni-menu-alt-r"></em></a>
                                                                                                <div class="toggle-expand-content" data-content="pageMenu">
                                                                                                    <ul class="nk-block-tools g-3">
                                                                                                        <li class="nk-block-tools-opt">
                                                                                                            <div class="drodown">
                                                                                                                <a href="#" class="dropdown-toggle btn btn-info btn-outline-light text-white" data-toggle="dropdown"><em class="icon ni ni-setting"></em></a>
                                                                                                                <div class="dropdown-menu dropdown-menu-right">
                                                                                                                    <ul class="link-list-opt no-bdr">
                                                                                                                        <li>
                                                                                                                            <asp:LinkButton OnClientClick="return checkDelete();" runat="server" ID="btnDelete" OnClick="btnDelete_Click"><i class="icon ni ni-trash"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %></asp:LinkButton></li>
                                                                                                                        <li>
                                                                                                                            <asp:LinkButton runat="server" ID="lnkAddNotes" OnClick="lnkAddNotes_Click"><i class="icon ni ni-plus-sm"></i>&nbsp; <%=GetGlobalResourceObject("pages","AddNewRecord") %>&nbsp;</asp:LinkButton></li>

                                                                                                                    </ul>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </li>

                                                                                                    </ul>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                                <div class="portlet-body">



                                                                                    <asp:DataGrid runat="server" ID="grdInboundNotes" AutoGenerateColumns="False"
                                                                                        AllowPaging="True" PageSize="20" class="table table-hover table-striped table-bordered table-advanced tablesorter" OnEditCommand="grdInboundNotes_EditCommand">
                                                                                        <PagerStyle Visible="False" />
                                                                                        <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                                                                                        <Columns>
                                                                                            <asp:BoundColumn DataField="code" Visible="False"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="TransDate" HeaderText="<%$ Resources:pages,TransDate %>" DataFormatString="{0:dd/MM/yyyy}">
                                                                                                <ItemStyle Width="10%" />
                                                                                            </asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="Notes" HeaderText="<%$ Resources:pages,InboundNotes %>"></asp:BoundColumn>

                                                                                            <asp:TemplateColumn HeaderText="<%$ Resources:pages,EditR %>">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" class="btn"><em class="icon ni ni-cards-fill"></em><span> <%=GetGlobalResourceObject("pages","EditR") %></span> </asp:LinkButton>

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

                                                                                                <cc1:Pager CurrentIndex="1" ShowFirstLast="true" ID="NotePager"
                                                                                                    runat="server" Width="100%" PageSize="20" OnCommand="Pager2_Command"></cc1:Pager>
                                                                                                &nbsp;
                                            <asp:Label ID="lblNotesCound" runat="server"></asp:Label>

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>

                                                            </asp:UpdatePanel>
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
    </div>
</asp:Content>
