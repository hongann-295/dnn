var pageArticle = function () {
    var Page = pageArticle;
    var chart;
    var articleId = $('#ArticleId').val();
    var siteRoot = dnn.getVar("sf_siteRoot", "/");
    function requestData() {
        var AxisY = [];
        var AxisX = [];
        $('#AXisY option:selected').each(function () {
            AxisY.push($(this).val());
        });
        if ($('#ArticleInTopic option:selected').length > 0) {
            $('#ArticleInTopic option:selected').each(function () {
                AxisX.push($(this).val());
            });
        }
        else {
            AxisX.push(articleId);
        }
        var car = [];
        if (AxisY != "") {
            $.ajax({
                url: Page.urls.chart,
                type: "POST",
                dataType: "json",
                data: {
                    articleId: articleId, AXisX: '' + AxisX + '', AxisY: '' + AxisY + '', Cater: $('#AXisX option:selected').val()
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
        var types = $('#child-datatopicTab').find('li a.active').attr('id'); //$(this).attr('id');
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
                text: $('.name-chart').val(),
            },
            subtitle: {
                text: $('.sub-title').val()
            },
            xAxis: {
                title: {
                    text: $('.name-x').val()
                },
                categories: []
            },
            yAxis: {
                min: 0,
                title: {
                    text: $('.name-y').val()
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