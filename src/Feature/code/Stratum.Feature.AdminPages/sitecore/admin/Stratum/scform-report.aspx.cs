
namespace Stratum.Feature.AdminPages.sitecore.admin.Stratum
{
    using global::Stratum.Feature.AdminPages.DbFactory;
    using global::Stratum.Feature.Base.Models;
    using global::Stratum.Foundation.Accounts.Services;
    using global::Stratum.Foundation.Common;
    using global::Stratum.Foundation.Common.Extensions;
    using global::Stratum.Foundation.Common.Utilities;
    using Sitecore;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Script.Serialization;
    using System.Web.Services;
    using SFA = global::Stratum.Feature.AdminPages;

    public partial class scform_report : System.Web.UI.Page
    {
        private AccountsService accountsService = new AccountsService();
        private string SitecoreFormsFolderId = "{B701850A-CB8A-4943-B2BC-DDDB1238C103}";
        private static int rowsPerPage = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (accountsService.IsAdministrator())
            {
                try
                {
                    hdnSessionId.Value = HelperUtility.GetRandomString(7);
                    BindFormsList();
                }
                catch (Exception ex)
                {
                    Log.Error("", ex, SFA.Constants.Stratum_AdminPage_Error);
                }
            }
            else
            {
                Response.Redirect(SFA.Constants.Paths.LoginPagePath);
            }
        }

        private void BindFormsList()
        {
            List<Item> lstForms = null;
            Item formsFolderItem = SitecoreUtility.GetItem(SitecoreFormsFolderId, CommonConstants.Databases.Master);

            if (formsFolderItem != null && formsFolderItem.HasChildren)
            {
                lstForms = formsFolderItem.GetChildItemsByTemplate(CommonTemplates.Form.ID, false, CommonConstants.Databases.Master);

                if (lstForms != null && lstForms.Count > 0)
                {
                    DataTable dt = new DataTable();
                    string col_Id = "Id";
                    string col_Name = "Name";
                    dt.Columns.Add(col_Id);
                    dt.Columns.Add(col_Name);
                    DataRow dr = null;

                    foreach (Item item in lstForms)
                    {
                        dr = dt.NewRow();
                        dr[col_Id] = item.ID.ToString();
                        dr[col_Name] = item.DisplayName;
                        dt.Rows.Add(dr);
                    }

                    ddlForms.DataSource = dt;
                    ddlForms.DataValueField = col_Id;
                    ddlForms.DataTextField = col_Name;
                    ddlForms.DataBind();
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static string GetData(string formId, string fromDate, string toDate, string sessionId)
        {
            AccountsService accountsService = new AccountsService();
            string output = "";
            BaseResponse result = new BaseResponse();
            HttpContext.Current.Session[sessionId] = null;

            try
            {
                if (accountsService.IsAuthenticated())
                {
                    DataTable dt = GetFormData(formId, fromDate, toDate, sessionId);
                    result.StatusMessage = GetTableHtml(dt);
                    result.StatusCode = 1;
                }
                else
                {
                    result.StatusCode = 2;
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex, SFA.Constants.Stratum_AdminPage_Error);
                result.StatusCode = 0;
                result.StatusMessage = SFA.Constants.GenericError;
                result.ErrorMessage = ex.Message;
            }

            output = new JavaScriptSerializer().Serialize(result);
            return output;
        }

        /// <summary>
        /// Gets all form entries for a form within a date range.
        /// First checks for the data in the session. If unavailable, gets from db
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        private static DataTable GetFormData(string formId, string fromDate, string toDate, string sessionId)
        {
            DataTable dt = null;

            if (HttpContext.Current.Session[sessionId] != null)
            {
                dt = (DataTable)(HttpContext.Current.Session[sessionId]);
            }
            else
            {
                Guid gFormId = new Guid(formId);
                DateTime dtFrom = System.Convert.ToDateTime(fromDate);
                DateTime dtTo = System.Convert.ToDateTime(toDate);
                dt = new ScFormReportDb().GetFormData(gFormId, dtFrom, dtTo);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                HttpContext.Current.Session[sessionId] = dt;
            }
            else
            {
                HttpContext.Current.Session[sessionId] = null;
            }

            return dt;
        }

        private static string GetTableHtml(DataTable dt)
        {
            string tableHtml = string.Empty;

            if (dt != null && dt.Rows.Count > 0)
            {
                int columnsCount = dt.Columns.Count;
                StringBuilder sbTable = new StringBuilder(string.Empty);
                sbTable.AppendLine("<table id=\"tblFormData\" class=\"table table-bordered table-striped\">");
                sbTable.AppendLine("<thead class=\"table-dark\"><tr>");

                ///Adding the first column for row number
                sbTable.AppendLine("<th scope=\"col\">#</th>");

                foreach (DataColumn dc in dt.Columns)
                {
                    sbTable.AppendLine("<th scope=\"col\">" + dc.ColumnName + "</th>");
                }

                sbTable.AppendLine("</tr></thead>");
                sbTable.AppendLine("<tbody>");

                int rowDisplayNum = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    sbTable.AppendLine("<tr>");

                    ///Adding the value first column i.e. RowNum
                    sbTable.AppendLine("<td  scope=\"row\">" + rowDisplayNum + "</td>");

                    foreach (DataColumn dc in dt.Columns)
                    {
                        sbTable.AppendLine("<td>" + dr[dc.ColumnName] + "</td>");
                    }

                    sbTable.AppendLine("</tr>");
                    rowDisplayNum++;
                }

                sbTable.AppendLine("</tbody>");
                sbTable.AppendLine("</table>");
                tableHtml = sbTable.ToString();
            }
            else
            {
                tableHtml = "No results found";
            }

            return tableHtml;
        }
    }
}