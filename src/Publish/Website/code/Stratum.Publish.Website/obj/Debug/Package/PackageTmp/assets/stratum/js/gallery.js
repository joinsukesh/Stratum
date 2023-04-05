$(document).ready(function () {

    $('body').on('click', '.pagination-first,.pagination-previous,.pagination-page,.pagination-next,.pagination-last', function () {
        GetImagesByPage(this);
    });
});

function GetImagesByPage(element) {
    var pageNum = $(element).attr('data-pagenum');
    pageNum = app.IsNullOrWhiteSpace(pageNum) ? 1 : pageNum;
    var sectionDatasourceId = $(element).parent().closest('ul.pagination').attr('data-datasourceId');
    var listContainerId = $(element).parent().closest('ul.pagination').attr('data-containerId');
    var params = {        
        datasourceId: sectionDatasourceId,
        selectedPage: pageNum,
        containerId: listContainerId
    }

    $.ajax({
        type: "GET",
        url: '/api/sitecore/StratumGallery/GetImagesByPage',
        data: params,
        contentType: 'application/json; charset=utf-8',
        dataType: "html",
        beforeSend: function () {
            app.ShowWaitModal();
        },
        success: function (response) {
            setTimeout(function () {
                $("#" + listContainerId).html(response);
                app.HideWaitModal();
            }, 1000);
        },
        error: function (response) {
            app.HideWaitModal();
            console.log(response);
        }
    });
}