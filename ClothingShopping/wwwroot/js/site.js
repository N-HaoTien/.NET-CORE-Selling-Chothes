// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

ShowInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
            console.log("Link là "+url)

        }
    })
}

JqueryAjax = form => {
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isSuccess) {
                    $("#View_All").html(res.html);
                    $("#form-modal").modal('hide');
                    $("#form-modal .modal-body").html('');
                    $("#form-modal .modal-title").html('');
                    console.log(res)

                }
                else {
                    console.log(res)
                    $("#form-modal .modal-body").html(res.html);
                }
            },
            error: function (err) {
                console.log(err);
            }

        })
    }
    catch (e) {
        console.log(e);
    }
    return false;
}

function ChangeStatus(Id) {
    try {
        $.ajax({
            type: "POST",
            url: "/Category/ChangeStatus?Id=" + Id,
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isSuccess) {
                    console.log(res)
                }
                else {
                    console.log(res)
                }
            },
            error: function (err) {
                console.log(err);
            }

        })
    }
    catch (e) {
        console.log(e);
    }
    return false;
}
function SearchByNameCategory() {
    try {
        var NameCategory = $("#NameCategory").val();
        console.log(NameCategory);
        $.ajax({
            type: "POST",
            url: "/Category/SearchCategory?NameCategory=" + NameCategory,
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isSuccess) {
                    $("#View_All").html(res.html);
                    toastr.options =
                    {
                        "closeButton": true,
                        "progressBar": true
                    }
                    toastr.success("Bạn đã xóa sản phẩm trong giỏ hàng", "Thông Báo");
                    console.log(res);
                }
                else {
                    $("#View_All").html(res.html);
                    console.log(res);
                }
            },
            error: function (err) {
                console.log(err);
            }

        })
    }
    catch (e) {
        console.log(e);
    }
    return false;
}

function DeleteCategory(url, Namecategory) {
    try {
        Swal.fire({
            title: 'Are you sure?',
            html: 'Bạn có muốn xóa sản phẩm <b>' + Namecategory + '</b> này không?',

            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    url: url,
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        if (res.isSuccess) {
                            $("#View_All").html(res.html);
                            console.log(res);
                            Swal.fire(
                                'Deleted!',
                                'Bạn đã xóa thành công sản phẩm <b>' + Namecategory + '</b> ',
                                'success'
                            )
                        }
                        else {

                            Swal.fire(
                                'Deleted!',
                                'Your file has been deleted.',
                                'error'
                            )
                            console.log(res);
                        }
                    },
                    error: function (err) {
                        console.log(err);
                    }
                });
            }
        })
    }
    catch (e) {
        console.log(e);
    }
    return false;
}