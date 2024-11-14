<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="AssetTransfer.aspx.cs" Inherits="UI.Web.Modules.Assets.AssetTransfer" %>

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

            var txt = document.getElementById("<%=txtFromDate.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please, Enter Action Date");
                return false;
            }


            var hdnSelectedLocation = document.getElementById("<%=selectedToLocation.ClientID %>")

            if (hdnSelectedLocation.value == "" || hdnSelectedLocation.value == "0") {
                Swal.fire("Please, select  Location ");
                return false;
            }

          <%--  var txt = document.getElementById("<%=hdnType.ClientID %>")
            if (txt.value == "1") {
                var emp = document.getElementById("<%=lstRefEmployee.ClientID %>")
                if (emp.value == "" || emp.value == "0") {
                    Swal.fire("Please, Select Employee   ");
                    return false;
                }
            }--%>

            var emp = document.getElementById("<%=lstToEmpRefCode.ClientID %>")
            if (emp.value == "" || emp.value == "0") {
                Swal.fire("Please, Select Employee   ");
                return false;
            }

       <%--     var txt = document.getElementById("<%=hdnItemCount.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("Please, select Items For action");
                return false;
            }--%>
            return true;
        }


        function setActiveTab(tab) {
            var txt = document.getElementById("<%=hdnActiveTab.ClientID %>")
            txt.value = tab;

        }
        function setSelectedtype(type) {

            var txt = document.getElementById("<%=hdnType.ClientID %>");
            txt.value = type;

            if (txt.value == "1") {
                $('.divEmployee').toggle(true);
            } else { $('.divEmployee').toggle(false); }
        }


        $(document).ready(function () {
            // $("#disReturnDate").toggle(false);
            $('#chkReturnDate').click(function () {
                $("#disReturnDate").toggle(this.checked);
                if (!this.checked) {
                    $("#txtReturnDate").val("");

                }
            });

            var hdnType = document.getElementById("<%=hdnType.ClientID %>")
            if (hdnType.value == "1") {
                $("#customRadio1").prop("checked", true);
                $("#customRadio2").prop("checked", false);
                $('.divEmployee').toggle(true);
            } else if (hdnType.value == "2") {
                $("#customRadio1").prop("checked", false);
                $("#customRadio2").prop("checked", true);
                $('.divEmployee').toggle(false);
            } else {
                $("#customRadio1").prop("checked", true);
                $("#customRadio2").prop("checked", false);
            }



            $(".iframe75callback").click(
                function (event) {
                    event.preventDefault();
                    var elementURL = $(this).attr("href");
                    $.colorbox({
                        iframe: true, href: elementURL, width: "75%", height: "95%"
                        , onCleanup: function () {
                         <%--   var btn = $('#<%= btnReload.ClientID %>');
                            btn.click();--%>
                        }

                    });
                });


           <%-- $('#txtOwnerLocationCode').on('change', function () {
                if (txtOwnerLocationCombo != null) {
                    var selectedLocation = txtOwnerLocationCombo.getSelectedIds();
                    if (selectedLocation != null) {
                        $(".selectedLocation").val(selectedLocation[0]);
                             var btn = $('#<%= btnReload.ClientID %>');
                            btn.click(); 

                    }

                }
            });--%>

        });


    </script>

    <input id="hdnType" runat="server" type="hidden" />
    <input id="hdnItemCount" runat="server" type="hidden" />

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
                                                <%--<asp:LinkButton OnClientClick="return checkDelete();" runat="server" ID="LinkButton1" OnClick="btnDelete_Click"><i class="icon ni ni-trash"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %></asp:LinkButton>--%>
                                                <asp:LinkButton ID="btnSave" runat="server" OnClientClick="return chkImage();" OnClick="btnSave_Click"><i class='icon ni ni-save'></i>&nbsp; &nbsp;<%=GetGlobalResourceObject("pages","Submit") %> </asp:LinkButton>
                                            </li>
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

    <div class="nk-block border rounded p-2 bg-primary-dim text-primary">



        <div class="col-md-4 ">


            <div class="form-group">
                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","TransferDate") %></label>

                <div class="form-control-wrap">
                    <div class="form-icon form-icon-right">
                        <em class="icon ni ni-calendar-alt"></em>
                    </div>
                    <asp:TextBox runat="server" ID="txtFromDate" placeholder="__/__/____" class="form-control date-picker"></asp:TextBox>
                </div>


            </div>
            <div class="form-group">
                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Notes") %></label>
                <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" class="form-control" Height="50px"></asp:TextBox>
            </div>
        </div>
    </div>


    <div class="row mt-10" style="margin-top: 20px;">
        <div class="col-5">

            <div class="card card-stretch border border-danger" style="min-height: 550px">
                <div class="card-inner">

                    <asp:Label runat="server" ID="lblerror"></asp:Label>
                    <div class="col-lg-12">
                        <div class="portlet box portlet-blue">

                            <div class="portlet-body">
                                <div role="form">


                                    <div class="row">
                                        <div class="col-md-12">



                                            <div class="form-group" runat="server">
                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","assignedToLocation") %>  </label>

                                                <input type="text" id="txtOwnerLocationCode" class="form-control" placeholder="Type to filter" autocomplete="off" />
                                                <input id="selectedLocation" runat="server" value="0" type="hidden" class="selectedLocation" />
                                                <asp:Button runat="server" ID="btnReload" OnClick="btnReload_Click" CssClass="hide" />

                                            </div>
                                            <div class="form-group divEmployee" id="divEmployee" runat="server">
                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","RefEmployee") %>  </label>
                                                <asp:DropDownList ID="lstRefEmployee" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static"></asp:DropDownList>

                                            </div>

                                            <div class="form-group">
                                                <asp:LinkButton runat="server" ID="lnkFilter" OnClick="lnkFilter_Click" class="btn btn-primary"><em class="icon ni ni-search"></em><span>عرض العهد</span></asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="card-inner">

                    <asp:DataGrid runat="server" ID="grdItems" AutoGenerateColumns="False"  
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
                            <asp:BoundColumn DataField="ItemTag" HeaderText="<%$ Resources:pages,Tagid %>"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="<%$ Resources:pages,PurchaseItems %>">
                                <ItemTemplate>
                                    <span><%#( ZeroIntergerIFNull(gets(Eval("EmpRefCode")))==0?"<em class='icon ni ni-building  text-info'></em>&nbsp;" :"<em class='icon ni ni-user-list  text-info'></em> &nbsp;")  %></span> <%#Eval("ItemNameAr") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <%--  <asp:TemplateColumn HeaderText="<%$ Resources:pages,status %>">
                                <ItemStyle Width="5%" />
                                <ItemTemplate>
                                <div><%# showAction(ZeroIntergerIFNull(gets(Eval("ActionId"))), gets( Eval("LastActiontitleAr"))) %></div> 
                                    <div><%# showAvailability(ZeroIntergerIFNull(gets(Eval("StatusId"))), gets( Eval("AvailabilityStatusAr"))) %></div>
                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                        </Columns>
                    </asp:DataGrid>

                    <div class="datatable-footer">
                        <div class="dataTables_info" id="DataTables_Table_3_info" role="status" aria-live="polite">
                            <asp:Label ID="lblcount" runat="server"></asp:Label>
                        </div>
                        <div class="dataTables_paginate paging_simple_numbers" id="DataTables_Table_3_paginate">

                            <cc1:Pager CurrentIndex="1" OnCommand="pager_Command" ShowFirstLast="False" ID="pager1"
                                runat="server" Width="100%" PageSize="20" AlternativeTextEnabled="False" BackToFirstClause="" BackToPageClause="" EnableSmartShortCuts="True" EnableTheming="True" FirstClause="" FromClause="" GoClause="" GoToLastClause="" LastClause="" NextClause="التالى" OfClause="من" PageClause="صفحة" PreviousClause="السابق" RTL="True" ShowingResultClause="" ShowResultClause=""></cc1:Pager>


                        </div>
                    </div>

                </div>
            </div>


        </div>
        <div class="col-1" style="padding-top:80px;">
            <%-- <div class="mt-2">
                <a href="#">
                    <div class="preview-icon-box card">

                        <div class="preview-icon-wrap">
                            <em class="ni ni-chevrons-left"></em>
                        </div>


                    </div>
                </a>

            </div>--%>
            <div class="mt-2">
                <asp:LinkButton runat="server" ID="lnkAddItem" class="mt-5" OnClick="lnkAddItem_Click"><div class="preview-icon-box card"><div class="preview-icon-wrap"> <em class="ni ni-chevrons-left"></em>   </div></div> </asp:LinkButton>

            </div>
            <div class="mt-2">
              <asp:LinkButton runat="server" ID="lnkRemove" class="mt-5" OnClick="lnkRemove_Click"><div class="preview-icon-box card text-danger"><div class="preview-icon-wrap"> <em class="ni ni-chevrons-right"></em>   </div></div> </asp:LinkButton>
            </div>

            <%--<div class="mt-2">
                <a href="#" class="mt-5">
                    <div class="preview-icon-box card">

                        <div class="preview-icon-wrap"> <em class="ni ni-chevrons-right"></em>   </div>


                    </div>
                </a>

            </div>--%>
        </div>

        <div class="col-6">

            <div class="card card-stretch border border-success" style="min-height: 550px">


                <div class="card-inner">

                    <div role="form">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","assignedToLocation") %>  </label>

                                    <input type="text" id="txtToLocation" class="form-control" placeholder="Type to filter" autocomplete="off" />
                                    <input id="selectedToLocation" runat="server" value="0" type="hidden" class="selectedToLocation" />
                                    <asp:Button runat="server" ID="Button1" OnClick="btnReload_Click" CssClass="hide" />

                                </div>
                                <div class="form-group divEmployee">
                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","RefEmployee") %>  </label>
                                    <asp:DropDownList ID="lstToEmpRefCode" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static"></asp:DropDownList>

                                </div>



                            </div>
                        </div>


                    </div>

                </div>

                <div class="card-inner">

                    <asp:DataGrid runat="server" ID="grdSelectedItems" AutoGenerateColumns="False"
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
                            <asp:BoundColumn DataField="ItemTag" HeaderText="<%$ Resources:pages,Tagid %>"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="<%$ Resources:pages,PurchaseItems %>">
                                <ItemTemplate>
                                    <span><%#( ZeroIntergerIFNull(gets(Eval("EmpRefCode")))==0?"<em class='icon ni ni-building  text-info'></em>&nbsp;" :"<em class='icon ni ni-user-list  text-info'></em> &nbsp;")  %></span> <%#Eval("ItemNameAr") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <%--  <asp:TemplateColumn HeaderText="<%$ Resources:pages,status %>">
                                <ItemStyle Width="5%" />
                                <ItemTemplate>
                                <div><%# showAction(ZeroIntergerIFNull(gets(Eval("ActionId"))), gets( Eval("LastActiontitleAr"))) %></div> 
                                    <div><%# showAvailability(ZeroIntergerIFNull(gets(Eval("StatusId"))), gets( Eval("AvailabilityStatusAr"))) %></div>
                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                        </Columns>
                    </asp:DataGrid>

                    <div class="datatable-footer">
                        <div class="dataTables_info" id="DataTables_Table_3_info" role="status" aria-live="polite">
                            <asp:Label ID="lblSelectedCount" runat="server"></asp:Label>
                        </div>
                        <div class="dataTables_paginate paging_simple_numbers" id="DataTables_Table_3_paginate">

                            <cc1:Pager CurrentIndex="1" OnCommand="pager2_Command" ShowFirstLast="False" ID="pager2"
                                runat="server" Width="100%" PageSize="20" AlternativeTextEnabled="False" BackToFirstClause="" BackToPageClause="" EnableSmartShortCuts="True" EnableTheming="True" FirstClause="" FromClause="" GoClause="" GoToLastClause="" LastClause="" NextClause="التالى" OfClause="من" PageClause="صفحة" PreviousClause="السابق" RTL="True" ShowingResultClause="" ShowResultClause=""></cc1:Pager>


                        </div>
                    </div>

                </div>

            </div>



        </div>

    </div>




    <script src="/Layout/Assets/businessScripts/locationCombo.js"></script>
</asp:Content>
