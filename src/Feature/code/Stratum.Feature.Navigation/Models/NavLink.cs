namespace Stratum.Feature.Navigation.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;
    using Stratum.Foundation.Common.Utilities;
    using System.Collections.Generic;
    using System.Linq;

    public class NavLink : CustomItem
    {
        public NavLink(Item innerItem) : base(innerItem) { }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
        }

        public string LinkId
        {
            get
            {
                return Templates.NavLink.Fields.Link.ToString();
            }
        }

        public string LinkDescription
        {
            get
            {
                return InnerItem.GetLinkDescription(Templates.NavLink.Fields.Link);
            }
        }

        public string CssClassId
        {
            get
            {
                return Templates.NavLink.Fields.CSSClass.ToString();
            }
        }

        public string CssClass
        {
            get
            {
                return InnerItem.GetString(Templates.NavLink.Fields.CSSClass);
            }
        }

        public List<NavLink> SubLinks
        {
            get
            {
                List<NavLink> lst = null;

                if (InnerItem.HasChildren)
                {
                    List<Item> childItems = InnerItem.GetChildItemsByTemplate(Templates.NavLink.ID);

                    if (childItems != null && childItems.Count > 0)
                    {
                        List<NavLink> allSublinks = SitecoreUtility.GetClassObjectsFromItems<NavLink>(childItems);
                        lst = allSublinks?.Where(x => x.IsActive).ToList();
                    }
                }
                return lst;
            }
        }
    }
}