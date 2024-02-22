$(document).ready(function () {
    GetAllTools();
});


function GetAllTools() {

    $("#ToolTable").DataTable().destroy();

    $("#ToolTable").DataTable({
        "bAutoWidth": true,
        "bFilter": true,
        "bSort": true,
        "aaSorting": [[0]],

        "ajax": {
            "url": "/Dashboard/GetAllTool",
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
                "render": function (full, type, data, meta) {
                    return `
                     <button type="button" id="Btn_Edit" data_id="${data.toolID}" class="btn btn-primary btn-xs" style="border-radius: 50%!important; width: 35px!important; aspect-ratio: 1!important;">
                         <i class="fa fa-edit" style="font-size: 18px!important;"></i>
                     </button>`
                }
            },
        ]
    });
}

$("#Btn_Submit").click(function () {

    var obj = {
        ToolID:Number($("#ToolID").val()),
        ToolName: $("#ToolName").val(),
        ToolURL: $("#ToolURL").val()
    }
    postRequest("/Dashboard/CreateUpdateTool", obj, function (res) {

            if (res.status == 200) {
                if (res.data && res.data != null) {
                    $("#Btn_Clear").click();
                    Swal.fire(
                        obj.ToolID > 0 ? "Update Successfuly" : 'Create Successfuly',
                        res.responseMsg,
                        'success'
                    );
                    

                    GetAllTools();
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
    postRequest("/Dashboard/GetToolDetailsById/"+Id,null, function (res) {

        if (res.status == 200) {

            if (res.data && res.data != null) {

                $("#Btn_Submit").text("Update").removeClass("btn-primary").addClass("btn-success");
                $("#ToolID").val(res.data[0].toolID),
                $("#ToolName").val(res.data[0].toolName),
                $("#ToolURL").val(res.data[0].toolURL)
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
    $("#ToolID").val("0");
    $("#ToolName").val("");
    $("#ToolURL").val("");
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