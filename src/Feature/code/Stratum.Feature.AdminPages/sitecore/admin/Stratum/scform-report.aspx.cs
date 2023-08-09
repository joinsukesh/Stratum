
namespace Stratum.Feature.AdminPages.sitecore.admin.Stratum
{
    using global::Stratum.Feature.AdminPages.DbFactory;
    using global::Stratum.Feature.Base.Models;
    using global::Stratum.Foundation.Accounts.Services;
    using global::Stratum.Foundation.Common;
    using global::Stratum.Foundation.Common.Extensions;
    using global::Stratum.Foundation.Common.Utilities;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web;
    using System.Web.Script.Serialization;
    using System.Web.Services;
    using System.Web.UI.WebControls;
    using FA = global::Stratum.Feature.AdminPages;

    public partial class scform_report : System.Web.UI.Page
    {
        private AccountsService accountsService = new AccountsService();
        private string SitecoreFormsFolderId = "{B701850A-CB8A-4943-B2BC-DDDB1238C103}";

        private string SortDirection
        {
            get { return ViewState[Constants.SortDirection] != null ? ViewState[Constants.SortDirection].ToString() : Constants.ASC; }
            set { ViewState[Constants.SortDirection] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (accountsService.IsAdministrator())
            {
                try
                {
                    if (!IsPostBack)
                    {
                        hdnSessionId.Value = HelperUtility.GetRandomString(7);
                        BindFormsList();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("", ex, FA.Constants.Stratum_AdminPage_Error);
                    lblError.Text = ex.Message;
                }
            }
            else
            {
                Response.Redirect(FA.Constants.Paths.LoginPagePath);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (accountsService.IsAdministrator())
                {
                    HttpContext.Current.Session[hdnSessionId.Value] = null;
                    BindFormData(true, true);
                }
                else
                {
                    Response.Redirect(FA.Constants.Paths.LoginPagePath);
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex, FA.Constants.Stratum_AdminPage_Error);
                lblError.Text = ex.Message;
            }
        }

        protected void gvForData_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex == 0)
                {
                    e.Row.RowType = DataControlRowType.Header;
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex, FA.Constants.Stratum_AdminPage_Error);
                lblError.Text = ex.Message;
            }
        }

        protected void gvFormData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvFormData.PageIndex = e.NewPageIndex;
                BindFormData(true, false);
            }
            catch (Exception ex)
            {
                Log.Error("", ex, FA.Constants.Stratum_AdminPage_Error);
                lblError.Text = ex.Message;
            }
        }

        protected void OnSorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                BindFormData(true, false, e.SortExpression);
            }
            catch (Exception ex)
            {
                Log.Error("", ex, FA.Constants.Stratum_AdminPage_Error);
                lblError.Text = ex.Message;
            }
        }

        private void BindFormData(bool isPostBack, bool isSubmitClick, string sortExpression = null)
        {
            if (isPostBack)
            {
                DataTable dt = GetFormData(ddlForms.SelectedValue, txtFromDate.Text, txtToDate.Text, hdnSessionId.Value);

                if (dt != null && dt.Rows.Count > 0 && sortExpression != null)
                {
                    DataView dv = dt.AsDataView();
                    this.SortDirection = this.SortDirection == Constants.ASC ? Constants.DESC : Constants.ASC;

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvFormData.DataSource = dv;
                    btnDownload.Visible = true;
                }
                else
                {
                    gvFormData.DataSource = dt;
                }

                if (isSubmitClick)
                {
                    SetSessionData(dt, hdnSessionId.Value);
                }

                gvFormData.DataBind();
                upFormData.Update();

                hdnIsPostBack.Value = CommonConstants.One;
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
                ///The user input has only date and not time selection. So, modifying the end date for the correct range.
                dtTo = (dtTo.AddDays(1)).AddSeconds(-1);
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

        private void SetSessionData(DataTable dt, string sessionId)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                HttpContext.Current.Session[sessionId] = dt;
            }
            else
            {
                HttpContext.Current.Session[sessionId] = null;
            }
        }
    }
}