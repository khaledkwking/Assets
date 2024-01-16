<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/MainEmpty.Master" AutoEventWireup="true" CodeBehind="InboundItemsUnits.aspx.cs" Inherits="UI.Web.Modules.StoreOperations.Forms.Inboud.InboundItemsUnits" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>


 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="JavaScript" type="text/javascript">
        function chkImage() {
           
            return true;
        }
    </script>


    <div id="title-breadcrumb-option-demo" class="page-title-breadcrumb">
        <div class="page-header pull-left">
            <div class="page-title"><%=_PageTitle %></div>
        </div>
        <ol class="breadcrumb page-breadcrumb pull-right">
            <li><i class="fa fa-home"></i>&nbsp;<a href="/admin/pages/home.aspx">Home</a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li><a href="InboundList.aspx">Inbound Operations  </a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active"><%=_PageTitle %></li>
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
            <div class="col-lg-12">
                <div class="portlet box portlet-blue" id="tblAdd" runat="server">
                    <div class="portlet-header">
                        <div class="caption">
                            <asp:Label runat="server" ID="lblSubTitle">Add New Item</asp:Label>
                        </div>
                        <div class="tools"><i class="fa fa-chevron-up"></i> </div>
                    </div>
                    <div class="portlet-body">
                        <div role="form" class="form-body pal">
                            <div class="row">


                                <div class="col-md-3">
                                       <div class="form-group">
                                        <label class="control-label" for="">Item Type</label>
                                      
                                            <asp:DropDownList ID="lstItemType" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="lstItemType_SelectedIndexChanged"  ></asp:DropDownList>
                                        
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label" for="">Shippment Receipt No</label>

                                        
                                            <asp:TextBox runat="server" ID="txtShippmentReceiptNo" placeholder=" \ \ " class="form-control"></asp:TextBox>
                                        
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label" for="">Shippment Receipt Date</label>

                                       

                                            <div class="input-group datetimepicker-default date">
                                                <asp:TextBox runat="server" ID="txtShippmentReceiptDate" placeholder="__/__/____" class="form-control"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>

                                        
                                    </div>

                                     <div class="form-group">
                                                                        <label class="control-label" for="">Bar Code</label>
          
                                                                            <asp:TextBox runat="server" ID="txtbarcode"  class="form-control"></asp:TextBox>
                                         
                                                                    </div>

                                    <div class="form-group">
                                        <label class="control-label" for="">Consignee</label>
                                      
                                            <asp:DropDownList ID="lstConsignee" runat="server" class="form-control"></asp:DropDownList>
                                         
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label" for="">Alert Party</label>
 
                                            <asp:TextBox runat="server" ID="txtAlertParty" class="form-control"></asp:TextBox>
                                         
                                    </div>

                                       <div class="form-group">
                                        <label class="control-label" for="">Good Category</label>
                                       
                                            <asp:DropDownList ID="lstGoodCategoryCode" runat="server" class="form-control"></asp:DropDownList>
                                        
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label" for="">Considerations</label>

                                        
                                            <asp:TextBox runat="server" ID="txtConsiderations" class="form-control"></asp:TextBox>
                                        
                                    </div>

                                   

                                </div>

                                <div class="col-md-3">
                                       <div class="form-group">
                                        <label class="control-label" for="">Qty Unit </label>
                                            <asp:DropDownList ID="lstQtyUnitCode" runat="server" class="form-control"></asp:DropDownList>
                                         
                                             </div>

                                    <div class="form-group">
                                        <label class="control-label" for="">Qty</label>

                                       
                                            <asp:TextBox runat="server" ID="txtQty"  AutoPostBack="true" Text="1"  class="form-control" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                        
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label" for="">Unit Code</label>
                                   
                                            <asp:DropDownList ID="lstWeightUnitCode" runat="server" class="form-control"></asp:DropDownList>
                                       
                                    </div>

                                    <div class="form-group">
                                        <label class="control-label" for="">Net Weight</label>

                                     
                                            <asp:TextBox runat="server" ID="txtNetWeight" class="form-control"></asp:TextBox>
                                       
                                    </div>
                                    <div class="form-group" style="display:none">
                                        <label class="control-label" for="">Net Weight (ActualReceived)</label>

                                      
                                            <asp:TextBox runat="server" ID="txtNetWeightActualReceived" class="form-control"></asp:TextBox>
                                       
                                    </div>


                                    <div class="form-group">
                                        <label class="control-label" for="">Gross Weight</label>

                                       
                                            <asp:TextBox runat="server" ID="txtGrossWeight" class="form-control"></asp:TextBox>
                                        
                                    </div>

                                    <div class="form-group" style="display:none">
                                        <label class="control-label" for="">Gross Weight (ActualReceived)</label>

                                     
                                            <asp:TextBox runat="server" ID="txtGrossWeightActualReceived" class="form-control"></asp:TextBox>
                                       
                                    </div>
                                      
                                       <div class="form-group" style="display:none">
                                        <label class="control-label" for="">Qty Actual Received</label>

                                      
                                            <asp:TextBox runat="server" ID="txtQtyActualReceived" class="form-control"></asp:TextBox>
                                       
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label" for="">Estimated Amount</label>

                                      
                                            <asp:TextBox runat="server" ID="txtEstimatedAmount" class="form-control"></asp:TextBox>
                                     
                                    </div>

                                              
                                    <div class="form-group">
                                        <label class="control-label" for="">Currency </label>
                                      
                                            <asp:DropDownList ID="lstCurrency" runat="server" class="form-control"></asp:DropDownList>
                                       
                                    </div>

                                    <div class="form-group" style="display:none">
                                        <label class="col-md-3 control-label" for="">Location </label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="lstLocationCode" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group" style="display:none">
                                        <label class="col-md-3 control-label" for="">Location No</label>

                                        <div class="col-md-9">
                                            <asp:TextBox runat="server" ID="txtLocationNo" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                        <label class="control-label" for="">Descirption</label>

                                     
                                            <asp:TextBox runat="server" ID="txtGoodDescirption" class="form-control"></asp:TextBox>
                                       
                                    </div>

                                </div>

                                <div class="col-md-6">
                                    Items Units
                                    <br />
                                    <asp:DataGrid runat="server" ID="grdItemUnits" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="20" class="table table-hover table-striped table-bordered table-advanced tablesorter" OnItemDataBound="grdItemUnits_ItemDataBound"   >
                                        <PagerStyle Visible="False" />
                                        <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                                        <Columns>
                                                <asp:BoundColumn DataField="Code" Visible="false"> </asp:BoundColumn>
                                            <asp:BoundColumn DataField="UnitRef" Visible="false"> </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ContainerSize" Visible="false"> </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ContainerType" Visible="false"> </asp:BoundColumn>
                                            <asp:BoundColumn DataField="VTypeCode" Visible="false"> </asp:BoundColumn>
                                            <asp:BoundColumn DataField="VCategoryCode" Visible="false"> </asp:BoundColumn>
                                             <asp:BoundColumn DataField="VModel" Visible="false"> </asp:BoundColumn>
                                             <asp:BoundColumn DataField="VColor" Visible="false"> </asp:BoundColumn>


                                            <asp:TemplateColumn HeaderText="Ref">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtRef" Text='<%#Eval("UnitRef") %>' class="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="lstUnitType"   runat="server" class="form-control"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Brand">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="lstUnitBrand" runat="server" class="form-control"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                              <asp:TemplateColumn HeaderText="Model">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="lstUnitmodel" runat="server" class="form-control"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>   
                                            <asp:TemplateColumn HeaderText="Color">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="lstUnitColor" runat="server" class="form-control"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>


                                             <asp:TemplateColumn HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtContainerType" Text='<%#Eval("ContainerType") %>' class="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Size">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtContainersize" Text='<%#Eval("ContainerSize") %>' class="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn HeaderText="Note">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtUnitNote" Text='<%#Eval("Notes") %>' class="form-control"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>


                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="col-md-1 control-label" for="">Notes </label>
                                        <div class="col-md-11">
                                            <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="col-md-1 control-label" for="">Goods Notes </label>
                                        <div class="col-md-11">
                                            <asp:TextBox runat="server" ID="txtGoodNotes" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-actions">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:LinkButton ID="btnSave" runat="server" class="btn btn-primary" OnClick="btnSave_Click"><i class='fa fa-save'></i>&nbsp; Save Item Information </asp:LinkButton>

                                            &nbsp;
				                               <asp:Button runat="server" ID="btnCancel" class="btn btn-default" Text=" Cancel " OnClick="btnCancel_Click" />

                                        </div>
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
