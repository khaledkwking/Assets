<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="StockTaking.aspx.cs" Inherits="UI.Web.Modules.WHM.Forms.StockTaking" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script>

         function ControlGrid(imgName, rowIndex, rowID) {
             //alert("CONTROL GRID");
             //alert(imgName);
             //alert(rowIndex);
             //alert(rowID);
             rowIndex = rowIndex + 3;

             var myrow = "";
             if (rowIndex < 10)
                 myrow = "ctl00_ContentPlaceHolder1_grdInboundItems_ctl0" + rowIndex;
             else
                 myrow = "ctl00_ContentPlaceHolder1_grdInboundItems_ctl" + rowIndex;
             var row = document.getElementById(myrow);
           //  alert("IMG NAME: "+imgName+" and ROW INDEX: "+rowIndex+" ID: "+rowID);
             //alert("MYROW: "+myrow+" AND VALUE FOUND: "+row);
             if (row.style.display == "") {
                 row.style.display = "none";
                 document.getElementById(imgName).src = plus.src;
             }
             else {
                 row.style.display = "";
                 document.getElementById(imgName).src = minus.src;
             }
         }
         function Checklist(obj, list) {
             //  alert("CHECK SYSTEM: "+obj.checked);
             if (list != "") {
                 var data = list.split(",");
                 //alert("LIST IS: "+data.length)
                 for (var i = 0; i < data.length; i++) {
                     document.getElementById(data[i]).checked = obj.checked;
                 }
             }
         }
     </script>
    <div id="title-breadcrumb-option-demo" class="page-title-breadcrumb">
        <div class="page-header pull-left">
            <div class="page-title">Stock Taking Report</div>
        </div>
        <ol class="breadcrumb page-breadcrumb pull-right">
            <li><i class="fa fa-home"></i>&nbsp;<a href="/admin/pages/home.aspx">Home</a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li><a href="#">Stock  </a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Stock Taking Report</li>
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
                        <div class="caption">
                            <asp:Label ID="lblSubTitle" runat="server" >Stock Items list </asp:Label> </div>
                        <div class="actions">

                             <a  runat="server" onclick="return ValidateInboundITems();"  href="/Modules/WHM/Reports/StocktakingReport.aspx" id="lnkReportPrint" class="btn btn-info btn-xs"  ><i class="fa fa-print"></i>&nbsp; Print Report&nbsp;</a>

                         
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="row mbm">

                            <div class="col-lg-12">
                                <div class="tb-group-actions">

                                    <span>Inbound Serial:</span>
                                    <asp:TextBox ID="txtPArtOfName" runat="server" class="table-group-action-select form-control input-inline" placeholder="IN\S.N\GCSYY"></asp:TextBox>
                                    &nbsp;

                                     <span>Consignee Ref:</span>
                                  <asp:TextBox ID="txtConsigneeRef" runat="server" class="table-group-action-select form-control input-inline" ></asp:TextBox>

                                    <asp:LinkButton runat="server" ID="btnFilter" class="btn btn-success dropdown-toggle" OnClick="btnFilter_Click"><i class="fa fa-search"></i>&nbsp;
                                                Filter</asp:LinkButton>
                                </div>
                            </div>
                        </div>


                         <div class="row">
          
            <div class="col-lg-12">
                <div class="portlet box portlet-blue" >
                    <div class="portlet-header" style="background:#000">
                        <div class="caption">
                           Advanced Search
                        </div>
                        <div class="tools"><i class="fa fa-chevron-up"></i> </div>
                    </div>
                    <div class="portlet-body">
                        <div role="form" class="form-horizontal">
                            <div class="row">


                                <div class="col-md-5">
                                       <div class="form-group">
                                        <label class="col-md-3 control-label" for="">Item Type</label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="lstItemType" runat="server" class="form-control" AutoPostBack="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                     

                                    <div class="form-group">
                                        <label class="col-md-3 control-label" for="">Consignee</label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="lstConsignee" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                 
 

                                       <div class="form-group">
                                        <label class="col-md-3 control-label" for="">Good Category</label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="lstGoodCategoryCode" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    

                                  

                                </div>

                                <div class="col-md-5">
                                       <div class="form-group">
                                        <label class="col-md-3 control-label" for="">Qty Unit </label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="lstQtyUnitCode" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                   

                                    <div class="form-group">
                                        <label class="col-md-3 control-label" for="">Weight Unit </label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="lstWeightUnitCode" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
    

                                    <div class="form-group">
                                        <label class="col-md-3 control-label" for="">Location </label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="lstLocationCode" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                     

                                </div>

                                 
                            </div>

                           


                           
                        </div>
                    </div>
            </div>
        </div>
    </div>




                        <asp:DataGrid ID="grdInboundItems" runat="server"
                            DataKeyField="code" ShowFooter="false"  AllowPaging="True" AutoGenerateColumns="false" PageSize="20" class="table table-hover table-striped table-bordered table-advanced tablesorter"
                            Width="100%" OnItemDataBound="grdInboundItems_ItemDataBound">
                            <SelectedItemStyle ForeColor="White" />
                            <ItemStyle CssClass="grdItem" />
                            <AlternatingItemStyle CssClass="grdItem" />
                            <PagerStyle Visible="false" />

                            <Columns>
                                <asp:ButtonColumn HeaderText="Del." Text="<img border=0 src='images/delete.gif' alt='Delete'>" CommandName="Delete" Visible="false">
                                    <HeaderStyle></HeaderStyle>
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:ButtonColumn>
                                <asp:TemplateColumn HeaderText="" Visible="false">
                                    <ItemStyle HorizontalAlign="left" />
                                    <ItemTemplate>
                                        <asp:Label Font-Bold="true" runat="server" ID="Label7" CssClass="black_Lable">
				                            Item Units :
                                        </asp:Label>
                                        <div>

                                            <asp:DataGrid ID="grdUnits" runat="server"
                                                class="table table-hover table-striped table-bordered table-advanced tablesorter"
                                                AutoGenerateColumns="False"
                                                BackColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Names="Tahoma"
                                                CellPadding="3" Width="100%"  >
                                                <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <ItemStyle CssClass="grdItem" />
                                                <AlternatingItemStyle CssClass="grdItem" />
                                                <HeaderStyle CssClass="grdHead" BackColor="Black" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                <FooterStyle CssClass="grdFoot" />
                                                <PagerStyle CssClass="grdPager" HorizontalAlign="center" Mode="NextPrev"
                                                    PrevPageText="&lt;&lt; Previous &nbsp;&nbsp;&nbsp;" NextPageText="&nbsp;&nbsp;&nbsp;Next&gt;&gt;" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="code" HeaderText="code" Visible="false">
                                                        <HeaderStyle Wrap="false" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="UnitRef" HeaderText="Ref">
                                                        <HeaderStyle Wrap="false" />
                                                    </asp:BoundColumn>
                                                   
                                                    <asp:BoundColumn DataField="ContainerSize" HeaderText="Size">
                                                        <HeaderStyle Wrap="false" />
                                                    </asp:BoundColumn>

                                                    <asp:TemplateColumn HeaderText="Type">
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle Wrap="False" HorizontalAlign="left" />

                                                        <ItemTemplate>
                                                            <%#Eval("D_VehicleType.TitleEn") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:TemplateColumn HeaderText="Brand">
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle Wrap="False" HorizontalAlign="left" />

                                                        <ItemTemplate>
                                                            <%#Eval("D_VehicleCategory.TitleEn") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:BoundColumn DataField="VModel" HeaderText="Model">
                                                        <HeaderStyle Wrap="false" />
                                                    </asp:BoundColumn>

                                                    <asp:BoundColumn DataField="VColor" HeaderText="Color">
                                                        <HeaderStyle Wrap="false" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Notes" HeaderText="Notes">
                                                        <HeaderStyle Wrap="false" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Status">
                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />

                                                        <ItemTemplate>
                                                            <%#ItemUnitisout(gets(Eval("ItemUnitStatus"))) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                    <asp:BoundColumn DataField="ItemUnitStatus" HeaderText="ItemUnitStatus" Visible="false">
                                                        <HeaderStyle Wrap="false" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn Visible="false">
                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                        <HeaderStyle Wrap="False" HorizontalAlign="Center" />
                                                        <%-- <HeaderTemplate>
                                        <input id="chkAllItems" class="checkall" style="border-style: none;" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkItem', this.checked)" />
                                    </HeaderTemplate>--%>
                                                        <ItemTemplate>
                                                            
                                                            <asp:CheckBox runat="server" ID="chkItem" CssClass="check" />
                                                          
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>

                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="" Visible="false">
                                    <ItemStyle HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <img style="cursor: pointer;" src="/layout/images/plus.gif" alt="" border="0" runat="server" id="imgControl" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn Visible="false" HeaderText="Code" DataField="code"></asp:BoundColumn>
                                 <asp:BoundColumn Visible="false" HeaderText="ItemType" DataField="ItemType"></asp:BoundColumn>

                                  <asp:BoundColumn Visible="false" HeaderText="QtyUnitCode" DataField="QtyUnitCode"></asp:BoundColumn>
                                  <asp:BoundColumn Visible="false" HeaderText="WeightUnitCode" DataField="WeightUnitCode"></asp:BoundColumn>
                                  <asp:BoundColumn Visible="false" HeaderText="CurrencyCode" DataField="CurrencyCode"></asp:BoundColumn>
                                  <asp:BoundColumn Visible="false" HeaderText="NetWeight" DataField="NetWeight"></asp:BoundColumn>
                                  <asp:BoundColumn Visible="false" HeaderText="EstimatedAmount" DataField="EstimatedAmount"></asp:BoundColumn>
                                    <asp:BoundColumn Visible="false" HeaderText="GrossWeight" DataField="GrossWeight"></asp:BoundColumn>

                                   <asp:BoundColumn DataField="Serial" HeaderText="Request#"></asp:BoundColumn>

                               <asp:BoundColumn DataField="ItemCategoryNameEn" HeaderText="Category"></asp:BoundColumn>
                                 <asp:BoundColumn DataField="ItemCategoryTypeNameEn" HeaderText="Type"></asp:BoundColumn>
                                <asp:BoundColumn DataField="GoodDescirption" HeaderText="Descirption">
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="Qty" Visible="false">
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle Wrap="False" HorizontalAlign="left" />

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty" Text='0' runat="server" class="form-control" Width="50px" Height="25px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="QtyBalance" HeaderText="Balance">
                                    <HeaderStyle Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="QUnitNameEn" HeaderText="Q. Unit">
                                    <HeaderStyle Wrap="false" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="WUnitEn" HeaderText="W. Unit"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NetWeight" HeaderText="NetWeight"></asp:BoundColumn>
                                <asp:BoundColumn DataField="LocationEn" HeaderText="Location"></asp:BoundColumn>
                                <asp:BoundColumn DataField="LocationNo" HeaderText="Location Ref#"></asp:BoundColumn>
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

                                    <cc1:Pager CurrentIndex="1" OnCommand="pager_Command" ShowFirstLast="true" ID="pager1"
                                        runat="server" Width="100%" PageSize="20"></cc1:Pager>
                                    &nbsp;
                                            records |<asp:Label ID="lblInboundItemsCount" runat="server"></asp:Label>

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
