<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderOperations.aspx.cs" Inherits="UI.Web.Modules.WHM.Forms.Purchase.PurchaseOrderOperations" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
  <%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="asp" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="hdnMasterID" runat="server" type="hidden" />
     <script language="JavaScript" type="text/javascript"> 
         function chkImage() {

             var txt = document.getElementById("<%=txtinboundNum.ClientID %>")
             if (txt.value == "") {
                 new $.Zebra_Dialog("Please, Enter document Serial #");
                 return false;
             }

             var txt = document.getElementById("<%=txtTransDate.ClientID %>")
             if (txt.value == "") {
                 new $.Zebra_Dialog("Please, Enter  Transaction Date");
                 return false;
             }


             var txt = document.getElementById("<%=txtRefNo.ClientID %>")
             if (txt.value == "") {
                 new $.Zebra_Dialog("Please, Enter Reference No ");
                 return false;
             }

             var txt = document.getElementById("<%=txtRefDate.ClientID %>")
             if (txt.value == "") {
                 new $.Zebra_Dialog("Please, Enter  Reference Date ");
                 return false;
             }
            <%-- var txt = document.getElementById("<%=txtManifestNo.ClientID %>")
             if (txt.value == "") {
                 new $.Zebra_Dialog("Please, Enter inbound Manifest No ");
                 return false;
             }


             var txt = document.getElementById("<%=txtManifestDate.ClientID %>")
             if (txt.value == "") {
                 new $.Zebra_Dialog("Please, Enter inbound Manifest Date ");
                 return false;
             }--%>

          <%--   var txt = document.getElementById("<%=txtDeliveryOrder.ClientID %>")
             if (txt.value == "") {
                 new $.Zebra_Dialog("Please, Enter inbound Delivery Order ");
                 return false;
             }


             var txt = document.getElementById("<%=txtDeliveryDate.ClientID %>")
             if (txt.value == "") {
                 new $.Zebra_Dialog("Please, Enter inbound Delivery Date ");
                 return false;
             }--%>


             return true;
         }


     

         function ValidatePurchaseItem()
        {
               var txt = document.getElementById("<%=hdnMasterID.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                new $.Zebra_Dialog("Please, Save  Master Data");
                return false;
             }  

             var txt = document.getElementById("<%=lstGoodCategoryCode.ClientID %>")
             if (txt.value == "" || txt.value == "0") {
                 new $.Zebra_Dialog("Please, Select Category");
                 return false;
             }  

             var txt = document.getElementById("<%=lstGoodCategoryTypeCode.ClientID %>")
             if (txt.value == "" || txt.value == "0") {
                 new $.Zebra_Dialog("Please, Select Product");
                 return false;
             }  

             var txt = document.getElementById("<%=lstPackingUnit.ClientID %>")
             if (txt.value == "" || txt.value == "0") {
                 new $.Zebra_Dialog("Please, Select Packing Unit");
                 return false;
             }  

             var txt = document.getElementById("<%=txtPacking.ClientID %>")
             if (txt.value == "" || txt.value == "0") {
                 new $.Zebra_Dialog("Please, Enter packing");
                 return false;
             }  

             var txt = document.getElementById("<%=txtPackingQty.ClientID %>")
             if (txt.value == "" || txt.value == "0") {
                 new $.Zebra_Dialog("Please, Enter Qty");
                 return false;
             }  

            return true;
        }
        

    </script>


    <div id="title-breadcrumb-option-demo" class="page-title-breadcrumb">
        <div class="page-header pull-left">
            <div class="page-title">  <%=GetGlobalResourceObject("pages","NewPurchaseOrder") %></div>
        </div>
        <ol class="breadcrumb page-breadcrumb pull-right">
            <li><i class="fa fa-home"></i>&nbsp;<a href="/admin/pages/home.aspx"> <%=GetGlobalResourceObject("pages","home") %></a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
             
            <li class="active"> <%=GetGlobalResourceObject("pages","NewPurchaseOrder") %></li>
        </ol>
        <div class="clearfix"></div>
    </div>


    <asp:UpdatePanel runat="server" ID="Updatepanel1" ChildrenAsTriggers="true" UpdateMode="conditional">
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
                            <asp:Label runat="server" ID="lblSubTitle"><%=GetGlobalResourceObject("pages","PurchaseMasterData") %></asp:Label>
                        </div>
                        <div class="tools"><i class="fa fa-chevron-down"> <%=GetGlobalResourceObject("pages","PurchaseMasterData") %></i></div>
                    </div>
                    <div class="portlet-body" style='<%=setmaterstyle()%>'>
                        <div role="form">
                           
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label" for=""> <%=GetGlobalResourceObject("pages","Serial") %></label>
                                            <asp:TextBox runat="server" ID="txtinboundNum" placeholder="IN\S.N\GCSYY" class="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <label class="control-label" for=""><%=GetGlobalResourceObject("pages","InboundType") %></label>

                                            <asp:DropDownList ID="lstInboundType" runat="server" class="form-control"></asp:DropDownList>

                                        </div>
                                        <div class="form-group">
                                            <label class="control-label" for=""><%=GetGlobalResourceObject("pages","DepositType") %></label>

                                            <asp:DropDownList ID="lstDepositType" runat="server" class="form-control"></asp:DropDownList>

                                        </div>

                                        


                                    </div>

                                    

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="control-label" for="">  <%=GetGlobalResourceObject("pages","RequestDate") %></label>



                                            <div class="input-group datetimepicker-disable-time date">
                                                <asp:TextBox runat="server" ID="txtTransDate" placeholder="__/__/____" class="form-control"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>


                                        </div>
                                        
                                        <div class="form-group" style="display:none">
                                            <label class="control-label" for="">Customs Department</label>

                                            <asp:DropDownList ID="lstcustomsDepartment" runat="server" class="form-control"></asp:DropDownList>

                                        </div>

                                         <div class="form-group">
                                            <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Customer") %>  </label>

                                            <asp:DropDownList ID="lstConsignee" AutoPostBack="False"   runat="server" class="form-control select2"></asp:DropDownList>

                                        </div>
<div class="form-group">
                                            <label class="control-label" for=""><%=GetGlobalResourceObject("pages","suppliers") %>  </label>

                                            <asp:DropDownList ID="lstSupplier" runat="server" class="form-control"></asp:DropDownList>

                                        </div>
                                    </div>

                                    <div class="col-md-4">

                                        
                                        


                                        <div class="form-group">
                                            <label class="control-label" for=""><%=GetGlobalResourceObject("pages","ReferenceType") %></label>

                                            <asp:DropDownList ID="lstReferanceType" runat="server" class="form-control"></asp:DropDownList>

                                        </div>

                                        <div class="form-group">
                                            <label class="control-label" for=""> <%=GetGlobalResourceObject("pages","ReferenceNo") %></label>
                                            <asp:TextBox runat="server" ID="txtRefNo" class="form-control"></asp:TextBox>

                                        </div>

                                        <div class="form-group">
                                            <label class="control-label" for=""><%=GetGlobalResourceObject("pages","ReferenceDate") %></label>



                                            <div class="input-group datetimepicker-disable-time date">
                                                <asp:TextBox runat="server" ID="txtRefDate" placeholder="__/__/____" class="form-control"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>

                                        </div>

                                        <div class="form-group" style="display:none">
                                            <label class="control-label" for="">Manifest No</label>
                                            <asp:TextBox runat="server" ID="txtManifestNo" class="form-control"></asp:TextBox>

                                        </div>

                                        <div class="form-group" style="display:none">
                                            <label class="control-label" for="">Manifest Date</label>



                                            <div class="input-group datetimepicker-disable-time date">
                                                <asp:TextBox runat="server" ID="txtManifestDate" placeholder="__/__/____" class="form-control"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>


                                        </div>

                                    </div>

                                    <div class="col-md-4" style="display:none">
                                        <div class="form-group">
                                            <label class="control-label" for="">Delivery Order</label>


                                            <asp:TextBox runat="server" ID="txtDeliveryOrder" class="form-control"></asp:TextBox>

                                        </div>
                                        <div class="form-group">
                                            <label class="control-label" for="">Delivery Date</label>


                                            <div class="input-group datetimepicker-disable-time date">
                                                <asp:TextBox runat="server" ID="txtDeliveryDate" placeholder="__/__/____" class="form-control"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>


                                        </div>

                                        <div class="form-group">
                                            <label class="control-label" for="">Deposit Declaration Type</label>

                                            <asp:DropDownList ID="lstDepositDeclarationType" runat="server" class="form-control"></asp:DropDownList>

                                        </div>

                                        <div class="form-group">
                                            <label class="control-label" for="">Deposit Declaration No</label>


                                            <asp:TextBox runat="server" ID="txtDepositDeclarationNo" class="form-control"></asp:TextBox>

                                        </div>
                                        <div class="form-group">
                                            <label class="control-label" for="">Deposit Declaration Date</label>



                                            <div class="input-group datetimepicker-disable-time date">
                                                <asp:TextBox runat="server" ID="txtDepositDeclarationDate" placeholder="__/__/____" class="form-control"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            </div>


                                        </div>

                                    </div>
                                </div>

                                <div class="row">
                                     <div class="form-body pal">
                                    <div class="col-md-12">

                                        <div role="form" class="form-horizontal">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Notes") %> </label>

                                                        <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" class="form-control"></asp:TextBox>

                                                    </div>
                                                      </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="row" style="display:none">
                                      <div class="form-body pal">
                                    <div class="col-md-12">

                                        <div role="form" class="form-horizontal">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label class="control-label" for="">Deposit Notes </label>

                                                        <asp:TextBox runat="server" ID="txtDepositNote" TextMode="MultiLine" class="form-control"></asp:TextBox>

                                                    </div>

                                                </div>
                                            </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-actions">
                                            <div class="col-md-offset-3 col-md-3 pull-right">
                                                <asp:LinkButton ID="btnSave" runat="server" class="btn btn-success" OnClick="btnSave_Click"><i class='fa fa-save'></i>&nbsp; <%=GetGlobalResourceObject("pages","Submit") %> </asp:LinkButton>

                                                &nbsp;
                                                <asp:LinkButton ID="btnSave2" Visible="false"  runat="server" class="btn btn-success" OnClick="btnSave2_Click"><i class='fa fa-save'></i>&nbsp; <%=GetGlobalResourceObject("pages","Submit") %> & Add Items </asp:LinkButton>

                                                &nbsp;
				                               <asp:Button runat="server" ID="btnCancel" class="btn btn-default" Text="<%$ Resources:Pages, Cancel%>" OnClick="btnCancel_Click" />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="row" id="tblshow" runat="server" visible="false">
            <div class="col-lg-12">
                <div class="portlet box">
                    
                    <div class="portlet-body" style="min-height:600px">
                        <div class="col-lg-12">
                            <h3>  <%=GetGlobalResourceObject("pages","PurchaseRequestDetail") %></h3>
                            <ul class="nav nav-tabs" id="myTab">
                                   <li class="active"><a data-toggle="tab" href="#Items">  <%=GetGlobalResourceObject("pages","InboundItems") %></a></li>
                                 
                                <li class=""><a data-toggle="tab" href="#Attachments"><%=GetGlobalResourceObject("pages","Attachments") %></a></li>
                                   
                                </ul>  
                            <div class="tab-content" id="myTabContent">
                                <div class="tab-pane fade active in" id="Items">

                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>

                                            <div class="row">

                                                <div class="col-lg-12">
                                                    <div class="portlet box portlet-blue" id="divinboundItemsAdd" runat="server" visible="false">
                                                        
                                                        <div class="portlet-body">
                                                            <div role="form" class="form-body pal">
                                                                <div class="row">


                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="display:none">
                                                                            <label class="control-label" for=""><%=GetGlobalResourceObject("pages","ItemType") %></label>

                                                                            <asp:DropDownList ID="lstItemType" runat="server" class="form-control" AutoPostBack="false"></asp:DropDownList>

                                                                        </div>

                                                                        <div class="form-group">
                                                                            <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Category") %></label>

                                                                            <asp:DropDownList ID="lstGoodCategoryCode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstGoodCategoryCode_SelectedIndexChanged" class="form-control"></asp:DropDownList>

                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Type") %></label>

                                                                            <asp:DropDownList ID="lstGoodCategoryTypeCode" runat="server" class="form-control"></asp:DropDownList>

                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label" for=""><%=GetGlobalResourceObject("pages","PackingUnit") %></label>
                                                                            <asp:DropDownList ID="lstPackingUnit" runat="server" class="form-control"></asp:DropDownList>

                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="control-label" for=""><%=GetGlobalResourceObject("pages","packing") %></label>

                                                                            <asp:TextBox runat="server" ID="txtPacking" AutoPostBack="true"  OnTextChanged="txtPacking_TextChanged" class="form-control"></asp:TextBox>

                                                                        </div>

                                                                          <div class="form-group">
                                                                            <label class="control-label"    for=""><%=GetGlobalResourceObject("pages","Qty") %></label>

                                                                            <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtPackingQty_TextChanged" Text="0" ID="txtPackingQty" class="form-control"></asp:TextBox>

                                                                        </div>


                                                                        <div class="form-group" style="display: none">
                                                                            <label class="control-label" for="">Shipment Receipt No</label>


                                                                            <asp:TextBox runat="server" ID="txtShippmentReceiptNo" placeholder=" \ \ " class="form-control"></asp:TextBox>

                                                                        </div>

                                                                        <div class="form-group" style="display: none">
                                                                            <label class="control-label" for="">Shipment Receipt Date</label>



                                                                            <div class="input-group datetimepicker-default date">
                                                                                <asp:TextBox runat="server" ID="txtShippmentReceiptDate" placeholder="__/__/____" class="form-control"></asp:TextBox>
                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            </div>


                                                                        </div>

                                                                       




                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <div class="col-md-6">
                                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Qty") %></label>
                                                                                <asp:TextBox runat="server" ID="txtQty" ReadOnly="true" Text="1" class="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-6">
                                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","QUnit") %> </label>
                                                                                <asp:DropDownList ID="lstQtyUnitCode" runat="server" class="form-control"></asp:DropDownList>
                                                                            </div>
                                                                            
                                                                        </div>



                                                                        <div class="form-group"  >
                                                                               <div class="col-md-6">
                                                                                 <label class="control-label" for=""><%=GetGlobalResourceObject("pages","NetWeight") %></label>
                                                                            <asp:TextBox runat="server" ID="txtNetWeight" class="form-control"></asp:TextBox>
                                                                             </div>
                                                                             <div class="col-md-6">
                                                                            <label class="control-label" for=""><%=GetGlobalResourceObject("pages","W.Unit") %></label>

                                                                            <asp:DropDownList ID="lstWeightUnitCode" runat="server" class="form-control"></asp:DropDownList>
                                                                                 </div>
                                                                          
                                                                        </div>

                                                                         
                                                                        <div class="form-group" style="display: none">
                                                                            <label class="control-label" for="">Net Weight (ActualReceived)</label>


                                                                            <asp:TextBox runat="server" ID="txtNetWeightActualReceived" class="form-control"></asp:TextBox>

                                                                        </div>


                                                                        <div class="form-group" style="display: none">
                                                                            <label class="control-label" for="">Gross Weight</label>


                                                                            <asp:TextBox runat="server" ID="txtGrossWeight" class="form-control"></asp:TextBox>

                                                                        </div>

                                                                        <div class="form-group" style="display: none">
                                                                            <label class="control-label" for="">Gross Weight (ActualReceived)</label>


                                                                            <asp:TextBox runat="server" ID="txtGrossWeightActualReceived" class="form-control"></asp:TextBox>

                                                                        </div>

                                                                        <div class="form-group" style="display: none">
                                                                            <label class="control-label" for="">Qty Actual Received</label>


                                                                            <asp:TextBox runat="server" ID="txtQtyActualReceived" class="form-control"></asp:TextBox>

                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="col-md-6" style="padding-top: 20px; padding-bottom: 10px">
                                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Estimated") %></label>
                                                                                <asp:TextBox runat="server" ID="txtEstimatedAmount" class="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-6" style="padding-top: 20px; padding-bottom: 10px">
                                                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Currency") %> </label>

                                                                                <asp:DropDownList ID="lstCurrency" runat="server" class="form-control"></asp:DropDownList>
                                                                            </div>
                                                                        </div>


                                                                        <div class="form-group" style="display:none">
                                                                            <label class="control-label" for="">Notify Party</label>

                                                                            <asp:TextBox runat="server" ID="txtAlertParty" class="form-control"></asp:TextBox>

                                                                        </div>
                                                                        <div class="form-group" style="display:none">
                                                                            <label class="control-label" for=""><%=GetGlobalResourceObject("pages","ExpiryDate") %></label>



                                                                            <div class="input-group datetimepicker-disable-time date">
                                                                                <asp:TextBox runat="server" ID="txtexpireyDate" placeholder="__/__/____" class="form-control"></asp:TextBox>
                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            </div>


                                                                        </div>
                                                                        <div class="form-group" style="display:none">
                                                                            <label class="control-label" for="">Considerations</label>


                                                                            <asp:TextBox runat="server" ID="txtConsiderations" class="form-control"></asp:TextBox>

                                                                        </div>


                                                                        <div class="form-group" style="display: none">
                                                                            <label class="col-md-3 control-label" for="">Location </label>
                                                                            <div class="col-md-9">
                                                                                <asp:DropDownList ID="lstLocationCode" runat="server" class="form-control"></asp:DropDownList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" style="display: none">
                                                                            <label class="col-md-3 control-label" for="">Location No</label>

                                                                            <div class="col-md-9">
                                                                                <asp:TextBox runat="server" ID="txtLocationNo" class="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>


                                                                    </div>


                                                                </div>

                                                                <div class="row">

                                                                    <div class="col-md-12">
                                                                        <div class="form-group">
                                                                            <label class="col-md-1 control-label" for=""><%=GetGlobalResourceObject("pages","GoodsNotes") %> </label>
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
                                                                                <asp:LinkButton ID="lnkSaveItems" OnClientClick="return ValidatePurchaseItem()" runat="server" class="btn btn-primary" OnClick="lnkSaveItems_Click"><i class='fa fa-save'></i>&nbsp; <%= GetGlobalResourceObject("pages","Submit") %> </asp:LinkButton>

                                                                                &nbsp;
				                                                        <asp:Button runat="server" ID="lnkCancelItem" class="btn btn-default" Text=" <%$ Resources: Pages, Cancel %> " OnClick="lnkCancelItem_Click" />

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
                                                        <div class="portlet-header">
                                                            <div class="caption"><%=GetGlobalResourceObject("pages","DataListing") %></div>
                                                            <div class="actions">

                                                                <asp:LinkButton runat="server" OnClientClick="return ValidateInboundITems();" ID="lnkaddnewItem" class="btn btn-info btn-xs" OnClick="lnkaddnewItem_Click"><i class="fa fa-plus"></i>&nbsp; <%=GetGlobalResourceObject("pages","AddNewRecord") %>&nbsp;</asp:LinkButton>

                                                                <asp:LinkButton OnClientClick="return checkDelete();" runat="server" ID="btnDelete" class="btn btn-danger btn-xs" OnClick="btnDelete_Click"><i class="fa fa-times"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %></asp:LinkButton>
                                                                <asp:LinkButton Visible="false" runat="server" ID="lnkRefresh" class="btn btn-info btn-xs" OnClick="lnkRefresh_Click"><i class="fa fa-refresh"></i>&nbsp;Refresh</asp:LinkButton>

                                                            </div>
                                                        </div>
                                                        <div class="portlet-body">



                                                            <asp:DataGrid runat="server" ID="grdInboundItems" AutoGenerateColumns="False" OnEditCommand="grdInboundItems_EditCommand"
                                                                AllowPaging="True" PageSize="20" class="table table-hover table-striped table-bordered table-advanced tablesorter">
                                                                <PagerStyle Visible="False" />
                                                                <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                                                                <Columns>
                                                                    <asp:BoundColumn DataField="code" Visible="False"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="ItemCategoryNamear" HeaderText="<%$ Resources:pages,Category  %>"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="ItemCategoryTypeNameAr" HeaderText="<%$ Resources:pages,Type  %>"></asp:BoundColumn>


                                                                    <asp:BoundColumn DataField="PurchaseQUnitAr" HeaderText="<%$ Resources:pages,PurchaseQUnitAr  %>"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="PurchaseQty" HeaderText="<%$ Resources:pages,PurchaseQty  %>"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="Packing" HeaderText="<%$ Resources:pages,Packing  %>"></asp:BoundColumn>
                                                                    
                                                                   
                                                                    <asp:BoundColumn DataField="Qty" HeaderText="<%$ Resources:pages,QtyUnit  %>"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="QtyActualReceived" HeaderText="<%$ Resources:pages,ReceivedQty  %>"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="QUnitNameEn" HeaderText="<%$ Resources:pages,QUnit  %>"></asp:BoundColumn>
                                                                      <asp:BoundColumn DataField="WUnitEn" HeaderText="<%$ Resources:pages,W.Unit  %>"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="NetWeight" HeaderText="<%$ Resources:pages,NetWeight  %>"></asp:BoundColumn>

                                                                     
                                                                    <asp:TemplateColumn HeaderText="<%$ Resources:pages,Edit  %>">
                                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                             

                                                                            <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" class="btn btn-default btn-xs">
                                                 <i class="fa fa-edit"></i>&nbsp;
                                                <%# GetGlobalResourceObject("pages","Edit") %>
                                                                            </asp:LinkButton>

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

                                                                        <cc1:Pager CurrentIndex="1" ShowFirstLast="true" ID="pager1"
                                                                            runat="server" Width="100%" PageSize="20" OnCommand="pager1_Command"></cc1:Pager>
                                                                        &nbsp;
                                            records |<asp:Label ID="lblInboundItemsCount" runat="server"></asp:Label>

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

                                <div class="tab-pane fade" id="Attachments">
                                  <asp:UpdatePanel ID="UpdatePanel4" runat="server"  childrenastriggers="true"   updatemode="conditional">
                                        <ContentTemplate>

                                    <div class="row">
                                         
                                        <div class="col-lg-12">
                                            <div class="portlet box portlet-blue" id="divAttachmentsAdd" runat="server" visible="false">
                                                <div class="portlet-header">
                                                    <div class="caption">
                                                        <asp:Label runat="server" ID="lblAttachmentTitle"><%=GetGlobalResourceObject("pages","AddNewRecord") %></asp:Label>
                                                    </div>
                                                    <div class="tools"><i class="fa fa-chevron-up"></i><i data-toggle="modal" data-target="#modal-config" class="fa fa-cog"></i><i class="fa fa-refresh"></i><i class="fa fa-save"></i></div>
                                                </div>
                                                <div class="portlet-body">
                                                    <div role="form" class="form-horizontal">
                                                        <div class="row">

                                                            <div class="col-md-6">


                                                                <div class="form-group">
                                                                    <label class="col-md-3 control-label" for=""><%=GetGlobalResourceObject("pages","AttachType") %> </label>

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
                                                                        
                                                                        <asp:TextBox ID="txtAttachmentNotes"   runat="server" class="form-control"></asp:TextBox>
                                                                        
                                                                    </div>
                                                                </div>
                                                               

                                                            </div>

                                                            <div class="col-md-12">
                                                                <div class="form-actions">
                                                                    <div class="col-md-offset-3 col-md-9">
                                                                        <asp:LinkButton ID="lnkSaveAttachment" runat="server" class="btn btn-primary" OnClick="lnkSaveAttachment_Click"   ><i class='fa fa-save'></i>&nbsp; <%=GetGlobalResourceObject("pages","Submit") %> </asp:LinkButton>

                                                                        &nbsp;
				                                                   <asp:Button runat="server" ID="lnkCancelAttachement" class="btn btn-default" Text=" <%$ Resources: Pages, Cancel %> " OnClick="lnkCancelAttachement_Click" />

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
                                                <div class="portlet-header">
                                                    <div class="caption"><%=GetGlobalResourceObject("pages","DataListing") %></div>
                                                    <div class="actions">

                                                        <asp:LinkButton runat="server" ID="lnkAttachmentAdd" class="btn btn-info btn-xs" OnClick="lnkAttachmentAdd_Click"   ><i class="fa fa-plus"></i>&nbsp; <%=GetGlobalResourceObject("pages","AddNewRecord") %>&nbsp;</asp:LinkButton>

                                                        <asp:LinkButton OnClientClick="return checkAttachementDelete();" runat="server" ID="lnkDeleteAttachment" class="btn btn-danger btn-xs" OnClick="lnkDeleteAttachment_Click"   ><i class="fa fa-times"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %></asp:LinkButton>

                                                    </div>
                                                </div>
                                                <div class="portlet-body">



                                                    <asp:DataGrid runat="server" ID="grdAttachment" AutoGenerateColumns="False"
                                                        AllowPaging="True" PageSize="20" class="table table-hover table-striped table-bordered table-advanced tablesorter" OnEditCommand="grdAttachment_EditCommand"  >
                                                        <PagerStyle Visible="False" />
                                                        <HeaderStyle BackColor="#efefef" Font-Bold="True" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="code" Visible="False"></asp:BoundColumn>
                                                            <asp:BoundColumn  DataField="TransDate" HeaderText="<%$ Resources:Pages ,TransDate %>" DataFormatString="{0:dd/MM/yyyy}">
                                                                <ItemStyle Width="10%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Notes" HeaderText="<%$ Resources:Pages ,Notes %>"></asp:BoundColumn>
                                                             <asp:TemplateColumn HeaderText="<%$ Resources:Pages ,File %>">
                                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <a href="/layout/Uploads/Attachments/<%#Eval("PurchaseOrderCode") %>/<%#Eval("FileName") %>" class="iframe btn btn-default btn-xs" target="_blank">
                                                                        <i class="fa fa-download"></i>&nbsp;
                                                                       <%=GetGlobalResourceObject("pages","View") %>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>

                                                            <asp:TemplateColumn HeaderText="<%$ Resources:Pages ,Edit %>">
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>

                                                                    <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit" class="btn btn-default btn-xs">
                                                                 <i class="fa fa-edit"></i>&nbsp;
                                                                <%=GetGlobalResourceObject("pages","Edit") %>
                                                                    </asp:LinkButton>
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
                                                                    runat="server" Width="100%" PageSize="20" OnCommand="AttachmentPager_Command" ></cc1:Pager>
                                                                &nbsp;
                                            records |<asp:Label ID="lblAttachmentCount" runat="server"></asp:Label>

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
