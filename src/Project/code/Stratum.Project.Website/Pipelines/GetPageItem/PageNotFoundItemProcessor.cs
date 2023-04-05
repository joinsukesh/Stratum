namespace Stratum.Project.Website.Pipelines.GetPageItem
{
    using Sitecore.Data.Items;
    using Sitecore.Mvc.Pipelines;
    using Sitecore.Mvc.Pipelines.Response.GetPageItem;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;
    using Stratum.Project.Website.Services;
    using System;

    public class PageNotFoundItemProcessor : MvcPipelineProcessor<GetPageItemArgs>
    {
        public override void Process(GetPageItemArgs args)
        {
            if (args.Result == null || Sitecore.Context.Site == null)
            {
                return;
            }

            Item contextItem = Sitecore.Context.Item;
            Item pageNotFound = new PageNotFoundService().Get404PageItem();

            if (pageNotFound == null)
            {
                return;
            }

            if (contextItem != null && pageNotFound != null && contextItem.ID == pageNotFound.ID)
            {
                return;
            }

            bool isActive = true;

            try
            {
                ///If the contextItem is not a page or doesn't have the isactive field.
                isActive = contextItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
            catch (Exception)
            {
                isActive = true;
            }

            if (isActive)
            {
                return;
            }

            ///ProcessorItem and Result - both of these properties must be set to the desired 404 page 
            ///or Sitecore will simply ignore the property value that you set.
            args.ProcessorItem = pageNotFound;
            args.Result = pageNotFound;
            Sitecore.Context.Item = pageNotFound;
        }
    }
}