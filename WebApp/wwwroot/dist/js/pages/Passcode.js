
var cId = 0;
$(document).ready(function () {

    GetAllTools();
});



function GetAllTools() {

    debugger
    postRequest("/Dashboard/GetAllTool", null, function (res) {

        if (res.status == 200) {

            debugger

            if (res.data && res.data != null) {

                $("#DDLToolID").html("").append(`<option value="-1">Select Tool</option>`);
                $.each(res.data, function (index, value) {

                    $("#DDLToolID").append(`<option toolurl="${value.toolURL}" value="${value.toolID}">${value.toolName}</option>`);

                });
            }
            GetAllPasscode();
        }
        if (res.status == 401) {

            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 403) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 500) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 600) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
    });
}


function GetToolEmailById(Id) {
    postRequest("/Dashboard/GetToolEmailById/"+Id, null, function (res) {

        if (res.status == 200) {
            if (res.data && res.data != null) {

                $("#DDLCredential").html("").append(`<option value="-1">Select Credential</option>`);
                $.each(res.data, function (index, value) {

                    $("#DDLCredential").append(`<option CEmail="${value.email}" value="${value.credentialID}">${value.email}</option>`);
                    

                });

                $("#DDLCredential").val(cId);
            }
        }
        if (res.status == 401) {

            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 403) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 500) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 600) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
    });
}


function GetAllPasscode() {

    $("#PasscodeTable").DataTable().destroy();

    $("#PasscodeTable").DataTable({
        "bAutoWidth": true,
        "bFilter": true,
        "bSort": true,
        "aaSorting": [[0]],

        "ajax": {
            "url": "/Dashboard/GetAllPasscode",
            "type": "POST",
            "dataType": "json",
            "dataSrc": function (data) {

                 GetAllClients();

                if (data.status == 200) {

                    return data.data;
                }

                if (data.status == 401) {

                    Swal.fire(
                        'Oops',
                        res.responseMsg,
                        'warning'
                    )
                }
                if (data.status == 403) {

                    Swal.fire(
                        'Oops',
                        res.responseMsg,
                        'warning'
                    )
                }

                if (data.status == 420) {

                    Swal.fire(
                        'Oops',
                        res.responseMsg,
                        'warning'
                    )
                }

                if (data.status == 500) {
                    Swal.fire(
                        'Oops',
                        res.responseMsg,
                        'warning'
                    )
                }

                if (data.status == 600) {

                    Swal.fire(
                        'Oops',
                        res.responseMsg,
                        'warning'
                    )
                }


            }

        },

        "columns": [

            {
                "data": "toolName",
                "name": "toolName",
                "autoWidth": true
            },

            {
                "data": "email",
                "name": "email",
                "autoWidth": true
            },

            {
                "data": "passcodeValue",
                "name": "passcodeValue",
                "autoWidth": true
            },
            {
                "data": "isActive",
                "name": "isActive",
                "autoWidth": true,
                "render": function (full, type, data, meta) {

                    if (full) {

                        return '<span class="badge badge-pill badge-primary">Yes</span>'
                    }
                    else {

                        return '<span class="badge badge-pill badge-danger">No</span>'
                    }
                }
            },

            {
                "data": "startTime",
                "name": "startTime",
                "autoWidth": true,
                "render": function (full, type, data, meta) {
                    debugger
                    return moment(full).format('MM-DD-YYYY h:mm:ss A')
                }

            },
            {
                "data": "endTime",
                "name": "endTime",
                "autoWidth": true,
                "render": function (full, type, data, meta) {
                    return moment(full).format('MM-DD-YYYY h:mm:ss A')
                }
            },
            {
                "data": "totalMinutes",
                "name": "totalMinutes",
                "autoWidth": true
            },
            {
                "data": "usedMinutes",
                "name": "usedMinutes",
                "autoWidth": true
            },
            {
                "render": function (full, type, data, meta) {
                    return `
                     <button type="button" id="Btn_Edit" data_id="${data.passcodeID}" class="btn btn-primary btn-xs" style="border-radius: 50%!important; width: 35px!important; aspect-ratio: 1!important;">
                         <i class="fa fa-edit" style="font-size: 18px!important;"></i>
                     </button>`
                }
            },
        ]
    });
}


function GetAllClients() {

    debugger
    postRequest("/Client/GetAllClients", null, function (res) {

        if (res.status == 200) {

            debugger

            if (res.data && res.data != null) {

                $("#DDLClient").html("").append(`<option value="-1">Select User</option>`);
                $.each(res.data, function (index, value) {

                    $("#DDLClient").append(`<option value="${value.id}">${value.email}</option>`);

                });
            }
        }
        if (res.status == 401) {

            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 403) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 500) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 600) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
    });
}


$("#Btn_Submit").click(function () {

    var obj = {
        PasscodeID: Number($("#PasscodeID").val()),
        ToolID: Number($("#DDLToolID").val()),
        CredentialID: Number($("#DDLCredential").val()),
        IsActive: $("#DDLIsActive").val() == "1" ? true : false,
        PasscodeValue: $("#Passcode").val(),
        StartTime: $("#StartTime").val(),
        EndTime: $("#EndTime").val(),
        TotalMinutes: $("#TotalMinutes").val(),
        ClientId: Number($("#DDLClient").val())
    }
    postRequest("/Dashboard/CreateUpdatePasscode", obj, function (res) {

        if (res.status == 200) {
            if (res.data && res.data != null) {

                Swal.fire(
                    obj.PasscodeID > 0 ? "Update Successfuly" : 'Create Successfuly',
                    res.responseMsg,
                    'success'
                );
                $("#Btn_Clear").click();
                GetAllPasscode();
            }
        }
        if (res.status == 401) {

            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 403) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 500) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 600) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
    });

});

$(document).on("click", "#Btn_Edit", function () {

    var Id = Number($(this).attr("data_id"));
    postRequest("/Dashboard/GetPasscodeDetailsById/" + Id, null, function (res) {

        if (res.status == 200) {
            
            if (res.data && res.data != null) {
                cId = res.data[0].credentialID;
                $("#Btn_Submit").text("Update").removeClass("btn-primary").addClass("btn-success");
                $("#PasscodeID").val(res.data[0].passcodeID);
                $("#DDLToolID").val(res.data[0].toolID);
               
                $("#ToolURL").val(res.data[0].toolURL);
                $("#DDLIsActive").val(res.data[0].isActive == true ? "1" : "0");
                $("#Passcode").val(res.data[0].passcodeValue);
                $("#StartTime").val(res.data[0].startTime);
                $("#EndTime").val(res.data[0].endTime);
                $("#TotalMinutes").val(res.data[0].totalMinutes);
                $("#DDLClient").val(res.data[0].clientId).change();
                $("#DDLToolID").change();

                $("html, body").animate({ scrollTop: 0 }, "slow");
            }
        }
        if (res.status == 401) {

            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 403) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 500) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
        if (res.status == 600) {
            Swal.fire(
                'Oops',
                res.responseMsg,
                'warning'
            )
        }
    });
});


$("#DDLToolID").change(function () {

    $("#ToolURL").val($('option:selected', this).attr('toolurl'));
    debugger
    GetToolEmailById($(this).val());

});


$("#Btn_Clear").click(function () {
    $("#Btn_Submit").text("Submit").removeClass("btn-success").addClass("btn-primary")
    $("#PasscodeID").val("0");
    $("#DDLCredential").val("0").removeAttr("disabled", true);
    $("#DDLToolID").val("0").removeAttr("disabled", true);
    $("#ToolURL").val("");
    $("#DDLIsActive").val("0");
    $("#Passcode").val("");
    $("#StartTime").val("");
    $("#EndTime").val("");
    $("#TotalMinutes").val("");
});

function GeneratePassword(length = 25) {
    var charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+";
    var password = "";
    for (var i = 0; i < length; i++) {
        var randomIndex = Math.floor(Math.random() * charset.length);
        password += charset.charAt(randomIndex);
    }
    $("#Passcode").val(password);
}
function postRequest(url, requestData, handledata) {
    $.ajax({
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        dataType: "json",
        url: url,
        headers: {

        },

        data: JSON.stringify(requestData),
        success: function (data, textstatus, xhr) {
            handledata(data);
        },
        error: function (xhr, textstatus, errorThrown) {
            Swal.fire({
                title: "Error",
                text: res.responseMsg,
                icon: "error",
                dangerMode: true,
                showCancelButton: false,
                confirmButtonText: "OK",
            });

        }
    });
}