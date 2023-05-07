namespace Stratum.Foundation.Assets.Models
{
    using Sitecore.Data.Items;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class RenderingAssets : CustomItem
    {
        public RenderingAssets(Item innerItem) : base(innerItem) { }

        public MvcHtmlString RenderingMediaAssetsVersion
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.RenderingAssets.Fields.RenderingMediaAssetsVersion].Value);
            }
        }

        public List<object> RenderingMediaStylesHead { get; set; }

        public List<object> RenderingMediaScriptsHead { get; set; }

        public MvcHtmlString RenderingStylesHead
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.RenderingAssets.Fields.RenderingStylesHead].Value);
            }
        }

        public MvcHtmlString RenderingScriptsHead
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.RenderingAssets.Fields.RenderingScriptsHead].Value);
            }
        }

        public MvcHtmlString RenderingStyleSnippetsHead
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.RenderingAssets.Fields.RenderingStyleSnippetsHead].Value);
            }
        }

        public MvcHtmlString RenderingScriptSnippetsHead
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.RenderingAssets.Fields.RenderingScriptSnippetsHead].Value);
            }
        }

        public List<object> RenderingMediaScriptsBody { get; set; }

        public MvcHtmlString RenderingScriptsBody
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.RenderingAssets.Fields.RenderingScriptsBody].Value);
            }
        }

        public MvcHtmlString RenderingScriptSnippetsBody
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.RenderingAssets.Fields.RenderingScriptSnippetsBody].Value);
            }
        }
    }

}
