namespace Stratum.Feature.Accounts
{
    public class Constants
    {
        public static string ViewsFolderPath = "~/Views/Stratum/Accounts/";
        public static string ProductsParentItemId = "{C49D7669-69EE-4282-A463-14A76F64AFA3}";

        public struct EmailTemplates
        {
            public static string SignUpComplete = "{13319455-B421-4AED-ADFB-3BB7B292FB74}";
            public static string ForgotPassword = "{F974F859-F814-4035-A981-69E8C9838C0D}";
        }

        public struct Placeholders
        {
            public static string NAME = "$$NAME$$";
            public static string LINK = "$$LINK$$";
        }
    }
}