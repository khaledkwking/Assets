<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="AssetCheckout.aspx.cs" Inherits="UI.Web.Modules.Assets.AssetCheckout" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="hdnMasterID" runat="server" type="hidden" />
    <input id="hdnOrgChartRefCode" runat="server" type="hidden" />
    <input id="hdnActiveTab" runat="server" type="hidden" />

    <script language="JavaScript" type="text/javascript">


        function chkImage() {



            var txt = document.getElementById("<%=hdnType.ClientID %>")
            if (txt.value == "1") {
                var emp = document.getElementById("<%=lstRefEmployee.ClientID %>")
                if (emp.value == "" || emp.value == "0") {
                    Swal.fire("فضلا ، إحتر الموظف   ");
                    return false;
                }
            }

            var txt = document.getElementById("<%=txtFromDate.ClientID %>")
            if (txt.value == "") {
                Swal.fire("فضلا ، ادخل تاريخ إستمارة العهدة");
                return false;
            }


            var hdnSelectedLocation = document.getElementById("<%=selectedLocation.ClientID %>")

            if (hdnSelectedLocation.value == "" || hdnSelectedLocation.value == "0") {
                Swal.fire("فضلا ، إختر موقع العهدة ");
                return false;
            }

            var txt = document.getElementById("<%=hdnItemCount.ClientID %>")
            if (txt.value == "" || txt.value == "0") {
                Swal.fire("فضلا ، ادخل المواد");
                return false;
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
                $('.divSelectedEmployeeInfo').toggle(true);

            } else {
                $('.divEmployee').toggle(false);
                $('.divSelectedEmployeeInfo').toggle(false);
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



            $(".iframe75callback").click(
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
                });
        });

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

        function InsertItem() {
            //   alert("insert");
            var isbn = getObjById("txtItemCode").value;
            var barcode = getObjById("txtBar").value;
            var desc = getObjById("txtItemDesc").value;

            alert(isbn);

            if (isbn == "" && barcode == "" && desc == "") {
                alert("You should insert either the item Number, Bar Code or the item description");
                return false;
            }
            document.getElementById("<%=hidIsbn.ClientID %>").value = isbn;
            document.getElementById("<%=hidBar.ClientID %>").value = barcode;
            document.getElementById("<%=hidDesc.ClientID %>").value = desc;

            var txtCurr = getObjById("txtFooterQuantity");
            var txtCost = getObjById("txtFooterCost");
            if (txtCurr.value == "") {
                alert("Error, Please insert the quantity  !");
                txtCurr.focus();
                return false;
            }
            if (!chkPriceObj(txtCurr)) {
                alert("Error, Please insert a valid quantity number!");
                txtCurr.focus();
                return false;
            }
            //if (txtCost.value == "") {
            //    alert("Error, Please insert the unit cost!");
            //    txtCost.focus();
            //    return false;
            //}
            //if (!chkPriceObj(txtCost)) {
            //    alert("Error, Please insert a valid unit cost number!");
            //    txtCost.focus();
            //    return false;
            //}


            document.getElementById("<%=hidQty.ClientID %>").value = txtCurr.value;
            document.getElementById("<%=hidPrice.ClientID %>").value = txtCost.value;
            document.getElementById("<%=hidCurrency.ClientID %>").value = getObjById("lstCurrency").value;

            // alert("before add");
            var btnAdd = document.getElementById("<%=btnAddNewItem.ClientID %>")
            alert(btnAdd);
            btnAdd.click();
        }
        function NumberKey(evt, index) // when the user click enter in the new item form
        {
            //alert("jj");
            //alert("DEFAULT: "+document.getElementById("<%=txtDefault.ClientID %>"));
            document.getElementById("<%=txtDefault.ClientID %>").value = index;
            var charCode = (evt.which) ? evt.which : event.keyCode
            //alert("CODE: "+charCode);
            if (charCode == 13) {
                InsertItem();
                return false;
            }
        }
        function NumberKeyEdit(evt, index) // when the user click enter in the edit quantity
        {
            document.getElementById("<%=txtDefault.ClientID %>").value = index;
            var charCode = (evt.which) ? evt.which : event.keyCode
            //alert("CODE: "+charCode);
            if (charCode == 13) {
                //alert("ENTER KEY CONTENT: "+hidContent);
                if (CheckQuantity(getObjById("txtQuantity").id, getObjById("txtItemCost").id)) {
                    //alert("SUCCESS");
                    btnUp = getObjById("btnUpdateItem");
                    //alert("UP: "+btnUp);
                    btnUp.click();
                }
                return false
            }
            else if (charCode == 27) {
                btnCancel = getObjById("btnCancelItem");
                btnCancel.click();
            }
        }
        function LinkAddClick() {
            InsertItem();
            return false;
        }

        function ShipKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 13) {
                var btn = getObjById("btnAddExpense")
                //alert("Enter Key "+btn.value);
                btn.click();
                return false
            }
        }


        function CheckQuantity(quanid, costid) {
            var txt = document.getElementById(quanid);
            var c = document.getElementById(costid);
            if (txt.value == "") {
                alert("You should insert the modified quantity!");
                txt.focus();
                txt.select();
                return false;
            }
            if (!chkPriceObj(txt)) {
                alert("Error, Please insert a valid quantity number");
                txt.focus();
                txt.select();
                return false;
            }
            if (c.value == "") {
                alert("You should insert the item unit cost!");
                c.focus();
                c.select();
                return false;
            }
            if (!chkPriceObj(c)) {
                alert("Error, Please insert a valid cost number");
                c.focus();
                c.select();
                return false;
            }
            var oldq = document.getElementById("<%=hidOldQuantity.ClientID %>").value;
            var oldc = document.getElementById("<%=hidOldCost.ClientID %>").value;
            //alert("OLD Q: "+oldq+" and OLD C: "+oldc+" NEW Q: "+txt.value+" and New C: "+c.value);
            if ((oldq != txt.value) || (oldc != c.value))
                return confirm("Are you sure you want to update this item??");

            return true;
        }
        function getObjById(id) {
            for (var i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i]
                if (elm.id.indexOf(id) != -1) {
                    return elm;
                }
            }
            return null;
        }

        function ComputeTotal() {

          <%--  var txtItems = document.getElementById("<%=lblItemsTotal.ClientID %>");
            document.getElementById("spnItem").innerHTML = txtItems.innerHTML;
            var txtExp = document.getElementById("<%=lblExpTotal.ClientID %>");
            document.getElementById("spnOther").innerHTML = txtExp.innerHTML;
            var txtShip = document.getElementById("<%=txtShip.ClientID %>");

            var it = parseFloat(txtItems.innerHTML);
            var ex = parseFloat(txtExp.innerHTML);
            var ss = parseFloat(txtShip.value);
            var total = 0;
            //alert("COMPUTING: IT: "+it+"   and EXPEN: "+ex+" ISNAN: "+isNaN);
            if (!isNaN(parseFloat(txtItems.innerHTML)) && !isNaN(parseFloat(txtExp.innerHTML))) {
                //alert("ALL IS RIGHT:")
                total = it + ex;
            }
            if (parseFloat(txtShip.value)) {
                //alert("TOTAL BEFORE: "+total);
                total = total + ss;

            }
            //alert("TOTAL: "+total+" and PLACE: "+document.getElementById("<%=lblTotalAmount.ClientID %>"));
            document.getElementById("<%=lblTotalAmount.ClientID %>").innerHTML = formatCurrency(total + "");
            document.getElementById("hidTotal").value = total;
            //document.getElementById("<%=txtPaidAmount.ClientID %>").value=total;--%>

        }
        listen("load", window, preloadImages);

    </script>

    <input id="hdnType" runat="server" type="hidden" />



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
            <div class="nk-block-head-content" style="display: none">
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
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>


    <div class="nk-block">




        <div class="card card-bordered">


            <div class="card-inner-group">
                <div class="card-inner" data-select2-id="22">
                    <div class="card-title-group" data-select2-id="21">
                        <div class="card-title">
                            <h5 class="title"><%=GetGlobalResourceObject("pages","CustodyDetails") %></h5>
                        </div>
                        <div class="card-tools mr-n1" data-select2-id="20">
                            <ul class="btn-toolbar gx-1" data-select2-id="19">
                                <li>
                                    <a href="#" class="search-toggle toggle-search btn btn-icon" data-target="search"><em class="icon ni ni-search"></em></a>
                                </li>
                                <li class="btn-toolbar-sep"></li>

                                <%--<li>
                                    <div class="dropdown">
                                        <a href="#" class="btn btn-trigger btn-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                            <em class="icon ni ni-setting"></em>
                                        </a>
                                        <div class="dropdown-menu  dropdown-menu-right" style="">
                                            <ul class="link-check">
                                                <li>
                                                    <asp:LinkButton OnClientClick="return checkDelete();" runat="server" ID="btnDelete" OnClick="btnDelete_Click"><i class="icon ni ni-trash"></i>&nbsp;<%=GetGlobalResourceObject("pages","DeleteSelectedData") %></asp:LinkButton></li>
                                            </ul>

                                        </div>
                                    </div>
                                </li>--%>
                                <li>
                                    <div class="dropdown">
                                        <a href="#" class="btn btn-trigger btn-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                            <em class="icon ni ni-setting"></em>
                                        </a>
                                        <div class="dropdown-menu  dropdown-menu-right" style="">
                                            <ul class="link-check">
                                                <li><a href="<%=GetGlobalResourceObject("Utilities","cutureRoute") %>/Modules/Assets/AssetCheckout.aspx?t=1"><span class='nk-menu-icon'><em class='icon ni ni-user-list-fill'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Pages","CustodyAdd") %></span></a></li>
                                                <li><a href="<%=GetGlobalResourceObject("Utilities","cutureRoute") %>/Modules/Assets/AssetCheckout.aspx?t=2"><span class='nk-menu-icon'><em class='icon ni ni-focus'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Pages","CustodyAdd1") %></span></a></li>
                                                <li id="viewPrint" runat="server" visible="false"><a id="lnkPrintRequest" runat="server" href="#" class="iframe75 text-danger"><span class='nk-menu-icon'><em class='icon ni ni-printer'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Pages","PrintRequest") %></span></a></li>

                                            </ul>

                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <div class="card-search search-wrap" data-search="search">
                            <div class="search-content">
                                <a href="#" class="search-back btn btn-icon toggle-search" data-target="search"><em class="icon ni ni-arrow-left"></em></a>
                                <asp:TextBox runat="server" ID="txtFilterCode" CssClass="form-control border-transparent form-focus-none" placeholder="بحث "></asp:TextBox>
                                <asp:LinkButton runat="server" ID="lnkQuick" OnClick="lnkQuick_Click" class="search-submit btn btn-icon"> <em class="icon ni ni-search"></em> </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-inner p-2">



                    <asp:Label runat="server" ID="lblerror"></asp:Label>
                    <div class="col-lg-12">
                                       
                        <div class="portlet box portlet-blue">

                            <div class="portlet-body">
                                <div role="form">
                                    <div class="row" style="margin-bottom: 20px;">
                                        <div class="col-md-12">
                                            <div style="text-align: left; display: none">
                                                <a href="assetsListPopup.aspx?eventId=1" class="btn btn-primary iframe75callback"><em class="icon ni ni-plus"></em><span>إضافة المواد</span></a>
                                                <asp:Button runat="server" ID="btnReload" OnClick="btnReload_Click" CssClass="hide" />

                                            </div>

                                            <div class="form-group" style="display:none">

                                                <div class="custom-control custom-radio" onclick="setSelectedtype('1')">
                                                    <input type="radio" id="customRadio1" name="customRadio" class="custom-control-input">
                                                    <label class="custom-control-label" for="customRadio1">عهدة شخصية </label>
                                                </div>

                                                <div class="custom-control custom-radio" onclick="setSelectedtype('2')">
                                                    <input type="radio" id="customRadio2" name="customRadio" class="custom-control-input">
                                                    <label class="custom-control-label" for="customRadio2">عهدة تنظيمية </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                  
                                    <div class="row">
                                        <div class="col-md-4">

                                            <div class="form-group" runat="server" style="display: none">
                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","CustodyType") %>  </label>

                                                <asp:RadioButtonList ID="custodyType" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="شخصية" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="تنظيمية" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>

                                            </div>

                                            <div class="form-group divEmployee" id="divEmployee" runat="server">
                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","RefEmployee") %>  </label>
                                                <asp:DropDownList ID="lstRefEmployee" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="lstRefEmployee_SelectedIndexChanged"></asp:DropDownList>

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
                                        </div>

                                        <div class="col-md-4">
                                 <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="true" UpdateMode="conditional">
                                                                <ContentTemplate>
                                            <div class="row" id="divLocationOrg" runat="server">
                                
                                                <div class="col-md-6">
                                                    <div class="form-group" runat="server">
                                                
                                                <label class="control-label" for="">الجهة  </label>
                                                <asp:DropDownList ID="ddlDirection" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlDirection_SelectedIndexChanged"></asp:DropDownList>
                                                
                                                <br /><br />

                                                <label class="control-label" for="">الامانة  </label>
                                                <asp:DropDownList ID="ddlAmana" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlAmana_SelectedIndexChanged"></asp:DropDownList>
                                                
                                                <br /><br />

                                                <label class="control-label" for="">الادارة  </label>
                                                <asp:DropDownList ID="ddlDepartment" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>

                                                <br /><br />

                                                <label class="control-label" for="">المراقبة </label>
                                                <asp:DropDownList ID="ddlMorakba" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlMorakba_SelectedIndexChanged"></asp:DropDownList>
                                                
                                                <br /><br />

                                                <label class="control-label" for="">القسم  </label>
                                                <asp:DropDownList ID="ddlSection" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>

                                            </div>
                                                </div>
                                                <div class="col-md-6">

                                                    <div class="form-group" runat="server">
                                                
                                                <label class="control-label" for="">المبنى  </label>
                                                <asp:DropDownList ID="ddlBuilding" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlBuilding_SelectedIndexChanged"></asp:DropDownList>
                                                
                                                 <br /><br />

                                                <label class="control-label" for="">الدور  </label>
                                                <asp:DropDownList ID="ddlFloor" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlFloor_SelectedIndexChanged"></asp:DropDownList>
                                                
                                                <br /><br />

                                                <label class="control-label" for="">الغرفة  </label>
                                                <asp:DropDownList ID="ddlRoom" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged"></asp:DropDownList>

                                            </div>
                                                </div>
                           
                                            </div>
                                            
                                               
                                            <div class="form-group" runat="server" id="divLocationPersonal">
                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","assignedToLocation") %>  </label>

                                                <input type="text" id="txtOwnerLocationCode" class="form-control" placeholder="Type to filter" autocomplete="off" />
                                                <input id="selectedLocation" runat="server" value="0" type="hidden" class="selectedLocation" />

                                            </div>
                                            <br /><br />
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
                                                  </ContentTemplate>

                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlDirection" EventName="SelectedIndexChanged" />

                                                                    <asp:AsyncPostBackTrigger ControlID="ddlAmana" EventName="SelectedIndexChanged" />

                                                                    <asp:AsyncPostBackTrigger ControlID="ddlDepartment" EventName="SelectedIndexChanged" />

                                                                    <asp:AsyncPostBackTrigger ControlID="ddlMorakba" EventName="SelectedIndexChanged" />

                                                                    <asp:AsyncPostBackTrigger ControlID="ddlSection" EventName="SelectedIndexChanged" />

                                                                    <asp:AsyncPostBackTrigger ControlID="ddlBuilding" EventName="SelectedIndexChanged" />

                                                                    <asp:AsyncPostBackTrigger ControlID="ddlFloor" EventName="SelectedIndexChanged" />
                                                                    
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlRoom" EventName="SelectedIndexChanged" />

                                                                </Triggers>

                                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-md-4 divSelectedEmployeeInfo" id="divSelectedEmployeeInfo" runat="server">
                                            <div class="card card-bordered bg-light h-100">
                                                <div class="card-inner">
                                                    <div class="project">
                                                        <div class="project-head">
                                                            <a href="#" class="project-title">
                                                                <div class="user-avatar sq bg-warning"><span><em class="icon ni ni-users"></em></span></div>
                                                                <div class="project-info">
                                                                    <h6 class="title">
                                                                        <asp:Label runat="server" ID="lblSelectedEmpName"></asp:Label></h6>
                                                                    <span class="sub-text">
                                                                        <asp:Label runat="server" ID="lblSelectedjobTitle"></asp:Label></span>
                                                                </div>
                                                            </a>
                                                            <div class="drodown">
                                                                <h5 class="title">
                                                                    <asp:Label runat="server" ID="lblSelectedEmpCode"></asp:Label></h5>
                                                            </div>
                                                        </div>
                                                        <div class="project-details" style="direction:rtl">
                                                            <span class="sub-text">
                                                                <asp:Label runat="server" ID="lblSelectedEmpLocationName"></asp:Label></span>

                                                        </div>
                                                        <%--   <div class="project-progress">
                                                            <div class="project-progress-details">
                                                                <div class="project-progress-task"><em class="icon ni ni-check-round-cut"></em><span>25 Tasks</span></div>
                                                                <div class="project-progress-percent">23%</div>
                                                            </div>
                                                            <div class="progress progress-pill progress-md bg-light">
                                                                <div class="progress-bar" data-progress="23" style="width: 23%;"></div>
                                                            </div>
                                                        </div>
                                                        <div class="project-meta">
                                                            <ul class="project-users g-1">
                                                                <li>
                                                                    <div class="user-avatar sm bg-primary">
                                                                        <img src="./images/avatar/c-sm.jpg" alt=""></div>
                                                                </li>
                                                                <li>
                                                                    <div class="user-avatar sm bg-blue"><span>N</span></div>
                                                                </li>
                                                            </ul>
                                                            <span class="badge badge-dim badge-light text-gray"><em class="icon ni ni-clock"></em><span>21 Days Left</span></span>
                                                        </div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                           
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label" for=""><%=GetGlobalResourceObject("pages","Notes") %></label>
                                                <asp:TextBox runat="server" ID="txtNotes" TextMode="MultiLine" class="form-control" Height="50px"></asp:TextBox>
                                            </div>


                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                         
                    </div>

                    <div class="card-inner">
                        <asp:UpdatePanel runat="server" ID="Updatepanel2" ChildrenAsTriggers="true" UpdateMode="conditional">
                            <ContentTemplate>

                                <input id="hdnItemCount" runat="server" type="hidden" />




                                <input type="hidden" runat="server" id="txtDefault" value="2" />
                                <input type="hidden" runat="server" id="hidItemID" value="0" />
                                <input type="hidden" runat="server" id="hidContentID" value="0" />
                                <input type="hidden" runat="server" id="hidQty" value="0" />
                                <input type="hidden" runat="server" id="hidPrice" value="0" />
                                <input type="hidden" runat="server" id="hidCurrency" value="1" />

                                <input type="hidden" runat="server" id="hidIsbn" />
                                <input type="hidden" runat="server" id="hidBar" />
                                <input type="hidden" runat="server" id="hidDesc" />

                                <input type="hidden" runat="server" id="hidOldQuantity" />
                                <input type="hidden" runat="server" id="hidOldCost" />

                                <asp:Button runat="server" UseSubmitBehavior="false" ID="btnCheckItem" Text="Check Item" Style="display: none;" />
                                <asp:Button runat="server" UseSubmitBehavior="false" ID="btnAddNewItem" Text="Add Item" Style="display: none;" />

                                <input type="hidden" id="txtFocus" value="0" runat="server" />
                                <asp:Button runat="server" ID="btnHide" Text="Hide Me" Style="display: none;" OnClick="btnHide_Click" />



                                <asp:DataGrid ID="grdCustodyItems" runat="server" AllowPaging="false" ShowFooter="true"
                                    DataKeyField="EventCode" class="table table-hover table-striped table-bordered table-advanced tablesorter" PageSize="15" AutoGenerateColumns="False"
                                    BackColor="White" BorderStyle="solid" BorderWidth="1px" Font-Names="Tahoma"
                                    CellPadding="3" GridLines="both" Width="100%" OnItemDataBound="grdCustodyItems_ItemDataBound" OnItemCommand="grdCustodyItems_ItemCommand">
                                    <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <ItemStyle CssClass="grdItem" />
                                    <AlternatingItemStyle CssClass="grdItem" />
                                    <HeaderStyle CssClass="grdHead" />
                                    <FooterStyle CssClass="grdFoot" />
                                    <PagerStyle Visible="false" CssClass="grdPager" HorizontalAlign="center" Mode="NextPrev"
                                        PrevPageText="&lt;&lt; Previous &nbsp;&nbsp;&nbsp;" NextPageText="&nbsp;&nbsp;&nbsp;Next&gt;&gt;" />
                                    <Columns>

                                        <asp:TemplateColumn HeaderText="">
                                            <ItemStyle Width="1%" HorizontalAlign="center" />
                                            <FooterStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkEdit" CommandName="Edit"><em class="icon ni ni-edit" style="font-size:20px;color:cornflowerblue"></em></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkAdd" Visible="false" CommandName="AddNew"> <em class="icon ni ni-property-add" style="font-size:30px;"></em></asp:LinkButton>
                                                <div style="width: 80px; margin: 0 auto; background: red">
                                                    <div style="float: left">
                                                        <asp:LinkButton runat="server" ID="lnkCancel" CommandName="Cancel"> <em class="icon ni ni-undo" style="font-size:20px;color:Highlight"></em></asp:LinkButton>
                                                    </div>
                                                    <div style="float: left">
                                                        <asp:LinkButton runat="server" ID="lnkUpdate" CommandName="Update"> <em class="icon ni ni-save" style="font-size:20px;margin-left:10px;"></em></asp:LinkButton>
                                                    </div>
                                                </div>

                                                <asp:Button runat="server" ID="btnUpdateItem" UseSubmitBehavior="false" CommandName="Update" Style="display: none;" />
                                                <asp:Button runat="server" ID="btnCancelItem" UseSubmitBehavior="false" CommandName="Cancel" Style="display: none;" />
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="">
                                            <ItemStyle Width="1%" HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkDelete" CommandName="Delete"><em class="icon ni ni-property-remove"  style="font-size:20px;color:red" ></em></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                &nbsp;
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="EventCode" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ItemCode" Visible="false"></asp:BoundColumn>

                                        <asp:TemplateColumn HeaderText="م">
                                            <ItemStyle Width="1%" HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <%#ZeroIntergerIFNull((DataBinder.Eval(Container, "ItemIndex")).ToString()) + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="رقم المادة">
                                            <HeaderStyle Wrap="false" />
                                            <ItemStyle Width="15%" HorizontalAlign="Center" />

                                            <HeaderStyle />
                                            <ItemTemplate>
                                                <%#EmptyIfZero( gets(DataBinder.Eval(Container.DataItem, "ItemRefCode")))%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label Visible="false" runat="server" ID="lblItemCode">
		                                                                                <%#DataBinder.Eval(Container.DataItem, "ItemRefCode")%>
                                                </asp:Label>
                                                <asp:TextBox onkeypress="return NumberKey(event,1)" runat="server"
                                                    ID="txtItemCode" CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <div align="right">
                                                    <%-- <asp:Label runat="server" ID="lblItTotal"></asp:Label>--%>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="وصف المادة">
                                            <HeaderStyle Wrap="false" />
                                            <ItemStyle Width="30%" />
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "ItemNameAr")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label Visible="false" runat="server" ID="lblDesc">
		                                                                                   <%#DataBinder.Eval(Container.DataItem, "ItemNameAr")%>
                                                </asp:Label>
                                                <asp:TextBox onkeypress="return NumberKey(event,3)" runat="server"
                                                    ID="txtItemDesc" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2"
                                                    runat="server" TargetControlID="txtItemDesc"
                                                    CompletionInterval="10" CompletionSetCount="10" MinimumPrefixLength="1"
                                                    ServicePath="/modules/autocomplete/Services/TextAutoComplete.asmx" ServiceMethod="ItemAutoCompete" />
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="الكمية/الرصيد">
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="center" Width="2%" />
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "Qty")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox onfocus="this.select();" Visible="false" runat="server" ID="txtQuantity" MaxLength="20" CssClass="form-control"
                                                    Text='<%#gets(DataBinder.Eval(Container.DataItem, "Qty"))%>'
                                                    onkeypress="return NumberKeyEdit(event,6)"></asp:TextBox>
                                                <asp:TextBox onkeypress="return NumberKey(event,4)" runat="server"
                                                    Text="1" ID="txtFooterQuantity" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                                <asp:Label Visible="false" runat="server" ID="lblQty">
		                                                                                      <%#DataBinder.Eval(Container.DataItem, "Qty")%>
                                                </asp:Label>

                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Unit Cost" Visible="false">
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Width="20%" HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "EstimatedUnitCost"))%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <table border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox onfocus="this.select();" Visible="false" runat="server" ID="txtItemCost" Width="50px"
                                                                Text='<%#DataBinder.Eval(Container.DataItem, "EstimatedUnitCost")%>'
                                                                onkeypress="return NumberKeyEdit(event,7)"></asp:TextBox>
                                                            <asp:TextBox onkeypress="return NumberKey(event,5)" runat="server"
                                                                Text="1" ID="txtFooterCost" MaxLength="20" Width="50px"></asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>

                                            </EditItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="وحدة المادة">
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="center" />
                                            <FooterStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "QtyUnitTitleAr"))%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "QtyUnitTitleAr"))%>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>


                                        <asp:TemplateColumn HeaderText="Total" Visible="false">
                                            <HeaderStyle Wrap="false" HorizontalAlign="center" />
                                            <ItemStyle Width="20%" HorizontalAlign="center" />
                                            <FooterStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "Total"))%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="تاريخ العهدة">
                                            <HeaderStyle Wrap="false" />
                                            <ItemStyle Width="20%" />
                                            <ItemTemplate>
                                                <%#NullDateifEmptyText( DataBinder.Eval(Container.DataItem, "ActionDate"))%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label Visible="false" runat="server" ID="lblCustodyDate">
		                                                                                      <%#DataBinder.Eval(Container.DataItem, "ActionDate")%>
                                                </asp:Label>
                                                <div class="form-control-wrap">
                                                    <div class="form-icon form-icon-right">
                                                        <em class="icon ni ni-calendar-alt"></em>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtCustodyDate" Text='<%#NullDateifEmptyText(Eval("ActionDate")).Equals("")?NullDateifEmptyText(DateTime.Now):NullDateifEmptyText(Eval("ActionDate")) %>' placeholder="__/__/____" class="form-control date-picker"></asp:TextBox>
                                                </div>

                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="أمر الصرف">
                                            <HeaderStyle Wrap="false" />
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "StoreRequestRefCode")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label Visible="false" runat="server" ID="lblStoreRequestRefCode"><%#DataBinder.Eval(Container.DataItem, "StoreRequestRefCode")%></asp:Label>
                                                <asp:TextBox runat="server" ID="txtStoreRequestRefCode" CssClass="form-control" Text='<%#DataBinder.Eval(Container.DataItem, "StoreRequestRefCode")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText=" ملاحظـــات">
                                            <HeaderStyle Wrap="false" />
                                            <ItemStyle Width="30%" />
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "Notes")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label Visible="false" runat="server" ID="lblNotes">
		                                                                                      <%#DataBinder.Eval(Container.DataItem, "Notes")%>
                                                </asp:Label>
                                                <asp:TextBox runat="server"
                                                    ID="txtNotes" CssClass="form-control" Text='<%#DataBinder.Eval(Container.DataItem, "Notes")%>'></asp:TextBox>

                                            </EditItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:BoundColumn DataField="Qty" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Total" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="EventCode" Visible="false" HeaderText="CONTENT ID"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="EstimatedUnitCost" Visible="false"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>


                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="form-group mt-2 pull-left">
                            <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" class="btn btn-outline-light">  <%=GetGlobalResourceObject("pages","Cancel") %> </asp:LinkButton>

                            <asp:LinkButton ID="btnSave" runat="server" OnClientClick="return chkImage();" OnClick="btnSave_Click" class="btn btn-primary"><i class='icon ni ni-save'></i>&nbsp; &nbsp;<%=GetGlobalResourceObject("pages","Submit") %> </asp:LinkButton>


                        </div>
                    </div>


                </div>

            </div>

        </div>
    </div>

    <script src="/Layout/Assets/businessScripts/locationCombo.js"></script>
</asp:Content>
