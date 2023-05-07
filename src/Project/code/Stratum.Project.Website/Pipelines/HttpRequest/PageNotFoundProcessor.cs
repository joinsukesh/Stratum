namespace Stratum.Project.Website.Pipelines.HttpRequest
{
    using Sitecore.Pipelines.HttpRequest;
    using Stratum.Project.Website.Services;
    using System;

    /// <summary>
    /// To handle when a context item has not been resolved. 
    /// This processor exists only to avoid Sitecore's default functionality of redirecting to the 404 page. 
    /// Instead of redirecting the client or use a server side only redirect, the goal is to leave the URL alone and 
    /// avoid causing a messy client experience. This is achieved merely by setting the Context.Item and ProcessorItem properties to the desired 404 page.
    /// </summary>
    public class PageNotFoundProcessor : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (Sitecore.Context.Item != null || Sitecore.Context.Site == null)
            {
                return;
            }

            if (string.Equals(Sitecore.Context.Site.Properties["enableCustomErrors"], "true", StringComparison.OrdinalIgnoreCase) == false)
            {
                return;
            }

            var pageNotFound = new PageNotFoundService().Get404PageItem();
            args.ProcessorItem = pageNotFound;
            Sitecore.Context.Item = pageNotFound;
        }        
    }
}