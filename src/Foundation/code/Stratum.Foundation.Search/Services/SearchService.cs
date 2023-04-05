namespace Stratum.Foundation.Search.Services
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.SearchTypes;
    using Stratum.Foundation.Search.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class SearchService
    {
        public IQueryable<T> GetSearchQuery<T>(string searchIndexName, Expression<Func<T, bool>> predicate) where T : SearchResultItem
        {
            IQueryable<T> query;

            using (var context = ContentSearchManager.GetIndex(searchIndexName).CreateSearchContext())
            {
                query = context.GetQueryable<T>().Where(predicate);
            }

            return query;
        }

        public BaseSearchResult<T> GetSearchResults<T>(string searchIndexName, IQueryable<T> query) where T : SearchResultItem
        {
            int totalResults = 0;
            List<T> resultsByFilters = null;

            /// get the index
            ISearchIndex searchIndex = ContentSearchManager.GetIndex(searchIndexName);

            /// create a search context
            using (IProviderSearchContext context = searchIndex.CreateSearchContext())
            {
                /// get results from this index based on query
                SearchResults<T> searchResults = query.GetResults();

                if (searchResults != null)
                {
                    totalResults = searchResults.TotalSearchResults;
                    resultsByFilters = searchResults.Hits.Select(x => x.Document)?.ToList();
                }

                return new BaseSearchResult<T>
                {
                    TotalResults = totalResults,
                    ResultsByFilters = resultsByFilters
                };
            }
        }
    }
}
