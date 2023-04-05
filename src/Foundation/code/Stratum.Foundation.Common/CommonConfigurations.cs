
namespace Stratum.Foundation.Common
{
    using Sitecore.Configuration;

    /// <summary>
    /// This class is used to read config settings
    /// </summary>
    public class CommonConfigurations
    {
        public static string AppName => Settings.GetSetting("AppName");
        public static string PassPhrase = Settings.GetSetting("PassPhrase");
        public static string Domain = Settings.GetSetting("Domain");
        public static string PageNotFoundPageUrl = Settings.GetSetting("PageNotFoundPageUrl");
    }
}
