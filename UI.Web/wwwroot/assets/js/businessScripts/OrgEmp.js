$(document).ready(function () {
    LoadContents();
});

function showLoader() {
    $("#progressbar").show();
}
function hideLoader() {
    setTimeout(() => { $("#progressbar").hide(); }, 1000);
}

// Main function to load the content
function LoadContents() {
    fillOrgChart();
    setupSearchFunctionality();

    //handelSelectedNode(1);
}


// Load organization chart and bind events
//function fillOrgChart() {
//    $.ajax({
//        url: "/api/hepler/orgChart",
//        dataType: 'json',
//        type: 'get',
//        data: { nodeid: 0 },
//        success: function (treeData) {
//            const treeFinalResult = treeData.map(data => ({
//                id: data.ENTITYCODE,
//                parent: data.PARENTCODE === null || data.PARENTCODE === 0 ? "#" : data.PARENTCODE,
//                text: data.ENTITYNAME,
//                type: data.ENTITYTYPE
//            }));

//            // Destroy existing jstree if already initialized
//            if ($.jstree.reference('#orgTree')) {
//                $('#orgTree').jstree("destroy").empty();
//            }

//            // Initialize jstree with new data
//            $('#orgTree').jstree({
//                core: {
//                    check_callback: true,
//                    themes: { stripes: true },
//                    data: treeFinalResult
//                },
//                types: {
//                    "org": { "icon": "icon ni ni-network" },
//                    "amana": { "icon": "icon ni ni-building" },
//                    "dept": { "icon": "icon ni ni-grid-box-alt text-info" },
//                    "div": { "icon": "icon ni ni-swap-alt text-secondary" },
//                    "sec": { "icon": "icon ni ni-wallet text-success" },
//                    "sub_sec": { "icon": "icon ni ni-wallet text-danger" }
//                },
//                plugins: ["types", "search", "state", "wholerow"]
//            }).on('ready.jstree', function () {
//                $('#orgTree').jstree('close_all');
//            }).on('select_node.jstree', function (e, data) {
//                const nodeId = data.node.original.id;
//                $('#hdnSelectedNode').val(nodeId);
//                handelSelectedNode(nodeId); // Call your function here
//            });
//        },
//        beforeSend: showLoader,
//        complete: hideLoader
//    });
//}


function fillOrgChart() {
    $.ajax({
        url: "/api/hepler/orgChart",
        dataType: 'json',
        type: 'get',
        data: { nodeid: 0 },
        success: function (treeData) {
            const treeFinalResult = treeData.map(data => ({
                id: data.ENTITYCODE,
                parent: data.PARENTCODE === null || data.PARENTCODE === 0 ? "#" : data.PARENTCODE,
                text: data.ENTITYNAME,
                type: data.ENTITYTYPE
            }));

            // Destroy and re-initialize jstree
            if ($.jstree.reference('#orgTree')) {
                $('#orgTree').jstree("destroy").empty();
            }

            $('#orgTree').jstree({
                core: {
                    check_callback: true,
                    themes: { stripes: true },
                    data: treeFinalResult
                },
                types: {
                    "org": { icon: "icon ni ni-network" },
                    "amana": { icon: "icon ni ni-building" },
                    "dept": { icon: "icon ni ni-grid-box-alt text-info" },
                    "div": { icon: "icon ni ni-swap-alt text-secondary" },
                    "sec": { icon: "icon ni ni-wallet text-success" },
                    "sub_sec": { icon: "icon ni ni-wallet text-danger" }
                },
                plugins: ["types", "search", "state", "wholerow"],
                search: {
                    show_only_matches: true, // Show only matching nodes
                    delay: 250             // Delay before search starts
                }
            }).on('ready.jstree', function () {
                $('#orgTree').jstree('close_all');
            }).on('select_node.jstree', function (e, data) {
                const nodeId = data.node.original.id;
                $('#hdnSelectedNode').val(nodeId);
                $('#lnkOrgChart').attr("href", "OrgChart.aspx?entityId=" + nodeId);
            }).on('dblclick.jstree', function () {
                const selectedNode = $('#orgTree').jstree().get_selected(true)[0];
                const nodeId = selectedNode.original.id;
                $('#hdnSelectedNode').val(nodeId);
                handelSelectedNode(nodeId);
                //setRelatedLinks(nodeId);
            });

            hideLoader();
        },
        beforeSend: showLoader
    });
}

function setupSearchFunctionality() {
    $("#treeSearch").on("keyup", function () {
        const searchString = $(this).val();
        $('#orgTree').jstree(true).search(searchString);

        if (searchString) {
            $('#treeSearchResult').text('Searching...');
        } else {
            $('#treeSearchResult').text('');
        }
    });
}



// Handle node selection to load employee table
function handelSelectedNode(nodeId) {

    var locationId = nodeId; // Replace `selectedValue` with your actual value

    // Construct the new URL with the query parameter
    var currentUrl = window.location.href.split('?')[0]; // Remove any existing query parameters
    var newUrl = currentUrl + "?locationId=" + encodeURIComponent(locationId);

    // Redirect to the new URL
    window.location.href = newUrl;


    $.ajax({
        url: "/api/hepler/GetEmployeeData",
        dataType: "json",
        type: 'get',
        data: { nodeId: nodeId },
        success: function (employeeData) {


            // Clear dropdown and add default option
            $('.lstRefEmployee').empty().append('<option value="0">--- اختر ---</option>');

            // Populate dropdown with employee data
            $.each(employeeData, function (index, employee) {
                $('.lstRefEmployee').append($('<option>', {
                    value: employee.EMP_ID,
                    text: employee.EMP_NAME
                }));
            });
          
        },
        error: function (request) {
            alert('Failed to load employees: ' + request.responseText);
        }
    });
}

function statusRenderer(data) {
    return data === 'active' ? '<span class="badge badge-primary">Active</span>' :
        data === 'not-active' ? '<span class="badge badge-danger">Not Active</span>' : data;
}

function actionRenderer(data, type, row) {
    return `<div class='dropdown'>
        <a href='#' class='btn btn-icon dropdown-toggle' data-toggle='dropdown'><em class='icon ni ni-more-h'></em></a>
        <div class='dropdown-menu'>
            <ul>
                <li><a href='#' onclick='call_cboxSmall("/MasterData/EmployeeLocation.aspx?empid=${data}")'>View Location</a></li>
                <li><a href='#' onclick='call_cbox("/Assets/AssetCheckout.aspx?empid=${data}")'>Asset Record</a></li>
                <li><a href='#' onclick='call_cbox("/Reports/AssetReceipt.aspx?empid=${data}")'>Print Form</a></li>
            </ul>
        </div>
    </div>`;
}