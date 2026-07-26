let cmgs_EmployeeList = [];
$(document).ready(function () {
    LoadContents();
});
 
LoadContents = function () {
    showProcessLoader();
    fillOrgChart();
    // handelSelectedNode(1);
};
function handelSelectedNode(nodeId) {
   // console.log("handelSelectedNode:", nodeId);
    $.ajax({
        url: "/api/hepler/GetEmployeeHierarhcy",
        dataType: "json",
        contentType: 'application/json',
        type: 'get',
        data:
            { nodeId: nodeId },
        success: function (employeeData) {
 
            cmgs_EmployeeList = employeeData;

            // Bind Employee List to Select2 dropdown
            let $employeeDropdown = $('#lstRefEmployee');
            $employeeDropdown.empty(); // Clear existing options
            $employeeDropdown.append(new Option("إختر الموظف", "", true, true)); // Placeholder option

            $.each(employeeData, function (index, employee) {
                $employeeDropdown.append(new Option(employee.EMP_NAME, employee.EMP_ID)); // Assuming employee has Name and Id properties
            });
            $employeeDropdown.select2(); // Initialize Select2

            // Handle change event to load employee information and execute server event
            $employeeDropdown.on('change', function () {
                let selectedEmployeeId = $(this).val();
                let selectedEmployee = cmgs_EmployeeList.find(emp => emp.EMP_ID === selectedEmployeeId);
                if (selectedEmployee) {
                    // Load employee information (you can customize this part)
                    loadEmployeeInfo(selectedEmployee);
                    // Execute server event to load employee assets
                 // loadEmployeeAssets(selectedEmployeeId);
                }
            });

            //Load Existing Employee
            if ($("#hdnEmployeeId").val() != null && $("#hdnEmployeeId").val() != "") {

                //let selectedEmployeeId = $("#hdnEmployeeId").val();
                //let selectedEmployee = cmgs_EmployeeList.find(emp => emp.EMP_ID === selectedEmployeeId);
                //if (selectedEmployee) {
                //    // Load employee information (you can customize this part)
                //    loadEmployeeInfo(selectedEmployee);
                //    // Execute server event to load employee assets
                //    loadEmployeeAssets(selectedEmployeeId);
                //}
                $employeeDropdown.val($("#hdnEmployeeId").val()).change();

            }


        },
        error: function (request, status, error) {
            toastr.clear();
            toastr.clear(); NioApp.Toast(JSON.parse(request.responseText).message, 'error', { position: 'top-right' });
        }


    });



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

            }).bind("loaded.jstree", function () {
                if ($("#hdnSelectedNode").val() != null && $("#hdnSelectedNode").val() != "") {
                    selectedIds = $("#hdnSelectedNode").val();
                    console.log("Loaded Ids:", selectedIds);
                    var node = $("#orgTree").jstree(true).get_node(selectedIds);
                    if (node==null) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'الوحدة التنظيمية غير موجودة ، يرجي مراجعة الشئون الإدارية او نقل إستمارة العهدة '

                        })

                        return;
                    }
                    handelSelectedNode(selectedIds);
                    $("#orgTree").jstree("select_node", selectedIds).trigger("select_node.jstree");

                } else {
                    //CLear Tree Selections
                    // alert("herer");
                    $('#orgTree').jstree("deselect_all");
                }

            });
            hideProcessLoader();

        }
        , beforeSend: function () {  },
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


function loadEmployeeInfo(employee) {



    var selectedEmployee = cmgs_EmployeeList.filter(emp => parseInt(emp.EMP_ID) === parseInt(employee.EMP_ID));
    console.log(selectedEmployee);
    
    if (selectedEmployee != null && selectedEmployee.length > 0) {



        var _empInfo = `<div style="margin:20px;"><div class="EmpHeader ` + (selectedEmployee[0].EMP_STATUS == 'active' ? "bg-success" :"bg-danger")+`">الموظف : ` + selectedEmployee[0].EMP_NAME + `</div>
            <div class="EmpHeader" style="background: #000 !important;">المدير : `+ selectedEmployee[0].EMPMANAGERNAME + `</div>
            <ul class="list list-checked text-primary">
            
            
            <li><i class=""></i>الجهة:  `+ selectedEmployee[0].ORG_NAME + ` </li>
            <li><i class=""></i>الأمانة:  `+ selectedEmployee[0].AMANA_NAME + ` </li>
            <li><i class=""></i>الإدارة:  `+ selectedEmployee[0].DEPT_NAME + `</li>
            <li><i class=""></i>المراقبة:  `+ selectedEmployee[0].DIV_NAME + `</li>
            <li><i class=""></i>القسم:  `+ selectedEmployee[0].SEC_NAME + `</li>
            <li><hr/></li>

            <li><i class=""></i>الوظيفة : `+ selectedEmployee[0].JOB_NAME + `   </li>
            </ul>
            </div>`;

        $("#cboxLoadedContent").html(_empInfo);
        $("#divSelectedEmployeeInfo").show();
    } else {
        alert("Couldn't load employee information")
        $("#divSelectedEmployeeInfo").hide();

    }
}


function loadEmployeeAssets(employeeId) {

    $('#hdnEmployeeId').val(employeeId);
    console.log($('#hdnEmployeeId').val());

    $('#btnReload').click();


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


function showProcessLoader() {
    $(".loader").show();
    $(".loader").fadeTo("slow", 0.7);
}
function hideProcessLoader() {
    $(".loader").hide();
    $("#progressbar").hide();
}

