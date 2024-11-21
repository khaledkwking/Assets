$(document).ready(function () {
    //loadDashboard();
    //$(".iframe").colorbox({ iframe: true, width: "70%", height: "95%" });
    //$(".inline").colorbox({ inline: true, width: "50%" });

});
var selectedOrg = "0";
var AllRelatedOrgs = "0";
loadDashboard = function () {
    DrawType();
    DrawCompany();
    DrawCategory();
};


function DrawCategory() {
    {
        selectedValue1 = selectedDropDownValue;
        selectedValue2 = $(".daterange-ranges").data('daterangepicker').startDate.format('YYYY-MM-DD');
        selectedValue3 = $(".daterange-ranges").data('daterangepicker').endDate.format('YYYY-MM-DD');
        $.ajax({
            type: 'POST',
            url: '/Admin/Pages/Home.aspx/GetChartDataCategory?DeptId=' + selectedValue1 + '&From=' + selectedValue2 + '&To=' + selectedValue3,
            //data: JSON.stringify({selectedValue1:selectedValue1,selectedValue2:selectedValue2,selectedValue3:selectedValue3}),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',


            success: function (result) {

                $("#divChartDataCategory").dxPieChart({
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
                        text: "التصنيفات الرئيسية",
                        font: { size: 20, family: 'Droid Arabic Kufi' },
                        subtitle: { text: "  إجمالي القيمة المالية للعقود الخاصة بكل تصنيف" }
                    },
                    series: [{
                        argumentField: "TypeCategoryName",
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
                //alert('Error loading data: ' + textStatus + ' - ' + errorThrown);
            }
        });
    }
    function DrawType() {
        {
            selectedValue1 = selectedDropDownValue;
            selectedValue2 = $(".daterange-ranges").data('daterangepicker').startDate.format('YYYY-MM-DD');
            selectedValue3 = $(".daterange-ranges").data('daterangepicker').endDate.format('YYYY-MM-DD');
            $.ajax({
                type: 'POST',
                url: '/Admin/Pages/Home.aspx/GetChartDataType?DeptId=' + selectedValue1 + '&From=' + selectedValue2 + '&To=' + selectedValue3,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',


                success: function (result) {
                    $("#divChartDataType").dxChart({
                        dataSource: JSON.parse(result.d),

                        commonSeriesSettings: {
                            argumentField: 'TypeName',
                            valueField: 'contract_Amount',
                            type: 'bar',
                        },
                        seriesTemplate: {
                            nameField: 'TypeName'
                        },
                        tooltip: {
                            enabled: true,
                            contentTemplate(info, container) {
                                const contentItems = [`<div class='state-tooltip'><img src='images/flags/${
                                    info.point.data.name.replace(/\s/, '')}.svg' />`,
                                    "<h4 class='state'></h4>",
                                    "<div class='capital'><span class='caption'>Capital</span>: </div>",
                                    "<div class='population'><span class='caption'>Population</span>: </div>",
                                    "<div><span class='caption'>Area</span>: ",
                                    "<span class='area-km'></span> km<sup>2</sup> (",
                                    "<span class='area-mi'></span> mi<sup>2</sup>)",
                                    '</div></div>'];

                                const content = $(contentItems.join(''));

                                content.find('.state').text(info.argument);
                                content.find('.capital').append(document.createTextNode(info.point.data.capital));
                                content.find('.population').append(document.createTextNode(`${formatNumber(info.value)} people`));
                                content.find('.area-km').text(formatNumber(info.point.data.area));
                                content.find('.area-mi').text(formatNumber(0.3861 * info.point.data.area));

                                content.appendTo(container);
                            },
                        },

                        title: {
                            text: " انواع التعاقد    ",
                            font: { size: 20, family: 'Droid Arabic Kufi' },
                            subtitle: { text: "إجمالي القيمة المالية للعقود الخاصة بكل نوع تعاقد" }
                        }


                    });
                },
                error: function (xhr, textStatus, errorThrown) {
                    //alert('Error loading data: ' + textStatus + ' - ' + errorThrown);
                }
            });
        }
        function DrawCompany() {
            var selectedValue1 = selectedDropDownValue;
            var selectedValue2 = $(".daterange-ranges").data('daterangepicker').startDate.format('YYYY-MM-DD');
            var selectedValue3 = $(".daterange-ranges").data('daterangepicker').endDate.format('YYYY-MM-DD');

            $.ajax({
                type: 'POST',
                url: '/Admin/Pages/Home.aspx/GetChartDataCompany?DeptId=' + selectedValue1 + '&From=' + selectedValue2 + '&To=' + selectedValue3,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',


                success: function (result) {
                    $("#divChartDataCompnay").dxChart({
                        dataSource: JSON.parse(result.d),
                        palette: 'soft',
                        commonSeriesSettings: {
                            argumentField: 'CompanyName',
                            valueField: 'contract_Amount',
                            type: 'bar',
                        },
                        seriesTemplate: {
                            nameField: 'CompanyName'
                        },
                        tooltip: {
                            enabled: true,
                            contentTemplate(info, container) {
                                const contentItems = [`<div class='state-tooltip'><img src='images/flags/${
                                    info.point.data.name.replace(/\s/, '')}.svg' />`,
                                    "<h4 class='state'></h4>",
                                    "<div class='capital'><span class='caption'>Capital</span>: </div>",
                                    "<div class='population'><span class='caption'>Population</span>: </div>",
                                    "<div><span class='caption'>Area</span>: ",
                                    "<span class='area-km'></span> km<sup>2</sup> (",
                                    "<span class='area-mi'></span> mi<sup>2</sup>)",
                                    '</div></div>'];

                                const content = $(contentItems.join(''));

                                content.find('.state').text(info.argument);
                                content.find('.capital').append(document.createTextNode(info.point.data.capital));
                                content.find('.population').append(document.createTextNode(`${formatNumber(info.value)} people`));
                                content.find('.area-km').text(formatNumber(info.point.data.area));
                                content.find('.area-mi').text(formatNumber(0.3861 * info.point.data.area));

                                content.appendTo(container);
                            },
                        },

                        title: {
                            text: " الشركات   ",
                            font: { size: 20, family: 'Droid Arabic Kufi' },
                            subtitle: { text: "إجمالي القيمة المالية للعقود الخاصة بكل شركة" }
                        }


                    });
                },
                error: function (xhr, textStatus, errorThrown) {
                    //alert('Error loading data: ' + textStatus + ' - ' + errorThrown);
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








