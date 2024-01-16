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
    var entityId = "0";
    if (typeof getUrlParameter('entityId') !== 'undefined') {
        entityId = getUrlParameter('entityId');
    }

    fillLocationChart(entityId);
    setTreeSelectedIds();

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



function fillLocationChart(entityId) {

    $.ajax({
        url: "/api/hepler/GetEntityLocationTree",
        dataType: 'json',
        type: 'get',
        data:
            { entityId: entityId },
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
            $('#EmployeelocationTree').jstree({
                "core": {
                    "check_callback": true,
                    "themes": { "stripes": true },
                    "data": treeFinalResult
                },

                "plugins": ["types", , "search",
                    "state", "types", "wholerow"]
            }).bind('ready.jstree', function (e, data) {
                $('#EmployeelocationTree').jstree('close_all')
                data.instance._open_to($("#hdnSelectedNode").val());
                $("#" + $("#hdnSelectedNode").val() + " >a").css("background", "#7adbc0");

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
                var selected_node = $('#EmployeelocationTree').jstree().get_selected(true)[0];
                handelSelectedNode(selected_node);
                // location.href="ItemsCategory.aspx?pid=" + selected_node.original.id;

            })
            hideLoader();
        }
        , beforeSend: function () { showLoader(); },
    });



    $("#treeSearch").keyup(function () {
        var searchString = $(this).val();
        $('#EmployeelocationTree').jstree('search', searchString);

        if ($("#treeSearch").val() === "") {
            $("#treeSearchResult").text('');
        }
    });
    $("#expandall").click(function () {
        $('#EmployeelocationTree').jstree('open_all')
    });
    $("#collapseall").click(function () {
        $('#EmployeelocationTree').jstree('close_all')
    });


}


function setTreeSelectedIds() {
    if ($("#hdnSelectedNode").val() != "") {
        console.log("Selected:" + $("#hdnSelectedNode").val())
        
    }
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

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};






