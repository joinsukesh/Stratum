
namespace Stratum.Foundation.Api.Models
{
    using System;
    using System.Collections.Generic;

    public class ApiLogger
    {
        public ApiLogger()
        {
            RequestHeaderKeyValues = new List<KeyValuePair<string, string>>();
        }

        public struct Properties
        {
            public static string Divider = "===========================================================================";
            public static string Time = string.Format("TIME              : {0}", DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));

        }

        public string Type
        {
            get
            {
                return "ApiLogger";
            }
        }
        public string URL { get; set; }
        public string Request { get; set; }
        public List<KeyValuePair<string, string>> RequestHeaderKeyValues { get; set; }
        public string ResponseStatusCode { get; set; }
        public string Response { get; set; }
        public string UserId
        {
            get
            {
                return Convert.ToString(Sitecore.Analytics.Tracker.Current.Contact.ContactId);
            }
        }
        public DateTime DateTime
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}