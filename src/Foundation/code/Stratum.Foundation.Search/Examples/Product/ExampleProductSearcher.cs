namespace Stratum.Foundation.Search.Examples.Product
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.ContentSearch.SearchTypes;
    using Stratum.Foundation.Search.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This class handles the following use case:
    /// USE CASE: 
    /// The overall search is performed in the ExampleProductSearcher type by the user entering a search term, a page number and a page size, that is wrapped in the SearchCriteria type.
    /// The search term will be used by the searcher to look for products that matches the search term. 
    /// The page number and page size is used to determine how many results that should be returned when performing the search. 
    /// Additionally, the searcher will also look for products, which are created within the last year of the time the search occurs, and lastly some sorting will be applied.
    /// Once the search results are fetched, these are mapped to the ExampleProduct model, and returned together with the total search result count, as a ExampleProductSearchResult type.
    /// </summary>
    public class ExampleProductSearcher
    {
        protected ExampleProductSearchResult Search(SearchCriteria criteria)
        {
            using (var context = ContentSearchManager.GetIndex("sitecore_master_index").CreateSearchContext())
            {
                /// Initialize queries with True for AND queries and False for OR queries
                var filterPredicate = PredicateBuilder.True<SearchResultItem>();

                /// Only take products created over the past year
                filterPredicate = filterPredicate
                                 .And(x => x.CreatedDate.Between(DateTime.Now.Date.AddYears(-1), DateTime.Now.Date, Inclusion.Both));

                /// Query by the search term
                /// Initialize queries with True for AND queries and False for OR queries
                var searchTermPredicate = PredicateBuilder.False<SearchResultItem>();
                searchTermPredicate = searchTermPredicate
                                     .Or(x => x.Name.Like(criteria.SearchTerm, 0.75f))
                                     .Or(x => x.Content.Contains(criteria.SearchTerm));

                /// Construct final filter predicate, and apply filter 
                var predicate = filterPredicate.And(searchTermPredicate);
                var query = context.GetQueryable<SearchResultItem>().Filter(predicate);
                 
                /// Apply sorting
                query = query.OrderBy(x => x.CreatedDate).ThenBy(x => x.Name);

                /// Apply pagination 
                query = query.Page(criteria.PageNumber, criteria.PageSize);

                /// Fetch the results
                var results = query.GetResults();
                int totalResults = results.TotalSearchResults;
                var productResults = results.Hits.Select(x=> MapSearchResultItemToProduct(x.Document))?.ToList();

                return new ExampleProductSearchResult
                {
                    NumberOfResults = totalResults,
                    Results = productResults
                };
            }
        }

        /// Map to a Product object...
        protected ExampleProduct MapSearchResultItemToProduct(SearchResultItem item)
        {
            ExampleProduct pdt = new ExampleProduct();
            //pdt.Name = item.Name;
            return pdt;
        }
    }

    public class ExampleProduct
    {

    }

    public class ExampleProductSearchResult
    {
        public int NumberOfResults { get; set; }
        public List<ExampleProduct> Results { get; set; }
    }
}
