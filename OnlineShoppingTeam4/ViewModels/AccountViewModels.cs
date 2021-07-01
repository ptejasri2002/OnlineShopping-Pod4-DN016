using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;
using System.Globalization;
using System.Web.Mvc;
using System.Web;

namespace OnlineShoppingTeam4.ViewModels
{
    /// <summary>
    /// External log in.
    /// </summary>
    public class ExternalLoginConfirmationViewModel
    {
        /// <summary>
        /// Email field.
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    /// <summary>
    /// External log in.
    /// </summary>
    public class ExternalLoginListViewModel
    {
        /// <summary>
        /// Return url to log in.
        /// </summary>
        public string ReturnUrl { get; set; }
    }

    /// <summary>
    /// Send code.
    /// </summary>
    public class SendCodeViewModel
    {
        /// <summary>
        /// Provider.
        /// </summary>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// Providers.
        /// </summary>
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }

        /// <summary>
        /// Return url to log in.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Remember booleand check box.
        /// </summary>
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// Verify code.
    /// </summary>

    public class VerifyCodeViewModel
    {
        /// <summary>
        /// Provider to verify.
        /// </summary>
        [Required]
        public string Provider { get; set; }

        /// <summary>
        /// Code to verify.
        /// </summary>
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        /// <summary>
        /// Return url to verify.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Browser to remember.
        /// </summary>
        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        /// <summary>
        /// Remember booleand check box.
        /// </summary>
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// Forgot view model.
    /// </summary>
    public class ForgotViewModel
    {
        /// <summary>
        /// Email of user.
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    /// <summary>
    /// Login view model.
    /// </summary>
    public class LoginViewModel
    {
        //[Required]
        //[Display(Name = "Email")]
        //[EmailAddress]
        //public string Email { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        [Required(ErrorMessage = "Acest camp '{0}' este obligatoriu!")]
        [Display(Name = "Nume utilizator")]
        public string UserName { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required(ErrorMessage = "Acest camp '{0}' este obligatoriu!")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        /// <summary>
        /// Remember boolean check box.
        /// </summary>
        [Display(Name = "Tine-ma minte")]
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// This Model class is used to retrieve and display user information.
    /// </summary>
    public class UserInfoViewModel
    {
        /// <summary>
        /// User name.
        /// </summary>
        [Display(Name = "Nume utilizator")]
        public string UserName { get; set; }

        /// <summary>
        /// email.
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        /// <summary>
        /// Last time when was activated.
        /// </summary>
        [Display(Name = "Last Active Date/Time")]
        public DateTime LastActiveDateTime { get; set; }
        /// <summary>
        /// Last time when was activated as string.
        /// </summary>
        public string LastActiveString { get; set; }

        /// <summary>
        /// Locked or not.
        /// </summary>
        [Display(Name = "User Locked?")]
        public bool IsLockedOut { get; set; }

        /// <summary>
        /// Online or not.
        /// </summary>
        [Display(Name = "Online?")]
        public bool IsOnline { get; set; }

        
    }
    /// <summary>
    /// This Model class is used to retrieve a new user information and create a new user.
    /// </summary>
    public class RegisterViewModel
    {
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        [Required(ErrorMessage = "Acest camp '{0}' este obligatoriu!")]
        [StringLength(100, ErrorMessage = "{0} trebuie sa aiba minim {2} caractere.", MinimumLength = 5)]
        [Display(Name = "Nume utilizator")]
        public string UserName { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [Required(ErrorMessage = "Acest camp '{0}' este obligatoriu!")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adresa de Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required(ErrorMessage = "Acest camp '{0}' este obligatoriu!")]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        /// <summary>
        /// Seccond password.
        /// </summary>
        [Required(ErrorMessage = "Acest camp '{0}' este obligatoriu!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmare parola")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Cele doua parole introduse nu se potrivesc !")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// The image user.
        /// </summary>
        public HttpPostedFileBase UserImage { get; set; }
    }

    /// <summary>
    /// Reset password view model.
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Email.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "{0} trebuie să contină cel putin {2} caractere.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        /// <summary>
        /// Seccond password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirmare parola")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Cele doua parole introduse nu se potrivesc !")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Check code.
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// Forgot Password View Model.
    /// </summary>
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Email of user.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    /// <summary>
    /// Represents Membership Roles
    /// </summary>
    public class RoleInfoViewModel
    {
        /// <summary>
        /// Role name.
        /// </summary>
        [Display(Name = "Denumire rol")]
        [Required(ErrorMessage = "Acest camp '{0}' este obligatoriu!")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }

    #region Validation

    /// <summary>
    /// Validate password.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute, IClientValidatable
    {
        private const string _defaultErrorMessage = "'{0}' trebuie sa aiba cel putin {1} caractere.";
        private readonly int _minCharacters = Membership.Provider.MinRequiredPasswordLength;

        /// <summary>
        /// Cstr.
        /// </summary>
        public ValidatePasswordLengthAttribute()
            : base(_defaultErrorMessage)
        {
        }

        /// <summary>
        /// Error message.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString,
                name, _minCharacters);
        }

        /// <summary>
        /// Valid or not.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            return (valueAsString != null && valueAsString.Length >= _minCharacters);
        }

        /// <summary>
        /// Get Client Validation Rules.
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[]{
                new ModelClientValidationStringLengthRule(FormatErrorMessage(metadata.GetDisplayName()), _minCharacters, int.MaxValue)
            };
        }
    }

    #endregion

}