$(document).ready(function () {

    /* SIGN-UP */

    $("#btnSUSubmit").click(function () {
        ClearSignUpForm();
    });

    /* END SIGN-UP */

    /* SIGN-IN */

    $("#btnSISubmit").click(function () {
        ClearSignInForm();
    });

    /* END SIGN-IN */

    /* FORGOT PASSWORD */

    $("#btnFpSubmit").click(function () {
        ClearFpForm();
    });

    /* END FORGOT PASSWORD */

    /* RESET PASSWORD */

    $("#btnRpSubmit").click(function () {
        ClearRpForm();
    });

    /* END RESET PASSWORD */
});

/* SIGN-UP */
function OnBegin_SU() {
    app.ShowWaitModal();
    app.DisableForm();
}

function OnSuccess_SU(data) {
    setTimeout(function () {
        //console.log(data);
        app.EnableForm();
        app.HideWaitModal();
        if (data != null && data != undefined) {
            if (data.StatusCode == 1) {
                app.DefaultResetForm();
                app.ShowToastr(false, app.status.Success, "");
                $("#divSignUpSubmitStatus").html(data.StatusMessage).css({ "color": "forestgreen" });
            }
            else {
                app.ShowToastr(true, app.status.Error, "");
                $("#divSignUpSubmitStatus").html(data.StatusMessage).css({ "color": "red" });
                console.log(data);
            }
        }
    }, 2000);
}

function OnFailure_SU(data) {
    app.HideWaitModal();
    app.EnableForm();
    $("#divSignUpSubmitStatus").html(data.StatusMessage).css({"color":"red"});
    app.ShowToastr(true, app.status.Error, "");
    console.log(data);
}

function ClearSignUpForm() {
    $("#formSignUp .field-validation-error").html("");
    $("#divSignUpSubmitStatus").html("");
}
/* END SIGN-UP */

/* SIGN-IN */
function OnBegin_SI() {
    app.ShowWaitModal();
    app.DisableForm();
}

function OnSuccess_SI(data) {
    setTimeout(function () {
        //console.log(data);
        
        if (data != null && data != undefined) {
            if (data.StatusCode == 1) {
                app.DefaultResetForm();
                var redirectUrl = app.GetQueryStringParamByName("return_url");
                redirectUrl = app.IsNullOrWhiteSpace(redirectUrl) ? "/" : redirectUrl;
                window.location.href = redirectUrl;
            }
            else {
                app.EnableForm();
                app.HideWaitModal();
                app.ShowToastr(true, app.status.Error, "");
                $("#divSignInSubmitStatus").html(data.StatusMessage).css({ "color": "red" });
                console.log(data);
            }
        }
    }, 2000);
}

function OnFailure_SI(data) {
    app.HideWaitModal();
    app.EnableForm();
    $("#divSignInSubmitStatus").html(data.StatusMessage).css({ "color": "red" });
    app.ShowToastr(true, app.status.Error, "");
    console.log(data);
}

function ClearSignInForm() {
    $("#formSignIn .field-validation-error").html("");
    $("#divSignInSubmitStatus").html("");
}
/* END SIGN-IN */

/* FORGOT PASSWORD */
function OnBegin_SFPE() {
    app.ShowWaitModal();
    app.DisableForm();
}

function OnSuccess_SFPE(data) {
    setTimeout(function () {
        //console.log(data);
        app.EnableForm();
        app.HideWaitModal();

        if (data != null && data != undefined) {
            if (data.StatusCode == 1) {
                app.DefaultResetForm();
                $("#divFpSubmitStatus").html(data.StatusMessage).css({ "color": "forestgreen" });
            }
            else {                
                app.ShowToastr(true, app.status.Error, "");
                $("#divFpSubmitStatus").html(data.StatusMessage).css({ "color": "red" });
                console.log(data);
            }
        }
    }, 2000);
}

function OnFailure_SI(data) {
    app.HideWaitModal();
    app.EnableForm();
    $("#divFpSubmitStatus").html(data.StatusMessage).css({ "color": "red" });
    app.ShowToastr(true, app.status.Error, "");
    console.log(data);
}

function ClearFpForm() {
    $("#formForgotPassword .field-validation-error").html("");
    $("#divFpSubmitStatus").html("");
}
/* END FORGOT PASSWORD */

/* RESET PASSWORD */
function OnBegin_RP() {
    app.ShowWaitModal();
    app.DisableForm();
}

function OnSuccess_RP(data) {
    setTimeout(function () {
        //console.log(data);
        app.EnableForm();
        app.HideWaitModal();

        if (data != null && data != undefined) {
            if (data.StatusCode == 1) {
                app.DefaultResetForm();
                app.ShowToastr(false, app.status.Success, "");
                $("#divRpSubmitSuccess").show();
            }
            else {
                app.ShowToastr(true, app.status.Error, "");
                $("#divRpSubmitFail").html(data.StatusMessage).css({ "color": "red" });
                console.log(data);
            }
        }
    }, 2000);
}

function OnFailure_RP(data) {
    app.HideWaitModal();
    app.EnableForm();
    $("#divRpSubmitFail").html(data.StatusMessage).css({ "color": "red" });
    app.ShowToastr(true, app.status.Error, "");
    console.log(data);
}

function ClearRpForm() {
    $("#formResetPassword .field-validation-error").html("");
    $("#divRpSubmitSuccess").hide();
    $("#divRpSubmitFail").html("");
}
/* END RESET PASSWORD */