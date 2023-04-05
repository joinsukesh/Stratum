namespace Stratum.Foundation.Search.Models
{
    using Sitecore.ContentSearch.SearchTypes;
    using System.Collections.Generic;

    public class BaseSearchResult<T> where T: SearchResultItem
    {
        public int TotalResults { get; set; }
        public List<T> ResultsByFilters { get; set; }
    }
}
