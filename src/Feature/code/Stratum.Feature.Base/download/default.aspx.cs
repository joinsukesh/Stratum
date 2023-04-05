namespace Stratum.Feature.Base.download
{
    using Sitecore;
    using Sitecore.Diagnostics;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Utilities;
    using System;
    using System.Data;
    using System.Web;

    public partial class _default : System.Web.UI.Page
    {
        public enum DownloadFileType
        {
            Excel = 1,
            Pdf = 2
        }

        #region EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string sessionId = Request.QueryString["id"];
                int downloadFileType = MainUtil.GetInt(Request.QueryString["type"], 0);
                string fileName = Request.QueryString["name"];
                fileName = fileName.Trim().Replace(" ", "");

                if ((!string.IsNullOrWhiteSpace(sessionId)) && (!string.IsNullOrWhiteSpace(fileName)) && downloadFileType > 0)
                {
                    DownloadFileType dft = (DownloadFileType)downloadFileType;

                    switch (dft)
                    {
                        case DownloadFileType.Excel:
                            if (HttpContext.Current.Session[sessionId] != null)
                            {
                                DataTable dt = (DataTable)HttpContext.Current.Session[sessionId];

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    fileName = fileName + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                                    HelperUtility.DownloadDataTable(dt, CommonConstants.HttpResponseContentTypes.Excel, fileName);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
            }
        }

        #endregion        
    }
}