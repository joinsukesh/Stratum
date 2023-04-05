namespace Stratum.Feature.Accounts.Models
{
    using Stratum.Foundation.Common;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class SignIn
    {
        public static string InvalidEmail => CommonDictionaryValues.Messages.Validations.InvalidEmail;
        public static string RequiredEmail => CommonDictionaryValues.Messages.Validations.RequiredEmail;
        public static string RequiredPassword => CommonDictionaryValues.Messages.Validations.RequiredPassword;

        [Required(ErrorMessageResourceName = nameof(RequiredEmail), ErrorMessageResourceType = typeof(SignIn))]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceName = nameof(InvalidEmail), ErrorMessageResourceType = typeof(SignIn))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = nameof(RequiredPassword), ErrorMessageResourceType = typeof(SignIn))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
        public string ForgotPasswordPageUrl { get; set; }
        public string ForgotPasswordLabel { get; set; }
        public MvcHtmlString Title { get; set; }
        public MvcHtmlString Description { get; set; }

        public string SectionId { get; set; }
    }
}