namespace Stratum.Feature.Metadata
{
    using Sitecore.Data;

    public class Templates
    {
        public struct PageMetadata
        {
            public static readonly ID ID = new ID("{256EE919-060B-4F6D-85E3-43CAE81E8125}");

            public struct Fields
            {
                public static readonly ID MetaTitle = new ID("{A201B976-5DEE-4787-A901-C6638BF7E958}");
                public static readonly ID MetaDescription = new ID("{083C5646-8D72-408C-AEE8-3D7598245D02}");
                public static readonly ID GenerateDynamicCanonicalURL = new ID("{620A0EC2-503D-4457-A181-3949838D812B}");
                public static readonly ID CanonicalURL = new ID("{0559B554-3781-4632-8EAC-64E5FBDD2C62}");
                public static readonly ID OGTitle = new ID("{879A4E4B-6BCD-4873-B655-941E0D1E41E3}");
                public static readonly ID OGDescription = new ID("{774B0F25-4867-40DB-A576-BCDD5793686E}");
                public static readonly ID OGURL = new ID("{6725173F-E80A-4DAF-8B58-B4FAAE9938A8}");
                public static readonly ID OGImage = new ID("{9B52082F-8B47-4BD0-BA6A-3508FDE11284}");
                public static readonly ID IndexAndFollow = new ID("{99F0CD4D-1EED-435C-90F1-EA6A86314302}");
                public static readonly ID OtherMetaTags = new ID("{365134A4-DD38-48EA-AC09-23F890A2418C}");
            }
        }

        public struct SiteMetadata
        {
            public static readonly ID ID = new ID("{A2AF5603-C0DE-4590-BF09-A2AEB626D3F9}");

            public struct Fields
            {
                public static readonly ID SiteMetaTags = new ID("{B1A9F408-D008-4D34-86BA-5EEF1C6951EE}");
                public static readonly ID MetaTitleAppend = new ID("{02F10EB0-4D8D-426D-817F-CBA12050F667}");
                public static readonly ID Favicons = new ID("{B200E399-FE15-4400-945A-7EB97C63E4C6}");
            }
        }
    }
}