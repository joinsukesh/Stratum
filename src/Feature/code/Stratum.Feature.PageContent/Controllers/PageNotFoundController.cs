
namespace Stratum.Feature.PageContent.Controllers
{
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Stratum.Feature.Base;
    using Stratum.Feature.Base.Controllers;
    using Stratum.Feature.PageContent.Models;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Utilities;
    using System;
    using System.Web.Mvc;

    public class PageNotFoundController : Controller
    {
        public ActionResult Render404Section()
        {
            RichTextSection viewModel = null;

            try
            {
                System.Web.HttpContext.Current.Response.StatusCode = 404;
                ///TrySkipIisCustomErrors will cause IIS to ignore the configuration in the httpErrors configuration node,
                ///since we've set the existingResponse attribute on that node to "Auto".
                System.Web.HttpContext.Current.Response.TrySkipIisCustomErrors = true;
                System.Web.HttpContext.Current.Response.StatusDescription = "404 File Not Found";

                Item datasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (datasourceItem != null && datasourceItem.TemplateID == Templates.RichTextSection.ID)
                {
                    viewModel = new RichTextSection(datasourceItem);

                    if (!viewModel.IsActive)
                    {
                        viewModel = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                return View(BaseConstants.CommonErrorViewPath, ex);
            }

            return View(Constants.ViewsFolderPath + "RichTextSection.cshtml", viewModel);
        }
    }
}