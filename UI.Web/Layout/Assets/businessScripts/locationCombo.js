var txtOwnerLocationCombo;
var txtToLocationCombo;
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

    fillLocationTree();
 
    $('#txtOwnerLocationCode').on('change', function () {
        if (txtOwnerLocationCombo != null) {
            var selectedLocation = txtOwnerLocationCombo.getSelectedIds();
            if (selectedLocation!=null) {
                $(".selectedLocation").val(selectedLocation[0]);
            }
            
        }
    });


    $('#txtToLocation').on('change', function () {
        if (txtToLocationCombo != null) {
            var selectedLocation = txtToLocationCombo.getSelectedIds();
            if (selectedLocation != null) {
                $(".selectedToLocation").val(selectedLocation[0]);
            }

        }
    });
};

 




function fillLocationTree() {
    
    $.ajax({
        url: "/api/hepler/GetLocationHera",
        dataType: 'json',
        type: 'get',
        data: {},
        success: function (treeData) {
            console.log(treeData);
            txtOwnerLocationCombo = $('#txtOwnerLocationCode').comboTree({
                source: treeData,
                isMultiple: false,
                collapse: false,
                 selected: [$(".selectedLocation").val()],
                selectableLastNode: false,
            });
            if ($('#txtOwnerLocationCode').length !== 0 && txtOwnerLocationCombo != null) {
                txtOwnerLocationCombo.setSelection([$(".selectedLocation").val()]);
               var selectedItem = $("ul").find("[data-id='" + $(".selectedLocation").val() + "']");
                selectedItem.addClass('comboTreeItemSelected');
                txtOwnerLocationCombo.dropDownScrollToHoveredItem(selectedItem);
            }


            txtToLocationCombo = $('#txtToLocation').comboTree({
                source: treeData,
                isMultiple: false,
                collapse: false,
                selected: [$(".selectedToLocation").val()],
                selectableLastNode: false,
            });
            
            if ($('#txtToLocation').length !==0 && txtToLocationCombo != null ) {
               
                txtToLocationCombo.setSelection([$(".selectedToLocation").val()]);
            }

        }
        , beforeSend: function () { },
    });
}





