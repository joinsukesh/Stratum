using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Stratum.Feature.Base;
using Stratum.Feature.Base.Controllers;
using Stratum.Feature.Navigation.Models;
using Stratum.Foundation.Common;
using Stratum.Foundation.Common.Utilities;
using System;
using System.Web.Mvc;

namespace Stratum.Feature.Navigation.Controllers
{
    public class NavigationController : Controller
    {
        public ActionResult RenderHeader()
        {
            Header viewModel = null;

            try
            {
                Item datasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (datasourceItem != null && datasourceItem.TemplateID == Templates.Header.ID)
                {
                    viewModel = new Header(datasourceItem);

                    if (viewModel.IsActive)
                    {
                        viewModel.SignOutPageUrl = viewModel.SignOutPageUrl + "?returnUrl=" + System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                    } 
                    else
                    {
                        viewModel = null;
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                return View(BaseConstants.CommonErrorViewPath, ex);
            }

            return View(Constants.ViewsFolderPath + "Header.cshtml", viewModel);
        }

        public ActionResult RenderFooter()
        {
            Footer viewModel = null;

            try
            {
                Item datasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (datasourceItem != null && datasourceItem.TemplateID == Templates.Footer.ID)
                {
                    viewModel = new Footer(datasourceItem);

                    if (!viewModel.IsActive)
                    {
                        viewModel = null;
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                return View(BaseConstants.CommonErrorViewPath, ex);
            }

            return View(Constants.ViewsFolderPath + "Footer.cshtml", viewModel);
        }
    }
}