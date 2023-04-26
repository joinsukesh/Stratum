namespace Stratum.Feature.Accounts
{
    public class Constants
    {
        public static string ViewsFolderPath = "~/Views/Stratum/Accounts/";
        public static string ProductsParentItemId = "{7C610233-D958-41AA-931F-34806D2E0C4E}";

        public struct EmailTemplates
        {
            public static string SignUpComplete = "{1492E95F-39EE-465B-A988-00684CA0873C}";
            public static string ResetPassword = "{B0A44A5C-DA71-499F-B716-E6CBFA626678}";
        }

        public struct Placeholders
        {
            public static string NAME = "$$NAME$$";
            public static string LINK = "$$LINK$$";
        }
    }
}