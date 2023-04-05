namespace Stratum.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;

    public class NumberedGridTile : CustomItem
    {
        public NumberedGridTile(Item innerItem) : base(innerItem) { }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
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