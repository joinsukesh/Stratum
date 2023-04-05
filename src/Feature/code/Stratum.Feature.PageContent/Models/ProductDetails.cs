namespace Stratum.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;
    using Stratum.Foundation.Common.Models;
    using System.Collections.Generic;

    public class ProductDetails : CustomItem
    {
        public ProductDetails(Item innerItem) : base(innerItem) { }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
        }

        public string ImageId
        {
            get
            {
                return Templates.ProductDetails.Fields.Image.ToString();
            }
        }

        public string CategoryId
        {
            get
            {
                return Templates.ProductDetails.Fields.Category.ToString();
            }
        }

        public string PriceId
        {
            get
            {
                return Templates.ProductDetails.Fields.Price.ToString();
            }
        }

        public string Price
        {
            get
            {
                string price = string.Empty;
                decimal dPrice = 0;
                decimal.TryParse(InnerItem.GetString(Templates.ProductDetails.Fields.Price), out dPrice);

                if (dPrice > 0)
                {
                    price = dPrice.ToString("C", new System.Globalization.CultureInfo("en-US"));
                }

                return price;
            }
        }

        public string TitleId
        {
            get
            {
                return CommonTemplates.BaseContent.Fields.Title.ToString();
            }
        }

        public string DescriptionId
        {
            get
            {
                return CommonTemplates.BaseContent.Fields.Description.ToString();
            }
        }

        public string PageUrl
        {
            get
            {
                return InnerItem.Url();
            }
        }

        public List<string> TagNames
        {
            get
            {
                List<string> lstTagNames = null;
                Item[] selectedTags = InnerItem.GetMultilistFieldItems(Templates.ProductDetails.Fields.Tags);

                if (selectedTags != null && selectedTags.Length > 0)
                {
                    lstTagNames = new List<string>();
                    Tag tag = null;

                    foreach (Item item in selectedTags)
                    {
                        tag = new Tag(item);

                        if (tag != null && tag.IsActive && (!string.IsNullOrWhiteSpace(tag.TagName)))
                        {
                            lstTagNames.Add(tag.TagName);
                        }
                    }
                }

                return lstTagNames;
            }
        }
    }

}