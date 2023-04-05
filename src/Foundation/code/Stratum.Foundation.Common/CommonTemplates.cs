namespace Stratum.Foundation.Common
{
    using Sitecore.Data;

    public class CommonTemplates
    {
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
            public static readonly ID ID = new ID("{C3816D4C-016E-45A9-8C24-3F7FDCE539FB}");

            public struct Fields
            {
                public static readonly ID IsActive = new ID("{3844EE29-DD02-420D-957B-1E8DD854738A}");
            }
        }

        public struct BaseRenderingParameters
        {
            public static readonly ID ID = new ID("{F2DB120D-EB7A-4AC8-A24A-F9D254899C15}");

            public struct Fields
            {
                public static readonly ID SectionId = new ID("{576D1A8B-308E-4462-A764-4ACE817328D0}");
                public static readonly ID AddDefaultBackgroundColor = new ID("{D7311633-DCF8-4367-A732-CF5E7D880A09}");
                public static readonly ID BackgroundColor = new ID("{CA9C11F7-FB87-4672-9823-5C81A88A5617}");
            }
        }

        public struct BaseContent
        {
            public static readonly ID ID = new ID("{DCF060E3-94C6-4078-B635-4B8DE70A25E1}");

            public struct Fields
            {
                public static readonly ID Title = new ID("{4651C31E-23B6-4352-93D2-15A12599FB81}");
                public static readonly ID Description = new ID("{0AD95964-56C5-489A-9FE8-A6C7FE729E5A}");
            }
        }

        public struct PaginationSettings
        {
            public static readonly ID ID = new ID("{0C961468-90C3-43B3-94EF-47F5196BED9C}");

            public struct Fields
            {
                public static readonly ID ShowPagination = new ID("{6C1F93A9-5F09-458E-91A7-EB7C5D88B1A8}");
                public static readonly ID PageSize = new ID("{BB4ED4B7-295B-4C29-80C2-CF5DAF135611}");
            }
        }

        public struct Form
        {
            public static readonly ID ID = new ID("{6ABEE1F2-4AB4-47F0-AD8B-BDB36F37F64C}");
        }

        public struct BaseEmailTemplate
        {
            public static readonly ID ID = new ID("{703D2083-7B0F-42B5-9804-3D69506069BE}");

            public struct Fields
            {
                public static readonly ID FromEmail = new ID("{747E8772-4DBE-44D8-8155-E8343F9424ED}");
                public static readonly ID ToEmails = new ID("{C8B14B2D-871A-4A50-AA69-A47C02A01D60}");
                public static readonly ID Subject = new ID("{16F28161-CC7E-4E41-816B-F848CBE8D4A4}");
                public static readonly ID Body = new ID("{7D510DC3-061F-48F4-B575-571535FB5378}");
                public static readonly ID CCEmails = new ID("{4374FB84-F726-4BBD-8BB7-F7FCEBAAB5C5}");
                public static readonly ID BCCEmails = new ID("{81F5C944-A135-4ECB-A2CA-047BCBFDC766}");
            }
        }

        public struct Tag
        {
            public static readonly ID ID = new ID("{10896EAA-9BAB-4B50-8577-EAB3495518AA}");

            public struct Fields
            {
                public static readonly ID TagName = new ID("{72B389B1-23C4-4935-8D68-4F7DA1F53657}");
            }
        }
    }
}
