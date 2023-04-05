$(document).ready(function () {

    $("#btnSearch").click(function () {
        GetProducts(1);
    });

    $("#ddlTags").change(function () {
        GetProducts(1);
    });

    $('body').on('click', '.pagination-first,.pagination-previous,.pagination-page,.pagination-next,.pagination-last', function () {
        var pageNum = $(this).attr('data-pagenum');
        pageNum = app.IsNullOrWhiteSpace(pageNum) ? 1 : pageNum;
        GetProducts(pageNum);
    });
});

function GetProducts(pageNum) {    
    $("#pdt-tiles-container").html("");

    var params = {
        searchTerm: $.trim($("#SearchKeyword").val()),
        tagId: $("#ddlTags").val(),
        selectedPage: pageNum,
        pageSize: $("#PageSize").val()        
    }

    $.ajax({
        type: "GET",
        url: '/api/sitecore/Products/GetProducts',
        data: params,
        contentType: 'application/json; charset=utf-8',
        dataType: "html",
        beforeSend: function () {
            app.ShowWaitModal();
        },
        success: function (response) {
            setTimeout(function () {
                $("#pdt-tiles-container").html(response);
                app.HideWaitModal();
            }, 1000);
        },
        error: function (response) {
            app.HideWaitModal();
            console.log(response);
        }
    });
}