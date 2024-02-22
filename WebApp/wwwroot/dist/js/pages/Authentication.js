$("#Btn_Authentication").click(function () {


    let data = {
        Email: $("#Email").val(),
        Password: $("#Password").val(),
    }

    postRequest('/Account/Authentication', data, function (res) {

        if (res.status == 200) {

            if (res.data != null) {

                swal({
                    title: "Success",
                    text: res.responseMsg,
                    icon: "success",
                    dangerMode: false,
                })
                window.location.href = "/Dashboard/Dashboard";

            }
        }
        if (res.status == 304) {

            swal({
                title: "Error",
                text: res.responseMsg,
                icon: "error",
                dangerMode: true,
            })
        }
        if (res.status == 305) {

            swal({
                title: "Error",
                text: res.responseMsg,
                icon: "error",
                dangerMode: true,
            })
        }
        if (res.status == 401) {

            swal({
                title: "Error",
                text: res.responseMsg,
                icon: "error",
                dangerMode: true,
            })
        }
        if (res.status == 403) {

            swal(res.responseMsg, {
                icon: "error",
                title: "Error",
            });
        }
        if (res.status == 320) {

            swal({
                title: "Error",
                text: res.responseMsg,
                icon: "error",
                dangerMode: true,
            })
        }
        if (res.status == 500) {

            swal({
                title: "Error",
                text: res.responseMsg,
                icon: "error",
                dangerMode: true,
            })
        }
        if (res.status == 600) {

            swal({
                title: "Warning",
                text: res.responseMsg,
                icon: "warning",
                dangerMode: true,
            })

        }
    });


});

function postRequest(url, requestData, handledata) {
    $.ajax({
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        dataType: "json",
        url: url,
        data: JSON.stringify(requestData),
        success: function (data, textStatus, xhr) {

            handledata(data);
        },
        error: function (xhr, textStatus, errorThrown) {
            swal({
                title: "Error",
                text: "Something Went Wrong!",
                icon: "error",
                dangerMode: true,
            })
        }
    });
}