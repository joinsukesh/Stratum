namespace Stratum.Feature.Metadata.Services
{
    using Sitecore.Data.Items;
    using Sitecore.Links.UrlBuilders;
    using Stratum.Foundation.Common.Extensions;

    public class MetadataService
    {
        public string GetCanonicalUrl(Item item)
        {
            string canonicalUrl = string.Empty;
            ItemUrlBuilderOptions options = new ItemUrlBuilderOptions
            {
                AddAspxExtension = false,
                AlwaysIncludeServerUrl = true,
                LowercaseUrls = true,
                EncodeNames = true,
                Language = item.Language,
                LanguageEmbedding = Sitecore.Links.LanguageEmbedding.Never,
                SiteResolving = false,
                UseDisplayName = false
            };

            canonicalUrl = item.Url(options);
            return canonicalUrl;
        }
    }
}