<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/MainEmpty.Master" AutoEventWireup="true" CodeBehind="AssetDetails.aspx.cs" Inherits="UI.Web.Modules.Assets.AssetDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="JavaScript" type="text/javascript">


        function chkImage() {

            var txt = document.getElementById("<%=txtFromDate.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please, Enter Action Date");
                return false;
            }


            var hdnSelectedLocation = document.getElementById("<%=selectedLocation.ClientID %>")

            if (hdnSelectedLocation.value == "" || hdnSelectedLocation.value == "0") {
                Swal.fire("Please, select  Location ");
                return false;
            }

            var txt = document.getElementById("<%=hdnType.ClientID %>")
            if (txt.value == "1") {
                var emp = document.getElementById("<%=lstRefEmployee.ClientID %>")
                if (emp.value == "" || emp.value == "0") {
                    Swal.fire("Please, Select Employee   ");
                    return false;
                }
            }

            return true;
        }
        function chkImage2() {

            var txt = document.getElementById("<%=txtOtherAction.ClientID %>")
            if (txt.value == "") {
                Swal.fire("Please, Enter Action Date");
                return false;
            }


            var hdnSelectedLocation = document.getElementById("<%=selectedToLocation.ClientID %>")
            var txt = document.getElementById("<%=hdnActionType.ClientID %>")
            if (txt.value == "2") {
                if (hdnSelectedLocation.value == "" || hdnSelectedLocation.value == "0") {
                    Swal.fire("Please, select  Location ");
                    return false;
                }
            }
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

        function SetActionType(type) {


            var txt = document.getElementById("<%=hdnActionType.ClientID %>");
            txt.value = type;


            if (type == "1") {//checkout
                $('.targetLocation').toggle(false);
                $('.modaltitle').html("تسليم عهدة ");
            } else if (type == "2") {//checkIn
                $('.targetLocation').toggle(true);
                $('.modaltitle').html("إستلام عهدة ");

            } else if (type == "3") {//Lost
                $('.targetLocation').toggle(false);
                $('.modaltitle').html("عهدة مفقودة ");

            } else if (type == "4") {//Broken
                $('.targetLocation').toggle(false);
                $('.modaltitle').html("إتلاف عهدة ");

            } else if (type == "5") {//Dispose
                $('.targetLocation').toggle(false);
                $('.modaltitle').html("إهلاك عهدة ");

            }  else if (type == "6") {//Transfer
                $('.targetLocation').toggle(false);
                $('.modaltitle').html("تحويل عهدة ");

            }
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



         <%--   $(".iframe75callback").click(
                function (event) {
                    event.preventDefault();
                    var elementURL = $(this).attr("href");
                    $.colorbox({
                        iframe: true, href: elementURL, width: "75%", height: "95%"
                        , onCleanup: function () {
                            var btn = $('#<%= btnReload.ClientID %>');
                            btn.click();
                        }

                    });
                });--%>
        });


    </script>
    <input id="hdnType" runat="server" type="hidden" />
    <input id="hdnItemCount" runat="server" type="hidden" />


    <input id="hdnActionType" runat="server" type="hidden" />



    <asp:UpdatePanel runat="server" ID="Updatepanel1" ChildrenAsTriggers="true" UpdateMode="conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>

    <input id="hdnActiveTab" runat="server" type="hidden" />

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
    </div>
    <div class="nk-block">
        <asp:Label runat="server" ID="lblerror"></asp:Label>

        <div class="card card-stretch">
            <div class="card-inner-group">
                <div class="card-inner" data-select2-id="22">
                    <div class="card-title-group" data-select2-id="21">
                        <div class="card-title">
                            <h5 class="title"><%=GetGlobalResourceObject("pages","CustodyDetails") %></h5>
                        </div>
                        <div class="card-tools mr-n1" data-select2-id="20">
                            <ul class="btn-toolbar gx-1" data-select2-id="19">
                                <li>
                                    <div class="dropdown">
                                        <a href="#" class="btn btn-primary" data-toggle="dropdown"><span>إجراءات العهدة</span><em class="icon ni ni-chevron-down"></em></a>
                                        <div class="dropdown-menu dropdown-menu-right dropdown-menu-auto mt-1">
                                            <ul class="link-list-opt no-bdr">

                                                <li id="checkout" runat="server"><a href="javascript:void(0)" onclick="SetActionType(1)" data-toggle="modal" data-target="#modalDefault"><span class='nk-menu-icon'><em class='icon ni ni-check-round-cut'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Actions","Checkout") %></span></a></li>
                                              
                                                <li><a href="javascript:void(0)" onclick="SetActionType(2)" data-toggle="modal" data-target="#modalCheckIn"><span class='nk-menu-icon'><em class='icon ni ni-unfold-less'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Actions","CheckIn") %></span></a></li>
                                                <li class="divider"></li>
                                                <li><a href="javascript:void(0)" onclick="SetActionType(3)" data-toggle="modal" data-target="#modalCheckIn"><span class='nk-menu-icon'><em class='icon ni ni-monitor'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Actions","Lost/Missing") %></span></a></li>
                                                <li><a href="javascript:void(0)" onclick="SetActionType(4)" data-toggle="modal" data-target="#modalCheckIn"><span class='nk-menu-icon'><em class='icon ni ni-unlink'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Actions","Broken") %></span></a></li>
                                                <li><a href="javascript:void(0)" onclick="SetActionType(5)" data-toggle="modal" data-target="#modalCheckIn"><span class='nk-menu-icon'><em class='icon ni ni-update'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Actions","Dispose") %></span></a></li>

                                                <li class="divider"></li>

                                                <li><a href="javascript:void(0)" onclick="SetActionType(6)" data-toggle="modal" data-target="#modalDefault"><span class='nk-menu-icon'><em class='icon ni ni-repeat'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Actions","Transfer") %></span></a></li>

                                            </ul>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>

                    </div>
                </div>
                <div class="card-inner">
                    <div class="nk-block">
                        <div class="nk-block-head">
                            <div style="float: right; padding: 0 10px;">
                                <%= FillImage(gets(ItemImage), Resources.Utilities.resourcespath+"uploads/ItemsData/", 50, 50,"")%>
                            </div>
                            <h5 class="title"><%=ItemNameAr %></h5>
                            <p>
                                <span class="profile-ud-label"><%=GetGlobalResourceObject("pages","Category") %> : <%=CategoryName %></span>
                            </p>
                            <p><%=ItemNamedesc %></p>

                        </div>
                        <!-- .nk-block-head -->
                        <div class="profile-ud-list border <%= ZeroIntergerIFNull(statusId.ToString())==1?"border-success":"border-warning" %> rounded m-0 p-10">
                            <div class="profile-ud-item">
                                <div class="profile-ud wider">
                                    <span class="profile-ud-label"><%=GetGlobalResourceObject("pages","PurchaseDate") %></span>
                                    <span class="profile-ud-value"><%=TransDate %></span>
                                </div>
                            </div>
                            <div class="profile-ud-item">
                                <div class="profile-ud wider">
                                    <span class="profile-ud-label"><%=GetGlobalResourceObject("pages","Vendor") %></span>
                                    <span class="profile-ud-value"><%=VendorNameAr%></span>
                                </div>
                            </div>

                            <div class="profile-ud-item">
                                <div class="profile-ud wider">
                                    <span class="profile-ud-label"><%=GetGlobalResourceObject("pages","FinanceRefCode") %></span>
                                    <span class="profile-ud-value"><%=FinanceRefCode %></span>
                                </div>
                            </div>
                            <div class="profile-ud-item">
                                <div class="profile-ud wider">
                                    <span class="profile-ud-label"><%=GetGlobalResourceObject("pages","refCode") %></span>
                                    <span class="profile-ud-value"><%= ItemRefCode %></span>
                                </div>
                            </div>
                            <div class="profile-ud-item">
                                <div class="profile-ud wider">
                                    <span class="profile-ud-label"><%=GetGlobalResourceObject("pages","LastAction") %></span>
                                    <span class="profile-ud-value"><%=LastActiontitleAr %></span>
                                </div>
                            </div>
                            <div class="profile-ud-item">
                                <div class="profile-ud wider">
                                    <span class="profile-ud-label"><%=GetGlobalResourceObject("pages","Status") %></span>
                                    <span class="profile-ud-value"><%=AvailabilityStatusAr %></span>
                                </div>
                            </div>
                            <div class="profile-ud-item">
                                <div class="profile-ud wider">
                                    <span class="profile-ud-label"><%=GetGlobalResourceObject("pages","UnitRefCode") %></span>
                                    <span class="profile-ud-value"><%=UnitRefCode %></span>
                                </div>
                            </div>



                        </div>
                        <!-- .profile-ud-list -->
                    </div>
                    <div class="nk-block rounded">
                        <div class="nk-block-head nk-block-head-line">
                            <h4 class="title overline-title text-base"><%=GetGlobalResourceObject("pages","AddtionalData") %> </h4>
                        </div>
                        <!-- .nk-block-head -->
                        <div class="profile-ud-list  m-0 p-10">
                            <div class="profile-ud-item">
                                <div class="profile-ud wider">
                                    <span class="profile-ud-label"><%=GetGlobalResourceObject("pages","lastActionDate") %></span>
                                    <span class="profile-ud-value"><%=ActionDate %></span>
                                </div>
                            </div>
                            <div class="profile-ud-item">
                                <div class="profile-ud wider">
                                    <span class="profile-ud-label"><%=GetGlobalResourceObject("pages","LocationName") %></span>
                                    <span class="profile-ud-value"><a href="locationAssets.aspx?lid=<%=locationId %>" class="iframe75"><%=LocationName %></a> </span>
                                </div>
                            </div>
                            <div class="profile-ud-item">
                                <div class="profile-ud wider">
                                    <span class="profile-ud-label"><%=GetGlobalResourceObject("pages","EmpName") %></span>
                                    <span class="profile-ud-value"><a href="EmpAssets.aspx?EmpRef=<%=EmpRef %>" class="iframe"><%=EmpName %></a></span>
                                </div>
                            </div>

                        </div>
                        <!-- .profile-ud-list -->
                    </div>


                </div>

                <div class="card-inner">


                    <ul class="nav nav-tabs">
                        <li class="nav-item" onclick="setActiveTab('1')"><a class="nav-link <%=getActiveTab("1") %>" data-toggle="tab" href="#MasterData"><em class="icon ni ni-list-thumb-alt"></em><span><%=GetGlobalResourceObject("pages","History") %> </span></a></li>
                        <%--<li class="nav-item" onclick="setActiveTab('2')"><a class="nav-link <%=getActiveTab("2") %> " data-toggle="tab" href="#Items"><em class="icon ni ni-list-thumb-alt"></em><span><%=GetGlobalResourceObject("pages","ItemsList") %> </span>(<asp:Label ID="lblItemCount" runat="server" ClientIDMode="Static">0</asp:Label>)</a></li>--%>
                        <%--<li class="nav-item" onclick="setActiveTab('3')"><a class="nav-link <%=getActiveTab("3") %>" data-toggle="tab" href="#StatusTracking"><em class="icon ni ni-tranx"></em><span><%=GetGlobalResourceObject("pages","ItemStatusTracking") %> </span></a></li>--%>
                        <li class="nav-item" onclick="setActiveTab('4')"><a class="nav-link <%=getActiveTab("4") %>" data-toggle="tab" href="#Attachments"><em class="icon ni ni-folders"></em><span><%=GetGlobalResourceObject("pages","Attachments") %> </span></a></li>
                        <li class="nav-item" onclick="setActiveTab('5')"><a class="nav-link <%=getActiveTab("5") %>" data-toggle="tab" href="#Notes"><em class="icon ni ni-list-round"></em><span><%=GetGlobalResourceObject("pages","Notes") %> </span></a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade in <%=getActiveTab("1") %>" id="MasterData">
                            <asp:Label runat="server" ID="Label1"></asp:Label>
                            <div class="card-inner">

                                <asp:DataGrid runat="server" ID="grdEventLog" AutoGenerateColumns="False"
                                    AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter">
                                    <PagerStyle Visible="False" />

                                    <Columns>
                                        <asp:BoundColumn DataField="Code" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="<%$ Resources:pages,Event %>">
                                            <ItemTemplate>
                                                <div><%#ZeroIntergerIFNull(gets(Eval("EmpRefCode")))==0?"<em class='icon ni ni-building text-info'></em>":"<em class='icon ni ni-user-list  text-info'></em>" %>  <%# showAction(ZeroIntergerIFNull(gets(Eval("actionId"))), gets( Eval("ActionsAr"))) %></div>
                                                <div><%#NullDateifEmptyText(Eval("ActionDate")) %></div>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$ Resources:pages,assignedTo %>">

                                            <ItemTemplate>
                                                <div class="textsize-12"><a href="assetsListPopup.aspx?locid=<%# Eval("ToLocationId") %>&empid=0" class="iframe"><%#Eval("LocationNameAr") %></a></div>
                                                <div class="textsize-12"><a href="assetsListPopup.aspx?locid=0&empid=<%# Eval("EmpRefCode") %>" class="iframe"><%#Eval("EmpName") %></a></div>

                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <%--                                        <asp:BoundColumn DataField="EmpName" HeaderText="<%$ Resources:pages,EmpName %>"  ></asp:BoundColumn>--%>
                                        <%--                                        <asp:BoundColumn DataField="ActionDate" HeaderText="<%$ Resources:pages,eventDate %>" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>--%>
                                        <asp:BoundColumn DataField="CreatedAt" HeaderText="<%$ Resources:pages,CreatedAt %>" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Notes" HeaderText="<%$ Resources:pages,Notes %>"></asp:BoundColumn>

                                        <asp:TemplateColumn HeaderText="<%$ Resources:pages,status %>">

                                            <ItemTemplate>

                                                <%#showAvailability(ZeroIntergerIFNull(gets(Eval("statusId"))), gets(Eval("AvailabilityStatusAr"))) %>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                    </Columns>
                                </asp:DataGrid>

                            </div>
                        </div>
                        <div class="tab-pane fade <%=getActiveTab("2") %>" id="Items">
                        </div>

                        <div class="tab-pane fade <%=getActiveTab("3") %>" id="Attachments">
                        </div>

                        <div class="tab-pane fade <%=getActiveTab("4") %>" id="StatusTracking">
                        </div>

                        <div class="tab-pane fade <%=getActiveTab("5") %>" id="Notes">
                        </div>

                    </div>

                </div>

            </div>
        </div>


    </div>


    <!-- Modal Trigger Code -->


    <!-- Modal Content Code -->
    <div class="modal fade" tabindex="-1" id="modalDefault">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <a href="#" class="close" data-dismiss="modal" aria-label="Close">
                    <em class="icon ni ni-cross"></em>
                </a>
                <div class="modal-header">
                    <h5 class="modal-title modaltitle">تسليم عهدة</h5>
                </div>
                <div class="modal-body">

                    <div role="form">
                        <div class="row" style="margin-bottom: 20px;">
                            <div class="col-md-12">


                                <div class="form-group">

                                    <div class="custom-control custom-radio" onclick="setSelectedtype('1')">
                                        <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                        <label class="custom-control-label" for="customRadio1">عهدة شخصية </label>
                                    </div>

                                    <div class="custom-control custom-radio" onclick="setSelectedtype('2')">
                                        <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                        <label class="custom-control-label" for="customRadio2">عهدة تنظيمية </label>
                                    </div>
                                </div>

                                <div class="form-group" runat="server" style="display: none">
                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","CustodyType") %>  </label>

                                    <asp:RadioButtonList ID="custodyType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="شخصية" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="تنظيمية" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>
                                <div class="form-group">
                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","CustodtDate") %></label>

                                    <div class="form-control-wrap">
                                        <div class="form-icon form-icon-right">
                                            <em class="icon ni ni-calendar-alt"></em>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtFromDate" placeholder="__/__/____" class="form-control date-picker"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="custom-control custom-checkbox">
                                        <input type="checkbox" class="custom-control-input" id="chkReturnDate">
                                        <label class="custom-control-label" for="chkReturnDate"></label>
                                    </div>

                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","ReturnDate") %></label>

                                    <div class="form-control-wrap" id="disReturnDate" style="display: none">
                                        <div class="form-icon form-icon-right">
                                            <em class="icon ni ni-calendar-alt"></em>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtReturnDate" placeholder="__/__/____" class="form-control date-picker" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group" runat="server">
                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","assignedToLocation") %>  </label>

                                    <input type="text" id="txtOwnerLocationCode" class="form-control" placeholder="Type to filter" autocomplete="off" />
                                    <input id="selectedLocation" runat="server" value="0" type="hidden" class="selectedLocation" />

                                </div>
                                <div class="form-group divEmployee" id="divEmployee" runat="server">
                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","RefEmployee") %>  </label>
                                    <asp:DropDownList ID="lstRefEmployee" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static"></asp:DropDownList>

                                </div>
                                <div class="form-group">
                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Notes") %></label>
                                    <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" class="form-control" Height="80px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-action">
                                    <asp:LinkButton runat="server" ID="lnkSaveAction" OnClientClick="return chkImage();" OnClick="lnkSaveAction_Click" CssClass="btn btn-primary"><%=GetGlobalResourceObject("pages","Submit") %></asp:LinkButton>
                                </div>


                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="modal fade" tabindex="-1" id="modalCheckIn">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <a href="#" class="close" data-dismiss="modal" aria-label="Close">
                    <em class="icon ni ni-cross"></em>
                </a>
                <div class="modal-header">
                    <h5 class="modal-title modaltitle">إستلام العهدة</h5>
                </div>
                <div class="modal-body">

                    <div role="form">
                        <div class="row" style="margin-bottom: 20px;">
                            <div class="col-md-12">


                                <div class="form-group">
                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","ActionDate") %></label>

                                    <div class="form-control-wrap">
                                        <div class="form-icon form-icon-right">
                                            <em class="icon ni ni-calendar-alt"></em>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtOtherAction" placeholder="__/__/____" class="form-control date-picker"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="form-group targetLocation" runat="server">
                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","assignedToLocation") %>  </label>

                                    <input type="text" id="txtToLocation" class="form-control" placeholder="Type to filter" autocomplete="off" />
                                    <input id="selectedToLocation" runat="server" value="0" type="hidden" class="selectedToLocation" />

                                </div>

                                <div class="form-group">
                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Notes") %></label>
                                    <asp:TextBox runat="server" ID="txtOtherNotes" TextMode="MultiLine" class="form-control" Height="80px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-action">
                                    <asp:LinkButton runat="server" ID="lnkOtherActions" OnClientClick="return chkImage2();" OnClick="lnkOtherActions_Click" CssClass="btn btn-primary"><%=GetGlobalResourceObject("pages","Submit") %></asp:LinkButton>
                                </div>


                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

    <script src="/wwwroot/assets/js/businessScripts/locationCombo.js"></script>
</asp:Content>
