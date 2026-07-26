<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/_shared/Main.Master" AutoEventWireup="true" CodeBehind="AssetCheckout.aspx.cs" Inherits="UI.Web.Modules.Assets.AssetCheckout" %>

<%@ Register TagPrefix="cc1" Namespace="CutePager" Assembly="ASPnetPagerV2netfx2_0" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input id="hdnMasterID" runat="server" type="hidden" />
    <asp:HiddenField runat="server" ID="hdnSelectedNode" ClientIDMode="Static" />
    <input id="hdnActiveTab" runat="server" type="hidden" />
    <asp:HiddenField ID="hfSelectedEmployeeText" runat="server" ClientIDMode="Static" />



        <!-- jQuery UI CSS -->
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" />


    <!-- jQuery UI -->
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>

    <script language="JavaScript" type="text/javascript">



        function chkImage() {


            var txt = document.getElementById("<%=hdnType.ClientID %>")
            if (txt.value == "1" || txt.value == "2") {
                var emp = document.getElementById("<%=lstRefEmployee.ClientID %>")
                var txtName = document.getElementById("<%=txtName.ClientID %>")
                var txtCivilID = document.getElementById("<%=txtCivilID.ClientID %>")
                if ((emp.value == "" || emp.value == "0") && (txtName.value == "" || txtCivilID.value == "")) {
                    Swal.fire("يجب اختيار الموظف او ادخال الاسم و الرقم المدني  ");
                    return false;
                }
                else if ((emp.value != "" && emp.value != "0") && (txtName.value != "")) {
                    Swal.fire(" فضلا ، إختر مابين الموظف او الاسم و الرقم المدني   ");
                    return false;
                }
                else {
                    document.getElementById("<%=hfSelectedEmployeeText.ClientID %>").value = emp.options[emp.selectedIndex].text;
                }
            }


            var txt = document.getElementById("<%=txtRequestDate.ClientID %>")
            if (txt.value == "") {
                Swal.fire("فضلا ، ادخل تاريخ إستمارة العهدة");
                return false;
            }


            var hdnSelectedLocation = document.getElementById("<%=lstToLocation.ClientID %>")

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
                /*$('.divOrgOwner').toggle(false);*/

            } else {
                $('.divEmployee').toggle(true);
                $('.divSelectedEmployeeInfo').toggle(true);
                /*$('.divOrgOwner').toggle(true);*/

            }
        }


        $(document).ready(function () {

            var civilIdInput = $('#<%= txtCivilID.ClientID %>');

            // Allow only digits and prevent more than 12 digits
            civilIdInput.on('keypress', function (e) {
                var charCode = e.which ? e.which : e.keyCode;

                // Block non-digit characters
                if (charCode < 48 || charCode > 57) {
                    e.preventDefault();
                    return;
                }

                // Prevent input if already 12 digits
                if ($(this).val().length >= 12) {
                    e.preventDefault();
                }
            });

            // Client-side validation on form submit
            $('form').on('submit', function (e) {
                var value = civilIdInput.val();
                if (!/^\d{12}$/.test(value)) {
                    alert("الرقم المدني يجب أن يكون مكونًا من 12 رقمًا");
                    civilIdInput.addClass('is-invalid').focus();
                    e.preventDefault();
                    return false;
                } else {
                    civilIdInput.removeClass('is-invalid');
                }
            });

            // Validate on blur (when leaving the field)
            civilIdInput.on('blur', function () {
                var value = $(this).val();
                if (!/^\d{12}$/.test(value)) {
                    civilIdInput.addClass('is-invalid');
                    alert("الرقم المدني يجب أن يكون مكونًا من 12 رقمًا");
                } else {
                    civilIdInput.removeClass('is-invalid');
                }
            });

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
                /*$('.divOrgOwner').toggle(true);*/
            } else if (hdnType.value == "2") {
                $("#customRadio1").prop("checked", false);
                $("#customRadio2").prop("checked", true);
                $('.divEmployee').toggle(true);
                /*$('.divOrgOwner').toggletruefalse);*/

            } else {
                $("#customRadio1").prop("checked", true);
                $("#customRadio2").prop("checked", false);
                /*$('.divOrgOwner').toggle(false);*/
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
                alert("jj");
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
                alert("CODE: "+charCode);
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


            listen("load", window, preloadImages);
        });
    </script>
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

    <style>
    
/* القائمة نفسها (ul) */
/* القائمة نفسها */
.autocomplete_completionListElement {
    width: 400px !important;
    max-height: 500px;
    overflow-y: auto;
    background-color: #fff;
    z-index: 9999;
    border: 1px solid #ccc;
    border-radius: 6px;
    padding: 5px !important;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
    font-family: "Cairo", Tahoma, sans-serif;
    font-size: 15px;
    text-align: right !important;
    direction: rtl !important;      /* 🔥 يفرض الاتجاه من اليمين */
    unicode-bidi: bidi-override;    /* 🔥 يجبر ترتيب النص RTL */
    left:700px !important;
}

/* العناصر داخل القائمة */
.autocomplete_completionListElement li {
    line-height:50px;
    display: block !important;      /* مهم عشان padding يشتغل */
    text-align: right !important;
    direction: rtl !important;
    padding: 8px 20px;
    margin-bottom: 3px;
    border-bottom: 1px solid #ddd;
    color: #333;
    background-color: #fff;
    cursor: pointer;
    transition: all 0.2s ease-in-out;
}

/* آخر عنصر بدون خط */
.autocomplete_completionListElement li:last-child {
    border-bottom: none;
}

/* عند المرور بالماوس */
.autocomplete_completionListElement li:hover {
    background-color: #f5f8fc;
    color: #0C476B;
    padding-right: 25px;
}



    </style>
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
                            <h5 class="title"><%=_PageSubTitle %></h5>
                        </div>
                        <div class="card-tools mr-n1" data-select2-id="20">
                            <ul class="btn-toolbar gx-1" data-select2-id="19">
                                <li>
                                    <a href="#" class="search-toggle toggle-search btn btn-icon" data-target="search"><em class="icon ni ni-search"></em></a>
                                </li>
                                <li class="btn-toolbar-sep"></li>


                                <li>
                                    <div class="dropdown">
                                        <a href="#" class="btn btn-trigger btn-icon dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                            <em class="icon ni ni-setting"></em>
                                        </a>
                                        <div class="dropdown-menu  dropdown-menu-right" style="">
                                            <ul class="link-check">
                                                <%--<li><a href="<%=GetGlobalResourceObject("Utilities","cutureRoute") %>/Modules/Assets/AssetCheckout.aspx?t=1"><span class='nk-menu-icon'><em class='icon ni ni-user-list-fill'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Pages","CustodyAdd") %></span></a></li>
                                                <li><a href="<%=GetGlobalResourceObject("Utilities","cutureRoute") %>/Modules/Assets/AssetCheckout.aspx?t=2"><span class='nk-menu-icon'><em class='icon ni ni-focus'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Pages","CustodyAdd1") %></span></a></li>--%>
                                                <li id="viewPrint" runat="server" visible="false"><a id="lnkPrintRequest" runat="server" href="#" class="iframe75 text-danger"><span class='nk-menu-icon'><em class='icon ni ni-printer'></em></span><span class='nk-menu-text'><%=GetGlobalResourceObject("Pages","PrintRequest") %></span></a></li>

                                                <li id="viewAssetsInventoryPrint" runat="server" visible="false"><a id="lnkAssetsInventoryPrint" runat="server" href="#" class="iframe75 text-danger"><span class='nk-menu-icon'><em class='icon ni ni-printer'></em></span><span class='nk-menu-text'>طباعة بطاقة الجرد</span></a></li>

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
                <div class="card-inner p-4">

                    <div class="row">

                        <asp:Label runat="server" ID="lblerror"></asp:Label>
                        <div class="col-md-3 treecontainer">

                            <div class="panel panel-yellow" style="min-height: 70vh">

                                <div class="p-1 " style="background-color: #0C476B; margin-bottom: 20px">
                                    <div class="d-flex">
                                        <div class="align-self-center me-3">
                                            <img src="/wwwroot/assets/images/logo/KuwaitLogo.png" class="avatar-xs rounded-circle" width="50px" alt="avatar-2">
                                        </div>
                                        <div class="flex-1" style="padding-top: 10px; padding-right: 10px;">
                                            <h5 class="font-size-15 mb-1" style="color: #E4DAC1; font-size: 16px;">الأمانة العامة لمجلس الوزراء</h5>
                                            <p class="text-muted text-truncate mb-0" style="line-height: 20px; font-size: 13px; color: #E4DAC1 !important;">الهيكل التنظيمي  </p>

                                        </div>


                                    </div>
                                </div>

                                <%--   <div class="panel-heading clearfix">
                                    <span class="mts"><%=GetGlobalResourceObject("pages","OrgChartTitle") %>  </span>
                                    <div style="float: left"><a href="../Reports/OrgChartPrint.aspx" class="btn btn-dim btn-primary  btn-xs iframe75 "><i class="icon ni ni-printer"></i></a></div>
                                </div>--%>
                                <div class="panel-body" style="padding: 0px; padding-top: 0px; margin-bottom: 10px;">

                                    <div class="form-control-wrap" style="margin-bottom: 20px;">
                                        <div class="form-icon form-icon-right">
                                            <em class="icon ni ni-search"></em>
                                        </div>
                                        <input type="text" class="form-control" id="treeSearch" placeholder="  بحث الهيكل التنظيمي">
                                    </div>

                                    <div id="progressbar">
                                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                        <span class="sr-only">Loading...</span>
                                    </div>

                                    <div id="orgTree" class=""></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-9">
                            <div class="portlet box portlet-blue">

                                <div class="portlet-body">
                                    <div role="form">
                                        <div class="row" style="margin-bottom: 20px; display: none">
                                            <div class="col-md-12">
                                                <div style="text-align: left; display: none">
                                                    <a href="assetsListPopup.aspx?eventId=1" class="btn btn-primary iframe75callback"><em class="icon ni ni-plus"></em><span>إضافة المواد</span></a>


                                                </div>

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
                                                    <asp:DropDownList ID="lstRefEmployee" name="ctl00$ContentPlaceHolder1$lstRefEmployee" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static"></asp:DropDownList>

                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label" for="">تاريخ إنشاء الإستمارة</label>

                                                    <div class="form-control-wrap">
                                                        <div class="form-icon form-icon-right">
                                                            <em class="icon ni ni-calendar-alt"></em>
                                                        </div>
                                                        <asp:TextBox runat="server" ID="txtRequestDate" placeholder="__/__/____" class="form-control date-pickers"></asp:TextBox>
                                                        
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","assignedToLocation") %>  </label>
                                                    <asp:DropDownList ID="lstToLocation" runat="server" class="form-control form-select" data-search="on" ClientIDMode="Static"></asp:DropDownList>
                                                    <%--<asp:DropDownList runat="server" ID="lstToLocation"></asp:DropDownList>--%>
                                                </div>

                                                <div class="form-group" style="display:none">

                                                    <div class="custom-control custom-checkbox">
                                                        <input type="checkbox" class="custom-control-input" id="chkReturnDate">
                                                        <label class="custom-control-label" for="chkReturnDate"></label>
                                                    </div>

                                                    <label class="control-label" for=""><%=GetGlobalResourceObject("pages","ReturnDate") %></label>

                                                    <div class="form-control-wrap" id="disReturnDate" style="display: none">
                                                        <div class="form-icon form-icon-right">
                                                            <em class="icon ni ni-calendar-alt"></em>
                                                        </div>
                                                        <asp:TextBox runat="server" ID="txtReturnDate" placeholder="__/__/____" class="form-control date-pickers" ClientIDMode="Static"></asp:TextBox>
                                                    </div>


                                                </div>

                                            </div>

                                            <div class="col-md-4 divSelectedEmployeeInfo" id="divSelectedEmployeeInfo" style="display: none">
                                                <div class="card card-bordered bg-light">

                                                    <div id="cboxLoadedContent">
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="row" id="divOrgOwner" runat="server">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label" for="">الاسم</label>
                                                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label" for="">الرقم المدني</label>
                                                    <asp:TextBox runat="server" ID="txtCivilID" CssClass="form-control"></asp:TextBox>


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
                            <div class="card-inner">
                                <asp:UpdatePanel runat="server" ID="Updatepanel2" ChildrenAsTriggers="true" UpdateMode="conditional">
                                    <ContentTemplate>


                                        <input id="hdnItemCount" runat="server" type="hidden" />
                                        <asp:HiddenField runat="server" ID="hdnEmployeeId" ClientIDMode="Static" />



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
                                        <asp:Button runat="server" ID="btnReload" OnClick="btnReload_Click" CssClass="hide" ClientIDMode="Static" Style="display: none;" />


                                        <input type="hidden" id="txtFocus" value="0" runat="server" />
                                        <asp:Button runat="server" ID="btnHide" Text="Hide Me" Style="display: none;" OnClick="btnHide_Click" />

                                        <div class="form-group mt-2 pull-left">
                                            <asp:LinkButton ID="btnConvert" runat="server" OnClientClick="return chkImage();" OnClick="btnConvert_Click" class="btn btn-primary"><i class='icon ni ni-edit'></i> </asp:LinkButton>
                                        </div>

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
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" />

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
                                                        <%--<%#DataBinder.Eval(Container.DataItem, "ItemNameAr")%>--%>
                                                        <%# HttpUtility.HtmlEncode(Eval("ItemNameAr")) %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label Visible="false" runat="server" ID="lblDesc">
		                                                                                   <%#DataBinder.Eval(Container.DataItem, "ItemNameAr")%>
                                                        </asp:Label>
                                                        <asp:TextBox onkeypress="return NumberKey(event,3)" runat="server"
                                                            ID="txtItemDesc" CssClass="form-control"></asp:TextBox>
                                                      <%--  <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2"
                                                            runat="server" TargetControlID="txtItemDesc"
                                                            CompletionInterval="10" CompletionSetCount="10" MinimumPrefixLength="1" CompletionListItemCssClass="autocomplete"
                                                            ServicePath="/modules/autocomplete/Services/TextAutoComplete.asmx" ServiceMethod="ItemAutoCompete" />--%>
                                                      <ajaxToolkit:AutoCompleteExtender 
                                                            ID="AutoCompleteExtender2"
                                                            runat="server"
                                                            TargetControlID="txtItemDesc"
                                                            CompletionInterval="10"
                                                            CompletionSetCount="10"
                                                            MinimumPrefixLength="1"
                                                            ServicePath="/modules/autocomplete/Services/TextAutoComplete.asmx"
                                                            ServiceMethod="ItemAutoCompete"
                                                            CompletionListCssClass="autocomplete_completionListElement" />



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

                                                        <asp:Label ID="lbldates" runat="server">
<%--                                                            <%#NullDateifEmptyText( DataBinder.Eval(Container.DataItem, "ActionDate" ,"{0:dd-MM-yyyy}"))% --%>
                                                            <%# FormatDate(Eval("ActionDate")) %>

                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label Visible="false" runat="server" ID="lblCustodyDate">
		                                                                                      <%#DataBinder.Eval(Container.DataItem, "ActionDate")%>
                                                        </asp:Label>
                                                        <div class="form-control-wrap">
                                                            <div class="form-icon form-icon-right">
                                                                <em class="icon ni ni-calendar-alt"></em>
                                                            </div>
                                                            <asp:TextBox runat="server" ID="txtCustodyDate" Text='<%#NullDateifEmptyText(Eval("ActionDate")).Equals("")?NullDateifEmptyText(DateTime.Now):NullDateifEmptyText(Eval("ActionDate")) %>' placeholder="__/__/____" class="form-control date-pickers"></asp:TextBox>
                                                        </div>

                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="تاريخ التشغيل">
                                                    <HeaderStyle Wrap="false" />
                                                    <ItemStyle Width="20%" />
                                                    <ItemTemplate>

                                                        <asp:Label ID="lbldatesxx" runat="server" Enabled="false">
                                                            <%#NullDateifEmptyText( DataBinder.Eval(Container.DataItem, "ItemDate" ,"{0:dd-MM-yyyy}"))%>
                                                        </asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="الحالة">
                                                    <ItemTemplate>
                                                        <!-- Label for View Mode -->
                                                        <asp:Label runat="server" ID="lblStatusIdTitle" Text=' <%#DataBinder.Eval(Container.DataItem, "ItemUsedStatusTitle")%>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label Visible="false" runat="server" ID="lblStatusId" Text='<%#DataBinder.Eval(Container.DataItem, "ItemUsedStatus")%>'> </asp:Label>
                                                        <!-- DropDownList for Add/Edit Mode -->
                                                        <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="-- Select --" Value="" />
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="أمر الصرف" Visible="false">
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

        </div>
    </div>
    <script src="/wwwroot/assets/js/businessscripts/Assetscheckout.js"></script>

    <%--  <script src="/wwwroot/assets/js/businessScripts/locationCombo.js"></script>--%>
</asp:Content>
