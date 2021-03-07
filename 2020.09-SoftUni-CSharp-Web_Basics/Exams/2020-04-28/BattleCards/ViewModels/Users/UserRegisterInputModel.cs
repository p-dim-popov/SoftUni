using System.ComponentModel.DataAnnotations;

namespace BattleCards.ViewModels.Users
{
    public class UserRegisterInputModel
    {
        [Required(ErrorMessage = 
            "Username" + ValidationHelper.RequiredErrorMessage)]
        [RegularExpression(@"^\S{5,20}$", ErrorMessage = 
            "Username" + ValidationHelper.RegexErrorMessage + ". Minimum length is 5, maximum length is 20")]
        public string Username { get; set; }

        [Required(ErrorMessage = 
            "Email" + ValidationHelper.RequiredErrorMessage)]
        [EmailAddress(ErrorMessage = 
            ValidationHelper.EmailErrorMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage = 
            "Password" + ValidationHelper.RequiredErrorMessage)]
        [RegularExpression(@"^\S{6,20}$", ErrorMessage = 
            "Password" + ValidationHelper.RegexErrorMessage + ". Minimum length is 6, maximum length is 20")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage =
            "Confirm password" + ValidationHelper.CompareErrorMessage + "password")]
        public string ConfirmPassword { get; set; }
    }
}
