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
                        string itemId = "{C20C7E49-E1DD-4157-A2EE-EA4CBC6A0D77}";
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
                        string itemId = "{CF54C464-4F71-4763-96CF-B4EC3B3E831F}";
                        string value = SitecoreUtility.GetDictionaryPhraseValue(itemId);
                        return value;
                    }
                }
            }
        }
    }
}