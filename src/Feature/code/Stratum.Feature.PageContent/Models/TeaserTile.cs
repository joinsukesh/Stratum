namespace Stratum.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;

    public class TeaserTile : CustomItem
    {
        public TeaserTile(Item innerItem) : base(innerItem) { }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
        }

        public string ContainerCssClassId
        {
            get
            {
                return Templates.TeaserTile.Fields.ContainerCssClass.ToString();
            }
        }

        public string ContainerCssClass
        {
            get
            {
                return InnerItem.GetString(Templates.TeaserTile.Fields.ContainerCssClass);
            }
        }

        public string IconCssClassId
        {
            get
            {
                return Templates.TeaserTile.Fields.IconCSSClass.ToString();
            }
        }

        public string IconCssClass
        {
            get
            {
                return InnerItem.GetString(Templates.TeaserTile.Fields.IconCSSClass);
            }
        }

        public string CtaId
        {
            get
            {
                return Templates.TeaserTile.Fields.CTA.ToString();
            }
        }

        public string CtaUrl
        {
            get
            {
                return InnerItem.GetLinkFieldUrl(Templates.TeaserTile.Fields.CTA);
            }
        }

        public string CtaTargetType
        {
            get
            {
                return InnerItem.GetLinkTargetType(Templates.TeaserTile.Fields.CTA);
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