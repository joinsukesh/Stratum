namespace Stratum.Feature.Accounts.Models
{
    using Stratum.Foundation.Common;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ForgotPassword
    {
        public static string InvalidEmail => CommonDictionaryValues.Messages.Validations.InvalidEmail;
        public static string RequiredEmail => CommonDictionaryValues.Messages.Validations.RequiredEmail;

        [Required(ErrorMessageResourceName = nameof(RequiredEmail), ErrorMessageResourceType = typeof(ForgotPassword))]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceName = nameof(InvalidEmail), ErrorMessageResourceType = typeof(ForgotPassword))]
        public string Email { get; set; }

        [Required]
        public string ResetPasswordPageUrl { get; set; }

        public MvcHtmlString Title { get; set; }
        public MvcHtmlString Description { get; set; }

        public string SectionId { get; set; }
        public string SectionCssClass { get; set; }
    }
}