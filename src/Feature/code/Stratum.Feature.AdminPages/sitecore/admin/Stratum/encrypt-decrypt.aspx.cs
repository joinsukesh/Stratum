
namespace Stratum.Feature.AdminPages.sitecore.admin.Stratum
{
    using global::Stratum.Feature.Base.Models;
    using global::Stratum.Foundation.Accounts.Services;
    using global::Stratum.Foundation.Common.Utilities;
    using Sitecore.Diagnostics;
    using System;
    using System.Web.Script.Serialization;
    using System.Web.Services;
    using FA = global::Stratum.Feature.AdminPages;

    public partial class encrypt_decrypt : System.Web.UI.Page
    {
        private AccountsService accountsService = new AccountsService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (accountsService.IsAdministrator())
            {
                try
                {

                }
                catch (Exception ex)
                {
                    Log.Error("", ex, FA.Constants.Stratum_AdminPage_Error);
                }
            }
            else
            {
                Response.Redirect(Constants.Paths.LoginPagePath);
            }
        }

        [WebMethod]
        public static string EncryptString(string inputString, string secretKey)
        {
            AccountsService accountsService = new AccountsService();
            string output = "";
            BaseResponse result = new BaseResponse();

            try
            {
                if (accountsService.IsAuthenticated())
                {
                    if (!string.IsNullOrWhiteSpace(inputString))
                    {
                        result.StatusMessage = StringCipher.EncryptString(inputString, secretKey);
                        result.StatusCode = 1;
                    }                    
                }
                else
                {
                    result.StatusCode = 2;
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex, FA.Constants.Stratum_AdminPage_Error);
                result.StatusCode = 0;
                result.StatusMessage = Constants.GenericError;
                result.ErrorMessage = ex.Message;
            }

            output = new JavaScriptSerializer().Serialize(result);
            return output;
        }

        [WebMethod]
        public static string DecryptString(string inputString, string secretKey)
        {
            AccountsService accountsService = new AccountsService();
            string output = "";
            BaseResponse result = new BaseResponse();

            try
            {
                if (accountsService.IsAuthenticated())
                {
                    if (!string.IsNullOrWhiteSpace(inputString))
                    {
                        result.StatusMessage = StringCipher.DecryptString(inputString, secretKey);
                        result.StatusCode = 1;
                    }
                }
                else
                {
                    result.StatusCode = 2;
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex, FA.Constants.Stratum_AdminPage_Error);
                result.StatusCode = 0;
                result.StatusMessage = Constants.GenericError;
                result.ErrorMessage = ex.Message;
            }

            output = new JavaScriptSerializer().Serialize(result);
            return output;
        }
    }
}