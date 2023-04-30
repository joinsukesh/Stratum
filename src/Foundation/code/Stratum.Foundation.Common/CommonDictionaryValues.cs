
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
                        string itemId = "{2309F53B-00C6-47F6-9F24-FDB007FFE4A6}";
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
                        string itemId = "{3DE07CB0-DA04-445E-BCD8-618E73066C31}";
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
                        string itemId = "{73FF6BAB-26A2-46C0-A410-23C6DA8CE008}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string InvalidName
                {
                    get
                    {
                        string itemId = "{946A610D-D118-4DAC-AEFC-D1165D835BE6}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string InvalidFirstName
                {
                    get
                    {
                        string itemId = "{075A52D2-1464-4262-8764-773E69FB5F1F}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string InvalidLastName
                {
                    get
                    {
                        string itemId = "{6B03E16F-4E11-450F-BA76-49DCD2E30587}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string InvalidSubject
                {
                    get
                    {
                        string itemId = "{C86933AD-B9C4-4E94-9CEE-C0059034C3B6}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string InvalidPassword
                {
                    get
                    {
                        string itemId = "{D0EABCF6-50B2-43E2-97F4-05663E284F4E}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string PasswordsMismatch
                {
                    get
                    {
                        string itemId = "{C7FB011A-91BE-439F-96A0-9DB11025CB42}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string RequiredEmail
                {
                    get
                    {
                        string itemId = "{F61CCE14-FEC3-45B8-ADAC-776A2945366E}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }

                public static string RequiredPassword
                {
                    get
                    {
                        string itemId = "{EE7A4F03-DABF-474F-8F9B-73AD0D73641E}";
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
                    string itemId = "{7E6DB217-95A7-4843-BF75-C0A2CEF8E740}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string FirstName
            {
                get
                {
                    string itemId = "{27F2957D-08BA-4FCD-A763-784168A7109D}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string LastName
            {
                get
                {
                    string itemId = "{36C22FC2-B57E-41CD-97C2-DA80883A3C2D}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string Email
            {
                get
                {
                    string itemId = "{BF9A0A7C-FF23-4386-896B-142FFA79318D}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string Subject
            {
                get
                {
                    string itemId = "{F0AAA237-5A84-4DC3-A38E-753D93906D9F}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string Message
            {
                get
                {
                    string itemId = "{D634D755-69D2-4B6B-AEE6-EBF79E2A8C7F}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }            

            public static string Submit
            {
                get
                {
                    string itemId = "{BE64D56A-78C6-4933-B3A5-7CD128D991A6}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string RememberMe
            {
                get
                {
                    string itemId = "{1E62FB5A-6662-4C65-85BB-D3F4FF174623}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string SignIn
            {
                get
                {
                    string itemId = "{E3923EE9-0375-4805-8DFD-46340AD9D57E}";
                    string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                    return value;
                }
            }

            public static string Search
            {
                get
                {
                    string itemId = "{C89CA148-B99F-4566-AF69-6F636245CFF3}";
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
