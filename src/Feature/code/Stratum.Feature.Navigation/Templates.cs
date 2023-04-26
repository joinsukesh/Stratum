
namespace Stratum.Feature.Navigation
{
    using Sitecore.Data;

    public class Templates
    {
        public struct NavLink
        {
            public static readonly ID ID = new ID("{FC1F3408-3F6F-4795-AC99-A7CBBB9B5CC4}");

            public struct Fields
            {
                public static readonly ID Link = new ID("{DD560F16-63AC-4B38-82A0-546367DF27B9}");
                public static readonly ID CSSClass = new ID("{EEFDD316-9BC9-4DE7-B32C-3D6AD51E4D16}");
            }
        }

        public struct Header
        {
            public static readonly ID ID = new ID("{D38B2B55-41A5-4288-8B71-BA4FED92DEF3}");

            public struct Fields
            {
                public static readonly ID Logo = new ID("{299323CA-1C95-438B-ABD8-FD40EEFE6DE1}");
                public static readonly ID LogoTargetURL = new ID("{F5FFFAD7-66EC-46D9-B527-B751AD0E577F}");
                public static readonly ID NavLinks = new ID("{9C915E75-47F9-4AE3-97BC-A149815E01C4}");
                public static readonly ID SignInPage = new ID("{3CDC6CF7-12AB-4FE7-B25F-8B0A102774D7}");
                public static readonly ID SignUpPage = new ID("{4AE62C71-2038-40B1-A402-BF74CD724463}");
                public static readonly ID SignOutPage = new ID("{1892B1C8-B22D-455C-A56A-A91902D53662}");
            }
        }

        public struct Footer
        {
            public static readonly ID ID = new ID("{20B3DC31-7ED7-432C-980D-0200A02E2B51}");

            public struct Fields
            {
                public static readonly ID Address = new ID("{5EA73BAA-3615-441F-A306-A275AAB6D4D9}");
                public static readonly ID SocialLinks = new ID("{D96398AD-9397-47ED-9804-8C58CF433816}");
                public static readonly ID Column2Title = new ID("{F0F95987-80FD-4FED-A5D5-22D8FFD807C8}");
                public static readonly ID Column2Links = new ID("{E8F0C75F-BE0C-4BE7-A41E-922F3687DE28}");
                public static readonly ID Column3Title = new ID("{3649A55C-216B-46F7-96B4-DF7BB7E80E7A}");
                public static readonly ID Column3Links = new ID("{786FFB2E-2BA1-4C2C-A2B2-8D9B329B072D}");
                public static readonly ID CopyrightSection = new ID("{A9DBF7B3-CEA0-4477-8160-789C862BDE18}");
            }
        }
    }
}