namespace Stratum.Feature.PageContent.Models.Product
{
    using Stratum.Feature.Base.Models;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ProductsListing
    {
        public List<ProductSearchResultItem> Products { get; set; }
        public Pagination Pagination { get; set; }
        public string SectionId { get; set; }
        public MvcHtmlString Title { get; set; }
        public MvcHtmlString Description { get; set; }
        public string SearchKeyword { get; set; }
        public string TagId { get; set; }
        public List<SelectListItem> Tags { get; set; }
        public BaseResponse Response { get; set; }
        public int PageSize { get; set; }
    }
}