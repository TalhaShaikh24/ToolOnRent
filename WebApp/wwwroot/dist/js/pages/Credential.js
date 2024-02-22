$(document).ready(function () {
  
    GetAllTools();
});


function GetAllTools() {
    postRequest("/Dashboard/GetAllTool",null, function (res) {

        if (res.status == 200) {
            if (res.data && res.data != null) {

                $("#DDLToolID").html("").append(`<option value="-1">Select Tool</option>`);
                $.each(res.data, function (index, value) {

                    $("#DDLToolID").append(`<option value="${value.toolID}">${value.toolName}</option>`);

                });
            
                debugger
                GetAllCredential();
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
function GetAllCredential() {

    $("#CredentialTable").DataTable().destroy();

    $("#CredentialTable").DataTable({
        "bAutoWidth": true,
        "bFilter": true,
        "bSort": true,
        "aaSorting": [[0]],

        "ajax": {
            "url": "/Dashboard/GetAllCredential",
            "type": "POST",
            "dataType": "json",
            "dataSrc": function (data) {

                if (data.status == 200) {

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

                return data.data;
            }

        },

        "columns": [

            {
                "data": "toolName",
                "name": "toolName",
                "autoWidth": true
            },

            {
                "data": "toolURL",
                "name": "toolURL",
                "autoWidth": true
            },

            {
                "data": "email",
                "name": "email",
                "autoWidth": true
            },
            {
                "data": "password",
                "name": "password",
                "autoWidth": true,
                "render": function (full, type, data, meta) {
                    return "********"
                }
            },
            {
                "data": "cardNumber",
                "name": "cardNumber",
                "autoWidth": true
            },
            {
                "data": "expiryDate",
                "name": "expiryDate",
                "autoWidth": true,
                "render": function (full, type, data, meta) {
                    debugger
                    if (full == null && full == "0001-01-01T00:00:00") {

                        return "-"
                    }
                     
                    else {
                        return moment(full).format("YYYY-MM-DD")
                    }
                }
            },
            {
                "render": function (full, type, data, meta) {
                    return `
                     <button type="button" id="Btn_Edit" data_id="${data.credentialID}" class="btn btn-primary btn-xs" style="border-radius: 50%!important; width: 35px!important; aspect-ratio: 1!important;">
                         <i class="fa fa-edit" style="font-size: 18px!important;"></i>
                     </button>`
                }
            },
        ]
    });
}

$("#Btn_Submit").click(function () {

    var obj = {
        CredentialID: Number($("#CredentialID").val()),
        ToolID: Number($("#DDLToolID").val()),
        Email: $("#Email").val(),
        Password: $("#Password").val(),
        CardNumber: $("#CardNumber").val(),
        ExpiryDate: $("#ExpiryDate").val(),
    }
    postRequest("/Dashboard/CreateUpdateCredentials", obj, function (res) {

            if (res.status == 200) {
                if (res.data && res.data != null) {

                    Swal.fire(
                        obj.CredentialID > 0 ? "Update Successfuly" : 'Create Successfuly',
                        res.responseMsg,
                        'success'
                    );
                   $("#Btn_Clear").click();
                   GetAllCredential();
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

$(document).on("click","#Btn_Edit",function () {

   var Id = Number($(this).attr("data_id"));
        postRequest("/Dashboard/GetCredentialDetailsById/"+Id,null, function (res) {

        if (res.status == 200) {

            if (res.data && res.data != null) {
                debugger
                $("#Btn_Submit").text("Update").removeClass("btn-primary").addClass("btn-success");
                $("#CredentialID").val(res.data[0].credentialID);
                $("#ToolName").val(res.data[0].toolName);
                $("#ToolURL").val(res.data[0].toolURL);
                $("#DDLToolID").val(res.data[0].toolID).attr("disabled", true);
                $("#Email").val(res.data[0].email)
                $("#Password").val(res.data[0].password)
                $("#ExpiryDate").val(moment(res.data[0].expiryDate).format("YYYY-MM-DD"));
                $("#CardNumber").val(res.data[0].cardNumber);
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


$("#Btn_Clear").click(function () {
    $("#Btn_Submit").text("Submit").removeClass("btn-success").addClass("btn-primary")
    $("#DDLToolID").val("0").removeAttr("disabled", true);
    $("#CredentialID").val("0");
    $("#ToolName").val("");
    $("#ToolURL").val("");
    $("#Email").val("")
    $("#Password").val("")
    $("#CardNumber").val("");
    $("#ExpiryDate").val("");
});
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