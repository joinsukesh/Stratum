
namespace Stratum.Feature.PageContent.Controllers
{
    using Sitecore;
    using Sitecore.Diagnostics;
    using Stratum.Feature.Base;
    using Stratum.Feature.Base.Controllers;
    using Stratum.Feature.PageContent.Models;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Services;
    using Stratum.Foundation.Common.Utilities;
    using System;
    using System.Web.Mvc;

    public class ColumnsSectionController : Controller
    {
        public ActionResult RenderColumnsSection()
        {
            ColumnsSection viewModel = null;

            try
            {
                viewModel = new ColumnsSection();
                viewModel.SectionId = new CommonService().GetSectionId(Templates.ColumnsSectionRenderingParameters.ID);
                viewModel.Title = new MvcHtmlString(SitecoreUtility.GetRenderingParameter(Templates.ColumnsSectionRenderingParameters.ID, CommonTemplates.BaseContent.Fields.Title));
                viewModel.Description = new MvcHtmlString(SitecoreUtility.GetRenderingParameter(Templates.ColumnsSectionRenderingParameters.ID, CommonTemplates.BaseContent.Fields.Description));

                int columns = MainUtil.GetInt(SitecoreUtility.GetRenderingParameter(Templates.ColumnsSectionRenderingParameters.ID, Templates.ColumnsSectionRenderingParameters.Fields.NumberOfColumns), 1);
                viewModel.NumberOfColumns = (columns < 1 || columns > 12) ? 1 : columns;
                viewModel.ColumnWidth = 12 / viewModel.NumberOfColumns;                
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                return View(BaseConstants.CommonErrorViewPath, ex);
            }

            return View(PageContent.Constants.ViewsFolderPath + "ColumnsSection.cshtml", viewModel);
        }
    }
}