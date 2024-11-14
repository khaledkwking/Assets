<%@ Page Title="" Language="C#" ClientIDMode="AutoID" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="PermissionsNew.aspx.cs" Inherits="UI.Web.Admin.Pages.PermissionsNew" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="<%= GetGlobalResourceObject("Utilities", "Assetspath")%>Assets/Basic/Basic.js"></script>
    <script language="javascript" type="text/javascript">
        <%=ViewState["Def"].ToString() %>
    </script>
    <script language="javascript" type="text/javascript">
        function ToggleSystemGroup(imgid, rowlist) {
         //   alert();

            var img = document.getElementById(imgid);
            if (img.src.indexOf("plus") != -1) // the group is hidden, show it
            {


                var data = rowlist.split(",");
                for (var i = 0; i < data.length; i++) {
                    document.getElementById(data[i]).style.display = "";
                }
                img.src = "<%= GetGlobalResourceObject("Utilities", "resourcespath")%>images/minus.gif"
            }
            else // the group is shown, hide it
            {
                // alert("test");
                img.src = "<%= GetGlobalResourceObject("Utilities", "resourcespath")%>images/plus.gif"
                var data = rowlist.split(",");
                for (var i = 0; i < data.length; i++) {
                    document.getElementById(data[i]).style.display = "none";
                }
            }
        }
        function CheckSystem(obj, list) {
            //  alert("CHECK SYSTEM: "+obj.checked);
            if (list != "") {
                var data = list.split(",");
                //alert("LIST IS: "+data.length)
                for (var i = 0; i < data.length; i++) {
                    document.getElementById(data[i]).checked = obj.checked;
                }
            }
        }
        function KeepCheck(parent, list) {
            var check = true;
            if (list != "") {
                var data = list.split(",");
                for (var i = 0; i < data.length; i++) {
                    if (!document.getElementById(data[i]).checked) {
                        check = false;
                        break;
                    }
                }
            }
            document.getElementById(parent).checked = check;
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
                                                <asp:LinkButton OnClientClick="return checkDelete();" runat="server" ID="btnClear" class="btn btn-danger btn-xs" OnClick="lbkColse_Click"><i class="icon ni ni-trash"></i>&nbsp;Clear Permission</asp:LinkButton>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <asp:LinkButton runat="server" ID="btnSave" class="btn btn-info btn-xs" OnClick="lnkSave_Click"><i class="icon ni ni-save""></i>&nbsp; Save changes&nbsp;</asp:LinkButton>
                            </li>

                        </ul>
                    </div>
                </div>
                <!-- .toggle-wrap -->
            </div>
            <!-- .nk-block-head-content -->
        </div>
        <!-- .nk-block-between -->
    </div>

    <div class="nk-block">

        <div class="card card-bordered" id="tblshow" runat="server">
            <div class="card-header border-bottom"><%=GetGlobalResourceObject("pages","DataListing") %></div>
            <div class="card-inner">
                <div class="card-inner p-0">
                    <div class="portlet box">
                        <asp:Label ID="lblError" runat="server"></asp:Label>
                        <div class="portlet-body">

                            <div class="row mbm">

                                <div class="col-lg-9">
                                    <div class="tb-group-actions">

                                        <span>  الوظيفة :</span>
                                        <asp:DropDownList runat="server"  Width="200px" ID="LstFilterType" class="table-group-action-select form-control input-inline" AutoPostBack="True" OnSelectedIndexChanged="LstFilterType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        &nbsp;


                                        <span>المستخدم :</span>
                                        <asp:DropDownList runat="server" ID="lstEmployee"  Width="200px" class="table-group-action-select form-control input-inline" OnSelectedIndexChanged="lstEmployee_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        &nbsp;
                                            
                                           

                                        <asp:LinkButton runat="server" ID="btnFilter" Width="200px" class="btn btn-wider btn-primary" style="margin-top:10px;"><em class="icon ni ni-arrow-right"></em>&nbsp;بحث</asp:LinkButton>
                                    </div>
                                </div>
                            </div>


                            <asp:DataGrid runat="server" ID="grdResult" AutoGenerateColumns="False"
                                AllowPaging="false" class="table table-hover table-striped table-bordered table-advanced tablesorter" OnItemDataBound="grdResult_ItemDataBound">
                                <PagerStyle Visible="False" />
                                <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                                <Columns>
                                    <asp:BoundColumn DataField="SystemID" HeaderText="SystemID" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="PageID" HeaderText="PageID" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="PermissionID" HeaderText="PermissionID" Visible="False"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="System Title">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <ItemTemplate>
                                            <table border="0">
                                                <tr>
                                                    <td style="border-style: none;">
                                                        <img alt="" runat="server" id="imgSystem" src='/Layout/images/plus.gif' style="cursor: pointer;" />
                                                    </td>
                                                    <td style="border-style: none;" class="gg1">
                                                        <a href="javascript:void(0);" style="color: #999; text-decoration: none;" runat="server" id="lnkSystem"><%#Eval("SystemTitle")%></a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="PageTitle" HeaderText="Page Title">
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Show">
                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox Style="border-style: none;" runat="server" ID="chkShow" Checked='<%#getBool(DataBinder.Eval(Container.DataItem,"show")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Insert">
                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox Style="border-style: none;" runat="server" ID="chkAdd" Checked='<%#getBool(DataBinder.Eval(Container.DataItem,"AddRecord")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Edit">
                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox Style="border-style: none;" runat="server" ID="chkModify" Checked='<%#getBool(DataBinder.Eval(Container.DataItem,"modify")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Delete">
                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox Style="border-style: none;" runat="server" ID="chkDelete" Checked='<%#getBool(DataBinder.Eval(Container.DataItem,"DeleteRecord")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Date" Visible="false">
                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox Style="border-style: none;" runat="server" ID="chkDate" Checked='<%#getBool(DataBinder.Eval(Container.DataItem,"DateControl")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" runat="server" id="txtChange" />
</asp:Content>
