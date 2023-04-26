
namespace Stratum.Feature.Base.Controllers
{
    using Sitecore.Diagnostics;
    using System;
    using System.Web.Mvc;

    public class BaseController : Controller
    {
        /// <summary>
        /// This BaseController can be inherited to other controllers.
        /// So that in future, if there is a need to trigger some code before the request goes to that controller,
        /// it is possible this way.
        /// </summary>
        /// <param name="context">.</param>
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            try
            {

            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
            }
        }
    }
}