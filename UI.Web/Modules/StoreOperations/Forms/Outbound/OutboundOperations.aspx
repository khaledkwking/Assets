<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="OutboundOperations.aspx.cs" Inherits="UI.Web.Modules.StoreOperations.Forms.Outbound.OutboundOperations" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="JavaScript" type="text/javascript">
        function chkImage() {



            var txt = document.getElementById("<%=txtSerial.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please, Enter Serial");
                txt.focus();
                return false;
            }


            var txt = document.getElementById("<%=txtTransDate.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please, Enter TransDate");
                txt.focus();
                return false;
            }
            var txt = document.getElementById("<%=lstOutboundTypeCode.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please, select Outbound Type");
                txt.focus();
                return false;
            }

            var txt = document.getElementById("<%=lstOwnerLocationCode.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("Please, Select store  ");
                txt.focus();
                return false;
            }



            return true;
        }


        function validateAddingItem() {

            var txt = document.getElementById("<%=hdnMasterID.ClientID %>")
            if (txt.value == "") {
                Swal.fire("فضلا ، احفظ بيانات الطلب ");
                txt.focus();
                return false;
            }
            var txt = document.getElementById("<%=lstPurchaseItems.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("Please, Select Item ");
                txt.focus();
                return false;
            }

            var txt = document.getElementById("<%=txtQty.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("Please, Enter Item Qty");
                txt.focus();
                return false;
            }
            var txtbalance = document.getElementById("<%=txtBalance.ClientID %>")
            if (txtbalance.value == "" || txtbalance.value == "0") {
                Swal.fire("Sorry ,No balance available");
                txtbalance.focus();
                return false;
            }

            if (parseFloat(txtbalance.value) < parseFloat(txt.value)) {
                Swal.fire("Sorry ,No balance available");
                txt.focus();
                return false;
            }


            var txt = document.getElementById("<%=lstStatusCode.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("Please, Select item status");
                txt.focus();
                return false;
            }

            return true;
        }

        

        function setActiveTab(tab) {
            var txt = document.getElementById("<%=hdnActiveTab.ClientID %>")
            txt.value = tab;

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



                                    <div class="col-md-9">


                                        <input id="hdnMasterID" runat="server" type="hidden" />
                                        <input id="hdnRequestType" runat="server" type="hidden" />
                                        <input id="hdnActiveTab" runat="server" type="hidden" />


                                        <div id="tblShow">
                                            <ul class="nav nav-tabs">
                                                <li class="nav-item" onclick="setActiveTab('1')"><a class="nav-link <%=getActiveTab("1") %>" data-toggle="tab" href="#BasicInfo"><em class="icon ni ni-server"></em><span>بيانات الطلب     </span></a></li>
                                                <li class="nav-item" onclick="setActiveTab('2')">
                                                    <a class="nav-link <%=getActiveTab("2") %>" data-toggle="tab" href="#ItemList"><em class="icon ni ni-menu-circled"></em><span>قائمة المواد (<asp:Label ID="lblItemCount" runat="server" ClientIDMode="Static">0</asp:Label>)</span></a>
                                                </li>
                                            </ul>
                                            <div class="tab-content">

                                                <div class="tab-pane <%=getActiveTab("1") %>" id="BasicInfo">
                                                    <asp:Label runat="server" ID="lblerror"></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hdnSelectedNode" ClientIDMode="Static" />
                                                    <asp:HiddenField runat="server" ID="hdnSelectedEditNode" ClientIDMode="Static" />


                                                    <div class="row">
                                                        <div class="col-md-4">

                                                            <div class="form-group">
                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Serial") %></label>
                                                                <asp:TextBox runat="server" ID="txtSerial" placeholder="OUT\S.N\CMGSYY" class="form-control"></asp:TextBox>
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
                                                            <div class="form-group">
                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","outboundTypeCode") %>  </label>

                                                                <asp:DropDownList ID="lstOutboundTypeCode" runat="server" class="form-control">
                                                                </asp:DropDownList>

                                                            </div>



                                                        </div>

                                                        <div class="col-md-4">
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

                                                            <div class="form-group" id="divOwnerLocation">
                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","OutOwnerLocation") %>  </label>

                                                                <asp:DropDownList ID="lstOwnerLocationCode" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>

                                                            </div>

                                                            <div class="form-group" id="divToStore" runat="server" visible="false">
                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","ToStore") %>  </label>

                                                                <asp:DropDownList ID="lstToStore" runat="server" class="form-control form-select" data-search="on"></asp:DropDownList>

                                                            </div>




                                                        </div>



                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Notes") %></label>
                                                                <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12 ">
                                                            <div class="pull-left" style="padding-top: 10px;">
                                                                <asp:LinkButton ID="btnSave" runat="server" class="btn btn-primary" OnClientClick="return chkImage();" OnClick="btnSave_Click"><i class='icon ni ni-save'></i>&nbsp; &nbsp;<%=GetGlobalResourceObject("pages","Submit") %> </asp:LinkButton>
                                                                &nbsp;
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="tab-pane <%=getActiveTab("2") %>" id="ItemList">
                                                    <%-- <table id="itemList-datatable" class="table table-hover table-striped table-bordered table-advanced tablesorter"></table>--%>
                                                    <%--   <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>--%>

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

                                                                            <div class="col-md-6">
                                                                                <div class="row">
                                                                                    <div class="col-md-4">
                                                                                        <div class="form-group">
                                                                                            <div class="col-md-12">
                                                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Qty") %></label>
                                                                                                <asp:TextBox runat="server" ID="txtQty" Text="1" class="form-control"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <div class="form-group">
                                                                                            <div class="col-md-12">
                                                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Balance") %></label>
                                                                                                <asp:TextBox runat="server" ReadOnly="true" Enabled="false" ID="txtBalance" Text="0" class="form-control"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <div class="form-group">
                                                                                            <div class="col-md-12">
                                                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","EstimatedCost") %></label>
                                                                                                <asp:TextBox runat="server" ReadOnly="true" Enabled="false" ID="txtUnitCost" Text="0" class="form-control"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <div class="form-group">
                                                                                            <div class="col-md-12">
                                                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","QUnit") %> </label>
                                                                                                <asp:DropDownList ID="lstQtyUnitCode" Enabled="false" runat="server" class="form-control"></asp:DropDownList>
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <div class="form-group">
                                                                                            <div class="col-md-12">
                                                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","StatusCode") %> </label>
                                                                                                <asp:DropDownList ID="lstStatusCode" runat="server" class="form-control"></asp:DropDownList>
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>


                                                                                </div>

                                                                            </div>


                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="form-actions" style="margin-bottom: 10px;">
                                                                                    <div class="col-md-12">
                                                                                        <asp:LinkButton ID="lnkSaveItems" runat="server" class="btn btn-primary" OnClick="lnkSaveItems_Click">  </asp:LinkButton>

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
                                                                    <asp:DataGrid runat="server" ID="grdOutboundItems" AutoGenerateColumns="False" OnEditCommand="grdOutboundItems_EditCommand" OnDeleteCommand="grdOutboundItems_DeleteCommand"
                                                                        AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter">
                                                                        <PagerStyle Visible="False" />
                                                                       
                                                                        <Columns>
                                                                            <asp:BoundColumn DataField="Code" Visible="False"></asp:BoundColumn>
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



                                                                            <asp:BoundColumn DataField="ItemRefCode" HeaderText="<%$ Resources:pages,ItemRefCode %>"></asp:BoundColumn>
                                                                            <%--<asp:BoundColumn DataField="ItemFinanceCode" HeaderText="<%$ Resources:pages,ItemFinanceCode %>"></asp:BoundColumn>--%>
                                                                            <asp:BoundColumn DataField="ItemNameAr" HeaderText="<%$ Resources:pages,PurchaseItems %>  "></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="Qty" HeaderText="<%$ Resources:pages,Qty %>"></asp:BoundColumn>
                                                                            
                                                                            <asp:BoundColumn DataField="unitCodeTitleAr" HeaderText="<%$ Resources:pages,QUnit %>"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="StatusNameAr" HeaderText="<%$ Resources:pages,StatusCode %>"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="EstimatedAmount" HeaderText="<%$ Resources:pages,EstimatedCost %>"></asp:BoundColumn>


                                                                            <asp:TemplateColumn>
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle Width="2%" />
                                                                                <ItemTemplate>
                                                                                    <div class="drodown">
                                                                                        <a href="#" class="btn btn-sm btn-icon btn-trigger dropdown-toggle" data-toggle="dropdown"><em class="icon ni ni-more-h"></em></a>
                                                                                        <div class="dropdown-menu dropdown-menu-right">
                                                                                            <ul class="link-list-opt no-bdr">
                                                                                              <%--  <li>
                                                                                                    <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" class="btn"><em class="icon ni ni-cards-fill"></em><span> <%=GetGlobalResourceObject("pages","EditR") %></span> </asp:LinkButton>

                                                                                                </li>--%>
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
                                                                    <%-- <div class="row mbm">
                                                                        <div class="col-lg-12">
                                                                            <div class="pagination-panel">
                                                                                &nbsp;
                                                                                              <asp:Label ID="lblInboundItemsCount" runat="server"></asp:Label>

                                                                            </div>
                                                                        </div>
                                                                    </div>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <%--   </ContentTemplate>
                                                </asp:UpdatePanel>--%>
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
