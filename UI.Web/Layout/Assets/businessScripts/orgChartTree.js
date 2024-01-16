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
    $('#CMGS-OrgChart').stiffChart({
        lineColor: '#0fac81',
        layoutType: 'vertical',
        lineWidth: 2,
        lineShape: 'curved',
        childCounter: true,
        activeClass: 'chart-active',
        bootstrapPopover: true,
        enableZoom: true
    });
};
 
 

 
 




