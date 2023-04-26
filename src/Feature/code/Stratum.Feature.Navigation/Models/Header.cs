
namespace Stratum.Feature.Navigation.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;
    using Stratum.Foundation.Common.Utilities;
    using System.Collections.Generic;

    public class Header : CustomItem
    {
        public Header(Item innerItem) : base(innerItem) { }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
        }

        public string LogoId
        {
            get
            {
                return Templates.Header.Fields.Logo.ToString();
            }
        }

        public string LogoUrlId
        {
            get
            {
                return Templates.Header.Fields.LogoTargetURL.ToString();
            }
        }

        public List<NavLink> NavLinks
        {
            get
            {
                List<NavLink> lst = SitecoreUtility.GetActiveItemsFromMultilistField<NavLink>(InnerItem, Templates.Header.Fields.NavLinks, CommonTemplates.ActiveStatus.Fields.IsActive);
                return lst;
            }
        }

        public string SignInPageId
        {
            get
            {
                return Templates.Header.Fields.SignInPage.ToString();
            }
        }

        public string SignUpPageId
        {
            get
            {
                return Templates.Header.Fields.SignUpPage.ToString();
            }
        }

        public string SignOutPageId
        {
            get
            {
                return Templates.Header.Fields.SignOutPage.ToString();
            }
        }

        public string SignOutPageLabel
        {
            get
            {
                return InnerItem.GetLinkDescription(Templates.Header.Fields.SignOutPage);
            }
        }

        private string _SignOutPageUrl;
        public string SignOutPageUrl
        {
            get
            {
                return InnerItem.GetLinkFieldUrl(Templates.Header.Fields.SignOutPage);
            }

            set { _SignOutPageUrl = value; }
        }
    }

}