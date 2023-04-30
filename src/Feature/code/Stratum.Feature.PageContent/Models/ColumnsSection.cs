namespace Stratum.Feature.PageContent.Models
{
    using System.Web.Mvc;

    public class ColumnsSection
    {
        public string SectionId { get; set; }
        public string SectionCssClass { get; set; }
        public int NumberOfColumns { get; set; }
        public int ColumnWidth { get; set; }
        public MvcHtmlString Title { get; set; }
        public MvcHtmlString Description { get; set; }
        public string ColumnCssClass { get; set; }
    }

}