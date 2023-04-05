namespace Stratum.Feature.Forms.Models
{
    using Stratum.Foundation.Common;
    using System.ComponentModel.DataAnnotations;

    public class ContactUsForm
    {
        public static string InvalidName => CommonDictionaryValues.Messages.Validations.InvalidName;
        public static string InvalidEmail => CommonDictionaryValues.Messages.Validations.InvalidEmail;
        public static string InvalidSubject => CommonDictionaryValues.Messages.Validations.InvalidSubject;
        public static string InvalidContactUsMessage => DictionaryValues.Messages.Validations.InvalidContactUsMessage;

        [Required(ErrorMessageResourceName = nameof(InvalidName), ErrorMessageResourceType = typeof(ContactUsForm))]
        [MaxLength(50)]
        [RegularExpression(@"^[^-\s][a-zA-Z\s]+$", ErrorMessageResourceName = nameof(InvalidName), ErrorMessageResourceType = typeof(ContactUsForm))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = nameof(InvalidEmail), ErrorMessageResourceType = typeof(ContactUsForm))]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceName = nameof(InvalidEmail), ErrorMessageResourceType = typeof(ContactUsForm))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = nameof(InvalidSubject), ErrorMessageResourceType = typeof(ContactUsForm))]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceName = nameof(InvalidContactUsMessage), ErrorMessageResourceType = typeof(ContactUsForm))]
        [MaxLength(300)]
        public string Message { get; set; }        
    }
}