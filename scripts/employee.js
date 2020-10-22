var employee = {} || employee;



//employee.showEmployee = function () {
//    var rvtoken = $("input[name='__RequestVerificationToken']").val();
//    var siteRoot = dnn.getVar("sf_siteRoot", "/");
//    $.ajax({
//        url: siteRoot + 'DesktopModules/MVC/Chart/Item/GetEmployees/',
//        method: "GET",
//        dataType: "json",
//        success: function (data) {
//            $.each(data.listEmployee, function (i, v) {
//                $('#tbEmployee tbody').append(
//                    `<tr>
//                    <td class="table-plus">${v.Id}</td>
//                    <td>${v.Name}</td>     
//                    <td>${v.Gender}</td>   
//                    </tr>`
//                );
//            })

//        }
//    });
//}

employee.openAddEmployee = function () {
    $('#addEmployee').modal('show');
};


//employee.save = function () {
//    var rvtoken = $("input[name='__RequestVerificationToken']").val();
//    var siteRoot = dnn.getVar("sf_siteRoot", "/");
//    var saveEmployee = {};
//    saveEmployee.Name = $('#Name').val();
//    saveEmployee.Id = parseInt($('#Id').val());
//    saveEmployee.Gender = $('#Gender').val();
//    //saveEmployee.Gender = $("input[name='customRadio']:checked").val();
//    $.ajax({
//        url: siteRoot + 'DesktopModules/MVC/Chart/Item/SaveEmployee/',
//        method: "POST",
//        dataType: "json",
//        contentType: "application/json",
//        data: JSON.stringify(saveEmployee),
//        success: function (data) {
//            $('#addEmployee').modal('hide');
//            bootbox.alert(data.result.message);
//        }
//    });
//}



employee.get = function (id) {
    var rvtoken = $("input[name='__RequestVerificationToken']").val();
    var ModuleId = $('#ModuleId').val();
    var TabId = $('#TabId').val();
    var siteRoot = dnn.getVar("sf_siteRoot", "/");
    $.ajax({
        url: siteRoot + 'DesktopModules/MVC/Chart/Item/GetEmployee',
        method: "Post",
        dataType: "json",
        data: { employeeId: 5 },
        headers: {
            "ModuleId": ModuleId,
            "TabId": TabId,
            "RequestVerificationToken": rvtoken
        },
        success: function (data) {
            //alert(data.data);
            var obj = JSON.parse(data.data);
            //alert(obj.Name);
            //$('.desEmployee #Name').html(obj.Name);
            $('.desEmployee #Name').val(obj.Name);
            // $('#desEmployee').find('#Id').text(id);
            $('.desEmployee #Gender').val(obj.Gender);
            $('#DetailEmployee').modal('show');
        }
    });
}


//employee.delete = function (id) {
//    bootbox.confirm({
//        title: "Delete employee?",
//        message: "Do you want to delete this employee.",
//        buttons: {
//            cancel: {
//                label: '<i class="fa fa-times"></i> No'
//            },
//            confirm: {
//                label: '<i class="fa fa-check"></i> Yes'
//            }
//        },
//        callback: function (result) {
//            if (result) {
//                $.ajax({
//                    url: siteRoot + 'DesktopModules/MVC/Chart/Item/GetEmployee',
//                    method: "GET",
//                    dataType: "json",
//                    success: function (data) {
//                        bootbox.alert(data.result.message);
//                    }
//                });
//            }
//        }
//    });
//}