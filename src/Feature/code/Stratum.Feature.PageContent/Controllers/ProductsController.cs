
namespace Stratum.Feature.PageContent.Controllers
{
    using Sitecore;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Stratum.Feature.Base;
    using Stratum.Feature.Base.Models;
    using Stratum.Feature.PageContent.Models.Product;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;
    using Stratum.Foundation.Common.Models;
    using Stratum.Foundation.Common.Services;
    using Stratum.Foundation.Common.Utilities;
    using Stratum.Foundation.Search.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants = Stratum.Feature.PageContent.Constants;

    public class ProductsController : Controller
    {
        private ProductSearcher productSearcher = new ProductSearcher();

        public ActionResult RenderProductsListing()
        {
            ProductsListing viewModel = null;
            int totalProducts = 0;

            try
            {
                viewModel = new ProductsListing();
                viewModel.SectionId = new CommonService().GetSectionId(Templates.ProductsListingRenderingParameters.ID);
                viewModel.Title = new MvcHtmlString(SitecoreUtility.GetRenderingParameter(Templates.ProductsListingRenderingParameters.ID, CommonTemplates.BaseContent.Fields.Title));
                viewModel.Description = new MvcHtmlString(SitecoreUtility.GetRenderingParameter(Templates.ProductsListingRenderingParameters.ID, CommonTemplates.BaseContent.Fields.Description));
                string pageSizeParam = SitecoreUtility.GetRenderingParameter(Templates.ProductsListingRenderingParameters.ID, CommonTemplates.PaginationSettings.Fields.PageSize);
                int pageSize = MainUtil.GetInt(pageSizeParam, 1);
                viewModel.PageSize = pageSize;

                /// get tag items
                List<SelectListItem> tags = new List<SelectListItem>();
                Item tagsFolderItem = SitecoreUtility.GetItem(CommonConstants.TagsFolderItemId);

                if (tagsFolderItem != null)
                {
                    IEnumerable<Tag> tagItems = tagsFolderItem.GetChildItemsByTemplate<Tag>(CommonTemplates.Tag.ID).Where(x => x.IsActive);

                    if (tagItems != null)
                    {
                        tags.Add(new SelectListItem
                        {
                            Text = "Select Tag",
                            Value = "",
                            Selected = true
                        });

                        foreach(Tag tag in tagItems)
                        {
                            tags.Add(new SelectListItem
                            {
                                Text = tag.TagName,
                                Value = tag.InnerItem.ID.ToString()                                
                            });
                        }

                        viewModel.Tags = tags;
                    }
                }

                BaseSearchResult<ProductSearchResultItem> result = productSearcher.GetSearchResult(Constants.SearchIndexes.Products, string.Empty, string.Empty, 1, pageSize);

                if (result != null)
                {
                    totalProducts = result.TotalResults;
                    viewModel.Products = result.ResultsByFilters;
                }

                if (viewModel.Products != null && viewModel.Products.Count > 0)
                {
                    bool showPagination = SitecoreUtility.GetRenderingParameter(Templates.ProductsListingRenderingParameters.ID, CommonTemplates.PaginationSettings.Fields.ShowPagination) == CommonConstants.One;

                    if (showPagination)
                    {
                        viewModel.Pagination = new Pagination
                        {
                            ShowPagination = showPagination,
                            PageSize = pageSize,
                            SelectedPage = 1,
                            TotalPages = Pagination.GetTotalPages(totalProducts, pageSize),
                            DatasourceId = string.Empty
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                return View(BaseConstants.CommonErrorViewPath, ex);
            }

            return View(Constants.ViewsFolderPath + "ProductsListing.cshtml", viewModel);
        }

        [HttpGet]
        public ActionResult GetProducts(string searchTerm, string tagId, string selectedPage, string pageSize)
        {
            BaseResponse response = new BaseResponse();
            ProductsListing viewModel = null;
            int totalProducts = 0;
            int statusCode = 0;
            string statusMessage = string.Empty;
            string errorMessage = string.Empty;

            try
            {
                viewModel = new ProductsListing();
                int pgSize = MainUtil.GetInt(pageSize, 1);
                int userSelectedPage = MainUtil.GetInt(selectedPage, 1);
                BaseSearchResult<ProductSearchResultItem> result = productSearcher.GetSearchResult(Constants.SearchIndexes.Products, searchTerm, tagId, userSelectedPage, pgSize);

                if (result != null)
                {
                    totalProducts = result.TotalResults;
                    viewModel.Products = result.ResultsByFilters;
                }

                if (viewModel.Products != null && viewModel.Products.Count > 0)
                {
                    viewModel.Pagination = new Pagination
                    {
                        ShowPagination = true,
                        PageSize = pgSize,
                        SelectedPage = userSelectedPage,
                        TotalPages = Pagination.GetTotalPages(totalProducts, pgSize),
                        DatasourceId = string.Empty
                    };
                }

                statusCode = 1;
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                statusCode = 0;
                statusMessage = CommonDictionaryValues.Messages.Errors.Generic;
                errorMessage = ex.ToString();
            }

            viewModel.Response = new BaseResponse
            {
                StatusCode = statusCode,
                StatusMessage = statusMessage,
                ErrorMessage = errorMessage
            };

            return View(Constants.ViewsFolderPath + "_ProductTiles.cshtml", viewModel);
        }
    }
}