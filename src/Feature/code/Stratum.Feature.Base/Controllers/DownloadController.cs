

namespace Stratum.Feature.Base.Controllers
{
    using Sitecore;
    using Sitecore.Diagnostics;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Utilities;
    using System;
    using System.Data;
    using System.IO;
    using System.Web.Mvc;

    public class DownloadController : Controller
    {
        public enum DownloadFileType
        {
            Excel,
            Pdf
        }

        /// <summary>
        /// Downloads the data from session
        /// </summary>
        /// <param name="id">id is the sessionId</param>
        /// <param name="type">type is the download file type e.g.: excel, pdf, word etc</param>
        /// <param name="id">name is the filename</param>
        /// <returns></returns>
        public ActionResult DownloadData(string id, string type, string name)
        {
            string sessionId = id;
            int downloadFileType = MainUtil.GetInt(type, 0);

            try
            {
                DownloadFileType dft = (DownloadFileType)downloadFileType;

                switch (dft)
                {
                    case DownloadFileType.Excel:

                        if (Session[sessionId] != null && !string.IsNullOrWhiteSpace(name))
                        {
                            DataTable dt = (DataTable)(Session[sessionId]);

                            if (dt != null)
                            {
                                name = name.Trim().Replace(" ", "");
                                string fileName = name + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                                MemoryStream stream = HelperUtility.GetDataTableAsMemoryStream(dt, fileName);
                                return File(stream.ToArray(), CommonConstants.HttpResponseContentTypes.Excel, fileName);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                return Content(CommonConstants.GenericError);
            }

            return Content(string.Empty);
        }
    }
}