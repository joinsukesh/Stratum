namespace Stratum.Feature.PageContent.Models
{
    using Sitecore;
    using Sitecore.Data.Items;
    using Stratum.Feature.Base.Models;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;
    using System.Collections.Generic;

    public class Gallery : CustomItem
    {
        public Gallery(Item innerItem) : base(innerItem) { }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
        }

        public string GalleryImagesFolderId
        {
            get
            {
                return Templates.Gallery.Fields.GalleryImageFolders.ToString();
            }
        }

        public string TitleId
        {
            get
            {
                return CommonTemplates.BaseContent.Fields.Title.ToString();
            }
        }

        public string Title
        {
            get
            {
                return InnerItem.GetString(CommonTemplates.BaseContent.Fields.Title);
            }
        }

        public string DescriptionId
        {
            get
            {
                return CommonTemplates.BaseContent.Fields.Description.ToString();
            }
        }

        public string Description
        {
            get
            {
                return InnerItem.GetString(CommonTemplates.BaseContent.Fields.Description);
            }
        }

        public int TotalImages
        {
            get
            {
                int totalImages = 0;
                if (InnerItem.HasChildren)
                {

                }
                    return totalImages;
            }
        }

        public List<GalleryImage> Images { get; set; }

        public bool ShowPagination
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.PaginationSettings.Fields.ShowPagination);
            }
        }

        public int PageSize
        {
            get
            {
                return MainUtil.GetInt(InnerItem.GetString(CommonTemplates.PaginationSettings.Fields.PageSize), 0);
            }
        }

        public Pagination Pagination { get; set; }

        public BaseResponse Response { get; set; }
    }

}