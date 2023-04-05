<%@ Page Title="Sitecore Form Report" Language="C#" MasterPageFile="~/sitecore/admin/Stratum/Default.Master" AutoEventWireup="true" CodeBehind="scform-report.aspx.cs" Inherits="Stratum.Feature.AdminPages.sitecore.admin.Stratum.scform_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Sitecore Form Report</title>
    <link href="/assets/stratum/css/vendor/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="/assets/stratum/js/vendor/jquery.dataTables.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Sitecore Form Report</h3>
    <br />
    <div class="row">
        <div class="col-12">
            <form runat="server">
                <asp:HiddenField ID="hdnSessionId" runat="server" ClientIDMode="Static" />
                <div class="mb-3">
                    <div class="row g-3 align-items-center">
                        <div class="col-auto">
                            <label class="col-form-label">Form</label>
                        </div>
                        <div class="col-auto">
                            <asp:DropDownList ClientIDMode="Static" ID="ddlForms" runat="server" CssClass="form-select" AppendDataBoundItems="true" AutoPostBack="false">
                                <asp:ListItem Value="" Text="Select Form"></asp:ListItem>
                            </asp:DropDownList>
                            <span id="spForms" class="validation-msg">Select a form</span>
                        </div>
                    </div>
                </div>
                <div class="mb-3">
                    <div class="row g-3 align-items-center">
                        <div class="col-auto">
                            <label class="col-form-label">From Date</label>
                        </div>
                        <div class="col-auto">
                            <input type="date" id="txtFromDate" class="form-control">
                        </div>
                        <div class="col-auto">
                            <label class="col-form-label">To Date</label>
                        </div>
                        <div class="col-auto">
                            <input type="date" id="txtToDate" class="form-control">
                        </div>
                        <div class="col-auto">
                            <span id="spDates" class="validation-msg">Invalid date(s). From & To dates cannot be later than today. 'To' date should be later than 'From' date.</span>
                        </div>
                    </div>
                </div>

                <div class="mb-3 clearfix">
                    <button type="button" id="btnSubmit" class="btn btn-primary float-end">Submit</button>
                </div>
                <hr />
                <div class="mb-3 divError" id="divError">
                    <label class="font-red">Error</label>
                    <span id="spError" class="spError"></span>
                </div>
                 <div id="divDownload" class="mb-3 clearfix" style="display:none">
                    <button type="button" id="btnDownload" class="btn btn-warning float-end">Download</button>
                </div>
                <div class="mb-3" id="divResult">
                </div>
            </form>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            SetDefaultDates();

            $("#btnSubmit").click(function () {
                ClearResult();
                if (IsValidData()) {
                    GetFormData($("#ddlForms").val(), $("#txtFromDate").val(), $("#txtToDate").val());
                }                
            });

            $("#btnDownload").click(function () {
                app.ShowWaitModal();
                setTimeout(function () {
                    app.DownloadData($("#hdnSessionId").val(), 1, $("#ddlForms option:selected").text());
                    app.HideWaitModal();
                }, 1000);
            });
        });

        ///set default dates in mm/dd/yyyy format
        function SetDefaultDates() {
            var d = new Date().toLocaleDateString().split('/');
            var dateValue = d[2] + "-" + ("0" + d[0]).slice(-2) + "-" + ("0" + d[1]).slice(-2);
            $("#txtFromDate").val(dateValue);
            $("#txtToDate").val(dateValue);
        }

        function IsValidData() {
            var isValid = true;
            if (app.IsNullOrWhiteSpace($("#ddlForms").val())) {
                isValid = false;
                $("#spForms").show();
            }

            if (!AreInputDatesValid()) {
                isValid = false;
                $("#spDates").show();
            }

            return isValid;
        }

        function AreInputDatesValid() {
            var areDatesValid = false;
            var fromDate = $("#txtFromDate").val();
            var toDate = $("#txtToDate").val();
            if ((!app.IsNullOrWhiteSpace(fromDate)) && (!app.IsNullOrWhiteSpace(toDate))) {
                var today = new Date().setHours(0, 0, 0, 0);
                var fromDate = new Date(fromDate).setHours(0, 0, 0, 0);
                var toDate = new Date(toDate).setHours(0, 0, 0, 0);
                areDatesValid = fromDate <= today && fromDate <= toDate && toDate <= today;
            }            

            return areDatesValid;
        }

        function ClearResult() {
            $(".validation-msg").hide();
            $("#divDownload").hide();
            $("#divResult").html("");
        }

        function GetFormData(selectedFormId, selectedFromDate, selectedToDate) {
            var submitSessionId = $("#hdnSessionId").val();
            $.ajax({
                type: "POST",
                url: "scform-report.aspx/GetData",
                data: JSON.stringify({ formId: selectedFormId, fromDate: selectedFromDate, toDate: selectedToDate, sessionId: submitSessionId }),
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
                            $("#divResult").html(data.StatusMessage);

                            if ($('#tblFormData').length) {
                                $('#tblFormData').DataTable();
                                $("#divDownload").show();
                            }
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
