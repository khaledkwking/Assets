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
    fillLocationChart();
};


function handelSelectedNode(selected_node) {
    
    var nodeId = selected_node.original.id;
    var parentId = selected_node.original.parent;
    $('#hdnSelectedEditNode').val(nodeId);
    $.ajax({
        url: "/api/hepler/GetEntityChart",
        dataType: "json",
        contentType: 'application/json',
        type: 'get',
        data:
            { nodeId: nodeId },
        success: function (objData) {
            $('#txttitleEn').val(objData.EntityNameEn);
            $('#txttitleAr').val(objData.EntityNameAr);
            $('#LstLocationParent').val(objData.ParentId).change();
            
        },
        error: function (request, status, error) {
            toastr.error(JSON.parse(request.responseText).message, 'System Error');
        }


    });

}

function getSelectedNodeRelatedData(selected_node) {

    var nodeId = selected_node.original.id;
    var parentId = selected_node.original.parent;

    $.ajax({
        url: "/api/hepler/EntityEmployeeList",
        dataType: "json",
        contentType: 'application/json',
        type: 'get',
        data:
            { nodeId: nodeId },
        success: function (employeeData) {
            console.log(employeeData);
            //  DrawEmployeeTree(employeeData);
            $('#lblEmpCount').text(employeeData.length);
            $('#employeeList-datatable').DataTable({
                data: employeeData,
                pageLength: 50,
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
                    { data: "EmpName", title: "الاسم  " },
                    { data: "CivilId", title: "الرقم المدني  " },
                    /*{ data: "ENTITYNAME", title: "المكان" },*/
                    { data: "JolbTitleAr", title: "الوظيفة  " },
                    { data: "EntityNameAr", title: "الجهة  " },
                    //{
                    //    data: "EMP_STATUS", title: "الحالة",
                    //    render: function (data, type, row) {
                    //        return (data == 'active' ? '<span class="badge badge-pill badge-outline-primary font-size-12">' + data + '</span>'
                    //            : data == 'not-active' ? '<span class="badge badge-pill badge-outline-danger font-size-12">' + data + '</span>'
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
                                "<li> <a href='javascript:void(0)' onclick='call_cbox(`../Assets/assetsListPopup.aspx?locid=0&empid=" + data + "`)' ><span class='nk-menu-icon'><em class='icon ni ni-cards'></em></span><span class='nk-menu-text' > سجل العهد</span ></a ></li>" +
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
            $('#lblLocatoinCount').text(locationData.length);
            $('#locationList-datatable').DataTable({
                data: locationData,
                pageLength: 20,
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
                    { data: "LocationNameAr", title: "الموقع" },
                    { data: "LocationTypeTitleAr", title: "النوع" },
                    { data: "LocationRefCode", title: "الكود" },
                    {
                        data: "Code", title: "", orderable: false,
                        render: function (data, type, row) {
                            return ("<div class='drodown'>" + "<a href='#' class='btn btn-sm btn-icon btn-trigger dropdown-toggle' data-toggle='dropdown'><em class='icon ni ni-more-h'></em></a>" +
                                "<div class='dropdown-menu dropdown-menu-right'>" +
                                "<ul class='link-list-opt no-bdr'>" +
                                "<li> <a href='javascript:void(0)' onclick='call_cbox(`../Assets/assetsListPopup.aspx?empid=0&locid=" + data + "`)' ><span class='nk-menu-icon'><em class='icon ni ni-cards'></em></span><span class='nk-menu-text' > قائمة العهد</span ></a ></li>" +
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


function fillLocationChart() {
    $.ajax({
        url: "/api/hepler/GetEntityTree",
        dataType: 'json',
        type: 'get',
        data: {},
        success: function (treeData) {
           
            var treeFinalResult = []
            var distinctClassID = {}
            $.each(treeData, function (i, data) {
             
                treeFinalResult.push({
                    id: data.Code,
                    parent: data.ParentId === 0 ? "#" : data.ParentId,
                    text: data.EntityNameAr,
                    // type: data.level
                })
            })
            $('#locationTree').jstree({
                "core": {
                    "check_callback": true,
                    "themes": { "stripes": true },
                    "data": treeFinalResult
                },
                
                "plugins": ["types", , "search",
                    "state", "types", "wholerow"]
            }).bind('ready.jstree', function (e, data) {
                $('#locationTree').jstree('close_all')
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
                
                $('.selectedNode').text(selected_node.original.text);
                $('#LstLocationParent').val(nodeId).change();
                $("#hdnSelectedNode").val(nodeId);

            }).bind('dblclick.jstree', function (e, data) {
                var selected_node = $('#locationTree').jstree().get_selected(true)[0];
                handelSelectedNode(selected_node);
                getSelectedNodeRelatedData(selected_node);
                // location.href="ItemsCategory.aspx?pid=" + selected_node.original.id;

            })
            hideLoader();
        }
        , beforeSend: function () { showLoader(); },
    });



    $("#treeSearch").keyup(function () {
        var searchString = $(this).val();
        $('#locationTree').jstree('search', searchString);

        if ($("#treeSearch").val() === "") {
            $("#treeSearchResult").text('');
        }
    });
    $("#expandall").click(function () {
        $('#locationTree').jstree('open_all')
    });
    $("#collapseall").click(function () {
        $('#locationTree').jstree('close_all')
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
    $.colorbox({ width: "400px", height: "95%", iframe: true, href: url });
}








