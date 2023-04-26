
namespace Stratum.Feature.PageContent.Controllers
{
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Stratum.Feature.Base;
    using Stratum.Feature.Base.Controllers;
    using Stratum.Feature.Base.Models;
    using Stratum.Feature.PageContent.Models;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;
    using Stratum.Foundation.Common.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class StratumGalleryController : Controller
    {
        public ActionResult RenderGallery()
        {
            Gallery viewModel = null;
            int totalImages = 0;

            try
            {
                Item datasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (datasourceItem != null && datasourceItem.TemplateID == Templates.Gallery.ID)
                {
                    viewModel = new Gallery(datasourceItem);

                    if (viewModel.IsActive)
                    {
                        viewModel.Images = GetImages(datasourceItem, 1, viewModel.PageSize, out totalImages);

                        if (viewModel.Images != null && viewModel.Images.Count > 0 && viewModel.ShowPagination)
                        {
                            viewModel.Pagination = new Pagination
                            {
                                ShowPagination = viewModel.ShowPagination,
                                PageSize = viewModel.PageSize,
                                SelectedPage = 1,
                                TotalPages = Pagination.GetTotalPages(totalImages, viewModel.PageSize),
                                DatasourceId = datasourceItem.ID.ToString()
                            };
                        }
                    }
                    else
                    {
                        viewModel = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                return View(BaseConstants.CommonErrorViewPath, ex);
            }

            return View(Constants.ViewsFolderPath + "Gallery.cshtml", viewModel);
        }

        [HttpGet]
        public ActionResult GetImagesByPage(string datasourceId, int selectedPage, string containerId)
        {
            BaseResponse response = new BaseResponse();
            Gallery viewModel = null;
            int totalImages = 0;
            int statusCode = 0;
            string statusMessage = string.Empty;
            string errorMessage = string.Empty;

            try
            {
                Item datasourceItem = SitecoreUtility.GetItem(datasourceId);

                if (datasourceItem != null && datasourceItem.TemplateID == Templates.Gallery.ID)
                {
                    viewModel = new Gallery(datasourceItem);
                    viewModel.Images = GetImages(datasourceItem, selectedPage, viewModel.PageSize, out totalImages);

                    if (viewModel.Images != null && viewModel.Images.Count > 0)
                    {
                        viewModel.Pagination = new Pagination
                        {
                            ShowPagination = viewModel.ShowPagination,
                            PageSize = viewModel.PageSize,
                            SelectedPage = selectedPage,
                            TotalPages = Pagination.GetTotalPages(totalImages, viewModel.PageSize),
                            DatasourceId = datasourceId,
                            ListContainerId = containerId
                        };
                    }

                    statusCode = 1;
                }
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
            return View(Constants.ViewsFolderPath + "_GalleryImages.cshtml", viewModel);
        }

        private List<GalleryImage> GetImages(Item datasourceItem, int selectedPage, int pageSize, out int totalImages)
        {
            List<GalleryImage> lst = null;
            totalImages = 0;
            int startIndex = 0;
            int numberOfItemsToGet = 0;
            List<GalleryImage> allImages = null;

            if (Session[Constants.Sessions.GalleryImages] != null)
            {
                allImages = (List<GalleryImage>)(Session[Constants.Sessions.GalleryImages]);

                if (allImages != null && allImages.Count > 0)
                {
                    totalImages = allImages.Count;
                    startIndex = Pagination.GetStartIndex(selectedPage, pageSize, totalImages);
                    numberOfItemsToGet = Pagination.GetNumberOfItemsToFetchFromList(startIndex, pageSize, totalImages);
                    lst = allImages.GetRange(startIndex, numberOfItemsToGet);
                }
            }

            if (lst == null || lst.Count == 0)
            {
                if (datasourceItem.HasChildren)
                {
                    Item[] selectedFolders = datasourceItem.GetMultilistFieldItems(Templates.Gallery.Fields.GalleryImageFolders);

                    if (selectedFolders != null && selectedFolders.Count() > 0)
                    {
                        List<Item> imageItems = new List<Item>();
                        List<Item> tempImageItems = new List<Item>();

                        foreach (Item folderItem in selectedFolders)
                        {
                            tempImageItems = folderItem.GetChildItemsByTemplate(Templates.GalleryImage.ID)
                                .Where(x => x.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive)).ToList();

                            if (tempImageItems != null && tempImageItems.Count > 0)
                            {
                                imageItems.AddRange(tempImageItems);
                            }
                        }

                        allImages = SitecoreUtility.GetClassObjectsFromItems<GalleryImage>(imageItems);
                        totalImages = allImages.Count;
                        startIndex = Pagination.GetStartIndex(selectedPage, pageSize, totalImages);
                        numberOfItemsToGet = Pagination.GetNumberOfItemsToFetchFromList(startIndex, pageSize, totalImages);
                        lst = allImages?.GetRange(startIndex, numberOfItemsToGet);
                        Session[Constants.Sessions.GalleryImages] = allImages;
                    }
                }
            }

            return lst;
        }
    }
}