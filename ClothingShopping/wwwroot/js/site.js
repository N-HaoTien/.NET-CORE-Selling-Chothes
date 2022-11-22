// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

ShowInPopup2 = (Id) => {
    console.log("Id là : " + Id);
    $.ajax({
        type: "Get",
        url: '/Category/AddorUpdate?Id=' + Id,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html('Update Category');
            $("#form-modal").modal('show');
        }
    })
}

// Modal
ShowInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
            console.log("Link là " + url)
        }
    })
}

// Create/Udapte Category

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
                    console.log(res);
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
// Udapte Status Category

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

// Find Category by Name
function LoadPaginationCategory(NameCategory, datapageIndex) {
    try {
        $.ajax({
            type: 'Post',
            url: "/Category/SearchPagination",
            data: {
                Index: datapageIndex,
                keyword: NameCategory
            },
            dataType: "json",
            success: function (res) {
                if (res.isSuccess) {
                    $("#View_All_Category").html(res.html);
                    $("#ComponentCategory").html(res.componentHtml)
                    console.log(res);
                    console.log(datapageIndex);
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
$(document).ready(function (e)
{
    $("body").on("click", ".Categorypagination li a", function (event) {
        event.preventDefault();
        var page = $(this).attr('data-page');
        console.log('Event is Active' + 'Page is : ' + page);
        //load event pagination
        var txtSearch = $("#NameCategory").val();
        if (txtSearch != null) {
            LoadPaginationCategory(txtSearch, page)
        }
        else {
            LoadPaginationCategory(null, page)
        }

    });
})

function SearchCategory()
{
    var txtSearch = $("#NameCategory").val();
    if (txtSearch != null) {
        LoadPaginationCategory(txtSearch, 1)
    }
    else {
        LoadPaginationCategory(null, 1)
    }

}


// Delete Category by Id
// Name to display
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
                            toastr.options =
                            {
                                "closeButton": true,
                                "progressBar": true
                            }
                            toastr.success('Bạn đã xóa sản phẩm <b> ' + Namecategory + '</b> thành công!', "Thông Báo");
                        }
                        else {

                            Swal.fire(
                                'Deleted!',
                                'Your file has been deleted.',
                                'error'
                            )
                            toastr.options =
                            {
                                "closeButton": true,
                                "progressBar": true
                            }
                            toastr.error("Bạn đã xóa sản phẩm " + Namecategory + " thất bại!", "Thông Báo");
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

// Sidebar--EJ2

document.addEventListener('DOMContentLoaded', function () {
    dockbar = document.getElementById("sidebar").ej2_instances[0];
    document.getElementById('sidebar-toggle').onclick = function () {
        dockbar.toggle();
    };
});
// btn-dark/light
$(document).ready(function (e) {
    var btnswitch = $('#btn-switch');
    btnswitch.on('click', function (e) {
        var check = btnswitch.is(":checked", true);
        if (check) {
            $('body').css('background-color', "#12161d");
            $('#sidebar').css('background-color', '#12161d');
            $('#nav-bar').css('background-color', '#12161d');
            $('#sidebar-toggle').css('background-color', '#12161d');
            $('#sidebar-toggle').removeClass('text-black');
            $('#sidebar-toggle').addClass('text-white');
            console.log('Test');
            /*$('.e-menu-icon').css('color', 'Black');
            $('.e-menu-icon').css('font', 'bold');*/

            $('#Tbl-text-color').addClass("p-3 mb-2 bg-gradient-info text-white");
            $('body').css('color', "#fff");
            console.log('check');
        }
        else {
            $('body').css('background-color', "White");
            $('#sidebar').css('background-color', 'White');
            $('#sidebar-toggle').css('background-color', 'White');
            $('#sidebar-toggle').removeClass('text-white');
            $('#sidebar-toggle').addClass('text-black');
            /*            $('.e-menu-icon').css('color', 'White');
                        $('.e-menu-icon').css('font', 'bold');*/
            $('#nav-bar').css('background-color', 'White');
            $('#Tbl-text-color').removeClass("p-3 mb-2 bg-gradient-info text-white");
        }

    });

});



$(document).ready(function () {
    $('#profile').on('click', function () {
        $('.list-open').toggle();
        console.log('check');
    });
});

function LogOut(url) {
    try {
        $.ajax({
            type: "POST",
            url: '/Users/LogOut?returnUrl=' + url,
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isSuccess) {
                    console.log(res);
                    window.location.href = res.url;

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

/*jQuery(document).ready(function ($) {
    //   $('.fulltext').hide();

    $('.readmore').insertAfter('.product-description');

    $('.readmore').click(function (event) {
        event.preventDefault();
        var description = document.querySelector('.product-description');
        console.log(description.style.height)


        if (description.style.height === '') {
            description.style.height = 'auto';
        } else if (description.style.height === 'auto') {
            description.style.height = '';
        }
        else {
            description.style.height = '92px';
        }

        $(this).text($(this).text() == 'Read less...' ? 'Read more...' : 'Read less...');
    });
});*/