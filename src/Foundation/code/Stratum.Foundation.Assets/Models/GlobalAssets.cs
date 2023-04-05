namespace Stratum.Foundation.Assets.Models
{
    using Sitecore;
    using Sitecore.Data.Items;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Stratum.Foundation.Common.Extensions;

    public class GlobalAssets : CustomItem
    {
        public GlobalAssets(Item innerItem) : base(innerItem) { }

        public string GlobalMediaAssetsVersion
        {
            get
            {
                return InnerItem.GetString(Templates.GlobalAssets.Fields.GlobalMediaAssetsVersion);
            }
        }

        public List<object> GlobalMediaStyleAssetsHead { get; set; }

        public List<object> GlobalMediaScriptAssetsHead { get; set; }

        public MvcHtmlString GlobalStylesHead
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.GlobalAssets.Fields.GlobalStylesHead].Value);
            }
        }

        public MvcHtmlString GlobalScriptsHead
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.GlobalAssets.Fields.GlobalScriptsHead].Value);
            }
        }

        public MvcHtmlString GlobalStyleSnippetsHead
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.GlobalAssets.Fields.GlobalStyleSnippetsHead].Value);
            }
        }

        public MvcHtmlString GlobalScriptSnippetsHead
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.GlobalAssets.Fields.GlobalScriptSnippetsHead].Value);
            }
        }

        public List<object> GlobalMediaScriptAssetsBody { get; set; }

        public MvcHtmlString GlobalScriptsBody
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.GlobalAssets.Fields.GlobalScriptsBody].Value);
            }
        }

        public MvcHtmlString GlobalScriptSnippetsBody
        {
            get
            {
                return new MvcHtmlString(InnerItem.Fields[Templates.GlobalAssets.Fields.GlobalScriptSnippetsBody].Value);
            }
        }
    }

}
