
namespace Stratum.Foundation.Common
{
    using Stratum.Foundation.Common.Utilities;

    public class CommonDictionaryValues
    {
        public struct Messages
        {
            public struct Errors
            {
                public static string Generic
                {
                    get
                    {
                        string itemId = "{DF0E66B7-9AF9-440C-A1EA-68A83E5DF4E4}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }
            }

            public struct Status
            {
                public static string NoResultsFound
                {
                    get
                    {
                        string itemId = "{7CBFD7FF-0C45-4C3C-8697-91EFB4882CC8}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }
            }

            public struct Validations
            {
                public static string InvalidEmail
                {
                    get
                    {
                        string itemId = "{BFC8E9FF-1ADF-472F-9254-F9D1C3D1DB67}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string InvalidName
                {
                    get
                    {
                        string itemId = "{AB5AE1BE-8E4B-416D-8D57-DAD0AE7A7EA8}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string InvalidFirstName
                {
                    get
                    {
                        string itemId = "{7E6B8930-FF37-4EDC-88AE-343A5AD09F6E}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string InvalidLastName
                {
                    get
                    {
                        string itemId = "{EDC49502-CCCC-413B-B09F-6BDAE07C6AEE}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string InvalidSubject
                {
                    get
                    {
                        string itemId = "{EACD2F87-4A86-4820-9DBE-74FE21748BF0}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string InvalidPassword
                {
                    get
                    {
                        string itemId = "{C16F72FF-26EE-4E07-9954-17E6C1D55431}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string PasswordsMismatch
                {
                    get
                    {
                        string itemId = "{0254A5DB-2A8E-48C4-B10E-143A55DA3035}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string RequiredEmail
                {
                    get
                    {
                        string itemId = "{ABF552A4-28B2-4DB7-A5F3-4CD623399CAC}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string RequiredPassword
                {
                    get
                    {
                        string itemId = "{D02A4F26-13ED-49F4-98FF-61F46EEB7B46}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }
            }
        }
        
        public struct Labels
        {
            public static string Name
            {
                get
                {
                    string itemId = "{429115BE-9328-465A-9A85-A4BD09715F92}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string FirstName
            {
                get
                {
                    string itemId = "{04437B70-871E-4C34-9271-88A5D427E670}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string LastName
            {
                get
                {
                    string itemId = "{4E8B710F-1FAE-4D44-A647-91A49320F8A6}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string Email
            {
                get
                {
                    string itemId = "{E592B697-A011-457F-A20D-ED27A0297C05}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string Subject
            {
                get
                {
                    string itemId = "{1FE75D35-880C-49E2-B53C-12CB8494DD4C}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string Message
            {
                get
                {
                    string itemId = "{B81482F8-BEF1-48EC-BA2C-07C8F2AC3346}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }            

            public static string Submit
            {
                get
                {
                    string itemId = "{00DBDBB5-AC42-4FB1-AF1C-C8ED0E153155}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string RememberMe
            {
                get
                {
                    string itemId = "{D240DB8C-B96D-4507-893C-BE96E64AA8C6}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string SignIn
            {
                get
                {
                    string itemId = "{1947B5AF-DAB6-4669-9207-D46764646A05}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string Search
            {
                get
                {
                    string itemId = "{0F063D69-964B-4E79-9F88-A35CD962992A}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }
        }

        public struct Settings
        {
            ///sitecore/system/Settings/Stratum/Project/Site URL
            public static string SiteUrl
            {
                get
                {
                    string itemId = "{56D39DAE-8C39-4177-B36B-3707F7EE8923}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }
        }

        public struct GeoIp
        {
            public static int GetGeoIpDataWaitMilliseconds
            {
                get
                {
                    return 500;
                    //string itemId = "";
                    //int value = MainUtil.GetInt(SitecoreUtility.GetDictionaryPhraseValue(itemId), 500);
                    //return value;
                }
            }
        }

        public struct MaxMind
        {
            public static string GeoipApiUrl
            {
                get
                {
                    string itemId = "";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string GeoIpApiPassword
            {
                get
                {
                    string itemId = "";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string GeoipApiUserId
            {
                get
                {
                    string itemId = "";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }
        }

        public struct Google
        {
            public static string GoogleApiKey
            {
                get
                {
                    string itemId = "";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string GoogleDirectionsUrl
            {
                get
                {
                    string itemId = "";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string GoogleGeocodeApiKey
            {
                get
                {
                    string itemId = "";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string GoogleGeocodeUrl
            {
                get
                {
                    string itemId = "";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string GoogleMapsApiKey
            {
                get
                {
                    string itemId = "";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }
        }
    }
}
