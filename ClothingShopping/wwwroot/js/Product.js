



$("#selectPictures").change(function () {
    var pictures = this.files;
    console.log('Files : ' + this.files);
    var formData = new FormData();
    var totalFiles = pictures.length;
    for (var i = 0; i < totalFiles; i++) {
        formData.append("Picture", pictures[i]);
    }
    $.ajax({
        type: 'Post',
        url: '/Product/UploadMultiImage',
        data: formData,
        contentType: false,
        processData: false,
    }).done(function (response) {
        $("#picturesArea").html('');
        console.log(response.data);
        for (var i = 0; i < response.data.length; i++) {
            var pictures = response.data[i];
            var $imgHTML = $("#imageTemplate").clone();
            var a = $imgHTML.val();
            console.log('imgHTML : ' + a)
            $imgHTML.find("img").attr("src", "/Content/" + pictures.url);
            $imgHTML.find("img").attr("data-id", pictures.id);
            $("#picturesArea").append($imgHTML).html();
        }
    })
});
function removeMe(element) {
    var file = element.src.substring(31);
    console.log('file : ' + file)
    console.log('file : ' + element.src.substring(30))

    var a = file;
    $.ajax({
        type: 'Post',
        url: '/Product/MinusImage?file=' + file ,
        contentType: false,
        processData: false,
    }).done(function (response) {
        console.log('elemt : ' + a);
        element.remove();
        console.log(response.data);
    })
}
function LoadProductPagination(datapageIndex,txtSearch,Category,Status) {
    try {
        $.ajax({
            type: 'Post',
            url: "/Product/SearchPagination", 
            data: {
                Index: datapageIndex,
                keyword: txtSearch,
                CategoryId: Category,
                StatusProduct: Status
            },
            dataType: "json",
            success: function (res) {
                if (res.isSuccess) {
                    $("#View_All_Product").html(res.html);
                    $("#ComponentProduct").html(res.componentHtml)
                    console.log('data : ' + res.dataList)
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
$(document).ready(function (e) {
    $("body").on("click", ".Productpagination li a", function (event) {
        event.preventDefault();
        var page = $(this).attr('data-page');
        console.log('Event is Active' + 'Page is : ' + page);
        //load event pagination
        var txtSearch = $("#NameProduct").val();
        var CategoryId = $("#CategorySelected").val();
        var Status = $("#StatusSelected").val();
        console.log('Filter : ' + txtSearch + " " + CategoryId + " " + Status)
        if (txtSearch != null || CategoryId != null || Status != null) {
            LoadProductPagination(page, txtSearch, CategoryId, Status)

        }
        else {
            LoadProductPagination(page, null, null, null)

        }
    });
})

function SearchProduct() {
    var txtSearch = $("#NameProduct").val();
    var CategoryId = $("#CategorySelected").val();
    var Status = $("#StatusSelected").val();
    if (txtSearch != null || CategoryId != null || Status != null) {
        LoadProductPagination(1, txtSearch, CategoryId, Status)
    }
    else {
        LoadProductPagination(1, null, null, null)
    }

}