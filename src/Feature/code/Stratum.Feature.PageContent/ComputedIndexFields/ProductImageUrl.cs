namespace Stratum.Feature.PageContent.ComputedIndexFields
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common.Extensions;

    public class ProductImageUrl : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;

            if (item == null || item.TemplateID != Templates.ProductDetailsPage.ID)
                return null;

            return item.GetMediaItemUrl(Templates.ProductDetails.Fields.Image);
        }
    }
}