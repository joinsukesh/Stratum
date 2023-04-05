namespace Stratum.Feature.Forms
{
    using Stratum.Foundation.Common.Utilities;

    public class DictionaryValues
    {
        public struct Messages
        {
            public struct Status
            {
                public static string ContactUsFormSubmitSuccess
                {
                    get
                    {
                        string itemId = "{5F7BDCD1-9D2D-4750-A052-973A48F628ED}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }
            }

            public struct Validations
            {
                public static string InvalidContactUsMessage
                {
                    get
                    {
                        string itemId = "{31EC5B0A-7D64-482F-9215-823CBA0B07C2}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }
            }
        }
    }
}