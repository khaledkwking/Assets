$(document).ready(function () {
   
    loadDashboard();
    //$(".iframe").colorbox({ iframe: true, width: "70%", height: "95%" });
    //$(".inline").colorbox({ inline: true, width: "50%" });

});
var selectedOrg = "0";
var AllRelatedOrgs = "0";
loadDashboard = function () {
    //moj 310
    //DrawEntityService(320);
    //DrawCasesAnaltics(391);
    DrawType();
    DrawCompany();
    DrawCasesAnaltics2(320);



    $("#divRequestCount").dxPieChart({
        dataSource: [{ type: ' عدد الطلبات القائمة', count: 91794 }, { type: 'عدد الطلبات المقدمة', count:8397 }],
        resolveLabelOverlapping: 'shift',
        legend: {
            horizontalAlignment: "center",
            verticalAlignment: "bottom",
            visible: false
        },
        series: [{
            argumentField: "type",
            valueField: "count",
            label: {
                visible: true,
                connector: {
                    visible: true,
                    width: 0.5
                },
                format: "fixedPoint",
                customizeText: function (point) {
                    return point.argumentText + ":" + "(" + point.percentText + ") " + point.valueText;
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
                        text: arg.seriesName + " : " + arg.percentText + " - " + arg.valueText
                    };
                }
            }


        }],
       


    });
    $("#divHousingRequestCount").dxPieChart({
        dataSource: [{ type: 'قسائم', count: 93740 }, { type: 'بيوت', count: 60971 }, { type: 'شقق', count: 869 }],
        resolveLabelOverlapping: 'shift',
        palette: "ocean",
        legend: {
            horizontalAlignment: "center",
            verticalAlignment: "bottom",
            visible: false
        },
        series: [{
            argumentField: "type",
            valueField: "count",
            label: {
                visible: true,
                connector: {
                    visible: true,
                    width: 0.5
                },
                format: "fixedPoint",
                customizeText: function (point) {
                    return point.argumentText + ":" + "(" + point.percentText + ") " + point.valueText;
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
                        text: arg.seriesName + " : " + arg.percentText + " - " + arg.valueText
                    };
                }
            }


        }],
       


    });

    

    $("#RequestGauage").dxCircularGauge({
        scale: {
            startValue: 0,
            endValue: 100,
            tickInterval: 10,
            label: {
                customizeText: function (arg) {
                    return arg.valueText + " %";
                }
            }
        },
        rangeContainer: {
            ranges: [
                { startValue: 0, endValue: 20, color: "#CE2029" },
                { startValue: 20, endValue: 50, color: "#FFD700" },
                { startValue: 50, endValue: 100, color: "#228B22" }
            ]
        },
        "export": {
            enabled: true
        },
        title: {
            //text: "نسبة إنجاز القرارات (" + DoneValue + "%)",
            //font: { family: 'Droid Arabic Kufi', size: 18 }
        },
        value: 39.7
        , rtlEnabled: true
    });
    $("#divHousingYearCount").dxChart({
        dataSource: [{year:1985,hcount:28},
            { year: 1986, hcount: 24 },
            { year: 1987, hcount: 22 },
            { year: 1988, hcount: 33 },
            { year: 1989, hcount: 27 },
            { year: 1990, hcount: 30 },
            { year: 1991, hcount: 28 },
            { year: 1992, hcount: 73 },
            { year: 1993, hcount: 64 },
            { year: 1994, hcount: 72 },
            { year: 1995, hcount: 98 },
            { year: 1996, hcount: 101 },
            { year: 1997, hcount: 123 },
            { year: 1998, hcount: 279 },
            { year: 1999, hcount: 349 },
            { year: 2000, hcount: 1899 },
            { year: 2001, hcount: 2514 },
            { year: 2002, hcount: 2972 },
            { year: 2003, hcount: 4009 },
            { year: 2004, hcount: 3870 },
            { year: 2005, hcount: 3832 },
            { year: 2006, hcount: 8460 },
            { year: 2007, hcount: 4336 },
            { year: 2008, hcount: 4135 },
            { year: 2009, hcount: 3792 },
            { year: 2010, hcount: 3949 },
            { year: 2011, hcount: 3838 },
            { year: 2012, hcount: 4185 },
            { year: 2013, hcount: 5388 },
            { year: 2014, hcount: 5595 },
            { year: 2015, hcount: 5439 },
            { year: 2016, hcount: 5042 },
            { year: 2017, hcount: 4930 },
            { year: 2018, hcount: 5210 },
            { year: 2019, hcount: 6171 },
            { year: 2020, hcount: 4225 }

        ],
        palette: "ocean",
        crosshair: {
            enabled: true,
            color: "#949494",
            width: 3,
            dashStyle: "dot",
            label: {
                visible: true,
                backgroundColor: "#949494",
                font: {
                    color: "#fff",
                    size: 12,
                }
            }
        },
        commonSeriesSettings: {
            argumentField: "year",
            valueField: "hcount",
            type: "spline",
            //label: {
            //    visible: true,
            //    format: {
            //        type: "fixedPoint",
            //        precision: 0
            //    }
            //}
        },
        //seriesTemplate: {
        //    nameField: "year",
        //    //customizeSeries: function (valueFromNameField) {
        //    //    return valueFromNameField === "USA" ? { color: "red" } : {};
        //    //}
        //},
        series: [{ valueField: "hcount", name: "طلبات الإسكان" }],

        legend: {
            verticalAlignment: "bottom",
            horizontalAlignment: "center",
            itemTextPosition: "right"
        },
        scrollBar: {
            visible: false
        },
        /*   palette: ["#00ced1", "#008000", "#ffd700", "#ff7f50"],*/
        onSeriesClick: function (e) {
            var series = e.target;
            series.isVisible() ? series.hide() : series.show();
        },
        title: {
            text: "  الطلبات الإسكانية حسب تاريخ الأولوية  ",
            font: { size: 20, family: 'Droid Arabic Kufi' },
            subtitle: {
                text: "إحصائية بعدد الطلبات الإسكانية القائمة حسب تاريخ الأولوية منذ سنة 1985"
            }
        },

        argumentAxis: {
            tickInterval: 5,
            //label: {
            //    overlappingBehavior: { mode: 'rotate', rotationAngle: 90 },
            //    font: { size: 12 }
            //},
            valueMarginsEnabled: false,
            discreteAxisDivisionMode: "crossLabels",
            grid: {
                visible: true
            }
        },
        tooltip: {
            enabled: true,
            customizeTooltip: function (arg) {
                return {
                    text: arg.seriesName + ": " + arg.value 
                };
            }
        }
    });

    $("#monthlyRent").dxChart({
        dataSource: [
            { Month: 'يناير', RentCount: 76197, RentType: ' مستفيدي بدل الايجار' },
            { Month: 'يناير', RentCount: 12541245, RentType: 'المبلغ الكلي لمستفيدي بدل الايجار' },

            { Month: 'فبراير', RentCount: 116197, RentType: ' مستفيدي بدل الايجار' },
            { Month: 'فبراير', RentCount: 18141245, RentType: 'المبلغ الكلي لمستفيدي بدل الايجار' },

            { Month: 'مارس', RentCount: 146197, RentType: ' مستفيدي بدل الايجار' },
            { Month: 'مارس', RentCount: 20141245, RentType: 'المبلغ الكلي لمستفيدي بدل الايجار' },

            { Month: 'ابريل', RentCount: 96197, RentType: ' مستفيدي بدل الايجار' },
            { Month: 'ابريل', RentCount: 10141245, RentType: 'المبلغ الكلي لمستفيدي بدل الايجار' },

            { Month: 'مايو', RentCount: 86197, RentType: ' مستفيدي بدل الايجار' },
            { Month: 'مايو', RentCount: 15141245, RentType: 'المبلغ الكلي لمستفيدي بدل الايجار' },

            { Month: 'يونيو', RentCount: 126197, RentType: ' مستفيدي بدل الايجار' },
            { Month: 'يونيو', RentCount: 19141245, RentType: 'المبلغ الكلي لمستفيدي بدل الايجار' },

        ],
        palette: "soft",
        crosshair: {
            enabled: true,
            color: "#949494",
            width: 3,
            dashStyle: "dot",
            label: {
                visible: true,
                backgroundColor: "#949494",
                font: {
                    color: "#fff",
                    size: 12,
                }
            }
        },
        commonSeriesSettings: {
            argumentField: "Month",
            valueField: "RentCount",
            type: "line",
            label: {
                visible: true,
                format: {
                    type: "fixedPoint",
                    precision: 0
                }
                /*format: "thousands"*/
            }
        },
        seriesTemplate: {
            nameField: "RentType",
            //customizeSeries: function (valueFromNameField) {
            //    return valueFromNameField === "USA" ? { color: "red" } : {};
            //}
        },

        legend: {
            verticalAlignment: "bottom",
            horizontalAlignment: "center",
            itemTextPosition: "right"
        },
        scrollBar: {
            visible: false
        },
        /*   palette: ["#00ced1", "#008000", "#ffd700", "#ff7f50"],*/
        onSeriesClick: function (e) {
            var series = e.target;
            series.isVisible() ? series.hide() : series.show();
        },
        title: {
            text: " مؤشــــر مستفيدي طلب الايــــجار ",
             font: { size: 20, family: 'Droid Arabic Kufi' },
            subtitle: {
                text: "التقرير الإحصائي لعدد المستفيدين من بدل الإيجار والمبلغ الكلي المخصص لهم ",
                font: { size: 14, family: 'Arial' },
            }
        },

        argumentAxis: {
            tickInterval: 5,
            //label: {
            //    overlappingBehavior: { mode: 'rotate', rotationAngle: 90 },
            //    font: { size: 12 }
            //},
            valueMarginsEnabled: false,
            discreteAxisDivisionMode: "crossLabels",
            grid: {
                visible: true
            }
        },
        tooltip: {
            enabled: true,
            customizeTooltip: function (arg) {
                return {
                    text: arg.seriesName + ":  " + arg.value  
                };
            }
        }
    });



    $("#CourtCases").dxChart({
        dataSource: [
            {
                "Court": "بند الميزانية رقم 1 ",
                "CaseType": "الموضوعات المنجزة",
                "CaseCount": 143026
            },
            {
                "Court": "بند الميزانية رقم2 ",
                "CaseType": "الموضوعات المنجزة",
                "CaseCount": 57280
            },
            {
                "Court": "بند الميزانية رقم3 ",
                "CaseType": "الموضوعات المنجزة",
                "CaseCount": "15579"
            },
            {
                "Court": "بند الميزانية رقم4 ",
                "CaseType": "الموضوعات المنجزة",
                "CaseCount": 33398
            },
            {
                "Court": "بند الميزانية رقم5 ",
                "CaseType": "الموضوعات المنجزة",
                "CaseCount": 36217
            },
            {
                "Court": "بند الميزانية رقم 1 ",
                "CaseType": "الموضوعات المتبقية",
                "CaseCount": 657225
            },
            {
                "Court": "بند الميزانية رقم2 ",
                "CaseType": "الموضوعات المتبقية",
                "CaseCount": 21822
            },
            {
                "Court": "بند الميزانية رقم3 ",
                "CaseType": "الموضوعات المتبقية",
                "CaseCount": 931
            },
            {
                "Court": "بند الميزانية رقم4 ",
                "CaseType": "الموضوعات المتبقية",
                "CaseCount": 120124
            },
            {
                "Court": "بند الميزانية رقم5 ",
                "CaseType": "الموضوعات المتبقية",
                "CaseCount": 17761
            }

        ],
        crosshair: {
            enabled: true,
            color: "#949494",
            width: 3,
            dashStyle: "dot",
            label: {
                visible: true,
                backgroundColor: "#949494",
                font: {
                    color: "#fff",
                    size: 12
                }
            }
        },
        commonSeriesSettings: {
            argumentField: "Court",
            valueField: "CaseCount",
            type: "Stackedbar"
            //label: {
            //    visible: true,
            //    format: {
            //        type: "fixedPoint",
            //        precision: 0
            //    }
            //}
        },
        seriesTemplate: {
            nameField: "CaseType",
            //customizeSeries: function (valueFromNameField) {
            //    return valueFromNameField === "USA" ? { color: "red" } : {};
            //}
        },

        legend: {
            verticalAlignment: "top",
            horizontalAlignment: "right",
            itemTextPosition: "right"
        },
        scrollBar: {
            visible: false
        },
        palette: "Office",
        onSeriesClick: function (e) {
            var series = e.target;
            series.isVisible() ? series.hide() : series.show();
        },
        title: {
            text: " إحصائية أعداد الموضوعات    ",
            font: { size: 20, family: 'Droid Arabic Kufi' },
            subtitle: { text: "تقرير اعداد الموضوعات طبقا لبنود الميزانية للسنة المالية الحالية     " }
        },

        argumentAxis: {
            tickInterval: 5,
            label: {
                overlappingBehavior: { mode: 'rotate', rotationAngle: 45 },
                font: { size: 12 }
            },
            valueMarginsEnabled: false,
            discreteAxisDivisionMode: "crossLabels",
            grid: {
                visible: true
            }
        },
        tooltip: {
            enabled: true,
            customizeTooltip: function (arg) {
                return {
                    text: arg.seriesName + ":" + arg.value
                };
            }
        }
    });

    $("#CourtCasesTotal").dxPieChart({
        dataSource: [
            { type: 'بند الميزانية رقم1', count: 800251 },
            { type: 'بند الميزانية رقم2', count: 79102 },
            { type: 'بند الميزانية رقم3', count: 16510 },
            { type: 'بند الميزانية رقم4', count: 153522 },
            { type: 'بند الميزانية رقم5', count: 53978 },
        ]

        ,
        resolveLabelOverlapping: 'shift',
        palette: "ocean",
        legend: {
            horizontalAlignment: "center",
            verticalAlignment: "bottom",
            visible: false
        },
        title: {
            text: " إجمالي الموضوعات      ",
            font: { size: 20, family: 'Droid Arabic Kufi' },
            subtitle: { text: "  إجمالي الموضوعات طبقا لبنود الميزانية للسنة المالية الحالية     " }
        },
        series: [{
            argumentField: "type",
            valueField: "count",
            label: {
                visible: true,
                connector: {
                    visible: true,
                    width: 0.5
                },
                format: "fixedPoint",
                customizeText: function (point) {
                    return point.argumentText + ":" + "(" + point.percentText + ") " + point.valueText;
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
                        text: arg.seriesName + " : " + arg.percentText + " - " + arg.valueText
                    };
                }
            }


        }],



    });
 
};

function DrawEntityService(entityId) {
    $.ajax({
        url: "/GovHub/getServicesAnalytics",
        dataType: 'json',
        type: 'get',
        data:
            { entityId: entityId}
        ,
        success: function (result) {
            $("#serviceAnaltyicsChart").dxChart({
                dataSource: result,
                palette: "office",
                //commonSeriesSettings: {
                //    argumentField: "Month",
                //    type: "spline",
                //    point: {
                //        hoverMode: "allArgumentPoints"
                //    }
                //},
                crosshair: {
                    enabled: true,
                    color: "#949494",
                    width: 3,
                    dashStyle: "dot",
                    label: {
                        visible: true,
                        backgroundColor: "#949494",
                        font: {
                            color: "#fff",
                            size: 12,
                        }
                    }
                },
                commonSeriesSettings: {
                    argumentField: "MonthName",
                    valueField: "RequestCount",
                    type: "bar"
                },
                seriesTemplate: {
                    nameField: "Notes",
                    //customizeSeries: function (valueFromNameField) {
                    //    return valueFromNameField === "USA" ? { color: "red" } : {};
                    //}
                },
                //series: [
                //    {
                //        valueField: "RequestCount", name: "إحصائيات تقديم طلب بيع/هبة/إرث", color: '#e884aa' },
                //    //{ valueField: "Backload40Full", name: "Backload 40 Full", color: '#9f5287' },
                //    //{ valueField: "Backload45Full", name: "Backload 45 Full", color: '#ffd365' },
                //    //{ valueField: "Backload20Empty", name: "Backload 20 Empty", color: '#02b6ad' },
                //    //{ valueField: "Backload40Empty", name: "Backload 40 Empty", color: '#b34446' },
                //    //{ valueField: "Backload45Empty", name: "Backload 45 Empty", color: '#5cb85c' }
                //],
                legend: {
                    verticalAlignment: "top",
                    horizontalAlignment: "right",
                    itemTextPosition: "right"
                },
                scrollBar: {
                    visible: false
                },
              
                onSeriesClick: function (e) {
                    var series = e.target;
                    series.isVisible() ? series.hide() : series.show();
                },
                title: {
                    text: "إحصائيات تقديم المعاملات",
                    font: { size: 20, family: 'Droid Arabic Kufi' },
                    subtitle: {
                        text: "عدد الطلبات الإالكترونية المقدمة للخدمات خلال السنة الحالية",
                        font: { size: 12, family: 'Arial' },                    }
                },

                argumentAxis: {
                    tickInterval: 5,
                    label: {
                        overlappingBehavior: { mode: 'rotate', rotationAngle: 90 },
                        font: { size: 12 }
                    },
                    valueMarginsEnabled: false,
                    discreteAxisDivisionMode: "crossLabels",
                    grid: {
                        visible: true
                    }
                },
                tooltip: {
                    enabled: true,
                    customizeTooltip: function (arg) {
                        return {
                            text: arg.seriesName + ":  " + arg.value 
                        };
                    }
                }
            });


        }
    });
}

function DrawCasesAnaltics(entityId) {
    $.ajax({
        url: "/GovHub/getPAHWAnalytics",
        dataType: 'json',
        type: 'get',
        data:
            { entityId: entityId, serviceTypeId: 1}
        ,
        success: function (result) {

            $("#divCasesAnalytics").dxChart({
                dataSource: result,
                palette: "office",
                crosshair: {
                    enabled: true,
                    color: "#949494",
                    width: 3,
                    dashStyle: "dot",
                    label: {
                        visible: true,
                        backgroundColor: "#949494",
                        font: {
                            color: "#fff",
                            size: 12,
                        }
                    }
                },
                commonSeriesSettings: {
                    argumentField: "Location",
                    valueField: "RecordCount",
                    type: "bar",
                    label: {
                        visible: false,
                        format: {
                            type: "fixedPoint",
                            precision: 0
                        }
                    }
                },
                seriesTemplate: {
                    nameField: "AnaliyticsType",
                    //customizeSeries: function (valueFromNameField) {
                    //    return valueFromNameField === "USA" ? { color: "red" } : {};
                    //}
                },
               
                legend: {
                    verticalAlignment: "top",
                    horizontalAlignment: "right",
                    itemTextPosition: "right"
                },
                scrollBar: {
                    visible: false
                },
             /*   palette: ["#00ced1", "#008000", "#ffd700", "#ff7f50"],*/
                onSeriesClick: function (e) {
                    var series = e.target;
                    series.isVisible() ? series.hide() : series.show();
                },
                title: {
                    text: " مشاريع المؤسسة العامة للرعاية السكنية خلال 2020م",
                    font: { size: 20, family: 'Droid Arabic Kufi' },
                    subtitle: { text: "إحصائية مشاريع المؤسسة العامة للرعاية السكنية خلال 2020 طبقا للحالة" }
                },

                argumentAxis: {
                    tickInterval: 5,
                    label: {
                        overlappingBehavior: { mode: 'rotate', rotationAngle: 45 },
                        font: { size: 12 }
                    },
                    valueMarginsEnabled: false,
                    discreteAxisDivisionMode: "crossLabels",
                    grid: {
                        visible: true
                    }
                },
                tooltip: {
                    enabled: true,
                    customizeTooltip: function (arg) {
                        return {
                            text: arg.seriesName + ":  " + arg.value 
                        };
                    }
                }
            });


        }
    });
}


function DrawCasesAnaltics2(entityId) {
    $.ajax({
        url: "/GovHub/getCasesAnalytics",
        dataType: 'json',
        type: 'get',
        data:
            { entityId: entityId, serviceTypeId: 2}
        ,
        success: function (result) {
            $("#divRealAnaltyics").dxChart({
                dataSource: result,
                palette: "ocean",
                crosshair: {
                    enabled: true,
                    color: "#949494",
                    width: 3,
                    dashStyle: "dot",
                    label: {
                        visible: true,
                        backgroundColor: "#949494",
                        font: {
                            color: "#fff",
                            size: 12,
                        }
                    }
                },
                commonSeriesSettings: {
                    argumentField: "CaseType",
                    valueField: "CasesCount",
                    type: "spline",
                    label: {
                        visible: true,
                        format: {
                            type: "fixedPoint",
                            precision: 0
                        }
                    }
                },
                seriesTemplate: {
                    nameField: "circuit",
                    //customizeSeries: function (valueFromNameField) {
                    //    return valueFromNameField === "USA" ? { color: "red" } : {};
                    //}
                },

                legend: {
                    verticalAlignment: "bottom",
                    horizontalAlignment: "center",
                    itemTextPosition: "right"
                },
                scrollBar: {
                    visible: false
                },
                /*   palette: ["#00ced1", "#008000", "#ffd700", "#ff7f50"],*/
                onSeriesClick: function (e) {
                    var series = e.target;
                    series.isVisible() ? series.hide() : series.show();
                },
                title: {
                    text: " مؤشــــر تـــداول العــقـود",
                    font: { size: 20, family: 'Droid Arabic Kufi' },
                    subtitle: {
                        text: "التقرير الإحصائي لحركة تداول العقار بإدارتي التسجيل العقاري والتوثيق" }
                },

                argumentAxis: {
                    tickInterval: 5,
                    //label: {
                    //    overlappingBehavior: { mode: 'rotate', rotationAngle: 90 },
                    //    font: { size: 12 }
                    //},
                    valueMarginsEnabled: false,
                    discreteAxisDivisionMode: "crossLabels",
                    grid: {
                        visible: true
                    }
                },
                tooltip: {
                    enabled: true,
                    customizeTooltip: function (arg) {
                        return {
                            text: arg.seriesName + ":  " + arg.value  
                        };
                    }
                }
            });


        }
    });
}

function DrawCompany(){
    $.ajax({
        type: 'POST',
        url: '/Admin/Pages/Home.aspx/GetChartDataCompany',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        

        success: function (result) {
            
            $("#divChartDataCompnay").dxPieChart({
                dataSource: JSON.parse(result.d)
                ,
                resolveLabelOverlapping: 'shift',
                palette: "ocean",
                legend: {
                    horizontalAlignment: "center",
                    verticalAlignment: "bottom",
                    visible: false
                },
                title: {
                    text: "الشركات",
                    font: { size: 20, family: 'Droid Arabic Kufi' },
                    subtitle: { text: "  إجمالي القيمة المالية للعقود الخاصة بكل شركة" }
                },
                series: [{
                    argumentField: "CompanyName",
                    valueField: "contract_Amount",
                    label: {
                        visible: true,
                        connector: {
                            visible: true,
                            width: 0.5
                        },
                        format: "fixedPoint",
                        customizeText: function (point) {
                            return point.argumentText + ":" + "(" + point.percentText + ") " + point.valueText;
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
                                text: arg.seriesName + " : " + arg.percentText + " - " + arg.valueText
                            };
                        }
                    }


                }],


                
            });
        },
         error: function (xhr, textStatus, errorThrown) {
             alert('Error loading data: ' + textStatus + ' - ' + errorThrown);
        }
    });
}
function DrawType() {
    $.ajax({
        type: 'POST',
        url: '/Admin/Pages/Home.aspx/GetChartDataType',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',


        success: function (result) {
            $("#divChartDataType").dxChart({
                dataSource: result,
     
                series: {
                    argumentField: 'TypeName',
                    valueField: 'contract_Amount',
                    name: 'My oranges',
                    type: 'bar',
                    color: '#ffaa66',
                },
           

              
                title: {
                    text: " انواع التعاقد    ",
                    font: { size: 20, family: 'Droid Arabic Kufi' },
                    subtitle: { text: "إجمالي القيمة الماليةللعقود الخاصة بكل نوع تعاقد" }
                }

               
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error loading data: ' + textStatus + ' - ' + errorThrown);
        }
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
    //return arr.reduce((a, d) => {
    //    if (!a.includes(d[prop])) { a.push(d[prop]); }
    //    return a;
    //}, []);


    //return data.reduce(function (acc, cur) {
    //    cur.ProductHandlingTypes
    //        .map(function (obj) {
    //            return obj.Name
    //        })
    //        .forEach(function (n) {
    //            return acc[n] = (acc[n] || 0) + 1
    //        })

    //    return acc
    //}, {});


}
function call_cbox(url) {
    // alert(url);
    $.colorbox({ width: "80%", height: "95%", iframe: true, href: url });
}
function call_cboxSmall(url) {
    // alert(url);
    $.colorbox({ width: "400px", height: "400px", iframe: true, href: url });
}




 



