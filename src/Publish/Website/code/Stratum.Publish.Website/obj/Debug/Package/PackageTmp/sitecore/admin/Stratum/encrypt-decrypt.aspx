<%@ Page Title="Encrypt Decrypt" Language="C#" MasterPageFile="~/sitecore/admin/Stratum/Default.Master" AutoEventWireup="true" CodeBehind="encrypt-decrypt.aspx.cs" Inherits="Stratum.Feature.AdminPages.sitecore.admin.Stratum.encrypt_decrypt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Enrypt-Decrypt</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Encrypt Decrypt</h3>
    <br />
    <div class="row">
        <div class="col-10">
            <form runat="server">
                <div class="mb-3">
                    <div class="form-check form-check-inline">
                        <input class="form-check-input" type="radio" name="ed" checked value="1">
                        <label class="form-check-label">Encrypt</label>
                    </div>
                    <div class="form-check form-check-inline">
                        <input class="form-check-input" type="radio" name="ed" value="2">
                        <label class="form-check-label">Decrypt</label>
                    </div>
                </div>                
                <div class="mb-3" id="divEncrypt">
                    <label class="form-label">String to Encrypt</label>
                    <textarea id="txtStringToEncrypt" class="form-control" cols="4"></textarea>
                </div>

                <div class="mb-3" id="divDecrypt" style="display: none;">
                    <label class="form-label">String to Decrypt</label>
                    <textarea id="txtStringToDecrypt" class="form-control" cols="4"></textarea>
                </div>

                <div class="mb-3" id="divKey">
                    <label class="form-label">Secret Key</label>
                    <input id="txtSecretKey" class="form-control" type="text" />
                    <div class="form-text">If this field is empty, system will take the <i>PassPhrase</i> declared here - <i>\App_Config\Include\zzz.Stratum\Foundation\Stratum.Foundation.Common.config</i></div>
                </div>
                <br />
                <div class="mb-3 clearfix">
                    <button type="button" id="btnSubmit" class="btn btn-primary float-end">Submit</button>
                </div>
                <hr />
                <div class="mb-3 divError" id="divError">
                    <label class="form-label font-red">Error</label>
                    <span id="spError" class="spError"></span>
                </div>
                <div class="mb-3" id="divResult">
                    <label class="form-label">Result</label>
                    <textarea id="txtResult" class="form-control" rows="4" readonly></textarea>
                </div>
            </form>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $("input[name='ed']").change(function () {
                ClearResult();

                if ($(this).val() == 1) {
                    $("#divEncrypt").show();
                    $("#divDecrypt").hide();
                }
                else if ($(this).val() == 2) {
                    $("#divEncrypt").hide();
                    $("#divDecrypt").show();
                }
            });

            $("#btnSubmit").click(function () {
                ClearResult();
                var action = $("input[name='ed']:checked").val();
                var value = "";

                if (action == 1) {
                    value = $("#txtStringToEncrypt").val();
                }
                else if (action == 2) {
                    value = $("#txtStringToDecrypt").val();
                }

                var passPhrase = $.trim($("#txtSecretKey").val());
                EncryptDecrypt(action, value, passPhrase);
            });
        });

        function ClearResult() {
            $("#txtResult").val("");
            app.ClearErrors();
        }

        function EncryptDecrypt(action, value, passPhrase) {
            var postUrl = action == 1 ? "encrypt-decrypt.aspx/EncryptString" : "encrypt-decrypt.aspx/DecryptString";
            $.ajax({
                type: "POST",
                url: postUrl,
                data: JSON.stringify({ inputString: value, secretKey: passPhrase }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: "true",
                cache: "false",
                beforeSend: function () {
                    app.ShowWaitModal();
                },
                success: function (data) {
                    data = JSON.parse(data.d);

                    if (data != null && data != undefined) {
                        if (data.StatusCode == 0) {
                            app.HideWaitModal();
                            $("#spError").html(data.StatusMessage);
                            $("#divError").show();
                            console.log(data);
                        }
                        else if (data.StatusCode == 1) {
                            app.HideWaitModal();
                            $("#txtResult").val(data.StatusMessage);
                        }
                        else if (data.StatusCode == 2) {
                            window.location = "/sitecore/login";
                        }
                    }
                    else {
                        app.HideWaitModal();
                    }
                },
                error: function (data) {
                    app.HideWaitModal();
                    console.log(data);
                }
            });
        }
    </script>
</asp:Content>
