namespace Stratum.Foundation.Assets.Services
{
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Mvc.Presentation;
    using Stratum.Foundation.Assets.Models;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using static Stratum.Foundation.Assets.Constants;

    public class AssetsService
    {
        private Item WebsiteItem
        {
            get
            {
                return Sitecore.Context.Item.GetAncestorByTemplate(CommonTemplates.Website.ID);
            }
        }

        private Item PageItem
        {
            get
            {
                return Sitecore.Context.Item;
            }
        }

        private List<Item> PageRenderingItems
        {
            get
            {
                List<Item> renderingItems = new List<Item>();
                List<Rendering> allPageRenderings = PageContext.Current.PageDefinition.Renderings.Where(x => x.RenderingItem.InnerItem.Paths.Path.StartsWith("/sitecore/layout/Renderings/", StringComparison.OrdinalIgnoreCase)).ToList();

                if (allPageRenderings != null && allPageRenderings.Count > 0)
                {
                    foreach (Rendering rendering in allPageRenderings)
                    {
                        renderingItems.Add(rendering.RenderingItem.InnerItem);
                    }
                }

                return renderingItems;
            }
        }

        public MvcHtmlString GetAssets(AssetPosition assetPosition)
        {
            StringBuilder sbAssets = new StringBuilder(string.Empty);
            List<string> lstAssets = new List<string>();
            lstAssets.AddRange(GetAssetReferences(assetPosition, AssetType.Style));
            lstAssets.AddRange(GetAssetReferences(assetPosition, AssetType.Script));
            lstAssets = lstAssets.Distinct().ToList();

            foreach (string asset in lstAssets)
            {
                sbAssets.AppendLine(asset);
            }

            sbAssets.AppendLine(GetStyleOrScriptSnippets(assetPosition, AssetType.Style));
            sbAssets.AppendLine(GetStyleOrScriptSnippets(assetPosition, AssetType.Script));
            return new MvcHtmlString(System.Convert.ToString(sbAssets));
        }

        private List<string> GetAssetReferences(AssetPosition assetPosition, AssetType assetType)
        {
            List<string> lstReferences = new List<string>();
            switch (assetPosition)
            {
                case AssetPosition.Head:
                    switch (assetType)
                    {
                        case AssetType.Style:
                            lstReferences.AddRange(GetFileAssets(WebsiteItem, AssetLevel.Global, assetType, Templates.GlobalAssets.Fields.GlobalStylesHead));
                            lstReferences.AddRange(GetFileAssets(PageItem, AssetLevel.Page, assetType, Templates.PageAssets.Fields.PageStylesHead));
                            lstReferences.AddRange(GetMediaAssets(WebsiteItem, AssetLevel.Global, assetType, Templates.GlobalAssets.Fields.GlobalMediaStylesHead));
                            lstReferences.AddRange(GetMediaAssets(PageItem, AssetLevel.Page, assetType, Templates.PageAssets.Fields.PageMediaStylesHead));
                            break;
                        case AssetType.Script:
                            lstReferences.AddRange(GetFileAssets(WebsiteItem, AssetLevel.Global, assetType, Templates.GlobalAssets.Fields.GlobalScriptsHead));
                            lstReferences.AddRange(GetFileAssets(PageItem, AssetLevel.Page, assetType, Templates.PageAssets.Fields.PageScriptsHead));
                            lstReferences.AddRange(GetMediaAssets(WebsiteItem, AssetLevel.Global, assetType, Templates.GlobalAssets.Fields.GlobalMediaScriptsHead));
                            lstReferences.AddRange(GetMediaAssets(PageItem, AssetLevel.Page, assetType, Templates.PageAssets.Fields.PageMediaScriptsHead));
                            break;
                    }
                    break;
                case AssetPosition.Body:
                    switch (assetType)
                    {
                        case AssetType.Script:
                            lstReferences.AddRange(GetFileAssets(WebsiteItem, AssetLevel.Global, assetType, Templates.GlobalAssets.Fields.GlobalScriptsBody));
                            lstReferences.AddRange(GetFileAssets(PageItem, AssetLevel.Page, assetType, Templates.PageAssets.Fields.PageScriptsBody));
                            lstReferences.AddRange(GetMediaAssets(WebsiteItem, AssetLevel.Global, assetType, Templates.GlobalAssets.Fields.GlobalMediaScriptsBody));
                            lstReferences.AddRange(GetMediaAssets(PageItem, AssetLevel.Page, assetType, Templates.PageAssets.Fields.PageMediaScriptsBody));
                            break;
                    }
                    break;
            }

            lstReferences.AddRange(GetRenderingAssetReferences(assetPosition, assetType));
            return lstReferences;
        }

        private string GetStyleOrScriptSnippets(AssetPosition assetPosition, AssetType assetType)
        {
            StringBuilder sbSnippets = new StringBuilder(string.Empty);
            switch (assetPosition)
            {
                case AssetPosition.Head:
                    switch (assetType)
                    {
                        case AssetType.Style:
                            sbSnippets.AppendLine(GetSnippet(WebsiteItem, AssetLevel.Global, Templates.GlobalAssets.Fields.GlobalStyleSnippetsHead));
                            sbSnippets.AppendLine(GetSnippet(PageItem, AssetLevel.Page, Templates.PageAssets.Fields.PageStyleSnippetsHead));

                            if (PageRenderingItems != null && PageRenderingItems.Count > 0)
                            {
                                foreach (Item item in PageRenderingItems)
                                {
                                    sbSnippets.AppendLine(GetSnippet(item, AssetLevel.Rendering, Templates.RenderingAssets.Fields.RenderingStyleSnippetsHead));
                                }
                            }
                            break;
                        case AssetType.Script:
                            sbSnippets.AppendLine(GetSnippet(WebsiteItem, AssetLevel.Global, Templates.GlobalAssets.Fields.GlobalScriptSnippetsHead));
                            sbSnippets.AppendLine(GetSnippet(PageItem, AssetLevel.Page, Templates.PageAssets.Fields.PageScriptSnippetsHead));

                            if (PageRenderingItems != null && PageRenderingItems.Count > 0)
                            {
                                foreach (Item item in PageRenderingItems)
                                {
                                    sbSnippets.AppendLine(GetSnippet(item, AssetLevel.Rendering, Templates.RenderingAssets.Fields.RenderingScriptSnippetsHead));
                                }
                            }
                            break;
                    }
                    break;
                case AssetPosition.Body:
                    switch (assetType)
                    {
                        case AssetType.Script:
                            sbSnippets.AppendLine(GetSnippet(WebsiteItem, AssetLevel.Global, Templates.GlobalAssets.Fields.GlobalScriptSnippetsBody));
                            sbSnippets.AppendLine(GetSnippet(PageItem, AssetLevel.Page, Templates.PageAssets.Fields.PageScriptSnippetsBody));

                            if (PageRenderingItems != null && PageRenderingItems.Count > 0)
                            {
                                foreach (Item item in PageRenderingItems)
                                {
                                    sbSnippets.AppendLine(GetSnippet(item, AssetLevel.Rendering, Templates.RenderingAssets.Fields.RenderingScriptSnippetsBody));
                                }
                            }
                            break;
                    }
                    break;
            }
            return System.Convert.ToString(sbSnippets);
        }

        private List<string> GetMediaAssets(Item assetsHoldingItem, AssetLevel assetLevel, AssetType assetType, ID mediaAssetsFieldId)
        {
            List<string> assetUrls = new List<string>();
            bool renderAssets = ShouldRenderAssets(assetLevel);
            string version = string.Empty;
            string url = string.Empty;

            if (renderAssets)
            {
                switch (assetLevel)
                {
                    case AssetLevel.Global:
                        version = assetsHoldingItem.GetString(Templates.GlobalAssets.Fields.GlobalMediaAssetsVersion);
                        break;
                    case AssetLevel.Page:
                        version = assetsHoldingItem.GetString(Templates.PageAssets.Fields.PageMediaAssetsVersion);
                        break;
                    case AssetLevel.Rendering:
                        version = assetsHoldingItem.GetString(Templates.RenderingAssets.Fields.RenderingMediaAssetsVersion);
                        break;
                }

                Item[] assetItems = assetsHoldingItem?.GetMultilistFieldItems(mediaAssetsFieldId);

                if (assetItems != null && assetItems.Count() > 0)
                {
                    foreach (Item item in assetItems)
                    {
                        url = string.IsNullOrWhiteSpace(version) ? item.Url(true, false) : item.Url(true, false) + "?v=" + version;
                        assetUrls.Add(url);
                    }

                    assetUrls = GetAssetReferenceTags(assetType, assetUrls);
                }
            }

            assetUrls.RemoveAll(string.IsNullOrWhiteSpace);
            return assetUrls;
        }

        private List<string> GetFileAssets(Item assetsHoldingItem, AssetLevel assetLevel, AssetType assetType, ID assetsFieldId)
        {
            List<string> assetUrls = new List<string>();
            bool renderAssets = ShouldRenderAssets(assetLevel);

            if (renderAssets)
            {
                string fileAssetsFieldValue = assetsHoldingItem?.GetString(assetsFieldId);

                if (!string.IsNullOrWhiteSpace(fileAssetsFieldValue))
                {
                    string[] arrAssets = fileAssetsFieldValue.Split(CommonConstants.Characters.Comma);
                    string trimmedAsset = string.Empty;

                    foreach (string asset in arrAssets)
                    {
                        trimmedAsset = asset.Trim();
                        assetUrls.Add(StringUtil.EnsurePrefix(CommonConstants.Characters.ForwardSlashChar, trimmedAsset));
                    }

                    assetUrls = GetAssetReferenceTags(assetType, assetUrls);
                }
            }

            assetUrls.RemoveAll(string.IsNullOrWhiteSpace);
            return assetUrls;
        }

        public List<string> GetRenderingAssetReferences(AssetPosition assetPosition, AssetType assetType)
        {
            List<string> lstReferences = new List<string>();

            if (PageRenderingItems != null && PageRenderingItems.Count > 0)
            {
                switch (assetPosition)
                {
                    case AssetPosition.Head:
                        switch (assetType)
                        {
                            case AssetType.Style:
                                foreach (Item item in PageRenderingItems)
                                {
                                    lstReferences.AddRange(GetFileAssets(item, AssetLevel.Rendering, assetType, Templates.RenderingAssets.Fields.RenderingStylesHead));
                                    lstReferences.AddRange(GetMediaAssets(item, AssetLevel.Rendering, assetType, Templates.RenderingAssets.Fields.RenderingMediaStylesHead));
                                }
                                break;
                            case AssetType.Script:
                                foreach (Item item in PageRenderingItems)
                                {
                                    lstReferences.AddRange(GetFileAssets(item, AssetLevel.Rendering, assetType, Templates.RenderingAssets.Fields.RenderingScriptsHead));
                                    lstReferences.AddRange(GetMediaAssets(item, AssetLevel.Rendering, assetType, Templates.RenderingAssets.Fields.RenderingMediaScriptsHead));
                                }
                                break;
                        }

                        break;
                    case AssetPosition.Body:
                        switch (assetType)
                        {
                            case AssetType.Script:
                                foreach (Item item in PageRenderingItems)
                                {
                                    lstReferences.AddRange(GetFileAssets(item, AssetLevel.Rendering, assetType, Templates.RenderingAssets.Fields.RenderingScriptsBody));
                                    lstReferences.AddRange(GetMediaAssets(item, AssetLevel.Rendering, assetType, Templates.RenderingAssets.Fields.RenderingMediaScriptsBody));
                                }
                                break;
                        }
                        break;
                }
            }

            lstReferences.RemoveAll(string.IsNullOrWhiteSpace);
            return lstReferences;
        }

        private bool ShouldRenderAssets(AssetLevel assetLevel)
        {
            bool renderAssets = true;

            if (assetLevel == AssetLevel.Global)
            {
                bool renderGlobalAssets = true;

                try
                {
                    PageAssets pageAssets = new PageAssets(PageItem);

                    if (pageAssets != null && pageAssets.DoNotLoadGlobalAssets)
                    {
                        renderGlobalAssets = false;
                    }
                }
                catch (Exception)
                {

                }

                renderAssets = renderGlobalAssets;
            }

            return renderAssets;
        }

        private List<string> GetAssetReferenceTags(AssetType assetType, List<string> assetUrls)
        {
            List<string> lstAssetReferences = new List<string>();

            if (assetUrls != null && assetUrls.Count > 0)
            {
                switch (assetType)
                {
                    case AssetType.Style:
                        foreach (string url in assetUrls)
                        {
                            lstAssetReferences.Add(string.Format("<link rel=\"stylesheet\" href=\"{0}\" />", url));
                        }
                        break;
                    case AssetType.Script:
                        foreach (string url in assetUrls)
                        {
                            lstAssetReferences.Add(string.Format("<script text=\"text/javascript\" src=\"{0}\"></script>", url));
                        }
                        break;
                }
            }

            return lstAssetReferences;
        }

        private string GetSnippet(Item assetsHoldingItem, AssetLevel assetLevel, ID snippetFieldId)
        {
            string snippet = string.Empty;
            bool renderAssets = ShouldRenderAssets(assetLevel);

            if (renderAssets)
            {
                snippet = assetsHoldingItem?.GetString(snippetFieldId);
            }

            return snippet;
        }
    }
}
