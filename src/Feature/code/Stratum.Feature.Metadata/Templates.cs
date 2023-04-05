namespace Stratum.Feature.Metadata
{
    using Sitecore.Data;

    public class Templates
    {
        public struct PageMetadata
        {
            public static readonly ID ID = new ID("{81FE32D5-1604-4C9A-AF0E-6AB2F0C054E2}");

            public struct Fields
            {
                public static readonly ID MetaTitle = new ID("{6DEF6F3A-46DF-4494-924F-605B8A490FCA}");
                public static readonly ID MetaDescription = new ID("{0DD79924-61CA-4283-8E87-5242286634F7}");
                public static readonly ID GenerateDynamicCanonicalURL = new ID("{94F31FC0-D23A-47FB-8E92-315FE3431040}");
                public static readonly ID CanonicalURL = new ID("{B2346818-AB8F-4C20-BC2C-90B914582DA8}");
                public static readonly ID OGTitle = new ID("{1C4BB3CA-08AF-4856-99FB-A4FF4AD83943}");
                public static readonly ID OGDescription = new ID("{79DEB299-3A40-4F40-8961-41188640AF4F}");
                public static readonly ID OGURL = new ID("{57CD4AF4-78D1-41DF-83B0-9923852A4292}");
                public static readonly ID OGImage = new ID("{604ED346-034F-40DF-B33E-E690482BF540}");
                public static readonly ID IndexAndFollow = new ID("{8829A74B-03C2-4606-AB08-DBEE511C38A3}");
                public static readonly ID OtherMetaTags = new ID("{FFF88314-76BB-4E9D-A10D-F87E55BC3806}");
            }
        }

        public struct SiteMetadata
        {
            public static readonly ID ID = new ID("{20B4BE58-5007-4DAC-B094-2FA0D1F085D9}");

            public struct Fields
            {
                public static readonly ID SiteMetaTags = new ID("{B8F4EB62-AB1F-4D33-BD97-7FB300C1EBC6}");
                public static readonly ID MetaTitleAppend = new ID("{0E947D0F-D473-4FAD-A013-9849F6DCF21B}");
                public static readonly ID Favicons = new ID("{C2E000D1-E481-4F22-ABF0-458B77A5D5FC}");
            }
        }
    }
}