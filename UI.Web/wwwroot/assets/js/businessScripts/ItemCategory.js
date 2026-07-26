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
    console.log("selected::", $('#hdnSelectedNode').val());
    if ($('#hdnSelectedNode').val() !== "" && $('#hdnSelectedNode').val() !== "0") {
        handelSelectedNode($('#hdnSelectedNode').val());
    }
};

function handelSelectedNode(nodeId) {
    
    console.log("selected Inner ::", nodeId);
    $('#hdnSelectedEditNode').val(nodeId);
    $.ajax({
        url: "/api/hepler/GetItemCategoryDetails",
        dataType: "json",
        contentType: 'application/json',
        type: 'get',
        data:
            { nodeId: nodeId },
        success: function (objData) {
            console.log(objData);
            $('#txttitleEn').val(objData.TitleEn);
            $('#txttitleAr').val(objData.TitleAr);
            $('#txtFinRefCode').val(objData.FinanceRefCode);
            $('.parentCategory').val(objData.Cat_ParentId).change();

            $('#txtScrapPeriod').val(objData.ServicePeriod);
            $('#txtScrapAmount').val(objData.ScrapPrice);
        },
        error: function (request, status, error) {
            toastr.error(JSON.parse(request.responseText).message, 'System Error');
        }


    });
    fillItemList(nodeId);

}
function fillItemList(nodeId) {
     $.ajax({
        url: "/api/hepler/GetCategoryItemList",
        dataType: "json",
        contentType: 'application/json',
        type: 'get',
        data:
            { nodeId: nodeId },
        success: function (itemList) {

            //console.log(itemList);

            $('#lblItemCount').text(itemList.length);
            $('#itemList-datatable').DataTable({
                data: itemList,
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
                    { data: "D_ItemsCategoryTitleAr", title: "التصنيف  " },
                    { data: "ItemNameAr", title: "  الصنف  " },
                    { data: "D_QtyUnitTitleAr", title: "وحدة القياس " },
                    { data: "ItemRFIDCode", title: "كود RFID  " },
                    { data: "MinQty", title: "  اقل كمية بالمخزن  " },
                    
                  
                ],


            });


        },
        error: function (request, status, error) {
            toastr.clear(); NioApp.Toast(JSON.parse(request.responseText).message, 'error', { position: 'top-right' });
        }


    });

}


function fillOrgChart() {
    $.ajax({
        url: "/api/hepler/GetItemsCategoryTree",
        dataType: 'json',
        type: 'get',
        data: {},
        success: function (treeData) {

            var treeFinalResult = []
            var distinctClassID = {}
            $.each(treeData, function (i, data) {
                hideLoader();
                treeFinalResult.push({
                    id: data.code,
                    parent: data.Cat_ParentId === 0 ? "#" : data.Cat_ParentId,
                    text: data.TitleAr,
                    // type: data.level
                })
            })
            $('#itemCategoryChart').jstree({
                "core": {
                    "check_callback": true,
                    "themes": { "stripes": true },
                    "data": treeFinalResult
                },
                "types": {
                    "0": {
                        "icon": "fa fa-laptop-house text-info"
                    },
                    "1": {
                        "icon": "fa fa-layer-group text-primary"
                    },
                    "2": {
                        "icon": "fa fa-building text-info "
                    },
                    "div": {
                        "icon": "fa fa-file-code text-secondary"
                    },
                    "sec": {
                        "icon": "fa fa-file text-success"
                    },
                    "sub_sec": {
                        "icon": "fa fa-file text-danger"
                    }
                },
                "plugins": ["types", , "search",
                    "state", "types", "wholerow"]
            }).bind('ready.jstree', function (e, data) {
                $('#itemCategoryChart').jstree('close_all')
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
                $('#hdnSelectedNode').val(nodeId);
                //document.getElementsByTagName("hdnSelectedNode").value = nodeId;
                //$('.selectedNode').text(selected_node.original.text);
                //$('.parentCategory').val(nodeId).change();
                //$("#hdnSelectedNode").val(nodeId);
                ///*Clear Form*/
                //$('#txttitleEn').val("");
                //$('#txttitleAr').val("");
                //$('#txtFinRefCode').val("");

            }).bind('dblclick.jstree', function (e, data) {
                var selected_node = $('#itemCategoryChart').jstree().get_selected(true)[0];
                handelSelectedNode(selected_node.original.id);

                var nodeId = selected_node.original.id;
                $('#hdnSelectedNode').val(nodeId);

                // location.href="ItemsCategory.aspx?pid=" + selected_node.original.id;

            })

        }
        , beforeSend: function () { showLoader(); },
    });



    $("#treeSearch").keyup(function () {
        var searchString = $(this).val();
        $('#itemCategoryChart').jstree('search', searchString);

        if ($("#treeSearch").val() === "") {
            $("#treeSearchResult").text('');
        }
    });
    $("#expandall").click(function () {
        $('#itemCategoryChart').jstree('open_all')
    });
    $("#collapseall").click(function () {
        $('#itemCategoryChart').jstree('close_all')
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
    $.colorbox({ width: "400px", height: "400px", iframe: true, href: url });
}








