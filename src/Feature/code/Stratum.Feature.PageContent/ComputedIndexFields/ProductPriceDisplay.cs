namespace Stratum.Feature.PageContent.ComputedIndexFields
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common.Extensions;

    public class ProductPriceDisplay : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;

            if (item == null || item.TemplateID != Templates.ProductDetailsPage.ID)
                return null;

            string price = string.Empty;
            decimal dPrice = 0;
            decimal.TryParse(item.GetString(Templates.ProductDetails.Fields.Price), out dPrice);

            if (dPrice > 0)
            {
                price = dPrice.ToString("C", new System.Globalization.CultureInfo("en-US"));
            }

            return price;
        }
    }
}