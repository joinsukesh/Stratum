
namespace Stratum.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;

    public class RichTextSection : CustomItem
    {
        public RichTextSection(Item innerItem) : base(innerItem) { }

        public string ContentId
        {
            get
            {
                return Templates.RichTextSection.Fields.Content.ToString();
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
        }
    }
}