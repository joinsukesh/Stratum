namespace Stratum.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;

    public class Testimonial : CustomItem
    {
        public Testimonial(Item innerItem) : base(innerItem) { }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
        }

        public string PersonNameId
        {
            get
            {
                return Templates.Testimonial.Fields.PersonName.ToString();
            }
        }

        public string PersonName
        {
            get
            {
                return InnerItem.GetString(Templates.Testimonial.Fields.PersonName);
            }
        }

        public string DesignationId
        {
            get
            {
                return Templates.Testimonial.Fields.Designation.ToString();
            }
        }

        public string Designation
        {
            get
            {
                return InnerItem.GetString(Templates.Testimonial.Fields.Designation);
            }
        }

        public string ImageId
        {
            get
            {
                return Templates.Testimonial.Fields.Image.ToString();
            }
        }

        public string ImageUrl
        {
            get
            {
                return InnerItem.GetMediaItemUrl(Templates.Testimonial.Fields.Image);
            }
        }

        public string CommentsId
        {
            get
            {
                return Templates.Testimonial.Fields.Comments.ToString();
            }
        }        
    }

}