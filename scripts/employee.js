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


employee.save = function () {
    var rvtoken = $("input[name='__RequestVerificationToken']").val();
    var siteRoot = dnn.getVar("sf_siteRoot", "/");
    var saveEmployee = {};
    saveEmployee.Name = $('#Name').val();
    saveEmployee.Id = parseInt($('#Id').val());
    saveEmployee.Gender = $('#Gender').val();
    $.ajax({
        url: siteRoot + 'DesktopModules/MVC/Chart/Item/SaveEmployee/',
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(saveEmployee),
        headers: {
            "ModuleId": ModuleId,
            "TabId": TabId,
            "RequestVerificationToken": rvtoken
        },
        success: function (data) {
            $('#addEmployee').modal('hide');
            bootbox.alert(data.data.message);
        }
    });
}



employee.get = function (id) {
    var rvtoken = $("input[name='__RequestVerificationToken']").val();
    var ModuleId = $('#ModuleId').val();
    var TabId = $('#TabId').val();
    var siteRoot = dnn.getVar("sf_siteRoot", "/");
    $.ajax({
        url: siteRoot + 'DesktopModules/MVC/Chart/Item/GetEmployee',
        method: "GET",
        dataType: "json",
        data: { employeeId: id },
        headers: {
            "ModuleId": ModuleId,
            "TabId": TabId,
            "RequestVerificationToken": rvtoken
        },
        success: function (data) {
            var obj = JSON.parse(data.data);
            $('#desEmployee #Name').val(obj.Name);
            $('#desEmployee').find('#Id').text(id);
           $('#desEmployee #Gender').val(obj.Gender);
            $('#desEmployee #Gender').empty();
            $.each(obj, function (i, v) {
                $('#desEmployee #Gender').append(`<option value=${obj.Id} >${obj.Gender}</option>`)
            });
            //$('#desEmployee #Gender').val(obj.Gender);
           //$('#desEmployee #Gender').empty();
           // $('#desEmployee #Gender').append(`<option value=${obj.Id} >${obj.Gender}</option>`)
           // $('#desEmployee #Gender').append(`<option value=${obj.Id} >${obj.Gender}</option>`)
           // $('#desEmployee #Gender').append(`<option value=${obj.Id} >${obj.Gender}</option>`)
            $('#DetailEmployee').modal('show');
        }
    });
}


employee.delete = function (id) {
    //alert(id);
    var rvtoken = $("input[name='__RequestVerificationToken']").val();
    var ModuleId = $('#ModuleId').val();
    var TabId = $('#TabId').val();
    var siteRoot = dnn.getVar("sf_siteRoot", "/");
    bootbox.confirm({
        title: "Delete employee?",
        message: "Do you want to delete this employee.",
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> No'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Yes'
            }
        },
        callback: function (data) {
            if (data) {
                $.ajax({
                    url: siteRoot + 'DesktopModules/MVC/Chart/Item/DeleteEmployee',
                    method: "GET",
                    dataType: "json",
                    data: { Id: id },
                    headers: {
                        "ModuleId": ModuleId,
                        "TabId": TabId,
                        "RequestVerificationToken": rvtoken
                    },
                    success: function (data) {
                        //var obj = JSON.parse(data.data);
                        //alert(data.data);
                        //alert(obj.message);
                        bootbox.alert(data.data.message);
                    }
                });
            }
        }
    });
}

employee.initGender = function () {
    var rvtoken = $("input[name='__RequestVerificationToken']").val();
    var ModuleId = $('#ModuleId').val();
    var TabId = $('#TabId').val();
    var siteRoot = dnn.getVar("sf_siteRoot", "/");
    $.ajax({
        url: siteRoot + 'DesktopModules/MVC/Chart/Item/GetEmployees',
        method: "GET",
        dataType: "json",
        headers: {
            "ModuleId": ModuleId,
            "TabId": TabId,
            "RequestVerificationToken": rvtoken
        },
        success: function (data) {
            var obj = JSON.parse(data.data);
            $('#desEmployee #Gender').empty();
            $.each(obj, function (i, v) {
                $('#desEmployee #Gender').append(`<option value=${obj.Id} >${obj.Gender}</option>`)
            });
        }
    });
}

employee.init = function () {

    employee.initGender();
};

$(document).ready(function () {
    employee.init();
});