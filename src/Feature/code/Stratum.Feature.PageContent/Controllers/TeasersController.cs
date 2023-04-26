
namespace Stratum.Feature.PageContent.Controllers
{
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Stratum.Feature.Base;
    using Stratum.Feature.Base.Controllers;
    using Stratum.Feature.PageContent.Models;
    using Stratum.Foundation.Common.Utilities;
    using System;
    using System.Web.Mvc;

    public class TeasersController : Controller
    {
        public ActionResult RenderTeaserTilesSection()
        {
            TeaserTilesSection viewModel = null;

            try
            {
                Item datasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (datasourceItem != null && datasourceItem.TemplateID == Templates.TeaserTilesSection.ID)
                {
                    viewModel = new TeaserTilesSection(datasourceItem);

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

            return View(Constants.ViewsFolderPath + "TeaserTilesSection.cshtml", viewModel);
        }
    }
}