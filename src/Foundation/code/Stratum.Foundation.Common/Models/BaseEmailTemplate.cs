namespace Stratum.Foundation.Common.Models
{
    using Sitecore.Data.Items;
    using Stratum.Foundation.Common.Extensions;

    public class BaseEmailTemplate : CustomItem
    {
        public BaseEmailTemplate(Item innerItem) : base(innerItem) { }

        public bool IsActive
        {
            get
            {
                return InnerItem.IsChecked(CommonTemplates.ActiveStatus.Fields.IsActive);
            }
        }

        public string FromEmail
        {
            get
            {
                return InnerItem.GetString(CommonTemplates.BaseEmailTemplate.Fields.FromEmail);
            }
        }

        public string ToEmails
        {
            get
            {
                return InnerItem.GetString(CommonTemplates.BaseEmailTemplate.Fields.ToEmails);
            }
        }

        public string Subject
        {
            get
            {
                return InnerItem.GetString(CommonTemplates.BaseEmailTemplate.Fields.Subject);
            }
        }

        public string Body
        {
            get
            {
                return InnerItem.GetString(CommonTemplates.BaseEmailTemplate.Fields.Body);
            }
        }

        public string CCEmails
        {
            get
            {
                return InnerItem.GetString(CommonTemplates.BaseEmailTemplate.Fields.CCEmails);
            }
        }

        public string BCCEmails
        {
            get
            {
                return InnerItem.GetString(CommonTemplates.BaseEmailTemplate.Fields.BCCEmails);
            }
        }        
    }

}
