<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="UI.Web.Modules.Dashboard.Dashboard" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .label-success {
            color: green;
        }

        .label-danger {
            color: red;
        }

        .swal2-rtl .swal2-close {
            left: 0;
            color: red;
        }

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

        .swal2-popup {
            width: auto;
        }

        .dataTables_wrapper {
            width: 100%;
        }

        .cardbox {
            box-shadow: 0px 3.6px 3.6px 0px rgba(0, 0, 0, 0.25);
            border-radius: 10px;
            background: #91C59F;
            position: relative;
            display: flex;
            align-items: center;
            box-sizing: border-box;
            margin-top: 10px;
            transition: box-shadow 0.3s, background-color 0.3s;
        }

        .selected {
            box-shadow: 10px 10px 20px 10px #65856cde;
            background: #a5d2b0;
            outline: none; /* Remove default focus outline if any */
            border-radius: 10px;
            position: relative;
            display: flex;
            align-items: center;
            box-sizing: border-box;
            margin-top: 10px;
            transition: box-shadow 0.3s, background-color 0.3s;
        }

        .text2 {
            display: inline-block;
            overflow-wrap: break-word;
            font-family: DroidNaskh;
            font-weight: 700;
            font-size: 19px;
            line-height: 1.33;
            color: #000000;
            text-align: center;
        }

        .cardbox2 {
            box-shadow: 0px 3.6px 3.6px 0px rgba(0, 0, 0, 0.25);
            border-radius: 10px;
            background: #919DC5;
            position: relative;
            display: flex;
            align-items: center;
            box-sizing: border-box;
            margin-top: 10px;
        }

        .selected2 {
            box-shadow: 1px 20px 20px 10px rgb(66,72,92,0.58);
            background: #919DC5;
            outline: none; /* Remove default focus outline if any */
            border-radius: 10px;
            position: relative;
            display: flex;
            align-items: center;
            box-sizing: border-box;
            margin-top: 10px;
            transition: box-shadow 0.3s, background-color 0.3s;
        }

        .cardbox3 {
            box-shadow: 0px 3.6px 3.6px 0px rgba(0, 0, 0, 0.25);
            border-radius: 10px;
            background: #C591B7;
            position: relative;
            display: flex;
            align-items: center;
            box-sizing: border-box;
            margin-top: 10px;
        }

        .selected3 {
            box-shadow: 1px 20px 20px 10px rgba(147, 145, 183, 0.73);
            background: #C591B7;
            outline: none; /* Remove default focus outline if any */
            border-radius: 10px;
            position: relative;
            display: flex;
            align-items: center;
            box-sizing: border-box;
            margin-top: 10px;
            transition: box-shadow 0.3s, background-color 0.3s;
        }

        .cardboxMoreLess {
            box-shadow: 0px 3.6px 3.6px 0px rgba(0, 0, 0, 0.25);
            border-radius: 10px;
            background: #C5B991;
            position: relative;
            display: flex;
            align-items: center;
            box-sizing: border-box;
            margin-top: 10px;
        }

        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #000000;
            line-height: -1px;
            padding: -21px 0.4375rem calc(-14.875rem + -1px);
            margin-top: -27px;
        }

        .table {
            width: 100%;
            margin-bottom: 1rem;
            color: #000000;
            background-color: white !important;
        }

        .text3 {
            display: inline-block;
            overflow-wrap: break-word;
            font-family: DroidNaskh;
            font-weight: 700;
            font-size: 13px;
            line-height: 1.33;
            color: #000000;
            text-align: center;
        }
    </style>

    <section id="dashboard-ecommerce" style="padding-top: 30px;">

        <div class="row match-height" style="display: none">

            <div class="col-xl-12 col-md-6 col-12" style="padding-bottom: 30px;">

                <div class="row">
                    <div class="col-lg-3 col-12">
                        <div id="card1" runat="server" class="cardbox">

                            <%--<asp:LinkButton ID="lnkbtnFilterValid" runat="server" Width="100%" OnClick="lnkbtnFilterValid_Click" OnClientClick="aspnetForm.target ='_self';">--%>

                            <table style="width: 100%; text-align: center">
                                <tr>
                                    <td>
                                        <h5 class="lblCards text2" style="padding: 25px;">العهد</h5>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" CssClass="lblCardsDetails text2" Style="padding: 10px;"></asp:Label>
                                        <span class="text2">الإجمالي</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblContractCount" CssClass="text2" runat="server" ClientIDMode="Static">500</asp:Label>
                                        <input type="hidden" id="hiddenContractCount" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblContractAmount" CssClass="text2" runat="server" ClientIDMode="Static">1000000</asp:Label>
                                        <span class="text2">د.ك  </span>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2">
                                        <asp:LinkButton ID="lnkbtnContractCountView" runat="server" class="btn btn-primaryV2" OnClientClick="aspnetForm.target ='_self';">&nbsp; &nbsp;المزيد </asp:LinkButton><%--<i class='icon ni ni-grid-add-c'></i>--%>

                                    </td>
                                </tr>
                            </table>
                            <%--</asp:LinkButton>--%>
                        </div>
                    </div>
                    <div class="col-lg-3 col-12">
                        <div id="card2" runat="server" class="cardbox2">
                            <asp:LinkButton ID="lnkbtnFilterExpired" OnClick="lnkbtnActiveEmpWithoutAsset_Click" runat="server" Width="100%" OnClientClick="aspnetForm.target ='_self';">
                                <table style="width: 100%; text-align: center">
                                    <tr>
                                        <td>
                                            <h5 class="lblCards text2" style="padding: 25px;">موظف فعال بدون عهد</h5>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="text2">

                                            <asp:Label ID="lblActiveEmpWithoutAsset" CssClass="text2" runat="server" ClientIDMode="Static"></asp:Label>&nbsp; &nbsp;موظف

                                        </td>

                                    </tr>
                                    <tr style="visibility: hidden">

                                        <td colspan="2">
                                            <asp:LinkButton ID="lnkbtnActiveEmpWithoutAsset" runat="server" class="btn btn-primaryExpired btn-primaryV2" OnClick="lnkbtnActiveEmpWithoutAsset_Click" OnClientClick="aspnetForm.target ='_self';">&nbsp; &nbsp;المزيد </asp:LinkButton>

                                        </td>
                                    </tr>
                                </table>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-lg-3 col-12">
                        <div id="card3" runat="server" class="cardbox3">
                            <asp:LinkButton ID="lnkbtnFilterAll" OnClick="lnkbtnNotActiveEmpHaveAssets_Click" runat="server" Width="100%" OnClientClick="aspnetForm.target ='_self';">
                                <table style="width: 100%; text-align: center">
                                    <tr>
                                        <td>
                                            <h5 class="lblCards text2" style="padding: 25px;">موظف غير فعال لديه عهد</h5>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="text2">
                                            <asp:Label ID="lblNotActiveEmpHaveAssets" CssClass="text2" runat="server" ClientIDMode="Static"></asp:Label>&nbsp; &nbsp;موظف

                                        </td>
                                    </tr>
                                    <tr style="visibility: hidden">
                                        <td colspan="2">
                                            <asp:LinkButton ID="lnkbtnNotActiveEmpHaveAssets" runat="server" OnClick="lnkbtnNotActiveEmpHaveAssets_Click" class="btn btn-primaryAll btn-primaryV2" OnClientClick="aspnetForm.target ='_self';">&nbsp; &nbsp;المزيد </asp:LinkButton>

                                        </td>
                                    </tr>
                                </table>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-12">
                        <div id="card4" runat="server" class="cardbox3">
                            <asp:LinkButton ID="LinkButton1" OnClick="lnkbtnNoEmpAssets_Click" runat="server" Width="100%" OnClientClick="aspnetForm.target ='_self';">
                                <table style="width: 100%; text-align: center">
                                    <tr>
                                        <td>
                                            <h5 class="lblCards text2" style="padding: 25px;">عدد استمارات العهد بدون موظف</h5>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="text2">
                                            <asp:Label ID="lblNoEmpAssets" CssClass="text2" runat="server" ClientIDMode="Static"></asp:Label>&nbsp; &nbsp;استمارة

                                        </td>
                                    </tr>
                                    <tr style="visibility: hidden">
                                        <td colspan="2">
                                            <asp:LinkButton ID="lnkbtnNoEmpAssets" runat="server" OnClick="lnkbtnNoEmpAssets_Click" class="btn btn-primaryAll btn-primaryV2" OnClientClick="aspnetForm.target ='_self';">&nbsp; &nbsp;المزيد </asp:LinkButton>

                                        </td>
                                    </tr>
                                </table>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-lg-3 col-12">
                        <div id="card5" runat="server" class="cardbox3">
                            <asp:LinkButton ID="LinkButton3" OnClick="lnkbtnEmpAssets_Click" runat="server" Width="100%" OnClientClick="aspnetForm.target ='_self';">
                                <table style="width: 100%; text-align: center">
                                    <tr>
                                        <td>
                                            <h5 class="lblCards text2" style="padding: 25px;">عدد استمارات العهد الفردية</h5>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="text2">
                                            <asp:Label ID="lblEmpAssets" CssClass="text2" runat="server" ClientIDMode="Static"></asp:Label>&nbsp; &nbsp;استمارة

                                        </td>
                                    </tr>
                                    <tr style="visibility: hidden">
                                        <td colspan="2">
                                            <asp:LinkButton ID="lnkbtnEmpAssets" runat="server" OnClick="lnkbtnEmpAssets_Click" class="btn btn-primaryAll btn-primaryV2" OnClientClick="aspnetForm.target ='_self';">&nbsp; &nbsp;المزيد </asp:LinkButton>

                                        </td>
                                    </tr>
                                </table>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-lg-3 col-12">
                        <div id="card6" runat="server" class="cardbox3">
                            <asp:LinkButton ID="LinkButton5" OnClick="lnkbtnOrgAssets_Click" runat="server" Width="100%" OnClientClick="aspnetForm.target ='_self';">
                                <table style="width: 100%; text-align: center">
                                    <tr>
                                        <td>
                                            <h5 class="lblCards text2" style="padding: 25px;">عدد استمارات العهد التنظيمية</h5>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="text2">
                                            <asp:Label ID="lblOrgAssets" CssClass="text2" runat="server" ClientIDMode="Static"></asp:Label>&nbsp; &nbsp;استمارة

                                        </td>
                                    </tr>
                                    <tr style="visibility: hidden">
                                        <td colspan="2">
                                            <asp:LinkButton ID="lnkbtnOrgAssets" runat="server" OnClick="lnkbtnOrgAssets_Click" class="btn btn-primaryAll btn-primaryV2" OnClientClick="aspnetForm.target ='_self';">&nbsp; &nbsp;المزيد </asp:LinkButton>

                                        </td>
                                    </tr>
                                </table>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Statistics Card -->
        <div style="background-color: white; padding: 30px; border-radius: 14px; box-shadow: 10px 10px 5px lightgray;">
            <div class="col-xl-12 col-md-12 col-12">
                <div class="row">

                    <div class="col-xl-6 col-md-6 col-12">
                        <div class="card card-statistics">
                            <div class="card-body statistics-body">
                                <div class="row">
                                    <div class="col-md-12 col-12">
                                        <div id="divChartDataCat"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-xl-6 col-md-6 col-12">

                        <div class="card card-statistics">
                            <div class="card-body statistics-body">
                                <div class="row">
                                    <div class="col-xl-12 col-md-12 col-12">
                                        <div id="divChartDataAssetsType"></div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <br />
        <div style="background-color: white; padding-top: 20px; padding-bottom: 20px; border-radius: 14px; box-shadow: 10px 10px 5px lightgray;display:none">
            <div class="col-xl-6 col-md-6 col-6" id="divOrgs" runat="server" style="padding-top: 20px;">
                <div class="row">
                    <div class="col-xl-12 col-md-12 col-12">
                        <div class="card card-statistics">
                            <div class="card-body statistics-body">
                                <div class="row">
                                    <div class="col-md-12 col-12">
                                        <div id="divChartDataEmpHaveAssets"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="col-xl-6 col-md-6 col-12">
                <div class="card card-statistics">
                    <div class="card-body statistics-body">
                        <div class="row">
                            <div class="col-md-12 col-12">
                                <div id="divChartDataEmp"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />

        <div class="col-xl-12 col-md-12 col-12" style="display:none">
            <div class="row">
                <div class="col-xl-12 col-md-12 col-12">
                    <div class="card card-statistics">
                        <div class="card-body statistics-body">
                            <div class="row">
                                <div class="col-md-12 col-12">
                                    <div id="divChartDataOrgQuarterAmount"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--/ Statistics Card -->
        <div class="swal2-container swal2-rtl swal2-center swal2-backdrop-show" style="overflow-y: auto; visibility: hidden" id="divPopup">
            <div aria-labelledby="swal2-title" aria-describedby="swal2-content" class="swal2-popup swal2-modal swal2-show" tabindex="-1" role="dialog" aria-live="assertive" aria-modal="true" style="display: flex;">
                <div class="swal2-header" style="margin-top: 20px;">

                    <asp:DataGrid runat="server" ID="grdData" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter" data-auto-responsive="false" OnItemDataBound="grdData_ItemDataBound">

                        <Columns>
                            <asp:BoundColumn DataField="Emp_Id" Visible="False"></asp:BoundColumn>

                            <asp:BoundColumn DataField="Emp_Name" HeaderText="الاسم"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="فعال">
                                <ItemTemplate>
                                    <%#ShowYesNo(getBool(Eval("Emp_Active")))%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <div class="row mbm">
                        <div class="col-lg-12">
                            <div class="pagination-panel">
                                &nbsp;
                                            <asp:Label ID="lblcount" runat="server"></asp:Label>

                            </div>
                        </div>
                    </div>



                    <asp:DataGrid runat="server" ID="grdDataAssets" AutoGenerateColumns="False" OnItemDataBound="grdDataAssets_ItemDataBound" Width="100%"
                        AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter" data-auto-responsive="false">

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


                            <%--  <asp:TemplateColumn HeaderText="">
                                <ItemStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <img style="cursor: pointer;" src="/layout/images/plus.gif" alt="" border="0" runat="server" id="imgControl" />
                                </ItemTemplate>
                            </asp:TemplateColumn>--%>
                            <asp:BoundColumn DataField="code" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Serial" HeaderText="<%$ Resources:pages,Serial %> "></asp:BoundColumn>
                            <asp:BoundColumn DataField="RequestRefCode" HeaderText="<%$ Resources:pages,RefCode %> "></asp:BoundColumn>
                            <asp:BoundColumn DataField="RequestDate" HeaderText=" <%$ Resources:pages,ReceiptDate %>  " DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CreatedAt" HeaderText=" <%$ Resources:pages,CreatedAt %>  " DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>

                            <asp:TemplateColumn HeaderText="<%$ Resources:pages,Event %>">
                                <ItemTemplate>
                                    <div class="text-info"><%#( ZeroIntergerIFNull(gets(Eval("RequestActionType")))==2?"<em class='icon ni ni-building  text-info'></em>&nbsp; <span class='badge badge-outline-info'>عهدة تنظيمية</span>"    :"<em class='icon ni ni-user-list  text-warning'></em> &nbsp; <span class='badge badge-outline-warning'>عهدة شخصية</span>")  %>   </div>
                                    <div class="text-info"><%#  ZeroIntergerIFNull(gets(Eval("EmpRefCode")))==0?""  : gets(Eval("EmpName")) %></div>
                                    <div class="text-indigo"><%# gets(Eval("LocationPath")) %></div>



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
                                                    <a href="AssetCheckout.aspx?t=<%#Eval("RequestActionType") %>&requestCode=<%#Eval("code") %>" class="btn btn-default btn-xs"><i class="icon ni ni-edit"></i>&nbsp; <%=GetGlobalResourceObject("pages","CustodyDetails") %> </a>
                                                </li>

                                                <li>
                                                    <a href="../Reports/AssetReceipt.aspx?docId=<%#Eval("code") %>" class="btn btn-default btn-xs iframe75"><i class="icon ni ni-printer"></i>&nbsp; <%=GetGlobalResourceObject("pages","PrintRequest") %> </a>
                                                </li>

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




                    <div class="row mbm">
                        <div class="col-lg-12">
                            <div class="pagination-panel">
                                &nbsp;
                                            <asp:Label ID="lblcountAssets" runat="server"></asp:Label>

                            </div>
                        </div>
                    </div>


                </div>
                <div style="float: left">
                    <button type="button" class="swal2-close" aria-label="Close this dialog" onclick="hideDiv()" style="">×</button>
                </div>
            </div>
        </div>

    </section>
    <script>

        function hideDiv() {
            document.getElementById("divPopup").style.visibility = "hidden";
        }
        function ViewDiv() {
            document.getElementById("divPopup").style.visibility = "visible";
        }
    </script>
    <script src="https://cdn.jsdelivr.net/npm/apexcharts"></script>

    <link href="/wwwroot/Charts/dx.common.css" rel="stylesheet" />
    <link href="/wwwroot/Charts/dx.dark.css" rel="stylesheet" />


</asp:Content>
