namespace Stratum.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;

    public class HeroBanner : CustomItem
    {
        public HeroBanner(Item innerItem) : base(innerItem) { }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
        }

        public string BannerImageId
        {
            get
            {
                return Templates.HeroBanner.Fields.BannerImage.ToString();
            }
        }

        public string BannerImageUrl
        {
            get
            {
                return InnerItem.GetMediaItemUrl(Templates.HeroBanner.Fields.BannerImage);
            }
        }

        public string TitleId
        {
            get
            {
                return Templates.HeroBanner.Fields.Title.ToString();
            }
        }

        public string Title
        {
            get
            {
                return InnerItem.GetString(Templates.HeroBanner.Fields.Title);
            }
        }

        public string DescriptionId
        {
            get
            {
                return Templates.HeroBanner.Fields.Description.ToString();
            }
        }

        public string Description
        {
            get
            {
                return InnerItem.GetString(Templates.HeroBanner.Fields.Description);
            }
        }

        public string CtaId
        {
            get
            {
                return Templates.HeroBanner.Fields.CTA.ToString();
            }
        }

        public string CtaDescription
        {
            get
            {
                return InnerItem.GetLinkDescription(Templates.HeroBanner.Fields.CTA);
            }
        }
    }

}