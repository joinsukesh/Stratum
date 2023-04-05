

$(function () {
    app.AppInit();

    /// Reposition when the loading modal is shown
    $('.mdlWait').on('show.bs.modal', app.RepositionModal);

    /// Reposition when the window is resized
    $(window).on('resize', function () {
        $('.mdlWait:visible').each(app.RepositionModal);
    });
});

let waitModal;

var app = {

    status: {
        Success: "Success",
        Error: "Error"
    },

    AppInit() {
        app.WaitModelInit();
        app.ToastrInit();
    },

    WaitModelInit() {
        waitModal = new bootstrap.Modal(document.getElementById('mdlWait'), {
            backdrop: 'static',
            keyboard: false
        });
    },

    ToastrInit() {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-center",
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": "3000",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
    },

    /* Vertically center loading modal*/
    RepositionModal() {
        var modal = $(this), dialog = modal.find('.modal-dialog');
        modal.css('display', 'block');

        /// Dividing by two centers the modal exactly, but dividing by three
        /// or four works better for larger screens.
        dialog.css("margin-top", Math.max(0, ($(window).height() - dialog.height()) / 2));
    },

    ShowWaitModal() {
        waitModal.show();
    },

    HideWaitModal() {
        waitModal.hide();
    },

    DisableForm() {
        toastr.remove();
        $("*", "form").prop('disabled', true);
    },

    EnableForm() {
        $("*", "form").prop('disabled', false);
    },

    DefaultResetForm() {
        ///remove all validation msgs
        $(".field-validation-error").remove();

        ///clear all textbox values
        $("input[type='text'], input[type='email'], input[type='password'], input[type='number'], textarea").each(function () {
            $(this).val("");
        });

        ///uncheck all radiobuttons
        $("input[type='radio']").prop("checked", "").removeAttr("checked");

        ///uncheck all checkboxes
        $("input[type='checkbox']").prop("checked", "").removeAttr("checked");

        ///set dropdown list to first option
        $("select").prop('selectedIndex', 0);
    },

    IsNullOrWhiteSpace(value) {
        return value == null || value == undefined || value.length <= 0 || value == "" || $.trim(value) == "";
    },

    ShowToastr(isError, title, message) {
        if (isError) {
            if (app.IsNullOrWhiteSpace(title)) {
                toastr.error(message);
            }
            else {
                toastr.error(message, title);
            }
        }
        else {
            if (app.IsNullOrWhiteSpace(title)) {
                toastr.success(message);
            }
            else {
                toastr.success(message, title);
            }
        }
    },

    ClearErrors() {
        $(".spError").html("");
        $("#divError").hide();
    },

    DownloadData(sessionId, fileType, fileName) {
        $("#d-iframe").remove();
        var iframe = document.createElement("iframe");
        iframe.setAttribute("id", "d-iframe");
        iframe.src = encodeURI("/download/default.aspx?id=" + sessionId + "&type=" + fileType + "&name=" + fileName);
        iframe.style.cssText = 'display:none';
        document.body.appendChild(iframe);
        return false;
    },

    GetQueryStringParams() {
        const params = new Proxy(new URLSearchParams(window.location.search), {
            get: (searchParams, prop) => searchParams.get(prop),
        });
        return params;
    },

    GetQueryStringParamByName(paramName) {
        const params = new Proxy(new URLSearchParams(window.location.search), {
            get: (searchParams, prop) => searchParams.get(prop),
        });
        return params[paramName];
    },

    ///Verifies if startdate <= today && startdate <= enddate
    AreInputDatesValid(startDate, endDate) {
        var areDatesValid = false;
        startDate = $.trim(startDate);
        endDate = $.trim(endDate);
        if ((!app.IsNullOrWhiteSpace(startDate)) && (!app.IsNullOrWhiteSpace(endDate))) {
            var today = new Date().setHours(0, 0, 0, 0);
            var fromDate = new Date(startDate).setHours(0, 0, 0, 0);
            var toDate = new Date(endDate).setHours(0, 0, 0, 0);
            areDatesValid = fromDate <= today && fromDate <= toDate;
        }

        return areDatesValid;
    }
};