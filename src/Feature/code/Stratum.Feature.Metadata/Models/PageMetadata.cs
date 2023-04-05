namespace Stratum.Feature.Metadata.Models
{
    using Sitecore.Data.Items;
    using Stratum.Feature.Metadata.Services;
    using Stratum.Foundation.Common.Extensions;
    using System.Web.Mvc;

    public class PageMetadata : CustomItem
    {
        private MetadataService metadataService = new MetadataService();

        public PageMetadata(Item innerItem) : base(innerItem) { }

        public MvcHtmlString SiteMetaTags { get; set; }

        public string MetaTitle
        {
            get
            {
                return InnerItem.GetString(Templates.PageMetadata.Fields.MetaTitle);
            }
        }

        public string MetaDescription
        {
            get
            {
                return InnerItem.GetString(Templates.PageMetadata.Fields.MetaDescription);
            }
        }

        public bool GenerateDynamicCanonicalUrl
        {
            get
            {
                return InnerItem.IsChecked(Templates.PageMetadata.Fields.GenerateDynamicCanonicalURL);
            }
        }

        public string CanonicalUrl
        {
            get
            {
                return InnerItem.GetString(Templates.PageMetadata.Fields.CanonicalURL);
            }
        }

        public string FinalCanonicalUrl
        {
            get
            {
                return this.GenerateDynamicCanonicalUrl ? metadataService.GetCanonicalUrl(InnerItem) : this.CanonicalUrl;
            }
        }

        public string OgTitle
        {
            get
            {
                string value = InnerItem.GetString(Templates.PageMetadata.Fields.OGTitle);
                return string.IsNullOrWhiteSpace(value) ? this.MetaTitle : value;
            }
        }

        public string OgDescription
        {
            get
            {
                string value = InnerItem.GetString(Templates.PageMetadata.Fields.OGDescription);
                return string.IsNullOrWhiteSpace(value) ? this.MetaDescription : value;
            }
        }

        public string OgUrl
        {
            get
            {
                string value = InnerItem.GetString(Templates.PageMetadata.Fields.OGURL);
                return string.IsNullOrWhiteSpace(value) ? this.FinalCanonicalUrl : value;
            }
        }

        public string OgImage
        {
            get
            {
                return InnerItem.GetMediaItemUrl(Templates.PageMetadata.Fields.OGImage, true);
            }
        }

        public MvcHtmlString Favicons { get; set; }

        public bool IndexAndFollow
        {
            get
            {
                return InnerItem.IsChecked(Templates.PageMetadata.Fields.IndexAndFollow);
            }
        }

        public MvcHtmlString OtherMetaTags
        {
            get
            {
                return new MvcHtmlString(InnerItem.GetString(Templates.PageMetadata.Fields.OtherMetaTags));
            }
        }

        public string FinalMetaTitle { get; set; }
        
    }

}