namespace Stratum.Feature.Accounts.Models
{
    using Stratum.Foundation.Common;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ResetPassword
    {
        public static string InvalidPassword => CommonDictionaryValues.Messages.Validations.InvalidPassword;
        public static string PasswordsMismatch => CommonDictionaryValues.Messages.Validations.PasswordsMismatch; 

        [Required(ErrorMessageResourceName = nameof(InvalidPassword), ErrorMessageResourceType = typeof(ResetPassword))]
        [DataType(DataType.Password)]
        //[StringLength(MinimumLength = 8, ErrorMessageResourceName = nameof(InvalidPassword), ErrorMessageResourceType = typeof(SignUp))]
        ///Password should be of min. 8 characters, with at least 1 number, 1 Uppercase alphabet, 1 lowercase alphabet & at least 1 special character
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8}$", ErrorMessageResourceName = nameof(InvalidPassword), ErrorMessageResourceType = typeof(ResetPassword))]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceName = nameof(PasswordsMismatch), ErrorMessageResourceType = typeof(ResetPassword))]
        [DataType(DataType.Password)]        
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        public string Email { get; set; }

        public MvcHtmlString SuccessMessage { get; set; }

        public MvcHtmlString Title { get; set; }
        public MvcHtmlString Description { get; set; }

        public string SectionId { get; set; }

        public string SectionCssClass { get; set; }
    }
}