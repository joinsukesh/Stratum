namespace Stratum.Feature.Accounts.Controllers
{
    using Sitecore;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Stratum.Feature.Accounts.Models;
    using Stratum.Feature.Base;
    using Stratum.Foundation.Accounts.Services;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Common.Extensions;
    using Stratum.Foundation.Common.Models;
    using Stratum.Foundation.Common.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants = Stratum.Feature.Accounts.Constants;
    using FA = Stratum.Foundation.Accounts;

    public class AccountsController : Controller
    {
        private AccountsService accService = new AccountsService();

        #region SIGN_UP

        public ActionResult RenderSignUpSection()
        {
            SignUp viewModel = null;

            try
            {
                viewModel = new SignUp();

                Item confirmSignUpPageItem = SitecoreUtility.GetItem(SitecoreUtility.GetRenderingParameter(Templates.SignUpRenderingParameters.ID, Templates.SignUpRenderingParameters.Fields.ConfirmSignUpPage));

                if (confirmSignUpPageItem != null)
                {
                    viewModel.ConfirmSignUpPageUrl = confirmSignUpPageItem.Url();
                }

                viewModel.Title = new MvcHtmlString(SitecoreUtility.GetRenderingParameter(Templates.SignUpRenderingParameters.ID, CommonTemplates.BaseContent.Fields.Title));
                viewModel.Description = new MvcHtmlString(SitecoreUtility.GetRenderingParameter(Templates.SignUpRenderingParameters.ID, CommonTemplates.BaseContent.Fields.Description));
                viewModel.SectionId = SitecoreUtility.GetRenderingParameter(Templates.SignUpRenderingParameters.ID, CommonTemplates.BaseRenderingParameters.Fields.SectionId);
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                return View(BaseConstants.CommonErrorViewPath, ex);
            }

            return View(Constants.ViewsFolderPath + "SignUp.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(SignUp model)
        {
            int statusCode = 0;
            string statusMessage = string.Empty;
            string errorMessage = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    bool userAlreadyExists = false;
                    List<KeyValuePair<string, string>> customProfileProperties = new List<KeyValuePair<string, string>>();

                    ///create username
                    string usernameWithDomain = accService.ConvertEmailToUsernameWithDomain(model.Email, CommonConfigurations.Domain);
                    string signUpSecretKey = HelperUtility.GetRandomString(10);
                    customProfileProperties.Add(new KeyValuePair<string, string>(FA.Constants.Profile.ProfileProperties.SignUpSecretKey, signUpSecretKey));
                    customProfileProperties.Add(new KeyValuePair<string, string>(FA.Constants.Profile.ProfileProperties.IsSignUpComplete, "0"));
                    accService.CreateUser(usernameWithDomain, model.FirstName, model.LastName, model.Email, model.Password, customProfileProperties, out userAlreadyExists);

                    if (!userAlreadyExists)
                    {
                        Item signUpEmailTemplateItem = SitecoreUtility.GetItem(Constants.EmailTemplates.SignUpComplete);

                        if (signUpEmailTemplateItem != null)
                        {
                            BaseEmailTemplate bet = new BaseEmailTemplate(signUpEmailTemplateItem);

                            if (bet.IsActive)
                            {
                                ///encrypt the key
                                signUpSecretKey = StringCipher.EncryptString(signUpSecretKey + CommonConstants.Characters.Pipe + usernameWithDomain, CommonConfigurations.PassPhrase);
                                string link = StringUtil.EnsurePostfix(CommonConstants.Characters.ForwardSlashChar, CommonDictionaryValues.Settings.SiteUrl);
                                link = link + model.ConfirmSignUpPageUrl.TrimStart(CommonConstants.Characters.ForwardSlashChar) + "?key=" + signUpSecretKey;
                                string body = bet.Body;
                                body = body.Replace(Constants.Placeholders.NAME, model.FirstName + " " + model.LastName)
                                    .Replace(Constants.Placeholders.LINK, link);
                                SitecoreUtility.SendEmail(bet.FromEmail, model.Email, bet.Subject, body, true, bet.CCEmails, bet.BCCEmails);
                                statusMessage = DictionaryValues.Messages.Status.RegistrationSuccess;
                                statusCode = 1;
                            }
                            else
                            {
                                statusMessage = CommonDictionaryValues.Messages.Errors.Generic;
                                statusCode = 2;
                            }
                        }
                    }
                    else
                    {
                        statusMessage = string.Format(DictionaryValues.Messages.Errors.AccountEmailExists, model.Email);
                        statusCode = 2;
                    }
                }
                else
                {
                    statusCode = 0;
                    statusMessage = CommonDictionaryValues.Messages.Errors.Generic;
                    string modelErrors = string.Join("<br>", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    
                    if (modelErrors.Length > 0)
                    {
                        Log.Error("", new Exception(modelErrors), this);
                        errorMessage = modelErrors;
                    }
                }
            }
            catch (Exception ex)
            {
                statusCode = 0;
                Log.Error("", ex, this);
                statusMessage = CommonDictionaryValues.Messages.Errors.Generic;
                errorMessage = ex.ToString();
            }

            return Json(new { StatusCode = statusCode, StatusMessage = statusMessage, ErrorMessage = errorMessage }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderConfirmSignUpSection(string key)
        {
            bool isValidKey = false;
            ConfirmSignUp viewModel = null;
            string usernameWithDomain = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = StringCipher.DecryptString(key, CommonConfigurations.PassPhrase);

                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        string[] arrKey = key.Split(CommonConstants.Characters.Pipe);

                        if (arrKey != null && arrKey.Length == 2)
                        {
                            string secretKey = arrKey[0];
                            usernameWithDomain = arrKey[1];

                            if (accService.IsValidUser(usernameWithDomain))
                            {
                                string profileSignUpSecretKey = System.Convert.ToString(accService.GetCustomProfilePropertyValue(usernameWithDomain, FA.Constants.Profile.ProfileProperties.SignUpSecretKey));
                                isValidKey = secretKey == profileSignUpSecretKey;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
            }

            if (isValidKey)
            {
                List<KeyValuePair<string, string>> customProfileProperties = new List<KeyValuePair<string, string>>();
                customProfileProperties.Add(new KeyValuePair<string, string>(FA.Constants.Profile.ProfileProperties.IsSignUpComplete, CommonConstants.One));
                accService.SetCustomProfilePropertyValues(usernameWithDomain, customProfileProperties);

                viewModel = new ConfirmSignUp();
                viewModel.Content = new MvcHtmlString(SitecoreUtility.GetRenderingParameter(Templates.ConfirmSignUpRenderingParameters.ID, Templates.ConfirmSignUpRenderingParameters.Fields.Content));
                return View(Constants.ViewsFolderPath + "ConfirmSignUp.cshtml", viewModel);
            }
            else
            {
                return Redirect(CommonConfigurations.PageNotFoundPageUrl);
            }
        }

        #endregion

        #region SIGN_IN

        public ActionResult RenderSignInSection()
        {
            SignIn viewModel = null;

            try
            {
                viewModel = new SignIn();
                viewModel.Title = new MvcHtmlString(SitecoreUtility.GetRenderingParameter(Templates.SignInRenderingParameters.ID, CommonTemplates.BaseContent.Fields.Title));
                viewModel.Description = new MvcHtmlString(SitecoreUtility.GetRenderingParameter(Templates.SignInRenderingParameters.ID, CommonTemplates.BaseContent.Fields.Description));
                viewModel.SectionId = SitecoreUtility.GetRenderingParameter(Templates.SignInRenderingParameters.ID, CommonTemplates.BaseRenderingParameters.Fields.SectionId);
                viewModel.ForgotPasswordLabel = SitecoreUtility.GetRenderingParameter(Templates.SignInRenderingParameters.ID, Templates.SignInRenderingParameters.Fields.ForgotPasswordLabel);

                string forgotPasswordPageId = SitecoreUtility.GetRenderingParameter(Templates.SignInRenderingParameters.ID, Templates.SignInRenderingParameters.Fields.ForgotPasswordPage);

                if (!string.IsNullOrWhiteSpace(forgotPasswordPageId))
                {
                    Item forgotPasswordPageItem = SitecoreUtility.GetItem(forgotPasswordPageId);

                    if (forgotPasswordPageItem != null)
                    {
                        viewModel.ForgotPasswordPageUrl = forgotPasswordPageItem.Url();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                return View(BaseConstants.CommonErrorViewPath, ex);
            }

            return View(Constants.ViewsFolderPath + "SignIn.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignInUser(SignIn model)
        {
            int statusCode = 0;
            string statusMessage = string.Empty;
            string errorMessage = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    bool isSignUpPending = false;

                    ///create username
                    string usernameWithDomain = accService.ConvertEmailToUsernameWithDomain(model.Email, CommonConfigurations.Domain);
                    bool isLoginSuccessful = accService.SignInUser(usernameWithDomain, model.Password, model.RememberMe, out isSignUpPending);

                    if (isLoginSuccessful)
                    {
                        statusCode = 1;
                    }
                    else
                    {
                        if (isSignUpPending)
                        {
                            statusMessage = DictionaryValues.Messages.Errors.IncompleteSignUp;
                        }
                        else
                        {
                            statusMessage = DictionaryValues.Messages.Errors.InvalidSignInCredentials;
                        }

                        statusCode = 2;
                    }
                }
                else
                {
                    statusCode = 0;
                    statusMessage = CommonDictionaryValues.Messages.Errors.Generic;
                    string modelErrors = string.Join("<br>", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    if (modelErrors.Length > 0)
                    {
                        Log.Error("", new Exception(modelErrors), this);
                        errorMessage = modelErrors;
                    }
                }
            }
            catch (Exception ex)
            {
                statusCode = 0;
                Log.Error("", ex, this);
                statusMessage = CommonDictionaryValues.Messages.Errors.Generic;
                errorMessage = ex.ToString();
            }

            return Json(new { StatusCode = statusCode, StatusMessage = statusMessage, ErrorMessage = errorMessage }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region FORGOT_PASSWORD

        public ActionResult RenderForgotPasswordSection()
        {
            ForgotPassword viewModel = null;

            try
            {
                viewModel = new ForgotPassword();
                Item resetPasswordPageItem = SitecoreUtility.GetItem(SitecoreUtility.GetRenderingParameter(Templates.ForgotPasswordRenderingParameters.ID, Templates.ForgotPasswordRenderingParameters.Fields.ResetPasswordPage));
                
                if (resetPasswordPageItem != null)
                {
                    viewModel.ResetPasswordPageUrl = resetPasswordPageItem.Url();
                }

                viewModel.Title = new MvcHtmlString(SitecoreUtility.GetRenderingParameter(Templates.ForgotPasswordRenderingParameters.ID, CommonTemplates.BaseContent.Fields.Title));
                viewModel.Description = new MvcHtmlString(SitecoreUtility.GetRenderingParameter(Templates.ForgotPasswordRenderingParameters.ID, CommonTemplates.BaseContent.Fields.Description));
                viewModel.SectionId = SitecoreUtility.GetRenderingParameter(Templates.ForgotPasswordRenderingParameters.ID, CommonTemplates.BaseRenderingParameters.Fields.SectionId);
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                return View(BaseConstants.CommonErrorViewPath, ex);
            }

            return View(Constants.ViewsFolderPath + "ForgotPassword.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendForgotPasswordEmail(ForgotPassword model)
        {
            int statusCode = 0;
            string statusMessage = string.Empty;
            string errorMessage = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    string usernameWithDomain = accService.ConvertEmailToUsernameWithDomain(model.Email, CommonConfigurations.Domain);

                    if (accService.IsValidUser(usernameWithDomain))
                    {
                        List<KeyValuePair<string, string>> customProfileProperties = new List<KeyValuePair<string, string>>();
                        string forgotPasswordSecretKey = HelperUtility.GetRandomString(10);
                        customProfileProperties.Add(new KeyValuePair<string, string>(FA.Constants.Profile.ProfileProperties.ForgotPasswordSecretKey, forgotPasswordSecretKey));
                        customProfileProperties.Add(new KeyValuePair<string, string>(FA.Constants.Profile.ProfileProperties.HasResetPassword, "0"));
                        accService.SetCustomProfilePropertyValues(usernameWithDomain, customProfileProperties);

                        Item forgotPasswordEmailTemplateItem = SitecoreUtility.GetItem(Constants.EmailTemplates.ResetPassword);

                        if (forgotPasswordEmailTemplateItem != null)
                        {
                            BaseEmailTemplate bet = new BaseEmailTemplate(forgotPasswordEmailTemplateItem);

                            if (bet.IsActive)
                            {
                                ///encrypt the key
                                forgotPasswordSecretKey = StringCipher.EncryptString(forgotPasswordSecretKey + CommonConstants.Characters.Pipe + usernameWithDomain, CommonConfigurations.PassPhrase);
                                string link = StringUtil.EnsurePostfix(CommonConstants.Characters.ForwardSlashChar, CommonDictionaryValues.Settings.SiteUrl);
                                link = link + model.ResetPasswordPageUrl.TrimStart(CommonConstants.Characters.ForwardSlashChar) + "?key=" + forgotPasswordSecretKey;
                                string body = bet.Body;
                                body = body.Replace(Constants.Placeholders.NAME, accService.GetFullName(usernameWithDomain, false))
                                    .Replace(Constants.Placeholders.LINK, link);
                                SitecoreUtility.SendEmail(bet.FromEmail, model.Email, bet.Subject, body, true, bet.CCEmails, bet.BCCEmails);
                                statusMessage = DictionaryValues.Messages.Status.ResetPasswordEmailSent;
                                statusCode = 1;
                            }
                            else
                            {
                                statusMessage = CommonDictionaryValues.Messages.Errors.Generic;
                                statusCode = 2;
                            }
                        }
                    }
                    else
                    {
                        statusMessage = string.Format(DictionaryValues.Messages.Errors.AccountDoesNotExist, model.Email);
                        statusCode = 2;
                    }
                }
                else
                {
                    statusCode = 0;
                    statusMessage = CommonDictionaryValues.Messages.Errors.Generic;
                    string modelErrors = string.Join("<br>", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    
                    if (modelErrors.Length > 0)
                    {
                        Log.Error("", new Exception(modelErrors), this);
                        errorMessage = modelErrors;
                    }
                }
            }
            catch (Exception ex)
            {
                statusCode = 0;
                Log.Error("", ex, this);
                statusMessage = CommonDictionaryValues.Messages.Errors.Generic;
                errorMessage = ex.ToString();
            }

            return Json(new { StatusCode = statusCode, StatusMessage = statusMessage, ErrorMessage = errorMessage }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region RESET_PASSWORD

        public ActionResult RenderResetPasswordSection(string key)
        {
            ResetPassword viewModel = null;
            bool isValidRequest = false;
            string usernameWithDomain = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    key = StringCipher.DecryptString(key, CommonConfigurations.PassPhrase);

                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        string[] arrKey = key.Split(CommonConstants.Characters.Pipe);

                        if (arrKey != null && arrKey.Length == 2)
                        {
                            string secretKey = arrKey[0];
                            usernameWithDomain = arrKey[1];

                            if (accService.IsValidUser(usernameWithDomain))
                            {
                                string forgotPasswordSecretKey = System.Convert.ToString(accService.GetCustomProfilePropertyValue(usernameWithDomain, FA.Constants.Profile.ProfileProperties.ForgotPasswordSecretKey));
                                isValidRequest = secretKey == forgotPasswordSecretKey;

                                ///Also check if the "Has Reset Password" != 1
                                if (isValidRequest)
                                {
                                    isValidRequest = System.Convert.ToString(accService.GetCustomProfilePropertyValue(usernameWithDomain, FA.Constants.Profile.ProfileProperties.HasResetPassword)) != CommonConstants.One;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                return View(BaseConstants.CommonErrorViewPath, ex);
            }

            if (isValidRequest)
            {
                viewModel = new ResetPassword();
                viewModel.Title = new MvcHtmlString(SitecoreUtility.GetRenderingParameter(Templates.ResetPasswordRenderingParameters.ID, CommonTemplates.BaseContent.Fields.Title));
                viewModel.Description = new MvcHtmlString(SitecoreUtility.GetRenderingParameter(Templates.ResetPasswordRenderingParameters.ID, CommonTemplates.BaseContent.Fields.Description));
                viewModel.SectionId = SitecoreUtility.GetRenderingParameter(Templates.ResetPasswordRenderingParameters.ID, CommonTemplates.BaseRenderingParameters.Fields.SectionId);
                viewModel.Email = accService.GetEmail(usernameWithDomain, false);

                string signInPageId = SitecoreUtility.GetRenderingParameter(Templates.ResetPasswordRenderingParameters.ID, Templates.ResetPasswordRenderingParameters.Fields.SignInPage);

                if (!string.IsNullOrWhiteSpace(signInPageId))
                {
                    Item signInPageItem = SitecoreUtility.GetItem(signInPageId);

                    if (signInPageItem != null)
                    {
                        viewModel.SuccessMessage = new MvcHtmlString(string.Format(DictionaryValues.Messages.Status.PasswordResetSuccess, signInPageItem.Url()));
                    }
                }

                return View(Constants.ViewsFolderPath + "ResetPassword.cshtml", viewModel);
            }
            else
            {
                return Redirect(CommonConfigurations.PageNotFoundPageUrl);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword model)
        {
            int statusCode = 0;
            string statusMessage = string.Empty;
            string errorMessage = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    string usernameWithDomain = accService.ConvertEmailToUsernameWithDomain(model.Email, CommonConfigurations.Domain);

                    if (accService.IsValidUser(usernameWithDomain))
                    {
                        List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>(FA.Constants.Profile.ProfileProperties.HasResetPassword, CommonConstants.One)
                        };

                        if (accService.ResetPassword(usernameWithDomain, model.Password, keyValuePairs))
                        {
                            statusCode = 1;
                        }
                        else
                        {
                            statusMessage = CommonDictionaryValues.Messages.Errors.Generic;
                            statusCode = 2;
                        }
                    }
                    else
                    {
                        statusMessage = string.Format(DictionaryValues.Messages.Errors.AccountDoesNotExist, model.Email);
                        statusCode = 2;
                    }
                }
                else
                {
                    statusCode = 0;
                    statusMessage = CommonDictionaryValues.Messages.Errors.Generic;
                    string modelErrors = string.Join("<br>", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    
                    if (modelErrors.Length > 0)
                    {
                        Log.Error("", new Exception(modelErrors), this);
                        errorMessage = modelErrors;
                    }
                }
            }
            catch (Exception ex)
            {
                statusCode = 0;
                Log.Error("", ex, this);
                statusMessage = CommonDictionaryValues.Messages.Errors.Generic;
                errorMessage = ex.ToString();
            }

            return Json(new { StatusCode = statusCode, StatusMessage = statusMessage, ErrorMessage = errorMessage }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SIGN_OUT

        public ActionResult SignOutUser(string returnUrl)
        {
            try
            {
                accService.SignOutUser();
                returnUrl = string.IsNullOrWhiteSpace(returnUrl) ? CommonConstants.Characters.ForwardSlash : returnUrl;
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
            }

            return Redirect(returnUrl);
        }

        #endregion
    }
}