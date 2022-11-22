


var CheckboxCategories = function () {
    var ListCategoryId = [];
    var ItemsId = "";
    $("#CategoriesDiv input[type=checkbox]").each(function (index, val) {
        var CategoryId = $(val).attr("id");
        var isChecked = $("#" + CategoryId).is(":checked", true);
        if (isChecked) {
            ListCategoryId.push(CategoryId);
        }
        ItemsId = ListCategoryId.toString();
        //$("#SizeIDs").val(ListSizeId.join());
    })
    console.log("ListCategories : " + ItemsId);
};

var OnclickCheckbox = function () {
    var ListSizeId = [];
    var ItemsId = "";

    $("#sizeDiv input[type=checkbox]").each(function (index, val) {
        var SizeId = $(val).attr("id");
        var SizeName = $(val).attr("data-name");
        console.log(SizeName);
        console.log(SizeId);
        var isChecked = $("#" + SizeId).is(":checked", true);
        if (isChecked) {
            ListSizeId.push(SizeId);
        }
        ItemsId = ListSizeId.toString();
        $("#SizeIDs").val(ListSizeId.join());
        console.log("ListSize : " + ItemsId);
    })
}

