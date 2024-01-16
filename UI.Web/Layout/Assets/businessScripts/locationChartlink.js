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

    $(".lnkselected").click(function () {
        var checked_ids = [];
        var selectedNodes = $('#locationTree').jstree("get_selected", true);
        $.each(selectedNodes, function () {
            checked_ids.push(parseInt(this.id));
        });
        $("#hdnSelectedNode").val(JSON.stringify(checked_ids));
       /* console.log($("#hdnSelectedNode").val());*/
    });

};


function handelSelectedNode(selected_node) {
    
    var nodeId = selected_node.original.id;
    var parentId = selected_node.original.parent;
    $('#hdnSelectedEditNode').val(nodeId);
    $.ajax({
        url: "/api/hepler/GetLocationDetails",
        dataType: "json",
        contentType: 'application/json',
        type: 'get',
        data:
            { nodeId: nodeId },
        success: function (objData) {
            console.log("objData", objData);
            $('#txttitleEn').val(objData.LocationNameEn);
            $('#txttitleAr').val(objData.LocationNameAr);
            $('#lstLocationType').val(objData.LocationType).change();
            $('#LstLocationParent').val(objData.LocationParentId).change();
            $('#txtFinRefCode').val(objData.LocationRefCode);
            
        },
        error: function (request, status, error) {
            toastr.error(JSON.parse(request.responseText).message, 'System Error');
        }


    });

}

function setTreeSelectedIds() {
    if ($("#hdnSelectedNode").val() != "") {
        var checked_ids = [];
        var selectedNodes = $("#hdnSelectedNode").val().split(',');
        for (var i = 0; i < selectedNodes.length; i++) {
            checked_ids.push(parseInt(selectedNodes[i]));
            $('#locationTree')
                .jstree(true)
                .select_node(parseInt(selectedNodes[i]));
        }
    }
}


function fillLocationChart() {
    $.ajax({
        url: "/api/hepler/GetLocationTree",
        dataType: 'json',
        type: 'get',
        data: {},
        success: function (treeData) {
           
            var treeFinalResult = []
            var distinctClassID = {}
            $.each(treeData, function (i, data) {
             
                treeFinalResult.push({
                    id: data.Code,
                    parent: data.LocationParentId === 0 ? "#" : data.LocationParentId,
                    text: data.LocationNameAr,
                    // type: data.level
                })
            })
            $('#locationTree').jstree({
                "core": {
                    "check_callback": false,
                    "themes": { "stripes": true },
                    "data": treeFinalResult
                },
                "checkbox": {
                    "keep_selected_style": false
                },
                "plugins": ["types", , "search","checkbox",
                     "types", "wholerow"]
            }).bind('ready.jstree', function (e, data) {
                $('#locationTree').jstree('close_all')
                setTreeSelectedIds();
            }).bind('search.jstree', function (e, data) {
                if ($("#treeSearch").val() !== "") {
                    $("#treeSearchResult").text('نتيجة البحث  (' + data.nodes.length + ')');
                } else { $("#treeSearchResult").text(''); }
            }).bind('select_node.jstree', function (e, data) {
                // var selected_node = $("#equipment_tree").jstree('get_selected');
                var selected_node = data.node;
               // console.log("Selected Node :" + JSON.stringify(selected_node.original));
                var nodeId = selected_node.original.id;
                var parentId = selected_node.original.parent;
                
                $('.selectedNode').text(selected_node.original.text);
                $('#LstLocationParent').val(nodeId).change();
                $("#hdnSelectedNode").val(nodeId);

            }).bind('dblclick.jstree', function (e, data) {
                var selected_node = $('#locationTree').jstree().get_selected(true)[0];
                handelSelectedNode(selected_node);
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
    $.colorbox({ width: "400px", height: "400px", iframe: true, href: url });
}








