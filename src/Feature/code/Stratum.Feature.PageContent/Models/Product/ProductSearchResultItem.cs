namespace Stratum.Feature.PageContent.Models.Product
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;
    using System;
    using System.Collections.Generic;

    public class ProductSearchResultItem : SearchResultItem
    {
        [IndexField("is_active")]
        public bool IsActive { get; set; }

        [IndexField("title")]
        public string Title { get; set; }

        [IndexField("description")]
        public string Description { get; set; }

        [IndexField("category")]
        public string Category { get; set; }

        [IndexField("tags")]
        public IEnumerable<Guid> Tags { get; set; }

        [IndexField("productimageurl")]
        public string ProductImageUrl { get; set; }

        [IndexField("producttags")]
        public List<string> ProductTags { get; set; }

        [IndexField("productpricedisplay")]
        public string ProductPriceDisplay { get; set; }
    }
}