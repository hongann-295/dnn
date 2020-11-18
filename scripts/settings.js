var pageChart = function () {
    var Page = pageChart;
    var chart;
    var cityId = $('#IdCities').val();
    var siteRoot = dnn.getVar("sf_siteRoot", "/");
    function requestData() {
        var AxisY = [];
        var AxisX = [];
        $('#selectY option:selected').each(function () {
            AxisY.push($(this).val());
        });
        if ($('#selectPerson option:selected').length > 0) {
            $('#selectPerson option:selected').each(function () {
                AxisX.push($(this).val());
            });
        }
        else {
            AxisX.push(cityId);
        }
        var car = [];
        if (AxisY != "") {
            $.ajax({
                url: siteRoot + 'DesktopModules/MVC/Chart/Item/GetResults',
                type: "POST",
                dataType: "json",
                data: {
                    cityId: cityId, AXisX: '' + AxisX + '', AxisY: '' + AxisY + '', Cater: $('#selectX option:selected').val()
                },
                headers: {
                    "ModuleId": $('#ModuleId').val(),
                    "TabId": $('#TabId').val(),
                    "RequestVerificationToken": $("input[name='__RequestVerificationToken']").val()
                },
                success: function (data) {
                    var obj = JSON.parse(data.data);
                    for (var i = 0; i < obj.length; i++) {
                        //alert(obj[i].data);
                        //car.push(obj[i].caterlog);
                        chart.addSeries({
                            name: obj[i].caterlog,
                            data: JSON.parse("[" + obj[i].data + "]")
                        });
                    }
                    var x = JSON.parse(data.axisx);

                    //alert(x);
                    //car.push(x);
                    //alert(car);
                    chart.xAxis[0].setCategories(x.split(','));
                    alertify.success('Vẽ biểu đồ thành công!');
                }
            });
        }
        else { alertify.error('Dữ liệu để vẻ biểu đồ chưa đầy đủ, vui lòng kiểm tra lại!'); }
    }
    Page.ShowChart = function (e) {
        var types = $('#selectChartId').find('li a.active').attr('id'); //$(this).attr('id');
        //alert(types);
        chart = new Highcharts.Chart({
            chart: {
                renderTo: 'container',
                type: types, //$("input[name='typechart']:checked").val(), //'column', //bar, area,column
                events: {
                    load: requestData
                }
            },
            title: {
                text: $('#name-chart').val(),
            },
            subtitle: {
                text: $('#des-chart').val()
            },
            xAxis: {
                title: {
                    text: $('#name-x').val()
                },
                categories: []
            },
            yAxis: {
                min: 0,
                title: {
                    text: $('#name-y').val()
                }
            },
            plotOptions: {
                column: {
                    dataLabels: {
                        enabled: true
                    }
                },
                spline: {
                    dataLabels: {
                        enabled: true
                    }
                },
                line: {
                    dataLabels: {
                        enabled: true
                    }
                },
                area: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            series: []
        });
        

    }
}