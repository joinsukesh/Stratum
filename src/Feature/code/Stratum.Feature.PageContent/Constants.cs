namespace Stratum.Feature.PageContent
{
    public class Constants
    {
        public static string ViewsFolderPath = "~/Views/Stratum/PageContent/";
        public static string ProductsParentItemId = "{C49D7669-69EE-4282-A463-14A76F64AFA3}";
        
        public struct SearchIndexes
        {
            public static string Products
            {
                get
                {
                    return string.Format("stratum_products_{0}_index", Sitecore.Context.Database.Name);
                }
            }
        }

        public struct Sessions
        {
            public const string GalleryImages = "GalleryImages";
        }
    }
}