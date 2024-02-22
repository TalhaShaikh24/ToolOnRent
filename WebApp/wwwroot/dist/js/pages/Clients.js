$(document).ready(function () {
  
    GetAllClients();
});

function GetAllClients() {

    $("#ClientsTable").DataTable().destroy();

    $("#ClientsTable").DataTable({
        "bAutoWidth": true,
        "bFilter": true,
        "bSort": true,
        "aaSorting": [[0]],

        "ajax": {
            "url": "/Client/GetAllClients",
            "type": "POST",
            "dataType": "json",
            "dataSrc": function (res) {

                if (res.status == 200) {

                    return res.data;
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

                if (res.status == 420) {

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


            }

        },

        "columns": [

            {
                "data": "firstName",
                "name": "firstName",
                "autoWidth": true
            },

            {
                "data": "lastName",
                "name": "lastName",
                "autoWidth": true
            },

            {
                "data": "email",
                "name": "email",
                "autoWidth": true
            },
            {
                "data": "contactNumber",
                "name": "contactNumber",
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
                "render": function (full, type, data, meta) {

                    debugger

                    if (data.isActive) {

                      return `
                     <button type="button" id="Btn_Approval" data_id="${data.id}" class="btn btn-danger btn-xs">
                      Rejected
                     </button>`
                    }
                    else
                    {
                      return `
                     <button type="button" id="Btn_Approval" data_id="${data.id}" class="btn btn-primary btn-xs">
                      Approved
                     </button>`
                    }
                  
                }
            },
        ]
    });
}


$(document).on("click", "#Btn_Approval", function () {

    var Id = Number($(this).attr("data_id"));
    postRequest("/Client/ClientApproval/"+Id,null, function (res) {

        if (res.status == 200) {
            if (res.data && res.data != null) {

                Swal.fire(
                   'Approval Successfuly',
                    res.responseMsg,
                    'success'
                );
                GetAllClients();
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


})


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