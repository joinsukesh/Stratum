namespace Stratum.Project.Website.Services
{
    using Sitecore.Configuration;
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common;

    public class PageNotFoundService
    {
        public Item Get404PageItem()
        {
            string path = Sitecore.Context.Site.StartPath + CommonConstants.Characters.ForwardSlash + Settings.GetSetting("ItemNotFoundUrl", "/404");
            Item item = Sitecore.Context.Database.GetItem(path);
            return item;
        }
    }
}