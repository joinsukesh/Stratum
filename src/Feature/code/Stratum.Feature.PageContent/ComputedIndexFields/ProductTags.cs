namespace Stratum.Feature.PageContent.ComputedIndexFields
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common.Extensions;
    using Stratum.Foundation.Common.Models;
    using System.Collections.Generic;

    public class ProductTags : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;

            if (item == null || item.TemplateID != Templates.ProductDetailsPage.ID)
                return null;

            List<string> lstTagNames = null;
            Item[] selectedTags = item.GetMultilistFieldItems(Templates.ProductDetails.Fields.Tags);

            if (selectedTags != null && selectedTags.Length > 0)
            {
                lstTagNames = new List<string>();
                Tag tag = null;

                foreach (Item tagItem in selectedTags)
                {
                    tag = new Tag(tagItem);

                    if (tag != null && tag.IsActive && (!string.IsNullOrWhiteSpace(tag.TagName)))
                    {
                        lstTagNames.Add(tag.TagName);
                    }
                }
            }

            return lstTagNames;
        }
    }
}