namespace Stratum.Foundation.Common.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common.Extensions;

    public class Tag : CustomItem
    {
        public Tag(Item innerItem) : base(innerItem)
        {
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
        }

        public string TagName
        {
            get
            {
                return InnerItem.GetString(CommonTemplates.Tag.Fields.TagName);
            }
        }
    }
}
