namespace Stratum.Feature.Accounts.Models
{
    using Stratum.Foundation.Common;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class SignUp
    {
        public static string InvalidFirstName => CommonDictionaryValues.Messages.Validations.InvalidFirstName;
        public static string InvalidLastName => CommonDictionaryValues.Messages.Validations.InvalidLastName;
        public static string InvalidEmail => CommonDictionaryValues.Messages.Validations.InvalidEmail;
        public static string InvalidPassword => CommonDictionaryValues.Messages.Validations.InvalidPassword;
        public static string PasswordsMismatch => CommonDictionaryValues.Messages.Validations.PasswordsMismatch; 

        [Required(ErrorMessageResourceName = nameof(InvalidFirstName), ErrorMessageResourceType = typeof(SignUp))]
        [MaxLength(50)]
        [MinLength(3)]
        [RegularExpression(@"^[^-\s][a-zA-Z\s]+$", ErrorMessageResourceName = nameof(InvalidFirstName), ErrorMessageResourceType = typeof(SignUp))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = nameof(InvalidLastName), ErrorMessageResourceType = typeof(SignUp))]
        [MaxLength(50)]
        [MinLength(1)]
        [RegularExpression(@"^[^-\s][a-zA-Z\s]+$", ErrorMessageResourceName = nameof(InvalidLastName), ErrorMessageResourceType = typeof(SignUp))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = nameof(InvalidEmail), ErrorMessageResourceType = typeof(SignUp))]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceName = nameof(InvalidEmail), ErrorMessageResourceType = typeof(SignUp))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = nameof(InvalidPassword), ErrorMessageResourceType = typeof(SignUp))]
        [DataType(DataType.Password)]
        //[StringLength(MinimumLength = 8, ErrorMessageResourceName = nameof(InvalidPassword), ErrorMessageResourceType = typeof(SignUp))]
        ///Password should be of min. 8 characters, with at least 1 number, 1 Uppercase alphabet, 1 lowercase alphabet & at least 1 special character
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8}$", ErrorMessageResourceName = nameof(InvalidPassword), ErrorMessageResourceType = typeof(SignUp))]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceName = nameof(PasswordsMismatch), ErrorMessageResourceType = typeof(SignUp))]
        [DataType(DataType.Password)]        
        public string ConfirmPassword { get; set; }

        [Required]
        public string ConfirmSignUpPageUrl { get; set; }

        public MvcHtmlString Title { get; set; }
        public MvcHtmlString Description { get; set; }

        public string SectionId { get; set; }
        public string SectionCssClass { get; set; }
    }
}