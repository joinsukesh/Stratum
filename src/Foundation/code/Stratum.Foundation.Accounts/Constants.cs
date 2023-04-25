namespace Stratum.Foundation.Accounts
{
    public class Constants
    {
        public struct Profile
        {
            ////sitecore/system/Settings/Security/Profiles/Stratum User
            public static string ProfileItemId = "{985A8C5D-234E-42D5-997D-ABD6AD04A375}";

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
