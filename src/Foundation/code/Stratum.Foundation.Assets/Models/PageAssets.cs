namespace Stratum.Foundation.Assets.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common.Extensions;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class PageAssets : CustomItem
    {
        public PageAssets(Item innerItem) : base(innerItem) { }

        public bool DoNotLoadGlobalAssets
        {
            get
            {
                return InnerItem.IsChecked(Templates.PageAssets.Fields.DoNotLoadGlobalAssets);
            }
        }

        public MvcHtmlString PageMediaAssetsVersion
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.PageAssets.Fields.PageMediaAssetsVersion].Value);
            }
        }

        public List<object> PageMediaStyleAssetsHead { get; set; }

        public List<object> PageMediaScriptAssetsHead { get; set; }

        public MvcHtmlString PageStylesHead
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.PageAssets.Fields.PageStylesHead].Value);
            }
        }

        public MvcHtmlString PageScriptsHead
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.PageAssets.Fields.PageScriptsHead].Value);
            }
        }

        public MvcHtmlString PageStyleSnippetsHead
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.PageAssets.Fields.PageStyleSnippetsHead].Value);
            }
        }

        public MvcHtmlString PageScriptSnippetsHead
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.PageAssets.Fields.PageScriptSnippetsHead].Value);
            }
        }

        public List<object> PageMediaScriptAssetsBody { get; set; }

        public string PageScriptsBodyId
        {
            get
            {
                return Templates.PageAssets.Fields.PageScriptsBody.ToString();
            }
        }

        public MvcHtmlString PageScriptsBody
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.PageAssets.Fields.PageScriptsBody].Value);
            }
        }

        public MvcHtmlString PageScriptSnippetsBody
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.PageAssets.Fields.PageScriptSnippetsBody].Value);
            }
        }
    }

}
