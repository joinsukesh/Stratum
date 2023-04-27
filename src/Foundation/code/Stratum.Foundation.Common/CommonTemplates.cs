namespace Stratum.Foundation.Common
{
    using Sitecore.Data;

    public class CommonTemplates
    {
        /// <summary>
        ////sitecore/templates/Stratum/Project/Website
        /// </summary>
        public struct Website
        {
            public static ID ID => new ID("{70106775-721D-4592-B645-1DC5F6624FD6}");
        }

        public struct DictionaryEntry
        {
            public static ID ID => new ID("{6D1CD897-1936-4A3A-A511-289A94C2A7B1}");

            public struct Fields
            {
                public static ID Phrase => new ID("{2BA3454A-9A9C-4CDF-A9F8-107FD484EB6E}");

                public static ID Key => new ID("{580C75A8-C01A-4580-83CB-987776CEB3AF}");
            }
        }

        public struct ActiveStatus
        {
            public static readonly ID ID = new ID("{76F14742-2033-43A5-B0A5-9C16DCF411B0}");

            public struct Fields
            {
                public static readonly ID IsActive = new ID("{89F162F8-13E0-40C0-ABAF-BA76492CA60A}");
            }
        }

        public struct BaseRenderingParameters
        {
            public static readonly ID ID = new ID("{37B76B46-C759-4C97-A22D-4556BAE7F74C}");

            public struct Fields
            {
                public static readonly ID SectionId = new ID("{78BE52B1-2628-46DF-984C-54AB53B8CADE}");
                public static readonly ID AddDefaultBackgroundColor = new ID("{09328E33-7A55-44FE-98F0-24461C7A8FFA}");
                public static readonly ID BackgroundColor = new ID("{11DE13B9-F4EC-42D5-9366-F951D58ACDCB}");
            }
        }

        public struct BaseContent
        {
            public static readonly ID ID = new ID("{1F4A7579-A7E5-4160-97D6-6315A10EBA24}");

            public struct Fields
            {
                public static readonly ID Title = new ID("{56EACD06-8389-44DB-A78B-D6B78FA11684}");
                public static readonly ID Description = new ID("{127CCB6A-3AF8-4965-B27F-DE7A552A09C2}");
            }
        }

        public struct PaginationSettings
        {
            public static readonly ID ID = new ID("{3D6A3DD7-6B55-42B3-B4D9-F6A5742F1EF6}");

            public struct Fields
            {
                public static readonly ID ShowPagination = new ID("{85068CEF-4F82-4C1F-A884-434F742BAA03}");
                public static readonly ID PageSize = new ID("{0A33FB6A-C9E7-4C81-A94F-DB7975D30D42}");
            }
        }

        public struct Form
        {
            public static readonly ID ID = new ID("{6ABEE1F2-4AB4-47F0-AD8B-BDB36F37F64C}");
        }

        public struct BaseEmailTemplate
        {
            public static readonly ID ID = new ID("{BEA9D87A-2B58-4DC1-923F-FB5CF7869715}");

            public struct Fields
            {
                public static readonly ID FromEmail = new ID("{D64940E1-7838-42A4-A0AA-BFF3A5860BA2}");
                public static readonly ID ToEmails = new ID("{0CC63E74-3BA1-477B-82F8-2B10707CD62F}");
                public static readonly ID CCEmails = new ID("{B75FA825-82B7-4168-8D58-E72FA6557C70}");
                public static readonly ID BCCEmails = new ID("{84BAAB2C-8533-45E9-9653-4572B4B2B471}");
                public static readonly ID Subject = new ID("{2C59C452-9580-4588-AA12-F9F4857E819D}");
                public static readonly ID Body = new ID("{AAFE9007-6CCD-49FD-BF8C-66C1195F5932}");
                
            }
        }

        public struct Tag
        {
            public static readonly ID ID = new ID("{E3624552-5ED9-42E4-9BF6-552EE27CF833}");

            public struct Fields
            {
                public static readonly ID TagName = new ID("{24B97149-7AB0-4D92-9C79-68733547D8D8}");
            }
        }
    }
}
