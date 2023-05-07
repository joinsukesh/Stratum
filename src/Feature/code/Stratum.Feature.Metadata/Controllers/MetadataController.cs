
namespace Stratum.Feature.Metadata.Controllers
{
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Stratum.Feature.Base.Controllers;
    using Stratum.Feature.Metadata.Models;
    using Stratum.Feature.Metadata.Services;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;
    using System.Web.Mvc;

    public class MetadataController : BaseController
    {
        private MetadataService metadataService = new MetadataService();

        public ActionResult RenderMetadata()
        {
            PageMetadata viewModel = null;

            try
            {
                Item contextItem = Sitecore.Context.Item;
                viewModel = new PageMetadata(contextItem);

                if (viewModel != null)
                {
                    viewModel.FinalMetaTitle = viewModel.MetaTitle;
                    Item websiteItem = Sitecore.Context.Item.GetAncestorByTemplate(CommonTemplates.Website.ID, true);

                    if (websiteItem != null)
                    {
                        SiteMetadata siteMetadata = new SiteMetadata(websiteItem);
                        if (siteMetadata != null)
                        {
                            viewModel.SiteMetaTags = siteMetadata.SiteMetaTags;
                            viewModel.Favicons = siteMetadata.Favicons;
                            viewModel.FinalMetaTitle += siteMetadata.MetaTitleAppend;
                        }
                    }

                }
            }
            catch (System.Exception ex)
            {
                Log.Error("", ex, this);
            }

            return View(Constants.ViewsFolderPath + "Metadata.cshtml", viewModel);
        }
    }
}