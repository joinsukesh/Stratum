
namespace Stratum.Feature.Navigation
{
    using Sitecore.Data;

    public class Templates
    {
        public struct NavLink
        {
            public static readonly ID ID = new ID("{A2E1CF94-3A93-436C-B5AA-1875470EC9F4}");

            public struct Fields
            {
                public static readonly ID Link = new ID("{B2404F71-2717-4224-AEEC-5C34CA56477D}");
                public static readonly ID CSSClass = new ID("{4A8CD0E3-C735-42DD-AA2B-39FB454AEA17}");
            }
        }

        public struct Header
        {
            public static readonly ID ID = new ID("{37247FD3-E33C-4F65-8832-57F29731324B}");

            public struct Fields
            {
                public static readonly ID Logo = new ID("{517DC906-CE60-4E71-A67C-CB4DE077BFD5}");
                public static readonly ID LogoURL = new ID("{C0D56B45-59A7-4A85-826F-470A492802D1}");
                public static readonly ID NavLinks = new ID("{5E54C551-4171-4827-B1B0-6B719E1E50A6}");
                public static readonly ID SignInPage = new ID("{57ED1AD8-FE2A-41B0-999C-E77E829EFC76}");
                public static readonly ID SignUpPage = new ID("{375538CA-9847-4FDD-AE29-7D33CBE4DEC9}");
                public static readonly ID SignOutPage = new ID("{F1AC54AC-1074-4EF0-82AD-CC334712EBD8}");
            }
        }

        public struct Footer
        {
            public static readonly ID ID = new ID("{08F6F7EF-CC99-4404-9AE2-0B578EF8D1CF}");

            public struct Fields
            {
                public static readonly ID Address = new ID("{FEC8E5FA-7389-4008-ABF3-585D77846E07}");
                public static readonly ID SocialLinks = new ID("{947FE336-4C78-4CF3-BA48-F48F4328A6C6}");
                public static readonly ID Column2Title = new ID("{ED0A6B2D-B71F-4A69-8ED7-9180F99C003E}");
                public static readonly ID Column2Links = new ID("{70D8757B-3806-4A16-A47E-D20450531F9E}");
                public static readonly ID Column3Title = new ID("{DFC217CF-5699-4D57-9C24-48FEC00CF48D}");
                public static readonly ID Column3Links = new ID("{223FAB94-F5AA-4EC0-B5BA-7A320701B654}");
                public static readonly ID CopyrightSection = new ID("{57A3AA3C-AD17-45B4-9211-CFFD359FC0C5}");
            }
        }


    }
}