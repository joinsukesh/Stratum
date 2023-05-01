namespace Stratum.Feature.PageContent.Models.Product
{
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common.Utilities;
    using Stratum.Foundation.Search.Models;
    using Stratum.Foundation.Search.Services;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class ProductSearcher
    {
        private SearchService searchService = new SearchService();

        public BaseSearchResult<ProductSearchResultItem> GetSearchResult(string searchIndexName, string searchTerm, string tagId, int pageNumber, int pageSize)
        {
            var query = GetSearchQuery(searchTerm, tagId, pageNumber, pageSize);
            BaseSearchResult<ProductSearchResultItem> result = searchService.GetSearchResults<ProductSearchResultItem>(searchIndexName, query);
            return result;
        }

        private IQueryable<ProductSearchResultItem> GetSearchQuery(string searchTerm, string tagId, int pageNumber, int pageSize)
        {
            var predicate = GetSearchPredicate(searchTerm, tagId);
            IQueryable<ProductSearchResultItem> query = searchService.GetSearchQuery<ProductSearchResultItem>(Constants.SearchIndexes.Products, predicate);

            /// Apply pagination 
            query = query.Page((pageNumber - 1), pageSize);
            //query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            return query;
        }

        private Expression<Func<ProductSearchResultItem, bool>> GetSearchPredicate(string searchTerm, string tagId, string languageRegionalIsoCode = "en")
        {
            Item productDetailsTemplateItem = SitecoreUtility.GetItem(Templates.ProductDetailsPage.ID);
            string productDetailsTemplateName = productDetailsTemplateItem != null ? productDetailsTemplateItem.Name : string.Empty;

            /// Initialize queries with True for AND queries and False for OR queries
            var andPredicate = PredicateBuilder.True<ProductSearchResultItem>();

            if (!string.IsNullOrWhiteSpace(productDetailsTemplateName))
            {
                /// get only product pages of the specified language
                andPredicate = andPredicate.And(x => x.TemplateName.Equals(productDetailsTemplateName, StringComparison.InvariantCultureIgnoreCase));
                    
                ///get items of specified language
                andPredicate = andPredicate.And(x => x.Language.Equals(languageRegionalIsoCode, StringComparison.InvariantCultureIgnoreCase));

                /// get only active items
                andPredicate = andPredicate.And(x => x.IsActive);

                ///filter by searchTerm
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    ///filter by title
                    var titlePredicate = PredicateBuilder.False<ProductSearchResultItem>();
                    titlePredicate = titlePredicate.Or(x => x.Title.Like(searchTerm, 0.75f));

                    foreach (var t in searchTerm.Split(' '))
                    {
                        var tempTerm = t;
                        titlePredicate = titlePredicate.Or(p => p.Title.MatchWildcard(tempTerm + "*").Boost(5.5f));
                        titlePredicate = titlePredicate.Or(p => p.Title.MatchWildcard(tempTerm + "*"));
                        titlePredicate = titlePredicate.Or(p => p.Title.Equals(tempTerm));
                    }

                    andPredicate = andPredicate.And(titlePredicate);

                    ///filter by category
                    var categoryPredicate = PredicateBuilder.False<ProductSearchResultItem>();
                    categoryPredicate = categoryPredicate.Or(x => x.Category.Like(searchTerm, 0.75f));

                    foreach (var t in searchTerm.Split(' '))
                    {
                        var tempTerm = t;
                        categoryPredicate = categoryPredicate.Or(p => p.Category.MatchWildcard(tempTerm + "*").Boost(5.5f));
                        categoryPredicate = categoryPredicate.Or(p => p.Category.MatchWildcard(tempTerm + "*"));
                        categoryPredicate = categoryPredicate.Or(p => p.Category.Equals(tempTerm));
                    }

                    ///using the or condition here
                    andPredicate = andPredicate.Or(categoryPredicate);
                }

                ///filter by tag
                if (!string.IsNullOrWhiteSpace(tagId))
                {
                    Guid tagGuid = new Guid(tagId);
                    andPredicate = andPredicate.And(x => x.Tags.Contains(tagGuid));
                }
            }            

            return andPredicate;
        }        
    }
}