<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="AssetTransfer.aspx.cs" Inherits="UI.Web.Modules.Assets.AssetTransfer" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input id="hdnMasterID" runat="server" type="hidden" />
    <input id="hdnActiveTab" runat="server" type="hidden" />
   <style>
    .transfer-type input[type="radio"] {
        margin-right: 24px; /* مسافة بين الدائرة والنص */
    }

    .transfer-type label {
        margin-right: 10px; /* مسافة بين العناصر */
        font-weight: bold;
        font-size: 15px;
        color: #0C476B; /* لون أساسي من ثيمك */
    }
</style>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" />


    <!-- jQuery UI -->
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Function to initialize the DatePicker
            function initializeDatepicker() {
                $(".date-pickers").datepicker({
                    dateFormat: 'dd/mm/yy',   // Set the date format (optional)
                    changeMonth: true,
                    changeYear: true
                });
            }

            // Initialize DatePicker when the page loads
            initializeDatepicker();

            // Reinitialize DatePicker after partial postbacks (AJAX)
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                initializeDatepicker();
            });
        });
    </script>
    <script language="JavaScript" type="text/javascript">


        function chkImage() {

            var txt = document.getElementById("<%=txtFromDate.ClientID %>")
            if (txt.value == "") {
                Swal.fire("يرجى اختيار التاريخ");
                return false;
            }


            var hdnSelectedLocation = document.getElementById("<%=selectedToLocation.ClientID %>")

            //if (hdnSelectedLocation.value == "" || hdnSelectedLocation.value == "0") {
            //    Swal.fire("Please, select  Location ");
            //    return false;
            //}

          <%--  var txt = document.getElementById("<%=hdnType.ClientID %>")
            if (txt.value == "1") {
                var emp = document.getElementById("<%=lstRefEmployee.ClientID %>")
                if (emp.value == "" || emp.value == "0") {
                    Swal.fire("Please, Select Employee   ");
                    return false;
                }
            }--%>
            var emp2 = document.getElementById("<%=lstRefEmployee.ClientID %>")
            if (emp2.value == "" || emp2.value == "0") {
                Swal.fire("يرجى اختيار الموظف   ");
                return false;
            }

           <%-- var emp = document.getElementById("<%=lstToEmpRefCode.ClientID %>")
            if (emp.value == "" || emp.value == "0") {
                Swal.fire("يرجى اختيار الموظف   ");
                return false;
            }--%>

            <%--     var txt = document.getElementById("<%=hdnItemCount.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("Please, select Items For action");
                return false;
            }--%>

            var transferType = document.querySelector('input[name="<%= rblTransferType.UniqueID %>"]:checked');
            if (!transferType) {
                Swal.fire("يرجى اختيار نوع التحويل (موظف / مخزن)");
                return false;
            }

            var selectedValue = transferType.value;

            if (selectedValue === "Employee") {
                // تحقق من الموظف
                var emp = document.getElementById("<%= lstToEmpRefCode.ClientID %>");
        if (!emp || emp.value === "" || emp.value === "0") {
            Swal.fire("يرجى اختيار الموظف الذي سيتم التحويل له");
            return false;
        }
    } 
    else if (selectedValue === "Store") {
        // تحقق من المخزن
        var store = document.getElementById("<%= lstToStore.ClientID %>");
        if (!store || store.value === "" || store.value === "0") {
            Swal.fire("يرجى اختيار المخزن الذي سيتم التحويل له");
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
          <script>
              function reinitializeDropDownSearch() {
                  console.log("Reinitializing search on DropDownList...");

                  // Select2 reinitialization
                  if ($('.form-select').length > 0) {
                      $('.form-select').select2();
                  }
              }

              // Attach to UpdatePanel postback completion
              Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                  reinitializeDropDownSearch();
              });

              // Call this on page load to initialize search
              $(document).ready(function () {
                  reinitializeDropDownSearch();
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

    <div class="nk-block border rounded p-2 bg-primary-dim text-primary" >



        <div class="col-md-4 ">


            <div class="form-group">
                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","TransferDate") %></label>

                <div class="form-control-wrap">
                    <div class="form-icon form-icon-right">
                        <em class="icon ni ni-calendar-alt"></em>
                    </div>
                    <asp:TextBox runat="server" ID="txtFromDate" placeholder="__/__/____" class="form-control date-pickers"></asp:TextBox>
                </div>


            </div>
            <div class="form-group">
                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Notes") %></label>
                <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" class="form-control" Height="50px"></asp:TextBox>
            </div>
        </div>
    </div>

<asp:UpdatePanel runat="server" ID="Updatepanel2">
    <ContentTemplate>
        <div class="row mt-10" style="margin-top: 20px;">
            <!-- الكارد الأول: الموظف والعهد -->
            <div class="col-5">
                <div class="card card-stretch border border-danger" style="min-height: 550px">
                    <div class="card-inner">
                        <asp:Label runat="server" ID="lblerror"></asp:Label>
                        <div class="form-group" style="display:none">
                           <label class="control-label" for=""><%=GetGlobalResourceObject("pages","assignedToLocation") %>  </label>

                           <input type="text" id="txtToLocation" class="form-control" placeholder="Type to filter" autocomplete="off" />
                           <input id="selectedToLocation" runat="server" value="0" type="hidden" class="selectedToLocation" />
                           <asp:Button runat="server" ID="Button1" OnClick="btnReload_Click" CssClass="hide" />

                        </div>
                        <div class="form-group divEmployee" id="divEmployee" runat="server">
                            <label class="control-label"><%=GetGlobalResourceObject("pages","RefEmployee") %></label>
                            <asp:DropDownList ID="lstRefEmployee" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="lstRefEmployee_SelectedIndexChanged"
                                class="form-control form-select" data-search="on" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="card-inner">
                        <asp:DataGrid runat="server" ID="grdItems" AutoGenerateColumns="False"  
                            AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter">
                            <PagerStyle Visible="False" />
                            <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                            <Columns>
                                <asp:BoundColumn DataField="Code" Visible="false"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <input id="chkAllItems" class="checkall" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkItem', this.checked)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkItem" CssClass="check" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="م">
                                    <ItemTemplate>
                                        <%#ZeroIntergerIFNull((DataBinder.Eval(Container, "ItemIndex")).ToString()) + 1%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="رقم المادة">
                                    <ItemTemplate>
                                        <%#EmptyIfZero(gets(DataBinder.Eval(Container.DataItem, "ItemRefCode")))%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="وصف المادة">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "ItemNameAr")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="الملاحظات">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "Notes")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        <div class="datatable-footer">
                            <asp:Label ID="lblcount" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

            <!-- أزرار التحويل -->
            <div class="col-1" style="padding-top:80px;">
                <div class="mt-2">
                    <asp:LinkButton runat="server" ID="lnkAddItem" 
                        OnClientClick="return chkImage();" 
                        OnClick="lnkAddItem_Click">
                        <div class="preview-icon-box card">
                            <div class="preview-icon-wrap"><em class="ni ni-chevrons-left"></em></div>
                        </div>
                    </asp:LinkButton>
                </div>
                <div class="mt-2">
                    <asp:LinkButton runat="server" ID="lnkRemove" 
                        OnClientClick="return chkImage();" 
                        OnClick="lnkRemove_Click">
                        <div class="preview-icon-box card text-danger">
                            <div class="preview-icon-wrap"><em class="ni ni-chevrons-right"></em></div>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>

            <!-- الكارد الثاني: تحويل لموظف أو لمخزن -->
            <div class="col-6">
                <div class="card card-stretch border border-success" style="min-height: 550px">
                    <div class="card-inner">
                        <!-- اختيار نوع التحويل -->
                        <div class="form-group">
                            <label class="control-label" style="font-size:20px;font-weight:bold"> نوع التحويل الى</label>
                            <asp:RadioButtonList ID="rblTransferType" runat="server" 
                                AutoPostBack="true"
                                OnSelectedIndexChanged="rblTransferType_SelectedIndexChanged"
                                RepeatDirection="Horizontal" 
                                
                                CssClass="transfer-type">
                                <asp:ListItem Text="موظف" Value="Employee" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="مخزن" Value="Store"></asp:ListItem>
                            </asp:RadioButtonList>

                        </div>

                        <!-- اختيار الموظف -->
                        <div class="form-group" id="divEmployeeTarget" runat="server">
                            <label class="control-label"><%=GetGlobalResourceObject("pages","RefEmployee") %></label>
                            <asp:DropDownList ID="lstToEmpRefCode" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="lstToEmpRefCode_SelectedIndexChanged"
                                class="form-control form-select" data-search="on" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>

                        <!-- اختيار المخزن -->
                        <div class="form-group" id="divStore" runat="server" style="display:none">
                            <label class="control-label">المخزن</label>
                            <asp:DropDownList ID="lstToStore" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="lstToStore_SelectedIndexChanged"
                                class="form-control form-select" data-search="on" ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="card-inner">
                        <asp:DataGrid runat="server" ID="grdSelectedItems" AutoGenerateColumns="False"
                            AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter">
                            <PagerStyle Visible="False" />
                            <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                            <Columns>
                                <asp:BoundColumn DataField="Code" Visible="false"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <input id="chkAllSelected" class="checkall" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkItem', this.checked)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkItem" CssClass="check" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="م">
                                    <ItemTemplate>
                                        <%#ZeroIntergerIFNull((DataBinder.Eval(Container, "ItemIndex")).ToString()) + 1%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="رقم المادة">
                                    <ItemTemplate>
                                        <%#EmptyIfZero(gets(DataBinder.Eval(Container.DataItem, "ItemRefCode")))%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="وصف المادة">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "ItemNameAr")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="الملاحظات">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "Notes")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        <div class="datatable-footer">
                            <asp:Label ID="lblSelectedCount" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lstRefEmployee" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="lstToEmpRefCode" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="lstToStore" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="lnkAddItem" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="lnkRemove" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>



    <script src="/wwwroot/assets/js/businessScripts/locationCombo.js"></script>
</asp:Content>
