namespace Stratum.Foundation.Accounts
{
    public class Constants
    {
        public struct Profile
        {
            ////sitecore/system/Settings/Security/Profiles/Stratum User
            public static string ProfileItemId = "{92C4F61C-C9CE-48BE-8577-E03EEF82CE3C}";

            public struct ProfileProperties
            {
                public static string SignUpSecretKey = "SignUpSecretKey";
                public static string IsSignUpComplete = "Is SignUp Complete";
                public static string ForgotPasswordSecretKey = "ForgotPasswordSecretKey";
                public static string HasResetPassword = "Has Reset Password";
            }
        }
    }
}
