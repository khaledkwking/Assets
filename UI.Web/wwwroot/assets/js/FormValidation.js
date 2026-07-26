
//function CheckAllDataGridCheckBoxes(aspCheckBoxID, checkVal) {
//    re = new RegExp(':' + aspCheckBoxID + '$')  //generated control name starts with a colon
//    for (i = 0; i < document.forms[0].elements.length; i++) {
//        elm = document.forms[0].elements[i]
//        if (elm.type == 'checkbox') {
//            if (elm.name.indexOf(aspCheckBoxID) != -1) {
//                elm.checked = checkVal
//            }
//        }
//    }
//}
function CheckAllDataGridCheckBoxes(aspCheckBoxID, checkVal) {
    var checkboxes = document.querySelectorAll('input[type="checkbox"][name*="' + aspCheckBoxID + '"]');

    checkboxes.forEach(function (checkbox) {
        checkbox.checked = checkVal;
    });
}

function isItemChecked() {
    for (i = 0; i < document.forms[0].elements.length; i++) {
        elm = document.forms[0].elements[i]
        if (elm.type == 'checkbox') {
            if (elm.name.indexOf("chkItem") != -1) {
                if (elm.checked) {
                    return true;
                }
            }
        }
    }
    return false;
}
function checkDelete(neglictValivation) {

    if (neglictValivation != true) {
        if (!isItemChecked()) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Please Select One record for action!'
                
            })
            return false;
        }

        //Swal.fire({
        //    title: 'Are you sure?',
        //    text: "You won't be able to revert this!",
        //    icon: 'warning',
        //    showCancelButton: true,
        //    confirmButtonColor: '#3085d6',
        //    cancelButtonColor: '#d33',
        //    confirmButtonText: 'Yes, delete it!'
        //}).then((result) => {
        //    if (result.isConfirmed) {
        //        Swal.fire(
        //            'Deleted!',
        //            'Your file has been deleted.',
        //            'success'
        //        )
        //    }
        //})

        return confirm("هل أنت متأكد أنك تريد حذف العناصر المحددة؟");
    }
    return true;

}
function listen(evnt, elem, func) {
    if (elem.addEventListener) // W3C DOM
        elem.addEventListener(evnt, func, false);

    else if (elem.attachEvent) { // IE DOM
        var r = elem.attachEvent("on" + evnt, func);
        return r;
    }
    //                else 
    //                    window.alert('I\'m sorry Dave, I\'m afraid I can\'t do that.');
}
var plus = new Image();
var minus = new Image();
function preloadImages() {
    //alert("PRE");
    //    plus.src = "<%= GetGlobalResourceObject("Utilities", "resourcespath")%>images/plus.gif";
    //minus.src = "<%= GetGlobalResourceObject("Utilities", "resourcespath")%>images/minus.gif";

}
listen("load", window, preloadImages);

function ToggleWindow(img, pnl) {
    // alert("ToggleWindow")
    var p = document.getElementById(pnl);
    //  alert(p);
    var i = document.getElementById(img);
    // alert(i);
    if (p.style.display == "none") {
        i.src = minus.src;
        p.style.display = "";
    }
    else {
        i.src = plus.src;
        p.style.display = "none";
    }
    //  alert("before out")
    //  alert(p.style.display);

}



function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
//function NumberKey(evt) {
//    var charCode = (evt.which) ? evt.which : event.keyCode
//    if (charCode == 13) {
//        var btn = getObjById("btnAddItem")
//        //alert("Enter Key "+btn.value);
//        btn.click();
//    }

//}



function getObjById(id) {
    for (var i = 0; i < document.forms[0].elements.length; i++) {
        elm = document.forms[0].elements[i]
        if (elm.id.indexOf(id) != -1) {
            return elm;
        }
    }
    return null;
}

function FocusInput() {
    // alert("FOCUS STARTED");
    var ff = document.getElementById("txtDefault").value;
    var bar = getObjById(barid);

    if (bar) {
        try {
            bar.focus();
            bar.focus();
        }
        catch (error) {
            setTimeout("FocusInput()", 1000);
        }
    }
    //alert("FOCUS END");
}
function FocusInput2() {
    // alert("FOCUS STARTED");
    var ff = document.getElementById("txtDefault").value;
    var bar = getObjById(barid2);

    if (bar) {
        try {
            bar.focus();

        }
        catch (error) {
            setTimeout("FocusInput2()", 1000);
        }
    }

}


