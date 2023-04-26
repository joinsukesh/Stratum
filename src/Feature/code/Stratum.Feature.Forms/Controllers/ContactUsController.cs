
namespace Stratum.Feature.Forms.Controllers
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;
    using Sitecore.Diagnostics;
    using Stratum.Feature.Base;
    using Stratum.Feature.Forms.Models;
    using Stratum.Foundation.Accounts.Services;
    using Stratum.Foundation.Common;
    using Stratum.Foundation.Forms.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class ContactUsController : Controller
    {
        private readonly ISitecoreFormsRepository _formsRepo;

        public ContactUsController()
        {
            _formsRepo = ServiceLocator.ServiceProvider.GetService<ISitecoreFormsRepository>();
        }

        private AccountsService accountsService = new AccountsService();

        public ActionResult RenderContactUsForm()
        {
            ContactUsForm viewModel = null;

            try
            {
                viewModel = new ContactUsForm();
            }
            catch (Exception ex)
            {
                Log.Error("", ex, this);
                return View(BaseConstants.CommonErrorViewPath, ex);
            }

            return View(Constants.ViewsFolderPath + "ContactUsForm.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveContactUsFormData(ContactUsForm model)
        {
            int statusCode = 0;
            string statusMessage = string.Empty;
            string errorMessage = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    List<KeyValuePair<string, string>> formFieldNameValues = null;
                    Guid formId = new Guid(Constants.Forms.ContactUs.ID);

                    if (formId != null)
                    {
                        formFieldNameValues = new List<KeyValuePair<string, string>>();
                        formFieldNameValues.Add(new KeyValuePair<string, string> ( key: Constants.Forms.ContactUs.Fields.Name, value: model.Name));
                        formFieldNameValues.Add(new KeyValuePair<string, string>(key: Constants.Forms.ContactUs.Fields.Email, value: model.Email));
                        formFieldNameValues.Add(new KeyValuePair<string, string>(key: Constants.Forms.ContactUs.Fields.Subject, value: model.Subject));
                        formFieldNameValues.Add(new KeyValuePair<string, string>(key: Constants.Forms.ContactUs.Fields.Message, value: model.Message));
                        _formsRepo.SaveDataInSitecoreFormsDb(formId, formFieldNameValues, accountsService.GetUserId());
                        statusMessage = DictionaryValues.Messages.Status.ContactUsFormSubmitSuccess;
                        statusCode = 1;
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
    }
}