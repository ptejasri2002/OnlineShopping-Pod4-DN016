using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;

namespace OnlineShoppingTeam4.ViewModels
{
    /// <summary>
    /// Index view.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Has a password.
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// Logins.
        /// </summary>
        public IList<UserLoginInfo> Logins { get; set; }

        /// <summary>
        /// Phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Two factor log-in.
        /// </summary>
        public bool TwoFactor { get; set; }

        /// <summary>
        /// Browser remember.
        /// </summary>
        public bool BrowserRemembered { get; set; }
    }

    /// <summary>
    /// Manage logins.
    /// </summary>
    public class ManageLoginsViewModel
    {
        /// <summary>
        /// Current login.
        /// </summary>
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        /// <summary>
        /// Others login.
        /// </summary>
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    /// <summary>
    /// Factor view model.
    /// </summary>
    public class FactorViewModel
    {
        /// <summary>
        /// Purpose.
        /// </summary>
        public string Purpose { get; set; }
    }

    /// <summary>
    /// Set password view.
    /// </summary>
    public class SetPasswordViewModel
    {
        /// <summary>
        /// New password.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "{0} trebuie să contină cel putin {2} caractere.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola noua")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirm password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirma noua parola")]
        [Compare("NewPassword", ErrorMessage = "Parola si parola de confirmare nu se potrivesc.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Change password view.
    /// </summary>
    public class ChangePasswordViewModel
    {

        /// <summary>
        /// Old.
        /// </summary>
        [Required(ErrorMessage = "Parola actuala este obligatorie")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola actuala")]
        public string OldPassword { get; set; }

        /// <summary>
        /// New.
        /// </summary>
        [Required(ErrorMessage = "Noua parola este obligatorie")]
        [StringLength(100, ErrorMessage = "{0} trebuie să contină cel putin {2} caractere.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola noua")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirmed password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirma noua parola")]
        [Compare("NewPassword", ErrorMessage = "Parola si parola de confirmare nu se potrivesc.")]
        public string ConfirmPassword { get; set; }
    }
    /// <summary>
    /// Change Email view.
    /// </summary>
    public class ChangeEmailViewModel
    {
        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Acest camp '{0}' este obligatoriu!")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [Required(ErrorMessage = "Acest camp '{0}' este obligatoriu!")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adresa de Email")]
        public string Email { get; set; }
    }
    /// <summary>
    /// Change Email view.
    /// </summary>
    public class ChangeProfileViewModel
    {
        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Acest camp '{0}' este obligatoriu!")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        /// <summary>
        /// The image user.
        /// </summary>
        [Display(Name = "Poza de profil")]
        public HttpPostedFileBase UserImage { get; set; }
    }

    /// <summary>
    /// Add phone number.
    /// </summary>
    public class AddPhoneNumberViewModel
    {
        /// <summary>
        /// Phone number.
        /// </summary>
        [Required]
        [Phone]
        [Display(Name = "Telefon")]
        public string Number { get; set; }
    }

    /// <summary>
    /// Verify phone view.
    /// </summary>
    public class VerifyPhoneNumberViewModel
    {
        /// <summary>
        /// Code.
        /// </summary>
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        /// <summary>
        /// Number of phone.
        /// </summary>
        [Required]
        [Phone]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }
    }

    /// <summary>
    /// Configure TwoFactor View.
    /// </summary>
    public class ConfigureTwoFactorViewModel
    {
        /// <summary>
        /// Provider.
        /// </summary>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// Providers collection.
        /// </summary>
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}