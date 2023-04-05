namespace Stratum.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;
    using Stratum.Foundation.Common.Utilities;
    using System.Collections.Generic;
    using System.Linq;

    public class TeaserTilesSection : CustomItem
    {
        public TeaserTilesSection(Item innerItem) : base(innerItem) { }

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

        public List<TeaserTile> Tiles
        {
            get
            {
                List<TeaserTile> lst = null;

                if (InnerItem.HasChildren)
                {
                    List<Item> childItems = InnerItem.GetChildItemsByTemplate(Templates.TeaserTile.ID);
                    lst = SitecoreUtility.GetClassObjectsFromItems<TeaserTile>(childItems);
                    lst = lst?.Where(x => x.IsActive).ToList();
                }

                return lst;
            }
        }
    }

}