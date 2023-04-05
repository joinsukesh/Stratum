
namespace Stratum.Feature.AdminPages.sitecore.admin.Stratum
{
    using global::Stratum.Foundation.Accounts.Services;
    using global::Stratum.Foundation.Common;
    using System;
    using System.Web;

    public partial class Default : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["logout"] == CommonConstants.One)
            {
                new AccountsService().SignOutUser();
                Response.Redirect(Constants.Paths.LoginPagePath);
            }
            else
            {
                if (ancLogout != null)
                {
                    ancLogout.NavigateUrl = String.Format("{0}?logout=1", HttpContext.Current.Request.Url.AbsoluteUri); 
                }
            }
        }
    }
}