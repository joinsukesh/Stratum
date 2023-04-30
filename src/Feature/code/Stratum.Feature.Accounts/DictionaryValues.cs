namespace Stratum.Feature.Accounts
{
    using Stratum.Foundation.Common.Utilities;

    public class DictionaryValues
    {
        public struct Labels
        {
            public static string Password
            {
                get
                {
                    string itemId = "{D11DC25D-5930-4A49-8FC9-F8FC8B4C0FD6}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string NewPassword
            {
                get
                {
                    string itemId = "{D75B7250-D215-41D8-A71C-3485AA2727C0}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string ConfirmPassword
            {
                get
                {
                    string itemId = "{C9B12883-1E50-449E-989B-467978349510}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }
        }

        public struct Messages
        {
            public struct Errors
            {
                public static string AccountUnavailable
                {
                    get
                    {
                        string itemId = "{F2C55267-D914-4399-97E2-B45A869C5879}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string AccountEmailExists
                {
                    get
                    {
                        string itemId = "{29B38AAA-031A-4D7C-8DAB-D152893BEA29}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string IncompleteSignUp
                {
                    get
                    {
                        string itemId = "{9B2D64C6-2DB4-4DE8-8FBE-50C658870A28}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string InvalidSignInCredentials
                {
                    get
                    {
                        string itemId = "{4CABB57E-2DD9-4FDB-B2B1-15701D87E774}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }
            }

            public struct Status
            {
                public static string RegistrationSuccess
                {
                    get
                    {
                        string itemId = "{42A21041-5894-4CFB-A411-4507B50065D3}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string ResetPasswordEmailSent
                {
                    get
                    {
                        string itemId = "{7DE4AA61-E4C4-461D-A44B-7CC1273D45C6}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string PasswordResetSuccess
                {
                    get
                    {
                        string itemId = "{944A811A-452F-4929-80AA-21679FA585E4}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }
            }
        }
    }
}