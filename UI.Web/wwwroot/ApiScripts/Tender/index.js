$(document).ready(function () {
    loadDashboard();
    //$(".iframe").colorbox({ iframe: true, width: "70%", height: "95%" });
    //$(".inline").colorbox({ inline: true, width: "50%" });
 
});
loadDashboard = function (val) {
    DrawEmps(val);
    DrawAssetsType(val);
    DrawEmpHaveAssets(val);
};

function DrawEmps(val) {
    
        $.ajax({
            type: 'POST',
            url: '/Modules/Dashboard/Dashboard.aspx/GetChartDataEmps',
            //data: JSON.stringify({selectedValue1:selectedValue1,selectedValue2:selectedValue2,selectedValue3:selectedValue3}),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',


            success: function (result) {

                $("#divChartDataEmp").dxPieChart({
                    dataSource: JSON.parse(result.d)
                    ,
                    resolveLabelOverlapping: 'shift',
                    legend: {
                        horizontalAlignment: "center",
                        verticalAlignment: "bottom",
                        visible: false
                    },
                    title: {
                        text: "الموظفين",
                        font: { size: 20, family: 'DroidNaskh' },
                        subtitle: { text: "اجمالي عدد الموظفين " }
                    },
                    palette: ["#C5B991", "#91C59F", "#919DC5", "#C591B7"],
                    series: [{
                        argumentField: "Status",
                        valueField: "EmployeeCount",
                        label: {
                            visible: true,
                            connector: {
                                visible: true,
                                width: 0.5
                            },
                            format: "fixedPoint",
                            customizeText: function (point) {
                                return point.argumentText + "  :  " + point.valueText + " " + "(" + point.percentText + ") ";
                            }
                        },
                        onPointClick: function (e) {
                            var point = e.target;
                            toggleVisibility(point);
                        },
                        onLegendClick: function (e) {
                            var arg = e.target;
                            toggleVisibility(this.getAllSeries()[0].getPointsByArg(arg)[0]);
                        }
                        ,
                        tooltip: {
                            enabled: true,
                            customizeTooltip: function (arg) {
                                return {
                                    text: arg.seriesName + ": " + arg.value
                                };
                            }
                        }

                    }],



                });
            }
        });
}

function DrawAssetsType(val) {

    $.ajax({
        type: 'POST',
        url: '/Modules/Dashboard/Dashboard.aspx/GetChartDataAssetsType',
        //data: JSON.stringify({selectedValue1:selectedValue1,selectedValue2:selectedValue2,selectedValue3:selectedValue3}),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',


        success: function (result) {

            $("#divChartDataAssetsType").dxPieChart({
                dataSource: JSON.parse(result.d)
                ,
                resolveLabelOverlapping: 'shift',
                legend: {
                    horizontalAlignment: "center",
                    verticalAlignment: "bottom",
                    visible: false
                },
                title: {
                    text: "العهد",
                    font: { size: 20, family: 'DroidNaskh' },
                    subtitle: { text: "اجمالي عدد العهد " }
                },
                palette: ["#C5B991", "#91C59F", "#919DC5", "#C591B7"],
                series: [{
                    argumentField: "AssetType",
                    valueField: "AssetCount",
                    label: {
                        visible: true,
                        connector: {
                            visible: true,
                            width: 0.5
                        },
                        format: "fixedPoint",
                        customizeText: function (point) {
                            return point.argumentText + "  :  " + point.valueText+" " + "(" + point.percentText + ") " ;
                        }
                    },
                    onPointClick: function (e) {
                        var point = e.target;
                        toggleVisibility(point);
                    },
                    onLegendClick: function (e) {
                        var arg = e.target;
                        toggleVisibility(this.getAllSeries()[0].getPointsByArg(arg)[0]);
                    }
                    ,
                    tooltip: {
                        enabled: true,
                        customizeTooltip: function (arg) {
                            return {
                                text: arg.seriesName + ": " + arg.value
                            };
                        }
                    }

                }],



            });
        }
    });
}

function DrawEmpHaveAssets(val) {

    $.ajax({
        type: 'POST',
        url: '/Modules/Dashboard/Dashboard.aspx/GetChartDataEmpHaveAssets',
        //data: JSON.stringify({selectedValue1:selectedValue1,selectedValue2:selectedValue2,selectedValue3:selectedValue3}),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',


        success: function (result) {

            $("#divChartDataEmpHaveAssets").dxPieChart({
                dataSource: JSON.parse(result.d)
                ,
                resolveLabelOverlapping: 'shift',
                legend: {
                    horizontalAlignment: "center",
                    verticalAlignment: "bottom",
                    visible: false
                },
                title: {
                    text: "الموظفين",
                    font: { size: 20, family: 'DroidNaskh' },
                    subtitle: { text: "اجمالي عدد الموظفين الذين لديهم عهد والذين ليس لديهم عهد " }
                },
                palette: ["#C5B991", "#91C59F", "#919DC5", "#C591B7"],
                series: [{
                    argumentField: "AssetStatus",
                    valueField: "EmployeeCount",
                    label: {
                        visible: true,
                        connector: {
                            visible: true,
                            width: 0.5
                        },
                        format: "fixedPoint",
                        customizeText: function (point) {
                            return point.argumentText +  "  :  " + point.valueText + " " + "(" + point.percentText + ") ";
                        }
                    },
                    onPointClick: function (e) {
                        var point = e.target;
                        toggleVisibility(point);
                    },
                    onLegendClick: function (e) {
                        var arg = e.target;
                        toggleVisibility(this.getAllSeries()[0].getPointsByArg(arg)[0]);
                    }
                    ,
                    tooltip: {
                        enabled: true,
                        customizeTooltip: function (arg) {
                            return {
                                text: arg.seriesName + ": " + arg.value
                            };
                        }
                    }

                }],



            });
        }
    });
}