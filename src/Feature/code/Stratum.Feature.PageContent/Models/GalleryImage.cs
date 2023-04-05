namespace Stratum.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;

    public class GalleryImage : CustomItem
    {
        public GalleryImage(Item innerItem) : base(innerItem) { }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
        }

        public string ImageId
        {
            get
            {
                return Templates.GalleryImage.Fields.Image.ToString();
            }
        }

        public string ImageUrl
        {
            get
            {
                return InnerItem.GetMediaItemUrl(Templates.GalleryImage.Fields.Image);
            }
        }

        public string ImageAlt
        {
            get
            {
                return InnerItem.GetImageAltText(Templates.GalleryImage.Fields.Image);
            }
        }

        public string CtaId
        {
            get
            {
                return Templates.GalleryImage.Fields.CTA.ToString();
            }
        }

        public string CtaUrl
        {
            get
            {
                return InnerItem.GetLinkFieldUrl(Templates.GalleryImage.Fields.CTA);
            }
        }

        public string CtaTargetType
        {
            get
            {
                return InnerItem.GetLinkTargetType(Templates.GalleryImage.Fields.CTA);
            }
        }

        public string TitleId
        {
            get
            {
                return CommonTemplates.BaseContent.Fields.Title.ToString();
            }
        }

        public string Title
        {
            get
            {
                return InnerItem.GetString(CommonTemplates.BaseContent.Fields.Title);
            }
        }

        public string DescriptionId
        {
            get
            {
                return CommonTemplates.BaseContent.Fields.Description.ToString();
            }
        }

        public string Description
        {
            get
            {
                return InnerItem.GetString(CommonTemplates.BaseContent.Fields.Description);
            }
        }
    }

}