namespace Stratum.Feature.Metadata.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common.Extensions;
    using System.Web.Mvc;

    public class SiteMetadata : CustomItem
    {
        public SiteMetadata(Item innerItem) : base(innerItem) { }

        public MvcHtmlString SiteMetaTags
        {
            get
            {
                return new MvcHtmlString(InnerItem.GetString(Templates.SiteMetadata.Fields.SiteMetaTags));
            }
        }

        public string MetaTitleAppend
        {
            get
            {
                return InnerItem.GetString(Templates.SiteMetadata.Fields.MetaTitleAppend);
            }
        }

        public MvcHtmlString Favicons
        {
            get
            {
                return new MvcHtmlString(InnerItem.GetString(Templates.SiteMetadata.Fields.Favicons));
            }
        }
    }
}