namespace Stratum.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;
    using Stratum.Foundation.Common.Utilities;
    using System.Collections.Generic;
    using System.Linq;

    public class NumberedGridTilesSection : CustomItem
    {
        public NumberedGridTilesSection(Item innerItem) : base(innerItem) { }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
        }

        public bool ShowTileNumbers
        {
            get
            {
                return InnerItem.IsChecked(Templates.NumberedGridTilesSection.Fields.ShowTileNumbers);
            }
        }

        public List<NumberedGridTile> Tiles
        {
            get
            {
                List<NumberedGridTile> lst = null;

                if (InnerItem.HasChildren)
                {
                    List<Item> childItems = InnerItem.GetChildItemsByTemplate(Templates.NumberedGridTile.ID);
                    lst = SitecoreUtility.GetClassObjectsFromItems<NumberedGridTile>(childItems);
                    lst = lst?.Where(x => x.IsActive).ToList();
                }

                return lst;
            }
        }
    }

}