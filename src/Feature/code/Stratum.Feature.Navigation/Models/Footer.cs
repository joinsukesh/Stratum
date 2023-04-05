namespace Stratum.Feature.Navigation.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;
    using Stratum.Foundation.Common.Utilities;
    using System.Collections.Generic;

    public class Footer : CustomItem
    {
        public Footer(Item innerItem) : base(innerItem) { }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
        }

        public string AddressId
        {
            get
            {
                return Templates.Footer.Fields.Address.ToString();
            }
        }

        public string Address
        {
            get
            {
                return InnerItem.GetString(Templates.Footer.Fields.Address);
            }
        }

        public List<NavLink> SocialLinks
        {
            get
            {
                List<NavLink> lst = SitecoreUtility.GetActiveItemsFromMultilistField<NavLink>(InnerItem, Templates.Footer.Fields.SocialLinks, CommonTemplates.ActiveStatus.Fields.IsActive);
                return lst;
            }
        }

        public string Column2TitleId
        {
            get
            {
                return Templates.Footer.Fields.Column2Title.ToString();
            }
        }

        public string Column2Title
        {
            get
            {
                return InnerItem.GetString(Templates.Footer.Fields.Column2Title);
            }
        }

        public List<NavLink> Column2Links
        {
            get
            {
                List<NavLink> lst = SitecoreUtility.GetActiveItemsFromMultilistField<NavLink>(InnerItem, Templates.Footer.Fields.Column2Links, CommonTemplates.ActiveStatus.Fields.IsActive);
                return lst;
            }
        }

        public string Column3TitleId
        {
            get
            {
                return Templates.Footer.Fields.Column3Title.ToString();
            }
        }

        public string Column3Title
        {
            get
            {
                return InnerItem.GetString(Templates.Footer.Fields.Column3Title);
            }
        }

        public List<NavLink> Column3Links
        {
            get
            {
                List<NavLink> lst = SitecoreUtility.GetActiveItemsFromMultilistField<NavLink>(InnerItem, Templates.Footer.Fields.Column3Links, CommonTemplates.ActiveStatus.Fields.IsActive);
                return lst;
            }
        }

        public string CopyrightSectionId
        {
            get
            {
                return Templates.Footer.Fields.CopyrightSection.ToString();
            }
        }

        public string CopyrightSection
        {
            get
            {
                return InnerItem.GetString(Templates.Footer.Fields.CopyrightSection);
            }
        }
    }

    
}