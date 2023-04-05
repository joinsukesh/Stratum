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
                    string itemId = "{A950FB01-BC4C-424C-870A-875733B5476E}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string NewPassword
            {
                get
                {
                    string itemId = "{B8AB3701-E305-4660-A94D-A7C78F6255EC}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string ConfirmPassword
            {
                get
                {
                    string itemId = "{E8030853-601B-4811-89E5-5606B208957E}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }
        }

        public struct Messages
        {
            public struct Errors
            {
                public static string AccountDoesNotExist
                {
                    get
                    {
                        string itemId = "{F742C334-9BF4-4BBF-BB1B-3650B241579A}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string AccountEmailExists
                {
                    get
                    {
                        string itemId = "{5CA9D4FB-6F03-4E69-A3E5-D9DA402A8213}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string IncompleteSignUp
                {
                    get
                    {
                        string itemId = "{39FAEE55-E49C-415B-865A-BA1AE4DA9C86}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string InvalidSignInCredentials
                {
                    get
                    {
                        string itemId = "{ED2F96BB-D1CA-44ED-86BE-E48F67DC0582}";
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
                        string itemId = "{519D593C-F9C3-4774-9131-DDD25B659D7C}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string ResetPasswordEmailSent
                {
                    get
                    {
                        string itemId = "{6B640B48-2695-4704-9FC3-D62661A91700}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string PasswordResetSuccess
                {
                    get
                    {
                        string itemId = "{38346402-D81C-4AAD-A4BB-4052EBE9975E}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }
            }
        }
    }
}