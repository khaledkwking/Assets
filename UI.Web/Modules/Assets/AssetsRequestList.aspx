<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="AssetsRequestList.aspx.cs" Inherits="UI.Web.Modules.MasterData.AssetsRequestList" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        function isNumberKeyq(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 13) {
                var btn = getObjById("btnAddItem")
                //alert("Enter Key "+btn.value);
                btn.click();
                return false
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        const empCache = {};

        function applyStatus(badgeId, status) {
            var badge = document.getElementById(badgeId);
            if (!badge) return;

            var s = (status || "").toLowerCase();
            if (s === "active") {
                badge.className = "badge badge-dim badge-success";
                badge.innerText = "فعال";
            } else if (s === "not-active") {
                badge.className = "badge badge-dim badge-danger";
                badge.innerText = "غير فعال";
            } else if (s === "error") {
                badge.className = "badge badge-dim badge-warning";
                badge.innerText = "فشل التحميل";
            } else {
                badge.className = "badge badge-dim badge-danger";
                badge.innerText = "غير فعال";
            }
        }

        function updateEmployeeStatus(empId, badgeId) {
            if (empCache[empId]) {
                applyStatus(badgeId, empCache[empId]);
                return;
            }

            fetch("/api/hepler/GetEmployeeStatus?empId=" + empId)
                .then(function (res) { return res.json(); })
                .then(function (result) {
                    var status = result && result.status ? result.status : "unknown";
                    empCache[empId] = status;
                    applyStatus(badgeId, status);
                })
                .catch(function () { applyStatus(badgeId, "error"); });
        }

        window.addEventListener('DOMContentLoaded', function () {
            var spans = document.querySelectorAll("span[id^='empBadge_']");
            for (var i = 0; i < spans.length; i++) {
                var span = spans[i];
                var parts = span.id.split("_"); // empBadge_{empId}_{Code}
                var empId = parts[1];
                updateEmployeeStatus(empId, span.id);
            }
        });
    </script>
    <style>
        .autocomplete {
            font-size: 15px !important;
            line-height: normal;
            padding: 10px 35px !important; /* Add padding around each item */
            margin-bottom: 5px !important; /* Add space between items */
            border-bottom: 1px solid #ddd !important; /* Optional: Add a bottom border for separation */
            text-align: right; /* Increase font size for all items */
            width: auto;
        }
    </style>


    <asp:UpdatePanel runat="server" ID="Updatepanel1" ChildrenAsTriggers="true" UpdateMode="conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>


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


        <div class="card card-stretch" id="tblshow" runat="server">
            <div class="card-inner-group">
                <div class="card-inner" data-select2-id="22">
                    <div class="card-title-group" data-select2-id="21">
                        <div class="card-title">
                            <h5 class="title">سجل إستمارات العهد (<asp:Label ID="lblCountTop" runat="server" tetx="0"></asp:Label>)</h5>
                        </div>
                        <div class="card-tools mr-n1" data-select2-id="20">
                            <ul class="btn-toolbar gx-1" data-select2-id="19">
                                <li>
                                    <a href="#" class="search-toggle toggle-search btn btn-icon" data-target="search"><em class="icon ni ni-search"></em></a>
                                </li>
                                <li class="btn-toolbar-sep"></li>

                                <li data-select2-id="18">
                                    <div class="dropdown" data-select2-id="17">
                                        <a href="#" class="btn btn-trigger btn-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                            <em class="icon ni ni-filter"></em>
                                        </a>
                                        <div class="filter-wg dropdown-menu dropdown-menu-xl dropdown-menu-right" style="" data-select2-id="16">
                                            <div class="dropdown-head">
                                                <span class="sub-title dropdown-title">بحث متقدم</span>
                                                <div class="dropdown">
                                                    <a href="#" class="link link-light">
                                                        <em class="icon ni ni-more-h"></em>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class="dropdown-body dropdown-body-rg" data-select2-id="15">
                                                <div class="row gx-6 gy-4" data-select2-id="14">

                                                    <div class="col-12">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","ProcessType") %></label>
                                                            <asp:DropDownList ID="lstFilterAction" runat="server" class="form-control form-select" data-search="on">
                                                                <asp:ListItem Text="الكل" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="عهدة شخصية" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="عهدة تنضيمية" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="عهدة غير محددة" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","EmpStatus") %></label>
                                                            <asp:DropDownList ID="lstFilterEmpStatus" runat="server" class="form-control form-select" data-search="on">
                                                                <asp:ListItem Text="الكل" Value="-1"></asp:ListItem>
                                                                <asp:ListItem Text="فعال  " Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="  غير فعال  " Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt">المواد</label>

                                                            <asp:TextBox onkeypress="return NumberKey(event,3)" runat="server"
                                                                ID="txtItemDesc" CssClass="form-control"></asp:TextBox>
                                                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2"
                                                                runat="server" TargetControlID="txtItemDesc"
                                                                CompletionInterval="10" CompletionSetCount="10" MinimumPrefixLength="1" CompletionListItemCssClass="autocomplete"
                                                                ServicePath="/modules/autocomplete/Services/TextAutoComplete.asmx" ServiceMethod="ItemAutoCompete" />
                                                        </div>
                                                    </div>
                                                     <div class="col-12">
     <div class="form-group">
         <label class="overline-title overline-title-alt">موقع العهدة</label>

    <asp:DropDownList ID="lstToLocation" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static"></asp:DropDownList>
     </div>
 </div>
                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","ReceiptDateFrom") %></label>
                                                            <div class="form-control-wrap">
                                                                <div class="form-icon form-icon-right">
                                                                    <em class="icon ni ni-calendar-alt"></em>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txtTransDate" placeholder="__/__/____" class="form-control date-pickers"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label class="overline-title overline-title-alt"><%=GetGlobalResourceObject("pages","ReceiptDateTo") %></label>
                                                            <div class="form-control-wrap">
                                                                <div class="form-icon form-icon-right">
                                                                    <em class="icon ni ni-calendar-alt"></em>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txtTransactionDateTo" placeholder="__/__/____" class="form-control date-pickers"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>



                                                    <div class="col-12">
                                                        <div class="form-group">
                                                            <asp:LinkButton runat="server" ID="btnFilter" class="btn btn-secondary" OnClick="btnFilter_Click"><i class="icon ni ni-search"></i>&nbsp;<%=GetGlobalResourceObject("pages","Filter") %> </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </li>
                                <li>
                                    <div class="dropdown">
                                        <a href="#" class="btn btn-trigger btn-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                            <em class="icon ni ni-setting"></em>
                                        </a>
                                        <div class="dropdown-menu  dropdown-menu-right" style="">
                                            <ul class="link-check">
                                                <li><a href="<%=GetGlobalResourceObject("Utilities","cutureRoute") %>/Modules/Assets/AssetCheckout.aspx?t=1"><span class='nk-menu-icon'><em class='icon ni ni-user-list-fill'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Pages","CustodyAdd") %></span></a></li>
                                                <li><a href="<%=GetGlobalResourceObject("Utilities","cutureRoute") %>/Modules/Assets/AssetCheckout.aspx?t=2"><span class='nk-menu-icon'><em class='icon ni ni-focus'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Pages","CustodyAdd1") %></span></a></li>
                                                <li>
                                                    <asp:LinkButton OnClientClick="return checkDelete();" runat="server" ID="btnDelete" OnClick="btnDelete_Click">
                                                        <i class="icon ni ni-trash"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %>

                                                    </asp:LinkButton>

                                                </li>
                                            </ul>

                                        </div>
                                    </div>
                                </li>
                                <%--<li>
                                    <asp:LinkButton runat="server" ID="btnNew" class="btn btn-icon btn-primary" OnClick="btnNew_Click"><em class="icon ni ni-plus"></em></asp:LinkButton>

                                </li>--%>
                            </ul>
                        </div>
                        <div class="card-search search-wrap" data-search="search">
                            <div class="search-content">
                                <a href="#" class="search-back btn btn-icon toggle-search" data-target="search"><em class="icon ni ni-arrow-left"></em></a>
                                <asp:TextBox runat="server" ID="txtPartOfName" CssClass="form-control border-transparent form-focus-none" placeholder="بحث "></asp:TextBox>
                                <asp:LinkButton runat="server" ID="lnkQuick" OnClick="lnkQuick_Click" class="search-submit btn btn-icon"> <em class="icon ni ni-search"></em> </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-inner p-2">

                    <%--OnItemDataBound="grdData_ItemDataBound"--%>

                    <asp:DataGrid runat="server" ID="grdAssets" AutoGenerateColumns="False" OnItemDataBound="grdData_ItemDataBound"
                        AllowPaging="true" PageSize="20" class="table table-hover table-striped table-bordered table-advanced tablesorter" data-auto-responsive="false">

                        <PagerStyle Visible="false" />
                        <Columns>
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



                            <asp:BoundColumn DataField="code" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Serial" HeaderText="<%$ Resources:pages,Serial %> "></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="<%$ Resources:pages,RequestType %>">
                                <ItemTemplate>
                                    <div class="text-primary">
                                        <%# 
                                            // شخصية
                                            (ZeroIntergerIFNull(gets(Eval("ProcessType"))) == 1 
                                                && (ZeroIntergerIFNull(gets(Eval("EmpRefCode"))) != 0 
                                                    || !string.IsNullOrEmpty(Convert.ToString(Eval("AssetOrgOwnerName")))))
                                            ? "<em class='icon ni ni-user-list text-primary'></em> &nbsp; <span class='badge badge-dim badge-light'>عهدة شخصية</span>"
                                            : (
                                                // تنظيمية
                                                (ZeroIntergerIFNull(gets(Eval("ProcessType"))) == 2 
                                                    && ZeroIntergerIFNull(gets(Eval("OrgChartRefCode"))) != 0)
                                                ? "<em class='icon ni ni-building text-info'></em> &nbsp; <span class='badge badge-outline-info'>عهدة تنظيمية</span>"
                                                : 
                                                // غير محددة
                                                "<em class='icon ni ni-alert-circle text-danger'></em> &nbsp; <span class='badge badge-outline-danger'>عهدة غير محددة</span>"
                                            )
                                        %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                           <%-- <asp:TemplateColumn HeaderText="<%$ Resources:pages,RequestType %>">
                                <ItemTemplate>--%>
                                    <%--<div class="text-primary"><%#( ZeroIntergerIFNull(gets(Eval("RequestActionType")))==2?"<em class='icon ni ni-building  text-info'></em>&nbsp; <span class='badge badge-outline-info'>عهدة تنظيمية</span>"    :"<em class='icon ni ni-user-list  text-primary'></em> &nbsp; <span class='badge badge-dim badge-light'>عهدة شخصية</span>")  %>   </div>--%>
                                   <%-- <div class="text-primary">
                                        <%#
                                            (ZeroIntergerIFNull(gets(Eval("RequestActionType"))) == 1 && ZeroIntergerIFNull(gets(Eval("EmpRefCode"))) == 0)
                                            || (ZeroIntergerIFNull(gets(Eval("RequestActionType"))) == 2 && ZeroIntergerIFNull(gets(Eval("OrgChartRefCode"))) == 0)
                                            ? "<em class='icon ni ni-alert-circle text-danger'></em> &nbsp; <span class='badge badge-outline-danger'>عهدة غير محددة</span>"
                                            : (
                                                ZeroIntergerIFNull(gets(Eval("RequestActionType"))) == 2
                                                ? "<em class='icon ni ni-building text-info'></em> &nbsp; <span class='badge badge-outline-info'>عهدة تنظيمية</span>"
                                                : "<em class='icon ni ni-user-list text-primary'></em> &nbsp; <span class='badge badge-dim badge-light'>عهدة شخصية</span>"
                                            )
                                        %>
                                     </div>

                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                           <%-- <asp:TemplateColumn HeaderText="<%$ Resources:pages,RequestLocation %>">
                                <ItemTemplate>
                                    <div class="text-info">
                                        <%#  ZeroIntergerIFNull(gets(Eval("EmpRefCode")))==0?"" :( ZeroIntergerIFNull(gets(Eval("Ora_EmpRefCode")))==0?gets(Eval("EmpName")): gets(Eval("Ora_EmpName"))) %>
                                        <%# Convert.ToInt32(Eval("RequestActionType")) == 2 ? "<span class=\"badge badge-dim badge-success\">فعال</span>" : (getBool(Eval("Emp_Active")) == true ? "<span class=\"badge badge-dim badge-success\">فعال</span>" : "<span class=\"badge badge-dim badge-danger\">غير فعال</span>") %>
                                    </div>
                                    <div class="text-indigo"><%# gets(Eval("LocationPath")) %></div>



                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                         <asp:TemplateColumn HeaderText="مكان العهدة">
                            <ItemTemplate>
                                <div class="text-info">
                                    <asp:Literal ID="litEmpNameBadge" runat="server"></asp:Literal>
                                </div>
                                <div class="text-indigo">
                                    <%# gets(Eval("LocationPath")) %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                            <%--  <asp:BoundColumn DataField="RequestRefCode" HeaderText="<%$ Resources:pages,RefCode %> "></asp:BoundColumn>--%>
                            <asp:BoundColumn DataField="RequestDate" HeaderText=" <%$ Resources:pages,ReceiptDate %>  " DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CreatedAt" HeaderText=" <%$ Resources:pages,CreatedAt %>  " DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RequestNotes" HeaderText="<%$ Resources:pages,RequestNotes %> "></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="<%$ Resources:pages,Linked %>">
                                <ItemTemplate>
                                    <asp:Label ID="lblOraEmpRefStatus" runat="server" Text='<%# Convert.ToInt32(Eval("RequestActionType")) == 2 
    ? "<span class=\"badge badge-dot badge-success\">نعم</span>" 
    : (ZeroIntergerIFNull(gets(Eval("Ora_EmpRefCode"))) != 0 
        ? "<span class=\"badge badge-dot badge-success\">نعم</span>" 
        : "<span class=\"badge badge-dot badge-danger\">لا</span>") %>'></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="2%" />
                                <ItemTemplate>
                                    <div class="drodown">
                                        <a href="#" class="btn btn-sm btn-icon btn-trigger dropdown-toggle" data-toggle="dropdown"><em class="icon ni ni-more-h"></em></a>
                                        <div class="dropdown-menu dropdown-menu-right">
                                            <ul class="link-list-opt no-bdr">
                                                <li>
                                                    <a href="AssetCheckout.aspx?t=<%#Eval("ProcessType") %>&requestCode=<%#Eval("code") %>" class="btn btn-default btn-xs"><i class="icon ni ni-edit"></i>&nbsp; <%=GetGlobalResourceObject("pages","CustodyDetails") %> </a>
                                                </li>

                                                <%-- <li>
                                                    <a href="../Reports/AssetReceipt.aspx?docId=<%#Eval("code") %>" class="btn btn-default btn-xs iframe75"><i class="icon ni ni-printer"></i>&nbsp; <%=GetGlobalResourceObject("pages","PrintRequest") %> </a>
                                                </li>--%>

                                                <%--  <li runat="server" visible='<%# (ZeroIntergerIFNull(gets(Eval("InboundLastStatusCode")))>1?false: true) %>'>
                                                    <a href="InboundItemReceving.aspx?serial=<%#Eval("serial") %>" class="btn btn-default btn-xs"><i class="icon ni ni-shrink"></i>&nbsp; <%=GetGlobalResourceObject("pages","ReceiveItem") %> </a>
                                                </li>

                                                <li>
                                                    <a href="/Modules/StoreOperations/Reports/InboundReceiptReport.aspx?id=<%#Eval("code") %>" class="iframe btn btn-default btn-xs"><i class="icon ni ni-printer"></i>&nbsp;  <%=GetGlobalResourceObject("pages","print") %></a>
                                                </li>--%>
                                            </ul>
                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:TemplateColumn>


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






    </div>

</asp:Content>
