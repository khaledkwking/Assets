$(document).ready(function () {
    LoadContents();
});
function showLoader() {
    $("#progressbar").css("display", "");
}
function hideLoader() {
    setTimeout(function () {
        $("#progressbar").css("display", "none");
    }, 1000);
}
LoadContents = function () {
    fillOrgChart();
    handelSelectedNode(1);
};
function handelSelectedNode(nodeId) {
    console.log(nodeId);
    $.ajax({
        url: "/api/hepler/GetEmployeeHierarhcy",
        dataType: "json",
        contentType: 'application/json',
        type: 'get',
        data:
            { nodeId: nodeId },
        success: function (employeeData) {
            //  DrawEmployeeTree(employeeData);
            $('#lblEmpCount').text(employeeData.length);
            $('#employeeList-datatable').DataTable({
                data: employeeData,
                pageLength: 25,
                responsive: true,
                destroy: true,
                autoWidth: false,
                order: [],
                language: {
                    search: "",
                    searchPlaceholder: "بحث",
                    lengthMenu: "<span class='d-none d-sm-inline-block'>عرض</span><div class='form-control-select'> _MENU_ </div>",
                    info: "_START_ -_END_ of _TOTAL_",
                    infoEmpty: "No records found",
                    infoFiltered: "( Total _MAX_  )",
                    paginate: {
                        "first": "First",
                        "last": "Last",
                        "next": "Next",
                        "previous": "Prev"
                    }
                },
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ],
                columns: [
                    { data: "EMP_NAME", title: "الاسم  " },
                    { data: "CIVIL_ID", title: "الرقم المدني  " },
                    { data: "ENTITYNAME", title: "المكان" },
                    { data: "POSITION_NAME", title: "الوظيفة  " },
                    { data: "JOB_NAME", title: "المسمى الوظيفي  " },
                    {
                        data: "EMP_STATUS", title: "الحالة",
                        render: function (data, type, row) {
                            return (data == 'active' ? '<span class="badge badge-pill badge-outline-success font-size-12">' + data + '</span>'
                                : data == 'not-active' ? '<span class="badge badge-pill badge-outline-danger font-size-12">' + data + '</span>'
                                    : data
                            );
                        }
                    },
                    {
                        data: "EMP_ID", title: "", orderable: false,
                        render: function (data, type, row) {
                            return ("<div class='drodown'>" + "<a href='#' class='btn btn-sm btn-icon btn-trigger dropdown-toggle' data-toggle='dropdown'><em class='icon ni ni-more-h'></em></a>" +
                                "<div class='dropdown-menu dropdown-menu-right'>" +
                                "<ul class='link-list-opt no-bdr'>" +

                                "<li> <a href='javascript:void(0)' onclick='call_cboxSmall(`../MasterData/EmployeeLocation.aspx?entityId=" + nodeId + "&empid=" + data + "`)' ><span class='nk-menu-icon'><em class='icon ni ni-location'></em></span><span class='nk-menu-text' >  موقع الموظف</span ></a ></li>" +
                                "<li> <a href='javascript:void(0)' onclick='call_cbox(`../Assets/AssetCheckout.aspx?t=1&empid=" + data + "`)' ><span class='nk-menu-icon'><em class='icon ni ni-cards'></em></span><span class='nk-menu-text' > سجل العهد</span ></a ></li>" +
                                "<li> <a href='javascript:void(0)' onclick='call_cbox(`../Reports/AssetReceipt.aspx?locid=0&empid=" + data + "`)' ><span class='nk-menu-icon'><em class='icon ni ni-printer'></em></span><span class='nk-menu-text' > طباعة إستمارة العهدة  </span ></a ></li>" +
                                "</ul>" +
                                "</div></div>"
                            );
                        }
                    },
                    //{ data: "ENTITYCODE", title: "ENTITYCODE" },
                    //{ data: "PARENTCODE", title: "PARENTCODE" },
                    //{ data: "POSITION_NO", title: "POSITION_NO" },

                ],


            });


        },
        error: function (request, status, error) {
            toastr.clear(); NioApp.Toast(JSON.parse(request.responseText).message, 'error', { position: 'top-right' });
        }


    });


    $.ajax({
        url: "/api/hepler/EntityLocationList",
        dataType: "json",
        contentType: 'application/json',
        type: 'get',
        data:
            { nodeId: nodeId },
        success: function (locationData) {
            //  DrawEmployeeTree(employeeData);
            $('#lblcount').text(locationData.length);
            //$('#locationList-datatable').DataTable({
            //    data: locationData,
            //    pageLength: 20,
            //    responsive: true,
            //    destroy: true,
            //    autoWidth: false,
            //    order: [],
            //    language: {
            //        search: "",
            //        searchPlaceholder: "بحث",
            //        lengthMenu: "<span class='d-none d-sm-inline-block'>عرض</span><div class='form-control-select'> _MENU_ </div>",
            //        info: "_START_ -_END_ of _TOTAL_",
            //        infoEmpty: "No records found",
            //        infoFiltered: "( Total _MAX_  )",
            //        paginate: {
            //            "first": "First",
            //            "last": "Last",
            //            "next": "Next",
            //            "previous": "Prev"
            //        }
            //    },
            //    buttons: [
            //        'copy', 'csv', 'excel', 'pdf', 'print'
            //    ],
            //    columns: [
            //        { data: "LocationNameAr", title: "الموقع" },
            //        { data: "LocationTypeTitleAr", title: "النوع" },
            //        { data: "LocationRefCode", title: "الكود" },
            //         {
            //             data: "Code", title: "", orderable: false,
            //            render: function (data, type, row) {
            //                return ("<div class='drodown'>" + "<a href='#' class='btn btn-sm btn-icon btn-trigger dropdown-toggle' data-toggle='dropdown'><em class='icon ni ni-more-h'></em></a>" +
            //                    "<div class='dropdown-menu dropdown-menu-right'>" +
            //                    "<ul class='link-list-opt no-bdr'>" +
            //                    "<li> <a href='javascript:void(0)' onclick='call_cbox(`../Assets/assetsListPopup.aspx?empid=0&locid=" + data + "`)' ><span class='nk-menu-icon'><em class='icon ni ni-cards'></em></span><span class='nk-menu-text' > قائمة العهد</span ></a ></li>" +
            //                    "</ul>" +
            //                    "</div></div>"
            //                );
            //            }
            //        },

            //    ],


            //});


        },
        error: function (request, status, error) {
            toastr.clear(); NioApp.Toast(JSON.parse(request.responseText).message, 'error', { position: 'top-right' });
        }


    });
    loadLocationFrame(nodeId);


    $.ajax({
        url: "/api/hepler/GetOrgChartCustodyHeader",
        dataType: "json",
        contentType: 'application/json',
        type: 'get',
        data:
            { nodeId: nodeId },
        success: function (CustodyData) {
            $('#lblAssetsCountHeader').text(CustodyData.length);

            $('#custodyList-datatableHeader').DataTable({
                data: CustodyData,
                pageLength: 25,
                responsive: true,
                destroy: true,
                autoWidth: false,
                order: [],
                language: {
                    search: "",
                    searchPlaceholder: "بحث",
                    lengthMenu: "<span class='d-none d-sm-inline-block'>عرض</span><div class='form-control-select'> _MENU_ </div>",
                    info: "_START_ -_END_ of _TOTAL_",
                    infoEmpty: "No records found",
                    infoFiltered: "( Total _MAX_  )",
                    paginate: {
                        "first": "First",
                        "last": "Last",
                        "next": "Next",
                        "previous": "Prev"
                    }
                },
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ],
                columns: [
                    { data: "Serial", title: "مسلسل  " },
                    /* { data: "Type", title: "نوع العهدة	" },*/

                    {
                        data: "RequestActionType", title: "نوع العهدة	",
                        render: function (data, type, row) {
                            return (data == '1' ? '<span class="badge badge-dim badge-light">عهدة شخصية</span>'
                                : data == '2' ? '<span class="badge badge-outline-info">عهدة تنظيمية</span>'
                                    : data
                            );
                        }
                    },

                    {
                        data: "Locationpath",
                        title: "مكان العهدة",
                        render: function (data, type, row) {
                            // Return the HTML structure
                            return `
                <div class="text-info">${row.Ora_EmpName} 
                    <span class="badge badge-dim ${row.Emp_Active ? 'badge-success' : 'badge-danger'}">
                        ${row.Emp_Active ? 'فعال' : 'غير فعال'}
                    </span>
                </div>
                <div class="text-indigo">${data}</div>`;
                        }
                    },
                    {
                        data: "RequestDate",
                        title: "تاريخ الإستمارة	",
                        render: function (data) {
                            if (data) {
                                // Format date to yyyy-MM-dd or any desired format
                                const date = new Date(data);
                                return date.toISOString().split('T')[0];
                            }
                            return "";
                        }
                    },
                    {
                        data: "CreatedAt",
                        title: "تاريخ العملية	 ",
                        render: function (data) {
                            if (data) {
                                const date = new Date(data);
                                return date.toISOString().split('T')[0];
                            }
                            return "";
                        }
                    },
                    { data: "RequestNotes", title: "ملاحظات   " },
                    //{
                    //    data: "Connected", title: "تم الربط",
                    //    render: function (data, type, row) {
                    //        return (data == 'نعم' ? '<span class="badge badge-pill badge-outline-success font-size-12">' + data + '</span>'
                    //            : data == 'لا' ? '<span class="badge badge-pill badge-outline-danger font-size-12">' + data + '</span>'
                    //                : data
                    //        );
                    //    }
                    //},
                    {

                        data: "Code", title: "", orderable: false,
                        render: function (data, type, row) {

                            return ("<div class='drodown'>" + "<a href='#' class='btn btn-sm btn-icon btn-trigger dropdown-toggle' data-toggle='dropdown'><em class='icon ni ni-more-h'></em></a>" +
                                "<div class='dropdown-menu dropdown-menu-right'>" +
                                "<ul class='link-list-opt no-bdr'>" +

                                "<li> <a href='javascript:void(0)' onclick='call_cbox(`../Assets/AssetCheckout.aspx?hidemaster=1&t=1&requestCode=" + data + "`)' ><span class='nk-menu-icon'><em class='icon ni ni-cards'></em></span><span class='nk-menu-text' > سجل العهد</span ></a ></li>" +
                                "<li> <a href='javascript:void(0)' onclick='call_cbox(`../Reports/AssetReceipt.aspx?hidemaster=1&locid=0&empid=" + row.Ora_EmpRefCode + "`)' ><span class='nk-menu-icon'><em class='icon ni ni-printer'></em></span><span class='nk-menu-text' > طباعة إستمارة العهدة  </span ></a ></li>" +

                                "</ul>" +
                                "</div></div>"
                            );
                        }
                    },

                ],


            });


        },
        error: function (request, status, error) {
            toastr.clear(); NioApp.Toast(JSON.parse(request.responseText).message, 'error', { position: 'top-right' });
        }


    });
}
function setRelatedLinks(nodeId) {
    //$("#lnkReportOrgCustody").href  = "../reports/StocktakingReport.aspx?ReportType=1&entityId=" + nodeId;
    //$("#lnkReportOrgCustody2").href  = "../reports/StocktakingReport.aspx?ReportType=2&entityId=" + nodeId;

    var a = document.getElementById('lnkReportOrgCustody');
    a.href = "../reports/StocktakingReport.aspx?ReportType=1&entityId=" + nodeId;


    var a = document.getElementById('lnkReportOrgCustody2');
    a.href = "../reports/StocktakingReport.aspx?ReportType=2&entityId=" + nodeId;



    var a = document.getElementById('lnkReportOrgReceiptList');
    a.href = "../reports/OrgAssetReceipt.aspx?entityId=" + nodeId;






}

function loadLocationFrame(nodeId) {
    var iframe = document.getElementById("entityLocationFrame");
    iframe.src = 'Locationslink.aspx?entityId=' + nodeId;
}

function fillOrgChart() {
    $.ajax({
        url: "/api/hepler/orgChart",
        dataType: 'json',
        type: 'get',
        data: { nodeid: 0 },
        success: function (treeData) {


            //now loop through the treeData  
            var treeFinalResult = []
            var distinctClassID = {}
            $.each(treeData, function (i, data) {

                treeFinalResult.push({
                    id: data.ENTITYCODE,
                    parent: data.PARENTCODE === null || data.PARENTCODE === 0 ? "#" : data.PARENTCODE,
                    text: data.ENTITYNAME,
                    type: data.ENTITYTYPE
                })
            })


            $('#orgTree').jstree({
                "core": {
                    "check_callback": true,
                    "themes": { "stripes": true },
                    "data": treeFinalResult
                },
                "types": {
                    "org": {
                        "icon": "icon ni ni-network"
                    },
                    "amana": {
                        "icon": "icon ni ni-building"
                    },
                    "dept": {
                        "icon": "icon ni ni-grid-box-alt text-info "
                    },
                    "div": {
                        "icon": "icon ni ni-swap-alt text-secondary"
                    },
                    "sec": {
                        "icon": "icon ni ni-wallet text-success"
                    },
                    "sub_sec": {
                        "icon": "icon ni ni-wallet text-danger"
                    }
                },
                "plugins": ["types", "search",
                    "state", "types", "wholerow"]
            }).bind('ready.jstree', function (e, data) {
                $('#orgTree').jstree('close_all')
            }).bind('search.jstree', function (e, data) {

                if ($("#treeSearch").val() !== "") {
                    $("#treeSearchResult").text('نتيجة البحث  (' + data.nodes.length + ')');
                } else { $("#treeSearchResult").text(''); }
            }).bind('select_node.jstree', function (e, data) {
                // var selected_node = $("#equipment_tree").jstree('get_selected');
                var selected_node = data.node;
                //console.log("Selected Node :" + JSON.stringify(selected_node.original));
                var nodeId = selected_node.original.id;
                var parentId = selected_node.original.parent;
                //  console.log("Selected Id : " + nodeId + ", ParentId: " + parentId);
                /*  $('.selectedNode').text(selected_node.original.text);*/
                $('#hdnSelectedNode').val(nodeId);
                //$('#lnkAddLocation').attr("href", "LocationsList.aspx?entityId=" + nodeId);
                //$('#lnkLocationlink').attr("href", "Locationslink.aspx?entityId=" + nodeId);
                $('#lnkOrgChart').attr("href", "OrgChart.aspx?entityId=" + nodeId);
            }).bind('dblclick.jstree', function (e, data) {
                var selected_node = $('#orgTree').jstree().get_selected(true)[0];
                var nodeId = selected_node.original.id;
                $('#hdnSelectedNode').val(nodeId);
                handelSelectedNode(nodeId);
                setRelatedLinks(nodeId);
            })
            hideLoader();

        }
        , beforeSend: function () { showLoader(); },
    });



    $("#treeSearch").keyup(function () {
        var searchString = $(this).val();
        $('#orgTree').jstree('search', searchString);

        if ($("#treeSearch").val() === "") {
            $("#treeSearchResult").text('');
        }
    });
    $("#expandall").click(function () {
        $('#orgTree').jstree('open_all')
    });
    $("#collapseall").click(function () {
        $('#orgTree').jstree('close_all')
    });


}
function getbartype(percentageValue) {
    if (percentageValue < 50) {
        return 'progress-bar-info';
    } else if (percentageValue >= 50 && percentageValue < 75) {
        return 'progress-bar-warning';
    } else {
        return 'progress-bar-success';
    }


}
function uniqueBy(arr, prop) {
}
function call_cbox(url) {
    // alert(url);

    $.colorbox({ width: "80%", height: "95%", iframe: true, href: url });
}
function call_cboxSmall(url) {
    // alert(url);
    $.colorbox({ width: "600px", height: "95%", iframe: true, href: url });
}
