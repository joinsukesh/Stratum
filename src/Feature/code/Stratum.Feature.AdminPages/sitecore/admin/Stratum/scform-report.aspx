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
                <asp:ScriptManager ID="ScriptManger1" runat="Server"></asp:ScriptManager>
                <asp:HiddenField ID="hdnSessionId" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnIsPostBack" runat="server" ClientIDMode="Static" Value="0"/>
                
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
                            <asp:TextBox ID="txtFromDate" runat="server" ClientIDMode="Static" type="date" CssClass="form-control" EnableViewState="true"></asp:TextBox>
                        </div>
                        <div class="col-auto">
                            <label class="col-form-label">To Date</label>
                        </div>
                        <div class="col-auto">
                            <asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static" type="date" CssClass="form-control" EnableViewState="true"></asp:TextBox>
                        </div>
                        <div class="">
                            <span id="spDates" class="validation-msg">Invalid date(s). From & To dates cannot be later than today. 'To' date should be later than 'From' date.</span>
                        </div>
                    </div>
                </div>

                <div class="mb-3 clearfix">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" ClientIDMode="Static" CssClass="btn btn-primary float-end" Text="SUBMIT" />
                </div>
                <hr />
                <div class="mb-3 divError" id="divError">
                    <label class="font-red">ERROR: </label>
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div id="divDownload" class="mb-3 clearfix">
                    <asp:Button ID="btnDownload" runat="server" ClientIDMode="Static" CssClass="btn btn-warning float-end" Text="DOWNLOAD" Visible="false" />
                </div>
                <div class="mb-3" id="divResult_FormData">
                    <asp:UpdatePanel ID="upFormData" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvFormData" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-sm table-striped thead-dark"
                                ClientIDMode="Static" Width="100%" ShowHeaderWhenEmpty="true" EnableViewState="true" OnRowCreated="gvForData_RowCreated"
                                AllowPaging="true" OnPageIndexChanging="gvFormData_PageIndexChanging" PageSize="10" AllowSorting="true" OnSorting="OnSorting">
                                <EmptyDataTemplate>
                                    <div align="center">No records found</div>
                                </EmptyDataTemplate>
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" FirstPageText="First" LastPageText="Last" PageButtonCount="5" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="gvFormData" EventName="PageIndexChanging" />
                            <asp:AsyncPostBackTrigger ControlID="gvFormData" EventName="Sorting" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </form>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            SetDefaultDates();

            $("#btnSubmit").click(function () {
                ClearResult();
                return IsValidData();
            });

            $("#btnDownload").click(function () {
                app.ShowWaitModal();
                setTimeout(function () {
                    app.DownloadData($("#hdnSessionId").val(), 1, $("#ddlForms option:selected").text());
                    app.HideWaitModal();
                }, 5000);
            });
        });

        Date.prototype.toDateInputValue = (function () {
            var local = new Date(this);
            local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
            return local.toJSON().slice(0, 10);
        });

        ///set default dates in mm/dd/yyyy format
        function SetDefaultDates() {
            var isPostBack = $("#hdnIsPostBack").val();

            /// set current date only on initial page load.
            if (isPostBack == 0) {
                $('#txtFromDate').val(new Date().toDateInputValue());
                $('#txtToDate').val(new Date().toDateInputValue());
            }
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
            $("#btnDownload").hide();
        }

    </script>
</asp:Content>
